using System.Net;
using System.Text;

using CleanService.Src.Constant;
using CleanService.Src.Exceptions;
using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models;
using CleanService.Src.Models.Configurations;
using CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;
using CleanService.Src.Utils;
using CleanService.Src.Utils.Crypto;
using CleanService.Src.Utils.RequestClient;

using Newtonsoft.Json;

namespace CleanService.Src.Modules.Payment.Services;

public class PayOsService : IPaymentService
{
    private readonly IRequestClient _requestClient;
    private readonly IConfiguration _configuration;
    private readonly string _clientId;
    private readonly string _apiKey;
    private readonly string _checksumKey;
    private readonly string _url;
    private readonly IUnitOfWork _unitOfWork;

    public PayOsService(IRequestClient requestClient, IConfiguration configuration,
        IUnitOfWork unitOfWork)
    {
        _requestClient = requestClient;
        _configuration = configuration;
        _clientId = _configuration["PayOS:ClientId"]!;
        _apiKey = _configuration["PayOS:ApiKey"]!;
        _checksumKey = _configuration["PayOS:ChecksumKey"]!;
        _url = _configuration["PayOS:Url"]!;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> CreatePaymentLink(Bookings booking)
    {
        var requestBody = new CreatePaymentLinkDto
        {
            OrderCode = booking.OrderId,
            Amount = Convert.ToInt32(decimal.Round(booking.TotalPrice)),
            Description = booking.ServiceType.Name,
            BuyerName = booking.Customer.FullName,
            BuyerEmail = booking.Customer.Email,
            BuyerPhone = booking.Customer.PhoneNumber ?? "",
            BuyerAddress = booking.Customer.Address ?? "",
            Items = new List<PaymentItems>()
            {
                new()
                {
                    Name = booking.ServiceType.Name,
                    Quantity = 1,
                    Price = Convert.ToInt32(decimal.Round(booking.TotalPrice))
                }
            },
            CancelUrl = "http://localhost:3000/payment-cancelled", // Change later
            ReturnUrl = "http://localhost:3000/payment-success", // Change later
            ExpiredAt = Timestamp.GetTimeStamp(DateTime.Now.AddMinutes(20))
        };
        var requestBodyDict = PaymentUtil.ConvertObjToDict(requestBody);

        // sort body by alphabet order
        var hmacInput = PaymentUtil.ConvertObjToQueryStr(requestBodyDict,
            new List<string> { "amount", "cancelUrl", "description", "orderCode", "returnUrl" }, "&");
        requestBody.Signature = HmacHelper.Compute(HmacAlgo.HMACSHA256, _checksumKey, hmacInput);

        var headers = new Dictionary<string, object>
        {
            { "x-client-id", _clientId },
            { "x-api-key", _apiKey },
        };
        try
        {
            var response = await _requestClient.PostAsync<PaymentLinkResponseDto>(uri: _url + "/payment-requests",
                content:
                new StringContent(JsonConvert.SerializeObject(requestBody)), contentType: "application/json",
                headers: headers);
            return response.Data.CheckoutUrl;
        }
        catch (Exception e)
        {
            throw new UnprocessableRequestException("Failed to create payment link", new[] { e.Message },
                ExceptionConvention.CannotCreatePayment);
        }
    }

    public bool CheckWebhookSignature(string signature, WebhookData body)
    {
        var requestBodyDict = PaymentUtil.ConvertObjToDict(body);
        var hmacInput = PaymentUtil.ConvertObjToQueryStr(requestBodyDict,
            new List<string>
            {
                "orderCode", "amount", "description", "accountNumber", "reference", "transactionDateTime",
                "currency", "paymentLinkId", "code", "desc", "counterAccountBankId", "counterAccountBankName",
                "counterAccountName", "counterAccountNumber", "virtualAccountName", "virtualAccountNumber"
            }, "&");
        var dataSignature = HmacHelper.Compute(HmacAlgo.HMACSHA256, _checksumKey, hmacInput);
        return dataSignature == signature;
    }

    public async Task ConfirmPayment(int orderCode)
    {
        var booking = await _unitOfWork.Repository<Bookings, PartialBookings>().GetFirstAsync(BookingSpecification.GetBookingByOrderIdSpec(orderCode));
        if (booking == null)
            return;

        booking.PaymentStatus = PaymentStatus.Paid;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CancelPayment(int orderCode)
    {
        var booking = await _unitOfWork.Repository<Bookings, PartialBookings>().GetFirstAsync(BookingSpecification.GetBookingByOrderIdSpec(orderCode));
        if (booking == null)
            throw new NotFoundException("Booking not found with order code " + orderCode,
                ExceptionConvention.NotFound);

        booking.Status = BookingStatus.Cancelled;
        booking.CancellationReason = "Payment canceled";
        booking.HelperId = null;
        await _unitOfWork.SaveChangesAsync();
    }
}
