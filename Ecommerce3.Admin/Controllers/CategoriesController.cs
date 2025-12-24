using Ecommerce3.Admin.ViewModels.Category;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.Controllers;

public class CategoriesController(
    ICategoryService categoryService,
    IIPAddressService ipAddressService,
    IConfiguration configuration) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(CategoryFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await categoryService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new CategoriesIndexViewModel()
        {
            Filter = filter,
            Parents = await GetParentsIdAndNameAsync(null, cancellationToken),
            Categories = result,
            PageTitle = "Categories"
        };
        
        return View(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Category";
        return View(new AddCategoryViewModel()
        {
            Parents = await GetParentsIdAndNameAsync(null, cancellationToken),
            IsActive = true,
            SortOrder = await categoryService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddCategoryViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("Parents");
        if (!ModelState.IsValid)
        {
            model.Parents = await GetParentsIdAndNameAsync(null, cancellationToken);
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await categoryService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            model.Parents = await GetParentsIdAndNameAsync(null, cancellationToken);
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(Category)}.{nameof(Category.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.AnchorText)}":
                    ModelState.AddModelError(nameof(model.AnchorText), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.AnchorTitle)}":
                    ModelState.AddModelError(nameof(model.AnchorTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.GoogleCategory)}":
                    ModelState.AddModelError(nameof(model.GoogleCategory), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.ParentId)}":
                    ModelState.AddModelError(nameof(model.ParentId), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaTitle)}":
                    ModelState.AddModelError(nameof(model.MetaTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaDescription)}":
                    ModelState.AddModelError(nameof(model.MetaDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaKeywords)}":
                    ModelState.AddModelError(nameof(model.MetaKeywords), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.H1)}":
                    ModelState.AddModelError(nameof(model.H1), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.ShortDescription)}":
                    ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.FullDescription)}":
                    ModelState.AddModelError(nameof(model.FullDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }
        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Name);
        return LocalRedirect("/Categories/Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var category = await categoryService.GetByCategoryIdAsync(id, cancellationToken);
        if (category is null) return NotFound();

        var model = EditCategoryViewModel.FromDTO(category);
        var excludeIds = (await categoryService.GetDescendantIdsAsync(id, cancellationToken)).Append(id).ToArray();
        model.Parents = await GetParentsIdAndNameAsync(excludeIds, cancellationToken);
        ViewData["Title"] = $"Edit Category - {category.Name}";
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditCategoryViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("Parents");
        if (!ModelState.IsValid)
        {
            model.Parents = await GetParentsIdAndNameAsync(null, cancellationToken);
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await categoryService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            model.Parents = await GetParentsIdAndNameAsync(null, cancellationToken);
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(Category)}.{nameof(Category.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.AnchorText)}":
                    ModelState.AddModelError(nameof(model.AnchorText), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.AnchorTitle)}":
                    ModelState.AddModelError(nameof(model.AnchorTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.GoogleCategory)}":
                    ModelState.AddModelError(nameof(model.GoogleCategory), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.ParentId)}":
                    ModelState.AddModelError(nameof(model.ParentId), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaTitle)}":
                    ModelState.AddModelError(nameof(model.MetaTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaDescription)}":
                    ModelState.AddModelError(nameof(model.MetaDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaKeywords)}":
                    ModelState.AddModelError(nameof(model.MetaKeywords), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.H1)}":
                    ModelState.AddModelError(nameof(model.H1), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.ShortDescription)}":
                    ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.FullDescription)}":
                    ModelState.AddModelError(nameof(model.FullDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(Category)}.{nameof(Category.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Name);
        return LocalRedirect("/Categories/Index");
    }
    
    [NonAction]
    private async Task<SelectList> GetParentsIdAndNameAsync(int[]? excludeIds, CancellationToken cancellationToken)
        => new SelectList(await categoryService.GetIdAndNameListAsync(excludeIds, cancellationToken), "Key", "Value");
}