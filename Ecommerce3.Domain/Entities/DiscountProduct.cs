using System.Net;

namespace Ecommerce3.Domain.Entities;

public sealed class DiscountProduct: Entity //, ICreatable, IDeletable
{
    public int DiscountId { get; set; }
    public Discount? Discount { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }   
    public int CreatedBy { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }   
    public DateTime CreatedAt { get; private set; }
    public IPAddress CreatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public IPAddress? DeletedByIp { get; private set; }

    private DiscountProduct()
    {
        
    }
    
    public void Delete(int deletedBy, DateTime deletedAt, IPAddress deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
}