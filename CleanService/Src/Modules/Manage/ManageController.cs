using System.Net;
using System.Security.Claims;
using CleanService.Src.Constant;
using CleanService.Src.Filters;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Auth.Services;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;
using CleanService.Src.Modules.Manage.Mapping.DTOs.Refund;
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
    
    [HttpGet("customers")]
    public async Task<ActionResult<UserResponseDto>> GetCustomers(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Page or limit param is negative",
                ExceptionConvention.ValidationFailed);
        }
        var customer = await _manageService.GetCustomer(page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get customer successfully",
            Data = customer
        });
    }
    
    [HttpGet("customers/{id}")]
    public async Task<ActionResult<UserResponseDto>> GetCustomerById(string id)
    {
        var customer = await _manageService.GetCustomerById(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get customer successfully",
            Data = customer
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
        
        var helpers = await _manageService.GetHelper(page, limit);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get helpers successfully",
            Data = helpers
        });
    }
    
    [HttpGet("refunds")]
    public async Task<ActionResult<Pagination<RefundResponseDto>>> GetRefunds(RefundStatus? status ,int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Page or limit param is negative",
                ExceptionConvention.ValidationFailed);
        }
        
        var complaints = await _manageService.GetRefunds(status,page, limit);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get refunds successfully",
            Data = complaints
        });
    }
    
    [HttpGet("refunds/{id}")]
    public async Task<ActionResult<RefundResponseDto>> GetRefundById(Guid id)
    {
        var refund = await _manageService.GetRefundById(id);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get refund successfully",
            Data = refund
        });
    }
    
    [HttpPatch("refunds/{id}")]
    [ModelValidation]
    public async Task<IActionResult> UpdateRefund(Guid id,[FromBody] UpdateRefundRequestDto updateRefundRequestDto)
    {
        await _manageService.UpdateRefund(id, updateRefundRequestDto);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update refund successfully"
        });
    }

    [HttpPatch("refunds/handle/{id}")]
    public async Task<IActionResult> HandleRefund(Guid id, [FromBody] UpdateRefundRequestDto updateRefund)
    {
        if (updateRefund.Status == null)
            return BadRequest();
        var status = updateRefund.Status;
        await _manageService.HandleRefund(id, status);

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Hande refund successfully"
        });
    }
    
    [HttpDelete("refunds/{id}")]
    public async Task<IActionResult> DeleteRefund(Guid id)
    {
        await _manageService.DeleteRefund(id);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Delete refund successfully"
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
    public async Task<IActionResult> CreateRoomPricing([FromBody]CreateRoomPricingRequestDto createRoomPricingRequestDto)
    {
        await _manageService.CreateRoomPricing(createRoomPricingRequestDto);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Create room pricing successfully"
        });
    }
    
    [HttpPatch("room-pricing/{id:guid}")]
    [ModelValidation]
    public async Task<IActionResult> UpdateRoomPricing(Guid id,[FromBody] UpdateRoomPricingRequestDto updateRoomPricingRequestDto)
    {
        
        await _manageService.UpdateRoomPricing(id, updateRoomPricingRequestDto);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update room pricing successfully"
        });
    }
    
    [HttpDelete("room-pricing/{id:guid}")]
    public async Task<IActionResult> DeleteRoomPricing(Guid id)
    {
        await _manageService.DeleteRoomPricing(id);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Delete room pricing successfully"
        });
    }
    
    [HttpPost("duration-price")]
    [ModelValidation]
    public async Task<IActionResult> CreateDurationPrice([FromBody] CreateDurationPriceRequestDto createDurationPriceRequestDto)
    {
        await _manageService.CreateDurationPrice(createDurationPriceRequestDto);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Create duration price successfully"
        });
    }
    
    [HttpGet("duration-price")]
    public async Task<ActionResult<Pagination<DurationPricingResponseDto>>> GetDurationPrices(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Page or limit param is negative",
                ExceptionConvention.ValidationFailed);
        }
        
        var durationPrices = await _manageService.GetDurationPrices(page, limit);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get duration prices successfully",
            Data = durationPrices
        });
    }
    
    [HttpPatch("duration-price/{id:guid}")]
    [ModelValidation]
    public async Task<IActionResult> UpdateDurationPrice(Guid id,[FromBody] UpdateDurationPriceRequestDto updateDurationPriceRequestDto)
    {
        await _manageService.UpdateDurationPrice(id, updateDurationPriceRequestDto);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update duration price successfully"
        });
    }
    
    [HttpDelete("duration-price/{id:guid}")]
    public async Task<IActionResult> DeleteDurationPrice(Guid id)
    {
        await _manageService.DeleteDurationPrice(id);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Delete duration price successfully"
        });
    }
    
    [HttpGet("feedbacks")]
    public async Task<ActionResult<Pagination<FeedbackResponseDto>>> GetFeedbacks(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Page or limit param is negative",
                ExceptionConvention.ValidationFailed);
        }
        
        var feedbacks = await _manageService.GetFeedbacks(page, limit);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get feedbacks successfully",
            Data = feedbacks
        });
    }
    
    [HttpGet("feedbacks/{id}")]
    public async Task<ActionResult<FeedbackResponseDto>> GetFeedbackById(Guid id)
    {
        var feedback = await _manageService.GetFeedbackById(id);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get feedback successfully",
            Data = feedback
        });
    }
    
    [HttpDelete("feedbacks/{id}")]
    public async Task<IActionResult> DeleteFeedback(Guid id)
    {
        await _manageService.DeleteFeedback(id);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Delete feedback successfully"
        });
    }
    
    [HttpPatch("users/{id}")]
    [ModelValidation]
    public async Task<IActionResult> UpdateUser(string id,[FromBody] AdminUpdateUserRequestDto adminUpdateUserRequestDto)
    {
        await _manageService.UpdateUserInfo(id, adminUpdateUserRequestDto);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update user successfully"
        });
    }
    
    [HttpPatch("helpers/{id}")]
    [ModelValidation]
    public async Task<IActionResult> UpdateHelper(string id,[FromBody] AdminUpdateHelperRequestDto adminUpdateHelperRequestDto)
    {
        await _manageService.UpdateHelperInfo(id, adminUpdateHelperRequestDto);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update helper successfully"
        });
    }
    
    [HttpGet("feedbacks/customer")]
    public async Task<ActionResult<Pagination<FeedbackResponseDto>>> GetFeedbacksOfCurrentCustomer(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Page or limit param is negative",
                ExceptionConvention.ValidationFailed);
        }
        
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        
        var feedbacks = await _manageService.GetFeedbacksOfCurrentCustomer(userId, page, limit);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get feedbacks of current customer successfully",
            Data = feedbacks
        });
    }
    
    [HttpGet("refunds/customer")]
    public async Task<ActionResult<Pagination<RefundResponseDto>>> GetRefundsOfCurrentCustomer(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Page or limit param is negative",
                ExceptionConvention.ValidationFailed);
        }
        
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        
        var refunds = await _manageService.GetRefundsOfCurrentCustomer(userId, page, limit);
        
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get refunds of current customer successfully",
            Data = refunds
        });
    }
}