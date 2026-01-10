using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.Admin.Country;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.Admin;

public static class CountryExtensions
{
    private static readonly Expression<Func<Country, CountryListItemDTO>> ListItemDTOExpression = c => new CountryListItemDTO
    {
        Id = c.Id,
        Name = c.Name,
        Iso2Code =  c.Iso2Code,
        Iso3Code =  c.Iso3Code,
        NumericCode =  c.NumericCode,
        SortOrder = c.SortOrder,
        IsActive = c.IsActive,
        CreatedUserFullName = c.CreatedByUser!.FullName,
        CreatedAt = c.CreatedAt
    };
    
    private static readonly Expression<Func<Country, CountryDTO>> DTOExpression = c => new CountryDTO
    {
        Id = c.Id,
        Name = c.Name,
        Iso2Code = c.Iso2Code,
        Iso3Code = c.Iso3Code,
        NumericCode =  c.NumericCode,
        IsActive = c.IsActive,
        SortOrder = c.SortOrder,
    };
    
    public static IQueryable<CountryDTO> ProjectToDTO(this IQueryable<Country> query) =>
        query.Select(DTOExpression);
    
    public static IQueryable<CountryListItemDTO> ProjectToListItemDTO(this IQueryable<Country> query) =>
        query.Select(ListItemDTOExpression);
}