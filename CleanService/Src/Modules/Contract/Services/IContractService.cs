using CleanService.Src.Modules.Contract.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Contract.Services;

public interface IContractService
{
    Task CreateContract(CreateContractRequestDto createContractDto);
    
    Task UpdateContract(Guid id, UpdateContractRequestDto updateContractDto);
    
    Task<Pagination<ContractResponseDto>> GetAllContracts(int? page, int? limit);
    
    Task<ContractResponseDto?> GetContractById(Guid id);
}