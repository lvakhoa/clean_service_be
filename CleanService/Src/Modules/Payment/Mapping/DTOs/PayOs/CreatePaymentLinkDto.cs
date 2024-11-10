using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CleanService.Src.Modules.Payment.Mapping.DTOs.PayOs;

public class CreatePaymentLinkDto
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

    [JsonPropertyName("buyerName")]
    [JsonProperty("buyerName")]
    public string BuyerName { get; set; } = null!;

    [JsonPropertyName("buyerEmail")]
    [JsonProperty("buyerEmail")]
    public string BuyerEmail { get; set; } = null!;

    [JsonPropertyName("buyerPhone")]
    [JsonProperty("buyerPhone")]
    public string BuyerPhone { get; set; } = null!;

    [JsonPropertyName("buyerAddress")]
    [JsonProperty("buyerAddress")]
    public string BuyerAddress { get; set; } = null!;

    [JsonPropertyName("items")]
    [JsonProperty("items")]
    public List<PaymentItems> Items { get; set; } = new List<PaymentItems>();

    [JsonPropertyName("cancelUrl")]
    [JsonProperty("cancelUrl")]
    public string CancelUrl { get; set; } = null!;

    [JsonPropertyName("returnUrl")]
    [JsonProperty("returnUrl")]
    public string ReturnUrl { get; set; } = null!;

    [JsonPropertyName("expiredAt")]
    [JsonProperty("expiredAt")]
    public long ExpiredAt { get; set; }
    
    [JsonPropertyName("signature")]
    [JsonProperty("signature")]
    public string Signature { get; set; } = null!;
}

public class PaymentItems
{
    [JsonPropertyName("name")]
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("quantity")]
    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("price")]
    [JsonProperty("price")]
    public int Price { get; set; }
}