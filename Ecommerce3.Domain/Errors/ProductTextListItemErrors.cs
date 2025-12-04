using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ProductTextListItemErrors
    {
        public static readonly DomainError InvalidProductId =
            new DomainError($"{nameof(ProductTextListItem)}.{nameof(ProductTextListItem.ProductId)}.",
                "Product Id is invalid.");
    }
}