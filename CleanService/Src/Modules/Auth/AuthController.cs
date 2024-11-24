using System.Net;
using System.Security.Claims;
using CleanService.Src.Constant;
using CleanService.Src.Filters;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Auth.Services;
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

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IStorageService _storageService;

    public AuthController(IAuthService authService, IStorageService storageService)
    {
        _authService = authService;
        _storageService = storageService;
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
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("OAuthRedirect", "Auth")
        };
        properties.Items["role"] = UserType.Customer.ToString();

        return Challenge(properties,
            authenticationSchemes: new[] { AuthProvider.Provider });
    }

    [HttpGet("signup/helper")]
    [AllowAnonymous]
    public IActionResult SignUpHelper()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("OAuthRedirect", "Auth")
        };
        properties.Items["role"] = UserType.Helper.ToString();

        return Challenge(properties,
            authenticationSchemes: new[] { AuthProvider.Provider });
    }

    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult LogIn()
    {
        return Challenge(new AuthenticationProperties()
            {
                RedirectUri = Url.Action("OAuthRedirect", "Auth")
            },
            authenticationSchemes: new[] { AuthProvider.Provider });
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> LogOut()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized", ExceptionConvention.Unauthorized);
        
        await _authService.LogoutUser(userId);
        if (Request.Cookies[".AspNetCore.Cookies"] != null)
            Response.Cookies.Delete(".AspNetCore.Cookies");

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Logout successfully",
        });
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
    [ExcludeProperties]
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
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Decode cookie successfully",
            Data = new
            {
                Claims = claims
            }
        });
    }
    
    [HttpPatch("me")]
    public async Task<IActionResult> UpdateCurrentCustomer([FromForm] UpdateUserRequestDto updateUserRequestDto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized", ExceptionConvention.Unauthorized);

        if (updateUserRequestDto.ProfilePicture != null)
        {
            var result = await _storageService.UploadImageAsync(updateUserRequestDto.ProfilePicture);
            updateUserRequestDto.ProfilePictureUri = result.Uri.ToString();
        }
        
        if (updateUserRequestDto.IdentityCard != null)
        {
            var result = await _storageService.UploadImageAsync(updateUserRequestDto.IdentityCard);
            updateUserRequestDto.IdentityCardUri = result.Uri.ToString();
        }
        
        await _authService.UpdateInfo(userId, updateUserRequestDto);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update personal information successfully"
        });
    }
    
    [HttpPatch("helper/me")]
    public async Task<IActionResult> UpdateCurrentHelper([FromForm] UpdateHelperRequestDto updateHelperRequestDto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized", ExceptionConvention.Unauthorized);
        
        if( updateHelperRequestDto.ResumeUploaded != null)
        {
            var result = await _storageService.UploadFileAsync(updateHelperRequestDto.ResumeUploaded);
            updateHelperRequestDto.ResumeUploadeUri = result.Uri.ToString();
        }
        
        await _authService.UpdateHelperInfo(userId, updateHelperRequestDto);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update personal information successfully"
        });
    }
}