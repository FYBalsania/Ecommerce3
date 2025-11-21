using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class ImageType : Entity, ICreatable, IUpdatable, IDeletable
{
    public string? Entity { get; private set; }
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
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

    private ImageType()
    {
    }

    public ImageType(string? entity, string name, string slug, string? description, bool isActive,
        int createdBy, string createdByIp)
    {
        ValidateName(name);
        ValidateSlug(slug);
        ValidateDescription(description);
        ValidateCreatedBy(createdBy);
        ValidateCreatedByIp(createdByIp);

        Entity = entity;
        Name = name;
        Slug = slug;
        Description = description;
        IsActive = isActive;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }

    public bool Update(string? entity, string name, string slug, string? description, bool isActive,
        int updatedBy, string updatedByIp)
    {
        ValidateName(name);
        ValidateSlug(slug);
        ValidateDescription(description);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);
        
        if (Entity == entity && Name == name && Slug == slug && Description == description && IsActive == isActive)
            return false;

        Entity = entity;
        Name = name;
        Description = description;
        IsActive = isActive;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;

        return true;
    }
    
    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
    
    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException(DomainErrors.ImageTypeErrors.NameRequired);
        if (name.Length > 256) throw new DomainException(DomainErrors.ImageTypeErrors.NameTooLong);
    }
    
    private static void ValidateSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug)) throw new DomainException(DomainErrors.ImageTypeErrors.SlugRequired);
        if (slug.Length > 256) throw new DomainException(DomainErrors.ImageTypeErrors.SlugTooLong);
    }

    private static void ValidateDescription(string? description)
    {
        if (description is not null && description.Length > 1024)
            throw new DomainException(DomainErrors.ImageTypeErrors.DescriptionTooLong);
    }
    
    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.ImageTypeErrors.InvalidCreatedBy);
    }
    
    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new DomainException(DomainErrors.ImageTypeErrors.CreatedByIpRequired);
        if (createdByIp.Length > 128) throw new DomainException(DomainErrors.ImageTypeErrors.CreatedByIpTooLong);
    }
    
    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.ImageTypeErrors.InvalidUpdatedBy);
    }
    
    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp)) throw new DomainException(DomainErrors.ImageTypeErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > 128) throw new DomainException(DomainErrors.ImageTypeErrors.UpdatedByIpTooLong);
    }
}