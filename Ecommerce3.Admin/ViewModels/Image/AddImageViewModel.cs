using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.ViewModels.Image;

public record AddImageViewModel
{
    public int ParentEntityId { get; set; } //BrandId, CategoryId, ProductId etc.
    public required Type ImageEntity { get; init; } //BrandImage, CategoryImage, ProductImage etc.
    public required int ImageTypeId { get; init; }
    public SelectList ImageTypes { get; init; }
    public required ImageSize ImageSize { get; init; }
    public required IFormFile File { get; init; }
    public string? AltText { get; private init; }
    public string? Title { get; private init; }
    public required string Loading { get; init; }
    public string? Link { get; private init; }
    public string? LinkTarget { get; private init; }
    public int SortOrder { get; private init; }
    public AddImageCommand ToCommand()
    {
        return new AddImageCommand();
    }
}