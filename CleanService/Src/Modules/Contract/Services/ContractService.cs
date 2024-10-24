using CleanService.Src.Modules.Contract.DTOs;
using CleanService.Src.Modules.Contract.Repositories;

namespace CleanService.Src.Modules.Contract.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;

    public ContractService(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }
    
    public async Task<ContractReturnDto> CreateContract(CreateContractDto createContractDto)
    {
        return await _contractRepository.CreateContract(createContractDto);
    }

    public async Task<ContractReturnDto?> UpdateContract(Guid id, UpdateContractDto updateContractDto)
    {
        return await _contractRepository.UpdateContract(id, updateContractDto);
    }

    public async Task<ContractReturnDto[]> GetAllContracts()
    {
        return await _contractRepository.GetAllContract();
    }

    public async Task<ContractReturnDto?> GetContractById(Guid id)
    {
        return await _contractRepository.GetContractById(id);
    }
}