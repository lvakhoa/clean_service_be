using CleanService.Src.Modules.Service.DTOs;
using CleanService.Src.Modules.Service.Services;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.ServiceType;

[Route("[controller]")]
public class ServiceTypeController : Controller
{
    private readonly IServiceTypeService _serviceTypeService;

    public ServiceTypeController(IServiceTypeService serviceTypeService)
    {
        _serviceTypeService = serviceTypeService;
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceTypeReturnDto>> CreateService([FromBody] CreateServiceTypeDto serviceType)
    {
        var test = await _serviceTypeService.CreateService(serviceType);
        return test;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceTypeReturnDto?>> GetServiceById(Guid id)
    {
        return await _serviceTypeService.GetServiceById(id);
    }
}