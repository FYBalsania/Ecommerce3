using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.Admin.ProductGroup;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Expressions.Admin.ProductGroup;

public static class ProductGroupProductAttributeExpressions
{
    public static readonly Expression<Func<ProductGroupProductAttribute, ProductGroupProductAttributeDTO>>
        DTOExpression = x => new ProductGroupProductAttributeDTO
        {
            Id = x.Id,
            ProductGroupId = x.ProductGroupId,
            ProductAttributeId = x.ProductAttributeId,
            ProductAttributeName = x.ProductAttribute!.Name,
            ProductAttributeSortOrder = x.ProductAttributeSortOrder,
            ProductAttributeValueId = x.ProductAttributeValueId,
            ProductAttributeValueValue = x.ProductAttributeValue!.Value,
            ProductAttributeValueDisplay = x.ProductAttributeValue.Display,
            ProductAttributeValueSortOrder = x.ProductAttributeValueSortOrder
        };
}