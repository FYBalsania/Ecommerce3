using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Category;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.DomainEvents.Category;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Application.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryPageRepository _categoryPageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryQueryRepository categoryQueryRepository, ICategoryRepository categoryRepository,
        ICategoryPageRepository categoryPageRepository, IUnitOfWork unitOfWork)
    {
        _categoryQueryRepository = categoryQueryRepository;
        _categoryRepository = categoryRepository;
        _categoryPageRepository = categoryPageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<CategoryListItemDTO>> GetCategoryListItemsAsync(CategoryFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _categoryQueryRepository.GetCategoryListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task<Dictionary<int, string>> GetCategoryIdAndNameAsync(CancellationToken cancellationToken)
        => await _categoryQueryRepository.GetCategoryIdAndNameAsync(cancellationToken);

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _categoryQueryRepository.GetMaxSortOrderAsync(cancellationToken);

    public async Task AddAsync(AddCategoryCommand command, CancellationToken cancellationToken)
    {
        var exists = await _categoryQueryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(Brand.Name));

        exists = await _categoryQueryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(Brand.Slug)} already exists.", nameof(Brand.Slug));

        //Path.
        LTree path;
        if (command.ParentId is not null)
        {
            var parent =
                await _categoryRepository.GetByIdAsync((int)command.ParentId, CategoryInclude.None, false,
                    cancellationToken);
            path = new LTree($"{parent!.Path}.{command.Slug}");
        }
        else
            path = new LTree(command.Slug);
        //

        var category = new Category(command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText,
            command.AnchorTitle, command.GoogleCategory, command.ParentId, path, command.ShortDescription,
            command.FullDescription, command.IsActive, command.SortOrder, command.CreatedBy, command.CreatedByIp);

        var page = new CategoryPage(null, command.MetaTitle, command.MetaDescription, command.MetaKeywords, null,
            command.H1, null, null, null, null, null, null, null, null,
            null, null, null, 0, SiteMapFrequency.Yearly, null, true, null
            , null, "en", "UK", 0, true, command.CreatedBy, command.CreatedAt, command.CreatedByIp, category);
        await _categoryRepository.AddAsync(category, cancellationToken);
        await _categoryPageRepository.AddAsync(page, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<CategoryDTO?> GetByCategoryIdAsync(int id, CancellationToken cancellationToken)
    {
        var category = await _categoryQueryRepository.GetByIdAsync(id, cancellationToken);

        return new CategoryDTO()
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Display = category.Display,
            Breadcrumb = category.Breadcrumb,
            AnchorText = category.AnchorText,
            AnchorTitle = category.AnchorTitle,
            ParentId = category.ParentId,
            GoogleCategory = category.GoogleCategory,
            Path = category.Path,
            ShortDescription = category.ShortDescription,
            FullDescription = category.FullDescription,
            IsActive = category.IsActive,
            SortOrder = category.SortOrder,
            H1 = category.H1,
            MetaTitle = category.MetaTitle,
            MetaDescription = category.MetaDescription,
            MetaKeywords = category.MetaKeywords
        };
    }

    public async Task UpdateAsync(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var exists = await _categoryQueryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(Category.Name));

        exists = await _categoryQueryRepository.ExistsBySlugAsync(command.Slug, command.Id, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(Category.Slug)} already exists.", nameof(Category.Slug));

        var category =
            await _categoryRepository.GetByIdAsync(command.Id, CategoryInclude.None, true, cancellationToken);
        if (category is null) throw new ArgumentNullException(nameof(command.Id), "Category not found.");

        var page = await _categoryPageRepository.GetByCategoryIdAsync(command.Id, CategoryPageInclude.None, true,
            cancellationToken);
        if (page is null) throw new ArgumentNullException(nameof(command.Id), "Category page not found.");
        
        //Path.

        var categoryUpdated = category.Update(command.Name, command.Slug, command.Display, command.Breadcrumb,
            command.AnchorText, command.AnchorTitle, command.ParentId, command.GoogleCategory, command.ShortDescription,
            command.FullDescription, command.IsActive, command.SortOrder, command.UpdatedBy, command.UpdatedAt,
            command.UpdatedByIp);

        var pageUpdated = page.Update(command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (categoryUpdated)
        {
            _categoryRepository.Update(category);
            foreach (var domainEvent in category.DomainEvents)
            {
                switch (domainEvent)
                {
                    case CategoryParentIdUpdatedDomainEvent:
                        break;
                    case CategorySlugUpdatedDomainEvent:
                        break;
                }
            }
        }

        if (pageUpdated) _categoryPageRepository.Update(page);

        if (categoryUpdated || pageUpdated) await _unitOfWork.CompleteAsync(cancellationToken);
    }
}