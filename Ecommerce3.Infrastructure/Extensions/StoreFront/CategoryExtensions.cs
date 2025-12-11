using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.StoreFront.Category;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.StoreFront;

public static class CategoryExtensions
{
    public static readonly Expression<Func<Category, CategoryListItemDTO>> DTOExpression = x => new CategoryListItemDTO
    {
        Id = x.Id,
        Name = x.Name,
        Slug = x.Slug,
        Display = x.Display,
        Breadcrumb = x.Breadcrumb,
        AnchorText = x.AnchorText,
        AnchorTitle = x.AnchorTitle,
        Images = x.Images.AsQueryable()
            .OrderBy(y => y.ImageType!.Name)
            .ThenBy(z => z.SortOrder)
            .Select(ImageExtensions.DTOExpression).ToList()
    };

    public static IQueryable<CategoryListItemDTO> ProjectToDTO(this IQueryable<Category> query) =>
        query.Select(DTOExpression);
}