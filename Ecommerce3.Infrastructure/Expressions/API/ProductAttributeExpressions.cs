using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.API.ProductAttribute;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Expressions.API;

public static class ProductAttributeExpressions
{
    public static readonly Expression<Func<ProductAttribute, ProductAttributeDTO>> DTOExpression = x => new ProductAttributeDTO
    {
        Id = x.Id,
        Name = x.Name,
        Display = x.Display,
        Breadcrumb = x.Breadcrumb,
        DataType = x.DataType,
        SortOrder = x.SortOrder
    };
}