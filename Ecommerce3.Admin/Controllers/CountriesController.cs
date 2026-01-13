using System.Net;
using Ecommerce3.Admin.ViewModels.Country;
using Ecommerce3.Application.Services.Admin.Interfaces;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class CountriesController(
    ICountryService countryService,
    IIPAddressService ipAddressService,
    IConfiguration configuration) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(CountryFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await countryService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new CountriesIndexViewModel()
        {
            Filter = filter,
            Countries = result
        };
        ViewData["Title"] = "Countries";
        return View(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Country";
        return View(new AddCountryViewModel
        {
            IsActive = true,
            SortOrder = await countryService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddCountryViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        //var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await countryService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(Country)}.{nameof(Country.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(Country)}.{nameof(Country.Iso2Code)}":
                    ModelState.AddModelError(nameof(model.Iso2Code), domainException.Message);
                    return View(model);
                case $"{nameof(Country)}.{nameof(Country.Iso3Code)}":
                    ModelState.AddModelError(nameof(model.Iso3Code), domainException.Message);
                    return View(model);
                case $"{nameof(Country)}.{nameof(Country.NumericCode)}":
                    ModelState.AddModelError(nameof(model.NumericCode), domainException.Message);
                    return View(model);
                case $"{nameof(Country)}.{nameof(Country.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(Country)}.{nameof(Country.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }
        
        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Name);
        return LocalRedirect("/Countries/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var country = await countryService.GetByIdAsync(id, cancellationToken);
        if (country is null) return NotFound();

        ViewData["Title"] = $"Edit Country - {country.Name}";
        return View(EditCountryViewModel.FromDTO(country));
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditCountryViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        //var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await countryService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(Country)}.{nameof(Country.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(Country)}.{nameof(Country.Iso2Code)}":
                    ModelState.AddModelError(nameof(model.Iso2Code), domainException.Message);
                    return View(model);
                case $"{nameof(Country)}.{nameof(Country.Iso3Code)}":
                    ModelState.AddModelError(nameof(model.Iso3Code), domainException.Message);
                    return View(model);
                case $"{nameof(Country)}.{nameof(Country.NumericCode)}":
                    ModelState.AddModelError(nameof(model.NumericCode), domainException.Message);
                    return View(model);
                case $"{nameof(Country)}.{nameof(Country.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(Country)}.{nameof(Country.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Name);
        return LocalRedirect("/Countries/Index");
    }
}