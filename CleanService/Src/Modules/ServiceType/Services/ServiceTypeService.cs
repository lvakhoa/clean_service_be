using System.Linq.Expressions;
using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.ServiceType.Infrastructures;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;
using CleanService.Src.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.ServiceType.Services;

public class ServiceTypeService : IServiceTypeService
{
    private readonly IServiceTypeUnitOfWork _serviceTypeUnitOfWork;
    private readonly IMapper _mapper;

    public ServiceTypeService(IServiceTypeUnitOfWork serviceTypeUnitOfWork, IMapper mapper)
    {
        _serviceTypeUnitOfWork = serviceTypeUnitOfWork;
        _mapper = mapper;
    }

    public async Task CreateServiceType(CreateServiceTypeRequestDto createServiceTypeDto)
    {
        var serviceType = _mapper.Map<ServiceTypes>(createServiceTypeDto);
        var category =
            await _serviceTypeUnitOfWork.ServiceCategoryRepository.FindOneAsync(entity =>
                entity.Id == serviceType.CategoryId);
        if (category == null)
            throw new KeyNotFoundException("Category not found");

        await _serviceTypeUnitOfWork.ServiceTypeRepository.AddAsync(serviceType);
        await _serviceTypeUnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateServiceType(Guid id, UpdateServiceTypeRequestDto updateServiceTypeDto)
    {
        var serviceType = await _serviceTypeUnitOfWork.ServiceTypeRepository.FindOneAsync(entity => entity.Id == id,
            new FindOptions
            {
                IsAsNoTracking = true,
                IsIgnoreAutoIncludes = true
            });
        if (serviceType == null)
            throw new KeyNotFoundException("Service type not found");

        var serviceTypeEntity = _mapper.Map<PartialServiceTypes>(updateServiceTypeDto);

        var category =
            await _serviceTypeUnitOfWork.ServiceCategoryRepository.FindOneAsync(entity =>
                entity.Id == serviceType.CategoryId, new FindOptions
            {
                IsAsNoTracking = true,
                IsIgnoreAutoIncludes = true
            });
        if (category == null)
            throw new KeyNotFoundException("Category not found");

        _serviceTypeUnitOfWork.ServiceTypeRepository.Detach(serviceType);
        _serviceTypeUnitOfWork.ServiceTypeRepository.Update(serviceTypeEntity, serviceType);
        await _serviceTypeUnitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<ServiceTypeResponseDto>> GetServiceTypes(int? page, int? limit, Guid? categoryId)
    {
        Expression<Func<ServiceTypes, bool>> predicate = categoryId != null ? entity => entity.CategoryId == categoryId : entity => true;

        var serviceTypes = _serviceTypeUnitOfWork.ServiceTypeRepository.Find(predicate, null, false, page, limit,
            new FindOptions()
            {
                IsAsNoTracking = true
            });
        var totalServiceTypes = await _serviceTypeUnitOfWork.ServiceTypeRepository.CountAsync();

        var serviceTypeDtos = _mapper.Map<ServiceTypeResponseDto[]>(serviceTypes);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalServiceTypes;

        return new Pagination<ServiceTypeResponseDto>(serviceTypeDtos, totalServiceTypes,
            currentPage,
            currentLimit);
    }

    public async Task<ServiceTypeResponseDto?> GetServiceTypeById(Guid id)
    {
        var serviceTypeEntity = await _serviceTypeUnitOfWork.ServiceTypeRepository.FindOneAsync(entity =>
            entity.Id == id);
        if(serviceTypeEntity == null)
            throw new KeyNotFoundException("Service type not found");
        
        return _mapper.Map<ServiceTypeResponseDto>(serviceTypeEntity);
    }
}