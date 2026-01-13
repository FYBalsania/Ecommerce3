using System.Net;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductTextListItem : TextListItem
{
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }

    private ProductTextListItem()
    {
    }

    internal ProductTextListItem(int productId, TextListItemType type, string text, decimal sortOrder, int createdBy,
        DateTime createdAt, IPAddress createdByIp)
        : base(type, text, sortOrder, createdBy, createdAt, createdByIp)
    {
        ValidateProductId(productId);
        
        ProductId = productId;
    }

    private static void ValidateProductId(int productId)
    {
        if (productId < 0)
            throw new DomainException(DomainErrors.ProductTextListItemErrors.InvalidProductId);
    }
}