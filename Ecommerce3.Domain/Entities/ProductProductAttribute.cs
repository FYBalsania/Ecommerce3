namespace Ecommerce3.Domain.Entities;

public sealed class ProductProductAttribute : ICreatable, IUpdatable, IDeletable
{
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }  
    public int ProductAttributeId { get; private set; }
    public ProductAttribute? ProductAttribute { get; private set; } 
    public int ProductAttributeSortOrder { get; private set; }
    public int ProductAttributeValueId { get; private set; }
    public ProductAttributeValue? ProductAttributeValue { get; private set; }
    public int ProductAttributeValueSortOrder { get; private set; }
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
}