namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttributeBooleanValue : ProductAttributeValue
{
    public bool BooleanValue { get; private set; }

    private ProductAttributeBooleanValue() : base()
    {
    }

    internal ProductAttributeBooleanValue(string value, string slug, string display, string breadcrumb,
        bool booleanValue, int sortOrder, int createdBy, DateTime createdAt, string createdByIp)
        : base(value, slug, display, breadcrumb, sortOrder, createdBy, createdAt, createdByIp)
    {
        BooleanValue = booleanValue;
    }

    public bool Update(string slug, string display, string breadcrumb, int sortOrder, int updatedBy, DateTime updatedAt,
        string updatedByIp)
    {
        return base.Update(Value, slug, display, breadcrumb, sortOrder, updatedBy, updatedAt, updatedByIp);
    }
}