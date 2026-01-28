using System.Net;
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
    public string SingularName { get; private set; }
    public string PluralName { get; private set; }
    public UnitOfMeasureType Type { get; private set; }
    public int? BaseId { get; private set; }
    public UnitOfMeasure? Base { get; private set; }
    public decimal ConversionFactor { get; private set; }
    public byte DecimalPlaces { get; private set; }
    public bool IsActive { get; private set; }
    public decimal SortOrder { get; private set; }
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

    private UnitOfMeasure()
    {
    }

    public UnitOfMeasure(string code, string singularName, string pluralName, UnitOfMeasureType type, int? baseId,
        decimal conversionFactor, byte decimalPlaces, bool isActive, decimal sortOrder, int createdBy,
        DateTime createdAt, IPAddress createdByIp)
    {
        //Code.
        ValidateRequiredAndTooLong(code, CodeMaxLength, DomainErrors.UnitOfMeasureErrors.CodeRequired,
            DomainErrors.UnitOfMeasureErrors.CodeTooLong);
        //Singular name.
        ValidateRequiredAndTooLong(singularName, NameMaxLength,
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(SingularName)}", "Singular name is required."),
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(SingularName)}",
                $"Singular name cannot exceed {NameMaxLength} characters."));
        //Plural name.
        ValidateRequiredAndTooLong(pluralName, NameMaxLength,
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(PluralName)}", "Plural name is required."),
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(PluralName)}",
                $"Plural name cannot exceed {NameMaxLength} characters."));
        //Base Id.
        if (baseId <= 0)
            throw new DomainException(new DomainError($"{nameof(UnitOfMeasure)}.{nameof(BaseId)}",
                "Base Id must be greater than 0."));
        //Conversion factor.
        if (baseId is null && conversionFactor != 1)
            throw new DomainException(new DomainError($"{nameof(UnitOfMeasure)}.{nameof(ConversionFactor)}",
                "Must have conversion factor as 1 if base is not set."));
        if (conversionFactor <= 0)
            throw new DomainException(new DomainError($"{nameof(UnitOfMeasure)}.{nameof(ConversionFactor)}",
                "Conversion factor must be greater than 0."));
        //Decimal places.
        if (decimalPlaces > 3)
            throw new DomainException(new DomainError($"{nameof(UnitOfMeasure)}.{nameof(DecimalPlaces)}",
                "Decimal places must be less than 4."));
        //Created.
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.UnitOfMeasureErrors.InvalidCreatedBy);
        ICreatable.ValidateCreatedAt(createdAt,
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(CreatedAt)}", "Invalid created at."));
        ICreatable.ValidateCreatedByIp(createdByIp,
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(CreatedByIp)}", "Invalid created by IP."));

        Code = code;
        SingularName = singularName;
        PluralName = pluralName;
        Type = type;
        BaseId = baseId;
        ConversionFactor = conversionFactor;
        DecimalPlaces = decimalPlaces;
        IsActive = isActive;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public void Update(string code, string singularName, string pluralName, UnitOfMeasureType type,
        int? baseId, decimal conversionFactor, byte decimalPlaces, bool isActive, decimal sortOrder, int updatedBy,
        DateTime updatedAt, IPAddress updatedByIp)
    {
        //Code.
        ValidateRequiredAndTooLong(code, CodeMaxLength, DomainErrors.UnitOfMeasureErrors.CodeRequired,
            DomainErrors.UnitOfMeasureErrors.CodeTooLong);
        //Singular name.
        ValidateRequiredAndTooLong(singularName, NameMaxLength,
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(SingularName)}", "Singular name is required."),
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(SingularName)}",
                $"Singular name cannot exceed {NameMaxLength} characters."));
        //Plural name.
        ValidateRequiredAndTooLong(pluralName, NameMaxLength,
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(PluralName)}", "Plural name is required."),
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(PluralName)}",
                $"Plural name cannot exceed {NameMaxLength} characters."));
        //Base Id.
        if (baseId <= 0)
            throw new DomainException(new DomainError($"{nameof(UnitOfMeasure)}.{nameof(BaseId)}",
                "Base Id must be greater than 0."));
        //Conversion factor.
        if (baseId is null && conversionFactor != 1)
            throw new DomainException(new DomainError($"{nameof(UnitOfMeasure)}.{nameof(ConversionFactor)}",
                "Must have conversion factor as 1 if base is not set."));
        if (conversionFactor <= 0)
            throw new DomainException(new DomainError($"{nameof(UnitOfMeasure)}.{nameof(ConversionFactor)}",
                "Conversion factor must be greater than 0."));
        //Decimal places.
        if (decimalPlaces > 3)
            throw new DomainException(new DomainError($"{nameof(UnitOfMeasure)}.{nameof(DecimalPlaces)}",
                "Decimal places must be less than 4."));
        //Updated.
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.UnitOfMeasureErrors.InvalidUpdatedBy);
        IUpdatable.ValidateUpdatedAt(updatedAt,
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(UpdatedAt)}", "Invalid updated at."));
        IUpdatable.ValidateUpdatedByIp(updatedByIp,
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(UpdatedByIp)}", "Invalid updated by IP."));

        if (Code == code && SingularName == singularName && PluralName == pluralName && Type == type &&
            BaseId == baseId && ConversionFactor == conversionFactor && DecimalPlaces == decimalPlaces &&
            IsActive == isActive && SortOrder == sortOrder) return;

        Code = code;
        SingularName = singularName;
        PluralName = pluralName;
        Type = type;
        BaseId = baseId;
        ConversionFactor = conversionFactor;
        DecimalPlaces = decimalPlaces;
        IsActive = isActive;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;
    }

    public void Delete(int deletedBy, DateTime deletedAt, IPAddress deletedByIp)
    {
        IDeletable.ValidateDeletedBy(deletedBy, DomainErrors.UnitOfMeasureErrors.InvalidDeletedBy);
        IDeletable.ValidateDeletedAt(deletedAt,
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(DeletedAt)}", "Invalid deleted at"));
        IDeletable.ValidateDeletedByIp(deletedByIp,
            new DomainError($"{nameof(UnitOfMeasure)}.{nameof(DeletedByIp)}", "Invalid deleted by IP"));

        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
}