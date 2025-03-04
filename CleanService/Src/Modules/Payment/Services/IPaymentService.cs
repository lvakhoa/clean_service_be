using CleanService.Src.Models;
using CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;

namespace CleanService.Src.Modules.Payment.Services;

public interface IPaymentService
{
    Task<string> CreatePaymentLink(Bookings booking);
    
    bool CheckWebhookSignature(string signature, WebhookData body);
    
    Task ConfirmPayment(int orderCode);
    
    Task CancelPayment(int orderCode);
}