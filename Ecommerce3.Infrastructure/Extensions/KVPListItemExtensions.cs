using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.KVPListItem;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions;

public static class KVPListItemExtensions
{
    public static readonly Expression<Func<KVPListItem, KVPListItemDTO>> ToDtoExpression = x => new KVPListItemDTO
    {
        Id = x.Id,
        Type = x.Type,
        Key = x.Key,
        Value = x.Value,
        SortOrder = x.SortOrder,
        CreatedUserFullName = x.CreatedByUser!.FullName,
        CreatedAt = x.CreatedAt
    };
    
    public static IQueryable<KVPListItemDTO> ProjectToDTO(this IQueryable<KVPListItem> query)
        => query.Select(ToDtoExpression);
    
    public static IEnumerable<KVPListItemDTO> MapToDTO(this IEnumerable<KVPListItem> items)
    {
        var mapper = ToDtoExpression.Compile();
        return items.Select(mapper);
    }
}