using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.StoreFront.Image;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.StoreFront;

public static class ImageExtensions
{
    // Expression-only projection (EF Core can translate this)
    public static readonly Expression<Func<Image, ImageDTO>> DTOExpression = i => new ImageDTO
    {
        Id = i.Id,
        FileName = i.FileName,
        FileExtension = i.FileExtension,
        ImageTypeId = i.ImageTypeId,
        // Null-safe access: if ImageType is null, return null (ImageDTO props are nullable where appropriate)
        // ImageTypeName = i.ImageType != null ? i.ImageType.Name : null!,
        // ImageTypeSlug = i.ImageType != null ? i.ImageType.Slug : null!,
        ImageTypeName = i.ImageType!.Name,
        ImageTypeSlug = i.ImageType!.Slug,
        Size = i.Size,
        AltText = i.AltText,
        Title = i.Title,
        Loading = i.Loading,
        Link = i.Link,
        LinkTarget = i.LinkTarget,
        SortOrder = i.SortOrder
    };

    // IQueryable<> projection that EF Core can translate to SQL
    public static IQueryable<ImageDTO> ProjectToDTO(this IQueryable<Image> query)
        => query.Select(DTOExpression);

    // In-memory mapping helper — compiles the expression (use only after materialization)
    public static IReadOnlyList<ImageDTO> MapToDTO(this IEnumerable<Image> items)
    {
        var mapper = DTOExpression.Compile();
        return items.Select(mapper).ToList();
    }
}