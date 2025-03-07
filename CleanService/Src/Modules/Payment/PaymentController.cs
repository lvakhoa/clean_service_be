using System.Net;

using CleanService.Src.Common;
using CleanService.Src.Constant;
using CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;
using CleanService.Src.Modules.Payment.Services;
using CleanService.Src.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Payment;

public class PaymentController : ApiController
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("confirm")]
    public async Task<ActionResult> ConfirmPayment([FromBody] WebhookReceivingDto webhookReceivingDto)
    {
        var isValid = _paymentService.CheckWebhookSignature(webhookReceivingDto.Signature, webhookReceivingDto.Data);
        if (isValid)
        {
            await _paymentService.ConfirmPayment(webhookReceivingDto.Data.OrderCode);
        }

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Payment confirmed successfully"
        });
    }

    [HttpPatch("cancel/{orderId:int}")]
    public async Task<ActionResult> CancelPayment(int orderId)
    {
        await _paymentService.CancelPayment(orderId);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Payment canceled successfully"
        });
    }
}
