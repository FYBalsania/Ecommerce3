using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class PageErrors
    {
        public static readonly DomainError PageQueryRepositoryNotFound =
            new($"{nameof(Page)}.{nameof(Page.Id)}", "Specific page query repository not found.");
        
        public static readonly DomainError InvalidId =
            new($"{nameof(Page)}.{nameof(Page.Id)}", "Page id is invalid.");
        
        public static readonly DomainError InvalidPath =
            new($"{nameof(Page)}.{nameof(Page.Path)}", "Path is invalid.");

        public static readonly DomainError PathRequired =
            new($"{nameof(Page)}.{nameof(Page.Path)}", "Path is required.");

        public static readonly DomainError PathTooLong =
            new($"{nameof(Page)}.{nameof(Page.Path)}", "Path cannot exceed 256 characters.");
        
        public static readonly DomainError DuplicatePath =
            new($"{nameof(Page)}.{nameof(Page.Path)}", "Duplicate path");

        public static readonly DomainError MetaTitleRequired =
            new($"{nameof(Page)}.{nameof(Page.MetaTitle)}", "Meta title is required.");

        public static readonly DomainError MetaTitleTooLong =
            new($"{nameof(Page)}.{nameof(Page.MetaTitle)}", "Meta title cannot exceed 256 characters.");

        public static readonly DomainError MetaDescriptionTooLong =
            new($"{nameof(Page)}.{nameof(Page.MetaDescription)}", "Meta description cannot exceed 2048 characters.");

        public static readonly DomainError MetaKeywordsTooLong =
            new($"{nameof(Page)}.{nameof(Page.MetaKeywords)}", "Meta keywords cannot exceed 1024 characters.");

        public static readonly DomainError MetaRobotsTooLong =
            new($"{nameof(Page)}.{nameof(Page.MetaRobots)}", "Meta robots cannot exceed 32 characters.");

        public static readonly DomainError H1TooLong =
            new($"{nameof(Page)}.{nameof(Page.H1)}", "H1 cannot exceed 256 characters.");

        public static readonly DomainError InvalidCanonicalUrl =
            new($"{nameof(Page)}.{nameof(Page.CanonicalUrl)}", "Canonical URL is invalid.");

        public static readonly DomainError CanonicalUrlTooLong =
            new($"{nameof(Page)}.{nameof(Page.CanonicalUrl)}", "Canonical URL cannot exceed 2048 characters.");
        
        public static readonly DomainError OgTitleTooLong =
            new($"{nameof(Page)}.{nameof(Page.OgTitle)}", "OG title cannot exceed 256 characters.");
        
        public static readonly DomainError OgDescriptionTooLong =
            new($"{nameof(Page)}.{nameof(Page.OgDescription)}", "OG description cannot exceed 2048 characters.");
        
        public static readonly DomainError InvalidOgImageUrl =
            new($"{nameof(Page)}.{nameof(Page.OgImageUrl)}", "OG image URL is invalid.");
        
        public static readonly DomainError OgImageUrlTooLong =
            new($"{nameof(Page)}.{nameof(Page.OgImageUrl)}", "OG image URL cannot exceed 2048 characters.");
        
        public static readonly DomainError OgTypeTooLong =
            new($"{nameof(Page)}.{nameof(Page.OgType)}", "OG type cannot exceed 16 characters.");
        
        public static readonly DomainError TwitterCardTooLong =
            new($"{nameof(Page)}.{nameof(Page.TwitterCard)}", "Twitter card cannot exceed 64 characters.");
        
        public static readonly DomainError SummaryTooLong =
            new($"{nameof(Page)}.{nameof(Page.Summary)}", "Summary cannot exceed 1024 characters.");
        
        public static readonly DomainError LanguageRequired =
            new($"{nameof(Page)}.{nameof(Page.Language)}", "Language is required.");
        
        public static readonly DomainError LanguageTooLong =
            new($"{nameof(Page)}.{nameof(Page.Language)}", "Language cannot exceed 8 characters.");
        
        public static readonly DomainError RegionTooLong =
            new($"{nameof(Page)}.{nameof(Page.Region)}", "Region cannot exceed 8 characters.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(Page)}.{nameof(Page.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(Page)}.{nameof(Page.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(Page)}.{nameof(Page.CreatedByIp)}", "Created by IP address cannot exceed 128 characters.");
        
        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(Page)}.{nameof(Page.UpdatedBy)}", "Updated by is invalid.");
        
        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(Page)}.{nameof(Page.UpdatedByIp)}", "Updated by IP address is required.");
        
        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(Page)}.{nameof(Page.UpdatedByIp)}", "Updated by IP address cannot exceed 128 characters.");
    }
    
    public static class BrandPageErrors
    {
        public static readonly DomainError InvalidBrandId =
            new($"{nameof(BrandPage)}.{nameof(BrandPage.Id)}", "Brand id is invalid.");
    }
    
    public static class CategoryPageErrors
    {
        public static readonly DomainError InvalidCategoryId =
            new($"{nameof(CategoryPage)}.{nameof(CategoryPage.Id)}", "Category id is invalid.");
    }
    
    public static class ProductGroupPageErrors
    {
        public static readonly DomainError InvalidProductGroupId =
            new($"{nameof(ProductGroupPage)}.{nameof(ProductGroupPage.Id)}", "Product group id is invalid.");
    }
    
    public static class ProductPageErrors
    {
        public static readonly DomainError InvalidProductId =
            new($"{nameof(ProductPage)}.{nameof(ProductPage.Id)}", "Product id is invalid.");
    }
    
    public static class BankPageErrors
    {
        public static readonly DomainError InvalidBankId =
            new($"{nameof(BankPage)}.{nameof(BankPage.Id)}", "Bank id is invalid.");
    }
}