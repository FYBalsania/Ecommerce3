using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ImageTypeErrors
    {
        public static readonly DomainError InvalidId =
            new($"{nameof(ImageType)}.{nameof(ImageType.Id)}", "Image type id is invalid.");
        
        public static readonly DomainError NameRequired =
            new($"{nameof(ImageType)}.{nameof(ImageType.Name)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(ImageType)}.{nameof(ImageType.Name)}", "Name cannot exceed 256 characters.");

        public static readonly DomainError DuplicateName =
            new($"{nameof(ImageType)}.{nameof(ImageType.Name)}", "Duplicate name.");

        public static readonly DomainError SlugRequired =
            new($"{nameof(ImageType)}.{nameof(ImageType.Slug)}", "Slug is required.");
        
        public static readonly DomainError DuplicateSlug =
            new($"{nameof(ImageType)}.{nameof(ImageType.Slug)}", "Duplicate slug.");

        public static readonly DomainError SlugTooLong =
            new($"{nameof(ImageType)}.{nameof(ImageType.Slug)}", "Slug cannot exceed 256 characters.");
        
        public static readonly DomainError DescriptionTooLong =
            new($"{nameof(ImageType)}.{nameof(ImageType.Description)}", "Description cannot exceed 1024 characters.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(ImageType)}.{nameof(ImageType.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(ImageType)}.{nameof(ImageType.UpdatedBy)}", "Updated by is invalid.");
    }
}