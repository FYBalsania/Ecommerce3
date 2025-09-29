namespace Ecommerce3.Domain.Enums;

[Flags]
public enum ImageInclude
{
    None = 0,
    ImageType = 1 << 0,
    Brand = 1 << 1,
    Product = 1 << 2,
    Category = 1 << 3,
    Page = 1 << 4,
    ProductGroup = 1 << 5,
    CreatedByUser = 1 << 6,
    UpdatedByUser = 1 << 7,
    DeletedByUser = 1 << 8,
}