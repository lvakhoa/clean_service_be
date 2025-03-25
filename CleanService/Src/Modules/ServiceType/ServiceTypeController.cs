using System.Net;

using CleanService.Src.Common;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;
using CleanService.Src.Modules.ServiceType.Services;
using CleanService.Src.Utils;
using CleanService.Src.Utils.Cache;

using Microsoft.AspNetCore.Mvc;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.ServiceType;

public class ServiceTypeController : ApiController
{
    private readonly IServiceTypeService _serviceTypeService;
    private readonly PaginatedCacheService<Pagination<ServiceTypeResponseDto>, ServiceTypeResponseDto> _paginatedCache;

    public ServiceTypeController(PaginatedCacheService<Pagination<ServiceTypeResponseDto>, ServiceTypeResponseDto> paginatedCache, IServiceTypeService serviceTypeService)
    {
        _paginatedCache = paginatedCache;
        _serviceTypeService = serviceTypeService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateServiceType([FromBody] CreateServiceTypeRequestDto createServiceType)
    {
        await _serviceTypeService.CreateServiceType(createServiceType);

        // Invalidate all service type caches when a new service type is created
        await _paginatedCache.InvalidatePaginationCacheAsync("service-types");

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
    public async Task<ActionResult<Pagination<ServiceTypeResponseDto>>> GetAllServices(int? page, int? limit, Guid? categoryId)
    {
        var cacheBaseKey = $"service-types:page_{page}:limit_{limit}:category_{categoryId}";

        var result = await _paginatedCache.GetPaginatedDataAsync(cacheBaseKey, page, limit,
            (p, l) => _serviceTypeService.GetServiceTypes(p, l, categoryId), TimeSpan.FromMinutes(5));

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get all service types successfully",
            Data = result
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
