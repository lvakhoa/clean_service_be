using System.Net;

using CleanService.Src.Common;
using CleanService.Src.Constant;
using CleanService.Src.Modules.Payment.Mapping.DTOs.ZaloPay;
using CleanService.Src.Modules.Payment.Services;
using CleanService.Src.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Payment;

public class PaymentController : ApiController
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("callback")]
    public async Task<ActionResult> ConfirmPayment([FromBody] dynamic webhookReceivingDto)
    {
        // var isValid = _paymentService.CheckWebhookSignature(webhookReceivingDto.Signature, webhookReceivingDto.Data);
        // if (isValid)
        // {
        //     await _paymentService.ConfirmPayment(webhookReceivingDto.Data.OrderCode);
        // }

        // _logger.LogInformation("Webhook received: {@webhookReceivingDto}", webhookReceivingDto);

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
