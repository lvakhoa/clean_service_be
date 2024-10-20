namespace CleanService.Src.Constant;

public static class AuthProvider
{
    public const string Provider = "Clerk";
    public const string CallbackPath = "/oauth/callback";
    public const string ClaimNameIdentifier = "user_id";
    public const string ClaimName = "name";
    public const string ClaimEmail = "email";
    public const string ClaimRole = "role";
    public static string AuthorizationEndpoint(string providerDomain) => $"{providerDomain}/oauth/authorize";
    public static string TokenEndpoint(string providerDomain) => $"{providerDomain}/oauth/token";
    public static string UserInformationEndpoint(string providerDomain) => $"{providerDomain}/oauth/userinfo";
};