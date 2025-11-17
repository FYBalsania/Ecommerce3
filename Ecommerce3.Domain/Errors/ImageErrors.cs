using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ImageErrors
    {
        public static readonly DomainError OriginalFileNameRequired =
            new($"{nameof(Image)}.{nameof(Image.OgFileName)}", "Original file name is required.");

        public static readonly DomainError FileNameRequired =
            new($"{nameof(Image)}.{nameof(Image.FileName)}", "File name is mandatory.");

        public static readonly DomainError FileExtensionRequired =
            new($"{nameof(Image)}.{nameof(Image.FileExtension)}", "File extension is required.");

        public static readonly DomainError FileExtensionTooLong =
            new($"{nameof(Image)}.{nameof(Image.FileExtension)}", "File extension cannot exceed 8 characters.");

        public static readonly DomainError InvalidImageTypeId =
            new($"{nameof(Image)}.{nameof(Image.ImageTypeId)}", "Image type Id is invalid.");

        public static readonly DomainError InvalidLink = new($"{nameof(Image)}.{nameof(Image.Link)}",
            "Image link is invalid.");

        public static readonly DomainError LinkTargetRequiredWhenLinkProvided =
            new($"{nameof(Image)}.{nameof(Image.LinkTarget)}", "Link target is required when Link is provided.");

        public static readonly DomainError InvalidLinkTarget =
            new($"{nameof(Image)}.{nameof(Image.LinkTarget)}", "Link target is invalid.");

        public static readonly DomainError InvalidCreatedByUser =
            new($"{nameof(Image)}.{nameof(Image.CreatedBy)}", "Created-by user Id is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(Image)}.{nameof(Image.CreatedByIp)}", "Created-by IP address is required.");

        public static readonly DomainError InvalidCreatedIp =
            new($"{nameof(Image)}.{nameof(Image.CreatedByIp)}", "Created-by IP address is invalid.");

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
    }
}