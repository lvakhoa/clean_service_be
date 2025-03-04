using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;

public class WebhookReceivingDto
{
    [JsonPropertyName("code")]
    [JsonProperty("code")]
    public string Code { get; set; } = null!;

    [JsonPropertyName("desc")]
    [JsonProperty("desc")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("success")]
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonPropertyName("data")]
    [JsonProperty("data")]
    public WebhookData Data { get; set; } = null!;

    [JsonPropertyName("signature")]
    [JsonProperty("signature")]
    public string Signature { get; set; } = null!;
}

public class WebhookData
{
    [JsonPropertyName("orderCode")]
    [JsonProperty("orderCode")]
    public int OrderCode { get; set; }

    [JsonPropertyName("amount")]
    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("description")]
    [JsonProperty("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("accountNumber")]
    [JsonProperty("accountNumber")]
    public string AccountNumber { get; set; } = null!;

    [JsonPropertyName("reference")]
    [JsonProperty("reference")]
    public string Reference { get; set; } = null!;

    [JsonPropertyName("transactionDateTime")]
    [JsonProperty("transactionDateTime")]
    public string TransactionDateTime { get; set; } = null!;

    [JsonPropertyName("currency")]
    [JsonProperty("currency")]
    public string Currency { get; set; } = null!;

    [JsonPropertyName("paymentLinkId")]
    [JsonProperty("paymentLinkId")]
    public string PaymentLinkId { get; set; } = null!;

    [JsonPropertyName("code")]
    [JsonProperty("code")]
    public string Code { get; set; } = null!;

    [JsonPropertyName("desc")]
    [JsonProperty("desc")]
    public string Desc { get; set; } = null!;

    [JsonPropertyName("counterAccountBankId")]
    [JsonProperty("counterAccountBankId")]
    public string CounterAccountBankId { get; set; } = null!;

    [JsonPropertyName("counterAccountBankName")]
    [JsonProperty("counterAccountBankName")]
    public string CounterAccountBankName { get; set; } = null!;

    [JsonPropertyName("counterAccountName")]
    [JsonProperty("counterAccountName")]
    public string CounterAccountName { get; set; } = null!;

    [JsonPropertyName("counterAccountNumber")]
    [JsonProperty("counterAccountNumber")]
    public string CounterAccountNumber { get; set; } = null!;

    [JsonPropertyName("virtualAccountName")]
    [JsonProperty("virtualAccountName")]
    public string VirtualAccountName { get; set; } = null!;

    [JsonPropertyName("virtualAccountNumber")]
    [JsonProperty("virtualAccountNumber")]
    public string VirtualAccountNumber { get; set; } = null!;
}