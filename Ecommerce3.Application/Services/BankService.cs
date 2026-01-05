using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Bank;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Bank;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class BankService(
    IBankRepository repository,
    IBankQueryRepository queryRepository,
    IBankPageRepository pageRepository,
    IUnitOfWork unitOfWork) : IBankService
{
    public async Task<PagedResult<BankListItemDTO>> GetListItemsAsync(BankFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddBankCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.BankErrors.DuplicateName);

        exists = await queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.BankErrors.DuplicateSlug);

        var bank = new Bank(command.Name, command.Slug, command.IsActive, command.SortOrder,
            command.CreatedBy, command.CreatedByIp);
        
        var page = new BankPage($"/{command.Slug}/b", command.MetaTitle, command.MetaDescription, command.MetaKeywords,
            null, command.H1, null, null, null, null, null, null, null, null,
            null, null, null, 0, SiteMapFrequency.Yearly, null, true, null
            , null, "en", "UK", 0, true, command.CreatedBy, command.CreatedAt, command.CreatedByIp, bank);
        
        await repository.AddAsync(bank, cancellationToken);
        await pageRepository.AddAsync(page, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<BankDTO?> GetByBankIdAsync(int id, CancellationToken cancellationToken) 
        => await queryRepository.GetByIdAsync(id, cancellationToken);
    

    public async Task EditAsync(EditBankCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.BankErrors.DuplicateName);

        exists = await queryRepository.ExistsBySlugAsync(command.Slug, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.BankErrors.DuplicateSlug);

        var bank = await repository.GetByIdAsync(command.Id, BankInclude.None, true, cancellationToken);
        if (bank is null) throw new DomainException(DomainErrors.BankErrors.InvalidId);
        
        var page = await pageRepository.GetByBankIdAsync(command.Id, BankPageInclude.None, true, cancellationToken);
        if (page is null) throw new DomainException(DomainErrors.BankPageErrors.InvalidBankId);

        bank.Update(command.Name, command.Slug, command.IsActive, command.SortOrder, 
            command.UpdatedBy, command.UpdatedByIp);
        
        page.Update(command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);

        repository.Update(bank);
        pageRepository.Update(page);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await queryRepository.GetMaxSortOrderAsync(cancellationToken);
}