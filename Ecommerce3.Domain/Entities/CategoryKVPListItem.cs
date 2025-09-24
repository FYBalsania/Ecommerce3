namespace Ecommerce3.Domain.Entities;

public sealed class CategoryKVPListItem : KVPListItem
{
    public int CategoryId { get; private set; }
    public Category? Category { get; private set; }
}