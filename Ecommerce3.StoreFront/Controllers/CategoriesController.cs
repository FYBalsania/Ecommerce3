using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.StoreFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.StoreFront.Controllers;

[Route("{category0Slug}")]
public class CategoriesController(ICategoryQueryRepository categoryQueryRepository) : Controller
{
    [HttpGet("c")]
    public async Task<IActionResult> CategoryLevel0(string category0Slug, string[]? brands, decimal? minPrice,
        decimal? maxPrice, IDictionary<string, decimal>? weights, IDictionary<string, string>? attributes,
        CancellationToken cancellationToken)
    {
        //Category.
        var category = await categoryQueryRepository.GetWithChildrenBySlugAsync(category0Slug, cancellationToken);
        if (category is null) return NotFound();

        //Brands.
        //Products.
        //Weights.

        //Breadcrumb.
        var breadcrumb = new List<BreadcrumbItem>
        {
            new()
            {
                Text = "Home",
                Url = Url.Action(nameof(HomeController.Index),
                    nameof(HomeController).Replace("Controller", string.Empty))
            },
            new() { Text = category.Breadcrumb }
        };

        return View();
    }

    [HttpGet("{category1Slug}/c")]
    public async Task<IActionResult> CategoryLevel1(string category0Slug, string category1Slug,
        CancellationToken cancellationToken)
    {
        await Task.Delay(0, cancellationToken);
        return View();
    }

    [HttpGet("{category1Slug}/{category2Slug}/c")]
    public async Task<IActionResult> CategoryLevel2(string category0Slug, string category1Slug, string category2Slug,
        CancellationToken cancellationToken)
    {
        await Task.Delay(0, cancellationToken);
        return View();
    }

    [HttpGet("{category1Slug}/{category2Slug}/{category3Slug}/c")]
    public async Task<IActionResult> CategoryLevel3(string category0Slug, string category1Slug, string category2Slug,
        string category3Slug, CancellationToken cancellationToken)
    {
        await Task.Delay(0, cancellationToken);
        return View();
    }
}