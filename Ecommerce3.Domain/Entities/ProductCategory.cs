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
        CategoryId = categoryId;
        IsPrimary = isPrimary;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
}