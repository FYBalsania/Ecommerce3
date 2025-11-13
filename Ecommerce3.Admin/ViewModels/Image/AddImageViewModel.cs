using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Admin.ViewModels.Image;

public class AddImageViewModel
{
    [Required(AllowEmptyStrings = false)] public string ParentEntityType { get; set; } //Brand, Category, Product etc.

    [Required(AllowEmptyStrings = false)]
    public string ParentEntityId { get; set; } //BrandId, CategoryId, ProductId etc.

    [Required(AllowEmptyStrings = false)]
    public string ImageEntityType { get; set; } //BrandImage, CategoryImage, ProductImage etc.

    [Required(ErrorMessage = "Image type is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Image type is invalid.")]
    public int ImageTypeId { get; set; }

    [Required(ErrorMessage = "Image size is required.")]
    public ImageSize ImageSize { get; set; }

    [Required(ErrorMessage = "Image is required.")]
    public IFormFile File { get; set; }

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

    public AddImageCommand ToCommand(string parentEntityType, string parentEntityId, string imageEntityType,
        byte[] file, int maxFileSizeKb, string fileName, string tempPath, string path, int createdBy,
        DateTime createdAt, string createdByIp)
    {
        return new AddImageCommand
        {
            ParentEntityType = parentEntityType,
            ParentEntityId = parentEntityId,
            ImageEntityType = imageEntityType,
            ImageTypeId = ImageTypeId,
            ImageSize = ImageSize,
            File = file,
            MaxFileSizeKb = maxFileSizeKb,
            FileName = fileName,
            TempImageFolderPath = tempPath,
            ImageFolderPath = path,
            AltText = AltText,
            Title = Title,
            Loading = Loading,
            Link = Link,
            LinkTarget = LinkTarget,
            SortOrder = SortOrder,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}