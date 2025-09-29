namespace Ecommerce3.Domain.Enums;

[Flags]
public enum ProductDiscountInclude
{
    Products = 1 << 0,
    CreatedByUser = 1 << 1,
    UpdatedByUser = 1 << 2,
    DeletedByUser = 1 << 3,
}