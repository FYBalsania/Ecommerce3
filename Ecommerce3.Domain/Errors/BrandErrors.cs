using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class BrandErrors
    {
        public static readonly DomainError InvalidBrandId =
            new($"{nameof(Brand)}.{nameof(Brand.Id)}", "Brand ID is invalid.");
        
        public static readonly DomainError NameRequired =
            new($"{nameof(Brand)}.{nameof(Brand.Name)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(Brand)}.{nameof(Brand.Name)}", "Name cannot exceed 256 characters.");

        public static readonly DomainError DuplicateName =
            new($"{nameof(Brand)}.{nameof(Brand.Name)}", "Duplicate name.");

        public static readonly DomainError SlugRequired =
            new($"{nameof(Brand)}.{nameof(Brand.Slug)}", "Slug is required.");
        
        public static readonly DomainError DuplicateSlug =
            new($"{nameof(Brand)}.{nameof(Brand.Slug)}", "Duplicate slug.");

        public static readonly DomainError SlugTooLong =
            new($"{nameof(Brand)}.{nameof(Brand.Slug)}", "Slug cannot exceed 256 characters.");

        public static readonly DomainError DisplayRequired =
            new($"{nameof(Brand)}.{nameof(Brand.Display)}", "Display is required.");

        public static readonly DomainError DisplayTooLong =
            new($"{nameof(Brand)}.{nameof(Brand.Display)}", "Display cannot exceed 256 characters.");

        public static readonly DomainError BreadcrumbRequired =
            new($"{nameof(Brand)}.{nameof(Brand.Breadcrumb)}", "Breadcrumb is required.");

        public static readonly DomainError BreadcrumbTooLong =
            new($"{nameof(Brand)}.{nameof(Brand.Breadcrumb)}", "Breadcrumb cannot exceed 256 characters.");

        public static readonly DomainError AnchorTextRequired =
            new($"{nameof(Brand)}.{nameof(Brand.AnchorText)}", "Anchor text is required.");

        public static readonly DomainError AnchorTextTooLong =
            new($"{nameof(Brand)}.{nameof(Brand.AnchorText)}", "Anchor text cannot exceed 256 characters.");

        public static readonly DomainError AnchorTitleTooLong =
            new($"{nameof(Brand)}.{nameof(Brand.AnchorTitle)}", "Anchor title cannot exceed 256 characters.");

        public static readonly DomainError ShortDescriptionTooLong =
            new($"{nameof(Brand)}.{nameof(Brand.ShortDescription)}", "Short description cannot exceed 512 characters.");

        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(Brand)}.{nameof(Brand.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(Brand)}.{nameof(Brand.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(Brand)}.{nameof(Brand.CreatedByIp)}", "Created by IP address cannot exceed 128 characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(Brand)}.{nameof(Brand.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(Brand)}.{nameof(Brand.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(Brand)}.{nameof(Brand.UpdatedByIp)}", "Updated by IP address cannot exceed 128 characters.");
    }
}