using Ecommerce3.Admin.ViewModels.Bank;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class BanksController : Controller
{
    private readonly IBankService _bankService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;

    public BanksController(IBankService bankService, IIPAddressService ipAddressService,
        IConfiguration configuration)
    {
        _bankService = bankService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }

    [HttpGet]
    public async Task<IActionResult> Index(BankFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await _bankService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new BanksIndexViewModel()
        {
            Filter = filter,
            Banks = result,
            PageTitle = "Banks"
        };
        
        ViewData["Title"] = "Banks";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Bank";
        return View(new AddBankViewModel
        {
            IsActive = true,
            SortOrder = await _bankService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddBankViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _bankService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DuplicateException e)
        {
            switch (e.ParamName)
            {
                case nameof(model.Name):
                    ModelState.AddModelError(nameof(model.Name), e.Message);
                    break;
                case nameof(model.Slug):
                    ModelState.AddModelError(nameof(model.Slug), e.Message);
                    break;
            }
        }
        
        return LocalRedirect("/Banks/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var bank = await _bankService.GetByBankIdAsync(id, cancellationToken);
        if (bank is null) return NotFound();

        ViewData["Title"] = $"Edit Bank - {bank.Name}";
        return View(EditBankViewModel.FromDTO(bank));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditBankViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _bankService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
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
                case nameof(model.Slug):
                    ModelState.AddModelError(nameof(model.Slug), e.Message);
                    break;
            }
        }

        return LocalRedirect("/Banks/Index");
    }
}