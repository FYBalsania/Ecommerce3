using System.ComponentModel.DataAnnotations;
using Ecommerce3.Application.Commands.KVPListItem;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Admin.ViewModels.KVPListItem;

public class DeleteKVPListItemViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Parent entity is required.")]
    public Type ParentEntityType { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Parent entity id is required.")]
    public int ParentEntityId { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required.")]
    public int Id { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required.")]
    public KVPListItemType Type { get; set; }

    public DeleteKVPListItemCommand ToCommand(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        return new DeleteKVPListItemCommand
        {
            Id = Id,
            DeletedBy = deletedBy,
            DeletedAt = deletedAt,
            DeletedByIp = deletedByIp
        };
    }
}