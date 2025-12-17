using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class ProductAttributeExtensions
{
    public static readonly Expression<Func<ProductAttribute, ProductAttributeListItemDTO>> ListItemDTOExpression = pa => new ProductAttributeListItemDTO
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
    
    public static readonly Expression<Func<ProductAttribute, ProductAttributeDTO>> DTOExpression = pa => new ProductAttributeDTO
    {
        Id = pa.Id,
        Name = pa.Name,
        Slug = pa.Slug,
        Display = pa.Display,
        Breadcrumb = pa.Breadcrumb,
        SortOrder = pa.SortOrder,
        DataType = pa.DataType,
        Values = pa.Values.AsQueryable()
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Value)
            .Select(y => y.ToDTO())
            .ToList()
    };
}