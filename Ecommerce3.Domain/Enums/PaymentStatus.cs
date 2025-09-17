namespace Ecommerce3.Domain.Enums;

public enum PaymentStatus
{
    Authorized,
    Pending,
    Paid,
    PartiallyRefunded,
    Refunded,
    Voided,
}