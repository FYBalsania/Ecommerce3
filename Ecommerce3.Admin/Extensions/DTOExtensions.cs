using Ecommerce3.Admin.ViewModels;
using Ecommerce3.Application.DTOs;

namespace Ecommerce3.Admin.Extensions;

public static class DTOExtensions
{
    public static EditBrandViewModel ToViewModel(this BrandDTO dto)
    {
        return new EditBrandViewModel()
        {
            Id = dto.Id,
            Name = dto.Name,
            Slug = dto.Slug,
            Display = dto.Display,
            Breadcrumb = dto.Breadcrumb,
            AnchorText = dto.AnchorText,
            AnchorTitle = dto.AnchorTitle,
            MetaTitle = dto.MetaTitle,
            MetaDescription = dto.MetaDescription,
            MetaKeywords = dto.MetaKeywords,
            H1 = dto.H1,
            ShortDescription = dto.ShortDescription,
            FullDescription = dto.FullDescription,
        };
    }
}