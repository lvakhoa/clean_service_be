using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;

public class PaymentLinkResponseDto
{
    [JsonPropertyName("code")]
    [JsonProperty("code")]
    public string Code { get; set; } = null!;

    [JsonPropertyName("desc")]
    [JsonProperty("desc")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("data")]
    [JsonProperty("data")]
    public PaymentLinkData Data { get; set; } = null!;

    [JsonPropertyName("signature")]
    [JsonProperty("signature")]
    public string Signature { get; set; } = null!;
}

public class PaymentLinkData
{
    [JsonPropertyName("bin")]
    [JsonProperty("bin")]
    public string Bin { get; set; } = null!;

    [JsonPropertyName("accountNumber")]
    [JsonProperty("accountNumber")]
    public string AccountNumber { get; set; } = null!;

    [JsonPropertyName("accountName")]
    [JsonProperty("accountName")]
    public string AccountName { get; set; } = null!;

    [JsonPropertyName("amount")]
    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("description")]
    [JsonProperty("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("orderCode")]
    [JsonProperty("orderCode")]
    public int OrderCode { get; set; }

    [JsonPropertyName("curency")]
    [JsonProperty("curency")]
    public string Currency { get; set; } = null!;

    [JsonPropertyName("paymentLinkId")]
    [JsonProperty("paymentLinkId")]
    public string PaymentLinkId { get; set; } = null!;

    [JsonPropertyName("status")]
    [JsonProperty("status")]
    public string Status { get; set; } = null!;

    [JsonPropertyName("checkoutUrl")]
    [JsonProperty("checkoutUrl")]
    public string CheckoutUrl { get; set; } = null!;

    [JsonPropertyName("qrCode")]
    [JsonProperty("qrCode")]
    public string QrCode { get; set; } = null!;
}