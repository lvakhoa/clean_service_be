using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.ServiceCategory.Infrastructures;
using CleanService.Src.Modules.ServiceCategory.Mapping.DTOs;
using CleanService.Src.Repositories;
using CleanService.Src.Repositories.ServiceCategory;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.ServiceCategory.Services;

public class ServiceCategoryService : IServiceCategoryService
{
    private readonly IServiceCategoryUnitOfWork _serviceCategoryUnitOfWork;
    private readonly IMapper _mapper;

    public ServiceCategoryService(IServiceCategoryUnitOfWork serviceCategoryUnitOfWork, IMapper mapper)
    {
        _serviceCategoryUnitOfWork = serviceCategoryUnitOfWork;
        _mapper = mapper;
    }

    public async Task CreateServiceCategory(CreateServiceCategoryRequestDto createServiceCategoryDto)
    {
        var serviceCategory = _mapper.Map<ServiceCategories>(createServiceCategoryDto);
        await _serviceCategoryUnitOfWork.ServiceCategoryRepository.AddAsync(serviceCategory);
    }

    public async Task UpdateServiceCategory(Guid id, UpdateServiceCategoryRequestDto updateServiceCategoryDto)
    {
        var serviceCategory = await _serviceCategoryUnitOfWork.ServiceCategoryRepository.FindOneAsync(
            entity => entity.Id == id,
            new FindOptions
            {
                IsIgnoreAutoIncludes = true
            });
        if (serviceCategory == null)
            throw new KeyNotFoundException("Service category not found");
        _serviceCategoryUnitOfWork.ServiceCategoryRepository.Detach(serviceCategory);

        var serviceCategoryEntity = _mapper.Map<PartialServiceCategories>(updateServiceCategoryDto);

        _serviceCategoryUnitOfWork.ServiceCategoryRepository.Update(serviceCategoryEntity, serviceCategory);
    }

    public async Task<ServiceCategoryResponseDto?> GetServiceCategoryById(Guid id)
    {
        var serviceCategoryEntity = await _serviceCategoryUnitOfWork.ServiceCategoryRepository.FindOneAsync(entity =>
            entity.Id == id);
        if (serviceCategoryEntity == null)
            throw new KeyNotFoundException("Service category not found");

        return _mapper.Map<ServiceCategoryResponseDto>(serviceCategoryEntity);
    }

    public async Task<Pagination<ServiceCategoryResponseDto>> GetServiceCategories(int? page, int? limit)
    {
        var serviceCategories = _serviceCategoryUnitOfWork.ServiceCategoryRepository.GetAll(page, limit,
            new FindOptions()
            {
                IsAsNoTracking = true
            });
        var totalServiceCategories = await _serviceCategoryUnitOfWork.ServiceCategoryRepository.CountAsync();

        var serviceCategoryDtos = _mapper.Map<ServiceCategoryResponseDto[]>(serviceCategories);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalServiceCategories;

        return new Pagination<ServiceCategoryResponseDto>(serviceCategoryDtos, totalServiceCategories,
            currentPage,
            currentLimit);
    }
}