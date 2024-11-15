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
    [JsonProperty("object")]
    public string Object { get; set; }
    
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("user_id")]
    public string UserId { get; set; }
    
    [JsonProperty("client_id")]
    public string ClientId { get; set; }
    
    [JsonProperty("actor")]
    public object Actor { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonProperty("last_active_organization_id")]
    public object LastActiveOrganizationId { get; set; }
    
    [JsonProperty("last_active_at")]
    public DateTime LastActiveAt { get; set; }
    
    [JsonProperty("latest_activity")]
    public object LatestActivity { get; set; }
    
    [JsonProperty("expire_at")]
    public DateTime ExpireAt { get; set; }
    
    [JsonProperty("abandon_at")]
    public object AbandonAt { get; set; }
    
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
}