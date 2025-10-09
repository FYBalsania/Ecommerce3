namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttributeColourValue : ProductAttributeValue
{
    public string? HexCode { get; private set; }
    public string ColourFamily { get; private set; }
    public string? ColourFamilyHexCode { get; private set; }

    public ProductAttributeColourValue(string value, string slug, string display, string breadcrumb, string? hexCode,
        string colourFamily, string? colourFamilyHexCode, int sortOrder, int createdBy, DateTime createdAt,
        string createdByIp)
        : base(value, slug, display, breadcrumb, sortOrder, createdBy, createdAt, createdByIp)
    {
        HexCode = hexCode;
        ColourFamily = colourFamily;
        ColourFamilyHexCode = colourFamilyHexCode;
    }
}