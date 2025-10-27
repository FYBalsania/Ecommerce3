using Ecommerce3.Admin.ViewModels.Image;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewComponents;

public class ImagesViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(Type imageType, IReadOnlyList<ImageListItemViewModel> images,
        int parentEntityId)
    {
        return View(images);
    }
}