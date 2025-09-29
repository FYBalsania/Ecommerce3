namespace Ecommerce3.Domain.Enums;

public enum SalesOrderInclude
{
    Lines,
    Cart,
    Customer,
    BillingAddress,
    ShippingAddress,
    CreatedByUser,
    CreatedByCustomer,
    UpdatedByUser,
    UpdatedByCustomer,
    DeletedByUser
}