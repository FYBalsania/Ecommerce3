using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.Admin.Product;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewModels.Product;

public class EditInventoryViewModel
{
    [HiddenInput]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 256 characters.")]
    [Display(Name = "Name")]
    public string Name { get; set; }
    
    [HiddenInput]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required.")]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Price is required.")]
    [Display(Name = "Price")]
    [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    [Display(Name = "Old Price")]
    [Range(1, int.MaxValue, ErrorMessage = "Old price must be greater than 0.")]
    public decimal? OldPrice { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Stock is required.")]
    [Display(Name = "Stock")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than or equal to 0.")]
    public decimal Stock { get; set; }
    
    public string? ReturnUrl { get; set; }

    public EditInventoryCommand ToCommand(int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        return new EditInventoryCommand()
        {
            Id = Id,
            Name = Name,
            Price = Price,
            OldPrice = OldPrice,
            Stock = Stock,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp,
            ReturnUrl =  ReturnUrl
        };
    }
}