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
    ITextListItemQueryRepository textListItemQueryRepository,
    IProductTextListItemQueryRepository productTextListItemQueryRepository,
    IUnitOfWork unitOfWork) : ITextListItemService
{
    public async Task AddAsync(AddTextListItemCommand command, CancellationToken cancellationToken)
    {
        if (command.ParentEntity == typeof(ProductTextListItem))
        {
            var exists = await productTextListItemQueryRepository.ExistsByProductIdAndTypeAndTextAsync(
                command.ParentEntityId, command.Type, command.Text, null, cancellationToken);
            if (exists) throw new DomainException(DomainErrors.TextListItemErrors.DuplicateText);
        }
        else
            throw new NotImplementedException("Specific TextListItem query repository not found.");

        var textListItem = TextListItem.Create(command.ParentEntity, command.ParentEntityId, command.Type, command.Text,
            command.SortOrder, command.CreatedBy, command.CreatedAt, command.CreatedByIp);

        await repository.AddAsync(textListItem, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditAsync(EditTextListItemCommand command, CancellationToken cancellationToken)
    {
        var textListItem = await repository
            .GetByIdAsync(command.Id, TextListItemInclude.None, true, cancellationToken);
        if (textListItem is null) throw new DomainException(DomainErrors.TextListItemErrors.InvalidId);

        if (command.ParentEntity == typeof(ProductTextListItem))
        {
            var exists = await productTextListItemQueryRepository.ExistsByProductIdAndTypeAndTextAsync(
                command.ParentEntityId, command.Type, command.Text, command.Id, cancellationToken);
            if (exists) throw new DomainException(DomainErrors.TextListItemErrors.DuplicateText);
        }
        else
            throw new NotImplementedException("Specific TextListItem query repository not found.");

        var updated = textListItem.Update(command.Text, command.SortOrder, command.UpdatedBy, command.UpdatedAt,
            command.UpdatedByIp);
        if (updated) await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeleteTextListItemCommand command, CancellationToken cancellationToken)
    {
        var textListItem = await repository
            .GetByIdAsync(command.Id, TextListItemInclude.None, true, cancellationToken);
        if (textListItem is null) throw new DomainException(DomainErrors.TextListItemErrors.InvalidId);

        textListItem.Delete(command.DeletedBy, command.DeletedAt, command.DeletedByIp);
        repository.Remove(textListItem);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TextListItemDTO>> GetByParamsAsync(Type parentEntity, int parentEntityId,
        TextListItemType type, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}