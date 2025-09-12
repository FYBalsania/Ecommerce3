using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public class Discount : Entity, ICreatable, IUpdatable, IDeletable
{
    public string Scope { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }
    public decimal? MinOrderValue { get; private set; }
    public DiscountType Type { get; private set; }
    public decimal? Percent { get; private set; }
    public decimal? MaxDiscountAmount { get; private set; }
    public decimal? Amount { get; private set; }
    public bool IsActive { get; private set; }  
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
}