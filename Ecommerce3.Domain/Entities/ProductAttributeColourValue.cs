using System.Net;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttributeColourValue : ProductAttributeValue
{
    public string? HexCode { get; private set; }
    public string ColourFamily { get; private set; }
    public string? ColourFamilyHexCode { get; private set; }

    private ProductAttributeColourValue() : base()
    {
    }

    public ProductAttributeColourValue(string value, string slug, string display, string breadcrumb, string? hexCode,
        string colourFamily, string? colourFamilyHexCode, int sortOrder, int createdBy, DateTime createdAt,
        IPAddress createdByIp)
        : base(value, slug, display, breadcrumb, sortOrder, createdBy, createdAt, createdByIp)
    {
        if (hexCode is not null) ValidateHexCode(hexCode);
        ValidateColourFamily(colourFamily);
        if (colourFamilyHexCode is not null) ValidateColourFamilyHexCode(colourFamilyHexCode);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ProductAttributeColourValueErrors.InvalidCreatedBy);

        HexCode = hexCode;
        ColourFamily = colourFamily;
        ColourFamilyHexCode = colourFamilyHexCode;
    }

    internal bool Update(string value, string slug, string display, string breadcrumb, int sortOrder,
        string? hexCode, string colourFamily, string? colourFamilyHexCode, int updatedBy, DateTime updatedAt,
        IPAddress updatedByIp)
    {
        if (hexCode is not null) ValidateHexCode(hexCode);
        ValidateColourFamily(colourFamily);
        if (colourFamilyHexCode is not null) ValidateColourFamilyHexCode(colourFamilyHexCode);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.ProductAttributeColourValueErrors.InvalidUpdatedBy);

        if (!base.Update(value, slug, display, breadcrumb, sortOrder, updatedBy, updatedAt, updatedByIp) &&
            HexCode == hexCode && ColourFamily == colourFamily && ColourFamilyHexCode == colourFamilyHexCode)
            return false;
        
        HexCode = hexCode;
        ColourFamily = colourFamily;
        ColourFamilyHexCode = colourFamilyHexCode;

        return true;
    }


    private static void ValidateColourFamilyHexCode(string colourFamilyHexCode)
    {
        if (colourFamilyHexCode.Length > 8)
            throw new DomainException(DomainErrors.ProductAttributeColourValueErrors.ColourFamilyHexCodeTooLong);
    }

    private static void ValidateColourFamily(string colourFamily)
    {
        if (string.IsNullOrWhiteSpace(colourFamily))
            throw new DomainException(DomainErrors.ProductAttributeColourValueErrors.ColourFamilyRequired);
        if (colourFamily.Length > 64)
            throw new DomainException(DomainErrors.ProductAttributeColourValueErrors.ColourFamilyTooLong);
    }

    private static void ValidateHexCode(string hexcode)
    {
        if (hexcode.Length > 8)
            throw new DomainException(DomainErrors.ProductAttributeColourValueErrors.HexCodeTooLong);
    }
}