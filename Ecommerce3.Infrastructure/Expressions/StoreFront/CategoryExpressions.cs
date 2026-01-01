using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.StoreFront.Category;
using Ecommerce3.Contracts.DTO.StoreFront.Image;
using Ecommerce3.Contracts.DTO.StoreFront.ProductListPage;
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

    public static readonly Expression<Func<Category, PLPParentCategoryDTO>> PLPParentCategoryDTOExpression = x =>
        new PLPParentCategoryDTO
        {
            Id = x.Id,
            Name = x.Name,
            Slug = x.Slug,
            Display = x.Display,
            Breadcrumb = x.Breadcrumb,
            AnchorText = x.AnchorText,
            AnchorTitle = x.AnchorTitle,
            GoogleCategory = x.GoogleCategory,
            ShortDescription = x.ShortDescription,
            FullDescription = x.FullDescription,
            Children = x.Children
                .Where(y => y.IsActive)
                .OrderBy(y => y.Name)
                .Select(y => new PLPChildCategoryDTO
                {
                    Id = y.Id,
                    Name = y.Name,
                    Slug = y.Slug,
                    Display = y.Display,
                    Breadcrumb = y.Breadcrumb,
                    AnchorText = y.AnchorText,
                    AnchorTitle = y.AnchorTitle,
                    ListItemImage = y.Images
                        .Where(z => z.ImageTypeId == 2)
                        .OrderBy(z => z.SortOrder)
                        .Select(z => new ImageDTO
                        {
                            Id = z.Id,
                            FileName = z.FileName,
                            FileExtension = z.FileExtension,
                            ImageTypeId = z.ImageTypeId,
                            Size = z.Size,
                            SortOrder = z.SortOrder,
                            AltText = z.AltText,
                            Title = z.Title,
                            Loading = z.Loading,
                            Link = z.Link,
                            LinkTarget = z.LinkTarget
                        })
                        .FirstOrDefault()
                })
                .ToList()
        };
}