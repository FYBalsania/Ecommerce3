namespace Ecommerce3.Domain.Enums;

[Flags]
public enum CartLineInclude
{
    None = 0,
    Cart = 1 << 0,
    Product = 1 << 1,
    CreatedByUser = 1 << 2,
    UpdatedByUser = 1 << 3,
    DeletedByUser = 1 << 4,
}