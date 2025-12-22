using Ecommerce3.Admin.ViewModels.Product;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTO.API.ProductGroup;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    private readonly IProductGroupService _productGroupService;
    private readonly IUnitOfMeasureService _unitOfMeasureService;
    private readonly IDeliveryWindowService _deliveryWindowService;

    public ProductsController(IProductService productService, IIPAddressService ipAddressService,
        IConfiguration configuration, IBrandService brandService,
        ICategoryService categoryService, IProductGroupService productGroupService,
        IUnitOfMeasureService unitOfMeasureService, IDeliveryWindowService deliveryWindowService)
    {
        _productService = productService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
        _brandService = brandService;
        _categoryService = categoryService;
        _productGroupService = productGroupService;
        _unitOfMeasureService = unitOfMeasureService;
        _deliveryWindowService = deliveryWindowService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(ProductFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await _productService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new ProductsIndexViewModel()
        {
            Filter = filter,
            Products = result,
            PageTitle = "Products"
        };

        ViewData["Title"] = "Products";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        var sortOrder = await _productService.GetMaxSortOrderAsync(cancellationToken);
        var brands = new SelectList(await _brandService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        var categories = new SelectList(await _categoryService.GetIdAndNameListAsync(null, cancellationToken), "Key", "Value");
        var productGroups = new SelectList(await _productGroupService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        var uoms = new SelectList(await _unitOfMeasureService.GetIdAndNameDictionaryAsync(null, false, cancellationToken), "Key", "Value");
        var deliveryWindows = new SelectList(await _deliveryWindowService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");

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
            DeliveryWindows = deliveryWindows
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProductViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove(nameof(AddProductViewModel.PageTitle));
        ModelState.Remove(nameof(AddProductViewModel.Brands));
        ModelState.Remove(nameof(AddProductViewModel.Categories));
        ModelState.Remove(nameof(AddProductViewModel.ProductGroups));
        ModelState.Remove(nameof(AddProductViewModel.UnitOfMeasures));
        ModelState.Remove(nameof(AddProductViewModel.DeliveryWindows));
        if (!ModelState.IsValid)
        {
            await PopulateViewModelForAdd(model, cancellationToken);
            return View(model);
        }

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var command = model.ToCommand(userId, createdAt, ipAddress);
        try
        {
            await _productService.AddAsync(command, cancellationToken);
        }
        catch (DomainException domainException)
        {
            HandleProductDomainException(domainException, model);
            await PopulateViewModelForAdd(model, cancellationToken);
            return View(model);
        }

        return LocalRedirect("/Products/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        if (product is null) return NotFound();
        
        var model = EditProductViewModel.FromDTO(product);
        model.Brands = new SelectList(await _brandService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.Categories = new SelectList(await _categoryService.GetIdAndNameListAsync(null, cancellationToken), "Key", "Value");
        model.ProductGroups = new SelectList(await _productGroupService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.UnitOfMeasures = new SelectList(await _unitOfMeasureService.GetIdAndNameDictionaryAsync(null, false, cancellationToken), "Key", "Value");
        model.DeliveryWindows = new SelectList(await _deliveryWindowService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");
        
        ViewData["Title"] = $"Edit Product - {product.Name}";
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove(nameof(model.PageTitle));
        ModelState.Remove(nameof(model.Brands));
        ModelState.Remove(nameof(model.Categories));
        ModelState.Remove(nameof(model.ProductGroups));
        ModelState.Remove(nameof(model.UnitOfMeasures));
        ModelState.Remove(nameof(model.DeliveryWindows));
        ModelState.Remove(nameof(model.DeliveryWindows));
        if (!ModelState.IsValid)
        {
            await PopulateViewModelForEdit(model, cancellationToken);
            return View(model);
        }
        
        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var command = model.ToCommand(userId, createdAt, ipAddress);
        try
        {
            await _productService.EditAsync(command, cancellationToken);
        }
        catch (DomainException domainException)
        {
            HandleProductDomainException(domainException, model);
            await PopulateViewModelForEdit(model, cancellationToken);
            return View(model);
        }
        
        return LocalRedirect("/Products/Index");
    }
    
    #region Private Helper Methods
    
    private async Task PopulateViewModelForAdd(AddProductViewModel model, CancellationToken cancellationToken)
    {
        var sortOrder = await _productService.GetMaxSortOrderAsync(cancellationToken);
        model.SortOrder = sortOrder + 1;
        model.Brands = new SelectList(await _brandService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.Categories = new SelectList(await _categoryService.GetIdAndNameListAsync(null, cancellationToken), "Key", "Value");
        model.ProductGroups = new SelectList(await _productGroupService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.UnitOfMeasures = new SelectList(await _unitOfMeasureService.GetIdAndNameDictionaryAsync(null, false, cancellationToken), "Key", "Value");
        model.DeliveryWindows = new SelectList(await _deliveryWindowService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");
        model.PageTitle = "Add Product";
    }
    
    private async Task PopulateViewModelForEdit(EditProductViewModel model, CancellationToken cancellationToken)
    {
        model.Brands = new SelectList(await _brandService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.Categories = new SelectList(await _categoryService.GetIdAndNameListAsync(null, cancellationToken), "Key", "Value");
        model.ProductGroups = new SelectList(await _productGroupService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        model.UnitOfMeasures = new SelectList(await _unitOfMeasureService.GetIdAndNameDictionaryAsync(null, false, cancellationToken), "Key", "Value");
        model.DeliveryWindows = new SelectList(await _deliveryWindowService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");
    
        var productDTO = await _productService.GetByIdAsync(model.Id, cancellationToken);
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

            case $"{nameof(Product)}.{nameof(Product.Name)}":
                ModelState.AddModelError(nameof(ProductGroupProductAttributeValueViewDTO.Value), domainException.Message);
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
            
            case $"{nameof(Product)}.{nameof(Product.SortOrder)}":
                ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.RedirectUrl)}":
                ModelState.AddModelError(nameof(model.RedirectUrl), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.ShortDescription)}":
                ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.FullDescription)}":
                ModelState.AddModelError(nameof(model.FullDescription), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.AllowReviews)}":
                ModelState.AddModelError(nameof(model.AllowReviews), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.ShowAvailability)}":
                ModelState.AddModelError(nameof(model.ShowAvailability), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.FreeShipping)}":
                ModelState.AddModelError(nameof(model.FreeShipping), domainException.Message);
                break;
            
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

            case $"{nameof(Product)}.{nameof(Product.BrandId)}":
                ModelState.AddModelError(nameof(model.BrandId), domainException.Message);
                break;

            case $"{nameof(Product)}.{nameof(Product.Categories)}":
                ModelState.AddModelError(nameof(model.CategoryIds), domainException.Message);
                break;

            case $"{nameof(Product)}.{nameof(Product.Price)}":
                ModelState.AddModelError(nameof(model.Price), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.OldPrice)}":
                ModelState.AddModelError(nameof(model.OldPrice), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.CostPrice)}":
                ModelState.AddModelError(nameof(model.CostPrice), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.MinOrderQuantity)}":
                ModelState.AddModelError(nameof(model.MinOrderQuantity), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.MaxOrderQuantity)}":
                ModelState.AddModelError(nameof(model.MaxOrderQuantity), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.Stock)}":
                ModelState.AddModelError(nameof(model.Stock), domainException.Message);
                break;
            
            case $"{nameof(Product)}.{nameof(Product.MinStock)}":
                ModelState.AddModelError(nameof(model.MinStock), domainException.Message);
                break;
            
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
            
            case $"{nameof(ProductPage)}.{nameof(ProductPage.H1)}":
                ModelState.AddModelError(nameof(model.H1), domainException.Message);
                break;
            
            case $"{nameof(ProductPage)}.{nameof(ProductPage.MetaTitle)}":
                ModelState.AddModelError(nameof(model.MetaTitle), domainException.Message);
                break;

            default:
                ModelState.AddModelError(string.Empty, domainException.Message);
                break;
        }
    }
    
    #endregion
}