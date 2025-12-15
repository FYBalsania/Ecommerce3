using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class CategoryExtensions
{
    private static readonly Expression<Func<Category, CategoryListItemDTO>> ListItemDTOExpression = c => new CategoryListItemDTO
    {
        Id = c.Id,
        ParentName = c.Parent!.Name,
        Name = c.Name,
        Slug = c.Slug,
        SortOrder = c.SortOrder,
        IsActive = c.IsActive,
        ImageCount = c.Images.Count,
        CreatedUserFullName = c.CreatedByUser!.FullName,
        CreatedAt = c.CreatedAt
    };
    
    private static readonly Expression<Func<Category, CategoryDTO>> DTOExpression = c => new CategoryDTO
    {
        Id = c.Id,
        Name = c.Name,
        Slug = c.Slug,
        Display = c.Display,
        Breadcrumb = c.Breadcrumb,
        AnchorText = c.AnchorText,
        AnchorTitle = c.AnchorTitle,
        ParentId = c.ParentId,
        GoogleCategory = c.GoogleCategory,
        Path = c.Path,
        IsActive = c.IsActive,
        SortOrder = c.SortOrder,
        ShortDescription = c.ShortDescription,
        FullDescription = c.FullDescription,
        H1 = c.Page!.H1,
        MetaTitle = c.Page!.MetaTitle,
        MetaDescription = c.Page!.MetaDescription,
        MetaKeywords = c.Page!.MetaKeywords,
        Images = c.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
            .Select(ImageExtensions.DTOExpression).ToList()
    };
    
    public static IQueryable<CategoryDTO> ProjectToDTO(this IQueryable<Category> query) =>
        query.Select(DTOExpression);
    
    public static IQueryable<CategoryListItemDTO> ProjectToListItemDTO(this IQueryable<Category> query) =>
        query.Select(ListItemDTOExpression);
}