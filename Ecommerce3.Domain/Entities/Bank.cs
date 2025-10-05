namespace Ecommerce3.Domain.Entities;

public sealed class Bank : Entity, ICreatable, IUpdatable, IDeletable
{
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public bool IsActive { get; private set; }
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

    private Bank(string name, string slug, bool isActive, int sortOrder, int createdBy, DateTime createdAt,
        string createdByIp)
    {
        Name = name;
        Slug = slug;
        IsActive = isActive;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }
}