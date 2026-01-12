using Ecommerce3.Contracts.DTO.StoreFront.Category;
using Ecommerce3.Contracts.DTO.StoreFront.Page;
using Ecommerce3.StoreFront.ViewModels.Page;
using Ecommerce3.StoreFront.ViewModels.Product;

namespace Ecommerce3.StoreFront.ViewModels.Home;

public class IndexViewModel(
    PageDTO page,
    IReadOnlyList<ProductCollectionViewModel> productCollections,
    IReadOnlyList<CategoryListItemDTO> categoryListItemDTOs)
    : PageViewModel(page)
{
    public IReadOnlyList<ProductCollectionViewModel> ProductCollections { get; private set; } = productCollections;
    public IReadOnlyList<CategoryListItemDTO> CategoryListItemDTOs { get; private set; } = categoryListItemDTOs;
}