using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductGroup : EntityWithImages<ProductGroupImage>, ICreatable, IUpdatable, IDeletable
{
    private readonly List<ProductGroupProductAttribute> _attributes = [];
    public override string ImageNamePrefix => Slug;
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Display { get; set; }
    public string Breadcrumb { get; set; }
    public string AnchorText { get; private set; }
    public string? AnchorTitle { get; private set; }
    public string? ShortDescription { get; private set; }
    public string? FullDescription { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
    public int CreatedBy { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
    public IReadOnlyList<ProductGroupProductAttribute> Attributes => _attributes;
    public ProductGroupPage? Page { get; private set; }

    private ProductGroup()
    {
    }

    public ProductGroup(string name, string slug, string display, string breadcrumb, string anchorText,
        string? anchorTitle, string? shortDescription, string? fullDescription, bool isActive, int sortOrder,
        int createdBy, string createdByIp)
    {
        ValidateName(name);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        ValidateAnchorText(anchorText);
        ValidateAnchorTitle(anchorTitle);
        ValidateShortDescription(shortDescription);
        ValidateCreatedBy(createdBy);
        ValidateCreatedByIp(createdByIp);

        Name = name;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        AnchorText = anchorText;
        AnchorTitle = anchorTitle;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
    
    public bool Update(string name, string slug, string display, string breadcrumb, string anchorText,
        string? anchorTitle, string? shortDescription, string? fullDescription, bool isActive, int sortOrder,
        int updatedBy, string updatedByIp)
    {
        ValidateName(name);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        ValidateAnchorText(anchorText);
        ValidateAnchorTitle(anchorTitle);
        ValidateShortDescription(shortDescription);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);
        
        if (Name == name && Slug == slug && Display == display && Breadcrumb == breadcrumb &&
            AnchorText == anchorText && AnchorTitle == anchorTitle && ShortDescription == shortDescription &&
            FullDescription == fullDescription && IsActive == isActive && SortOrder == sortOrder)
            return false;

        Name = name;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        AnchorText = anchorText;
        AnchorTitle = anchorTitle;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;

        return true;
    }
    
    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new DomainException(DomainErrors.ProductGroupErrors.CreatedByIpRequired);
        if (createdByIp.Length > 128) throw new DomainException(DomainErrors.ProductGroupErrors.CreatedByIpTooLong);
    }

    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.ProductGroupErrors.InvalidCreatedBy);
    }
    
    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.ProductGroupErrors.InvalidUpdatedBy);
    }

    private static void ValidateShortDescription(string? shortDescription)
    {
        if (shortDescription is not null && shortDescription.Length > 512)
            throw new DomainException(DomainErrors.ProductGroupErrors.ShortDescriptionTooLong);
    }

    private static void ValidateAnchorTitle(string? anchorTitle)
    {
        if (anchorTitle is not null && anchorTitle.Length > 256)
            throw new DomainException(DomainErrors.ProductGroupErrors.AnchorTitleTooLong);
    }

    private static void ValidateAnchorText(string anchorText)
    {
        if (string.IsNullOrWhiteSpace(anchorText))
            throw new DomainException(DomainErrors.ProductGroupErrors.AnchorTextRequired);
        if (anchorText.Length > 256) throw new DomainException(DomainErrors.ProductGroupErrors.AnchorTextTooLong);
    }

    private static void ValidateBreadcrumb(string breadcrumb)
    {
        if (string.IsNullOrWhiteSpace(breadcrumb))
            throw new DomainException(DomainErrors.ProductGroupErrors.BreadcrumbRequired);
        if (breadcrumb.Length > 256) throw new DomainException(DomainErrors.ProductGroupErrors.BreadcrumbTooLong);
    }

    private static void ValidateDisplay(string display)
    {
        if (string.IsNullOrWhiteSpace(display)) throw new DomainException(DomainErrors.ProductGroupErrors.DisplayRequired);
        if (display.Length > 256) throw new DomainException(DomainErrors.ProductGroupErrors.DisplayTooLong);
    }

    private static void ValidateSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug)) throw new DomainException(DomainErrors.ProductGroupErrors.SlugRequired);
        if (slug.Length > 256) throw new DomainException(DomainErrors.ProductGroupErrors.SlugTooLong);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException(DomainErrors.ProductGroupErrors.NameRequired);
        if (name.Length > 256) throw new DomainException(DomainErrors.ProductGroupErrors.NameTooLong);
    }
    
    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp)) throw new DomainException(DomainErrors.ProductGroupErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > 128) throw new DomainException(DomainErrors.ProductGroupErrors.UpdatedByIpTooLong);
    }
}