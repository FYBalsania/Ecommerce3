namespace Ecommerce3.Contracts.DTOs;

public record ProductAttributeColourValueDTO : ProductAttributeValueDTO
{
    public string? HexCode { get; private set; }
    public string ColourFamily { get; private set; }
    public string? ColourFamilyHexCode { get; private set; }

    public ProductAttributeColourValueDTO(int id, string value, string slug, string display, string breadcrumb,
        decimal sortOrder, string createdUserFullName, DateTime createdAt, string? hexCode, string colourFamily,
        string? colourFamilyHexCode)
        : base(id, value, slug, display, breadcrumb, sortOrder, createdUserFullName, createdAt)
    {
        HexCode = hexCode;
        ColourFamily = colourFamily;
        ColourFamilyHexCode = colourFamilyHexCode;
    }
}