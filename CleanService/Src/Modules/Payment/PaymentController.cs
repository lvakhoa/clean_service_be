using System.Net;
using CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;
using CleanService.Src.Modules.Payment.Services;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Payment;

[Route("[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("confirm")]
    public async Task<ActionResult> ConfirmPayment([FromBody] WebhookReceivingDto webhookReceivingDto)
    {
        await _paymentService.ConfirmPayment(webhookReceivingDto.Data.OrderCode);

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Payment confirmed successfully"
        });
    }
}