using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.ViewModels.Image;

public record AddImageViewModel
{
    public int ParentEntityId { get; set; } //BrandId, CategoryId, ProductId etc.
    public Type ImageEntity { get; set; } //BrandImage, CategoryImage, ProductImage etc.
    public int ImageTypeId { get; set; }
    public SelectList ImageTypes { get; set; }
    public ImageSize ImageSize { get; set; }
    public IFormFile File { get; set; }
    public string? AltText { get; set; }
    public string? Title { get; set; }
    public string Loading { get; set; }
    public string? Link { get; set; }
    public string? LinkTarget { get; set; }
    public int SortOrder { get; set; }
    
    public AddImageCommand ToCommand()
    {
        return new AddImageCommand();
    }
}