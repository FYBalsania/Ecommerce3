using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.Bank;
using Ecommerce3.Contracts.DTOs.Bank;
using Ecommerce3.Contracts.DTOs.Image;

namespace Ecommerce3.Admin.ViewModels.Bank;

public class EditBankViewModel
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
    
    public IReadOnlyList<ImageDTO> Images { get; private set; } = [];
    
    public EditBankCommand ToCommand(int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        return new EditBankCommand()
        {
            Id = Id,
            Name = Name,
            Slug = Slug,
            IsActive = IsActive,
            SortOrder = SortOrder,
            MetaTitle = MetaTitle,
            MetaDescription = MetaDescription,
            MetaKeywords = MetaKeywords,
            H1 = H1,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp,
        };
    }

    public static EditBankViewModel FromDTO(BankDTO dto)
    {
        return new EditBankViewModel()
        {
            Id = dto.Id,
            Name = dto.Name,
            Slug = dto.Slug,
            IsActive = dto.IsActive,
            SortOrder = dto.SortOrder,
            MetaTitle = dto.MetaTitle,
            MetaDescription = dto.MetaDescription,
            MetaKeywords = dto.MetaKeywords,
            H1 = dto.H1,
            Images = dto.Images
        };
    }
}