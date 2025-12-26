using Ecommerce3.Domain.Errors;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductProductAttribute : Entity, ICreatable, IUpdatable, IDeletable
{
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }
    public int ProductAttributeId { get; private set; }
    public ProductAttribute? ProductAttribute { get; private set; }
    public decimal ProductAttributeSortOrder { get; private set; }
    public int ProductAttributeValueId { get; private set; }
    public ProductAttributeValue? ProductAttributeValue { get; private set; }
    public decimal ProductAttributeValueSortOrder { get; private set; }
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

    private ProductProductAttribute()
    {
    }

    internal ProductProductAttribute(int productAttributeId, decimal productAttributeSortOrder,
        int productAttributeValueId, decimal productAttributeValueSortOrder, int createdBy, DateTime createdAt,
        string createdByIp)
    {
        ValidatePositiveNumber(productAttributeId, DomainErrors.ProductAttributeErrors.InvalidProductAttributeId);
        ValidatePositiveNumber(productAttributeValueId, DomainErrors.ProductAttributeValueErrors.InvalidId);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ProductAttributeErrors.InvalidCreatedBy);
        ICreatable.ValidateCreatedByIp(createdByIp, DomainErrors.ProductAttributeErrors.CreatedByIpRequired,
            DomainErrors.ProductAttributeErrors.CreatedByIpTooLong);

        ProductAttributeId = productAttributeId;
        ProductAttributeSortOrder = productAttributeSortOrder;
        ProductAttributeValueId = productAttributeValueId;
        ProductAttributeValueSortOrder = productAttributeValueSortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    internal void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
}