using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Policies;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public class ImageService : IImageService
{
    private readonly IImageTypeDetector _imageTypeDetector;
    private readonly IImageTypeRepository _imageTypeRepository;
    private readonly IEnumerable<IImageEntityRepository> _imageEntityRepositories;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageRepository<Image> _imageRepository;

    public ImageService(IImageTypeDetector imageTypeDetector,
        IImageTypeRepository imageTypeRepository,
        IEnumerable<IImageEntityRepository> imageEntityRepositories,
        IUnitOfWork unitOfWork,
        IImageRepository<Image> imageRepository)
    {
        _imageTypeDetector = imageTypeDetector;
        _imageTypeRepository = imageTypeRepository;
        _imageEntityRepositories = imageEntityRepositories;
        _unitOfWork = unitOfWork;
        _imageRepository = imageRepository;
    }

    public async Task AddImageAsync(AddImageCommand command, CancellationToken cancellationToken)
    {
        var imageEntityRepository = _imageEntityRepositories
            .FirstOrDefault(x => x.EntityType == command.ParentEntityType && x.ImageType == command.ImageEntityType);

        //Validation start.
        //ParentEntityType.
        if (imageEntityRepository is null)
            throw new ArgumentException("ParentEntityType is not of type EntityWithImages<>.",
                nameof(command.ParentEntityType));

        //ParentId.
        var parent = await imageEntityRepository.GetByIdAsync(command.ParentEntityId, cancellationToken);
        if (parent is null) throw new ArgumentNullException(nameof(command.ParentEntityId), "Parent not found.");

        //ImageType.
        if (imageEntityRepository is null)
            throw new ArgumentException("ImageEntityType is not of type Image.", nameof(command.ImageEntityType));

        //ImageTypeId.
        var imageType = await _imageTypeRepository.GetByIdAsync(command.ImageTypeId, false, cancellationToken);
        if (imageType is null) throw new ArgumentException("ImageTypeId does not exist.", nameof(command.ImageTypeId));

        //File.
        if (command.File is null)
            throw new ArgumentNullException(nameof(command.File), "File is required.");
        if (command.File.Length > command.MaxFileSizeKb)
            throw new ArgumentException($"File size exceeds maximum allowed size of {command.MaxFileSizeKb} bytes.",
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