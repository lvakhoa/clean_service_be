using CleanService.Src.Models;
using CleanService.Src.Modules.Contract.Mapping.DTOs;

namespace CleanService.Src.Repositories.Contract;

public interface IContractRepository : IRepository<Contracts, PartialContracts>
{
}