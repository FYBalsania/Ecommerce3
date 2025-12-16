using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Admin.Product;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTO.Admin.Product;
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
    IUnitOfWork unitOfWork,
    IBrandQueryRepository brandQueryRepository,
    ICategoryQueryRepository categoryQueryRepository,
    IProductGroupQueryRepository productGroupQueryRepository,
    IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository,
    IDeliveryWindowQueryRepository deliveryWindowQueryRepository,
    IProductPageRepository pageRepository) : IProductService
{
    public async Task<PagedResult<ProductListItemDTO>> GetListItemsAsync(ProductFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddProductCommand command, CancellationToken cancellationToken)
    {
        //SKU duplicate check.
        var exists = await queryRepository.ExistsBySKUAsync(command.SKU, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductErrors.DuplicateSKU);

        //Name duplicate check.
        exists = await queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductErrors.DuplicateName);

        //Slug duplicate check.
        exists = await queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductErrors.DuplicateSlug);

        //Brand exists check.
        exists = await brandQueryRepository.ExistsByIdAsync(command.BrandId, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidBrandId);

        //Category exists check.
        exists = await categoryQueryRepository.ExistsByIdsAsync(command.CategoryIds, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidCategoryId);

        //Product group exists check.
        if (command.ProductGroupId is not null)
        {
            exists = await productGroupQueryRepository.ExistsByIdAsync(command.ProductGroupId.Value, cancellationToken);
            if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidProductGroupId);
        }

        //Unit of measure exists check.
        exists = await unitOfMeasureQueryRepository.ExistsByIdAsync(command.UnitOfMeasureId, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidUnitOfMeasureId);

        //Delivery window exists check.
        exists = await deliveryWindowQueryRepository.ExistsByIdAsync(command.DeliveryWindowId, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidDeliveryWindowId);

        var product = new Product(command.SKU, command.GTIN, command.MPN, command.MFC, command.EAN, command.UPC,
            command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText, command.AnchorTitle,
            command.BrandId, command.CategoryIds, command.ProductGroupId, command.ShortDescription,
            command.FullDescription, command.AllowReviews, command.Price, command.OldPrice, command.CostPrice,
            command.Stock, command.MinStock, command.ShowAvailability, command.FreeShipping,
            command.AdditionalShippingCharge, command.UnitOfMeasureId, command.QuantityPerUnitOfMeasure,
            command.DeliveryWindowId, command.MinOrderQuantity, command.MaxOrderQuantity, command.IsFeatured,
            command.IsNew, command.IsBestSeller, command.IsReturnable, command.Status, command.RedirectUrl,
            command.SortOrder, command.H1, command.MetaTitle, command.MetaDescription, command.MetaKeywords,
            command.CreatedBy, command.CreatedAt, command.CreatedByIp);

        await repository.AddAsync(product, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditAsync(EditProductCommand command, CancellationToken cancellationToken)
    {
        //SKU duplicate check.
        var exists = await queryRepository.ExistsBySKUAsync(command.SKU, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductErrors.DuplicateSKU);

        //Name duplicate check.
        exists = await queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductErrors.DuplicateName);

        //Slug duplicate check.
        exists = await queryRepository.ExistsBySlugAsync(command.Slug, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ProductErrors.DuplicateSlug);

        //Brand exists check.
        exists = await brandQueryRepository.ExistsByIdAsync(command.BrandId, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidBrandId);

        //Category exists check.
        exists = await categoryQueryRepository.ExistsByIdsAsync(command.CategoryIds, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidCategoryId);

        //Product group exists check.
        if (command.ProductGroupId is not null)
        {
            exists = await productGroupQueryRepository.ExistsByIdAsync(command.ProductGroupId.Value, cancellationToken);
            if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidProductGroupId);
        }

        //Unit of measure exists check.
        exists = await unitOfMeasureQueryRepository.ExistsByIdAsync(command.UnitOfMeasureId, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidUnitOfMeasureId);

        //Delivery window exists check.
        exists = await deliveryWindowQueryRepository.ExistsByIdAsync(command.DeliveryWindowId, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidDeliveryWindowId);

        //Get Product
        var product = await repository.GetByIdAsync(command.Id, ProductInclude.Categories, true, cancellationToken);
        if (product is null) throw new DomainException(DomainErrors.ProductErrors.InvalidId);

        var page = await pageRepository.GetByProductIdAsync(command.Id, ProductPageInclude.None, true,
            cancellationToken);
        if (page is null) throw new DomainException(DomainErrors.ProductPageErrors.InvalidProductId);

        product.Update(command.SKU, command.GTIN, command.MPN, command.MFC, command.EAN,
            command.UPC, command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText,
            command.AnchorTitle, command.BrandId, command.CategoryIds, command.ProductGroupId, command.ShortDescription,
            command.FullDescription, command.AllowReviews, command.Price, command.OldPrice, command.CostPrice,
            command.Stock, command.MinStock, command.ShowAvailability, command.FreeShipping,
            command.AdditionalShippingCharge, command.UnitOfMeasureId, command.QuantityPerUnitOfMeasure,
            command.DeliveryWindowId, command.MinOrderQuantity, command.MaxOrderQuantity, command.IsFeatured,
            command.IsNew, command.IsBestSeller, command.IsReturnable, command.Status, command.RedirectUrl,
            command.SortOrder, command.H1, command.MetaTitle, command.MetaDescription, command.MetaKeywords,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        page.Update(command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<decimal> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await queryRepository.GetMaxSortOrderAsync(cancellationToken);

    public async Task<ProductDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await queryRepository.GetByIdAsync(id, cancellationToken);
}