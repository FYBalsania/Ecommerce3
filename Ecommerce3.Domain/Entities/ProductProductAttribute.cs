using System.Net;
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
    public IPAddress CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IPAddress? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public IPAddress? DeletedByIp { get; private set; }

    private ProductProductAttribute()
    {
    }

    internal ProductProductAttribute(int productAttributeId, decimal productAttributeSortOrder,
        int productAttributeValueId, decimal productAttributeValueSortOrder, int createdBy, DateTime createdAt,
        IPAddress createdByIp)
    {
        ValidatePositiveNumber(productAttributeId, DomainErrors.ProductAttributeErrors.InvalidProductAttributeId);
        ValidatePositiveNumber(productAttributeValueId, DomainErrors.ProductAttributeValueErrors.InvalidId);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ImageErrors.InvalidCreatedBy);

        ProductAttributeId = productAttributeId;
        ProductAttributeSortOrder = productAttributeSortOrder;
        ProductAttributeValueId = productAttributeValueId;
        ProductAttributeValueSortOrder = productAttributeValueSortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    internal void Delete(int deletedBy, DateTime deletedAt, IPAddress deletedByIp)
    {
        IDeletable.ValidateDeletedBy(deletedBy, DomainErrors.ProductAttributeErrors.InvalidDeletedBy);

        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    internal void UpdateValueId(int valueId, int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.ProductAttributeErrors.InvalidUpdatedBy);

        if (ProductAttributeValueId == valueId) return;
        
        ProductAttributeValueId = valueId;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;
    }
}