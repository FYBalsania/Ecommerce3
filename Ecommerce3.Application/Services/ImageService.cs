using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public class ImageService : IImageService
{
    private readonly IEnumerable<IImageRepository<Image>> _imageRepositories;
    private readonly IBrandImageRepository _brandImageRepository;

    public ImageService(IEnumerable<IImageRepository<Image>> imageRepositories, IBrandImageRepository brandImageRepository)
    {
        _imageRepositories = imageRepositories;
        _brandImageRepository = brandImageRepository;
    }

    public async Task AddAsync(AddImageCommand command, CancellationToken cancellationToken)
    {
        _ = _imageRepositories.FirstOrDefault(x => x.ImageType == command.ImageType);
        if (command.ImageType is BrandImage)
        {
            //validate command.ParentId;
        }
        else if (command.ImageType is ProductImage)
        {
        }
        else
        {
        }
    }
}