using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.ProductAttribute;

namespace Ecommerce3.Admin.ViewModels.ProductAttribute;

public record EditProductAttributeDecimalValueViewModel
{
    [Required(ErrorMessage = "Id is required.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Product attribute id is required.")]
    public int ProductAttributeId { get; set; }

    [Required(ErrorMessage = "Decimal value is required.")]
    public decimal DecimalValue { get; set; }

    [Required(ErrorMessage = "Slug is required.")]
    public string Slug { get; set; }

    [Required(ErrorMessage = "Display is required.")]
    public string Display { get; set; }

    [Required(ErrorMessage = "Breadcrumb is required.")]
    public string Breadcrumb { get; set; }

    [Required(ErrorMessage = "Sort order is required.")]
    public int SortOrder { get; set; }

    public EditProductAttributeDecimalValueCommand ToCommand(int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        return new EditProductAttributeDecimalValueCommand
        {
            Id = Id,
            ProductAttributeId = ProductAttributeId,
            Slug = Slug,
            Display = Display,
            Breadcrumb = Breadcrumb,
            SortOrder = SortOrder,
            DecimalValue = DecimalValue,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp,
        };
    }
}