using Ecommerce3.Admin.ViewModels.UnitOfMeasure;
using Ecommerce3.Application.Commands.UnitOfMeasure;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.Controllers;

public class UnitOfMeasureController(
    IUnitOfMeasureService unitOfMeasureService,
    IIPAddressService ipAddressService,
    IDataProtectionProvider dataProtectionProvider,
    IConfiguration configuration) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(UnitOfMeasureFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await unitOfMeasureService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new UnitOfMeasureIndexViewModel()
        {
            Filter = filter,
            UnitOfMeasures = result,
            PageTitle = "Unit Of Measures",
        };

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
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage;
            model.Bases = await GetIdAndNameDictionaryAsync(null, cancellationToken);
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        try
        {
            await unitOfMeasureService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage;
            model.Bases = await GetIdAndNameDictionaryAsync(null, cancellationToken);
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

        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Name);
        return LocalRedirect("/UnitOfMeasure/Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var unitOfMeasure = await unitOfMeasureService.GetByUnitOfMeasureIdAsync(id, cancellationToken);
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
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage;
            model.Bases = await GetIdAndNameDictionaryAsync(model.Id, cancellationToken);
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await unitOfMeasureService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage;
            model.Bases = await GetIdAndNameDictionaryAsync(model.Id, cancellationToken);
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

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Name);
        return LocalRedirect("/UnitOfMeasure/Index");
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; // int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var command = new DeleteUnitOfMeasureCommand
        {
            Id = id,
            DeletedBy = userId,
            DeletedAt = DateTime.Now,
            DeletedByIp = ipAddress
        };

        await unitOfMeasureService.DeleteAsync(command, cancellationToken);
        TempData["SuccessMessage"] = Common.DeletedSuccessfully;
        return LocalRedirect("/UnitOfMeasure/Index");
    }
    
    [NonAction]
    private async Task<SelectList> GetIdAndNameDictionaryAsync(int? excludeId = null, CancellationToken cancellationToken = default)
        => new SelectList(await unitOfMeasureService.GetIdAndNameDictionaryAsync(excludeId, true, cancellationToken),"Key","Value");
}