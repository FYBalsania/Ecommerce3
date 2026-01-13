using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ProductAttributeErrors
    {
        public static readonly DomainError InvalidProductAttributeId =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Id)}", "Product attribute Id is invalid.");
        
        public static readonly DomainError NameRequired =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Name)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Name)}", "Name cannot exceed 256 characters.");

        public static readonly DomainError DuplicateName =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Name)}", "Duplicate name.");

        public static readonly DomainError SlugRequired =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Slug)}", "Slug is required.");
        
        public static readonly DomainError DuplicateSlug =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Slug)}", "Duplicate slug.");

        public static readonly DomainError SlugTooLong =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Slug)}", "Slug cannot exceed 256 characters.");

        public static readonly DomainError DisplayRequired =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Display)}", "Display is required.");

        public static readonly DomainError DisplayTooLong =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Display)}", "Display cannot exceed 256 characters.");

        public static readonly DomainError BreadcrumbRequired =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Breadcrumb)}", "Breadcrumb is required.");

        public static readonly DomainError BreadcrumbTooLong =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Breadcrumb)}", "Breadcrumb cannot exceed 256 characters.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.CreatedBy)}", "Created by is invalid.");
        
        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(ProductAttribute)}.{nameof(ProductAttribute.DeletedBy)}",
                "Deleted by is invalid.");
    }
}