using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.UnitOfMeasure;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.ViewModels.UnitOfMeasure;

public sealed class AddUnitOfMeasureViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Code is required.")]
    [StringLength(16, MinimumLength = 1, ErrorMessage = "Code must be between 1 and 16 characters.")]
    public string Code { get; set; }
    
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 256 characters.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Type is required.")]
    public UnitOfMeasureType Type { get; set; }
    
    [Display(Name = "Base Unit")]
    public int? BaseId { get; set; }
    public SelectList Bases { get; set; }
    
    [Required(ErrorMessage = "Conversion factor is required.")]
    [Display(Name = "Conversion Factor")]
    public decimal ConversionFactor { get; set; }
    
    [Required(ErrorMessage = "Is active is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }
    
    public AddUnitOfMeasureCommand ToCommand(int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        return new AddUnitOfMeasureCommand()
        {
            Name = Name,
            Code = Code,
            Type = Type,
            BaseId = BaseId,
            ConversionFactor =  ConversionFactor,
            IsActive = IsActive,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}