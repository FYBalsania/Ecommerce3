namespace Ecommerce3.Domain.Enums;

public enum SalesOrderLineInclude
{
    SalesOrder,
    CartLine,
    Product,
    CreatedByUser,
    CreatedByCustomer,
    UpdatedByUser,
    UpdatedByCustomer,
    DeletedByUser
}