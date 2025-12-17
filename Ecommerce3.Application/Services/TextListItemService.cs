using Ecommerce3.Application.Commands.TextListItem;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class TextListItemService(
    ITextListItemRepository repository,
    IEnumerable<ITextListItemQueryRepository> textListItemQueryRepositories,
    IUnitOfWork unitOfWork,
    ITextListItemQueryRepository textListItemQueryRepository) : ITextListItemService
{
    public async Task AddAsync(AddTextListItemCommand command, CancellationToken cancellationToken)
    {
        var parentEntity = Type.GetType(command.ParentEntity);
        if (parentEntity is null)
            throw new DomainException(DomainErrors.TextListItemErrors.ParentEntityRequired);
        
        var entity = Type.GetType(command.Entity);
        if (entity is null)
            throw new DomainException(DomainErrors.TextListItemErrors.EntityRequired);
        
        var queryRepository = TryGetTextListItemQueryRepository(parentEntity);
        
        var exists = await queryRepository.ExistsByParentEntityIdAsync(command.ParentEntityId, command.Type,
            command.Text, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.TextListItemErrors.DuplicateText);
        
        var textListItem = TextListItem.Create(entity, command.ParentEntityId, command.Type, command.Text,
            command.SortOrder, command.CreatedBy, command.CreatedAt, command.CreatedByIp);
        
        await repository.AddAsync(textListItem, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditAsync(EditTextListItemCommand command, CancellationToken cancellationToken)
    {
        var parentEntity = Type.GetType(command.ParentEntity);
        if (parentEntity is null)
            throw new DomainException(DomainErrors.TextListItemErrors.ParentEntityRequired);
        
        var textListItem = await repository.GetByIdAsync(command.Id, TextListItemInclude.None, true, cancellationToken);
        if (textListItem is null) throw new DomainException(DomainErrors.TextListItemErrors.InvalidId);

        var queryRepository = TryGetTextListItemQueryRepository(parentEntity);
        var exists = await queryRepository.ExistsByParentEntityIdAsync(command.ParentEntityId, command.Type,
            command.Text, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.TextListItemErrors.DuplicateText);

        var updated = textListItem.Update(command.Text, command.SortOrder, command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);
        if (updated) await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeleteTextListItemCommand command, CancellationToken cancellationToken)
    {
        var textListItem = await repository.GetByIdAsync(command.Id, TextListItemInclude.None, true, cancellationToken);
        if (textListItem is null) throw new DomainException(DomainErrors.TextListItemErrors.InvalidId);

        textListItem.Delete(command.DeletedBy, command.DeletedAt, command.DeletedByIp);
        repository.Remove(textListItem);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TextListItemDTO>> GetByParamsAsync(Type parentEntity, int parentEntityId,
        TextListItemType type, CancellationToken cancellationToken)
    {
        return await TryGetTextListItemQueryRepository(parentEntity)
            .GetByParamsAsync(parentEntityId, type, cancellationToken);
    }

    private ITextListItemQueryRepository TryGetTextListItemQueryRepository(Type parentEntity)
    {
        var queryRepository = textListItemQueryRepositories.FirstOrDefault(x => x.ParentEntityType == parentEntity);
        if (queryRepository is null)
            throw new NotImplementedException($"{nameof(ITextListItemQueryRepository)} not implemented for {nameof(parentEntity)}.");

        return queryRepository;
    }
    
    public async Task<TextListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await textListItemQueryRepository.GetByIdAsync(id, cancellationToken);
}