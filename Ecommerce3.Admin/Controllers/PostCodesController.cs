using Ecommerce3.Admin.ViewModels.PostCode;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class PostCodesController : Controller
{
    private readonly IPostCodeService _postCodeService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;

    public PostCodesController(IPostCodeService postCodeService, IIPAddressService ipAddressService,
        IConfiguration configuration)
    {
        _postCodeService = postCodeService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }

    [HttpGet]
    public async Task<IActionResult> Index(PostCodeFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await _postCodeService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new PostCodesIndexViewModel()
        {
            Filter = filter,
            PostCodes = result,
            PageTitle = "PostCodes"
        };
        
        ViewData["Title"] = "PostCodes";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add PostCode";
        return View(new AddPostCodeViewModel
        {
            IsActive = true,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddPostCodeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _postCodeService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DuplicateException e)
        {
            switch (e.ParamName)
            {
                case nameof(model.Code):
                    ModelState.AddModelError(nameof(model.Code), e.Message);
                    break;
            }
        }
        
        return LocalRedirect("/PostCodes/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var postCode = await _postCodeService.GetByPostCodeIdAsync(id, cancellationToken);
        if (postCode is null) return NotFound();

        ViewData["Title"] = $"Edit PostCode - {postCode.Code}";
        return View(EditPostCodeViewModel.FromDTO(postCode));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditPostCodeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _postCodeService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (ArgumentNullException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
        }
        catch (DuplicateException e)
        {
            switch (e.ParamName)
            {
                case nameof(model.Code):
                    ModelState.AddModelError(nameof(model.Code), e.Message);
                    break;
            }
        }

        return LocalRedirect("/PostCodes/Index");
    }
}