using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.UnitOfMeasure;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class UnitOfMeasureExtensions
{
    private static readonly Expression<Func<UnitOfMeasure, UnitOfMeasureListItemDTO>> ListItemDTOExpression = uom => new UnitOfMeasureListItemDTO
    {
        Id = uom.Id,
        Code = uom.Code,
        Name = uom.SingularName,
        Type = uom.Type,
        BaseName = uom.Base!.SingularName,
        ConversionFactor = uom.ConversionFactor,
        IsActive = uom.IsActive,
        CreatedUserFullName = uom.CreatedByUser!.FullName,
        CreatedAt = uom.CreatedAt
    };
    
    private static readonly Expression<Func<UnitOfMeasure, UnitOfMeasureDTO>> DTOExpression = uom => new UnitOfMeasureDTO
    {
        Id = uom.Id,
        Code = uom.Code,
        Name = uom.SingularName,
        Type = uom.Type,
        BaseId = uom.BaseId,
        ConversionFactor = uom.ConversionFactor,
        IsActive = uom.IsActive
    };
    
    public static IQueryable<UnitOfMeasureDTO> ProjectToDTO(this IQueryable<UnitOfMeasure> query) =>
        query.Select(DTOExpression);
    
    public static IQueryable<UnitOfMeasureListItemDTO> ProjectToListItemDTO(this IQueryable<UnitOfMeasure> query) =>
        query.Select(ListItemDTOExpression);
}