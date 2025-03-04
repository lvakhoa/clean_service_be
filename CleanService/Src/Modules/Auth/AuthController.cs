using System.Net;
using System.Security.Claims;

using CleanService.Src.Common;
using CleanService.Src.Constant;
using CleanService.Src.Exceptions;
using CleanService.Src.Filters;
using CleanService.Src.Models;
using CleanService.Src.Models.Enums;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Auth.Services;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Services;
using CleanService.Src.Modules.Storage.Services;
using CleanService.Src.Utils;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Auth;

public class AuthController : ApiController
{
    private readonly IAuthService _authService;
    private readonly IStorageService _storageService;
    private readonly IManageService _manageService;

    public AuthController(IAuthService authService, IStorageService storageService, IManageService manageService)
    {
        _authService = authService;
        _storageService = storageService;
        _manageService = manageService;
    }


    [AllowAnonymous]
    [HttpGet("oauth/redirect")]
    public IActionResult OAuthRedirect()
    {
        var cookies = Request.Cookies;

        cookies.ToList().ForEach(pair => Response.Cookies.Append(pair.Key, pair.Value));
        return Redirect("http://localhost:3000/");
    }

    [HttpGet("signup/customer")]
    [AllowAnonymous]
    public IActionResult SignUpCustomer()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("OAuthRedirect", "Auth") };
        properties.Items["role"] = UserType.Customer.ToString();

        return Challenge(properties, authenticationSchemes: new[] { AuthProvider.Provider });
    }

    [HttpGet("signup/helper")]
    [AllowAnonymous]
    public IActionResult SignUpHelper()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("OAuthRedirect", "Auth") };
        properties.Items["role"] = UserType.Helper.ToString();

        return Challenge(properties, authenticationSchemes: new[] { AuthProvider.Provider });
    }

    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult LogIn()
    {
        return Challenge(new AuthenticationProperties() { RedirectUri = Url.Action("OAuthRedirect", "Auth") },
            authenticationSchemes: new[] { AuthProvider.Provider });
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> LogOut()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) throw new UnauthorizedAccessException();

        await _authService.LogoutUser(userId);
        if (Request.Cookies[".AspNetCore.Cookies"] != null) Response.Cookies.Delete(".AspNetCore.Cookies");

        return Ok(new ApiSuccessResult<string>(StatusCodes.Status200OK, "Logout successfully"));
    }

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _authService.GetUserById(id);
        return Ok(new ApiSuccessResult<UserResponseDto>(StatusCodes.Status200OK, "Get user successfully", user));
    }

    [HttpPatch("user/{id}")]
    // [ModelValidation]
    public async Task<IActionResult> UpdateInfo(string id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
    {
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        if (currentUserId != id && !User.IsInRole(UserType.Admin.ToString()))
        {
            throw new ForbiddenException("Forbidden");
        }

        await _authService.UpdateInfo(id, updateUserRequestDto);
        return Ok(new ApiSuccessResult<string>(StatusCodes.Status200OK, "Update user's information successfully"));
    }

    [HttpPatch("helper/{id}")]
    // [ModelValidation]
    public async Task<IActionResult> UpdateHelperInfo(string id,
        [FromBody] UpdateHelperRequestDto updateHelperRequestDto)
    {
        // var currentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        // if (currentUserId != id && !User.IsInRole(UserType.Admin.ToString()))
        // {
        //     throw new ExceptionResponse(HttpStatusCode.Forbidden, "Forbidden", ExceptionConvention.Forbidden);
        // }

        await _authService.UpdateHelperInfo(id, updateHelperRequestDto);
        return Ok(new ApiSuccessResult<string>(StatusCodes.Status200OK, "Update helper's information successfully"));
    }

    [HttpGet("users")]
    //[Authorize(Policy = AuthPolicy.IsAdmin)]
    public async Task<IActionResult> GetUsers(UserType? userType = null, int? page = null, int? limit = null,
        UserStatus? status = UserStatus.Active)
    {
        if (page < 1 || limit < 1)
        {
            throw new BadRequestException("Page or limit param is negative", ExceptionConvention.ValidationFailed);
        }

        var users = await _authService.GetUsers(userType, page, limit, status);
        return Ok(new ApiSuccessResult<Pagination<UserResponseDto>>(StatusCodes.Status200OK, "Get users successfully",
            users));
    }

    [HttpGet("me")]
    [ExcludeProperties]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) throw new UnauthorizedAccessException();

        var user = await _authService.GetUserById(userId);
        return Ok(new ApiSuccessResult<UserResponseDto>(StatusCodes.Status200OK,
            "Get personal information successfully", user));
    }

    [HttpPatch("user/{id}/block")]
    [Authorize(Policy = AuthPolicy.IsAdmin)]
    public async Task<IActionResult> BlockUser(string id)
    {
        await _authService.BlockUser(id);
        return Ok(new ApiSuccessResult<string>(StatusCodes.Status200OK, "Block user successfully"));
    }

    [HttpPatch("user/{id}/activate")]
    [Authorize(Policy = AuthPolicy.IsAdmin)]
    public async Task<IActionResult> ActivateUser(string id)
    {
        await _authService.ActivateUser(id);
        return Ok(new ApiSuccessResult<string>(StatusCodes.Status200OK, "Activate user successfully"));
    }

    [HttpPost("decode")]
    public IActionResult DecodeCookie()
    {
        var claims = User.Claims.Select(pair =>
        {
            var type = pair.Type switch
            {
                ClaimTypes.NameIdentifier => "Id",
                ClaimTypes.Email => "Email",
                ClaimTypes.Name => "Fullname",
                ClaimTypes.Role => "Role",
                _ => "Unknown"
            };
            return new { type, pair.Value };
        }).ToList();
        return Ok(new ApiSuccessResult<object>(StatusCodes.Status200OK, "Decode cookie successfully",
            new { Claims = claims }));
    }

    [HttpPatch("me")]
    public async Task<IActionResult> UpdateCurrentCustomer([FromForm] UpdateUserRequestDto updateUserRequestDto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) throw new UnauthorizedAccessException();

        if (updateUserRequestDto.ProfilePictureFile != null)
        {
            var result = await _storageService.UploadImageAsync(updateUserRequestDto.ProfilePictureFile);
            updateUserRequestDto.ProfilePicture = result.Uri.ToString();
        }

        if (updateUserRequestDto.IdentityCardFile != null)
        {
            var result = await _storageService.UploadImageAsync(updateUserRequestDto.IdentityCardFile);
            updateUserRequestDto.IdentityCard = result.Uri.ToString();
        }

        await _authService.UpdateInfo(userId, updateUserRequestDto);
        return Ok(new ApiSuccessResult<object>(StatusCodes.Status200OK, "Update personal information successfully"));
    }

    [HttpGet("helper/me")]
    public async Task<IActionResult> GetCurrentHelper()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) throw new UnauthorizedAccessException();

        var helper = await _manageService.GetHelperById(userId);
        return Ok(new ApiSuccessResult<HelperDetailResponseDto>(StatusCodes.Status200OK,
            "Get personal information successfully", helper));
    }

    [HttpPatch("helper/me")]
    public async Task<IActionResult> UpdateCurrentHelper([FromForm] UpdateHelperRequestDto updateHelperRequestDto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) throw new UnauthorizedAccessException();

        if (updateHelperRequestDto.ResumeUploadedFile != null)
        {
            var result = await _storageService.UploadFileAsync(updateHelperRequestDto.ResumeUploadedFile);
            updateHelperRequestDto.ResumeUploaded = result.Uri.ToString();
        }

        await _authService.UpdateHelperInfo(userId, updateHelperRequestDto);
        return Ok(new ApiSuccessResult<object>(StatusCodes.Status200OK, "Update personal information successfully"));
    }
}
