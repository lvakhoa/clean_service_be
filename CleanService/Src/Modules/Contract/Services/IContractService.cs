using CleanService.Src.Modules.Contract.Mapping.DTOs;

namespace CleanService.Src.Modules.Contract.Services;

public interface IContractService
{
    Task<ContractReturnDto> CreateContract(CreateContractDto createContractDto);
    
    Task<ContractReturnDto?> UpdateContract(Guid id, UpdateContractDto updateContractDto);
    
    Task<ContractReturnDto[]> GetAllContracts();

    Task<ContractReturnDto?> GetContractById(Guid id);
}