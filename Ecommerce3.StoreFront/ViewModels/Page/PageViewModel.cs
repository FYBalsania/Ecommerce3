using Ecommerce3.Contracts.DTO.StoreFront.Page;

namespace Ecommerce3.StoreFront.ViewModels.Page;

public class PageViewModel(PageDTO page)
{
    public PageDTO Page { get; private set; } = page;
}