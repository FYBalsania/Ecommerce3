using System.Net;
using Ecommerce3.Admin.ViewModels.TextListItem;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class TextListItemsController(
    ILogger<TextListItemsController> logger,
    IIPAddressService ipAddressService,
    ITextListItemService textListItemService) : Controller
{
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([FromForm] AddTextListItemViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return ValidationProblem(ModelState);
        }
        
        var userId = 1;
        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        await textListItemService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        
        var textListItemDTOs = await textListItemService.GetByParamsAsync(Type.GetType(model.ParentEntity)!, model.ParentEntityId, model.Type, cancellationToken);
        
        var textType = model.Type.ToString();
        var prefix = "text-" + textType;
        ViewData["TextPrefix"] = "text-" + textType;
        ViewData["TextTableId"] = $"{prefix}-List";
        
        return PartialView("_TextListItemsPartial", textListItemDTOs);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] EditTextListItemViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return ValidationProblem(ModelState);
        }

        var userId = 1;
        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));

        await textListItemService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        var textListItemDTOs = await textListItemService.GetByParamsAsync(Type.GetType(model.ParentEntity)!, model.ParentEntityId, model.Type, cancellationToken);

        var textType = model.Type.ToString();
        var prefix = "text-" + textType;
        ViewData["TextPrefix"] = "text-" + textType;
        ViewData["TextTableId"] = $"{prefix}-List";
        
        return PartialView("_TextListItemsPartial", textListItemDTOs);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromForm] DeleteTextListItemViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return ValidationProblem(ModelState);
        }

        var userId = 1;
        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));

        await textListItemService.DeleteAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        var textListItemDTOs = await textListItemService.GetByParamsAsync(Type.GetType(model.ParentEntity)!, model.ParentEntityId,
            model.Type, cancellationToken);

        var textType = model.Type.ToString();
        var prefix = "text-" + textType;
        ViewData["TextPrefix"] = "text-" + textType;
        ViewData["TextTableId"] = $"{prefix}-List";

        return PartialView("_TextListItemsPartial", textListItemDTOs);
    }
}