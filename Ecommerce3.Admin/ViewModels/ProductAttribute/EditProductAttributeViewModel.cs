using System.ComponentModel.DataAnnotations;
using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using DataType = Ecommerce3.Domain.Enums.DataType;

namespace Ecommerce3.Admin.ViewModels.ProductAttribute;

public sealed class EditProductAttributeViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Name)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Name))]
    public string Name { get; set; }

    [Required(ErrorMessage = $"{nameof(Slug)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Slug)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Slug))]
    [RegularExpression(@"^[a-z0-9]+(?:[-._~][a-z0-9]+)*$", ErrorMessage = "Invalid slug format.")]
    public string Slug { get; set; }

    [Required(ErrorMessage = $"{nameof(Display)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Display)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Display))]
    public string Display { get; set; }

    [Required(ErrorMessage = $"{nameof(Breadcrumb)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Breadcrumb)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Breadcrumb))]
    public string Breadcrumb { get; set; }

    [Required(ErrorMessage = "Sort order is required.")]
    [Display(Name = "Sort order")]
    public int SortOrder { get; set; }
    
    [Required(ErrorMessage = "Data type is required.")]
    public DataType DataType { get; set; }
    
    public IReadOnlyList<ProductAttributeValueDTO> Values { get; private set; } = [];

    public static EditProductAttributeViewModel FromDTO(ProductAttributeDTO dto)
    {
        return new EditProductAttributeViewModel()
        {
            Id = dto.Id,
            Name = dto.Name,
            Slug = dto.Slug,
            Display = dto.Display,
            Breadcrumb = dto.Breadcrumb,
            SortOrder = dto.SortOrder,
            DataType = dto.DataType,
            Values =  dto.Values,
        };
    }
}