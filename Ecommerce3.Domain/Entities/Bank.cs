using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class Bank : EntityWithImages<BankImage>, ICreatable, IUpdatable, IDeletable
{
    public override string ImageNamePrefix => Slug;
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
        ValidateName(name);
        ValidateSlug(slug);
        ValidateCreatedBy(createdBy);
        ValidateCreatedByIp(createdByIp);

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
        ValidateName(name);
        ValidateSlug(slug);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);
        
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
    
    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException(DomainErrors.BankErrors.NameRequired);
        if (name.Length > 256) throw new DomainException(DomainErrors.BankErrors.NameTooLong);
    }
    
    private static void ValidateSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug)) throw new DomainException(DomainErrors.BankErrors.SlugRequired);
        if (slug.Length > 256) throw new DomainException(DomainErrors.BankErrors.SlugTooLong);
    }

    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.BankErrors.InvalidCreatedBy);
    }
    
    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new DomainException(DomainErrors.BankErrors.CreatedByIpRequired);
        if (createdByIp.Length > 128) throw new DomainException(DomainErrors.BankErrors.CreatedByIpTooLong);
    }
    
    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.BankErrors.InvalidUpdatedBy);
    }
    
    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp)) throw new DomainException(DomainErrors.BankErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > 128) throw new DomainException(DomainErrors.BankErrors.UpdatedByIpTooLong);
    }
}