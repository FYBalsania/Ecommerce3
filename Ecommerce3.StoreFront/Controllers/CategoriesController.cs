using Ecommerce3.Contracts.DTO.StoreFront.Product;
using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.Domain.Enums;
using Ecommerce3.StoreFront.Models;
using Ecommerce3.StoreFront.ViewModels.Category;
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

        //Brands for facets.
        var brandIdAndDisplay =
            await brandQueryRepository.GetIdAndDisplayByCategoryIdsAsync(descendantIds, cancellationToken);

        //Price range for facets.
        var priceRange = await productQueryRepository.GetPriceRangeAsync(descendantIds, cancellationToken);

        //Weight range for facets.
        var weightFacets = await productQueryRepository.GetWeightsAsync(descendantIds, cancellationToken);

        //Attributes for facets.
        var attributeFacets = await productQueryRepository.GetAttributesAsync(descendantIds, cancellationToken);

        //Products.
        var products = await productQueryRepository.GetListItemsAsync(descendantIds, brands, 
            minPrice, maxPrice, weights, attributes, sortOrder, 1, pageSize, cancellationToken);

        //Breadcrumb.
        var breadcrumb = new List<BreadcrumbItem>
        {
            new() { Text = category.Breadcrumb }
        };

        //Model.
        var model = new CategoryLevel0ViewModel(category, breadcrumb, page!, brandIdAndDisplay, brands,
            priceRange, minPrice, maxPrice, weightFacets, weights, attributeFacets, attributes, products);

        return View(model);
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
    
    // private static decimal GetStepSize(decimal max)
    // {
    //     if (max <= 50) return 10;
    //     if (max <= 200) return 25;
    //     if (max <= 500) return 50;
    //     if (max <= 2000) return 100;
    //     if (max <= 10000) return 500;
    //     return 1000;
    // }
    //
    // public static List<PriceRangeDTO> GenerateAutoPriceRanges(decimal minPrice, decimal maxPrice)
    // {
    //     var step = GetStepSize(maxPrice);
    //     var ranges = new List<PriceRangeDTO>();
    //
    //     // First upper bound → next step - 0.01 (ex: 9.99, 24.99, 49.99)
    //     decimal firstUpper = Math.Floor(minPrice / step) * step + (step - 0.01m);
    //
    //     if (firstUpper >= maxPrice)
    //     {
    //         ranges.Add(new PriceRangeDTO() { From = minPrice, To = maxPrice });
    //         return ranges;
    //     }
    //
    //     // First bucket
    //     ranges.Add(new PriceRange
    //     {
    //         From = minPrice,
    //         To = firstUpper
    //     });
    //
    //     decimal currentFrom = firstUpper + 0.01m;
    //
    //     // Middle buckets
    //     while (currentFrom + (step - 0.01m) < maxPrice)
    //     {
    //         ranges.Add(new PriceRange
    //         {
    //             From = currentFrom,
    //             To = currentFrom + (step - 0.01m)
    //         });
    //
    //         currentFrom += step;
    //     }
    //
    //     // Final bucket
    //     ranges.Add(new PriceRange
    //     {
    //         From = currentFrom,
    //         To = maxPrice
    //     });
    //
    //     return ranges;
    // }
}