using System.Net;

using CleanService.Src.Common;
using CleanService.Src.Modules.ServiceCategory.Mapping.DTOs;
using CleanService.Src.Modules.ServiceCategory.Services;
using CleanService.Src.Utils;

using Microsoft.AspNetCore.Mvc;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.ServiceCategory;

public class ServiceCategoryController : ApiController
{
    private readonly IServiceCategoryService _serviceCategoryService;

    public ServiceCategoryController(IServiceCategoryService serviceCategoryService)
    {
        _serviceCategoryService = serviceCategoryService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateServiceCategory([FromBody] CreateServiceCategoryRequestDto createServiceCategory)
    {
        await _serviceCategoryService.CreateServiceCategory(createServiceCategory);
        return CreatedAtAction("CreateServiceCategory", new SuccessResponse
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Create service category successfully",
        });
    }

    [HttpPatch("update/{id}")]
    public async Task<ActionResult> UpdateServiceCategory(Guid id, [FromBody] UpdateServiceCategoryRequestDto updateServiceCategory)
    {
        await _serviceCategoryService.UpdateServiceCategory(id, updateServiceCategory);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update service category successfully",
        });
    }

    [HttpGet("all")]
    public async Task<ActionResult<Pagination<ServiceCategoryResponseDto>>> GetAllServiceCategories(int? page, int? limit)
    {
        var serviceCategories = await _serviceCategoryService.GetServiceCategories(page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get all service categories successfully",
            Data = serviceCategories
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceCategoryResponseDto?>> GetServiceCategoryById(Guid id)
    {
        var serviceCategory = await _serviceCategoryService.GetServiceCategoryById(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get service category successfully",
            Data = serviceCategory
        });
    }
}
