using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Entities;

public sealed class SalesOrder : Entity, ICreatable, IUpdatable
{
    private readonly List<SalesOrderLine> _lines = [];
    public string Number { get; private set; }
    public DateTime Dated { get; private set; }
    public int? CartId { get; private set; }
    public int CustomerId { get; private set; }
    public CustomerReference CustomerReference { get; private set; }
    public int BillingCustomerAddressId { get; private set; }
    public CustomerAddressReference BillingAddressReference { get; private set; }
    public int ShippingCustomerAddressId { get; private set; }
    public CustomerAddressReference ShippingAddressReference { get; private set; }
    public decimal SubTotal { get; private set; }
    public decimal Discount { get; private set; }
    public decimal ShippingCharge { get; private set; }
    public decimal Total { get; private set; }
    public OrderStatus Status { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public ShippingStatus ShippingStatus { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public IReadOnlyList<SalesOrderLine> Lines => _lines;
}