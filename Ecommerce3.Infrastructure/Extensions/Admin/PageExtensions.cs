using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class PageExtensions
{
    private static readonly Expression<Func<Page, PageListItemDTO>> ListItemDTOExpression = p => new PageListItemDTO
    {
        Id = p.Id,
        Type = EF.Property<string>(p, "Discriminator"),
        Path = p.Path!,
        MetaTitle = p.MetaTitle,
        IsActive = p.IsActive,
        CreatedUserFullName = p.CreatedByUser!.FullName,
        CreatedAt = p.CreatedAt
    };
    
    public static IQueryable<PageListItemDTO> ProjectToListItemDTO(this IQueryable<Page> query)
        => query.Select(ListItemDTOExpression);
}