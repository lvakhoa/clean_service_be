using System.Security.Claims;
using CleanService.Src.Models;
using CleanService.Src.Repositories.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Utils;

public class ClaimsTransformation : IClaimsTransformation
{
    private readonly IUserRepository _userRepository;

    public ClaimsTransformation(CleanServiceContext dbContext, IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var claimsIdentity = principal.Identities.First();

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var claimRoleValue = await _userRepository.GetUserType(userId ?? "");
        if (!principal.HasClaim(claim => claim.Type == ClaimTypes.Role))
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, claimRoleValue.ToString()));
        }

        return principal;
    }
}