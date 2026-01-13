using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.PostCode;
using Ecommerce3.Contracts.DTOs.PostCode;

namespace Ecommerce3.Admin.ViewModels.PostCode;

public class EditPostCodeViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = $"{nameof(Code)} is required.")]
    [StringLength(16, MinimumLength = 1, ErrorMessage = $"{nameof(Code)} must be between 1 and 16 characters.")]
    [Display(Name = nameof(Code))]
    public string Code { get; set; }

    [Required(ErrorMessage = "Is active is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }
    
    public EditPostCodeCommand ToCommand(int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        return new EditPostCodeCommand()
        {
            Id = Id,
            Code = Code,
            IsActive = IsActive,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp,
        };
    }

    public static EditPostCodeViewModel FromDTO(PostCodeDTO dto)
    {
        return new EditPostCodeViewModel()
        {
            Id = dto.Id,
            Code = dto.Code,
            IsActive = dto.IsActive,
        };
    }
}