using Ecommerce3.Admin.ViewModels.KVPListItem;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class KVPListItemsController(
    IKVPListItemService kvpListItemService,
    IIPAddressService ipAddressService)
    : Controller
{
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAsync(AddKVPListItemViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        const int userId = 1;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        await kvpListItemService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);

        var kvpListItemDTOs = await kvpListItemService.GetAllByParamsAsync(Type.GetType(model.ParentEntity)!, model.ParentEntityId,
            model.Type, cancellationToken);
        
        var kvpType = model.Type.ToString();
        var prefix = "kvp-" + kvpType;
        ViewData["KVPPrefix"] = "kvp-" + kvpType;
        ViewData["KVPTableId"] = $"{prefix}-List";
        
        return PartialView("_KVPListItemsPartial", kvpListItemDTOs);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(EditKVPListItemViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        const int userId = 1;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        await kvpListItemService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        
        var kvpListItemDTOs = await kvpListItemService.GetAllByParamsAsync(Type.GetType(model.ParentEntity)!, model.ParentEntityId,
            model.Type, cancellationToken);
        
        var kvpType = model.Type.ToString();
        var prefix = "kvp-" + kvpType;
        ViewData["KVPPrefix"] = "kvp-" + kvpType;
        ViewData["KVPTableId"] = $"{prefix}-List";
        
        return PartialView("_KVPListItemsPartial", kvpListItemDTOs);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAsync(DeleteKVPListItemViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        
        const int userId = 1;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        await kvpListItemService.DeleteAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        
        var kvpListItemDTOs = await kvpListItemService.GetAllByParamsAsync(Type.GetType(model.ParentEntity)!, model.ParentEntityId,
            model.Type, cancellationToken);
        
        var kvpType = model.Type.ToString();
        var prefix = "kvp-" + kvpType;
        ViewData["KVPPrefix"] = "kvp-" + kvpType;
        ViewData["KVPTableId"] = $"{prefix}-List";
        
        return PartialView("_KVPListItemsPartial", kvpListItemDTOs);
    }
}