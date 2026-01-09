using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.StoreFront.Image;
using Ecommerce3.Contracts.DTO.StoreFront.Product;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Expressions.StoreFront;

public static class ProductExpressions
{
    public static readonly Expression<Func<Product, ProductListItemDTO>> DTOExpression = x => new ProductListItemDTO
    {
        Id = x.Id,
        SKU = x.SKU,
        Name = x.Name,
        Slug = x.Slug,
        Display = x.Display,
        AnchorText = x.AnchorText,
        AnchorTitle = x.AnchorTitle,
        BrandName = x.Brand!.Name,
        Price = x.Price,
        OldPrice = x.OldPrice,
        Stock = x.Stock,
        AverageRating = x.AverageRating,
        Image = x.Images
            .Where(y => y.ImageTypeId == 2)
            .OrderBy(y => y.SortOrder)
            .Select(y => new ImageDTO
            {
                Id = y.Id,
                FileName = y.FileName,
                FileExtension = y.FileExtension,
                ImageTypeId = y.ImageTypeId,
                Size = y.Size,
                SortOrder = y.SortOrder,
                AltText = y.AltText,
                Title = y.Title,
                Loading = y.Loading,
                Link = y.Link,
                LinkTarget = y.LinkTarget
            })
            .FirstOrDefault()
    };
}