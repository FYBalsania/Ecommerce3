namespace Ecommerce3.Domain.Enums;

[Flags]
public enum BrandInclude
{
    Images = 1 << 0,
    CreatedUser = 1 << 1,
    UpdatedUser = 1 << 2,
    DeletedUser = 1 << 3,
}