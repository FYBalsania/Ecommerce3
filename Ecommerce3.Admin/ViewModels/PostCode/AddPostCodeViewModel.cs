using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.PostCode;

namespace Ecommerce3.Admin.ViewModels.PostCode;

public class AddPostCodeViewModel
{
    [Required(ErrorMessage = $"{nameof(Code)} is required.")]
    [StringLength(16, MinimumLength = 1, ErrorMessage = $"{nameof(Code)} must be between 1 and 16 characters.")]
    [Display(Name = nameof(Code))]
    public string Code { get; set; }
    
    [Required(ErrorMessage = "Is active is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }
    
    public AddPostCodeCommand ToCommand(int createdBy, DateTime createdAt, string createdByIp)
    {
        return new AddPostCodeCommand()
        {
            Code = Code,
            IsActive = IsActive,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}