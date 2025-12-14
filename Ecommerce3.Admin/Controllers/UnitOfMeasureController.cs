using Ecommerce3.Admin.ViewModels.UnitOfMeasure;
using Ecommerce3.Application.Commands.UnitOfMeasure;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        var result = await _unitOfMeasureService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new UnitOfMeasureIndexViewModel()
        {
            Filter = filter,
            UnitOfMeasures = result,
            PageTitle = "Unit Of Measures",
        };

        ViewData["Title"] = "Unit Of Measures";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Unit of Measure";
        return View(new AddUnitOfMeasureViewModel
        {
            Bases = await GetIdAndNameDictionaryAsync(null, cancellationToken),
            IsActive = true,
        });
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddUnitOfMeasureViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("Bases");
        if (!ModelState.IsValid)
        {
            model.Bases = await GetIdAndNameDictionaryAsync(null, cancellationToken);
            return View(model);
        }

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        try
        {
            await _unitOfMeasureService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            switch (domainException.Error.Code)
            {
                case $"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Code)}":
                    ModelState.AddModelError(nameof(model.Code), domainException.Message);
                    return View(model);
                case $"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Type)}":
                    ModelState.AddModelError(nameof(model.Type), domainException.Message);
                    return View(model);
                case $"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.ConversionFactor)}":
                    ModelState.AddModelError(nameof(model.ConversionFactor), domainException.Message);
                    return View(model);
            }
        }

        return LocalRedirect("/UnitOfMeasure/Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var unitOfMeasure = await _unitOfMeasureService.GetByUnitOfMeasureIdAsync(id, cancellationToken);
        if (unitOfMeasure is null) return NotFound();
        
        var model = EditUnitOfMeasureViewModel.FromDTO(unitOfMeasure);
        model.Bases = await GetIdAndNameDictionaryAsync(id, cancellationToken);
        
        ViewData["Title"] = $"Edit Unit of Measure - {unitOfMeasure.Name}";
        return View(model);
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUnitOfMeasureViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("Bases");
        if (!ModelState.IsValid)
        {
            model.Bases = await GetIdAndNameDictionaryAsync(model.Id, cancellationToken);
            return View(model);
        }

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _unitOfMeasureService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            switch (domainException.Error.Code)
            {
                case $"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Code)}":
                    ModelState.AddModelError(nameof(model.Code), domainException.Message);
                    return View(model);
                case $"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Type)}":
                    ModelState.AddModelError(nameof(model.Type), domainException.Message);
                    return View(model);
                case $"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.ConversionFactor)}":
                    ModelState.AddModelError(nameof(model.ConversionFactor), domainException.Message);
                    return View(model);
            }
        }

        return LocalRedirect("/UnitOfMeasure/Index");
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; // int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var command = new DeleteUnitOfMeasureCommand
        {
            Id = id,
            DeletedBy = userId,
            DeletedAt = DateTime.Now,
            DeletedByIp = ipAddress
        };

        await _unitOfMeasureService.DeleteAsync(command, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
    
    [NonAction]
    private async Task<SelectList> GetIdAndNameDictionaryAsync(int? excludeId = null, CancellationToken cancellationToken = default)
    {
        var categoryParents = await _unitOfMeasureService.GetIdAndNameDictionaryAsync(excludeId, true, cancellationToken);
        return new SelectList(categoryParents,"Key","Value");
    }
}