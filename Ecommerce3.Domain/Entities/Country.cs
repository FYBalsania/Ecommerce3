using System.Net;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class Country : Entity //, ICreatable, IUpdatable, IDeletable
{
    public static readonly int NameMaxLength = 256;
    public static readonly int Iso2CodeMaxLength = 2;
    public static readonly int Iso3CodeMaxLength = 3;
    public static readonly int NumericCodeMaxLength = 3;

    public string Name { get; private set; }
    public string Iso2Code { get; private set; }
    public string Iso3Code { get; private set; }
    public string? NumericCode { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
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

    private Country()
    {
    }

    public Country(string name, string iso2Code, string iso3Code, string? numericCode, bool isActive, int sortOrder,
        int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        ValidateName(name);
        ValidateIso2Code(iso2Code);
        ValidateIso3Code(iso3Code);
        ValidateIsoNumericCode(numericCode!);
        
        Name = name;
        Iso2Code = iso2Code;
        Iso3Code = iso3Code;
        NumericCode = numericCode;
        IsActive = isActive;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public void Update(string name, string iso2Code, string iso3Code, string? numericCode, bool isActive, int sortOrder,
        int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        ValidateName(name);
        ValidateIso2Code(iso2Code);
        ValidateIso3Code(iso3Code);
        ValidateIsoNumericCode(numericCode!);
        
        if (Name == name && Iso2Code == iso2Code && Iso3Code == iso3Code && NumericCode == numericCode &&
            IsActive == isActive && SortOrder == sortOrder) return;

        Name = name;
        Iso2Code = iso2Code;
        Iso3Code = iso3Code;
        NumericCode = numericCode;
        IsActive = isActive;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException(DomainErrors.CountryErrors.NameRequired);
        if (name.Length > 256) throw new DomainException(DomainErrors.CountryErrors.NameTooLong);
    }
    
    private static void ValidateIso2Code(string iso2Code)
    {
        if (string.IsNullOrWhiteSpace(iso2Code)) throw new DomainException(DomainErrors.CountryErrors.Iso2CodeRequired);
        if (iso2Code.Length > 2) throw new DomainException(DomainErrors.CountryErrors.Iso2CodeTooLong);
    }
    
    private static void ValidateIso3Code(string iso3Code)
    {
        if (string.IsNullOrWhiteSpace(iso3Code)) throw new DomainException(DomainErrors.CountryErrors.Iso3CodeRequired);
        if (iso3Code.Length > 3) throw new DomainException(DomainErrors.CountryErrors.Iso3CodeTooLong);
    }
    
    private static void ValidateIsoNumericCode(string numericCode)
    {
        if (numericCode.Length > 3) throw new DomainException(DomainErrors.CountryErrors.NumericCodeTooLong);
    }
}