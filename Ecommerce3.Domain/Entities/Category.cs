using Ecommerce3.Domain.DomainEvents.Category;
using Ecommerce3.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Domain.Entities;

public sealed class Category : EntityWithImages<CategoryImage>, ICreatable, IUpdatable, IDeletable
{
    private readonly List<CategoryKVPListItem> _kvpListItems = [];
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

    public IReadOnlyList<CategoryKVPListItem> GetKVPListItemsByType(KVPListItemType type) =>
        _kvpListItems.Where(x => x.Type == type).OrderBy(x => x.SortOrder).ToList();

    public CategoryPage? Page { get; private set; }

    private Category()
    {
    }

    public Category(string name, string slug, string display, string breadcrumb, string anchorText, string? anchorTitle,
        string? googleCategory, Category? parent, string? shortDescription, string? fullDescription, bool isActive,
        int sortOrder, int createdBy, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));
        ArgumentException.ThrowIfNullOrWhiteSpace(display, nameof(display));
        ArgumentException.ThrowIfNullOrWhiteSpace(breadcrumb, nameof(breadcrumb));
        ArgumentException.ThrowIfNullOrWhiteSpace(anchorText, nameof(anchorText));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));

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
        string? fullDescription, bool isActive, int sortOrder, int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        if (Name == name && Slug == slug && Display == display && Breadcrumb == breadcrumb &&
            AnchorText == anchorText && AnchorTitle == anchorTitle && ParentId == parent?.Id &&
            GoogleCategory == googleCategory && ShortDescription == shortDescription &&
            FullDescription == fullDescription && IsActive == isActive && SortOrder == sortOrder)
            return false;

        ValidateParent(parent);

        if (Slug != slug) AddDomainEvent(new CategorySlugUpdatedDomainEvent(Slug, slug));
        if (ParentId != parent?.Id) AddDomainEvent(new CategoryParentIdUpdatedDomainEvent(ParentId, parent?.Id));

        Name = name;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        AnchorText = anchorText;
        AnchorTitle = anchorTitle;
        ParentId = parent?.Id;
        Path = parent is null ? new LTree(slug) : new LTree($"{parent.Path}.{slug}");
        GoogleCategory = googleCategory;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;

        return true;
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

    private void ValidateParent(Category? parent)
    {
        if (parent is null) return;
        if (parent.Id == Id) throw new InvalidOperationException("Cannot set a category's parent to itself.");
        if (parent.Path == Path || parent.Path.IsDescendantOf(Path))
            throw new InvalidOperationException(
                "Cannot set a category's parent to one of its own descendants (circular reference detected).");
    }
}