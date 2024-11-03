using CleanService.Src.Models;
using CleanService.Src.Modules.Contract.Mapping.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Repositories.Contract;

public class ContractRepository : Repository<Contracts>, IContractRepository
{
    private readonly CleanServiceContext _dbContext;

    public ContractRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ContractReturnDto> CreateContract(CreateContractDto createContractDto)
    {
        var contractEntity = await _dbContext.Contracts.AddAsync(new Contracts
        {
            BookingId = createContractDto.BookingId,
            Content = createContractDto.Content,
        });
        
        await _dbContext.SaveChangesAsync();

        return new ContractReturnDto
        {
            Id = contractEntity.Entity.Id,
            BookingId = contractEntity.Entity.BookingId,
            Content = contractEntity.Entity.Content,
            CreatedAt = contractEntity.Entity.CreatedAt,
        };
    }

    public async Task<ContractReturnDto?> UpdateContract(Guid id, UpdateContractDto updateContractDto)
    {
        var contract = await _dbContext.Contracts.FirstOrDefaultAsync(x => x.Id == id);
        if (contract is null)
            return null;

        // if (updateContractDto.BookingId.HasValue)
        //     contract.BookingId = updateContractDto.BookingId.Value;
        // if(updateContractDto.Content is not null)
        //     contract.Content = updateContractDto.Content;
        
        contract.BookingId = updateContractDto.BookingId ?? contract.BookingId;
        contract.Content = updateContractDto.Content ?? contract.Content;

        
        _dbContext.SaveChanges();

        return new ContractReturnDto
        {
            Id = contract.Id,
            BookingId = contract.BookingId,
            Content = contract.Content,
            CreatedAt = contract.CreatedAt,
        };
    }

    public async Task<ContractReturnDto?> GetContractById(Guid id)
    {
        var contract = await _dbContext.Contracts.FirstOrDefaultAsync(x => x.Id == id);

        return contract is not null
            ? new ContractReturnDto
            {
                Id = contract.Id,
                BookingId = contract.BookingId,
                Content = contract.Content,
                CreatedAt = contract.CreatedAt,
            }
            : null;
    }

    public async Task<ContractReturnDto[]> GetAllContract()
    {
        var contracts = await _dbContext.Contracts.ToArrayAsync();

        return contracts.Select(contract => new ContractReturnDto
        {
            Id = contract.Id,
            BookingId = contract.BookingId,
            Content = contract.Content,
            CreatedAt = contract.CreatedAt,
        }).ToArray();
    }
}