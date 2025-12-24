using Ecommerce3.Admin.ViewModels.ProductGroup;
using Ecommerce3.Application.Commands.Admin.ProductGroup;
using Ecommerce3.Application.Services.Admin.Interfaces;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.Controllers;

public class ProductGroupsController(
    IProductGroupService productGroupService,
    IIPAddressService ipAddressService,
    IConfiguration configuration,
    IProductAttributeService productAttributeService,
    IProductGroupProductAttributeService productGroupProductAttributeService,
    IProductAttributeValueService productAttributeValueService) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(ProductGroupFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await productGroupService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new ProductGroupsIndexResponse()
        {
            Filter = filter,
            ProductGroups = result,
            PageTitle = "Product Groups"
        };

        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Product Group";
        return View(new AddProductGroupViewModel()
        {
            IsActive = true,
            SortOrder = await productGroupService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProductGroupViewModel model, CancellationToken cancellationToken)
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
            await productGroupService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorText)}":
                    ModelState.AddModelError(nameof(model.AnchorText), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorTitle)}":
                    ModelState.AddModelError(nameof(model.AnchorTitle), domainException.Message);
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
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.ShortDescription)}":
                    ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.FullDescription)}":
                    ModelState.AddModelError(nameof(model.FullDescription), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Name);
        return LocalRedirect("/ProductGroups/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {   
        var productGroup = await productGroupService.GetByProductGroupIdAsync(id, cancellationToken);
        if (productGroup is null) return NotFound();

        ViewData["Title"] = $"Edit ProductGroup - {productGroup.Name}";
        return View(EditProductGroupViewModel.FromDTO(productGroup));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductGroupViewModel model, CancellationToken cancellationToken)
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
            await productGroupService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorText)}":
                    ModelState.AddModelError(nameof(model.AnchorText), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorTitle)}":
                    ModelState.AddModelError(nameof(model.AnchorTitle), domainException.Message);
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
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.ShortDescription)}":
                    ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.FullDescription)}":
                    ModelState.AddModelError(nameof(model.FullDescription), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Name);
        return LocalRedirect("/ProductGroups/Index");
    }

    [HttpGet]
    public async Task<IActionResult> AddAttribute([FromQuery] int productGroupId, CancellationToken cancellationToken)
    {
        var productAttributes = new SelectList(await productAttributeService.GetIdAndNameDictionaryAsync(productGroupId, cancellationToken), "Key", "Value");
        var sortOrder = await productGroupProductAttributeService.GetMaxSortOrderAsync(productGroupId, cancellationToken);

        return PartialView("_AddProductAttributePartial", (productAttributes, sortOrder));
    }

    [HttpGet]
    public async Task<IActionResult> GetAttributeValues([FromQuery] int productAttributeId, CancellationToken cancellationToken)
    {
        var attributeValues = await productAttributeValueService.GetAllByProductAttributeIdAsync(productAttributeId, cancellationToken);

        return PartialView("_ProductAttributeValuesPartial", attributeValues);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAttribute([FromForm] AddProductGroupAttributeViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return ValidationProblem(ModelState);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1;

        await productGroupService.AddAttributeAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);

        var attributes = await productGroupService.GetAttributesByProductGroupIdAsync(model.ProductGroupId, cancellationToken);
        return PartialView("_ProductGroupProductAttributesListItemPartial", attributes);
    }

    [HttpGet]
    public async Task<IActionResult> EditAttribute([FromQuery] int productGroupId, [FromQuery] int productAttributeId,
        CancellationToken cancellationToken)
    {
        var productAttributeEditDTO = await productGroupProductAttributeService.GetByParamsAsync(productGroupId, productAttributeId,
            cancellationToken);

        return PartialView("_EditProductAttributePartial", productAttributeEditDTO);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAttribute([FromBody] EditProductGroupAttributeViewModel model,
        CancellationToken cancellationToken)
    {

        return View();
    }
}