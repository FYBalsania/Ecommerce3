using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class Category : EntityWithImages, ICreatable, IUpdatable, IDeletable
{
    private readonly List<CategoryKVPListItem> _kvpListItems = [];
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Display { get; private set; }
    public string Breadcrumb { get; private set; }
    public string AnchorText { get; private set; }
    public string? AnchorTitle { get; private set; }
    public string? GoogleCategory { get; private set; }
    public string MetaTitle { get; private set; }
    public string? MetaDescription { get; private set; }
    public string? MetaKeywords { get; private set; }
    public string H1 { get; private set; }
    public int? ParentId { get; private set; }
    public Category? Parent { get; private set; }
    public string Path { get; private set; }
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

    private Category()
    {
    }

    public Category(string name, string slug, string display, string breadcrumb, string anchorText, string? anchorTitle,
        string? googleCategory, string metaTitle, string? metaDescription, string? metaKeywords, string h1,
        int? parentId, string path, string? shortDescription, string? fullDescription, bool isActive, int sortOrder,
        int createdBy, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));
        ArgumentException.ThrowIfNullOrWhiteSpace(display, nameof(display));
        ArgumentException.ThrowIfNullOrWhiteSpace(breadcrumb, nameof(breadcrumb));
        ArgumentException.ThrowIfNullOrWhiteSpace(anchorText, nameof(anchorText));
        ArgumentException.ThrowIfNullOrWhiteSpace(metaTitle, nameof(metaTitle));
        ArgumentException.ThrowIfNullOrWhiteSpace(h1, nameof(h1));
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));
        
        Name = name;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        AnchorText = anchorText;
        AnchorTitle = anchorTitle;
        GoogleCategory = googleCategory;
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        MetaKeywords = metaKeywords;
        H1 = h1;
        ParentId = parentId;
        Path = path;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
}