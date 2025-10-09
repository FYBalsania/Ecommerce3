namespace Ecommerce3.Domain.Entities;

public class ProductAttributeValue : Entity, ICreatable, IUpdatable, IDeletable
{
    public int ProductAttributeId { get; private set; }
    public ProductAttribute? ProductAttribute { get; private set; }
    public string Discriminator { get; private set; } = string.Empty;
    public string Value { get; private set; }
    public string Slug { get; private set; }
    public string Display { get; private set; }
    public string Breadcrumb { get; private set; }
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

    private protected ProductAttributeValue()
    {
    }

    public ProductAttributeValue(string value, string slug, string display, string breadcrumb, int sortOrder,
        int createdBy, DateTime createdAt, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
        ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));
        ArgumentException.ThrowIfNullOrWhiteSpace(display, nameof(display));
        ArgumentException.ThrowIfNullOrWhiteSpace(breadcrumb, nameof(breadcrumb));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));

        Value = value;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }
}