using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.ProductAttribute;

namespace Ecommerce3.Admin.ViewModels.ProductAttribute;

public class AddProductAttributeDecimalValueViewModel
{
    [Required(ErrorMessage = "Product attribute id is required.")]
    public int ProductAttributeId { get; set; }
    
    [Required(ErrorMessage = "Discriminator is required.")]
    public string Discriminator { get; set; }
    
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
    
    [Required(ErrorMessage = "Decimal value is required.")]
    public decimal DecimalValue { get; set; }
    
    public AddProductAttributeDecimalValueCommand ToCommand(int createdBy, DateTime createdAt, string createdByIp)
    {
        return new AddProductAttributeDecimalValueCommand
        {
            ProductAttributeId = ProductAttributeId,
            Discriminator = Discriminator,
            Value = Value,
            Slug = Slug,
            Display = Display,
            Breadcrumb = Breadcrumb,
            SortOrder = SortOrder,
            DecimalValue = DecimalValue,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}