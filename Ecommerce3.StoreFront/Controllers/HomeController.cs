using System.Diagnostics;
using Ecommerce3.Application.Services.StoreFront.Interfaces;
using Ecommerce3.StoreFront.Models;
using Ecommerce3.StoreFront.Options;
using Ecommerce3.StoreFront.ViewModels.Home;
using Ecommerce3.StoreFront.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce3.StoreFront.Controllers;

public class HomeController(
    IPageService pageService,
    IProductService productService,
    ICategoryService categoryService,
    ILogger<HomeController> logger)
    : Controller
{
    public async Task<IActionResult> Index([FromServices] IOptions<List<ProductCollections>> productCollections,
        CancellationToken cancellationToken)
    {
        //Page.
        var page = await pageService.GetByPathAsync("/", cancellationToken);
        if (page is null) return NotFound();
        
        //Categories.
        var categories = await categoryService.GetListItemsAsync(cancellationToken);

        //Products.
        var productSKUs = productCollections.Value.SelectMany(x => x.ProductSKUs).Distinct().ToArray();
        var products = await productService.GetListAsync(productSKUs,  cancellationToken);

        // Build lookup for fast SKU → Product access
        var productLookup = products.ToDictionary(p => p.SKU, p => p);

        // Build final list
        var result = productCollections.Value
            .Select(pc => new ProductCollectionViewModel
            {
                Name = pc.Name,
                Products = pc.ProductSKUs
                    .Where(sku => productLookup.TryGetValue(sku, out _))
                    .Select(sku => productLookup[sku])
                    .ToList()
            })
            .ToList();

        return View(new IndexViewModel { Page = page, ProductCollections = result, CategoryListItemDTOs = categories});
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}