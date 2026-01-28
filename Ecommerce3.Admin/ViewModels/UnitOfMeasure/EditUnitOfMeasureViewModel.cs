using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.UnitOfMeasure;
using Ecommerce3.Contracts.DTOs.UnitOfMeasure;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.ViewModels.UnitOfMeasure;

public class EditUnitOfMeasureViewModel
{
    public int Id { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Code is required.")]
    [StringLength(16, MinimumLength = 1, ErrorMessage = "Code must be between 1 and 16 characters.")]
    public string Code { get; set; }
    
    [Required(ErrorMessage = "Singular name is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Singular name must be between 1 and 256 characters.")]
    public string SingularName { get; set; }
    
    [Required(ErrorMessage = "Plural name is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Plural name must be between 1 and 256 characters.")]
    public string PluralName { get; set; }
    
    [Required(ErrorMessage = "Type is required.")]
    public UnitOfMeasureType Type { get; set; }
    
    [Display(Name = "Base Unit")]
    public int? BaseId { get; set; }
    public SelectList Bases { get; set; }
    
    [Required(ErrorMessage = "Conversion factor is required.")]
    [Display(Name = "Conversion Factor")]
    public decimal ConversionFactor { get; set; }
    
    [Required(ErrorMessage = "Decimal places is required.")]
    [Display(Name = "Decimal places")]
    public byte DecimalPlaces { get; set; }
    
    [Required(ErrorMessage = "Is active is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }
    
    [Required(ErrorMessage = "Sort order is required.")]
    [Display(Name = "Sort order")]
    public int SortOrder { get; set; }
    
    public EditUnitOfMeasureCommand ToCommand(int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        return new EditUnitOfMeasureCommand
        {
            Id = Id,
            Code = Code,
            SingularName = SingularName,
            PluralName = PluralName,
            Type = Type,
            BaseId = BaseId,
            ConversionFactor = ConversionFactor,
            DecimalPlaces = DecimalPlaces,
            IsActive = IsActive,
            SortOrder = SortOrder,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp,
        };
    }

    public static EditUnitOfMeasureViewModel FromDTO(UnitOfMeasureDTO dto)
    {
        return new EditUnitOfMeasureViewModel()
        {
            Id = dto.Id,
            Code = dto.Code,
            SingularName = dto.Name,
            Type = dto.Type,
            BaseId = dto.BaseId,
            ConversionFactor = dto.ConversionFactor,
            IsActive = dto.IsActive,
        };
    }
}