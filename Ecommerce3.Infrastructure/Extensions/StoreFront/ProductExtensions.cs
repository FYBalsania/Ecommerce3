using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.StoreFront.Product;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.StoreFront;

public static class ProductExtensions
{
    private static readonly Expression<Func<Product, ProductListItemDTO>> DTOExpression = x => new ProductListItemDTO
    {
        Id = x.Id,
        SKU = x.SKU,
        Name = x.Name,
        Slug = x.Slug,
        Images = x.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
            .Select(ImageExtensions.DTOExpression).ToList()
    };

    public static IQueryable<ProductListItemDTO> ProjectToDTO(this IQueryable<Product> query) =>
        query.Select(DTOExpression);

    public static IReadOnlyList<ProductListItemDTO> MapToDTO(this IReadOnlyList<Product> items) =>
        items.Select(DTOExpression.Compile()).ToList();
}