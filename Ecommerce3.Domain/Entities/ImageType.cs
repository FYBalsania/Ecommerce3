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
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ImageTypeErrors.InvalidCreatedBy);
        ICreatable.ValidateCreatedByIp(createdByIp, DomainErrors.ImageTypeErrors.CreatedByIpRequired, DomainErrors.ImageTypeErrors.CreatedByIpTooLong);

        Entity = entity;
        Name = name;
        Slug = slug;
        Description = description;
        IsActive = isActive;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }

    public void Update(string? entity, string name, string slug, string? description, bool isActive,
        int updatedBy, string updatedByIp)
    {
        ValidateName(name);
        ValidateSlug(slug);
        ValidateDescription(description);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.ImageTypeErrors.InvalidUpdatedBy);
        IUpdatable.ValidateUpdatedByIp(updatedByIp, DomainErrors.ImageTypeErrors.UpdatedByIpRequired, DomainErrors.ImageTypeErrors.UpdatedByIpTooLong);
        
        if (Entity == entity && Name == name && Slug == slug && Description == description && IsActive == isActive)
            return;

        Entity = entity;
        Name = name;
        Description = description;
        IsActive = isActive;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;
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
}