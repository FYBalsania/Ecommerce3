using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.Image;

namespace Ecommerce3.Admin.ViewModels.Image;

public record DeleteImageViewModel
{
    [Required(ErrorMessage = "Image id is required.")]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string ParentEntityId { get; set; } //BrandId, CategoryId, ProductId etc.

    [Required(AllowEmptyStrings = false)]
    public string ImageEntityType { get; set; } //BrandImage, CategoryImage, ProductImage etc.

    public DeleteImageCommand ToCommand(int userId, string ipAddress)
    {
        return new DeleteImageCommand
        {
            Id = Id,
            DeletedBy = userId,
            DeletedByIp = ipAddress,
            DeletedAt = DateTime.Now
        };
    }
}