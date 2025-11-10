using Ecommerce3.Contracts.DTOs.Image;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewComponents;

public class ImagesViewComponent : ViewComponent
{
    private readonly IDataProtector _dataProtector;

    public ImagesViewComponent(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtector = dataProtectionProvider.CreateProtector(nameof(ImagesViewComponent));
    }

    public async Task<IViewComponentResult> InvokeAsync(Type parentEntityType, int parentEntityId, Type imageEntityType,
        IReadOnlyList<ImageDTO> images)
    {
        return View(ValueTuple.Create(
            _dataProtector.Protect(parentEntityType.AssemblyQualifiedName!),
            _dataProtector.Protect(parentEntityId.ToString()),
            _dataProtector.Protect(imageEntityType.AssemblyQualifiedName!),
            images));
    }
}