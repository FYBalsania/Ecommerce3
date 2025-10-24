namespace Ecommerce3.Domain.Enums;

[Flags]
public enum ProductGroupInclude
{
    None = 0,
    Attributes = 1 << 0,
    Images = 1 << 1,
    CreatedByUser = 1 << 2,
    UpdatedByUser = 1 << 3,
    DeletedByUser = 1 << 4,
}