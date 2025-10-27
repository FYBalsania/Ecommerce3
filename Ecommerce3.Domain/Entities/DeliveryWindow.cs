using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class DeliveryWindow : Entity, ICreatable, IUpdatable, IDeletable
{
    public string Name { get; private set; }
    public DeliveryUnit Unit { get; private set; }
    public int MinValue { get; private set; }
    public int? MaxValue { get; private set; }
    public decimal NormalizedMinDays { get; private set; }
    public decimal? NormalizedMaxDays { get; private set; }
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; }
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

    private DeliveryWindow()
    {
    }

    public DeliveryWindow(string name, DeliveryUnit deliveryUnit, uint minValue, uint? maxValue, int sortOrder,
        bool isActive, int createdBy, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 256, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));
        if (maxValue <= minValue) throw new ArgumentException("MaxValue must be greater than MinValue.", nameof(maxValue));
        
        Name = name;
        Unit = deliveryUnit;
        MinValue = (int)minValue;
        MaxValue = maxValue.HasValue ? (int)maxValue : null;
        NormalizedMinDays = deliveryUnit switch
        {
            DeliveryUnit.Hour => MinValue / 24m,
            DeliveryUnit.Day => MinValue,
            DeliveryUnit.Week => MinValue * 7m,
            _ => throw new ArgumentOutOfRangeException(nameof(deliveryUnit), deliveryUnit, null)
        };
        NormalizedMaxDays = MaxValue.HasValue
            ? deliveryUnit switch
            {
                DeliveryUnit.Hour => MaxValue.Value / 24m,
                DeliveryUnit.Day => MaxValue.Value,
                DeliveryUnit.Week => MaxValue.Value * 7m,
                _ => throw new ArgumentOutOfRangeException(nameof(deliveryUnit), deliveryUnit, null)
            }
            : null;
        SortOrder = sortOrder;
        IsActive = isActive;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
    
    public bool Update(string name, DeliveryUnit deliveryUnit, uint minValue, uint? maxValue, bool isActive, int sortOrder,
        int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        if (Name == name && Unit == deliveryUnit && MinValue == minValue && MaxValue == maxValue && IsActive == isActive && SortOrder == sortOrder)
            return false;
        
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 256, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(updatedByIp, nameof(updatedByIp));
        if (maxValue <= minValue) throw new ArgumentException("MaxValue must be greater than MinValue.", nameof(maxValue));
        
        Name = name;
        Unit = deliveryUnit;
        MinValue = (int)minValue;
        MaxValue = maxValue.HasValue ? (int)maxValue : null;
        NormalizedMinDays = deliveryUnit switch
        {
            DeliveryUnit.Hour => MinValue / 24m,
            DeliveryUnit.Day => MinValue,
            DeliveryUnit.Week => MinValue * 7m,
            _ => throw new ArgumentOutOfRangeException(nameof(deliveryUnit), deliveryUnit, null)
        };
        NormalizedMaxDays = MaxValue.HasValue
            ? deliveryUnit switch
            {
                DeliveryUnit.Hour => MaxValue.Value / 24m,
                DeliveryUnit.Day => MaxValue.Value,
                DeliveryUnit.Week => MaxValue.Value * 7m,
                _ => throw new ArgumentOutOfRangeException(nameof(deliveryUnit), deliveryUnit, null)
            }
            : null;
        SortOrder = sortOrder;
        IsActive = isActive;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;
        
        return true;
    }
}