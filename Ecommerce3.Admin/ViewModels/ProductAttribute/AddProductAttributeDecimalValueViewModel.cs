using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.ProductAttribute;

namespace Ecommerce3.Admin.ViewModels.ProductAttribute;

public class AddProductAttributeDecimalValueViewModel
{
    [Required(ErrorMessage = "Product attribute id is required.")]
    public int ProductAttributeId { get; set; }
    
    [Required(ErrorMessage = "Value is required.")]
    public decimal DecimalValue { get; set; }
    
    [Required(ErrorMessage = "Slug is required.")]
    public string Slug { get; set; }
    
    [Required(ErrorMessage = "Display is required.")]
    public string Display { get; set; }
    
    [Required(ErrorMessage = "Breadcrumb is required.")]
    public string Breadcrumb { get; set; }
    
    [Required(ErrorMessage = "Sort order is required.")]
    public int SortOrder { get; set; }
    
    public AddProductAttributeDecimalValueCommand ToCommand(int createdBy, DateTime createdAt, string createdByIp)
    {
        return new AddProductAttributeDecimalValueCommand
        {
            ProductAttributeId = ProductAttributeId,
            DecimalValue = DecimalValue,
            Slug = Slug,
            Display = Display,
            Breadcrumb = Breadcrumb,
            SortOrder = SortOrder,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}