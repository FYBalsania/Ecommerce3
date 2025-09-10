using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public abstract class TextListItem : Entity, ICreatable, IUpdatable, IDeletable
{
    public string Type { get; private set; }
    public TextListItemType TextListItemType { get; private set; }
    public string Text { get; private set; }
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