using CleanService.Src.Modules.Contract.DTOs;
using CleanService.Src.Modules.Contract.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Contract;

[Route("[controller]")]
public class ContractController : Controller
{
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }
    
    [HttpPost]
    public async Task<ActionResult<ContractReturnDto>> CreateContract([FromBody] CreateContractDto contract)
    {
        return await _contractService.CreateContract(contract);
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult<ContractReturnDto?>> UpdateContract(Guid id,[FromBody] UpdateContractDto contract)
    {
        return await _contractService.UpdateContract(id, contract);
    }
    
    [HttpGet]
    public async Task<ActionResult<ContractReturnDto[]>> GetAllContracts()
    {
        return await _contractService.GetAllContracts();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ContractReturnDto?>> GetContractById(Guid id)
    {
        return await _contractService.GetContractById(id);
    }
}