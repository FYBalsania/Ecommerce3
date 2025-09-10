namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttributeColourValue : ProductAttributeValue
{
    public string? HexCode { get; private set; }
    public string ColourFamily { get; private set; }
    public string? ColourFamilyHexCode { get; private set; }

    private ProductAttributeColourValue() : base()
    {
    }

    public ProductAttributeColourValue(string value, string slug, string display, string breadcrumb,
        decimal? numberValue,
        bool? booleanValue, DateOnly? dateOnlyValue, string? hexCode, string colourFamily, string? colourFamilyHexCode,
        int sortOrder, int createdBy, string createdByIp)
        : base(value, slug, display, breadcrumb, numberValue, booleanValue, dateOnlyValue, sortOrder, createdBy,
            createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(colourFamily, nameof(colourFamily));

        HexCode = hexCode;
        ColourFamily = colourFamily;
        ColourFamilyHexCode = colourFamilyHexCode;
    }
}