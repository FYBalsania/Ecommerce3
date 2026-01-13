using System.Net;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttributeBooleanValue : ProductAttributeValue
{
    public bool BooleanValue { get; private set; }

    private ProductAttributeBooleanValue() : base()
    {
    }

    internal ProductAttributeBooleanValue(bool booleanValue, string slug, string display, string breadcrumb,
        int sortOrder, int createdBy, DateTime createdAt, IPAddress createdByIp)
        : base(booleanValue.ToString(), slug, display, breadcrumb, sortOrder, createdBy, createdAt, createdByIp)
    {
        BooleanValue = booleanValue;
    }

    public bool Update(string slug, string display, string breadcrumb, int sortOrder, int updatedBy, DateTime updatedAt,
        IPAddress updatedByIp)
    {
        return base.Update(Value, slug, display, breadcrumb, sortOrder, updatedBy, updatedAt, updatedByIp);
    }
}