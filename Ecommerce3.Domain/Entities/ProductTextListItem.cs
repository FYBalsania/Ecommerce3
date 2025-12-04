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

    public ProductTextListItem(int productId, TextListItemType type, string text, int sortOrder, int createdBy,
        DateTime createdAt, string createdByIp)
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