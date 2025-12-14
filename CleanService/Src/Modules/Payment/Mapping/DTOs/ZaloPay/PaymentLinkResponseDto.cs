using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CleanService.Src.Modules.Payment.Mapping.DTOs.ZaloPay;

public class PaymentLinkResponseDto
{
    [JsonPropertyName("return_code")]
    [JsonProperty("return_code")]
    public int ReturnCode { get; set; }

    [JsonPropertyName("return_message")]
    [JsonProperty("return_message")]
    public string ReturnMessage { get; set; } = string.Empty;

    [JsonPropertyName("sub_return_code")]
    [JsonProperty("sub_return_code")]
    public int SubReturnCode { get; set; }

    [JsonPropertyName("sub_return_message")]
    [JsonProperty("sub_return_message")]
    public string SubReturnMessage { get; set; } = string.Empty;

    [JsonPropertyName("zp_trans_token")]
    [JsonProperty("zp_trans_token")]
    public string ZpTransToken { get; set; } = string.Empty;

    [JsonPropertyName("order_url")]
    [JsonProperty("order_url")]
    public string OrderUrl { get; set; } = string.Empty;

    [JsonPropertyName("order_token")]
    [JsonProperty("order_token")]
    public string OrderToken { get; set; } = string.Empty;
}
