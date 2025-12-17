using Ecommerce3.Admin.ViewModels.TextListItem;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class TextListItemsController(
    ILogger<TextListItemsController> logger,
    IConfiguration configuration,
    IIPAddressService ipAddressService,
    ITextListItemService textListItemService) : Controller
{
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([FromForm] AddTextListItemViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        
        var userId = 1;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        await textListItemService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        
        var textListItemDTOs = await textListItemService.GetByParamsAsync(Type.GetType(model.ParentEntity)!, model.ParentEntityId, model.Type, cancellationToken);
        ViewData["TextListType"] = model.Type.ToString();
        return PartialView("_TextListItemsPartial", textListItemDTOs);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] EditTextListItemViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        await textListItemService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        var textListItemDTOs = await textListItemService.GetByParamsAsync(Type.GetType(model.ParentEntity)!, model.ParentEntityId, model.Type, cancellationToken);
        ViewData["TextListType"] = model.Type.ToString();
        return PartialView("_TextListItemsPartial", textListItemDTOs);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromForm] DeleteTextListItemViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        await textListItemService.DeleteAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        var textListItemDTOs = await textListItemService.GetByParamsAsync(Type.GetType(model.ParentEntity)!, model.ParentEntityId,
            model.Type, cancellationToken);

        ViewData["TextListType"] = model.Type.ToString();
        return PartialView("_TextListItemsPartial", textListItemDTOs);
    }
}