using AutoMapper;

using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models;
using CleanService.Src.Modules.ServiceCategory.Mapping.DTOs;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.ServiceCategory.Services;

public class ServiceCategoryService : IServiceCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServiceCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateServiceCategory(CreateServiceCategoryRequestDto createServiceCategoryDto)
    {
        var serviceCategory = _mapper.Map<ServiceCategories>(createServiceCategoryDto);
        await _unitOfWork.Repository<ServiceCategories, PartialServiceCategories>().AddAsync(serviceCategory);
    }

    public async Task UpdateServiceCategory(Guid id, UpdateServiceCategoryRequestDto updateServiceCategoryDto)
    {
        var serviceCategory = await _unitOfWork.Repository<ServiceCategories, PartialServiceCategories>()
            .GetFirstOrThrowAsync(ServiceCategorySpecification.GetServiceCategoryById(id));
        await _unitOfWork.Repository<ServiceCategories, PartialServiceCategories>().Detach(serviceCategory);

        var serviceCategoryEntity = _mapper.Map<PartialServiceCategories>(updateServiceCategoryDto);

        await _unitOfWork.Repository<ServiceCategories, PartialServiceCategories>()
            .UpdateAsync(serviceCategoryEntity, serviceCategory);
    }

    public async Task<ServiceCategoryResponseDto?> GetServiceCategoryById(Guid id)
    {
        var serviceCategory = await _unitOfWork.Repository<ServiceCategories, PartialServiceCategories>()
            .GetFirstOrThrowAsync(ServiceCategorySpecification.GetServiceCategoryById(id));

        return _mapper.Map<ServiceCategoryResponseDto>(serviceCategory);
    }

    public async Task<Pagination<ServiceCategoryResponseDto>> GetServiceCategories(int? page, int? limit)
    {
        var serviceCategories = await _unitOfWork.Repository<ServiceCategories, PartialServiceCategories>()
            .GetAllAsync(ServiceCategorySpecification.GetPagedServiceCategory(page, limit));
        var totalServiceCategories =
            await _unitOfWork.Repository<ServiceCategories, PartialServiceCategories>().CountAsync();

        var serviceCategoryDtos = _mapper.Map<ServiceCategoryResponseDto[]>(serviceCategories);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalServiceCategories;

        return new Pagination<ServiceCategoryResponseDto>(serviceCategoryDtos, totalServiceCategories, currentPage,
            currentLimit);
    }
}
