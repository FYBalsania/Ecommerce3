using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class ProductAttributeExtensions
{
    private static readonly Expression<Func<ProductAttribute, ProductAttributeListItemDTO>> ListItemDTOExpression = pa => new ProductAttributeListItemDTO
    {
        Id = pa.Id,
        Name = pa.Name,
        Slug = pa.Slug,
        Display = pa.Display,
        Breadcrumb = pa.Breadcrumb,
        ValuesCount = pa.Values.Count,
        DataType = pa.DataType.ToString(),
        SortOrder = pa.SortOrder,
        CreatedUserFullName = pa.CreatedByUser!.FullName,
        CreatedAt = pa.CreatedAt
    };
    
    private static readonly Expression<Func<ProductAttribute, ProductAttributeDTO>> DTOExpression = pa => new ProductAttributeDTO
    {
        Id = pa.Id,
        Name = pa.Name,
        Slug = pa.Slug,
        Display = pa.Display,
        Breadcrumb = pa.Breadcrumb,
        SortOrder = pa.SortOrder,
        DataType = pa.DataType,
        Values = pa.Values.Select(x => x.ToDTO())
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Value)
            .ToList()
    };
    
    public static IQueryable<ProductAttributeDTO> ProjectToDTO(this IQueryable<ProductAttribute> query) =>
        query.Select(DTOExpression);
    
    public static IQueryable<ProductAttributeListItemDTO> ProjectToListItemDTO(this IQueryable<ProductAttribute> query) =>
        query.Select(ListItemDTOExpression);
}