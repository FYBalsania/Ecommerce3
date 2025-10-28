using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ImageType;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.ImageType;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public sealed class ImageTypeService : IImageTypeService
{
    private readonly IImageTypeRepository _repository;
    private readonly IImageTypeQueryRepository _queryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ImageTypeService(IImageTypeRepository repository, IImageTypeQueryRepository queryRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PagedResult<ImageTypeListItemDTO>> GetListItemsAsync(ImageTypeFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddImageTypeCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(ImageType.Name));

        var imageType = new ImageType(command.Entity, command.Name, command.Description, command.IsActive, 
            command.CreatedBy, command.CreatedByIp);
        
        await _repository.AddAsync(imageType, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<ImageTypeDTO?> GetByImageTypeIdAsync(int id, CancellationToken cancellationToken)
    {
        var imageType = await _queryRepository.GetByIdAsync(id, cancellationToken);

        return new ImageTypeDTO
        {
            Id = imageType.Id,
            Entity = imageType.Entity,
            Name = imageType.Name,
            Description = imageType.Description,
            IsActive = imageType.IsActive,
        };
    }

    public async Task EditAsync(EditImageTypeCommand command, CancellationToken cancellationToken)
    {
        var exists = await _queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(ImageType.Name));

        var imageType = await _repository.GetByIdAsync(command.Id, true, cancellationToken);
        if (imageType is null) throw new ArgumentNullException(nameof(command.Id), "Image type not found.");

        var imageTypeUpdated = imageType.Update(command.Entity, command.Name, command.Description, command.IsActive, 
            command.UpdatedBy, command.UpdatedByIp);
        
        if (imageTypeUpdated)
        {
            _repository.Update(imageType);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}