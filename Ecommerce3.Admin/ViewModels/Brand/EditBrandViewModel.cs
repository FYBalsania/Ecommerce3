using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.Brand;
using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Contracts.DTOs.Image;

namespace Ecommerce3.Admin.ViewModels.Brand;

public class EditBrandViewModel
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

    [Required(ErrorMessage = "Anchor text is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Anchor text must be between 1 and 256 characters.")]
    [Display(Name = "Anchor text")]
    public string AnchorText { get; set; }

    [MaxLength(256, ErrorMessage = "Anchor title may be between 1 and 256 characters.")]
    [Display(Name = "Anchor title")]
    public string? AnchorTitle { get; set; }

    [Required(ErrorMessage = "Meta title is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Meta title must be between 1 and 256 characters.")]
    [Display(Name = "Meta title")]
    public string MetaTitle { get; set; }

    [MaxLength(1024, ErrorMessage = "Meta description may be between 1 and 1024 characters.")]
    [Display(Name = "Meta description")]
    public string? MetaDescription { get; set; }

    [MaxLength(1024, ErrorMessage = "Meta keywords may be between 1 and 1024 characters.")]
    [Display(Name = "Meta keywords")]
    public string? MetaKeywords { get; set; }

    [Required(ErrorMessage = $"{nameof(H1)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(H1)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(H1))]
    public string? H1 { get; set; }
    
    [StringLength(512, MinimumLength = 1, ErrorMessage = "Short description must be between 1 and 512 characters.")]
    [Display(Name = "Short description")]
    public string? ShortDescription { get; set; }

    [Display(Name = "Full description")]
    public string? FullDescription { get; set; }

    [Required(ErrorMessage = "Is active is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Sort order is required.")]
    [Display(Name = "Sort order")]
    public int SortOrder { get; set; }

    public IReadOnlyList<ImageDTO> Images { get; private set; } = [];

    public EditBrandCommand ToCommand(int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        return new EditBrandCommand()
        {
            Id = Id,
            Name = Name,
            Slug = Slug,
            Display = Display,
            Breadcrumb = Breadcrumb,
            AnchorText = AnchorText,
            AnchorTitle = AnchorTitle,
            MetaTitle = MetaTitle,
            MetaDescription = MetaDescription,
            MetaKeywords = MetaKeywords,
            H1 = H1,
            ShortDescription = ShortDescription,
            FullDescription = FullDescription,
            IsActive = IsActive,
            SortOrder = SortOrder,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp,
        };
    }

    public static EditBrandViewModel FromDTO(BrandDTO dto)
    {
        return new EditBrandViewModel()
        {
            Id = dto.Id,
            Name = dto.Name,
            Slug = dto.Slug,
            Display = dto.Display,
            Breadcrumb = dto.Breadcrumb,
            AnchorText = dto.AnchorText,
            AnchorTitle = dto.AnchorTitle,
            MetaTitle = dto.MetaTitle,
            MetaDescription = dto.MetaDescription,
            MetaKeywords = dto.MetaKeywords,
            H1 = dto.H1,
            ShortDescription = dto.ShortDescription,
            FullDescription = dto.FullDescription,
            IsActive = dto.IsActive,
            SortOrder = dto.SortOrder,
            Images = dto.Images
        };
    }
}