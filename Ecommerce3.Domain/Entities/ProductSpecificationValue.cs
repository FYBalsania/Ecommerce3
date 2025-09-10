namespace Ecommerce3.Domain.Entities;

public sealed class ProductSpecificationValue : Entity
{
    public int ProductSpecificationId { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Breadcrumb { get; private set; } = string.Empty;
    public decimal? NumberValue { get; private set; }
    public bool? BooleanValue { get; private set; }
    public DateOnly? DateOnlyValue { get; private set; }
    public int SortOrder { get; private set; }
}