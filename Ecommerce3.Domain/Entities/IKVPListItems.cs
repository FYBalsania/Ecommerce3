using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public interface IKVPListItems<out T> where T : KVPListItem
{
    IReadOnlyList<T> KVPListItems { get; }

    IReadOnlyList<T> GetKVPListItems(KVPListItemType type)
        => KVPListItems.Where(x => x.Type == type)
            .OrderBy(x => x.SortOrder).ThenBy(x => x.Key)
            .ToList();

    bool KVPListItemsContainsKey(KVPListItemType type, string key)
        => KVPListItems.Any(x => x.Type == type && x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

    bool KVPListItemsContainsValue(KVPListItemType type, string value)
        => KVPListItems.Any(x => x.Type == type && x.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
}