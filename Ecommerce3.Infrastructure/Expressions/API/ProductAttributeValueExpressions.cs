using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.API.ProductAttributeValue;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Expressions.API;

public static class ProductAttributeValueExpressions
{
    public static readonly Expression<Func<ProductAttributeValue, ProductAttributeValueDTO>> DTOExpression = x =>
        new ProductAttributeValueDTO
        {
            Id = x.Id,
            ProductAttributeId = x.ProductAttributeId,
            Value = x.Value,
            Slug = x.Slug,
            Display = x.Display,
            Breadcrumb = x.Breadcrumb,
            SortOrder = x.SortOrder
        };
}