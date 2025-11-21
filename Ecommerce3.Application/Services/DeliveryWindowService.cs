using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.DeliveryWindow;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.DeliveryWindow;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class DeliveryWindowService : IDeliveryWindowService
{
    private readonly IDeliveryWindowRepository _repository;
    private readonly IDeliveryWindowQueryRepository _queryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeliveryWindowService(IDeliveryWindowRepository repository, IDeliveryWindowQueryRepository queryRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PagedResult<DeliveryListItemDTO>> GetListItemsAsync(DeliveryWindowFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddDeliveryWindowCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.DeliveryWindowErrors.DuplicateName);

        var deliveryWindow = new DeliveryWindow(command.Name, command.Unit, (uint)command.MinValue, (uint)command.MaxValue!.Value,
            command.SortOrder, command.IsActive, command.CreatedBy, command.CreatedByIp);
        
        await _repository.AddAsync(deliveryWindow, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
    
    public async Task<DeliveryWindowDTO?> GetByDeliveryWindowIdAsync(int id, CancellationToken cancellationToken)
    {
        var deliveryWindow = await _queryRepository.GetByIdAsync(id, cancellationToken);

        return new DeliveryWindowDTO
        {
            Id = deliveryWindow.Id,
            Name = deliveryWindow.Name,
            Unit = deliveryWindow.Unit,
            MinValue = deliveryWindow.MinValue,
            MaxValue = deliveryWindow.MaxValue,
            IsActive = deliveryWindow.IsActive,
            SortOrder = deliveryWindow.SortOrder,
        };
    }

    public async Task EditAsync(EditDeliveryWindowCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.DeliveryWindowErrors.DuplicateName);

        var deliveryWindow = await _repository.GetByIdAsync(command.Id, true, cancellationToken);
        if (deliveryWindow is null) throw new ArgumentNullException(nameof(command.Id), "Delivery window not found.");
        
        var deliveryWindowUpdated = deliveryWindow.Update(command.Name, command.Unit, (uint)command.MinValue, (uint)command.MaxValue!.Value,
            command.IsActive, command.SortOrder, command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);
        
        if (deliveryWindowUpdated)
        {
            _repository.Update(deliveryWindow);
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