using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions;

public static class TextListItemExtensions
{
    private static readonly Expression<Func<TextListItem, TextListItemDTO>> ToDtoExpression = x => new TextListItemDTO
    {
        Id = x.Id,
        Text = x.Text,
        SortOrder = x.SortOrder,
        // be defensive: use null-conditional to avoid null nav prop issues
        CreatedAppUserFullName = x.CreatedByUser!.FullName,
        CreatedAt = x.CreatedAt
    };
    
    public static IQueryable<TextListItemDTO> ProjectToDTO(this IQueryable<TextListItem> query)
        => query.Select(ToDtoExpression);
    
    public static IEnumerable<TextListItemDTO> MapToDTO(this IEnumerable<TextListItem> items)
    {
        var mapper = ToDtoExpression.Compile();
        return items.Select(mapper);
    }
}