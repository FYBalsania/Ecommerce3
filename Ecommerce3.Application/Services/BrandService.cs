using Ecommerce3.Application.Commands;
using Ecommerce3.Application.DTOs;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public sealed class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository) => _brandRepository = brandRepository;
    
    public async Task<(IEnumerable<BrandListItemDTO> ListItems, int Count)> GetBrandListItemsAsync(string? name,
        int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var result = await _brandRepository.GetBrandsAsync(name, [BrandInclude.CreatedUser], pageNumber, pageSize,
            cancellationToken);
        var brands = result.Brands.Select(x => new BrandListItemDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Slug = x.Slug,
            CreatedUserFullName = x.CreatedByUser.FullName!,
            CreatedAt = x.CreatedAt,
            CreatedByIp = x.CreatedByIp
        });

        return (brands, result.Count);
    }

    public async Task AddBrandAsync(AddBrandCommand command, CancellationToken cancellationToken)
    {
        var exists = await _brandRepository.ExistsByNameAsync(command.Name, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(Brand.Name));

        exists = await _brandRepository.ExistsBySlugAsync(command.Slug, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(Brand.Slug)} already exists.", nameof(Brand.Slug));

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