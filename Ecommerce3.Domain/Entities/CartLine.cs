namespace Ecommerce3.Domain.Entities;

public sealed class CartLine : Entity, ICreatable, IUpdatable, IDeletable
{
    public int CartId { get; private set; }
    public Cart? Cart { get; private set; }   
    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal SubTotal {get; private set;}
    public decimal Discount { get; private set; }
    public decimal Total {get; private set;}
    public int CreatedBy { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
    
    private CartLine()
    {
    }
}