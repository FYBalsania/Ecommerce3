namespace Ecommerce3.Domain.Entities;

public sealed class Bank : EntityWithImages<BankImage>, ICreatable, IUpdatable, IDeletable
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
    public BankPage? Page { get; private set; }

    public Bank(string name, string slug, bool isActive, int sortOrder, int createdBy, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 256, nameof(name));

        ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));

        Name = name;
        Slug = slug;
        IsActive = isActive;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }

    public bool Update(string name, string slug, bool isActive, int sortOrder, int updatedBy, string updatedByIp)
    {
        if (Name == name && Slug == slug && IsActive == isActive && SortOrder == sortOrder)
            return false;

        Name = name;
        Slug = slug;
        IsActive = isActive;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;

        return true;
    }
}