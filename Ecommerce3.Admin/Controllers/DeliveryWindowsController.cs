using System.Net;
using Ecommerce3.Admin.ViewModels.DeliveryWindow;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class DeliveryWindowsController(
    IDeliveryWindowService deliveryWindowService,
    IIPAddressService ipAddressService,
    IConfiguration configuration) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(DeliveryWindowFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await deliveryWindowService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new DeliveryWindowIndexViewModel()
        {
            Filter = filter,
            DeliveryWindows = result
        };
        ViewData["Title"] = "Delivery Windows";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Delivery Window";
        return View(new AddDeliveryWindowViewModel
        {
            IsActive = true,
            SortOrder = await deliveryWindowService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddDeliveryWindowViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await deliveryWindowService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.Unit)}":
                    ModelState.AddModelError(nameof(model.Unit), domainException.Message);
                    return View(model);
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.MinValue)}":
                    ModelState.AddModelError(nameof(model.MinValue), domainException.Message);
                    return View(model);
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.MaxValue)}":
                    ModelState.AddModelError(nameof(model.MaxValue), domainException.Message);
                    return View(model);
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }
        
        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Name);
        return LocalRedirect("/DeliveryWindows/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var deliveryWindow = await deliveryWindowService.GetByDeliveryWindowIdAsync(id, cancellationToken);
        if (deliveryWindow is null) return NotFound();

        ViewData["Title"] = $"Edit Delivery Window - {deliveryWindow.Name}";
        return View(EditDeliveryWindowViewModel.FromDTO(deliveryWindow));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditDeliveryWindowViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await deliveryWindowService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.Unit)}":
                    ModelState.AddModelError(nameof(model.Unit), domainException.Message);
                    return View(model);
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.MinValue)}":
                    ModelState.AddModelError(nameof(model.MinValue), domainException.Message);
                    return View(model);
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.MaxValue)}":
                    ModelState.AddModelError(nameof(model.MaxValue), domainException.Message);
                    return View(model);
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(DeliveryWindow)}.{nameof(DeliveryWindow.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Name);
        return LocalRedirect("/DeliveryWindows/Index");
    }
}