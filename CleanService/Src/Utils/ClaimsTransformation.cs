using System.Security.Claims;

using CleanService.Src.Database;
using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;

using Microsoft.AspNetCore.Authentication;

namespace CleanService.Src.Utils;

public class ClaimsTransformation : IClaimsTransformation
{
    private readonly IUnitOfWork _unitOfWork;

    public ClaimsTransformation(CleanServiceContext dbContext, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var claimsIdentity = principal.Identities.First();

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return principal;
        }

        var claimRoleValue = await _unitOfWork.Repository<Users, PartialUsers>()
            .GetFirstAsync(UserSpecification.GetUserByIdSpec(userId)).ContinueWith(x => x.Result?.UserType);
        if (!principal.HasClaim(claim => claim.Type == ClaimTypes.Role))
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, claimRoleValue.ToString()));
        }

        return principal;
    }
}
