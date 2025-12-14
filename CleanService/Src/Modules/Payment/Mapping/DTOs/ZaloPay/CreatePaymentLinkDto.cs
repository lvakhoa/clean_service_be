using System.Text.Json.Serialization;

using Newtonsoft.Json;

namespace CleanService.Src.Modules.Payment.Mapping.DTOs.ZaloPay;

public class CreatePaymentLinkDto
{
    [JsonPropertyName("app_id")]
    [JsonProperty("app_id")]
    public int AppId { get; set; }

    [JsonPropertyName("app_user")]
    [JsonProperty("app_user")]
    public string AppUser { get; set; } = string.Empty;

    [JsonPropertyName("app_trans_id")]
    [JsonProperty("app_trans_id")]
    public string AppTransId { get; set; } = string.Empty;

    [JsonPropertyName("app_time")]
    [JsonProperty("app_time")]
    public long AppTime { get; set; }

    [JsonPropertyName("expire_duration_seconds")]
    [JsonProperty("expire_duration_seconds")]
    public int ExpireDurationSeconds { get; set; }

    [JsonPropertyName("amount")]
    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("item")]
    [JsonProperty("item")]
    public string Item { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("bank_code")]
    [JsonProperty("bank_code")]
    public string BankCode { get; set; } = string.Empty;

    [JsonPropertyName("mac")]
    [JsonProperty("mac")]
    public string Mac { get; set; } = string.Empty;

    [JsonPropertyName("embed_data")]
    [JsonProperty("embed_data")]
    public string EmbedData { get; set; } = string.Empty;

    [JsonPropertyName("callback_url")]
    [JsonProperty("callback_url")]
    public string CallbackUrl { get; set; } = string.Empty;
}

public class PaymentItems
{
    [JsonPropertyName("itemid")]
    [JsonProperty("itemid")]
    public string ItemId { get; set; } = null!;

    [JsonPropertyName("itename")]
    [JsonProperty("itename")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("itemquantity")]
    [JsonProperty("itemquantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("itemprice")]
    [JsonProperty("itemprice")]
    public int Price { get; set; }
}

public class EmbedData
{
    [JsonPropertyName("redirecturl")]
    [JsonProperty("redirecturl")]
    public string RedirectUrl { get; set; } = string.Empty;
}
