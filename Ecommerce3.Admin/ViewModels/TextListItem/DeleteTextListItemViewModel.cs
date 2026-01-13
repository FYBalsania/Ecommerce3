using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.TextListItem;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewModels.TextListItem;

public class DeleteTextListItemViewModel
{
    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Entity is required.")]
    public string ParentEntity { get; set; }
    
    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Entity id is required.")]
    public int ParentEntityId { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required.")]
    public int Id { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required.")]
    public TextListItemType Type { get; set; }
    
    public DeleteTextListItemCommand ToCommand(int deletedBy, DateTime deletedAt, IPAddress deletedByIp)
    {
        return new DeleteTextListItemCommand
        {
            Id = Id,
            DeletedBy = deletedBy,
            DeletedAt = deletedAt,
            DeletedByIp = deletedByIp
        };
    }
}