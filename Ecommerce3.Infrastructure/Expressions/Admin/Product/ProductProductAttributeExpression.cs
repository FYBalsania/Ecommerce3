using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.Admin.Product;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Expressions.Admin.Product;

public static class ProductProductAttributeExpression
{
    public static readonly Expression<Func<ProductProductAttribute, ProductProductAttributeDTO>> DTOExpression = x =>
        new ProductProductAttributeDTO
        {
            Id = x.Id,
            ProductId = x.ProductId,
            ProductAttributeId = x.ProductAttributeId,
            ProductAttributeName = x.ProductAttribute!.Name,
            ProductAttributeSortOrder = x.ProductAttributeSortOrder,
            ProductAttributeValueId = x.ProductAttributeValueId,
            ProductAttributeValueValue = x.ProductAttributeValue!.Value,
            ProductAttributeValueDisplay = x.ProductAttributeValue.Display,
            ProductAttributeValueSortOrder = x.ProductAttributeValueSortOrder
        };
}