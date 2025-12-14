using System.Text.Json.Serialization;

using Newtonsoft.Json;

namespace CleanService.Src.Modules.Payment.Mapping.DTOs.ZaloPay;

public class WebhookReceivingDto
{
    [JsonPropertyName("data")]
    [JsonProperty("data")]
    public WebhookData Data { get; set; } = new WebhookData();

    [JsonPropertyName("mac")]
    [JsonProperty("mac")]
    public string Mac { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [JsonProperty("type")]
    public int Type { get; set; }
}

public class WebhookData : BaseWebhookData
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

    [JsonPropertyName("amount")]
    [JsonProperty("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("item")]
    [JsonProperty("item")]
    public string Item { get; set; } = string.Empty;

    [JsonPropertyName("embed_data")]
    [JsonProperty("embed_data")]
    public string EmbedData { get; set; } = string.Empty;

    [JsonPropertyName("zp_trans_id")]
    [JsonProperty("zp_trans_id")]
    public long ZpTransId { get; set; }

    [JsonPropertyName("server_time")]
    [JsonProperty("server_time")]
    public long ServerTime { get; set; }

    [JsonPropertyName("channel")]
    [JsonProperty("channel")]
    public int Channel { get; set; }

    [JsonPropertyName("merchant_user_id")]
    [JsonProperty("merchant_user_id")]
    public string MerchantUserId { get; set; } = string.Empty;

    [JsonPropertyName("user_fee_amount")]
    [JsonProperty("user_fee_amount")]
    public long UserFeeAmount { get; set; }

    [JsonPropertyName("discount_amount")]
    [JsonProperty("discount_amount")]
    public long DiscountAmount { get; set; }
}
