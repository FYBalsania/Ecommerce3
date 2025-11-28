using System.ComponentModel.DataAnnotations;

namespace Ecommerce3.Application.Commands.ProductAttribute;

public record EditProductAttributeColourValueViewModel
{
    [Required(ErrorMessage = "Id is required.")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Product attribute id is required.")]
    public int ProductAttributeId { get; set; }
    
    [Required(ErrorMessage = "Value is required.")]
    public string Value { get; set; }
    
    [Required(ErrorMessage = "Slug is required.")]
    public string Slug { get; set; }
    
    [Required(ErrorMessage = "Display is required.")]
    public string Display { get; set; }
    
    [Required(ErrorMessage = "Breadcrumb is required.")]
    public string Breadcrumb { get; set; }
    
    [Required(ErrorMessage = "Sort order is required.")]
    public int SortOrder { get; set; }
    
    public string? HexCode { get; set; }
    
    [Required(ErrorMessage = "Colour family is required.")]
    public string ColourFamily { get; set; }
    
    public string? ColourFamilyHexCode { get; set; }
    
    public EditProductAttributeColourValueCommand ToCommand(int updatedBy, string updatedByIp)
    {
        return new EditProductAttributeColourValueCommand
        {
            Id = Id,
            ProductAttributeId = ProductAttributeId,
            Value = Value,
            Slug = Slug,
            Display = Display,
            Breadcrumb = Breadcrumb,
            SortOrder = SortOrder,
            HexCode = HexCode,
            ColourFamily = ColourFamily,
            ColourFamilyHexCode = ColourFamilyHexCode,
            UpdatedBy = updatedBy,
            UpdatedAt = DateTime.Now,
            UpdatedByIp = updatedByIp,
        };
    }
}