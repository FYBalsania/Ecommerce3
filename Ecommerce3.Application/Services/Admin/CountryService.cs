using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Admin.Country;
using Ecommerce3.Application.Services.Admin.Interfaces;
using Ecommerce3.Contracts.DTO.Admin.Country;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories.Admin;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services.Admin;

internal sealed class CountryService(
    ICountryRepository repository,
    ICountryQueryRepository queryRepository,
    IUnitOfWork unitOfWork) : ICountryService
{
    public async Task<PagedResult<CountryListItemDTO>> GetListItemsAsync(CountryFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddCountryCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CountryErrors.DuplicateName);

        exists = await queryRepository.ExistsByIso2CodeAsync(command.Iso2Code, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CountryErrors.DuplicateIso2Code);
        
        exists = await queryRepository.ExistsByIso3CodeAsync(command.Iso3Code, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CountryErrors.DuplicateIso3Code);
        
        exists = await queryRepository.ExistsByNumericCodeAsync(command.NumericCode!, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CountryErrors.DuplicateNumericCode);

        var country = new Country(command.Name, command.Iso2Code, command.Iso3Code, command.NumericCode, command.IsActive, command.SortOrder,
            command.CreatedBy, command.CreatedAt, command.CreatedByIp);
        
        await repository.AddAsync(country, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<CountryDTO?> GetByIdAsync(int id, CancellationToken cancellationToken) 
        => await queryRepository.GetByIdAsync(id, cancellationToken);
    

    public async Task EditAsync(EditCountryCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CountryErrors.DuplicateName);

        exists = await queryRepository.ExistsByIso2CodeAsync(command.Iso2Code, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CountryErrors.DuplicateIso2Code);
        
        exists = await queryRepository.ExistsByIso3CodeAsync(command.Iso3Code, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CountryErrors.DuplicateIso3Code);
        
        exists = await queryRepository.ExistsByNumericCodeAsync(command.NumericCode!, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.CountryErrors.DuplicateNumericCode);

        var country = await repository.GetByIdAsync(command.Id, CountryInclude.None, true, cancellationToken);
        if (country is null) throw new DomainException(DomainErrors.CountryErrors.InvalidId);

        country.Update(command.Name, command.Iso2Code, command.Iso3Code, command.NumericCode, command.IsActive, command.SortOrder, 
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);
        
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await queryRepository.GetMaxSortOrderAsync(cancellationToken);
    
    public async Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken)
        => await queryRepository.GetIdAndNameDictionaryAsync(cancellationToken);
}