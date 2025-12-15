using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Category;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.DomainEvents.Category;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Application.Services;

internal sealed class CategoryService(
    ICategoryQueryRepository queryRepository,
    ICategoryRepository repository,
    ICategoryPageRepository pageRepository,
    IUnitOfWork unitOfWork) : ICategoryService
{
    public async Task<PagedResult<CategoryListItemDTO>> GetListItemsAsync(CategoryFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task<Dictionary<int, string>> GetIdAndNameListAsync(int? excludeSelfId, int[]? excludeDescendants,
        CancellationToken cancellationToken)
        => await queryRepository.GetIdAndNameAsync(excludeSelfId, excludeDescendants, cancellationToken);

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await queryRepository.GetMaxSortOrderAsync(cancellationToken);

    public async Task AddAsync(AddCategoryCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CategoryErrors.DuplicateName);

        exists = await queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CategoryErrors.DuplicateSlug);

        var parent = command.ParentId is not null
            ? await repository.GetByIdAsync((int)command.ParentId, CategoryInclude.None, false, cancellationToken)
            : null;

        var category = new Category(command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText,
            command.AnchorTitle, command.GoogleCategory, parent, command.ShortDescription,
            command.FullDescription, command.IsActive, command.SortOrder, command.CreatedBy, command.CreatedByIp);

        var page = new CategoryPage(null, command.MetaTitle, command.MetaDescription, command.MetaKeywords, null,
            command.H1, null, null, null, null, null, null, null, null,
            null, null, null, 0, SiteMapFrequency.Yearly, null, true, null
            , null, "en", "UK", 0, true, command.CreatedBy, command.CreatedAt, command.CreatedByIp, category);
        await repository.AddAsync(category, cancellationToken);
        await pageRepository.AddAsync(page, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<CategoryDTO?> GetByCategoryIdAsync(int id, CancellationToken cancellationToken)
        => await queryRepository.GetByIdAsync(id, cancellationToken);

    public async Task EditAsync(EditCategoryCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CategoryErrors.DuplicateName);

        exists = await queryRepository.ExistsBySlugAsync(command.Slug, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CategoryErrors.DuplicateSlug);

        var category = await repository.GetByIdAsync(command.Id, CategoryInclude.None, true, cancellationToken);
        if (category is null) throw new DomainException(DomainErrors.CategoryErrors.InvalidId);

        if (command.ParentId is not null)
        {
            exists = await queryRepository.ExistsByParentIdAsync(command.ParentId, cancellationToken);
            if (!exists) throw new DomainException(DomainErrors.CategoryErrors.InvalidParentId);
        }

        var page = await pageRepository.GetByCategoryIdAsync(command.Id, CategoryPageInclude.None, true,
            cancellationToken);
        if (page is null) throw new DomainException(DomainErrors.CategoryPageErrors.InvalidCategoryId);

        var parent = command.ParentId is not null
            ? await repository.GetByIdAsync((int)command.ParentId, CategoryInclude.None, false, cancellationToken)
            : null;

        var categoryUpdated = category.Update(command.Name, command.Slug, command.Display, command.Breadcrumb,
            command.AnchorText, command.AnchorTitle, parent, command.GoogleCategory, command.ShortDescription,
            command.FullDescription, command.IsActive, command.SortOrder, command.UpdatedBy, command.UpdatedByIp);

        var pageUpdated = page.Update(command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (!categoryUpdated && !pageUpdated) return;
        
        if (categoryUpdated)
        {
            //Slug changed.
            var slugChangedEvent = category.DomainEvents.OfType<CategorySlugUpdatedDomainEvent>().FirstOrDefault();
            if (slugChangedEvent is not null)
            {
                //Get descendants.
                var descendants = await repository.GetDescendantsAsync(slugChangedEvent.Id, CategoryInclude.None,
                    true, cancellationToken);
                //Remove the current category from the list.
                descendants = descendants.Where(x => x.Id != slugChangedEvent.Id).ToList();
                //Update the path of the descendants.
                foreach (var descendant in descendants)
                {
                    descendant.UpdatePath(slugChangedEvent.NewPath);
                }
            }
        }

        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<int[]> GetDescendantIdsAsync(int id, CancellationToken cancellationToken)
        => await queryRepository.GetDescendantIdsAsync(id, cancellationToken);
}