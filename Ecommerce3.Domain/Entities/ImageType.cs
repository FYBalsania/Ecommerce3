namespace Ecommerce3.Domain.Entities;

public sealed class ImageType : Entity, ICreatable, IUpdatable, IDeletable
{
    public string Type { get; private set; }
    public string Entity { get; private set; }
    public string? Description { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
    
    private ImageType()
    {
    }

    public ImageType(string type, Entity entity, string? description, int createdBy, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(type, nameof(type));
        
        Type = type;
        Entity = nameof(entity);
        Description = description;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
}