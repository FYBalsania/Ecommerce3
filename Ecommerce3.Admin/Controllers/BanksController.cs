using Ecommerce3.Admin.ViewModels.Bank;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class BanksController(
    IBankService bankService,
    IIPAddressService ipAddressService,
    IConfiguration configuration) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(BankFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await bankService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new BanksIndexViewModel()
        {
            Filter = filter,
            Banks = result,
            PageTitle = "Banks"
        };
        
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Bank";
        return View(new AddBankViewModel
        {
            IsActive = true,
            SortOrder = await bankService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddBankViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage;
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await bankService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage;
            switch (domainException.Error.Code)
            {
                case $"{nameof(Bank)}.{nameof(Bank.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(Bank)}.{nameof(Bank.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
            }
        }
        
        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Name);
        return LocalRedirect("/Banks/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var bank = await bankService.GetByBankIdAsync(id, cancellationToken);
        if (bank is null) return NotFound();

        ViewData["Title"] = $"Edit Bank - {bank.Name}";
        return View(EditBankViewModel.FromDTO(bank));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditBankViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage;
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await bankService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage;
            switch (domainException.Error.Code)
            {
                case $"{nameof(Bank)}.{nameof(Bank.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(Bank)}.{nameof(Bank.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Name);
        return LocalRedirect("/Banks/Index");
    }
}