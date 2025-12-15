using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class BrandExtensions
{
    private static readonly Expression<Func<Brand, BrandListItemDTO>> ListItemDTOExpression = b => new BrandListItemDTO
    {
        Id = b.Id,
        Name = b.Name,
        Slug = b.Slug,
        SortOrder = b.SortOrder,
        IsActive = b.IsActive,
        ImageCount = b.Images.Count,
        CreatedUserFullName = b.CreatedByUser!.FullName,
        CreatedAt = b.CreatedAt
    };
    
    private static readonly Expression<Func<Brand, BrandDTO>> DTOExpression = b => new BrandDTO
    {
        Id = b.Id,
        Name = b.Name,
        Slug = b.Slug,
        Display = b.Display,
        Breadcrumb = b.Breadcrumb,
        AnchorText = b.AnchorText,
        AnchorTitle = b.AnchorTitle,
        IsActive = b.IsActive,
        SortOrder = b.SortOrder,
        ShortDescription = b.ShortDescription,
        FullDescription = b.FullDescription,
        H1 = b.Page!.H1,
        MetaTitle = b.Page.MetaTitle,
        MetaDescription = b.Page.MetaDescription,
        MetaKeywords = b.Page.MetaKeywords,
        Images = b.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
            .Select(ImageExtensions.DTOExpression).ToList()
    };
    
    public static IQueryable<BrandDTO> ProjectToDTO(this IQueryable<Brand> query) =>
        query.Select(DTOExpression);
    
    public static IQueryable<BrandListItemDTO> ProjectToListItemDTO(this IQueryable<Brand> query) =>
        query.Select(ListItemDTOExpression);
}