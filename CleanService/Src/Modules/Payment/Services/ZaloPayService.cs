using CleanService.Src.Constant;
using CleanService.Src.Exceptions;
using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Models.Enums;
using CleanService.Src.Modules.Mail.Services;
using CleanService.Src.Modules.Payment.Mapping.DTOs;
using CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;
using CleanService.Src.Modules.Payment.Mapping.DTOs.ZaloPay;
using CleanService.Src.Utils;
using CleanService.Src.Utils.Crypto;
using CleanService.Src.Utils.RequestClient;

using Newtonsoft.Json;

using CreatePaymentLinkDto = CleanService.Src.Modules.Payment.Mapping.DTOs.ZaloPay.CreatePaymentLinkDto;
using HttpClientHandler = CleanService.Src.Utils.HttpClientHandler;
using PaymentItems = CleanService.Src.Modules.Payment.Mapping.DTOs.ZaloPay.PaymentItems;
using PaymentLinkResponseDto = CleanService.Src.Modules.Payment.Mapping.DTOs.ZaloPay.PaymentLinkResponseDto;
using WebhookData = CleanService.Src.Modules.Payment.Mapping.DTOs.ZaloPay.WebhookData;

namespace CleanService.Src.Modules.Payment.Services;

public class ZaloPayService : IPaymentService
{
    private readonly IRequestClient _requestClient;
    private readonly IConfiguration _configuration;
    private readonly HttpClientHandler _httpClientHandler;
    private readonly string _appId;
    private readonly string _url;
    private readonly string _key1;
    private readonly string _clientUrl;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMailService _mailService;

    public ZaloPayService(IRequestClient requestClient, IConfiguration configuration,
        HttpClientHandler httpClientHandler, IUnitOfWork unitOfWork, IMailService mailService)
    {
        _requestClient = requestClient;
        _configuration = configuration;
        _httpClientHandler = httpClientHandler;
        _appId = _configuration["ZaloPay:AppId"]!;
        _url = _configuration["ZaloPay:Url"]!;
        _key1 = _configuration["ZaloPay:Key1"]!;
        _clientUrl = httpClientHandler.getClientUrl();
        _unitOfWork = unitOfWork;
        _mailService = mailService;
    }

    public async Task<string> CreatePaymentLink(Bookings booking)
    {
        DateTime now = DateTime.Now;

        var requestBody = new CreatePaymentLinkDto
        {
            AppId = Int32.Parse(_appId),
            AppUser = booking.Customer.FullName,
            AppTransId = now.ToString("yyMMdd") + "_" + booking.OrderId,
            AppTime = Timestamp.GetTimeStamp(now, TimeType.Millisecond),
            ExpireDurationSeconds = 900,
            Amount = Convert.ToInt32(decimal.Round(booking.TotalPrice)),
            Item = JsonConvert.SerializeObject(new List<PaymentItems>()
            {
                new()
                {
                    ItemId = booking.ServiceType.Name,
                    Name = booking.ServiceType.Name,
                    Quantity = 1,
                    Price = Convert.ToInt32(decimal.Round(booking.TotalPrice))
                }
            }),
            Description = booking.ServiceType.Name,
            EmbedData = JsonConvert.SerializeObject(new EmbedData()
            {
                RedirectUrl = $"{_configuration["APP_URL"]}/customer/booking/payment-result",
            }),
            // BankCode = "zalopayapp",
            CallbackUrl = $"{_configuration["API_URL"]}/api/payment/callback",
        };

        var requestBodyDict = PaymentUtil.ConvertObjToDict(requestBody);

        // sort body by alphabet order
        var hmacInput = PaymentUtil.ConvertObjToQueryStr(requestBodyDict,
            new List<string>
            {
                "app_id",
                "app_trans_id",
                "app_user",
                "amount",
                "app_time",
                "embed_data",
                "item"
            }, "|", true);
        requestBody.Mac = HmacHelper.Compute(HmacAlgo.HMACSHA256, _key1, hmacInput);

        try
        {
            var response = await _requestClient.PostAsync<PaymentLinkResponseDto>(uri: _url + "/create",
                content: new StringContent(JsonConvert.SerializeObject(requestBody)), contentType: "application/json");
            return response.OrderUrl;
        }
        catch (Exception e)
        {
            throw new UnprocessableRequestException("Failed to create payment link", new[] { e.Message },
                ExceptionConvention.CannotCreatePayment);
        }
    }

