using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.DeliveryWindow;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Admin.ViewModels.DeliveryWindow;

public class AddDeliveryWindowViewModel
{
    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Name)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Name))]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Delivery window is required.")]
    [Display(Name = "Delivery window")]
    public DeliveryUnit Unit { get; set; }
    
    [Required(ErrorMessage = "Minimum value is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Minimum value must be 0 or greater.")]
    [Display(Name = "Minimum value")]
    public int MinValue { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Maximum value must be 0 or greater.")]
    [Display(Name = "Maximum value")]
    public int? MaxValue { get; set; }
    
    [Required(ErrorMessage = "Is active is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Sort order is required.")]
    [Display(Name = "Sort order")]
    public int SortOrder { get; set; }
    
    public AddDeliveryWindowCommand ToCommand(int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        return new AddDeliveryWindowCommand()
        {
            Name = Name,
            Unit = Unit,
            MinValue = MinValue,
            MaxValue = MaxValue,
            IsActive = IsActive,
            SortOrder = SortOrder,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}