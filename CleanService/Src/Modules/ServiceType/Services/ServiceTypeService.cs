using System.Linq.Expressions;

using AutoMapper;

using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.ServiceType.Services;

public class ServiceTypeService : IServiceTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServiceTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateServiceType(CreateServiceTypeRequestDto createServiceTypeDto)
    {
        var serviceType = _mapper.Map<ServiceTypes>(createServiceTypeDto);
        await _unitOfWork.Repository<ServiceCategories, PartialServiceCategories>().GetFirstOrThrowAsync(ServiceCategorySpecification.GetServiceCategoryById(Guid.Parse(createServiceTypeDto.CategoryId))
       );

        await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>().AddAsync(serviceType);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateServiceType(Guid id, UpdateServiceTypeRequestDto updateServiceTypeDto)
    {
        var serviceType = await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>().GetFirstOrThrowAsync(
        );

        var serviceTypeEntity = _mapper.Map<PartialServiceTypes>(updateServiceTypeDto);

        // await _unitOfWork.Repository<ServiceCategories, PartialServiceCategories>().GetFirstOrThrowAsync(ServiceCategorySpecification.GetServiceCategoryById(id)
        // );

        await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>().Detach(serviceType);
        await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>().UpdateAsync(serviceTypeEntity, serviceType);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<ServiceTypeResponseDto>> GetServiceTypes(int? page, int? limit, Guid? categoryId)
    {
        var serviceTypeSpec = ServiceTypeSpecification.GetServiceTypeByCategoryIdSpec(categoryId);
        if (page.HasValue && limit.HasValue)
        {
            serviceTypeSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }

        var serviceTypes = await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>().GetAllAsync(serviceTypeSpec);
        var totalServiceTypes = await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>().CountAsync();

        var serviceTypeDtos = _mapper.Map<List<ServiceTypeResponseDto>>(serviceTypes);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalServiceTypes;

        return new Pagination<ServiceTypeResponseDto>(serviceTypeDtos, totalServiceTypes,
            currentPage,
            currentLimit);
    }

    public async Task<ServiceTypeResponseDto?> GetServiceTypeById(Guid id)
    {
        var serviceTypeEntity = await _unitOfWork.Repository<ServiceTypes, PartialServiceTypes>().GetFirstOrThrowAsync(ServiceTypeSpecification.GetServiceTypeByIdSpec(id));
        return _mapper.Map<ServiceTypeResponseDto>(serviceTypeEntity);
    }
}
