namespace Ecommerce3.Domain.Enums;

[Flags]
public enum ProductInclude
{
    None = 0,
    Images = 1 << 0,
    Brand = 1 << 1,
    ProductGroup = 1 << 2,
    DeliveryWindow = 1 << 3,
    CreatedByUser = 1 << 4,
    UpdatedByUser = 1 << 5,
    DeletedByUser = 1 << 6,
    Categories = 1 << 7,
    TextListItems = 1 << 8,
    KVPListItems = 1 << 9,
    QnAs = 1 << 10,
    Reviews = 1 << 11,
    Attributes = 1 << 12,
    UnitOfMeasure = 1 << 13,
}