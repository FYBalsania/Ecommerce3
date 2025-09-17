using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Entities;

public sealed class SalesOrderLine : Entity, ICreatable, IUpdatable
{
    public int SalesOrderId { get; private set; }
    public int? CartLineId { get; private set; }
    public int ProductId { get; private set; }
    public ProductReference ProductReference  { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public decimal Total { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
}