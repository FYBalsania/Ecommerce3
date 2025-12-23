using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.Admin.ProductGroupProductAttribute;
using Ecommerce3.Contracts.DTO.API.ProductGroup;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class ProductGroupProductAttributesExtensions
{
    public static readonly Expression<Func<ProductGroupProductAttribute, ProductGroupProductAttributeListItemDTO>> DTOExpression = x => new ProductGroupProductAttributeListItemDTO
        {
            ProductGroupId = x.ProductGroupId,
            ProductAttributeId = x.ProductAttributeId,
            ProductAttributeName = x.ProductAttribute!.Name,
            ProductAttributeSortOrder = x.ProductAttributeSortOrder,
            ProductAttributeValues = x.ProductAttributeValue!.Value,
            ProductAttributeValuesDisplay = x.ProductAttributeValue!.Display,
            ProductAttributeValueSortOrder = x.ProductAttributeValueSortOrder
        };
    
    public static IQueryable<ProductGroupProductAttributeListItemDTO> ProjectToDTO(this IQueryable<ProductGroupProductAttribute> query) =>
        query.Select(DTOExpression);
}