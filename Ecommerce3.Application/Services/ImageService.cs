using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Image;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Policies;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class ImageService : IImageService
{
    private readonly IImageTypeDetector _imageTypeDetector;
    private readonly IImageTypeRepository _imageTypeRepository;
    private readonly IEnumerable<IImageEntityRepository> _imageEntityRepositories;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageRepository<Image> _imageRepository;
    private readonly IEnumerable<IImageQueryRepository> _imageQueryRepositories;

    public ImageService(IImageTypeDetector imageTypeDetector,
        IImageTypeRepository imageTypeRepository,
        IEnumerable<IImageEntityRepository> imageEntityRepositories,
        IUnitOfWork unitOfWork,
        IImageRepository<Image> imageRepository,
        IEnumerable<IImageQueryRepository> imageQueryRepositories)
    {
        _imageTypeDetector = imageTypeDetector;
        _imageTypeRepository = imageTypeRepository;
        _imageEntityRepositories = imageEntityRepositories;
        _unitOfWork = unitOfWork;
        _imageRepository = imageRepository;
        _imageQueryRepositories = imageQueryRepositories;
    }

    public async Task AddImageAsync(AddImageCommand command, CancellationToken cancellationToken)
    {
        //Validation start.
        //ParentEntityType.
        var parentEntityType = Type.GetType(command.ParentEntityType);
        if (parentEntityType is null)
            throw new DomainException(DomainErrors.ImageErrors.ParentEntityTypeRequired);
        if (parentEntityType.BaseType == typeof(EntityWithImages<>))
            throw new DomainException(DomainErrors.ImageErrors.InvalidParentEntityType);

        //ImageEntityType.
        var imageEntityType = Type.GetType(command.ImageEntityType);
        if (imageEntityType is null)
            throw new DomainException(DomainErrors.ImageErrors.ImageEntityTypeRequired);
        if (imageEntityType.BaseType != typeof(Image))
            throw new DomainException(DomainErrors.ImageErrors.InvalidImageEntityType);

        //ParentEntityId.
        if (string.IsNullOrWhiteSpace(command.ParentEntityId))
            throw new DomainException(DomainErrors.ImageErrors.ParentEntityIdRequired);
        if (!int.TryParse(command.ParentEntityId, out int parentEntityId))
            throw new DomainException(DomainErrors.ImageErrors.InvalidParentEntityId);
        if (parentEntityId <= 0)
            throw new DomainException(DomainErrors.ImageErrors.InvalidParentEntityId);
        var imageEntityRepository = _imageEntityRepositories
            .FirstOrDefault(x => x.EntityType == parentEntityType && x.ImageType == imageEntityType);
        if (imageEntityRepository is null)
            throw new DomainException(new DomainError("ImageEntityRepository", "ImageEntityRepository not found."));
        var parent = await imageEntityRepository.GetByIdAsync(parentEntityId, cancellationToken);
        if (parent is null) throw new DomainException(DomainErrors.ImageErrors.InvalidParentEntityId);

        //ImageTypeId.
        var imageType = await _imageTypeRepository.GetByIdAsync(command.ImageTypeId, false, cancellationToken);
        if (imageType is null)
            throw new DomainException(DomainErrors.ImageErrors.InvalidImageTypeId);

        //File.
        if (command.File is null)
            throw new DomainException(DomainErrors.ImageErrors.FileRequired);
        if (command.File.Length == 0)
            throw new DomainException(DomainErrors.ImageErrors.EmptyFile);
        if (command.File.Length > command.MaxFileSizeKb)
            throw new DomainException(DomainErrors.ImageErrors.FileSizeTooLarge);
        _imageTypeDetector.EnsureMatchesExtension(command.File, command.FileName);
        //Validation end.

        //Generate image name.
        var fileNameExtension = Path.GetExtension(command.FileName);
        var imageFileName =
            $"{GetEntitySlug(parent)}-{imageType.Slug}-{command.ImageSize.ToString().ToLower()}-{Path.GetRandomFileName().Replace(".", "")}{fileNameExtension}";

        //Create an image instance.
        var image = Image.Create(imageEntityRepository.ImageType, command.FileName, imageFileName, fileNameExtension,
            command.ImageTypeId, command.ImageSize, command.AltText, command.Title, command.Loading, command.Link,
            command.LinkTarget, parent.Id, command.SortOrder, command.CreatedBy, command.CreatedAt,
            command.CreatedByIp);

        //Save image to media.
        var fileNameWithPath = Path.Combine(command.ImageFolderPath, imageFileName);
        await File.WriteAllBytesAsync(fileNameWithPath, command.File, cancellationToken);

        //Save the image to a database.
        await _imageRepository.AddAsync(image, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditImageAsync(EditImageCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteImageAsync(DeleteImageCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<ImageDTO>> GetImagesByImageTypeAndParentIdAsync(Type imageEntityType, int parentId,
        CancellationToken cancellationToken)
    {
        var imageQueryRepository =
            _imageQueryRepositories.FirstOrDefault(x => x.ImageType == imageEntityType);
        if (imageQueryRepository is null)
            throw new ArgumentNullException(nameof(imageEntityType), "Specific Image query repository not found.");

        return await imageQueryRepository.GetByParentIdAsync(parentId, cancellationToken);
    }

    private static string? GetEntitySlug(Entity entity) => entity switch
    {
        Brand b => b.Slug,
        Category c => c.Slug,
        ProductGroup pg => pg.Slug,
        Product p => p.Slug,
        Bank bnk => bnk.Slug,
        Page pg => pg.Path, // Path used for Page
        _ => throw new ArgumentException("Entity is not of type EntityWithImages<>.", nameof(entity))
    };
}