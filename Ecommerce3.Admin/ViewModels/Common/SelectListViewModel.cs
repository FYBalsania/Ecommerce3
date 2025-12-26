using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.ViewModels.Common;

public record SelectListViewModel
{
    [HiddenInput]
    public required int Id { get; init; }
    public required string Text { get; init; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Value is required.")]
    public required int? ValueId { get; init; }
    
    public required SelectList Values { get; init; }
}