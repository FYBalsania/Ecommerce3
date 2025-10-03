using AutoMapper;
using Ecommerce3.Application.Commands;
using Ecommerce3.Application.Commands.Brand;
using Ecommerce3.Application.DTOs.Brand;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public sealed class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BrandService(IBrandRepository brandRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<BrandListItemDTO> ListItems, int Count)> GetBrandListItemsAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var result = await _brandRepository.GetBrandsAsync(name, BrandInclude.CreatedUser, pageNumber, pageSize,
            cancellationToken);
        var brands = _mapper.Map<IEnumerable<BrandListItemDTO>>(result.Brands);

        return (brands, result.Count);
    }

    public async Task AddBrandAsync(AddBrandCommand command, CancellationToken cancellationToken)
    {
        var exists = await _brandRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(Brand.Name));

        exists = await _brandRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(Brand.Slug)} already exists.", nameof(Brand.Slug));

        var brand = new Brand(command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText,
            command.AnchorTitle, command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.ShortDescription, command.FullDescription, command.IsActive, command.SortOrder, command.CreatedBy,
            command.CreatedByIp);

        await _brandRepository.AddAsync(brand, cancellationToken);
    }

    public async Task<BrandDTO?> GetBrandAsync(int id, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(id, BrandInclude.None, false, cancellationToken);
        if (brand == null) return null;

        return _mapper.Map<BrandDTO>(brand);
    }

    public async Task UpdateBrandAsync(UpdateBrandCommand command, CancellationToken cancellationToken)
    {
        var exists = await _brandRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(Brand.Name));

        exists = await _brandRepository.ExistsBySlugAsync(command.Slug, command.Id, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(Brand.Slug)} already exists.", nameof(Brand.Slug));

        var brand = await _brandRepository.GetByIdAsync(command.Id, BrandInclude.None, true, cancellationToken);
        if (brand is null) throw new ArgumentNullException(nameof(command.Id), "Brand not found.");

        var updated = brand.Update(command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText,
            command.AnchorTitle, command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.ShortDescription, command.FullDescription, command.IsActive, command.SortOrder, command.UpdatedBy,
            command.UpdatedAt, command.UpdatedByIp);

        if (updated)
        {
            _brandRepository.Update(brand);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
    }

    public Task DeleteBrandAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}