using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.Domain.Enums;
using Ecommerce3.StoreFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.StoreFront.Controllers;

[Route("{category0Slug}")]
public class CategoriesController(
    ICategoryQueryRepository categoryQueryRepository,
    IBrandQueryRepository brandQueryRepository,
    IPageQueryRepository pageQueryRepository,
    IProductQueryRepository productQueryRepository,
    IConfiguration configuration) : Controller
{
    [HttpGet("c")]
    public async Task<IActionResult> CategoryLevel0(string category0Slug, int[] brands, decimal? minPrice,
        decimal? maxPrice, IDictionary<int, decimal>? weights, IDictionary<int, int>? attributes,
        SortOrder sortOrder = SortOrder.NameAsc, CancellationToken cancellationToken = default)
    {
        weights ??= new Dictionary<int, decimal>();
        attributes ??= new Dictionary<int, int>();
        var pageSize = configuration.GetValue<int>("PLPSize");

        //Category with children.
        var category = await categoryQueryRepository.GetWithChildrenBySlugAsync(category0Slug, cancellationToken);
        if (category is null) return NotFound();

        //Category page.
        var page = await pageQueryRepository.GetByCategoryIdAsync(category.Id, cancellationToken);

        //Descendant category Ids.
        var descendantIds = await categoryQueryRepository.GetDescendantIdsAsync(category.Id, cancellationToken);

        //Brands by descendant category Ids.
        var brandCheckBoxListItems = await brandQueryRepository.GetByCategoryIdsAsync(descendantIds, cancellationToken);

        //Products.
        var products = await productQueryRepository.GetListAsync(descendantIds, brands, minPrice, maxPrice,
            weights, attributes, sortOrder, 1, pageSize, cancellationToken);
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