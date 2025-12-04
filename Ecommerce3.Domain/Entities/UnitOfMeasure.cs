using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

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
        ValidateCode(code);
        ValidateName(name);
        ValidateCreatedBy(createdBy);
        ValidateCreatedByIp(createdByIp);

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
        ValidateCode(code);
        ValidateName(name);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);

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
        ValidateDeletedBy(deletedBy);
        ValidateDeletedByIp(deletedByIp);

        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    private static void ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new DomainException(DomainErrors.UnitOfMeasureErrors.CodeRequired);
        if (code.Length > CodeMaxLength) throw new DomainException(DomainErrors.UnitOfMeasureErrors.CodeTooLong);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException(DomainErrors.UnitOfMeasureErrors.NameRequired);
        if (name.Length > NameMaxLength) throw new DomainException(DomainErrors.UnitOfMeasureErrors.NameTooLong);
    }

    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.UnitOfMeasureErrors.InvalidCreatedBy);
    }

    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new DomainException(DomainErrors.UnitOfMeasureErrors.CreatedByIpRequired);
        if (createdByIp.Length > ICreatable.CreatedByIpMaxLength)
            throw new DomainException(DomainErrors.UnitOfMeasureErrors.CreatedByIpTooLong);
    }

    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.UnitOfMeasureErrors.InvalidUpdatedBy);
    }

    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp))
            throw new DomainException(DomainErrors.UnitOfMeasureErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > IUpdatable.UpdatedByIpMaxLength)
            throw new DomainException(DomainErrors.UnitOfMeasureErrors.UpdatedByIpTooLong);
    }

    private static void ValidateDeletedBy(int deletedBy)
    {
        if (deletedBy <= 0) throw new DomainException(DomainErrors.UnitOfMeasureErrors.InvalidDeletedBy);
    }

    private static void ValidateDeletedByIp(string deletedByIp)
    {
        if (string.IsNullOrWhiteSpace(deletedByIp))
            throw new DomainException(DomainErrors.UnitOfMeasureErrors.DeletedByIpRequired);
        if (deletedByIp.Length > IDeletable.DeletedByIpMaxLength)
            throw new DomainException(DomainErrors.UnitOfMeasureErrors.DeletedByIpTooLong);
    }
}