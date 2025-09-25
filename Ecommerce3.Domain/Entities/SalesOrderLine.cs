using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Entities;

public sealed class SalesOrderLine : Entity, IDeletable
{
    public int SalesOrderId { get; private set; }
    public SalesOrder? SalesOrder { get; private set; }
    public int? CartLineId { get; private set; }
    public CartLine? CartLine { get; private set; }
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }
    public ProductReference ProductReference { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public decimal Total { get; private set; }
    public int CreatedByUserId { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public int CreatedByCustomerId { get; private set; }
    public Customer? CreatedByCustomer { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedByUserId { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public int? UpdatedByCustomerId { get; private set; }
    public Customer? UpdatedByCustomer { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
    
    private SalesOrderLine(){}
}