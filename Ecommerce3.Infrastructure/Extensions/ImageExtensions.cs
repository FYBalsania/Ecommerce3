using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.Image;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions;

public static class ImageExtensions
{
    public static readonly Expression<Func<Image, ImageDTO>> DTOExpression = x => new ImageDTO
    {
        Id = x.Id,
        OgFileName = x.OgFileName,
        FileName = x.FileName,
        FileExtension = x.FileExtension,
        ImageTypeId = x.ImageTypeId,
        ImageTypeName = x.ImageType!.Name,
        ImageTypeSlug = x.ImageType!.Slug,
        Size = x.Size,
        AltText = x.AltText,
        Title = x.Title,
        Loading = x.Loading,
        Link = x.Link,
        LinkTarget = x.LinkTarget,
        SortOrder = x.SortOrder,
        CreatedAppUserFullName = x.CreatedByUser!.FullName,
        CreatedAt = x.CreatedAt,
        UpdatedAppUserFullName = x.UpdatedByUser == null ? null : x.UpdatedByUser.FullName,
        UpdatedAt = x.UpdatedAt
    };

    public static IQueryable<ImageDTO> ProjectToDTO(this IQueryable<Image> query)
        => query.Select(DTOExpression);

    public static IReadOnlyList<ImageDTO> MapToDTO(this IReadOnlyList<Image> items)
        => items.Select(DTOExpression.Compile()).ToList();
}