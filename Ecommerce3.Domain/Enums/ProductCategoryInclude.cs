namespace Ecommerce3.Domain.Enums;

[Flags]
public enum ProductCategoryInclude
{
    Product = 1 << 0,
    Category = 1 << 1,
    CreatedByUser = 1 << 2,
    UpdatedByUser = 1 << 3,
    DeletedByUser = 1 << 4,
}