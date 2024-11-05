using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Contract.Infrastructures;
using CleanService.Src.Modules.Contract.Mapping.DTOs;
using CleanService.Src.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Contract.Services;

public class ContractService : IContractService
{
    private readonly IContractUnitOfWork _contractUnitOfWork;
    private readonly IMapper _mapper;

    public ContractService(IContractUnitOfWork contractUnitOfWork, IMapper mapper)
    {
        _contractUnitOfWork = contractUnitOfWork;
        _mapper = mapper;
    }

    public async Task CreateContract(CreateContractRequestDto createContractDto)
    {
        var contractEntity = _mapper.Map<Contracts>(createContractDto);
        await _contractUnitOfWork.ContractRepository.AddAsync(contractEntity);
        await _contractUnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateContract(Guid id, UpdateContractRequestDto updateContractDto)
    {
        var contract = await _contractUnitOfWork.ContractRepository.FindOneAsync(entity => entity.Id == id,
            new FindOptions()
            {
                IsIgnoreAutoIncludes = true
            });
        if (contract == null)
            throw new KeyNotFoundException("Contract not found");

        var contractEntity = _mapper.Map<PartialContracts>(updateContractDto);

        _contractUnitOfWork.ContractRepository.Update(contractEntity, contract);
        
        await _contractUnitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<ContractResponseDto>> GetAllContracts(int? page, int? limit)
    {
        var contracts = _contractUnitOfWork.ContractRepository.GetAll(page, limit,
            new FindOptions()
            {
                IsAsNoTracking = true
            });
        var totalContracts = await _contractUnitOfWork.ContractRepository.CountAsync();

        var contractDtos = _mapper.Map<ContractResponseDto[]>(contracts);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalContracts;

        return new Pagination<ContractResponseDto>(contractDtos, totalContracts,
            currentPage,
            currentLimit);
    }

    public async Task<ContractResponseDto?> GetContractById(Guid id)
    {
        var contract = await _contractUnitOfWork.ContractRepository.FindOneAsync(entity => entity.Id == id);
        if (contract == null)
            throw new KeyNotFoundException("Contract not found");
        
        var contractDto = _mapper.Map<ContractResponseDto>(contract);
        return contractDto;
    }
}