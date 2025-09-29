namespace Ecommerce3.Domain.Enums;

[Flags]
public enum ProductProductAttributeInclude
{
    Product = 1 << 0,
    ProductAttribute = 1 << 1,
    ProductAttributeValue = 1 << 2,
    CreatedByUser = 1 << 3,
    UpdatedByUser = 1 << 4,
    DeletedByUser = 1 << 5,
}