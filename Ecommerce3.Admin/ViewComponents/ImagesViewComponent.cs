using Ecommerce3.Admin.ViewModels.Image;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewComponents;

public class ImagesViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(Type parentEntityType, int parentEntityId, Type imageEntityType,
        IReadOnlyList<ImageListItemViewModel> images)
    {
        return View(ValueTuple.Create(parentEntityType, parentEntityId, imageEntityType, images));
    }
}