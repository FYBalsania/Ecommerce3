namespace Ecommerce3.Domain.Entities;

public sealed class ImageType : Entity, ICreatable, IUpdatable, IDeletable
{
    public string? Entity { get; private set; }
    public string Name { get; private set; }
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

    public ImageType(string? entity, string name, string? description, bool isActive, 
        int createdBy, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 128, nameof(name));

        Entity = entity;
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
    
    public bool Update(string? entity, string name, string? description, bool isActive,
        int updatedBy, string updatedByIp)
    {
        if (Entity == entity && Name == name && Description == description && IsActive == isActive)
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
}