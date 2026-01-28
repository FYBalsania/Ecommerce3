using System.Net;
using Ecommerce3.Admin.ViewModels.Common;
using Ecommerce3.Admin.ViewModels.Product;
using Ecommerce3.Application.Services.Admin.Interfaces;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.Controllers;

public class ProductsController(
    IProductService productService,
    IIPAddressService ipAddressService,
    IConfiguration configuration,
    IBrandService brandService,
    ICategoryService categoryService,
    IProductGroupService productGroupService,
    IUnitOfMeasureService unitOfMeasureService,
    IDeliveryWindowService deliveryWindowService,
    ICountryService countryService) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(ProductFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await productService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new ProductsIndexViewModel()
        {
            Filter = filter,
            Products = result
        };
        ViewData["Title"] = "Products";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        var sortOrder = await productService.GetMaxSortOrderAsync(cancellationToken);
        var brands = new SelectList(await brandService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        var categories = new SelectList(await categoryService.GetIdAndNameListAsync(null, cancellationToken), "Key", "Value");
        var productGroups = new SelectList(await productGroupService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        var uoms = new SelectList(await unitOfMeasureService.GetIdAndNameDictionaryAsync(null, false, cancellationToken), "Key", "Value");
        var deliveryWindows = new SelectList(await deliveryWindowService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");
        var countries = new SelectList(await countryService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");

        ViewData["Title"] = "Add Product";

        return View(new AddProductViewModel
        {
            BrandId = 1,
            Status = ProductStatus.Active,
            PageTitle = "Add Product",
            SortOrder = sortOrder + 1,
            Brands = brands,
            Categories = categories,
            ProductGroups = productGroups,
            UnitOfMeasures = uoms,
            DeliveryWindows = deliveryWindows,
            Countries = countries,
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProductViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove(nameof(AddProductViewModel.PageTitle));
        ModelState.Remove(nameof(AddProductViewModel.Brands));
        ModelState.Remove(nameof(AddProductViewModel.Categories));
        ModelState.Remove(nameof(AddProductViewModel.ProductGroups));
        ModelState.Remove(nameof(AddProductViewModel.UnitOfMeasures));
        ModelState.Remove(nameof(AddProductViewModel.DeliveryWindows));
        ModelState.Remove(nameof(AddProductViewModel.Countries));
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            await PopulateViewModelForAdd(model, cancellationToken);
            return View(model);
        }

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));

        var command = model.ToCommand(userId, createdAt, ipAddress);
        try
        {
            await productService.AddAsync(command, cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            HandleProductDomainException(domainException, model);
            await PopulateViewModelForAdd(model, cancellationToken);
            return View(model);
        }

        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Name);
        return LocalRedirect("/Products/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var product = await productService.GetByIdAsync(id, cancellationToken);
        if (product is null) return NotFound();

        var model = EditProductViewModel.FromDTO(product);
        model.Brands = new SelectList(await brandService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.Categories = new SelectList(await categoryService.GetIdAndNameListAsync(null, cancellationToken), "Key", "Value");
        model.ProductGroups = new SelectList(await productGroupService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.UnitOfMeasures = new SelectList(await unitOfMeasureService.GetIdAndNameDictionaryAsync(null, false, cancellationToken), "Key", "Value");
        model.DeliveryWindows = new SelectList(await deliveryWindowService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");
        model.Countries = new SelectList(await countryService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");

        if (product.ProductGroupId is not null)
        {
            var productGroupProductAttributes =
                await productGroupService.GetAttributesAsync(product.ProductGroupId.Value, cancellationToken);
            var lookup = productGroupProductAttributes
                .ToLookup(x => new { x.ProductAttributeId, x.ProductAttributeName });

            var selectListViewModels = lookup.Select(item => new SelectListViewModel
            {
                Id = item.Key.ProductAttributeId,
                Text = item.Key.ProductAttributeName,
                ValueId = product.Attributes.FirstOrDefault(x => x.ProductAttributeId == item.Key.ProductAttributeId)?.ProductAttributeValueId,
                Values = new SelectList(
                    item, 
                    "ProductAttributeValueId",
                    "ProductAttributeValueDisplay",
                    product.Attributes.FirstOrDefault(x => x.ProductAttributeId == item.Key.ProductAttributeId)?.ProductAttributeValueId)
            }).ToList();
            
            model.AttributesSelectList = selectListViewModels;
        }

        ViewData["Title"] = $"Edit Product - {product.Name}";
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove(nameof(model.PageTitle));
        ModelState.Remove(nameof(model.Brands));
        ModelState.Remove(nameof(model.Categories));
        ModelState.Remove(nameof(model.ProductGroups));
        ModelState.Remove(nameof(model.UnitOfMeasures));
        ModelState.Remove(nameof(model.DeliveryWindows));
        ModelState.Remove(nameof(model.DeliveryWindows));
        ModelState.Remove(nameof(model.Countries));
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            await PopulateViewModelForEdit(model, cancellationToken);
            return View(model);
        }

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));

        var command = model.ToCommand(userId, createdAt, ipAddress);
        try
        {
            await productService.EditAsync(command, cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            HandleProductDomainException(domainException, model);
            await PopulateViewModelForEdit(model, cancellationToken);
            return View(model);
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Name);
        return LocalRedirect("/Products/Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetAttributes([FromQuery] int productGroupId, CancellationToken cancellationToken)
    {
        var productGroupProductAttributes =
            await productGroupService.GetAttributesAsync(productGroupId, cancellationToken);
        var lookup = productGroupProductAttributes
            .ToLookup(x => new { x.ProductAttributeId, x.ProductAttributeName });

        var selectListViewModels = lookup.Select(item => new SelectListViewModel
        {
            Id = item.Key.ProductAttributeId,
            Text = item.Key.ProductAttributeName,
            ValueId = null,
            Values = new SelectList(item, "ProductAttributeValueId", "ProductAttributeValueDisplay", null)
        }).ToList();

        return PartialView("~/Views/Shared/Product/_ProductAttributesPartial.cshtml", selectListViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Inventory(InventoryFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await productService.GetInventoryListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new InventoryIndexViewModel()
        {
            Filter = filter,
            Inventories = result
        };
        ViewData["Title"] = "Inventories";
        return View(response);
    }
    
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditInventory(EditInventoryViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove(nameof(model.Name));
        if (!ModelState.IsValid)
        {
            var rowId = model.Id;
            var fieldName = ModelState.First(x => x.Value!.Errors.Count > 0).Key;
            var message = ModelState[fieldName]!.Errors.First().ErrorMessage;
        
            TempData["RowId"] = rowId;
            TempData["Field"] = fieldName;
            TempData["ErrorMessage"] = message;
        
            return LocalRedirect(model.ReturnUrl ?? "/Products/Inventory");
        }

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));

        var command = model.ToCommand(userId, createdAt, ipAddress);
        try
        {
            await productService.EditInventoryAsync(command, cancellationToken);
        }
        catch (DomainException domainException)
        {
            var rowId = model.Id;
            string field = string.Empty;

            switch (domainException.Error.Code)
            {
                case $"{nameof(Product)}.{nameof(Product.Price)}":
                    field = nameof(model.Price);
                    break;
                
                case $"{nameof(Product)}.{nameof(Product.OldPrice)}":
                    field = nameof(model.OldPrice);
                    break;

                case $"{nameof(Product)}.{nameof(Product.Stock)}":
                    field = nameof(model.Stock);
                    break;
            }

            TempData["RowId"] = rowId;
            TempData["Field"] = field;
            TempData["ErrorMessage"] = domainException.Message;

            return LocalRedirect(model.ReturnUrl ?? "/Products/Inventory");
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Name);
        return LocalRedirect("/Products/Inventory");
    }
    
    #region Private Helper Methods

    private async Task PopulateViewModelForAdd(AddProductViewModel model, CancellationToken cancellationToken)
    {
        var sortOrder = await productService.GetMaxSortOrderAsync(cancellationToken);
        model.SortOrder = sortOrder + 1;
        model.Brands = new SelectList(await brandService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.Categories = new SelectList(await categoryService.GetIdAndNameListAsync(null, cancellationToken), "Key", "Value");
        model.ProductGroups = new SelectList(await productGroupService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.UnitOfMeasures = new SelectList(await unitOfMeasureService.GetIdAndNameDictionaryAsync(null, false, cancellationToken), "Key", "Value");
        model.DeliveryWindows = new SelectList(await deliveryWindowService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");
        model.Countries = new SelectList(await countryService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");
        model.PageTitle = "Add Product";
    }

    private async Task PopulateViewModelForEdit(EditProductViewModel model, CancellationToken cancellationToken)
    {
        model.Brands = new SelectList(await brandService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.Categories = new SelectList(await categoryService.GetIdAndNameListAsync(null, cancellationToken), "Key", "Value");
        model.ProductGroups = new SelectList(await productGroupService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.UnitOfMeasures = new SelectList(await unitOfMeasureService.GetIdAndNameDictionaryAsync(null, false, cancellationToken), "Key", "Value");
        model.DeliveryWindows = new SelectList(await deliveryWindowService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");
        model.Countries = new SelectList(await countryService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");

        var productDTO = await productService.GetByIdAsync(model.Id, cancellationToken);
        if (productDTO != null)
        {
            model.Images = productDTO.Images;
            model.TextListItems = productDTO.TextListItems;
        }

        model.PageTitle = $"Edit Product - {model.Name}";
    }

    private void HandleProductDomainException(DomainException domainException, dynamic model)
    {
        switch (domainException.Error.Code)
        {
            // --- Identifiers ---
            case $"{nameof(Product)}.{nameof(Product.SKU)}":
                ModelState.AddModelError(nameof(model.SKU), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.GTIN)}":
                ModelState.AddModelError(nameof(model.GTIN), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.MPN)}":
                ModelState.AddModelError(nameof(model.MPN), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.MFC)}":
                ModelState.AddModelError(nameof(model.MFC), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.EAN)}":
                ModelState.AddModelError(nameof(model.EAN), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.UPC)}":
                ModelState.AddModelError(nameof(model.UPC), domainException.Message);
                break;

            // --- Core product ---
            case $"{nameof(Product)}.{nameof(Product.Name)}":
                ModelState.AddModelError(nameof(UnitOfMeasure.SingularName), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.Slug)}":
                ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.Display)}":
                ModelState.AddModelError(nameof(model.Display), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.Breadcrumb)}":
                ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.AnchorText)}":
                ModelState.AddModelError(nameof(model.AnchorText), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.AnchorTitle)}":
                ModelState.AddModelError(nameof(model.AnchorTitle), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.CountryOfOriginId)}":
                ModelState.AddModelError(nameof(model.CountryOfOriginId), domainException.Message);
                break;

            // --- Relations ---
            case $"{nameof(Product)}.{nameof(Product.BrandId)}":
                ModelState.AddModelError(nameof(model.BrandId), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.ProductGroupId)}":
                ModelState.AddModelError(nameof(model.ProductGroupId), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.Categories)}":
                ModelState.AddModelError(nameof(model.CategoryIds), domainException.Message);
                break;

            // --- Descriptions ---
            case $"{nameof(Product)}.{nameof(Product.ShortDescription)}":
                ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.FullDescription)}":
                ModelState.AddModelError(nameof(model.FullDescription), domainException.Message);
                break;

            // --- Flags ---
            case $"{nameof(Product)}.{nameof(Product.AllowReviews)}":
                ModelState.AddModelError(nameof(model.AllowReviews), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.ShowAvailability)}":
                ModelState.AddModelError(nameof(model.ShowAvailability), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.FreeShipping)}":
                ModelState.AddModelError(nameof(model.FreeShipping), domainException.Message);
                break;

            // --- Pricing & stock ---
            case $"{nameof(Product)}.{nameof(Product.Price)}":
                ModelState.AddModelError(nameof(model.Price), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.OldPrice)}":
                ModelState.AddModelError(nameof(model.OldPrice), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.CostPrice)}":
                ModelState.AddModelError(nameof(model.CostPrice), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.Stock)}":
                ModelState.AddModelError(nameof(model.Stock), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.MinStock)}":
                ModelState.AddModelError(nameof(model.MinStock), domainException.Message);
                break;

            // --- Shipping & quantity ---
            case $"{nameof(Product)}.{nameof(Product.AdditionalShippingCharge)}":
                ModelState.AddModelError(nameof(model.AdditionalShippingCharge), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.UnitOfMeasureId)}":
                ModelState.AddModelError(nameof(model.UnitOfMeasureId), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.QuantityPerUnitOfMeasure)}":
                ModelState.AddModelError(nameof(model.QuantityPerUnitOfMeasure), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.DeliveryWindowId)}":
                ModelState.AddModelError(nameof(model.DeliveryWindowId), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.MinOrderQuantity)}":
                ModelState.AddModelError(nameof(model.MinOrderQuantity), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.MaxOrderQuantity)}":
                ModelState.AddModelError(nameof(model.MaxOrderQuantity), domainException.Message);
                break;

            // --- Flags ---
            case $"{nameof(Product)}.{nameof(Product.IsFeatured)}":
                ModelState.AddModelError(nameof(model.IsFeatured), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.IsNew)}":
                ModelState.AddModelError(nameof(model.IsNew), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.IsBestSeller)}":
                ModelState.AddModelError(nameof(model.IsBestSeller), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.IsReturnable)}":
                ModelState.AddModelError(nameof(model.IsReturnable), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.Status)}":
                ModelState.AddModelError(nameof(model.Status), domainException.Message);
                break;

            // --- SEO / page ---
            case $"{nameof(Product)}.{nameof(Product.RedirectUrl)}":
                ModelState.AddModelError(nameof(model.RedirectUrl), domainException.Message);
                break;
            case $"{nameof(Product)}.{nameof(Product.SortOrder)}":
                ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                break;
            case $"{nameof(Page)}.{nameof(Page.H1)}":
                ModelState.AddModelError(nameof(model.H1), domainException.Message);
                break;
            case $"{nameof(Page)}.{nameof(Page.MetaTitle)}":
                ModelState.AddModelError(nameof(model.MetaTitle), domainException.Message);
                break;
            case $"{nameof(Page)}.{nameof(Page.MetaDescription)}":
                ModelState.AddModelError(nameof(model.MetaDescription), domainException.Message);
                break;
            case $"{nameof(Page)}.{nameof(Page.MetaKeywords)}":
                ModelState.AddModelError(nameof(model.MetaKeywords), domainException.Message);
                break;
        }
    }

    #endregion
}