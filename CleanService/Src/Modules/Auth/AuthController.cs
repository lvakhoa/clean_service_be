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

namespace CleanService.Src.Modules.Auth;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("create-customer")]
    public async Task<IActionResult> CreateCustomer()
    {
        await _authService.RegisterUser(new RegistrationDto
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
        await _authService.RegisterUser(new RegistrationDto
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
    
    [ModelValidation]
    [HttpPatch("user/{id}")]
    public async Task<IActionResult> UpdateInfo(string id, [FromBody] UpdateInfoDto updateInfoDto)
    {
        var modelstate = ModelState;
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        if (currentUserId != id && !User.IsInRole(UserType.Admin.ToString()))
        {
            throw new ExceptionResponse(HttpStatusCode.Forbidden, "Forbidden", ExceptionConvention.Forbidden);
        }
        var user = await _authService.UpdateInfo(id, updateInfoDto);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update user's information successfully",
            Data = user
        });
    }

    [HttpPatch("helper/{id}")]
    [ModelValidation]
    public async Task<IActionResult> UpdateHelperInfo(string id, [FromBody] UpdateHelperDto updateHelperDto)
    {
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        if (currentUserId != id && !User.IsInRole(UserType.Admin.ToString()))
        {
            throw new ExceptionResponse(HttpStatusCode.Forbidden, "Forbidden", ExceptionConvention.Forbidden);
        }
        var user = await _authService.UpdateHelperInfo(id, updateHelperDto);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update helper's information successfully",
            Data = user
        });
    }

    [HttpGet("users")]
    [Authorize(Policy = AuthPolicy.IsAdmin)]
    public async Task<IActionResult> GetAllUsers(UserType? userType, UserStatus? status = UserStatus.Active)
    {
        var users = await _authService.GetAllUsers(userType, status);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get all users successfully",
            Data = users
        });
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
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
        var user = await _authService.BlockUser(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Block user successfully",
            Data = user
        });
    }

    [HttpPatch("user/{id}/activate")]
    [Authorize(Policy = AuthPolicy.IsAdmin)]
    public async Task<IActionResult> ActivateUser(string id)
    {
        var user = await _authService.ActivateUser(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Activate user successfully",
            Data = user
        });
    }
}