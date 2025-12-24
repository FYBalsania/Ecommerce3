using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.Admin.ProductGroup;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewModels.ProductGroup;

public class EditProductGroupAttributeViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Product group id is required.")]
    public required int ProductGroupId { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Product attribute id is required.")]
    public required int ProductAttributeId { get; init; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Product attribute sort order is required.")]
    public required decimal ProductAttributeSortOrder { get; init; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Value is required.")]
    public required IDictionary<int, decimal> Values { get; init; }

    public EditProductGroupProductAttributesCommand ToCommand(int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        return new EditProductGroupProductAttributesCommand
        {
            ProductGroupId = ProductGroupId,
            ProductAttributeId = ProductAttributeId,
            ProductAttributeSortOrder = ProductAttributeSortOrder,
            Values = Values,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp
        };
    }
}