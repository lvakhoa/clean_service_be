using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using CleanService.Src.Constant;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Infrastructures;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Auth.Mapping.Profiles;
using CleanService.Src.Modules.Auth.Services;
using CleanService.Src.Repositories.User;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;

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
        }).AddCookie(options =>
        {
            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Message = "Forbidden",
                    ExceptionCode = ExceptionConvention.Forbidden
                });
                return context.Response.CompleteAsync();
            };
        }).AddOAuth(AuthProvider.Provider, options =>
        {
            var providerDomain = config.GetValue<string>("OAuthProvider:Domain");

            options.ClientId = config.GetValue<string>("OAuthProvider:ClientId")!;
            options.ClientSecret = config.GetValue<string>("OAuthProvider:ClientSecret")!;

            options.AuthorizationEndpoint = AuthProvider.AuthorizationEndpoint(providerDomain!);
            options.TokenEndpoint = AuthProvider.TokenEndpoint(providerDomain!);
            options.CallbackPath = new PathString(AuthProvider.CallbackPath);

            options.Scope.Add("profile");
            options.Scope.Add("email");

            options.SaveTokens = true;

            options.UserInformationEndpoint = AuthProvider.UserInformationEndpoint(providerDomain!);

            options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, AuthProvider.ClaimNameIdentifier);
            options.ClaimActions.MapJsonKey(ClaimTypes.Name, AuthProvider.ClaimName);
            options.ClaimActions.MapJsonKey(ClaimTypes.Email, AuthProvider.ClaimEmail);

            options.Events = new OAuthEvents
            {
                OnCreatingTicket = async context =>
                {
                    var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                    
                    // var queryString = context.Request.Query.ToList();
                    // var code = queryString.FirstOrDefault(q => q.Key == "code").Value.ToString();
                    // var state = queryString.FirstOrDefault(q => q.Key == "state").Value.ToString();
                    //     var tokenResponse = await authService.ExchangeCodeForTokensAsync(code);
                    
                    // Send user's information to oauth provider
                    var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                    var response = await context.Backchannel.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var user = await response.Content.ReadFromJsonAsync<JsonElement>();

                    context.RunClaimActions(user);

                    // Register user to database
                    if (context.Properties.Items.TryGetValue("role", out var role))
                    {
                        var id = context.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                            ?.Value;
                        var email = context.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                        var fullname = context.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                        if (id != null && email != null && fullname != null && role != null)
                        {
                            await authService.RegisterUser(new RegistrationRequestDto
                            {
                                Id = id,
                                Email = email,
                                Fullname = fullname,
                                UserType = Enum.Parse<UserType>(role)
                            });
                        }
                    }
                }
            };
        });

        return services;
    }


    public static IServiceCollection AddAuthDependency(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();

        return services;
    }

    public static IServiceCollection AddAuthMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(RegistrationRequestProfile))
            .AddAutoMapper(typeof(UpdateUserRequestProfile))
            .AddAutoMapper(typeof(UpdateHelperRequestProfile))
            .AddAutoMapper(typeof(UserResponseProfile))
            .AddAutoMapper(typeof(HelperResponseProfile));

        return services;
    }

    public static IServiceCollection AddAuthModule(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddTransient<IClaimsTransformation, ClaimsTransformation>()
            .AddAuthScheme(config)
            .AddAuthDependency()
            .AddAuthMapping();

        return services;
    }
}