using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ProductCategoryErrors
    {
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(KVPListItem)}.{nameof(KVPListItem.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(ProductCategory)}.{nameof(ProductCategory.UpdatedBy)}", "Updated by is invalid.");
        
        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(ProductCategory)}.{nameof(ProductCategory.DeletedBy)}", "Deleted by is invalid.");
    }
}