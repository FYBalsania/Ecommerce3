using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class CategoryErrors
    {
        public static readonly DomainError NameRequired =
            new($"{nameof(Category)}.{nameof(Category.Name)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(Category)}.{nameof(Category.Name)}", "Name cannot exceed 256 characters.");

        public static readonly DomainError DuplicateName =
            new($"{nameof(Category)}.{nameof(Category.Name)}", "Duplicate name.");

        public static readonly DomainError SlugRequired =
            new($"{nameof(Category)}.{nameof(Category.Slug)}", "Slug is required.");
        
        public static readonly DomainError DuplicateSlug =
            new($"{nameof(Category)}.{nameof(Category.Slug)}", "Duplicate slug.");

        public static readonly DomainError SlugTooLong =
            new($"{nameof(Category)}.{nameof(Category.Slug)}", "Slug cannot exceed 256 characters.");

        public static readonly DomainError DisplayRequired =
            new($"{nameof(Category)}.{nameof(Category.Display)}", "Display is required.");

        public static readonly DomainError DisplayTooLong =
            new($"{nameof(Category)}.{nameof(Category.Display)}", "Display cannot exceed 256 characters.");

        public static readonly DomainError BreadcrumbRequired =
            new($"{nameof(Category)}.{nameof(Category.Breadcrumb)}", "Breadcrumb is required.");

        public static readonly DomainError BreadcrumbTooLong =
            new($"{nameof(Category)}.{nameof(Category.Breadcrumb)}", "Breadcrumb cannot exceed 256 characters.");

        public static readonly DomainError AnchorTextRequired =
            new($"{nameof(Category)}.{nameof(Category.AnchorText)}", "Anchor text is required.");

        public static readonly DomainError AnchorTextTooLong =
            new($"{nameof(Category)}.{nameof(Category.AnchorText)}", "Anchor text cannot exceed 256 characters.");

        public static readonly DomainError AnchorTitleTooLong =
            new($"{nameof(Category)}.{nameof(Category.AnchorTitle)}", "Anchor title cannot exceed 256 characters.");

        public static readonly DomainError ShortDescriptionTooLong =
            new($"{nameof(Category)}.{nameof(Category.ShortDescription)}", "Short description cannot exceed 512 characters.");

        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(Category)}.{nameof(Category.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(Category)}.{nameof(Category.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(Category)}.{nameof(Category.CreatedByIp)}", "Created by IP address cannot exceed 128 characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(Category)}.{nameof(Category.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(Category)}.{nameof(Category.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(Category)}.{nameof(Category.UpdatedByIp)}", "Updated by IP address cannot exceed 128 characters.");
    }
}