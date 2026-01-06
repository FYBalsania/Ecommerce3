using Ecommerce3.Domain.Errors;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductCategory : Entity, ICreatable, IUpdatable, IDeletable
{
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }
    public int CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public bool IsPrimary { get; private set; }
    public int SortOrder { get; private set; }
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

    private ProductCategory()
    {
    }

    internal ProductCategory(int categoryId, bool isPrimary, int sortOrder, int createdBy, DateTime createdAt,
        string createdByIp)
    {
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ProductCategoryErrors.InvalidCreatedBy);
        ICreatable.ValidateCreatedByIp(createdByIp, DomainErrors.ProductCategoryErrors.CreatedByIpRequired, 
            DomainErrors.PageErrors.CreatedByIpTooLong);
        
        CategoryId = categoryId;
        IsPrimary = isPrimary;
        SortOrder = sortOrder;
        
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    internal void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        IDeletable.ValidateDeletedBy(deletedBy, DomainErrors.ProductCategoryErrors.InvalidDeletedBy);
        IDeletable.ValidateDeletedByIp(deletedByIp, DomainErrors.ProductCategoryErrors.DeletedByIpRequired, 
            DomainErrors.ProductCategoryErrors.DeletedByIpTooLong);
        
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    internal void Update(bool isPrimary, int sortOrder, int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.ProductCategoryErrors.InvalidUpdatedBy);
        IUpdatable.ValidateUpdatedByIp(updatedByIp, DomainErrors.ProductCategoryErrors.UpdatedByIpRequired, 
            DomainErrors.KVPListItemErrors.UpdatedByIpTooLong);
        
        if (IsPrimary == isPrimary && SortOrder == sortOrder) return;
        
        IsPrimary = isPrimary;
        SortOrder = sortOrder;
        
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;
    }
}