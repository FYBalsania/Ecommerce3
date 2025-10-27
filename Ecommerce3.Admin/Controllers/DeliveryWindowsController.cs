using Ecommerce3.Admin.ViewModels.DeliveryWindow;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class DeliveryWindowsController : Controller
{
    private readonly IDeliveryWindowService _deliveryWindowService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;


    public DeliveryWindowsController(IDeliveryWindowService deliveryWindowService, IIPAddressService ipAddressService, IConfiguration configuration)
    {
        _deliveryWindowService = deliveryWindowService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(DeliveryWindowFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await _deliveryWindowService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new DeliveryWindowIndexViewModel()
        {
            Filter = filter,
            DeliveryWindows = result,
            PageTitle = "Delivery Window"
        };
        
        ViewData["Title"] = "Delivery Window";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Delivery Window";
        return View(new AddDeliveryWindowViewModel
        {
            IsActive = true,
            SortOrder = await _deliveryWindowService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddDeliveryWindowViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _deliveryWindowService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DuplicateException e)
        {
            switch (e.ParamName)
            {
                case nameof(model.Name):
                    ModelState.AddModelError(nameof(model.Name), e.Message);
                    break;
            }
        }
        
        return LocalRedirect("/DeliveryWindows/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var deliveryWindow = await _deliveryWindowService.GetByDeliveryWindowIdAsync(id, cancellationToken);
        if (deliveryWindow is null) return NotFound();

        ViewData["Title"] = $"Edit Delivery Window - {deliveryWindow.Name}";
        return View(EditDeliveryWindowViewModel.FromDTO(deliveryWindow));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditDeliveryWindowViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _deliveryWindowService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (ArgumentNullException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
        }
        catch (DuplicateException e)
        {
            switch (e.ParamName)
            {
                case nameof(model.Name):
                    ModelState.AddModelError(nameof(model.Name), e.Message);
                    break;
            }
        }

        return LocalRedirect("/DeliveryWindows/Index");
    }
}