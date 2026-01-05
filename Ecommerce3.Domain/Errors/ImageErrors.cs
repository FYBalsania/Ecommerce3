using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ImageErrors
    {
        public static readonly DomainError InvalidId =
            new($"{nameof(Image)}.{nameof(Image.Id)}", "Image id is invalid.");
        
        public static readonly DomainError ImageQueryRepositoryNotFound =
            new($"{nameof(Image)}.{nameof(Image.Id)}", "Specific Image query repository not found.");
        
        public static readonly DomainError OriginalFileNameRequired =
            new($"{nameof(Image)}.{nameof(Image.OgFileName)}", "Original file name is required.");

        public static readonly DomainError FileNameRequired =
            new($"{nameof(Image)}.{nameof(Image.FileName)}", "File name is mandatory.");

        public static readonly DomainError FileExtensionRequired =
            new($"{nameof(Image)}.{nameof(Image.FileExtension)}", "File extension is required.");

        public static readonly DomainError FileExtensionTooLong =
            new($"{nameof(Image)}.{nameof(Image.FileExtension)}", "File extension cannot exceed 8 characters.");
        
        public static readonly DomainError InvalidFormat =
            new($"{nameof(Image)}.File", "File format is invalid.");
        
        public static readonly DomainError EmptyFile =
            new($"{nameof(Image)}.File", "Image file cannot be empty.");
        
        public static readonly DomainError FileRequired =
            new($"{nameof(Image)}.File", "Image file is required.");
        
        public static readonly DomainError FileSizeTooLarge =
            new($"{nameof(Image)}.File", "Image file size cannot exceed allowed limit.");

        public static readonly DomainError InvalidImageTypeId =
            new($"{nameof(Image)}.{nameof(Image.ImageTypeId)}", "Image type Id is invalid.");
        
        public static readonly DomainError InvalidImageType =
            new($"{nameof(Image)}.{nameof(Image.ImageType)}", "Invalid image type.");

        public static readonly DomainError InvalidLink = 
            new($"{nameof(Image)}.{nameof(Image.Link)}", "Image link is invalid.");

        public static readonly DomainError LinkTargetRequiredWhenLinkProvided =
            new($"{nameof(Image)}.{nameof(Image.LinkTarget)}", "Link target is required when Link is provided.");

        public static readonly DomainError InvalidLinkTarget =
            new($"{nameof(Image)}.{nameof(Image.LinkTarget)}", "Link target is invalid.");

        public static readonly DomainError InvalidBrandId =
            new($"{nameof(Image)}.{nameof(BrandImage.BrandId)}", "Brand Id is invalid.");

        public static readonly DomainError InvalidCategoryId =
            new($"{nameof(Image)}.{nameof(CategoryImage.CategoryId)}", "Category Id is invalid.");

        public static readonly DomainError InvalidProductId =
            new($"{nameof(Image)}.{nameof(ProductImage.ProductId)}", "Product Id is invalid.");

        public static readonly DomainError InvalidProductGroupId =
            new($"{nameof(Image)}.{nameof(ProductGroupImage.ProductGroupId)}", "Product Group Id is invalid.");

        public static readonly DomainError InvalidBankId =
            new($"{nameof(Image)}.{nameof(BankImage.BankId)}", "Bank Id is invalid.");

        public static readonly DomainError InvalidPageId =
            new($"{nameof(Image)}.{nameof(PageImage.PageId)}", "Page Id is invalid.");

        public static readonly DomainError ParentEntityTypeRequired =
            new($"{nameof(Image)}.ParentEntityType", "Parent entity type is required.");
        
        public static readonly DomainError InvalidParentEntityType =
            new($"{nameof(Image)}.ParentEntityType", "Parent entity type is invalid.");
        
        public static readonly DomainError ParentEntityIdRequired =
            new($"{nameof(Image)}.ParentEntityId", "Parent entity id. is required.");
        
        public static readonly DomainError InvalidParentEntityId =
            new($"{nameof(Image)}.ParentEntityId", "Parent entity id. is invalid.");
        
        public static readonly DomainError ImageEntityTypeRequired =
            new($"{nameof(Image)}.ImageEntityType", "Image entity type is required.");
        
        public static readonly DomainError InvalidImageEntityType =
            new($"{nameof(Image)}.ImageEntityType", "Image entity type is invalid.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(Image)}.{nameof(Image.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(Image)}.{nameof(Image.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(Image)}.{nameof(Image.CreatedByIp)}", $"Created by IP address cannot exceed {ICreatable.CreatedByIpMaxLength} characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(Image)}.{nameof(Image.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(Image)}.{nameof(Image.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(Image)}.{nameof(Image.UpdatedByIp)}",
                $"Updated by IP address cannot exceed {IUpdatable.UpdatedByIpMaxLength} characters.");
        
        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(Image)}.{nameof(Image.DeletedBy)}", "Deleted by is invalid.");

        public static readonly DomainError DeletedByIpRequired =
            new($"{nameof(Image)}.{nameof(Image.DeletedByIp)}", "Deleted by IP address is required.");

        public static readonly DomainError DeletedByIpTooLong =
            new($"{nameof(Image)}.{nameof(Image.DeletedByIp)}",
                $"Deleted by IP address cannot exceed {IDeletable.DeletedByIpMaxLength} characters.");
    }
}