using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.ViewModels.Image;

public record EditImageViewModel()
{
    [Required(AllowEmptyStrings = false)]
    public string ParentEntityId { get; set; } //BrandId, CategoryId, ProductId etc.

    [Required(AllowEmptyStrings = false)]
    public string ImageEntityType { get; set; } //BrandImage, CategoryImage, ProductImage etc.
    
    public int Id { get; set; }
    public SelectList ImageTypes { get; init; }

    [Required(ErrorMessage = "Image type is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Image type is invalid.")]
    public int ImageTypeId { get; set; }

    [Required(ErrorMessage = "Image size is required.")]
    public ImageSize ImageSize { get; set; }

    public string? AltText { get; set; }

    public string? Title { get; set; }

    [Required(ErrorMessage = "Loading is required.")]
    public ImageLoading Loading { get; set; }

    [MaxLength(256, ErrorMessage = "Link may be between 1 and 256 characters.")]
    public string? Link { get; set; }

    [MaxLength(8, ErrorMessage = "Link target may be between 1 and 8 characters.")]
    public string? LinkTarget { get; set; }

    [Required(ErrorMessage = "Sort order is required.")]
    public int SortOrder { get; set; }

    public EditImageCommand ToCommand(int updatedBy, string updatedByIp)
    {
        return new EditImageCommand
        {
            Id = Id,
            ImageTypeId = ImageTypeId,
            ImageSize = ImageSize,
            AltText = AltText,
            Title = Title,
            Loading = Loading,
            SortOrder = SortOrder,
            Link = Link,
            LinkTarget = LinkTarget,
            UpdatedBy = updatedBy,
            UpdatedAt = DateTime.Now,
            UpdatedByIp = updatedByIp,
        };
    }
}