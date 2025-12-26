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
    IBrandRepository brandRepository,
    ICategoryRepository categoryRepository,
    IBrandQueryRepository brandQueryRepository,
    ICategoryQueryRepository categoryQueryRepository,
    IProductGroupQueryRepository productGroupQueryRepository,
    IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository,
    IDeliveryWindowQueryRepository deliveryWindowQueryRepository,
    IProductPageRepository pageRepository,
    IProductGroupRepository productGroupRepository) : IProductService
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
        var brand = await brandRepository.GetByIdAsync(command.BrandId, BrandInclude.None, false, cancellationToken);
        if (brand is null) throw new DomainException(DomainErrors.ProductErrors.InvalidBrandId);

        //Category exists check.
        var categories =
            await categoryRepository.GetByIdAsync(command.CategoryIds, CategoryInclude.None, false, cancellationToken);
        if (categories is null || categories.Count == 0 || categories.Count != command.CategoryIds.Length)
            throw new DomainException(DomainErrors.ProductErrors.InvalidCategoryId);

        //Product group & attributes check.
        ProductGroup? productGroup = null;
        var productAttributes = new List<Domain.ValueObjects.ProductAttribute>();
        if (command.ProductGroupId is not null)
        {
            //Exists check.
            productGroup = await productGroupRepository.GetByIdAsync(command.ProductGroupId.Value,
                ProductGroupInclude.Attributes | ProductGroupInclude.AttributeValues, false, cancellationToken);
            if (productGroup is null) throw new DomainException(DomainErrors.ProductErrors.InvalidProductGroupId);

            var productGroupAttributeIds = productGroup.Attributes
                .Select(x => x.ProductAttributeId).Distinct().ToArray();

            //Attributes count check.
            if (productGroupAttributeIds.Length != command.Attributes.Count)
                throw new DomainException(DomainErrors.ProductErrors.InvalidAttributesCount);

            //AttributeId & AttributeValueId exists check.
            for (var idx = 0; idx < productGroupAttributeIds.Length; idx++)
            {
                //Attribute and its index position must be equal to the ProductGroupAttribute and its index position.
                if (command.Attributes.ElementAt(idx).Key != productGroupAttributeIds.ElementAt(idx))
                    throw new DomainException(DomainErrors.ProductErrors.InvalidAttributeId);

                //ProductAttributeValueId validity check.
                if (!productGroup.Attributes
                        .Any(x => x.ProductAttributeId == command.Attributes.ElementAt(idx).Key
                                  && x.ProductAttributeValueId == command.Attributes.ElementAt(idx).Value))
                    throw new DomainException(DomainErrors.ProductErrors.InvalidAttributeValueId);
            }

            //Populate productAttribute value objects.
            for (var idx = 0; idx < productGroupAttributeIds.Length; idx++)
            {
                var attribute = productGroup.Attributes
                    .First(x => x.ProductAttributeId == command.Attributes.ElementAt(idx).Key
                                && x.ProductAttributeValueId == command.Attributes.ElementAt(idx).Value);

                productAttributes.Add(new Domain.ValueObjects.ProductAttribute
                {
                    ProductAttributeId = attribute.ProductAttributeId,
                    ProductAttributeSlug = attribute.ProductAttribute!.Slug,
                    ProductAttributeSortOrder = attribute.ProductAttributeSortOrder,
                    ProductAttributeValueId = attribute.ProductAttributeValueId,
                    ProductAttributeValueSlug = attribute.ProductAttributeValue!.Slug,
                    ProductAttributeValueSortOrder = attribute.ProductAttributeValueSortOrder
                });
            }
        }

        //Unit of measure exists check.
        exists = await unitOfMeasureQueryRepository.ExistsByIdAsync(command.UnitOfMeasureId, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidUnitOfMeasureId);

        //Delivery window exists check.
        exists = await deliveryWindowQueryRepository.ExistsByIdAsync(command.DeliveryWindowId, cancellationToken);
        if (!exists) throw new DomainException(DomainErrors.ProductErrors.InvalidDeliveryWindowId);

        var product = new Product(command.SKU, command.GTIN, command.MPN, command.MFC, command.EAN, command.UPC,
            command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText, command.AnchorTitle,
            new KeyValuePair<int, string>(brand.Id, brand.Slug),
            categories.ToDictionary(x => x.Id, y => y.Slug),
            productGroup is not null ? new KeyValuePair<int, string>(productGroup.Id, productGroup.Slug) : null,
            productAttributes, command.ShortDescription,
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