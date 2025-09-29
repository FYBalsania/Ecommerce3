namespace Ecommerce3.Domain.Enums;

[Flags]
public enum ProductReviewInclude
{
    Product = 1 << 0,
    ApproverUser = 1 << 1,
    CreatedByUser = 1 << 2,
    UpdatedByUser = 1 << 3,
    DeletedByUser = 1 << 4,
}