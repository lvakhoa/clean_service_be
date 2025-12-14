using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Payment.Mapping.DTOs;
using CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;

using WebhookData = CleanService.Src.Modules.Payment.Mapping.DTOs.ZaloPay.WebhookData;

namespace CleanService.Src.Modules.Payment.Services;

public interface IPaymentService
{
    Task<string> CreatePaymentLink(Bookings booking);

    bool CheckWebhookSignature(string signature, BaseWebhookData body);

    Task ConfirmPayment(int orderCode);

    Task CancelPayment(int orderCode);
}
