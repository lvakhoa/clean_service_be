using CleanService.Src.Modules.Contract.DTOs;

namespace CleanService.Src.Modules.Contract.Repositories;

public interface IContractRepository
{
    public Task<ContractReturnDto> CreateContract(CreateContractDto createContractDto);
    
    public Task<ContractReturnDto?> UpdateContract(Guid id, UpdateContractDto updateContractDto);
    
    public Task<ContractReturnDto?> GetContractById(Guid id);
    
    public Task<ContractReturnDto[]> GetAllContract();
}