using Ecommerce3.Application.Commands;
using Ecommerce3.Application.Commands.Brand;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

public sealed class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IBrandQueryRepository _brandQueryRepository;
    private readonly IPageRepository _pageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BrandService(IBrandRepository brandRepository, IBrandQueryRepository brandQueryRepository,
        IPageRepository pageRepository, IUnitOfWork unitOfWork)
    {
        _brandRepository = brandRepository;
        _brandQueryRepository = brandQueryRepository;
        _pageRepository = pageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<(IReadOnlyList<BrandListItemDTO>, int)> GetBrandListItemsAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken)
        => await _brandQueryRepository.GetBrandListItemsAsync(name, pageNumber, pageSize, cancellationToken);

    public async Task AddBrandAsync(AddBrandCommand command, CancellationToken cancellationToken)
    {
        var exists = await _brandRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DuplicateException($"{command.Name} already exists.", nameof(Brand.Name));

        exists = await _brandRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DuplicateException($"{nameof(Brand.Slug)} already exists.", nameof(Brand.Slug));

        var brand = new Brand(command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText,
            command.AnchorTitle, command.ShortDescription, command.FullDescription, command.IsActive, command.SortOrder,
            command.CreatedBy, command.CreatedByIp);

        var page = new BrandPage(null, command.MetaTitle, command.MetaDescription, command.MetaKeywords, null, null,
            null, null, null, null, null, null, null, null,
            null, null, null, 0, SiteMapFrequency.Yearly, null, true, null
            , null, "en", "UK", 0, true, command.CreatedBy, command.CreatedAt, command.CreatedByIp, brand);
        await _brandRepository.AddAsync(brand, cancellationToken);
        await _pageRepository.AddAsync(page, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
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
            command.AnchorTitle,
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