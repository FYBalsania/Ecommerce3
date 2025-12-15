using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.PostCode;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class PostCodeExtensions
{
    private static readonly Expression<Func<PostCode, PostCodeListItemDTO>> ListItemDTOExpression = pc => new PostCodeListItemDTO
    {
        Id = pc.Id,
        Code = pc.Code,
        IsActive = pc.IsActive,
        CreatedUserFullName = pc.CreatedByUser!.FullName,
        CreatedAt = pc.CreatedAt
    };
    
    private static readonly Expression<Func<PostCode, PostCodeDTO>> DTOExpression = pc => new PostCodeDTO
    {
        Id = pc.Id,
        Code = pc.Code,
        IsActive = pc.IsActive,
    };
    
    public static IQueryable<PostCodeDTO> ProjectToDTO(this IQueryable<PostCode> query) =>
        query.Select(DTOExpression);
    
    public static IQueryable<PostCodeListItemDTO> ProjectToListItemDTO(this IQueryable<PostCode> query) =>
        query.Select(ListItemDTOExpression);
}