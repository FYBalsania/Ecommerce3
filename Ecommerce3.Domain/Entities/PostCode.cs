using System.Net;
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
    public IPAddress CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IPAddress? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public IPAddress? DeletedByIp { get; private set; }
    
    public PostCode(string code, bool isActive, int createdBy, IPAddress createdByIp)
    {
        ValidateCode(code);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.PostCodeErrors.InvalidCreatedBy);
        
        Code = code;
        IsActive = isActive;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
    
    public void Update(string code, bool isActive, int updatedBy, IPAddress updatedByIp)
    {
        ValidateCode(code);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.PostCodeErrors.InvalidUpdatedBy);
        
        if (Code == code && IsActive == isActive)
            return;

        Code = code;
        IsActive = isActive;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;
    }
    
    private static void ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new DomainException(DomainErrors.PostCodeErrors.CodeRequired);
        if (code.Length > 16) throw new DomainException(DomainErrors.PostCodeErrors.CodeTooLong);
    }
}