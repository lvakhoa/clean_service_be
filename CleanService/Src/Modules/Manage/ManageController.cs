using System.Net;
using CleanService.Src.Constant;
using CleanService.Src.Filters;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Auth.Services;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;
using CleanService.Src.Modules.Manage.Services;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Manage;

[Route("[controller]")]
public class ManageController : Controller
{
    private readonly IManageService _manageService; 
    private readonly IAuthService _authService;
    public ManageController(IManageService manageService, IAuthService authService)
    {
        _manageService = manageService;
        _authService = authService;
    }
    
    [HttpGet("users")]
    public async Task<ActionResult<Pagination<UserResponseDto>>> GetUsers(UserType? userType,int? page, int? limit, UserStatus? userStatus = UserStatus.Active)
    {
        var users = await _authService.GetUsers(userType,page, limit, userStatus);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get users successfully",
            Data = users
        });
    }
    
    [HttpGet("helpers")]
    public async Task<ActionResult<Pagination<HelperDetailResponseDto>>> GetHelpers(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Page or limit param is negative",
                ExceptionConvention.ValidationFailed);
        }
        
        var helpers = await _manageService.GetDetailHelper(page, limit);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get helpers successfully",
            Data = helpers
        });
    }
    
    [HttpGet("complaints")]
    public async Task<ActionResult<Pagination<ComplaintResponseDto>>> GetComplaints(ComplaintStatus? status ,int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Page or limit param is negative",
                ExceptionConvention.ValidationFailed);
        }
        
        var complaints = await _manageService.GetComplaints(status,page, limit);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get complaints successfully",
            Data = complaints
        });
    }
    
    [HttpPatch("complaints/{id}")]
    [ModelValidation]
    public async Task<IActionResult> UpdateComplaint(Guid id,[FromBody] UpdateComplaintRequestDto updateComplaintRequestDto)
    {
        await _manageService.UpdateComplaint(id, updateComplaintRequestDto);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update complaint successfully"
        });
    }
    
    [HttpDelete("complaints/{id}")]
    public async Task<IActionResult> DeleteComplaint(Guid id)
    {
        await _manageService.DeleteComplaint(id);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Delete complaint successfully"
        });
    }
    
    [HttpGet("room-pricing")]
    public async Task<ActionResult<Pagination<RoomPricingResponseDto>>> GetRoomPricings(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Page or limit param is negative",
                ExceptionConvention.ValidationFailed);
        }
        
        var roomPricings = await _manageService.GetRoomPricing(page, limit);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get room pricings successfully",
            Data = roomPricings
        });
    }
    
    [HttpPost("room-pricing")]
    [ModelValidation]
    public async Task<IActionResult> CreateRoomPricing([FromBody] CreateRoomPricingRequestDto createRoomPricingRequestDto)
    {
        await _manageService.CreateRoomPricing(createRoomPricingRequestDto);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Create room pricing successfully"
        });
    }
}