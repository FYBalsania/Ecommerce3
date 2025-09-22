using Ecommerce3.Application.Commands;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public sealed class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository) => _brandRepository = brandRepository;

    public async Task AddBrandAsync(AddBrandCommand command, CancellationToken cancellationToken)
    {
        var exists = await _brandRepository.ExistsByNameAsync(command.Name, cancellationToken);
        if (exists) throw new Exception($"{nameof(Brand.Name)} already exists.");
        
        exists = await _brandRepository.ExistsBySlugAsync(command.Slug, cancellationToken);
        if (exists) throw new Exception($"{nameof(Brand.Slug)} already exists.");

        var brand = new Brand(command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText,
            command.AnchorTitle, command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.ShortDescription, command.FullDescription, command.IsActive, command.SortOrder, command.CreatedBy,
            command.CreatedByIp);

        await _brandRepository.AddAsync(brand, cancellationToken);
    }

    public Task UpdateBrandAsync(UpdateBrandCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBrandAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}