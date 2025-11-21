namespace Ecommerce3.Domain.Entities;

public sealed class Cart : Entity, ICreatable, IUpdatable, IDeletable
{
    private readonly List<CartLine> _lines = [];
    public int? CustomerId { get; private set; }
    public Customer? Customer { get; private set; } 
    public string? SessionId { get; private set; }
    public decimal SubTotal { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total { get; private set; }
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
    public IReadOnlyCollection<CartLine> Lines => _lines;

    private Cart()
    {
    }

    public Cart(int? customerId, string? sessionId, int createdBy, string createdByIp)
    {
        CustomerId = customerId;
        SessionId = sessionId;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
    
    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    private CartLine? FindCartLineByProductId(int productId) => _lines.FirstOrDefault(l => l.ProductId == productId);
}