using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.Bank;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class BankExtensions
{
    private static readonly Expression<Func<Bank, BankListItemDTO>> ListItemDTOExpression = b => new BankListItemDTO
    {
        Id = b.Id,
        Name = b.Name,
        Slug = b.Slug,
        SortOrder = b.SortOrder,
        IsActive = b.IsActive,
        ImageCount = b.Images.Count,
        CreatedUserFullName = b.CreatedByUser!.FullName,
        CreatedAt = b.CreatedAt
    };
    
    private static readonly Expression<Func<Bank, BankDTO>> DTOExpression = b => new BankDTO
    {
        Id = b.Id,
        Name = b.Name,
        Slug = b.Slug,
        IsActive = b.IsActive,
        SortOrder = b.SortOrder,
        Images = b.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
            .Select(ImageExtensions.DTOExpression).ToList()
    };
    
    public static IQueryable<BankDTO> ProjectToDTO(this IQueryable<Bank> query) =>
        query.Select(DTOExpression);
    
    public static IQueryable<BankListItemDTO> ProjectToListItemDTO(this IQueryable<Bank> query) =>
        query.Select(ListItemDTOExpression);
}