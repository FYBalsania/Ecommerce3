namespace Ecommerce3.Domain.Enums;

[Flags]
public enum DiscountProductInclude
{
    None = 0,
    Discount = 1 << 0,
    Product = 1 << 1,
    CreatedByUser = 1 << 2,
    DeletedByUser = 1 << 3,
}