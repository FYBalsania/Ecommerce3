using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.DeliveryWindow;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class DeliveryWindowExtensions
{
    private static readonly Expression<Func<DeliveryWindow, DeliveryWindowListItemDTO>> ListItemDTOExpression = dw => new DeliveryWindowListItemDTO
    {
        Id = dw.Id,
        Name = dw.Name,
        Unit = dw.Unit,
        MinValue = dw.MinValue,
        MaxValue = dw.MaxValue,
        NormalizedMinDays = dw.NormalizedMinDays,
        NormalizedMaxDays = dw.NormalizedMaxDays,
        SortOrder = dw.SortOrder,
        IsActive = dw.IsActive,
        CreatedUserFullName = dw.CreatedByUser!.FullName,
        CreatedAt = dw.CreatedAt
    };
    
    private static readonly Expression<Func<DeliveryWindow, DeliveryWindowDTO>> DTOExpression = dw => new DeliveryWindowDTO
    {
        Id = dw.Id,
        Name = dw.Name,
        Unit = dw.Unit,
        MinValue = dw.MinValue,
        MaxValue = dw.MaxValue,
        SortOrder = dw.SortOrder,
        IsActive = dw.IsActive,
    };
    
    public static IQueryable<DeliveryWindowDTO> ProjectToDTO(this IQueryable<DeliveryWindow> query) =>
        query.Select(DTOExpression);
    
    public static IQueryable<DeliveryWindowListItemDTO> ProjectToListItemDTO(this IQueryable<DeliveryWindow> query) =>
        query.Select(ListItemDTOExpression);
}