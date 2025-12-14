using AutoMapper;

using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Contract.Mapping.DTOs;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Contract.Services;

public class ContractService : IContractService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ContractService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateContract(CreateContractRequestDto createContractDto)
    {
        var contractEntity = _mapper.Map<Contracts>(createContractDto);
        await _unitOfWork.Repository<Contracts, PartialContracts>().AddAsync(contractEntity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateContract(Guid id, UpdateContractRequestDto updateContractDto)
    {
        var contract = await _unitOfWork.Repository<Contracts, PartialContracts>()
            .GetFirstOrThrowAsync(ContractSpecification.GetContractByIdSpec(id));

        var contractEntity = _mapper.Map<PartialContracts>(updateContractDto);

        await _unitOfWork.Repository<Contracts, PartialContracts>().UpdateAsync(contractEntity, contract);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<ContractResponseDto>> GetAllContracts(int? page, int? limit)
    {
        var contracts = _unitOfWork.Repository<Contracts, PartialContracts>()
            .GetAllAsync(ContractSpecification.GetPagedContractsSpec(page, limit));
        var totalContracts = await _unitOfWork.Repository<Contracts, PartialContracts>().CountAsync();

        var contractDtos = _mapper.Map<ContractResponseDto[]>(contracts);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalContracts;

        return new Pagination<ContractResponseDto>(contractDtos, totalContracts, currentPage, currentLimit);
    }

    public async Task<ContractResponseDto?> GetContractById(Guid id)
    {
        return await _unitOfWork.Repository<Contracts, PartialContracts>()
            .GetFirstOrThrowAsync(ContractSpecification.GetContractByIdSpec(id))
            .ContinueWith(x => _mapper.Map<ContractResponseDto>(x.Result));
    }
}
