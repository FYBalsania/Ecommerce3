namespace Ecommerce3.Domain.Entities;

public sealed class DiscountProduct: ICreatable, IDeletable
{
    public int DiscountId { get; set; }
    public int ProductId { get; set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
}