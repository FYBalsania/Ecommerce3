using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ProductCategoryErrors
    {
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(KVPListItem)}.{nameof(KVPListItem.CreatedBy)}", "Created by is invalid.");
        
        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(ProductCategory)}.{nameof(ProductCategory.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(ProductCategory)}.{nameof(ProductCategory.CreatedByIp)}", "Created by IP address cannot exceed 128 characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(ProductCategory)}.{nameof(ProductCategory.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(ProductCategory)}.{nameof(ProductCategory.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(ProductCategory)}.{nameof(ProductCategory.UpdatedByIp)}", "Updated by IP address cannot exceed 128 characters.");
        
        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(ProductCategory)}.{nameof(ProductCategory.DeletedBy)}", "Deleted by is invalid.");

        public static readonly DomainError DeletedByIpRequired =
            new($"{nameof(ProductCategory)}.{nameof(ProductCategory.DeletedByIp)}", "Deleted by IP address is required.");

        public static readonly DomainError DeletedByIpTooLong =
            new($"{nameof(ProductCategory)}.{nameof(ProductCategory.DeletedByIp)}",
                $"Updated by IP address cannot exceed {IDeletable.DeletedByIpMaxLength} characters.");
    }
}