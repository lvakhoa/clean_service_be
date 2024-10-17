using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using CleanService.Src.Constant;
using CleanService.Src.Modules.Auth.Repositories;
using CleanService.Src.Modules.Auth.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;

namespace CleanService.Src.Modules.Auth;

public static class AuthModule
{
    public static IServiceCollection AddAuthScheme(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = AuthProvider.Provider;
        }).AddCookie().AddOAuth(AuthProvider.Provider, options =>
        {
            var providerDomain =
                config.GetValue<string>("OAuthProvider:Domain");

            options.ClientId = config.GetValue<string>(
                "OAuthProvider:ClientId")!;
            options.ClientSecret =
                config.GetValue<string>(
                    "OAuthProvider:ClientSecret")!;

            options.AuthorizationEndpoint = AuthProvider.AuthorizationEndpoint(providerDomain!);

            options.TokenEndpoint = AuthProvider.TokenEndpoint(providerDomain!);

            options.CallbackPath = new PathString(AuthProvider.CallbackPath);

            options.Scope.Add("profile");
            options.Scope.Add("email");

            options.SaveTokens = true;

            options.UserInformationEndpoint = AuthProvider.UserInformationEndpoint(providerDomain!);

            options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier,
                AuthProvider.ClaimNameIdentifier);
            options.ClaimActions.MapJsonKey(ClaimTypes.Name, AuthProvider.ClaimName);
            options.ClaimActions.MapJsonKey(ClaimTypes.Email,
                AuthProvider.ClaimEmail);

            options.Events = new OAuthEvents
            {
                OnRemoteFailure = context =>
                {
                    context.Response.Redirect("/auth/index");
                    var res = context.Response;
                    context.HandleResponse();
                    return Task.CompletedTask;
                },
                OnCreatingTicket = async context =>
                {
                    var request = new HttpRequestMessage(
                        HttpMethod.Get,
                        context.Options.UserInformationEndpoint);

                    request.Headers.Accept.Add(
                        new MediaTypeWithQualityHeaderValue(
                            "application/json"));

                    request.Headers.Authorization =
                        new AuthenticationHeaderValue(
                            "Bearer",
                            context.AccessToken);

                    var response = await context.Backchannel.SendAsync(
                        request);

                    response.EnsureSuccessStatusCode();

                    var user = await response.Content.ReadFromJsonAsync<JsonElement>();

                    // if (context.AccessToken != null)
                    //     context.HttpContext.Response.Cookies.Append("access_token", context.AccessToken);
                    //
                    // if (context.RefreshToken != null)
                    //     context.HttpContext.Response.Cookies.Append("refresh_token", context.RefreshToken);

                    context.RunClaimActions(user);
                }
            };
        });

        return services;
    }

    public static IServiceCollection AddAuthDependency(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        return services;
    }
}