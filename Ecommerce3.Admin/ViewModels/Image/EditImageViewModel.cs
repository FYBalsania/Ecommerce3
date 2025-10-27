using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.ViewModels.Image;

public record EditImageViewModel()
{
    public required int ImageTypeId { get; init; }
    public SelectList ImageTypes { get; init; }
}