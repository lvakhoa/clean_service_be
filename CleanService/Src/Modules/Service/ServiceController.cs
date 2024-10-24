
using CleanService.Src.Models;
using CleanService.Src.Modules.Service.DTOs;
using CleanService.Src.Modules.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Service;

[Route("[controller]")]
public class ServiceController : Controller
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceReturnDto>> CreateService([FromBody] CreateServiceDto service)
    {
        var test = await _serviceService.CreateService(service);
        return test;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceReturnDto?>> GetServiceById(Guid id)
    {
        return await _serviceService.GetServiceById(id);
    }
}