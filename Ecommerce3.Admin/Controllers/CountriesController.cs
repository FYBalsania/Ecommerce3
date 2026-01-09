using Ecommerce3.Admin.ViewModels.Country;
using Ecommerce3.Application.Services.Admin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class CountriesController(ICountryService countryService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddCountryViewModel model, CancellationToken cancellationToken)
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        return View();
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditCountryViewModel model, CancellationToken cancellationToken)
    {
        return View();
    }
}