using Ecommerce3.Application.Commands.KVPListItem;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.KVPListItem;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class KVPListItemService(
    IKVPListItemRepository repository,
    IEnumerable<IKVPListItemQueryRepository> queryRepositories,
    IUnitOfWork unitOfWork,
    IKVPListItemQueryRepository textListItemQueryRepository)
    : IKVPListItemService
{
    public async Task AddAsync(AddKVPListItemCommand command, CancellationToken cancellationToken)
    {
        var parentEntity = Type.GetType(command.ParentEntity);
        if (parentEntity is null)
            throw new DomainException(DomainErrors.KVPListItemErrors.ParentEntityRequired);
        
        var queryRepository = TryGetKVPListItemQueryRepository(parentEntity);

        var exists = await queryRepository.KeyExistsAsync(command.ParentEntityId, command.Type, command.Key, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.KVPListItemErrors.DuplicateKey);

        exists = await queryRepository.ValueExistsAsync(command.ParentEntityId, command.Type, command.Value, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.KVPListItemErrors.DuplicateValue);

        var kvpListItem = KVPListItem.Create(parentEntity, command.ParentEntityId, command.Type,
            command.Key, command.Value, command.SortOrder, command.CreatedBy, command.CreatedAt, command.CreatedByIp);

        await repository.AddAsync(kvpListItem, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditAsync(EditKVPListItemCommand command, CancellationToken cancellationToken)
    {
        var parentEntity = Type.GetType(command.ParentEntity);
        if (parentEntity is null)
            throw new DomainException(DomainErrors.KVPListItemErrors.ParentEntityRequired);
        
        var kvpListItem = await repository.GetByIdAsync(command.Id, KVPListItemInclude.None, true, cancellationToken);
        if (kvpListItem is null)
            throw new DomainException(DomainErrors.KVPListItemErrors.InvalidId);

        var queryRepository = TryGetKVPListItemQueryRepository(parentEntity);

        var exists = await queryRepository.KeyExistsAsync(command.ParentEntityId, command.Type, command.Key, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.KVPListItemErrors.DuplicateKey);

        exists = await queryRepository.ValueExistsAsync(command.ParentEntityId, command.Type, command.Value, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.KVPListItemErrors.DuplicateValue);

        var updated = kvpListItem.Update(command.Key, command.Value, command.SortOrder, command.UpdatedBy,
            command.UpdatedAt, command.UpdatedByIp);
        if (updated) await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeleteKVPListItemCommand command, CancellationToken cancellationToken)
    {
        var kvpListItem = await repository.GetByIdAsync(command.Id, KVPListItemInclude.None, true, cancellationToken);
        if (kvpListItem is null)
            throw new DomainException(DomainErrors.KVPListItemErrors.InvalidId);

        kvpListItem.Delete(command.DeletedBy, command.DeletedAt, command.DeletedByIp);
        repository.Remove(kvpListItem);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<KVPListItemDTO>> GetAllByParamsAsync(Type parentEntityType, int parentEntityId,
        KVPListItemType type, CancellationToken cancellationToken)
    {
        return await TryGetKVPListItemQueryRepository(parentEntityType).GetAllByParamsAsync(parentEntityId, type, cancellationToken);
    }

    private IKVPListItemQueryRepository TryGetKVPListItemQueryRepository(Type entityType)
    {
        var queryRespository = queryRepositories
            .FirstOrDefault(x => x.Entity == entityType);
        return queryRespository ??
               throw new NotImplementedException(
                   $"Specific ({nameof(entityType)}) KVPListItemQueryRepository not found.");
    }
    
    public async Task<KVPListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await textListItemQueryRepository.GetByIdAsync(id, cancellationToken);
}