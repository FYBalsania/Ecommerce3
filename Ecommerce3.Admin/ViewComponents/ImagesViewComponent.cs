using Ecommerce3.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewComponents;

public class ImagesViewComponent : ViewComponent
{
    public ImagesViewComponent()
    {
    }

    public async Task<IViewComponentResult> InvokeAsync(EntityWithImages entity, int entityId,
        CancellationToken cancellationToken)
    {
        return View();
    }
}