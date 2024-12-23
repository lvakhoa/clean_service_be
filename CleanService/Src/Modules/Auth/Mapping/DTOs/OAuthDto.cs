using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class Tokens
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class UserInfo
{
    public string Email { get; set; }
}

public class User
{
    public string EmailAddress { get; set; }
}

public class Session
{
    [JsonPropertyName("object")]
    [JsonProperty("object")]
    public string Object { get; set; }
    
    [JsonPropertyName("id")]
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("user_id")]
    [JsonProperty("user_id")]
    public string UserId { get; set; }
    
    [JsonPropertyName("client_id")]
    [JsonProperty("client_id")]
    public string ClientId { get; set; }
    
    [JsonPropertyName("actor")]
    [JsonProperty("actor")]
    public object? Actor { get; set; }
    
    [JsonPropertyName("status")]
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("last_active_organization_id")]
    [JsonProperty("last_active_organization_id")]
    public object? LastActiveOrganizationId { get; set; }
    
    [JsonPropertyName("last_active_at")]
    [JsonProperty("last_active_at")]
    public int LastActiveAt { get; set; }
    
    [JsonPropertyName("latest_activity")]
    [JsonProperty("latest_activity")]
    public object LatestActivity { get; set; }
    
    [JsonPropertyName("expire_at")]
    [JsonProperty("expire_at")]
    public int ExpireAt { get; set; }
    
    [JsonPropertyName("abandon_at")]
    [JsonProperty("abandon_at")]
    public int AbandonAt { get; set; }
    
    [JsonPropertyName("created_at")]
    [JsonProperty("created_at")]
    public int CreatedAt { get; set; }
    
    [JsonPropertyName("updated_at")]
    [JsonProperty("updated_at")]
    public int UpdatedAt { get; set; }
}