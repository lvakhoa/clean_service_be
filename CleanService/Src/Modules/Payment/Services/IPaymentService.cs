using CleanService.Src.Models;

namespace CleanService.Src.Modules.Payment.Services;

public interface IPaymentService
{
    Task<string> CreatePaymentLink(Bookings booking);
    
    Task ConfirmPayment(int orderCode);
}