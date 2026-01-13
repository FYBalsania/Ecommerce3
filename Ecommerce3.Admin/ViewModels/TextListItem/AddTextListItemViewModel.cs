using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.TextListItem;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewModels.TextListItem;

public class AddTextListItemViewModel
{
    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Parent Entity is required.")]
    public string ParentEntity { get; set; }
    
    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Parent Entity id is required.")]
    public int ParentEntityId { get; set; }

    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Entity is required.")]
    public string Entity { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required.")]
    public TextListItemType Type { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Text is required.")]
    public string Text { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Sort order is required.")]
    public decimal SortOrder { get; set; }

    public AddTextListItemCommand ToCommand(int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        return new AddTextListItemCommand
        {
            ParentEntity = ParentEntity,
            ParentEntityId = ParentEntityId,
            Entity = Entity,
            Type = Type,
            Text = Text,
            SortOrder = SortOrder,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}