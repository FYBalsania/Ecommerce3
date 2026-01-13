using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.KVPListItem;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewModels.KVPListItem;

public class AddKVPListItemViewModel
{
    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Parent entity is required.")]
    public string ParentEntity { get; set; }

    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Parent entity id is required.")]
    public int ParentEntityId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required.")]
    public KVPListItemType Type { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Key is required.")]
    public string Key { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Value is required.")]
    public string Value { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Sort order is required.")]
    public decimal SortOrder { get; set; }

    public AddKVPListItemCommand ToCommand(int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        return new AddKVPListItemCommand
        {
            ParentEntity = ParentEntity,
            ParentEntityId = ParentEntityId,
            Type = Type,
            Key = Key,
            Value = Value,
            SortOrder = SortOrder,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp,
        };
    }
}