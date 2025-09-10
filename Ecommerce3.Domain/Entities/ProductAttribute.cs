using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttribute : Entity, ICreatable, IUpdatable, IDeletable
{
    private readonly List<ProductAttributeValue> _values = [];

    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Display { get; private set; }
    public string Breadcrumb { get; private set; }
    public DataType DataType { get; private set; }
    public int SortOrder { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }

    public IReadOnlyList<ProductAttributeValue> Values => _values;

    public ProductAttribute(string name, string slug, string display, string breadcrumb, DataType dataType, int sortOrder,
        int createdBy, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));
        ArgumentException.ThrowIfNullOrWhiteSpace(display, nameof(display));
        ArgumentException.ThrowIfNullOrWhiteSpace(breadcrumb, nameof(breadcrumb));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));

        Name = name;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        DataType = dataType;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
}