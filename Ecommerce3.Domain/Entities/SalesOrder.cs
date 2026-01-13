using System.Net;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Entities;

public sealed class SalesOrder : Entity, IDeletable
{
    private readonly List<SalesOrderLine> _lines = [];
    public string Number { get; private set; }
    public DateTime Dated { get; private set; }
    public int? CartId { get; private set; }
    public Cart? Cart { get; private set; }
    public int CustomerId { get; private set; }
    public Customer? Customer { get; private set; }
    public CustomerReference CustomerReference { get; private set; }
    public int BillingCustomerAddressId { get; private set; }
    public CustomerAddress? BillingAddress { get; private set; }
    public CustomerAddressReference BillingAddressReference { get; private set; }
    public int ShippingCustomerAddressId { get; private set; }
    public CustomerAddress? ShippingAddress { get; private set; }
    public CustomerAddressReference ShippingAddressReference { get; private set; }
    public decimal SubTotal { get; private set; }
    public decimal Discount { get; private set; }
    public decimal ShippingCharge { get; private set; }
    public decimal Total { get; private set; }
    public OrderStatus Status { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public ShippingStatus ShippingStatus { get; private set; }
    public int? CreatedByUserId { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public int? CreatedByCustomerId { get; private set; }
    public Customer? CreatedByCustomer { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IPAddress CreatedByIp { get; private set; }
    public int? UpdatedByUserId { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public int? UpdatedByCustomerId { get; private set; }
    public Customer? UpdatedByCustomer { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IPAddress? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public IPAddress? DeletedByIp { get; private set; }
    public IReadOnlyList<SalesOrderLine> Lines => _lines;
    
    private SalesOrder(){}
    
    public void Delete(int deletedBy, DateTime deletedAt, IPAddress deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
}