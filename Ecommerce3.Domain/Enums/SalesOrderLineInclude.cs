namespace Ecommerce3.Domain.Enums;

[Flags]
public enum SalesOrderLineInclude
{
    SalesOrder = 1 << 0,
    CartLine = 1 << 1,
    Product = 1 << 2,
    CreatedByUser = 1 << 3,
    CreatedByCustomer = 1 << 4,
    UpdatedByUser = 1 << 5,
    UpdatedByCustomer = 1 << 6,
    DeletedByUser = 1 << 7,
}