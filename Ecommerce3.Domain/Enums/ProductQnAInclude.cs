namespace Ecommerce3.Domain.Enums;

[Flags]
public enum ProductQnAInclude
{
    Product = 1 << 0,
    AnsweredByUser = 1 << 1,
    ApproverUser = 1 << 2,
    CreatedByUser = 1 << 3,
    UpdatedByUser = 1 << 4,
    DeletedByUser = 1 << 5,
}