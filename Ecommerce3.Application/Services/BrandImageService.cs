using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public sealed class BrandImageService : IImageService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IBrandQueryRepository _brandQueryRepository;

    public BrandImageService(IBrandRepository brandRepository, IBrandQueryRepository brandQueryRepository)
    {
        _brandRepository = brandRepository;
        _brandQueryRepository = brandQueryRepository;
    }

    public Type HandledType => typeof(BrandImage);

    public async Task AddImageAsync(AddImageCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task EditImageAsync(EditImageCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteImageAsync(DeleteImageCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}