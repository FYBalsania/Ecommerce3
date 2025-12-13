using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.Admin.Product;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class ProductExtensions
{
    private static readonly Expression<Func<Product, ProductDTO>> DTOExpression = x => new ProductDTO
    {
        Id = x.Id,
        SKU = x.SKU,
        GTIN = x.GTIN,
        MPN = x.MPN,
        MFC = x.MFC,
        EAN = x.EAN,
        UPC = x.UPC,
        Name = x.Name,
        Slug = x.Slug,
        Display = x.Display,
        Breadcrumb = x.Breadcrumb,
        AnchorText = x.AnchorText,
        AnchorTitle = x.AnchorTitle,
        BrandId = x.BrandId,
        ProductGroupId = x.ProductGroupId,
        CategoryIds = x.Categories.Select(y => y.CategoryId).ToArray(),
        ShortDescription = x.ShortDescription,
        FullDescription = x.FullDescription,
        AllowReviews = x.AllowReviews,
        AverageRating = x.AverageRating,
        TotalReviews = x.TotalReviews,
        Price = x.Price,
        OldPrice = x.OldPrice,
        CostPrice = x.CostPrice,
        Stock = x.Stock,
        MinStock = x.MinStock,
        ShowAvailability = x.ShowAvailability,
        FreeShipping = x.FreeShipping,
        AdditionalShippingCharge = x.AdditionalShippingCharge,
        UnitOfMeasureId = x.UnitOfMeasureId,
        QuantityPerUnitOfMeasure = x.QuantityPerUnitOfMeasure,
        DeliveryWindowId = x.DeliveryWindowId,
        MinOrderQuantity = x.MinOrderQuantity,
        MaxOrderQuantity = x.MaxOrderQuantity,
        IsFeatured = x.IsFeatured,
        IsNew = x.IsNew,
        IsBestSeller = x.IsBestSeller,
        IsReturnable = x.IsReturnable,
        Status = x.Status,
        RedirectUrl = x.RedirectUrl,
        SortOrder = x.SortOrder,
    };
    
    public static IQueryable<ProductDTO> ProjectToDTO(this IQueryable<Product> query) => query.Select(DTOExpression);
    
    public static ProductDTO MapToDTO(this Product product) => DTOExpression.Compile()(product);
}