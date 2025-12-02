namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttributeDateOnlyValue : ProductAttributeValue
{
    public DateOnly DateOnlyValue { get; private set; }

    private ProductAttributeDateOnlyValue() : base()
    {
    }

    public ProductAttributeDateOnlyValue(DateOnly dateOnlyValue, string slug, string display, string breadcrumb,
        int sortOrder, int createdBy, DateTime createdAt, string createdByIp)
        : base(dateOnlyValue.ToString(Common.DateOnlyFormat), slug, display, breadcrumb, sortOrder, createdBy,
            createdAt, createdByIp)
    {
        DateOnlyValue = dateOnlyValue;
    }

    internal bool Update(DateOnly dateOnlyValue, string slug, string display, string breadcrumb, int sortOrder,
        int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        if (!base.Update(dateOnlyValue.ToString(Common.DateOnlyFormat), slug, display, breadcrumb, sortOrder, updatedBy,
                updatedAt, updatedByIp) && DateOnlyValue == dateOnlyValue) return false;

        DateOnlyValue = dateOnlyValue;

        return true;
    }
}