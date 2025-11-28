using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.ProductAttribute;

namespace Ecommerce3.Admin.ViewModels.ProductAttribute;

public class AddProductAttributeDateOnlyValueViewModel
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
    
    [Required(ErrorMessage = "Dateonly value is required.")]
    public DateOnly DateOnlyValue { get; set; }
    
    public AddProductAttributeDateOnlyValueCommand ToCommand(int createdBy, DateTime createdAt, string createdByIp)
    {
        return new AddProductAttributeDateOnlyValueCommand
        {
            ProductAttributeId = ProductAttributeId,
            Discriminator = Discriminator,
            Value = Value,
            Slug = Slug,
            Display = Display,
            Breadcrumb = Breadcrumb,
            SortOrder = SortOrder,
            DateOnlyValue = DateOnlyValue,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}