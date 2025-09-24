namespace Ecommerce3.Domain.Entities;

public sealed class ProductKVPListItem : KVPListItem
{
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }   
}