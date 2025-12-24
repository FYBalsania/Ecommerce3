using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.ViewModels.Common;

public record SelectListViewModel
{
    public required int Id { get; init; }
    public required string Text { get; init; }
    public required SelectList Values { get; init; }
}