using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Payment.Mapping.DTOs;
using CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;

namespace CleanService.Src.Modules.Payment.Services;

public class VnPayService : IPaymentService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly string _tmnCode;
    private readonly string _hashSecret;
    private readonly string _vnpUrl;

    public VnPayService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _httpClient = _httpClientFactory.CreateClient();
        _tmnCode = _configuration["VnPay:TmnCode"]!;
        _hashSecret = _configuration["VnPay:HashSecret"]!;
        _vnpUrl = _configuration["VnPay:Url"]!;
    }


    public Task<string> CreatePaymentLink(Bookings booking)
    {
         throw new NotImplementedException();
    }

    public bool CheckWebhookSignature(string signature, BaseWebhookData body)
    {
        throw new NotImplementedException();
    }

    public Task ConfirmPayment(int orderCode)
    {
        throw new NotImplementedException();
    }

    public Task CancelPayment(int orderCode)
    {
        throw new NotImplementedException();
    }
}
