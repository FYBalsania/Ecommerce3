using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;

namespace Ecommerce3.Domain.Entities;

public sealed class UnitOfMeasure : Entity, ICreatable, IUpdatable, IDeletable
{
    public static readonly int CodeMaxLength = 32;
    public static readonly int NameMaxLength = 128;
    public static readonly int TypeMaxLength = 16;

    public string Code { get; private set; }
    public string Name { get; private set; }
    public UnitOfMeasureType Type { get; private set; }
    public int? BaseId { get; private set; }
    public UnitOfMeasure? Base { get; private set; }
    public decimal ConversionFactor { get; private set; }
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

    private UnitOfMeasure()
    {
    }

    public UnitOfMeasure(string code, string name, UnitOfMeasureType type, int? baseId, decimal conversionFactor,
        bool isActive, int createdBy, DateTime createdAt, string createdByIp)
    {
        ValidateRequiredAndTooLong(code, CodeMaxLength, DomainErrors.UnitOfMeasureErrors.CodeRequired,
            DomainErrors.UnitOfMeasureErrors.CodeTooLong);
        ValidateRequiredAndTooLong(name, NameMaxLength, DomainErrors.UnitOfMeasureErrors.NameRequired,
            DomainErrors.UnitOfMeasureErrors.NameTooLong);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.UnitOfMeasureErrors.InvalidCreatedBy);
        ICreatable.ValidateCreatedByIp(createdByIp, DomainErrors.UnitOfMeasureErrors.CreatedByIpRequired,
            DomainErrors.UnitOfMeasureErrors.CreatedByIpTooLong);

        Code = code;
        Name = name;
        Type = type;
        BaseId = baseId;
        ConversionFactor = conversionFactor;
        IsActive = isActive;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public bool Update(string code, string name, UnitOfMeasureType type, int? baseId, decimal conversionFactor,
        bool isActive,
        int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        ValidateRequiredAndTooLong(code, CodeMaxLength, DomainErrors.UnitOfMeasureErrors.CodeRequired,
            DomainErrors.UnitOfMeasureErrors.CodeTooLong);
        ValidateRequiredAndTooLong(name, NameMaxLength, DomainErrors.UnitOfMeasureErrors.NameRequired,
            DomainErrors.UnitOfMeasureErrors.NameTooLong);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.UnitOfMeasureErrors.InvalidUpdatedBy);
        IUpdatable.ValidateUpdatedByIp(updatedByIp, DomainErrors.UnitOfMeasureErrors.UpdatedByIpRequired,
            DomainErrors.UnitOfMeasureErrors.UpdatedByIpTooLong);

        if (Code == code && Name == name && Type == type && BaseId == baseId && ConversionFactor == conversionFactor &&
            IsActive == isActive) return false;

        Code = code;
        Name = name;
        Type = type;
        BaseId = baseId;
        ConversionFactor = conversionFactor;
        IsActive = isActive;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;

        return true;
    }

    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        IDeletable.ValidateDeletedBy(deletedBy, DomainErrors.UnitOfMeasureErrors.InvalidDeletedBy);
        IDeletable.ValidateDeletedByIp(deletedByIp, DomainErrors.UnitOfMeasureErrors.DeletedByIpRequired,
            DomainErrors.UnitOfMeasureErrors.DeletedByIpTooLong);

        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
}