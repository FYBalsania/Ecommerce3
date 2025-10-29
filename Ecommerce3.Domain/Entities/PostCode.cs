namespace Ecommerce3.Domain.Entities;

public sealed class PostCode : Entity, ICreatable, IUpdatable, IDeletable
{
    public string Code { get; private set; }
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
    
    public PostCode(string code, bool isActive, int createdBy, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code, nameof(code));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(code.Length, 16, nameof(code));
        
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));
        
        Code = code;
        IsActive = isActive;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
    
    public bool Update(string code, bool isActive, int updatedBy, string updatedByIp)
    {
        if (Code == code && IsActive == isActive)
            return false;

        Code = code;
        IsActive = isActive;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;

        return true;
    }
}