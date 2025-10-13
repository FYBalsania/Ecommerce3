namespace Ecommerce3.Domain.Enums;

[Flags]
public enum BrandImageInclude
{
    None = 0,
    ImageType = 1 << 0,
    Brand = 1 << 1,
    CreatedByUser = 1 << 2,
    UpdatedByUser = 1 << 3,
    DeletedByUser = 1 << 4
}