namespace Ecommerce3.Domain.Entities;

public sealed class ProductProductAttribute : Entity, ICreatable, IUpdatable, IDeletable
{
    public int ProductId { get; private set; }
    public int ProductAttributeId { get; private set; }
    public int ProductAttributeSortOrder { get; private set; }
    public int ProductAttributeValueId { get; private set; }
    public int ProductAttributeValueSortOrder { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
}