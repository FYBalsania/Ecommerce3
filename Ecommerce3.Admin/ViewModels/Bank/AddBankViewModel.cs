using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.Bank;

namespace Ecommerce3.Admin.ViewModels.Bank;

public class AddBankViewModel
{
    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Name)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Name))]
    public string Name { get; set; }

    [Required(ErrorMessage = $"{nameof(Slug)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Slug)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Slug))]
    [RegularExpression(@"^[a-z0-9]+(?:[-._~][a-z0-9]+)*$", ErrorMessage = "Invalid slug format.")]
    public string Slug { get; set; }
    
    [Required(ErrorMessage = "Is active is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Sort order is required.")]
    [Display(Name = "Sort order")]
    public int SortOrder { get; set; }
    
    [Required(ErrorMessage = "Meta title is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Meta title must be between 1 and 256 characters.")]
    [Display(Name = "Meta title")]
    public string MetaTitle { get; set; }

    [StringLength(1024, MinimumLength = 1, ErrorMessage = "Meta description must be between 1 and 1024 characters.")]
    [Display(Name = "Meta description")]
    public string? MetaDescription { get; set; }

    [StringLength(1024, MinimumLength = 1, ErrorMessage = $"Meta keywords must be between 1 and 1024 characters.")]
    [Display(Name = "Meta keywords")]
    public string? MetaKeywords { get; set; }

    [Required(ErrorMessage = $"{nameof(H1)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(H1)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(H1))]
    public string H1 { get; set; }
    
    public AddBankCommand ToCommand(int createdBy, DateTime createdAt, string createdByIp)
    {
        return new AddBankCommand()
        {
            Name = Name,
            Slug = Slug,
            IsActive = IsActive,
            SortOrder = SortOrder,
            MetaTitle = MetaTitle,
            MetaDescription = MetaDescription,
            MetaKeywords = MetaKeywords,
            H1 = H1,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}