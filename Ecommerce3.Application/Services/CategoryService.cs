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

    public async Task<Dictionary<int, string>> GetIdAndNameListAsync(int[]? excludeIds, CancellationToken cancellationToken)
        => await queryRepository.GetIdAndNameAsync(excludeIds, cancellationToken);

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
            exists = await queryRepository.ExistsByIdAsync((int)command.ParentId, cancellationToken);
            if (!exists) throw new DomainException(DomainErrors.CategoryErrors.InvalidParentId);
        }

        var page = await pageRepository.GetByCategoryIdAsync(command.Id, CategoryPageInclude.None, true,
            cancellationToken);
        if (page is null) throw new DomainException(DomainErrors.CategoryPageErrors.InvalidCategoryId);

        var parent = command.ParentId is not null
            ? await repository.GetByIdAsync((int)command.ParentId, CategoryInclude.None, false, cancellationToken)
            : null;

        category.Update(command.Name, command.Slug, command.Display, command.Breadcrumb,
            command.AnchorText, command.AnchorTitle, parent, command.GoogleCategory, command.ShortDescription,
            command.FullDescription, command.IsActive, command.SortOrder, command.UpdatedBy, command.UpdatedByIp);

        page.Update(command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (category.DomainEvents.OfType<CategorySlugUpdatedDomainEvent>().Any())
        {
            var slugUpdatedDomainEvent =
                category.DomainEvents.OfType<CategorySlugUpdatedDomainEvent>().FirstOrDefault()!;
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await repository.UpdateDescendantPathsAsync(slugUpdatedDomainEvent.OldPath,
                    slugUpdatedDomainEvent.NewPath, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
            category.ClearDomainEvents();
        }
        else
        {
            await unitOfWork.CompleteAsync(cancellationToken);
        }
    }

    public async Task<int[]> GetDescendantIdsAsync(int id, CancellationToken cancellationToken)
        => await queryRepository.GetDescendantIdsAsync(id, cancellationToken);
}