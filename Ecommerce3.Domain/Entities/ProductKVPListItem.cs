using System.Net;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductKVPListItem : KVPListItem
{
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }

    private ProductKVPListItem() : base()
    {
    }

    public ProductKVPListItem(KVPListItemType type, string key, string value, decimal sortOrder, int productId,
        int createdBy, DateTime createdAt, IPAddress createdByIp)
        : base(type, key, value, sortOrder, createdBy, createdAt, createdByIp)
    {
        ValidateProductId(productId);
        
        ProductId = productId;
    }

    private static void ValidateProductId(int productId)
    {
        if (productId <= 0) throw new DomainException(DomainErrors.ProductKVPListItemErrors.InvalidProductId);
    }
}