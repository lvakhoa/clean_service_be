using System.Net;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;
using CleanService.Src.Modules.ServiceType.Services;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Mvc;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.ServiceType;

[Route("[controller]")]
public class ServiceTypeController : Controller
{
    private readonly IServiceTypeService _serviceTypeService;

    public ServiceTypeController(IServiceTypeService serviceTypeService)
    {
        _serviceTypeService = serviceTypeService;
    }
    
    [HttpPost("create")]
    public async Task<ActionResult> CreateServiceType([FromBody] CreateServiceTypeRequestDto createServiceType)
    {
        await _serviceTypeService.CreateServiceType(createServiceType);
        return CreatedAtAction("CreateServiceType", new SuccessResponse
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Create service type successfully",
        });
    }
    
    [HttpPatch("update/{id}")]
    public async Task<ActionResult> UpdateServiceType(Guid id, [FromBody] UpdateServiceTypeRequestDto updateServiceType)
    {
        await _serviceTypeService.UpdateServiceType(id, updateServiceType);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Update service type successfully",
        });
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<Pagination<ServiceTypeResponseDto>>> GetAllServices(int? page, int? limit)
    {
        var serviceTypes = await _serviceTypeService.GetServiceTypes(page, limit);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get all service types successfully",
            Data = serviceTypes
        });
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceTypeResponseDto?>> GetServiceTypeById(Guid id)
    {
        var serviceType = await _serviceTypeService.GetServiceTypeById(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get service type successfully",
            Data = serviceType
        });
    }
}