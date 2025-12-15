using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.StoreFront.Category;
using Ecommerce3.Contracts.DTO.StoreFront.Image;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Expressions.StoreFront;

public static class CategoryExpressions
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
        Image = x.Images
            .Where(y => y.ImageTypeId == 2)
            .OrderBy(y => y.SortOrder)
            .Select(y => new ImageDTO
            {
                Id = y.Id,
                FileName = y.FileName,
                FileExtension = y.FileExtension,
                ImageTypeId = y.ImageTypeId,
                Size = y.Size,
                SortOrder = y.SortOrder,
                AltText = y.AltText,
                Title = y.Title,
                Loading = y.Loading,
                Link = y.Link,
                LinkTarget = y.LinkTarget
            })
            .FirstOrDefault()
    };
}