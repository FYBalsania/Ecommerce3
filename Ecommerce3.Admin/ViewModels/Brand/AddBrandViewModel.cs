using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.Brand;

namespace Ecommerce3.Admin.ViewModels.Brand;

public class AddBrandViewModel
{
    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Name)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Name))]
    public string Name { get; set; }

    [Required(ErrorMessage = $"{nameof(Slug)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Slug)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Slug))]
    public string Slug { get; set; }

    [Required(ErrorMessage = $"{nameof(Display)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Display)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Display))]
    public string Display { get; set; }

    [Required(ErrorMessage = $"{nameof(Breadcrumb)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Breadcrumb)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Breadcrumb))]
    public string Breadcrumb { get; set; }

    [Required(ErrorMessage = $"{nameof(AnchorText)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(AnchorText)} must be between 1 and 256 characters.")]
    [Display(Name = "Anchor text")]
    public string AnchorText { get; set; }

    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(AnchorTitle)} must be between 1 and 256 characters.")]
    [Display(Name = "Anchor title")]
    public string? AnchorTitle { get; set; }

    [Required(ErrorMessage = $"{nameof(MetaTitle)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(MetaTitle)} must be between 1 and 256 characters.")]
    [Display(Name = "Meta title")]
    public string MetaTitle { get; set; }

    [StringLength(1024, MinimumLength = 1, ErrorMessage = $"{nameof(MetaDescription)} must be between 1 and 1024 characters.")]
    [Display(Name = "Meta description")]
    public string? MetaDescription { get; set; }

    [StringLength(1024, MinimumLength = 1, ErrorMessage = $"{nameof(MetaKeywords)} must be between 1 and 1024 characters.")]
    [Display(Name = "Meta keywords")]
    public string? MetaKeywords { get; set; }

    [Required(ErrorMessage = $"{nameof(H1)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(H1)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(H1))]
    public string H1 { get; set; }

    [StringLength(512, MinimumLength = 1, ErrorMessage = $"{nameof(ShortDescription)} must be between 1 and 512 characters.")]
    [Display(Name = "Short description")]
    public string? ShortDescription { get; set; }

    [Display(Name = "Full description")]
    public string? FullDescription { get; set; }

    [Required(ErrorMessage = $"{nameof(IsActive)} is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = $"{nameof(SortOrder)} is required.")]
    [Display(Name = "Sort order")]
    public int SortOrder { get; set; }

    public AddBrandCommand ToCommand(int createdBy, DateTime createdAt, string createdByIp)
    {
        return new AddBrandCommand()
        {
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
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}