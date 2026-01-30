using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.StoreFront.Controllers;

public class ProductsController(
    IConfiguration configuration,
    ICategoryQueryRepository categoryQueryRepository,
    IProductQueryRepository productQueryRepository) : Controller
{
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetByParams(int category, int[] brands, decimal? minPrice, decimal? maxPrice,
        List<KeyValuePair<int, decimal>>? weights, IDictionary<int, int>? attributes, int pageNumber, SortOrder sortOrder,
        CancellationToken cancellationToken)
    {
        weights ??= [];
        attributes ??= new Dictionary<int, int>();

        var pageSize = configuration.GetValue<int>("PLPSize");
        var descendantIds = await categoryQueryRepository.GetDescendantIdsAsync(category, cancellationToken);
        var pagedResult = await productQueryRepository.GetListItemsAsync(descendantIds, brands,
            minPrice, maxPrice, weights, attributes, sortOrder, pageNumber, pageSize, cancellationToken);

        return pageNumber == 1
            ? PartialView("_PLPProductListPartial", pagedResult)
            : PartialView("_PLPProductListItemsPartial", pagedResult.Data);
    }
}