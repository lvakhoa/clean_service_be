using System.Net;
using CleanService.Src.Modules.Contract.Mapping.DTOs;
using CleanService.Src.Modules.Contract.Services;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Mvc;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Contract;

[Route("[controller]")]
public class ContractController : Controller
{
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }
    
    [HttpPost("create")]
    public async Task<ActionResult> CreateContract([FromBody] CreateContractRequestDto createContract)
    {
         await _contractService.CreateContract(createContract);
         return CreatedAtAction("CreateContract", new SuccessResponse
         {
             StatusCode = HttpStatusCode.Created,
             Message = "Create contract successfully"
         });
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateContract(Guid id,[FromBody] UpdateContractRequestDto updateContract)
    {
         await _contractService.UpdateContract(id, updateContract);
         return Ok(new SuccessResponse
         {
             StatusCode = HttpStatusCode.OK,
             Message = "Update contract successfully"
         });
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<Pagination<ContractResponseDto>>> GetAllContracts(int? page, int? limit)
    {
        var contracts = await _contractService.GetAllContracts(page, limit);
        return Ok(new SuccessResponse()
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get all contracts successfully",
            Data = contracts
        });
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ContractResponseDto?>> GetContractById(Guid id)
    {
        var contract = await _contractService.GetContractById(id);
        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get contract successfully",
            Data = contract
        });
    }
}