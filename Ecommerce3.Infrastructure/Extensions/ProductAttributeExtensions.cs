using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions;

internal static class ProductAttributeExtensions
{
    internal static ProductAttributeValueDTO ToDTO(this ProductAttributeValue productAttributeValue)
    {
        return productAttributeValue switch
        {
            ProductAttributeBooleanValue bv => new ProductAttributeBooleanValueDTO(
                bv.Id, bv.Value, bv.Slug, bv.Display, bv.Breadcrumb, bv.SortOrder,
                bv.CreatedByUser!.FullName, bv.CreatedAt, bv.BooleanValue),

            ProductAttributeColourValue cv => new ProductAttributeColourValueDTO(
                cv.Id, cv.Value, cv.Slug, cv.Display, cv.Breadcrumb, cv.SortOrder,
                cv.CreatedByUser!.FullName, cv.CreatedAt, cv.HexCode,
                cv.ColourFamily, cv.ColourFamilyHexCode),

            ProductAttributeDecimalValue dv => new ProductAttributeDecimalValueDTO(
                dv.Id, dv.Value, dv.Slug, dv.Display, dv.Breadcrumb, dv.SortOrder,
                dv.CreatedByUser!.FullName, dv.CreatedAt, dv.DecimalValue),

            ProductAttributeDateOnlyValue dv => new ProductAttributeDateOnlyValueDTO(
                dv.Id, dv.Value, dv.Slug, dv.Display, dv.Breadcrumb, dv.SortOrder,
                dv.CreatedByUser!.FullName, dv.CreatedAt, dv.DateOnlyValue),

            not null => new ProductAttributeValueDTO(productAttributeValue.Id, productAttributeValue.Value,
                productAttributeValue.Slug, productAttributeValue.Display, productAttributeValue.Breadcrumb,
                productAttributeValue.SortOrder, productAttributeValue.CreatedByUser!.FullName,
                productAttributeValue.CreatedAt),

            _ => throw new NotSupportedException(
                $"Product attribute value type '{productAttributeValue!.GetType().Name}' is not supported.")
        };
    }
}