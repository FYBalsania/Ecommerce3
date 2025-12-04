using System.ComponentModel.DataAnnotations;

namespace Ecommerce3.Admin.ViewModels.UnitOfMeasure;

public sealed class AddUnitOfMeasureViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Code is required.")]
    [StringLength(16, MinimumLength = 1, ErrorMessage = "Code must be between 1 and 16 characters.")]
    public string Code { get; set; }
    
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 256 characters.")]
    public string Name { get; set; }
}