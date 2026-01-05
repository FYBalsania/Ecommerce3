using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.ImageType;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class ImageTypesExtensions
{
    private static readonly Expression<Func<ImageType, ImageTypeListItemDTO>> ListItemDTOExpression = it => new ImageTypeListItemDTO
    {
        Id = it.Id,
        Entity = it.Entity,
        Name = it.Name,
        Slug = it.Slug,
        IsActive = it.IsActive,
        CreatedUserFullName = it.CreatedByUser!.FullName,
        CreatedAt = it.CreatedAt
    };
    
    private static readonly Expression<Func<ImageType, ImageTypeDTO>> DTOExpression = it => new ImageTypeDTO
    {
        Id = it.Id,
        Entity = it.Entity,
        Name = it.Name,
        Slug = it.Slug,
        Description = it.Description,
        IsActive = it.IsActive,
    };
    
    public static IQueryable<ImageTypeDTO> ProjectToDTO(this IQueryable<ImageType> query) =>
        query.Select(DTOExpression);
    
    public static IQueryable<ImageTypeListItemDTO> ProjectToListItemDTO(this IQueryable<ImageType> query) =>
        query.Select(ListItemDTOExpression);
}