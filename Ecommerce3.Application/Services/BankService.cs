using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Bank;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Bank;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class BankService : IBankService
{
    private readonly IBankRepository _repository;
    private readonly IBankQueryRepository _queryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BankService(IBankRepository repository, IBankQueryRepository queryRepository, IUnitOfWork unitOfWork)
    {
        _queryRepository = queryRepository;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<BankListItemDTO>> GetListItemsAsync(BankFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddBankCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(Bank.Name));

        exists = await _queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(Bank.Slug)} already exists.", nameof(Bank.Slug));

        var bank = new Bank(command.Name, command.Slug, command.IsActive, command.SortOrder,
            command.CreatedBy, command.CreatedByIp);
        
        await _repository.AddAsync(bank, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<BankDTO?> GetByBankIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _queryRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task EditAsync(EditBankCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(Bank.Name));

        exists = await _queryRepository.ExistsBySlugAsync(command.Slug, command.Id, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(Bank.Slug)} already exists.", nameof(Bank.Slug));

        var bank = await _repository.GetByIdAsync(command.Id, BankInclude.None, true, cancellationToken);
        if (bank is null) throw new ArgumentNullException(nameof(command.Id), "Bank not found.");

        var bankUpdated = bank.Update(command.Name, command.Slug, command.IsActive, command.SortOrder, 
            command.UpdatedBy, command.UpdatedByIp);

        if (bankUpdated)
        {
            _repository.Update(bank);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _queryRepository.GetMaxSortOrderAsync(cancellationToken);
}