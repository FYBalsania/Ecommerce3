using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Category;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Application.Services;

internal sealed class CategoryService : ICategoryService
{
    private readonly ICategoryQueryRepository _queryRepository;
    private readonly ICategoryRepository _repository;
    private readonly ICategoryPageRepository _pageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryQueryRepository queryRepository, ICategoryRepository repository,
        ICategoryPageRepository pageRepository, IUnitOfWork unitOfWork)
    {
        _queryRepository = queryRepository;
        _repository = repository;
        _pageRepository = pageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<CategoryListItemDTO>> GetListItemsAsync(CategoryFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task<Dictionary<int, string>> GetIdAndNameListAsync(CancellationToken cancellationToken)
        => await _queryRepository.GetIdAndNameAsync(cancellationToken);

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _queryRepository.GetMaxSortOrderAsync(cancellationToken);

    public async Task AddAsync(AddCategoryCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CategoryErrors.DuplicateName);

        exists = await _queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CategoryErrors.DuplicateSlug);

        var parent = command.ParentId is not null
            ? await _repository.GetByIdAsync((int)command.ParentId, CategoryInclude.None, false,
                cancellationToken)
            : null;

        var category = new Category(command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText,
            command.AnchorTitle, command.GoogleCategory, parent, command.ShortDescription,
            command.FullDescription, command.IsActive, command.SortOrder, command.CreatedBy, command.CreatedByIp);

        var page = new CategoryPage(null, command.MetaTitle, command.MetaDescription, command.MetaKeywords, null,
            command.H1, null, null, null, null, null, null, null, null,
            null, null, null, 0, SiteMapFrequency.Yearly, null, true, null
            , null, "en", "UK", 0, true, command.CreatedBy, command.CreatedAt, command.CreatedByIp, category);
        await _repository.AddAsync(category, cancellationToken);
        await _pageRepository.AddAsync(page, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<CategoryDTO?> GetByCategoryIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _queryRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task EditAsync(EditCategoryCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CategoryErrors.DuplicateName);

        exists = await _queryRepository.ExistsBySlugAsync(command.Slug, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CategoryErrors.DuplicateSlug);

        var category =
            await _repository.GetByIdAsync(command.Id, CategoryInclude.None, true, cancellationToken);
        if (category is null) throw new ArgumentNullException(nameof(command.Id), "Category not found.");

        var page = await _pageRepository.GetByCategoryIdAsync(command.Id, CategoryPageInclude.None, true,
            cancellationToken);
        if (page is null) throw new ArgumentNullException(nameof(command.Id), $"Category {command.Id} page not found.");

        var parent = command.ParentId is not null
            ? await _repository.GetByIdAsync((int)command.ParentId, CategoryInclude.None, false,
                cancellationToken)
            : null;

        var categoryUpdated = category.Update(command.Name, command.Slug, command.Display, command.Breadcrumb,
            command.AnchorText, command.AnchorTitle, parent, command.GoogleCategory, command.ShortDescription,
            command.FullDescription, command.IsActive, command.SortOrder, command.UpdatedBy, command.UpdatedByIp);

        var pageUpdated = page.Update(command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (!categoryUpdated && !pageUpdated) return;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            if (categoryUpdated)
            {
                _repository.Update(category);
                await _repository.UpdateDescendantsPath(new LTree(""), new LTree(""), cancellationToken);
            }
            if (pageUpdated) _pageRepository.Update(page);

            await _unitOfWork.CompleteAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public Type EntityType { get; }

    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}