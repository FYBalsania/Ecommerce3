using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.ProductGroup;

namespace Ecommerce3.Admin.ViewModels.ProductGroup;

public record AddProductGroupAttributeViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Product group id is required.")]
    public required int ProductGroupId { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Product attribute id is required.")]
    public required int ProductAttributeId { get; init; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Product attribute sort order is required.")]
    public required decimal ProductAttributeSortOrder { get; init; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Value is required.")]
    public required IDictionary<int, decimal> Values { get; init; }

    public AddProductGroupProductAttributeCommand ToCommand(int createdBy, DateTime createdAt, string createdByIp)
    {
        return new AddProductGroupProductAttributeCommand
        {
            ProductGroupId = ProductGroupId,
            ProductAttributeId = ProductAttributeId,
            SortOrder = ProductAttributeSortOrder,
            Values = Values,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp
        };
    }
}