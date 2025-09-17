using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public abstract class KVPListItem : Entity, ICreatable, IUpdatable, IDeletable
{
    public KVPListItemType Type { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public int SortOrder { get; private set; }
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