using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Admin.Product;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Product;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class ProductService(
    IProductRepository repository,
    IProductQueryRepository queryRepository,
    IProductPageRepository pageRepository,
    IUnitOfWork unitOfWork,
    IBrandQueryRepository brandQueryRepository,
    IProductGroupQueryRepository productGroupQueryRepository,
    IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository,
    IDeliveryWindowQueryRepository deliveryWindowQueryRepository)
    : IProductService
{
    public async Task<PagedResult<ProductListItemDTO>> GetListItemsAsync(ProductFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddProductCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsBySKUAsync(command.SKU, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductErrors.DuplicateSKU);

        exists = await queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductErrors.DuplicateName);

        exists = await queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductErrors.DuplicateSlug);

        exists = await brandQueryRepository.ExistsByIdAsync(command.BrandId, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.BrandErrors.InvalidBrandId);

        if (command.ProductGroupId is not null)
        {
            exists = await productGroupQueryRepository.ExistsByIdAsync(command.ProductGroupId.Value, cancellationToken);
            if (exists) throw new DomainException(DomainErrors.ProductGroupErrors.InvalidProductGroupId);
        }
        
        exists = await unitOfMeasureQueryRepository.ExistsByIdAsync(command.UnitOfMeasureId, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.UnitOfMeasureErrors.InvalidUnitOfMeasureId);
        
        exists = await deliveryWindowQueryRepository.ExistsByIdAsync(command.DeliveryWindowId, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.DeliveryWindowErrors.InvalidDeliveryWindowId);

        var product = new Product(command.SKU, command.GTIN, command.MPN, command.MFC, command.EAN, command.UPC,
            command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText, command.AnchorTitle,
            command.BrandId, command.ProductGroupId, command.ShortDescription, command.FullDescription,
            command.AllowReviews, command.Price, command.OldPrice, command.CostPrice, command.Stock, command.MinStock,
            command.ShowAvailability, command.FreeShipping, command.AdditionalShippingCharge, command.UnitOfMeasureId,
            command.QuantityPerUnitOfMeasure, command.DeliveryWindowId, command.MinOrderQuantity,
            command.MaxOrderQuantity, command.IsFeatured, command.IsNew, command.IsBestSeller, command.IsReturnable,
            command.Status, command.RedirectUrl, command.SortOrder, command.CreatedBy, command.CreatedAt,
            command.CreatedByIp);

        var page = new ProductPage(null, command.MetaTitle, command.MetaDescription, command.MetaKeywords, null,
            command.H1, null, null, null, null, null, null, null,
            null, null, null, null, 0, SiteMapFrequency.Always,
            null, true, null, null, "en", "IN", 0,
            true, command.CreatedBy, command.CreatedAt, command.CreatedByIp, product);

        await repository.AddAsync(product, cancellationToken);
        await pageRepository.AddAsync(page, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditAsync(EditProductCommand command, CancellationToken cancellationToken)
    {
    }

    public async Task<decimal> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await queryRepository.GetMaxSortOrderAsync(cancellationToken);
}