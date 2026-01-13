using System.Net;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class Brand : EntityWithImages<BrandImage>, ICreatable, IUpdatable, IDeletable
{
    public override string ImageNamePrefix => Slug;
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Display { get; private set; }
    public string Breadcrumb { get; private set; }
    public string AnchorText { get; private set; }
    public string? AnchorTitle { get; private set; }
    public string? ShortDescription { get; private set; }
    public string? FullDescription { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
    public int CreatedBy { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IPAddress CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IPAddress? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public IPAddress? DeletedByIp { get; private set; }
    public BrandPage? Page { get; private set; }

    private Brand()
    {
    }

    public Brand(string name, string slug, string display, string breadcrumb, string anchorText, string? anchorTitle,
        string? shortDescription, string? fullDescription, bool isActive, int sortOrder, int createdBy,
        IPAddress createdByIp)
    {
        ValidateName(name);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        ValidateAnchorText(anchorText);
        ValidateAnchorTitle(anchorTitle);
        ValidateShortDescription(shortDescription);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.BrandErrors.InvalidCreatedBy);

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

    private static void ValidateShortDescription(string? shortDescription)
    {
        if (shortDescription is not null && shortDescription.Length > 512)
            throw new DomainException(DomainErrors.BrandErrors.ShortDescriptionTooLong);
    }

    private static void ValidateAnchorTitle(string? anchorTitle)
    {
        if (anchorTitle is not null && anchorTitle.Length > 256)
            throw new DomainException(DomainErrors.BrandErrors.AnchorTitleTooLong);
    }

    private static void ValidateAnchorText(string anchorText)
    {
        if (string.IsNullOrWhiteSpace(anchorText))
            throw new DomainException(DomainErrors.BrandErrors.AnchorTextRequired);
        if (anchorText.Length > 256) throw new DomainException(DomainErrors.BrandErrors.AnchorTextTooLong);
    }

    private static void ValidateBreadcrumb(string breadcrumb)
    {
        if (string.IsNullOrWhiteSpace(breadcrumb))
            throw new DomainException(DomainErrors.BrandErrors.BreadcrumbRequired);
        if (breadcrumb.Length > 256) throw new DomainException(DomainErrors.BrandErrors.BreadcrumbTooLong);
    }

    private static void ValidateDisplay(string display)
    {
        if (string.IsNullOrWhiteSpace(display)) throw new DomainException(DomainErrors.BrandErrors.DisplayRequired);
        if (display.Length > 256) throw new DomainException(DomainErrors.BrandErrors.DisplayTooLong);
    }

    private static void ValidateSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug)) throw new DomainException(DomainErrors.BrandErrors.SlugRequired);
        if (slug.Length > 256) throw new DomainException(DomainErrors.BrandErrors.SlugTooLong);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException(DomainErrors.BrandErrors.NameRequired);
        if (name.Length > 256) throw new DomainException(DomainErrors.BrandErrors.NameTooLong);
    }

    public void Update(string name, string slug, string display, string breadcrumb, string anchorText,
        string? anchorTitle, string? shortDescription, string? fullDescription, bool isActive, int sortOrder,
        int updatedBy, IPAddress updatedByIp)
    {
        ValidateName(name);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        ValidateAnchorText(anchorText);
        ValidateAnchorTitle(anchorTitle);
        ValidateShortDescription(shortDescription);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.BrandErrors.InvalidUpdatedBy);

        if (Name == name && Slug == slug && Display == display && Breadcrumb == breadcrumb &&
            AnchorText == anchorText && AnchorTitle == anchorTitle && ShortDescription == shortDescription &&
            FullDescription == fullDescription && IsActive == isActive && SortOrder == sortOrder)
            return;
        
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
    }
}