using Ecommerce3.Domain.DomainEvents.Category;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Domain.Entities;

public sealed class Category : EntityWithImages<CategoryImage>, ICreatable, IUpdatable, IDeletable,
    IKVPListItems<CategoryKVPListItem>
{
    private readonly List<CategoryKVPListItem> _kvpListItems = [];
    public override string ImageNamePrefix => Slug;
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Display { get; private set; }
    public string Breadcrumb { get; private set; }
    public string AnchorText { get; private set; }
    public string? AnchorTitle { get; private set; }
    public string? GoogleCategory { get; private set; }
    public int? ParentId { get; private set; }
    public Category? Parent { get; private set; }
    public LTree Path { get; private set; }
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
    public IReadOnlyList<CategoryKVPListItem> KVPListItems => _kvpListItems;

    // public IReadOnlyList<CategoryKVPListItem> GetKVPListItems(KVPListItemType type) =>
    //     _kvpListItems.Where(x => x.Type == type).OrderBy(x => x.SortOrder).ToList();

    public CategoryPage? Page { get; private set; }

    private Category()
    {
    }

    public Category(string name, string slug, string display, string breadcrumb, string anchorText, string? anchorTitle,
        string? googleCategory, Category? parent, string? shortDescription, string? fullDescription, bool isActive,
        int sortOrder, int createdBy, string createdByIp)
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
        GoogleCategory = googleCategory;
        ParentId = parent?.Id;
        Path = parent is null ? new LTree(slug) : new LTree($"{parent.Path}.{slug}");
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }

    public bool Update(string name, string slug, string display, string breadcrumb, string anchorText,
        string? anchorTitle, Category? parent, string? googleCategory, string? shortDescription,
        string? fullDescription, bool isActive, int sortOrder, int updatedBy, string updatedByIp)
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
            AnchorText == anchorText && AnchorTitle == anchorTitle && ParentId == parent?.Id &&
            GoogleCategory == googleCategory && ShortDescription == shortDescription &&
            FullDescription == fullDescription && IsActive == isActive && SortOrder == sortOrder)
            return false;

        ValidateParent(parent);

        if (Slug != slug || ParentId != parent?.Id)
        {
            var oldSlug = Slug;
            var oldPath = Path.ToString();

            Slug = slug;
            ParentId = parent?.Id;
            Path = parent is null ? new LTree(slug) : new LTree($"{parent.Path}.{slug}");

            AddDomainEvent(new CategorySlugUpdatedDomainEvent(Id, oldSlug, slug, oldPath, Path.ToString()));
        }
        
        Name = name;
        Display = display;
        Breadcrumb = breadcrumb;
        AnchorText = anchorText;
        AnchorTitle = anchorTitle;
        GoogleCategory = googleCategory;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;

        return true;
    }

    public void UpdateSlug(string parentSlug)
    {
        Path = new LTree($"{parentSlug}.{Slug}");
    }

    public void ChangeParent(Category? parent, int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        if (ParentId == parent?.Id) return;

        ValidateParent(parent);

        ParentId = parent?.Id;
        Path = parent is null ? new LTree(Slug) : new LTree($"{parent.Path}.{Slug}");
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;
    }

    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    private void ValidateParent(Category? parent)
    {
        if (parent is null) return;
        if (parent.Id == Id) throw new InvalidOperationException("Cannot set a category's parent to itself.");
        if (parent.Path == Path) // || parent.Path.IsDescendantOf(Path)
            throw new InvalidOperationException(
                "Cannot set a category's parent to one of its own descendants (circular reference detected).");
    }

    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new DomainException(DomainErrors.CategoryErrors.CreatedByIpRequired);
        if (createdByIp.Length > 128) throw new DomainException(DomainErrors.CategoryErrors.CreatedByIpTooLong);
    }

    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.CategoryErrors.InvalidCreatedBy);
    }

    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.CategoryErrors.InvalidUpdatedBy);
    }

    private static void ValidateShortDescription(string? shortDescription)
    {
        if (shortDescription is not null && shortDescription.Length > 512)
            throw new DomainException(DomainErrors.CategoryErrors.ShortDescriptionTooLong);
    }

    private static void ValidateAnchorTitle(string? anchorTitle)
    {
        if (anchorTitle is not null && anchorTitle.Length > 256)
            throw new DomainException(DomainErrors.CategoryErrors.AnchorTitleTooLong);
    }

    private static void ValidateAnchorText(string anchorText)
    {
        if (string.IsNullOrWhiteSpace(anchorText))
            throw new DomainException(DomainErrors.CategoryErrors.AnchorTextRequired);
        if (anchorText.Length > 256) throw new DomainException(DomainErrors.CategoryErrors.AnchorTextTooLong);
    }

    private static void ValidateBreadcrumb(string breadcrumb)
    {
        if (string.IsNullOrWhiteSpace(breadcrumb))
            throw new DomainException(DomainErrors.CategoryErrors.BreadcrumbRequired);
        if (breadcrumb.Length > 256) throw new DomainException(DomainErrors.CategoryErrors.BreadcrumbTooLong);
    }

    private static void ValidateDisplay(string display)
    {
        if (string.IsNullOrWhiteSpace(display)) throw new DomainException(DomainErrors.CategoryErrors.DisplayRequired);
        if (display.Length > 256) throw new DomainException(DomainErrors.CategoryErrors.DisplayTooLong);
    }

    private static void ValidateSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug)) throw new DomainException(DomainErrors.CategoryErrors.SlugRequired);
        if (slug.Length > 256) throw new DomainException(DomainErrors.CategoryErrors.SlugTooLong);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException(DomainErrors.CategoryErrors.NameRequired);
        if (name.Length > 256) throw new DomainException(DomainErrors.CategoryErrors.NameTooLong);
    }

    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp))
            throw new DomainException(DomainErrors.CategoryErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > 128) throw new DomainException(DomainErrors.CategoryErrors.UpdatedByIpTooLong);
    }
}