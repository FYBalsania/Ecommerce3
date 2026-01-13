using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.ProductAttribute;

namespace Ecommerce3.Admin.ViewModels.ProductAttribute;

public record DeleteProductAttributeValueViewModel
{
    [Required(ErrorMessage = "Product attribute value id is required.")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Product attribute id is required.")]
    public int ProductAttributeId { get; set; }

    public DeleteProductAttributeValueCommand ToCommand(int deletedBy, DateTime deletedAt, IPAddress deletedByIp)
    {
        return new DeleteProductAttributeValueCommand
        {
            Id = Id,
            ProductAttributeId = ProductAttributeId,
            DeletedBy = deletedBy,
            DeletedByIp = deletedByIp,
            DeletedAt = deletedAt
        };
    }
}