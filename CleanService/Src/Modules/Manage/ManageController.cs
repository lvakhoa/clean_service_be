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
using CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;
using CleanService.Src.Modules.Manage.Mapping.DTOs.Refund;
using CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;
using CleanService.Src.Modules.Manage.Services;
using CleanService.Src.Modules.Storage.Services;
using CleanService.Src.Utils;

using Microsoft.AspNetCore.Mvc;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Manage;

public class ManageController : ApiController
{
    private readonly IManageService _manageService;
    private readonly IAuthService _authService;
    private readonly IStorageService _storageService;

    public ManageController(IManageService manageService, IAuthService authService, IStorageService storageService)
    {
        _manageService = manageService;
        _authService = authService;
        _storageService = storageService;
    }

    //User
    [HttpPatch("users/{id}")]
    [ModelValidation]
    public async Task<IActionResult> UpdateUser(string id,
        [FromForm] AdminUpdateUserRequestDto adminUpdateUserRequestDto)
    {
        if (adminUpdateUserRequestDto.ProfilePictureFile != null)
        {
            var result = await _storageService.UploadImageAsync(adminUpdateUserRequestDto.ProfilePictureFile);
            adminUpdateUserRequestDto.ProfilePicture = result.Uri.ToString();
        }

        if (adminUpdateUserRequestDto.IdCardFile != null)
        {
            var result = await _storageService.UploadImageAsync(adminUpdateUserRequestDto.IdCardFile);
            adminUpdateUserRequestDto.IdCard = result.Uri.ToString();
        }

        await _manageService.UpdateUserInfo(id, adminUpdateUserRequestDto);

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update user successfully"
        });
    }

    [HttpGet("users")]
    public async Task<ActionResult<Pagination<UserResponseDto>>> GetUsers(UserType? userType, int? page, int? limit,
        UserStatus? userStatus = UserStatus.Active)
    {
        var users = await _authService.GetUsers(userType, page, limit, userStatus);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get users successfully",
            Data = users
        });
    }

    //Customer
    [HttpGet("customers")]
    public async Task<ActionResult<UserResponseDto>> GetCustomers(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new UnprocessableRequestException("Page or limit param is negative",
                exceptionCode: ExceptionConvention.ValidationFailed);
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

    //Helper
    [HttpGet("helpers")]
    public async Task<ActionResult<Pagination<HelperDetailResponseDto>>> GetHelpers(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new UnprocessableRequestException("Page or limit param is negative",
                exceptionCode: ExceptionConvention.ValidationFailed);
        }

        var helpers = await _manageService.GetHelper(page, limit);

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get helpers successfully",
            Data = helpers
        });
    }

    [HttpGet("helpers/{id}")]
    public async Task<ActionResult<HelperDetailResponseDto>> GetHelperById(string id)
    {
        var helper = await _manageService.GetHelperById(id);

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get helper successfully",
            Data = helper
        });
    }

    [HttpPatch("helpers/{id}")]
    [ModelValidation]
    public async Task<IActionResult> UpdateHelper(string id,
        [FromForm] AdminUpdateHelperRequestDto adminUpdateHelperRequestDto)
    {
        if (adminUpdateHelperRequestDto.ResumeUploadedFile != null)
        {
            var result = await _storageService.UploadFileAsync(adminUpdateHelperRequestDto.ResumeUploadedFile);
            adminUpdateHelperRequestDto.ResumeUploaded = result.Uri.ToString();
        }

        await _manageService.UpdateHelperInfo(id, adminUpdateHelperRequestDto);

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update helper successfully"
        });
    }

    //Refund
    [HttpGet("refunds")]
    public async Task<ActionResult<Pagination<RefundResponseDto>>> GetRefunds(RefundStatus? status, int? page,
        int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new UnprocessableRequestException("Page or limit param is negative",
                exceptionCode: ExceptionConvention.ValidationFailed);
        }

        var complaints = await _manageService.GetRefunds(status, page, limit);

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
    public async Task<IActionResult> UpdateRefund(Guid id, [FromBody] UpdateRefundRequestDto updateRefundRequestDto)
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

    [HttpGet("refunds/customer")]
    public async Task<ActionResult<Pagination<RefundResponseDto>>> GetRefundsOfCurrentCustomer(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new UnprocessableRequestException("Page or limit param is negative",
                exceptionCode: ExceptionConvention.ValidationFailed);
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

    //Room Pricing
    [HttpGet("room-pricing")]
    public async Task<ActionResult<Pagination<RoomPricingResponseDto>>> GetRoomPricings(int? page, int? limit,
        RoomType? roomType, Guid? serviceTypeId)
    {
        if (page < 1 || limit < 1)
        {
            throw new UnprocessableRequestException("Page or limit param is negative",
                exceptionCode: ExceptionConvention.ValidationFailed);
        }

        var roomPricings = await _manageService.GetRoomPricing(page, limit, roomType, serviceTypeId);

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get room pricings successfully",
            Data = roomPricings
        });
    }

    [HttpPost("room-pricing")]
    [ModelValidation]
    public async Task<IActionResult> CreateRoomPricing(
        [FromBody] CreateRoomPricingRequestDto createRoomPricingRequestDto)
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
    public async Task<IActionResult> UpdateRoomPricing(Guid id,
        [FromBody] UpdateRoomPricingRequestDto updateRoomPricingRequestDto)
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

    //Duration Price
    [HttpPost("duration-price")]
    [ModelValidation]
    public async Task<IActionResult> CreateDurationPrice(
        [FromBody] CreateDurationPriceRequestDto createDurationPriceRequestDto)
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
            throw new UnprocessableRequestException("Page or limit param is negative",
                exceptionCode: ExceptionConvention.ValidationFailed);
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
    public async Task<IActionResult> UpdateDurationPrice(Guid id,
        [FromBody] UpdateDurationPriceRequestDto updateDurationPriceRequestDto)
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

    //Feedback
    [HttpGet("feedbacks")]
    public async Task<ActionResult<Pagination<FeedbackResponseDto>>> GetFeedbacks(int? page, int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new UnprocessableRequestException("Page or limit param is negative",
                exceptionCode: ExceptionConvention.ValidationFailed);
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

    [HttpGet("feedbacks/customer")]
    public async Task<ActionResult<Pagination<FeedbackResponseDto>>> GetFeedbacksOfCurrentCustomer(int? page,
        int? limit)
    {
        if (page < 1 || limit < 1)
        {
            throw new UnprocessableRequestException("Page or limit param is negative",
                exceptionCode: ExceptionConvention.ValidationFailed);
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

}
