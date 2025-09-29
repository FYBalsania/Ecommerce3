namespace Ecommerce3.Domain.Enums;

[Flags]
public enum BrandPageInclude
{
    None = 0,
    Brand = 1 << 0,
    CreatedByUser = 1 << 1,
    UpdatedByUser = 1 << 2,
    DeletedByUser = 1 << 3,
}