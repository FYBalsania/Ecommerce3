namespace Ecommerce3.Domain.Enums;

[Flags]
public enum ImageTypeInclude
{
    None = 0,
    CreatedByUser = 1 << 0,
    UpdatedByUser = 1 << 1,
    DeletedByUser = 1 << 2,
}