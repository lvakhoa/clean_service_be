using System.Security.Claims;
using CleanService.Src.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Helpers;

public class ClaimsTransformation : IClaimsTransformation
{
    private readonly CleanServiceContext _dbContext;

    public ClaimsTransformation(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var claimsIdentity = new ClaimsIdentity();

        var userId =
            Guid.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id)
                ? id
                : Guid.Empty;
        var claimRoleValue = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (!principal.HasClaim(claim => claim.Type == ClaimTypes.Role))
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, claimRoleValue?.UserType.ToString() ?? ""));
        }

        principal.AddIdentity(claimsIdentity);
        return principal;
    }
}