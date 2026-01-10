using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.Admin.Product;
using Ecommerce3.Contracts.DTOs.Product;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Expressions.Admin.Product;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class ProductExtensions
{
    private static readonly Expression<Func<Product, ProductListItemDTO>> ListItemDTOExpression = p =>
        new ProductListItemDTO
        {
            Id = p.Id,
            Name = p.Name,
            Slug = p.Slug,
            SortOrder = p.SortOrder,
            ImageCount = p.Images.Count,
            SKU = p.SKU,
            Status = p.Status,
            CategoryNames = p.Categories.Select(c => c.Category!.Name).ToArray(),
            CreatedUserFullName = p.CreatedByUser!.FullName,
            CreatedAt = p.CreatedAt
        };

    public static readonly Expression<Func<Product, ProductDTO>> DTOExpression = p => new ProductDTO
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
        H1 = p.Page!.H1,
        MetaTitle = p.Page.MetaTitle,
        MetaDescription = p.Page.MetaDescription,
        MetaKeywords = p.Page.MetaKeywords,
        CountryOfOriginId = p.CountryOfOriginId,
        Attributes = p.Attributes
            .AsQueryable()
            .OrderBy(x => x.ProductAttributeSortOrder)
            .Select(ProductProductAttributeExpression.DTOExpression)
            .ToList(),
        Images = p.Images
            .AsQueryable()
            .OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
            .Select(ImageExtensions.DTOExpression)
            .ToList(),
        TextListItems = p.TextListItems
            .AsQueryable()
            .OrderBy(tl => tl.Text).ThenBy(tli => tli.SortOrder)
            .Select(TextListItemExtensions.ToDtoExpression)
            .ToList(),
        KVPListItems = p.KVPListItems
            .AsQueryable()
            .Where(tl => tl.ProductId == p.Id)
            .OrderBy(tl => tl.Key).ThenBy(tli => tli.SortOrder)
            .Select(KVPListItemExtensions.ToDtoExpression)
            .ToList()
    };
    
    private static readonly Expression<Func<Product, InventoryListItemDTO>> InventoryListItemDTOExpression = i =>
        new InventoryListItemDTO
        {
            Id = i.Id,
            Name = i.Name,
            SKU = i.SKU,
            Price = i.Price,
            OldPrice = i.OldPrice,
            Stock = i.Stock,
            UpdatedUserFullName = i.UpdatedByUser!.FullName,
            UpdatedAt = i.UpdatedAt
        };

    public static IQueryable<ProductListItemDTO> ProjectToListItemDTO(this IQueryable<Product> query) =>
        query.Select(ListItemDTOExpression);

    public static ProductDTO MapToDTO(this Product product) => DTOExpression.Compile()(product);
    
    public static IQueryable<InventoryListItemDTO> ProjectToInventoryListItemDTO(this IQueryable<Product> query) =>
        query.Select(InventoryListItemDTOExpression);
}