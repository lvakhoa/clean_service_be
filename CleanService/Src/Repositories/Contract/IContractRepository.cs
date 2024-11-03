using CleanService.Src.Models;
using CleanService.Src.Modules.Contract.Mapping.DTOs;

namespace CleanService.Src.Repositories.Contract;

public interface IContractRepository : IRepository<Contracts>
{
    public Task<ContractReturnDto> CreateContract(CreateContractDto createContractDto);
    
    public Task<ContractReturnDto?> UpdateContract(Guid id, UpdateContractDto updateContractDto);
    
    public Task<ContractReturnDto?> GetContractById(Guid id);
    
    public Task<ContractReturnDto[]> GetAllContract();
}