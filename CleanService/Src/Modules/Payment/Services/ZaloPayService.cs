using System.Globalization;
using System.Text;
using CleanService.Src.Models;
using CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;
using CleanService.Src.Utils;
using CleanService.Src.Utils.Crypto;
using CleanService.Src.Utils.RequestClient;
using Newtonsoft.Json;

namespace CleanService.Src.Modules.Payment.Services;

public class ZaloPayService : IPaymentService
{
    private readonly IRequestClient _requestClient;
    private readonly IConfiguration _configuration;
    private readonly string _appId;
    private readonly string _url;
    private readonly string _key1;

    public ZaloPayService(IRequestClient requestClient, IConfiguration configuration)
    {
        _requestClient = requestClient;
        _configuration = configuration;
        _appId = _configuration["ZaloPay:AppId"]!;
        _url = _configuration["ZaloPay:Url"]!;
        _key1 = _configuration["ZaloPay:Key1"]!;
    }

    public async Task<string> CreatePaymentLink(Bookings booking)
    {
        var items = new List<Dictionary<string, object>>(new[]
        {
            new Dictionary<string, object>
            {
                { "itemid", booking.ServiceTypeId.ToString() },
                { "itename", booking.ServiceType.Name },
                { "itemprice", Convert.ToInt64(decimal.Round(booking.TotalPrice)) },
                { "itemquantity", 1 }
            }
        });

        var requestBody = new Dictionary<string, object>
        {
            { "app_id", int.Parse(_appId) },
            { "app_user", booking.CustomerId },
            { "app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + new Random().Next(1000000) },
            { "app_time", Timestamp.GetTimeStamp(TimeType.Millisecond) },
            { "amount", Convert.ToInt64(decimal.Round(booking.TotalPrice)) },
            { "item", items },
            { "description", "Clean Service" },
            { "bank_code", "zalopayapp" },
            { "embed_data", new { } },
            { "callback_url", "https://clean-service.com/callback" },
        };
        // sort body by alphabet order
        var hmacInput = PaymentUtil.ConvertObjToQueryStr(requestBody,
            new List<string> { "app_id", "app_trans_id", "app_user", "amount", "app_time", "embed_data", "item" }, "|");
        // var hmacInput = _appId + "|" + requestBody["app_trans_id"] + "|" + requestBody["app_user"] + "|" +
        //                 requestBody["amount"] + "|" + requestBody["app_time"] + "|" + requestBody["embed_data"] +
        //                 "|" + requestBody["item"];
        requestBody.Add("mac", HmacHelper.Compute(HmacAlgo.HMACSHA256, _key1, hmacInput));

        var response = await _requestClient.PostAsync(uri: _url + "/create", content:
            new StringContent(JsonConvert.SerializeObject(requestBody)));
        return "";
    }

    public bool CheckWebhookSignature(string signature, WebhookData body)
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