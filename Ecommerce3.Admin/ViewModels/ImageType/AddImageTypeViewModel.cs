using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.ImageType;

namespace Ecommerce3.Admin.ViewModels.ImageType;

public class AddImageTypeViewModel
{
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
    
    public AddImageTypeCommand ToCommand(int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        return new AddImageTypeCommand()
        {
            Entity = Entity,
            Name = Name,
            Slug = Slug,
            Description = Description,
            IsActive = IsActive,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}