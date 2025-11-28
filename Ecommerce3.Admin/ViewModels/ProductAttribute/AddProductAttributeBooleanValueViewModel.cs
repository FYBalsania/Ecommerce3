using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.ProductAttribute;

namespace Ecommerce3.Admin.ViewModels.ProductAttribute;

public class AddProductAttributeBooleanValueViewModel
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
    
    [Required(ErrorMessage = "Boolean value is required.")]
    public bool BooleanValue { get; set; }
    
    public AddProductAttributeBooleanValueCommand ToCommand(int createdBy, DateTime createdAt, string createdByIp)
    {
        return new AddProductAttributeBooleanValueCommand
        {
            ProductAttributeId = ProductAttributeId,
            Discriminator = Discriminator,
            Value = Value,
            Slug = Slug,
            Display = Display,
            Breadcrumb = Breadcrumb,
            SortOrder = SortOrder,
            BooleanValue = BooleanValue,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}