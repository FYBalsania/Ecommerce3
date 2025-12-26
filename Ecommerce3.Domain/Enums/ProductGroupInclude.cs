namespace Ecommerce3.Domain.Enums;

[Flags]
public enum ProductGroupInclude
{
    None = 0,
    Attributes = 1 << 0,
    AttributeValues = 1 << 1,
    Images = 1 << 2,
    CreatedByUser = 1 << 3,
    UpdatedByUser = 1 << 4,
    DeletedByUser = 1 << 5,
}