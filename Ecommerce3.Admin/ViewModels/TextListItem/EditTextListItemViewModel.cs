using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.TextListItem;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewModels.TextListItem;

public class EditTextListItemViewModel
{
    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Parent Entity is required.")]
    public string ParentEntity { get; set; }
    
    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Parent Entity id is required.")]
    public int ParentEntityId { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required.")]
    public int Id { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required.")]
    public TextListItemType Type { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Text is required.")]
    public string Text { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Sort order is required.")]
    public decimal SortOrder { get; set; }

    public EditTextListItemCommand ToCommand(int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        return new EditTextListItemCommand
        {
            ParentEntity = ParentEntity,
            ParentEntityId = ParentEntityId,
            Id = Id,
            Type = Type,
            Text = Text,
            SortOrder = SortOrder,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp
        };
    }
}