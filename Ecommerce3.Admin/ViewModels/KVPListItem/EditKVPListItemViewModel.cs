using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.KVPListItem;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewModels.KVPListItem;

public class EditKVPListItemViewModel
{
    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Parent entity is required.")]
    public string ParentEntity { get; set; }

    [HiddenInput, Required(AllowEmptyStrings = false, ErrorMessage = "Parent entity id is required.")]
    public int ParentEntityId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required.")]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required.")]
    public KVPListItemType Type { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Key is required.")]
    public string Key { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Value is required.")]
    public string Value { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Sort order is required.")]
    public decimal SortOrder { get; set; }

    public EditKVPListItemCommand ToCommand(int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        return new EditKVPListItemCommand
        {
            ParentEntity = ParentEntity,
            ParentEntityId = ParentEntityId,
            Id = Id,
            Type = Type,
            Key = Key,
            Value = Value,
            SortOrder = SortOrder,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp
        };
    }
}