    public bool CheckWebhookSignature(string signature, BaseWebhookData body)
    {
        throw new NotImplementedException();
    }

    public async Task ConfirmPayment(int orderCode)
    {
        var booking = await _unitOfWork.Repository<Bookings, PartialBookings>()
            .GetFirstAsync(BookingSpecification.GetBookingByOrderIdSpec(orderCode));
        if (booking == null) return;

        booking.PaymentStatus = PaymentStatus.Paid;
        await _unitOfWork.SaveChangesAsync();

        // Send email to customer
        try
        {
            var customerEmailBody = $@"
                <html>
                <body>
                    <h2>✅ Payment Successful</h2>
                    <p>Dear {booking.Customer.FullName},</p>
                    <p>Your payment has been successfully processed and your booking is now confirmed!</p>
                    <h3>Booking Details:</h3>
                    <ul>
                        <li><strong>Order ID:</strong> {booking.OrderId}</li>
                        <li><strong>Service:</strong> {booking.ServiceType.Name}</li>
                        <li><strong>Scheduled Start:</strong> {booking.ScheduledStartTime:yyyy-MM-dd HH:mm}</li>
                        <li><strong>Scheduled End:</strong> {booking.ScheduledEndTime:yyyy-MM-dd HH:mm}</li>
                        <li><strong>Total Price:</strong> ${booking.TotalPrice:F2}</li>
                        <li><strong>Payment Status:</strong> Paid ✓</li>
                        <li><strong>Booking Status:</strong> {booking.Status}</li>
                    </ul>
                    <p>Our helper will arrive at your location at the scheduled time. You will receive their contact information shortly.</p>
                    <p>Thank you for choosing our cleaning service!</p>
                </body>
                </html>
            ";

            await _mailService.SendMail(booking.Customer.Email, "Payment Successful - Order #" + booking.OrderId,
                customerEmailBody);
        }
        catch (Exception ex)
        {
            // Log the error but don't fail the booking creation
            Console.WriteLine($"Failed to send email to customer: {ex.Message}");
        }

        // Send email to helper
        if (booking.HelperId != null)
        {
            try
            {
                var helperSpec = UserSpecification.GetUserByIdSpec(booking.HelperId);
                var helper = await _unitOfWork.Repository<Users, PartialUsers>().GetFirstAsync(helperSpec);

                if (helper != null)
                {
                    var helperEmailBody = $@"
                        <html>
                        <body>
                            <h2>New Booking Assignment</h2>
                            <p>Dear {helper.FullName},</p>
                            <p>You have been assigned to a new booking!</p>
                            <h3>Booking Details:</h3>
                            <ul>
                                <li><strong>Order ID:</strong> {booking.OrderId}</li>
                                <li><strong>Service:</strong> {booking.ServiceType.Name}</li>
                                <li><strong>Customer:</strong> {booking.Customer.FullName}</li>
                                <li><strong>Customer Phone:</strong> {booking.Customer.PhoneNumber ?? "N/A"}</li>
                                <li><strong>Address:</strong> {booking.Location}</li>
                                <li><strong>Scheduled Start:</strong> {booking.ScheduledStartTime:yyyy-MM-dd HH:mm}</li>
                                <li><strong>Scheduled End:</strong> {booking.ScheduledEndTime:yyyy-MM-dd HH:mm}</li>
                                <li><strong>Status:</strong> {booking.Status}</li>
                            </ul>
                            <p>Please be ready for this assignment at the scheduled time.</p>
                            <p>Thank you!</p>
                        </body>
                        </html>
                    ";

                    await _mailService.SendMail(helper.Email, "New Booking Assignment - Order #" + booking.OrderId,
                        helperEmailBody);
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't fail the booking creation
                Console.WriteLine($"Failed to send email to helper: {ex.Message}");
            }
        }
    }

    public async Task CancelPayment(int orderCode)
    {
        var booking = await _unitOfWork.Repository<Bookings, PartialBookings>()
            .GetFirstAsync(BookingSpecification.GetBookingByOrderIdSpec(orderCode));
        if (booking == null)
            throw new NotFoundException("Booking not found with order code " + orderCode, ExceptionConvention.NotFound);

        booking.Status = BookingStatus.Cancelled;
        booking.CancellationReason = "Payment canceled";
        booking.HelperId = null;
        await _unitOfWork.SaveChangesAsync();
    }
}
