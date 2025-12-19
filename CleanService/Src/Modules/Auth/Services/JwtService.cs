using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using CleanService.Src.Utils;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanService.Src.Modules.Auth.Services;

public class JwtService
{
    private static int AccessTokenExpirationInMinutes = 1000000000;
    private static int RefreshTokenExpirationInDays = 7;

    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JwtResponse GenerateTokens(JwtUserInfo user)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(AccessTokenExpirationInMinutes);
        var accessToken = SignToken(user, accessTokenExpiration);

        var refreshTokenExpiration = DateTime.UtcNow.AddDays(RefreshTokenExpirationInDays);
        var refreshToken = SignToken(user, refreshTokenExpiration);

        return new JwtResponse { AccessToken = accessToken, RefreshToken = refreshToken };
    }

    private string SignToken(JwtUserInfo user, DateTime expiration)
    {
        var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SECRET_KEY"));

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
            }),
            Expires = expiration,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SECRET_KEY"));

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        try
        {
            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch (SecurityTokenException)
        {
            // Token validation failed
            return null;
        }
    }
}
