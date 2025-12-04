using Ecommerce3.Admin.ViewModels.UnitOfMeasure;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class UnitOfMeasureController : Controller
{
    private readonly IUnitOfMeasureService _unitOfMeasureService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;
    private readonly IDataProtector _dataProtector;

    public UnitOfMeasureController(IUnitOfMeasureService unitOfMeasureService,
        IIPAddressService ipAddressService,IDataProtectionProvider dataProtectionProvider,
        IConfiguration configuration)
    {
        _unitOfMeasureService = unitOfMeasureService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
        _dataProtector = dataProtectionProvider.CreateProtector(nameof(UnitOfMeasure));
    }

    [HttpGet]
    public async Task<IActionResult> Index(UnitOfMeasureFilter filter, int pageNumber,
        CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddUnitOfMeasureViewModel model, CancellationToken cancellationToken)
    {
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        return View();
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUnitOfMeasureViewModel model, CancellationToken cancellationToken)
    {
        return View(model);
    }
}