using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.ProductGroup;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class ProductGroupExtensions
{
    private static readonly Expression<Func<ProductGroup, ProductGroupListItemDTO>> ListItemDTOExpression = pg => new ProductGroupListItemDTO
    {
        Id = pg.Id,
        Name = pg.Name,
        Slug = pg.Slug,
        SortOrder = pg.SortOrder,
        IsActive = pg.IsActive,
        ImageCount = pg.Images.Count,
        CreatedUserFullName = pg.CreatedByUser!.FullName,
        CreatedAt = pg.CreatedAt
    };
    
    private static readonly Expression<Func<ProductGroup, ProductGroupDTO>> DTOExpression = pg => new ProductGroupDTO
    {
        Id = pg.Id,
        Name = pg.Name,
        Slug = pg.Slug,
        Display = pg.Display,
        Breadcrumb = pg.Breadcrumb,
        AnchorText = pg.AnchorText,
        AnchorTitle = pg.AnchorTitle,
        IsActive = pg.IsActive,
        SortOrder = pg.SortOrder,
        ShortDescription = pg.ShortDescription,
        FullDescription = pg.FullDescription,
        H1 = pg.Page!.H1!,
        MetaTitle = pg.Page!.MetaTitle,
        MetaDescription = pg.Page!.MetaDescription,
        MetaKeywords = pg.Page!.MetaKeywords,
        Images = pg.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
            .Select(ImageExtensions.DTOExpression).ToList()
    };
    
    public static IQueryable<ProductGroupDTO> ProjectToDTO(this IQueryable<ProductGroup> query) =>
        query.Select(DTOExpression);
    
    public static IQueryable<ProductGroupListItemDTO> ProjectToListItemDTO(this IQueryable<ProductGroup> query) =>
        query.Select(ListItemDTOExpression);
}