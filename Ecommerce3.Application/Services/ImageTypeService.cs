using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ImageType;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.ImageType;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class ImageTypeService(
    IImageTypeRepository repository,
    IImageTypeQueryRepository queryRepository,
    IUnitOfWork unitOfWork) : IImageTypeService
{
    public async Task<PagedResult<ImageTypeListItemDTO>> GetListItemsAsync(ImageTypeFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddImageTypeCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameForEntityAsync(command.Name, command.Entity, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ImageTypeErrors.DuplicateName);
        
        exists = await queryRepository.ExistsBySlugForEntityAsync(command.Slug, command.Entity, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ImageTypeErrors.DuplicateSlug);

        var imageType = new ImageType(command.Entity, command.Name, command.Slug, command.Description, command.IsActive,
            command.CreatedBy, command.CreatedByIp);

        await repository.AddAsync(imageType, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<ImageTypeDTO?> GetByImageTypeIdAsync(int id, CancellationToken cancellationToken)
        => await queryRepository.GetByIdAsync(id, cancellationToken);

    public async Task EditAsync(EditImageTypeCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameForEntityAsync(command.Name, command.Entity, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ImageTypeErrors.DuplicateName);
        
        exists = await queryRepository.ExistsBySlugForEntityAsync(command.Slug, command.Entity, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.ImageTypeErrors.DuplicateSlug);

        var imageType = await repository.GetByIdAsync(command.Id, true, cancellationToken);
        if (imageType is null) throw new DomainException(DomainErrors.ImageTypeErrors.InvalidId);

        imageType.Update(command.Entity, command.Name, command.Slug, command.Description,
            command.IsActive, command.UpdatedBy, command.UpdatedByIp);

        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<Dictionary<int, string>> GetIdAndNamesByEntityAsync(string entity, CancellationToken cancellationToken)
        => await queryRepository.GetIdAndNamesByEntityAsync(entity, cancellationToken);
}