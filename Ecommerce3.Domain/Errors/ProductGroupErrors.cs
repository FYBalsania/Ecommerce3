using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ProductGroupErrors
    {
        public static readonly DomainError NameRequired =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.Name)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.Name)}", "Name cannot exceed 256 characters.");

        public static readonly DomainError DuplicateName =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.Name)}", "Duplicate name.");

        public static readonly DomainError SlugRequired =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.Slug)}", "Slug is required.");
        
        public static readonly DomainError DuplicateSlug =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.Slug)}", "Duplicate slug.");

        public static readonly DomainError SlugTooLong =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.Slug)}", "Slug cannot exceed 256 characters.");

        public static readonly DomainError DisplayRequired =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.Display)}", "Display is required.");

        public static readonly DomainError DisplayTooLong =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.Display)}", "Display cannot exceed 256 characters.");

        public static readonly DomainError BreadcrumbRequired =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.Breadcrumb)}", "Breadcrumb is required.");

        public static readonly DomainError BreadcrumbTooLong =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.Breadcrumb)}", "Breadcrumb cannot exceed 256 characters.");

        public static readonly DomainError AnchorTextRequired =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorText)}", "Anchor text is required.");

        public static readonly DomainError AnchorTextTooLong =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorText)}", "Anchor text cannot exceed 256 characters.");

        public static readonly DomainError AnchorTitleTooLong =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorTitle)}", "Anchor title cannot exceed 256 characters.");

        public static readonly DomainError ShortDescriptionTooLong =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.ShortDescription)}", "Short description cannot exceed 512 characters.");

        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.CreatedByIp)}", "Created by IP address cannot exceed 128 characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(ProductGroup)}.{nameof(ProductGroup.UpdatedByIp)}", "Updated by IP address cannot exceed 128 characters.");
    }
}