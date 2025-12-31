using Ecommerce3.Admin.ViewModels.PostCode;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class PostCodesController(
    IPostCodeService postCodeService,
    IIPAddressService ipAddressService,
    IConfiguration configuration) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(PostCodeFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await postCodeService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new PostCodesIndexViewModel()
        {
            Filter = filter,
            PostCodes = result
        };
        ViewData["Title"] = "PostCodes";
        return View(response);
    }

    [HttpGet]
    public IActionResult Add()
    {
        ViewData["Title"] = "Add PostCode";
        return View(new AddPostCodeViewModel
        {
            IsActive = true,
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddPostCodeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await postCodeService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(PostCode)}.{nameof(PostCode.Code)}":
                    ModelState.AddModelError(nameof(model.Code), domainException.Message);
                    return View(model);
                case $"{nameof(PostCode)}.{nameof(PostCode.IsActive)}":
                    ModelState.AddModelError(nameof(model.Code), domainException.Message);
                    return View(model);
            }
        }
        
        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Code);
        return LocalRedirect("/PostCodes/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var postCode = await postCodeService.GetByPostCodeIdAsync(id, cancellationToken);
        if (postCode is null) return NotFound();

        ViewData["Title"] = $"Edit PostCode - {postCode.Code}";
        return View(EditPostCodeViewModel.FromDTO(postCode));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditPostCodeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await postCodeService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(PostCode)}.{nameof(PostCode.Code)}":
                    ModelState.AddModelError(nameof(model.Code), domainException.Message);
                    return View(model);
                case $"{nameof(PostCode)}.{nameof(PostCode.IsActive)}":
                    ModelState.AddModelError(nameof(model.Code), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Code);
        return LocalRedirect("/PostCodes/Index");
    }
}