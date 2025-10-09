namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttributeBooleanValue : ProductAttributeValue
{
    public bool BooleanValue { get; private set; }

    public ProductAttributeBooleanValue(string value, string slug, string display, string breadcrumb,
        bool booleanValue, int sortOrder, int createdBy, DateTime createdAt, string createdByIp)
        : base(value, slug, display, breadcrumb, sortOrder, createdBy, createdAt, createdByIp)
    {
        BooleanValue = booleanValue;
    }
}