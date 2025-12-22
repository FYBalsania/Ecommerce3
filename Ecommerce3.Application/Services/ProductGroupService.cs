using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductGroup;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.ProductGroup;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class ProductGroupService(
    IProductGroupRepository repository,
    IProductGroupQueryRepository queryRepository,
    IProductGroupPageRepository pageRepository,
    IProductAttributeQueryRepository productAttributeQueryRepository,
    IProductAttributeValueQueryRepository productAttributeValueQueryRepository,
    IUnitOfWork unitOfWork) : IProductGroupService
{
    public async Task<PagedResult<ProductGroupListItemDTO>> GetListItemsAsync(ProductGroupFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddProductGroupCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductGroupErrors.DuplicateName);

        exists = await queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductGroupErrors.DuplicateSlug);

        var productGroup = new ProductGroup(command.Name, command.Slug, command.Display, command.Breadcrumb,
            command.AnchorText,
            command.AnchorTitle, command.ShortDescription, command.FullDescription, command.IsActive, command.SortOrder,
            command.CreatedBy, command.CreatedByIp);

        var page = new ProductGroupPage(null, command.MetaTitle, command.MetaDescription, command.MetaKeywords, null,
            command.H1,
            null, null, null, null, null, null, null, null,
            null, null, null, 0, SiteMapFrequency.Yearly, null, true, null
            , null, "en", "UK", 0, true, command.CreatedBy, command.CreatedAt, command.CreatedByIp, productGroup);
        await repository.AddAsync(productGroup, cancellationToken);
        await pageRepository.AddAsync(page, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<ProductGroupDTO?> GetByProductGroupIdAsync(int id, CancellationToken cancellationToken)
        => await queryRepository.GetByIdAsync(id, cancellationToken);

    public async Task<IDictionary<int, string>> GetIdAndNameListAsync(CancellationToken cancellationToken)
        => await queryRepository.GetIdAndNameListAsync(cancellationToken);

    public async Task AddAttributeAsync(AddProductGroupProductAttributeCommand command,
        CancellationToken cancellationToken)
    {
        //Validate ProductAttributeId.
        var exists =
            await productAttributeQueryRepository.ExistsByIdAsync(command.ProductAttributeId, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductAttributeErrors.InvalidProductAttributeId);

        //Validate ProductAttributeValueId.
        foreach (var commandValue in command.Values)
        {
            exists = await productAttributeValueQueryRepository.ExistsAsync(commandValue.Key,
                command.ProductAttributeId, cancellationToken);
            if (!exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidId);
        }

        //Fetch ProductGroup.
        var productGroup =
            await repository.GetByIdAsync(command.ProductGroupId, ProductGroupInclude.Attributes, true,
                cancellationToken);
        if (productGroup is null) throw new DomainException(DomainErrors.ProductGroupErrors.InvalidId);

        //Add ProductAttribute to the ProductGroup.
        productGroup.AddAttribute(command.ProductAttributeId, command.SortOrder, command.Values, command.CreatedBy,
            command.CreatedAt, command.CreatedByIp);
        
        //Commit changes.
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditAsync(EditProductGroupCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductGroupErrors.DuplicateName);

        exists = await queryRepository.ExistsBySlugAsync(command.Slug, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductGroupErrors.DuplicateSlug);

        var productGroup = await repository.GetByIdAsync(command.Id, ProductGroupInclude.None, true, cancellationToken);
        if (productGroup is null) throw new DomainException(DomainErrors.ProductGroupErrors.InvalidId);

        var page = await pageRepository.GetByProductGroupIdAsync(command.Id, ProductGroupPageInclude.None, true,
            cancellationToken);
        if (page is null) throw new DomainException(DomainErrors.ProductGroupPageErrors.InvalidProductGroupId);

        productGroup.Update(command.Name, command.Slug, command.Display, command.Breadcrumb,
            command.AnchorText, command.AnchorTitle, command.ShortDescription, command.FullDescription,
            command.IsActive, command.SortOrder, command.UpdatedBy, command.UpdatedByIp);

        var pageUpdated = page.Update(command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (pageUpdated) pageRepository.Update(page);

        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await queryRepository.GetMaxSortOrderAsync(cancellationToken);
    
    public async Task<IReadOnlyList<ProductGroupProductAttributeDTO>> GetAttributesAsync(int id, CancellationToken cancellationToken)
        => await queryRepository.GetAttributesAsync(id, cancellationToken);
}