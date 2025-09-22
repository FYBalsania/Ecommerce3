using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands;

namespace Ecommerce3.Admin.ViewModels;

public sealed class AddBrandViewModel
{
    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [Range(1, 256, ErrorMessage = $"{nameof(Name)} must be between 1 and 256 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = $"{nameof(Slug)} is required.")]
    [Range(1, 256, ErrorMessage = $"{nameof(Slug)} must be between 1 and 256 characters.")]
    public string Slug { get; set; }

    [Required(ErrorMessage = $"{nameof(Display)} is required.")]
    [Range(1, 256, ErrorMessage = $"{nameof(Display)} must be between 1 and 256 characters.")]
    public string Display { get; set; }

    [Required(ErrorMessage = $"{nameof(Breadcrumb)} is required.")]
    [Range(1, 256, ErrorMessage = $"{nameof(Breadcrumb)} must be between 1 and 256 characters.")]
    public string Breadcrumb { get; set; }

    [Required(ErrorMessage = $"{nameof(AnchorText)} is required.")]
    [Range(1, 256, ErrorMessage = $"{nameof(AnchorText)} must be between 1 and 256 characters.")]
    public string AnchorText { get; set; }

    [MaxLength(256, ErrorMessage = $"{nameof(AnchorTitle)} may be between 1 and 256 characters.")]
    public string? AnchorTitle { get; set; }

    [Required(ErrorMessage = $"{nameof(MetaTitle)} is required.")]
    [Range(1, 256, ErrorMessage = $"{nameof(MetaTitle)} must be between 1 and 256 characters.")]
    public string MetaTitle { get; set; }

    [MaxLength(1024, ErrorMessage = $"{nameof(MetaDescription)} may be between 1 and 1024 characters.")]
    public string? MetaDescription { get; set; }

    [MaxLength(1024, ErrorMessage = $"{nameof(MetaKeywords)} may be between 1 and 1024 characters.")]
    public string? MetaKeywords { get; set; }

    [Required(ErrorMessage = $"{nameof(H1)} is required.")]
    [Range(1, 256, ErrorMessage = $"{nameof(H1)} must be between 1 and 256 characters.")]
    public string H1 { get; set; }

    [MaxLength(512, ErrorMessage = $"{nameof(ShortDescription)} may be between 1 and 512 characters.")]
    public string? ShortDescription { get; set; }

    public string? FullDescription { get; set; }

    [Required(ErrorMessage = $"{nameof(IsActive)} is required.")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = $"{nameof(SortOrder)} is required.")]
    public int SortOrder { get; set; }
    
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedByIp { get; set; }

    public AddBrandCommand ToCommand()
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
            CreatedBy = CreatedBy,
            CreatedAt = CreatedAt,
            CreatedByIp = CreatedByIp,
        };
    }
}