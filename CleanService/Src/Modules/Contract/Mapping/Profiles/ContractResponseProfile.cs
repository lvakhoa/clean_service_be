using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Contract.Mapping.DTOs;

namespace CleanService.Src.Modules.Contract.Mapping.Profiles;

public class ContractResponseProfile : Profile
{
    public ContractResponseProfile()
    {
        CreateMap<BookingContracts, ContractResponseDto>()
            .ConstructUsing(entity => new ContractResponseDto
            {
                Id = entity.Id,
                BookingId = entity.BookingId,
                Content = entity.Content,
                CreatedAt = entity.CreatedAt
            });
    }
}