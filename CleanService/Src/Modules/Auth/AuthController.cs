using System.Net;
using System.Security.Claims;
using CleanService.Src.Constant;
using CleanService.Src.Filters;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Auth.Services;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Auth;

[Authorize]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("create-customer")]
    public async Task<IActionResult> CreateCustomer()
    {
        await _authService.RegisterUser(new RegistrationRequestDto
        {
            Id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!,
            Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!,
            Fullname = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!,
            UserType = UserType.Customer
        });

        return Redirect("http://localhost:3000/");
    }

    [HttpGet("create-helper")]
    public async Task<IActionResult> CreateHelper()
    {
        await _authService.RegisterUser(new RegistrationRequestDto
        {
            Id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!,
            Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!,
            Fullname = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!,
            UserType = UserType.Helper
        });

        return Redirect("http://localhost:3000/");
    }

    [HttpGet("login/customer")]
    public IActionResult LoginCustomer()
    {
        return Challenge(new AuthenticationProperties()
            {
                RedirectUri = "http://localhost:5011/api/v1/auth/create-customer"
            },
            authenticationSchemes: new[] { AuthProvider.Provider });
    }

    [HttpGet("login/helper")]
    public IActionResult LoginHelper()
    {
        return Challenge(new AuthenticationProperties()
            {
                RedirectUri = "http://localhost:5011/api/v1/auth/create-helper"
            },
            authenticationSchemes: new[] { AuthProvider.Provider });
    }

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _authService.GetUserById(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get user successfully",
            Data = user
        });
    }

    [HttpPatch("user/{id}")]
    // [ModelValidation]
    public async Task<IActionResult> UpdateInfo(string id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
    {
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        if (currentUserId != id && !User.IsInRole(UserType.Admin.ToString()))
        {
            throw new ExceptionResponse(HttpStatusCode.Forbidden, "Forbidden", ExceptionConvention.Forbidden);
        }

        await _authService.UpdateInfo(id, updateUserRequestDto);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update user's information successfully"
        });
    }

    [HttpPatch("helper/{id}")]
    [ModelValidation]
    public async Task<IActionResult> UpdateHelperInfo(string id, [FromBody] UpdateHelperRequestDto updateHelperRequestDto)
    {
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        if (currentUserId != id && !User.IsInRole(UserType.Admin.ToString()))
        {
            throw new ExceptionResponse(HttpStatusCode.Forbidden, "Forbidden", ExceptionConvention.Forbidden);
        }

        await _authService.UpdateHelperInfo(id, updateHelperRequestDto);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update helper's information successfully"
        });
    }

    [HttpGet("users")]
    //[Authorize(Policy = AuthPolicy.IsAdmin)]
    public async Task<IActionResult> GetUsers(UserType? userType = null, int? page = null, int? limit = null,
        UserStatus? status = UserStatus.Active)
    {
        if (page < 1 || limit < 1)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Page or limit param is negative",
                ExceptionConvention.ValidationFailed);
        }

        var users = await _authService.GetUsers(userType, page, limit, status);
        return Ok(users);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized", ExceptionConvention.Unauthorized);
        
        var user = await _authService.GetUserById(userId);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get personal information successfully",
            Data = user
        });
    }

    [HttpPatch("user/{id}/block")]
    [Authorize(Policy = AuthPolicy.IsAdmin)]
    public async Task<IActionResult> BlockUser(string id)
    {
        await _authService.BlockUser(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Block user successfully"
        });
    }

    [HttpPatch("user/{id}/activate")]
    [Authorize(Policy = AuthPolicy.IsAdmin)]
    public async Task<IActionResult> ActivateUser(string id)
    {
        await _authService.ActivateUser(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Activate user successfully"
        });
    }
}