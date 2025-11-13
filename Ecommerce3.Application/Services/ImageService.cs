using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Image;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
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
            throw new ArgumentNullException(nameof(command.ParentEntityType), "ParentEntityType is required.");
        if (!typeof(EntityWithImages<>).IsAssignableFrom(parentEntityType))
            throw new ArgumentOutOfRangeException(nameof(command.ParentEntityType),
                "ParentEntityType must be of type EntityWithImages<>");

        //ImageEntityType.
        var imageEntityType = Type.GetType(command.ImageEntityType);
        if (imageEntityType is null)
            throw new ArgumentNullException(nameof(command.ImageEntityType), "ImageEntityType is required.");
        if (!typeof(Image).IsAssignableFrom(imageEntityType))
            throw new ArgumentOutOfRangeException(nameof(command.ImageEntityType),
                "ImageEntityType must be of type Image.");

        //ParentEntityId.
        if (!int.TryParse(command.ParentEntityId, out int parentEntityId))
            throw new ArgumentOutOfRangeException(nameof(command.ParentEntityId), "ParentEntityId is not an integer.");
        if (parentEntityId <= 0)
            throw new ArgumentOutOfRangeException(nameof(command.ParentEntityId),
                "Invalid ParentEntityId.");
        var imageEntityRepository = _imageEntityRepositories
            .FirstOrDefault(x => x.EntityType == parentEntityType && x.ImageType == imageEntityType);
        if (imageEntityRepository is null)
            throw new ArgumentNullException(nameof(command.ParentEntityId),
                "Specific ImageEntityRepository not found.");
        var parent = await imageEntityRepository.GetByIdAsync(parentEntityId, cancellationToken);
        if (parent is null) throw new ArgumentNullException(nameof(command.ParentEntityId), "Invalid ParentEntityId.");

        //ImageTypeId.
        var imageType = await _imageTypeRepository.GetByIdAsync(command.ImageTypeId, false, cancellationToken);
        if (imageType is null)
            throw new ArgumentOutOfRangeException(nameof(command.ImageTypeId), "Invalid ImageTypeId.");

        //File.
        if (command.File is null)
            throw new ArgumentNullException(nameof(command.File), "File is required.");
        if (command.File.Length > command.MaxFileSizeKb)
            throw new ArgumentOutOfRangeException(
                $"File size exceeds maximum allowed size of {command.MaxFileSizeKb} bytes.",
                nameof(command.File));
        _imageTypeDetector.EnsureMatchesExtension(command.File, command.FileName);
        //Validation end.

        //Generate image name.
        var fileNameExtension = Path.GetExtension(command.FileName);
        var imageFileName =
            $"{GetEntitySlug(parent)}-{imageType.Slug}-{command.ImageSize}-{Path.GetRandomFileName()}{fileNameExtension}";

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