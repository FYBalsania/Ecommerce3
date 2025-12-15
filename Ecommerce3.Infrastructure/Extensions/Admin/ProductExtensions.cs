using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.Admin.Product;
using Ecommerce3.Contracts.DTOs.Product;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class ProductExtensions
{
    private static readonly Expression<Func<Product, ProductListItemDTO>> ListItemDTOExpression = p => new ProductListItemDTO
    {
        Id = p.Id,
        Name = p.Name,
        Slug = p.Slug,
        SortOrder = p.SortOrder,
        ImageCount = p.Images.Count,
        CreatedUserFullName = p.CreatedByUser!.FullName,
        CreatedAt = p.CreatedAt
    };
    
    private static readonly Expression<Func<Product, ProductDTO>> DTOExpression = p => new ProductDTO
    {
        Id = p.Id,
        SKU = p.SKU,
        GTIN = p.GTIN,
        MPN = p.MPN,
        MFC = p.MFC,
        EAN = p.EAN,
        UPC = p.UPC,
        Name = p.Name,
        Slug = p.Slug,
        Display = p.Display,
        Breadcrumb = p.Breadcrumb,
        AnchorText = p.AnchorText,
        AnchorTitle = p.AnchorTitle,
        BrandId = p.BrandId,
        ProductGroupId = p.ProductGroupId,
        CategoryIds = p.Categories.Select(y => y.CategoryId).ToArray(),
        ShortDescription = p.ShortDescription,
        FullDescription = p.FullDescription,
        AllowReviews = p.AllowReviews,
        AverageRating = p.AverageRating,
        TotalReviews = p.TotalReviews,
        Price = p.Price,
        OldPrice = p.OldPrice,
        CostPrice = p.CostPrice,
        Stock = p.Stock,
        MinStock = p.MinStock,
        ShowAvailability = p.ShowAvailability,
        FreeShipping = p.FreeShipping,
        AdditionalShippingCharge = p.AdditionalShippingCharge,
        UnitOfMeasureId = p.UnitOfMeasureId,
        QuantityPerUnitOfMeasure = p.QuantityPerUnitOfMeasure,
        DeliveryWindowId = p.DeliveryWindowId,
        MinOrderQuantity = p.MinOrderQuantity,
        MaxOrderQuantity = p.MaxOrderQuantity,
        IsFeatured = p.IsFeatured,
        IsNew = p.IsNew,
        IsBestSeller = p.IsBestSeller,
        IsReturnable = p.IsReturnable,
        Status = p.Status,
        RedirectUrl = p.RedirectUrl,
        SortOrder = p.SortOrder,
        Images = p.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
            .Select(ImageExtensions.DTOExpression).ToList()
    };
    
    public static IQueryable<ProductDTO> ProjectToDTO(this IQueryable<Product> query) => 
        query.Select(DTOExpression);
    
    public static IQueryable<ProductListItemDTO> ProjectToListItemDTO(this IQueryable<Product> query) =>
        query.Select(ListItemDTOExpression);
    
    public static ProductDTO MapToDTO(this Product product) => DTOExpression.Compile()(product);
}