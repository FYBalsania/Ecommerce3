using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

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
        ValidateCode(code);
        ValidateCreatedBy(createdBy);
        ValidateCreatedByIp(createdByIp);
        
        Code = code;
        IsActive = isActive;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
    
    public bool Update(string code, bool isActive, int updatedBy, string updatedByIp)
    {
        ValidateCode(code);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);
        
        if (Code == code && IsActive == isActive)
            return false;

        Code = code;
        IsActive = isActive;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;

        return true;
    }
    
    private static void ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new DomainException(DomainErrors.PostCodeErrors.CodeRequired);
        if (code.Length > 16) throw new DomainException(DomainErrors.PostCodeErrors.CodeTooLong);
    }

    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.PostCodeErrors.InvalidCreatedBy);
    }
    
    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new DomainException(DomainErrors.PostCodeErrors.CreatedByIpRequired);
        if (createdByIp.Length > 128) throw new DomainException(DomainErrors.PostCodeErrors.CreatedByIpTooLong);
    }
    
    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.PostCodeErrors.InvalidUpdatedBy);
    }
    
    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp)) throw new DomainException(DomainErrors.PostCodeErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > 128) throw new DomainException(DomainErrors.PostCodeErrors.UpdatedByIpTooLong);
    }
    
    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
}