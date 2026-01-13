using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.ProductAttribute;

namespace Ecommerce3.Admin.ViewModels.ProductAttribute;

public record EditProductAttributeValueViewModel
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

    public EditProductAttributeValueCommand ToCommand(int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        return new EditProductAttributeValueCommand
        {
            Id = Id,
            ProductAttributeId = ProductAttributeId,
            Value = Value,
            Slug = Slug,
            Display = Display,
            Breadcrumb = Breadcrumb,
            SortOrder = SortOrder,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp,
        };
    }
}