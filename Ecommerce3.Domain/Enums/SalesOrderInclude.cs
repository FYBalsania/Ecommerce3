namespace Ecommerce3.Domain.Enums;

[Flags]
public enum SalesOrderInclude
{
    Lines = 1 << 0,
    Cart = 1 << 1,
    Customer = 1 << 2,
    BillingAddress = 1 << 3,
    ShippingAddress = 1 << 4,
    CreatedByUser = 1 << 5,
    CreatedByCustomer = 1 << 6,
    UpdatedByUser = 1 << 7,
    UpdatedByCustomer = 1 << 8,
    DeletedByUser = 1 << 9,
}