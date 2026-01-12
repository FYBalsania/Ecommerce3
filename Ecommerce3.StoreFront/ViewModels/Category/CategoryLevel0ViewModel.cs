using System.Globalization;
using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.StoreFront.Page;
using Ecommerce3.Contracts.DTO.StoreFront.Product;
using Ecommerce3.Contracts.DTO.StoreFront.ProductListPage;
using Ecommerce3.Contracts.DTO.StoreFront.UOM;
using Ecommerce3.StoreFront.Models;
using Ecommerce3.StoreFront.ViewModels.Common;
using Ecommerce3.StoreFront.ViewModels.Page;

namespace Ecommerce3.StoreFront.ViewModels.Category;

public class CategoryLevel0ViewModel : PageViewModel
{
    public PLPParentCategoryDTO Category { get; private init; }
    public IReadOnlyList<BreadcrumbItem> Breadcrumb { get; private init; }
    public IReadOnlyList<CheckBoxListItemViewModel> Brands { get; private init; }
    public PriceRangeViewModel PriceRange { get; private init; }
    public IReadOnlyList<CheckBoxListItemViewModel> Weights { get; private init; }

    public IDictionary<KeyValuePair<int, string>, IReadOnlyList<CheckBoxListItemViewModel>> Attributes
    {
        get;
        private init;
    }

    public PagedResult<ProductListItemDTO> Products { get; private init; }

    public CategoryLevel0ViewModel(PLPParentCategoryDTO category, IReadOnlyList<BreadcrumbItem> breadcrumb,
        PageDTO page, IReadOnlyDictionary<int, string> brands, int[] selectedBrands,
        PriceRangeDTO priceRange, decimal? selectedMinPrice, decimal? selectedMaxPrice,
        IReadOnlyList<UOMFacetDTO> weights, IDictionary<int, decimal> selectedWeights,
        IReadOnlyList<ProductAttributeFacetDTO> attributes, IDictionary<int, int> selectedAttributes,
        PagedResult<ProductListItemDTO> products) 
        : base(page)
    {
        Category = category;
        Breadcrumb = breadcrumb;
        Brands = brands.Select(x => new CheckBoxListItemViewModel
        {
            Id = x.Key,
            Text = x.Value,
            IsSelected = selectedBrands.Contains(x.Key)
        }).ToList();
        PriceRange = new PriceRangeViewModel
        {
            PriceRange = priceRange,
            SelectedMinPrice = selectedMinPrice,
            SelectedMaxPrice = selectedMaxPrice
        };
        Weights = weights.Select(x =>
        {
            var isSelected = selectedWeights.TryGetValue(x.Id, out var qty) && qty == x.QtyPerUOM;
            return new CheckBoxListItemViewModel
            {
                Id = x.Id,
                Text = $"{x.QtyPerUOM} {x.Name}",
                IsSelected = isSelected,
                Tags = [x.QtyPerUOM.ToString(CultureInfo.InvariantCulture), x.Name]
            };
        }).ToList();
        Products = products;
        Attributes = attributes
            .GroupBy(a => new KeyValuePair<int, string>(a.AttributeId, a.AttributeDisplay))
            .ToDictionary(
                g => g.Key,
                g => (IReadOnlyList<CheckBoxListItemViewModel>)g.Select(a =>
                    new CheckBoxListItemViewModel
                    {
                        Id = a.AttributeValueId,
                        Text = a.AttributeValueDisplay,
                        IsSelected = selectedAttributes.TryGetValue(a.AttributeId, out var selectedValueId)
                                     && selectedValueId == a.AttributeValueId
                    }).ToList()
            );
    }
}