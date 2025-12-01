using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ProductAttributeValueErrors
    {
        public static readonly DomainError InvalidId =
            new DomainError($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Id)}",
                "Product attribute value Id is invalid.");

        public static readonly DomainError ValueRequired =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Value)}", "Value is required.");

        public static readonly DomainError ValueTooLong =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Value)}",
                "Value cannot exceed 256 characters.");

        public static readonly DomainError DuplicateValue =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Value)}", "Duplicate Value.");

        public static readonly DomainError SlugRequired =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Slug)}", "Slug is required.");

        public static readonly DomainError DuplicateSlug =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Slug)}", "Duplicate slug.");

        public static readonly DomainError SlugTooLong =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Slug)}",
                "Slug cannot exceed 256 characters.");

        public static readonly DomainError DisplayRequired =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Display)}", "Display is required.");

        public static readonly DomainError DisplayTooLong =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Display)}",
                "Display cannot exceed 256 characters.");

        public static readonly DomainError BreadcrumbRequired =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Breadcrumb)}",
                "Breadcrumb is required.");

        public static readonly DomainError BreadcrumbTooLong =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Breadcrumb)}",
                "Breadcrumb cannot exceed 256 characters.");

        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.CreatedByIp)}",
                "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.CreatedByIp)}",
                "Created by IP address cannot exceed 128 characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.UpdatedByIp)}",
                "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.UpdatedByIp)}",
                "Updated by IP address cannot exceed 128 characters.");

        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.DeletedBy)}", "Deleted by is invalid.");

        public static readonly DomainError DeletedByIpRequired =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.DeletedByIp)}",
                "Deleted by IP address is required.");

        public static readonly DomainError DeletedByIpTooLong =
            new($"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.DeletedByIp)}",
                "Deleted by IP address cannot exceed 128 characters.");
    }
}