namespace Ecommerce3.Domain.Entities;

public sealed class SalesOrder : Entity
{
    public int CustomerId { get; private set; }
}