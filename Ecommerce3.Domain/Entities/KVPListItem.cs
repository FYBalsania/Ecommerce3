using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public abstract class KVPListItem : Entity
{
    public KVPListItemType Type { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public int SortOrder { get; private set; }
}