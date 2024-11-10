using System.Net;
using System.Text;
using CleanService.Src.Models;
using CleanService.Src.Modules.Payment.Infrastructures;
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
    private readonly IPaymentUnitOfWork _paymentUnitOfWork;

    public PayOsService(IRequestClient requestClient, IConfiguration configuration,
        IPaymentUnitOfWork paymentUnitOfWork)
    {
        _requestClient = requestClient;
        _configuration = configuration;
        _clientId = _configuration["PayOS:ClientId"]!;
        _apiKey = _configuration["PayOS:ApiKey"]!;
        _checksumKey = _configuration["PayOS:ChecksumKey"]!;
        _url = _configuration["PayOS:Url"]!;
        _paymentUnitOfWork = paymentUnitOfWork;
    }

    public async Task<string> CreatePaymentLink(Bookings booking)
    {
        // var items = new List<Dictionary<string, object>>(new[]
        // {
        //     new Dictionary<string, object>
        //     {
        //         { "name", booking.ServiceType.Name },
        //         { "quantity", 1 },
        //         { "price", Convert.ToInt32(decimal.Round(booking.TotalPrice)) }
        //     }
        // });
        //
        // var requestBody = new Dictionary<string, object>
        // {
        //     { "orderCode", new Random().Next(1, 100) },
        //     { "amount", Convert.ToInt32(decimal.Round(booking.TotalPrice)) },
        //     { "description", "Clean" },
        //     { "buyerName", booking.Customer.FullName },
        //     { "buyerEmail", booking.Customer.Email },
        //     { "buyerPhone", booking.Customer.PhoneNumber ?? "" },
        //     { "buyerAddress", booking.Customer.Address ?? "" },
        //     { "items", items },
        //     { "cancelUrl", "https://your-cancel-url.com" },
        //     { "returnUrl", "https://your-success-url.com" },
        //     { "expiredAt", Timestamp.GetTimeStamp(DateTime.Now.AddMinutes(20)) },
        // };

        var requestBody = new CreatePaymentLinkDto
        {
            OrderCode = booking.OrderId,
            Amount = Convert.ToInt32(decimal.Round(booking.TotalPrice)),
            Description = booking.Location,
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
            CancelUrl = "https://your-cancel-url.com", // Change later
            ReturnUrl = "https://your-success-url.com", // Change later
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
        var response = await _requestClient.PostAsync<PaymentLinkResponseDto>(uri: _url + "/payment-requests", content:
            new StringContent(JsonConvert.SerializeObject(requestBody)), contentType: "application/json",
            headers: headers);

        return response.Data.CheckoutUrl;
    }

    public async Task ConfirmPayment(int orderCode)
    {
        var booking = await _paymentUnitOfWork.BookingRepository.FindOneAsync(entity => entity.OrderId == orderCode);
        if (booking == null)
            return;

        booking.PaymentStatus = PaymentStatus.Paid;
        await _paymentUnitOfWork.SaveChangesAsync();
    }
}