using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductGroup;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.ProductGroup;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public sealed class ProductGroupService : IProductGroupService
{
    private readonly IProductGroupRepository _repository;
    private readonly IProductGroupQueryRepository _queryRepository;
    private readonly IProductGroupPageRepository _pageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductGroupService(IProductGroupRepository repository, IProductGroupQueryRepository queryRepository,
        IProductGroupPageRepository pageRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _pageRepository = pageRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PagedResult<ProductGroupListItemDTO>> GetListItemsAsync(ProductGroupFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);
    
    public async Task AddAsync(AddProductGroupCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(ProductGroup.Name));

        exists = await _queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(ProductGroup.Slug)} already exists.", nameof(ProductGroup.Slug));

        var productGroup = new ProductGroup(command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText,
            command.AnchorTitle, command.ShortDescription, command.FullDescription, command.IsActive, command.SortOrder,
            command.CreatedBy, command.CreatedByIp);

        var page = new ProductGroupPage(null, command.MetaTitle, command.MetaDescription, command.MetaKeywords, null,
            command.H1,
            null, null, null, null, null, null, null, null,
            null, null, null, 0, SiteMapFrequency.Yearly, null, true, null
            , null, "en", "UK", 0, true, command.CreatedBy, command.CreatedAt, command.CreatedByIp, productGroup);
        await _repository.AddAsync(productGroup, cancellationToken);
        await _pageRepository.AddAsync(page, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<ProductGroupDTO?> GetByProductGroupIdAsync(int id, CancellationToken cancellationToken)
    {
        var productGroup = await _queryRepository.GetByIdAsync(id, cancellationToken);

        return new ProductGroupDTO
        {
            Id = productGroup.Id,
            Name = productGroup.Name,
            Slug = productGroup.Slug,
            Display = productGroup.Display,
            Breadcrumb = productGroup.Breadcrumb,
            AnchorText = productGroup.AnchorText,
            AnchorTitle = productGroup.AnchorTitle,
            ShortDescription = productGroup.ShortDescription,
            FullDescription = productGroup.FullDescription,
            IsActive = productGroup.IsActive,
            SortOrder = productGroup.SortOrder,
            H1 = productGroup.H1,
            MetaTitle = productGroup.MetaTitle,
            MetaDescription = productGroup.MetaDescription,
            MetaKeywords = productGroup.MetaKeywords
        };
    }
    
    public async Task EditAsync(EditProductGroupCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(ProductGroup.Name));

        exists = await _queryRepository.ExistsBySlugAsync(command.Slug, command.Id, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(ProductGroup.Slug)} already exists.", nameof(ProductGroup.Slug));

        var productGroup = await _repository.GetByIdAsync(command.Id, ProductGroupInclude.None, true, cancellationToken);
        if (productGroup is null) throw new ArgumentNullException(nameof(command.Id), "Product Group not found.");

        var page = await _pageRepository.GetByProductGroupIdAsync(command.Id, ProductGroupPageInclude.None, true,
            cancellationToken);
        if (page is null) throw new ArgumentNullException(nameof(command.Id), "Product Group page not found.");

        var productGroupUpdated = productGroup.Update(command.Name, command.Slug, command.Display, command.Breadcrumb,
            command.AnchorText, command.AnchorTitle, command.ShortDescription, command.FullDescription,
            command.IsActive, command.SortOrder, command.UpdatedBy, command.UpdatedByIp);

        var pageUpdated = page.Update(command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        if (productGroupUpdated) _repository.Update(productGroup);
        if (pageUpdated) _pageRepository.Update(page);

        if (productGroupUpdated || pageUpdated) await _unitOfWork.CompleteAsync(cancellationToken);
    }
    
    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _queryRepository.GetMaxSortOrderAsync(cancellationToken);
}