using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.ImageType;
using Ecommerce3.Contracts.DTOs.ImageType;

namespace Ecommerce3.Admin.ViewModels.ImageType;

public class EditImageTypeViewModel
{
    public int Id { get; set; }

    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Entity)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Entity))]
    public string? Entity { get; set; }
    
    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [StringLength(128, MinimumLength = 1, ErrorMessage = $"{nameof(Name)} must be between 1 and 128 characters.")]
    [Display(Name = nameof(Name))]
    public string Name { get; set; }
    
    [Required(ErrorMessage = $"{nameof(Slug)} is required.")]
    [StringLength(128, MinimumLength = 1, ErrorMessage = $"{nameof(Slug)} must be between 1 and 128 characters.")]
    [Display(Name = nameof(Slug))]
    public string Slug { get; set; }
    
    [StringLength(1024, MinimumLength = 1, ErrorMessage = $"{nameof(Description)} must be between 1 and 1024 characters.")]
    [Display(Name = nameof(Description))]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Is active is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }
    
    public EditImageTypeCommand ToCommand(int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        return new EditImageTypeCommand()
        {
            Id = Id,
            Name = Name,
            Slug = Slug,
            Entity = Entity,
            Description = Description,
            IsActive = IsActive,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp,
        };
    }

    public static EditImageTypeViewModel FromDTO(ImageTypeDTO dto)
    {
        return new EditImageTypeViewModel()
        {
            Id = dto.Id,
            Entity = dto.Entity,
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive,
        };
    }
}