using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.StoreFront.Image;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Expressions.StoreFront;

public static class ImageExpressions
{
    public static readonly Expression<Func<Image, ImageDTO>> DTOExpression = i => new ImageDTO
    {
        Id = i.Id,
        FileName = i.FileName,
        FileExtension = i.FileExtension,
        ImageTypeId = i.ImageTypeId,
        Size = i.Size,
        AltText = i.AltText,
        Title = i.Title,
        Loading = i.Loading,
        Link = i.Link,
        LinkTarget = i.LinkTarget,
        SortOrder = i.SortOrder
    };
}