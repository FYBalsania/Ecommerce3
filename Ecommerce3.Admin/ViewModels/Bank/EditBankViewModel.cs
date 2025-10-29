using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.Bank;
using Ecommerce3.Contracts.DTOs.Bank;

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
    
    public EditBankCommand ToCommand(int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        return new EditBankCommand()
        {
            Id = Id,
            Name = Name,
            Slug = Slug,
            IsActive = IsActive,
            SortOrder = SortOrder,
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
        };
    }
}