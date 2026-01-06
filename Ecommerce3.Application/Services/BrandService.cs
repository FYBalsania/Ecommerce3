using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Brand;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class BrandService(
    IBrandRepository repository,
    IBrandQueryRepository queryRepository,
    IBrandPageRepository pageRepository,
    IUnitOfWork unitOfWork)
    : IBrandService
{
    public async Task<PagedResult<BrandListItemDTO>> GetListItemsAsync(BrandFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);

    public async Task AddAsync(AddBrandCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameAsync(command.Name, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.BrandErrors.DuplicateName);

        exists = await queryRepository.ExistsBySlugAsync(command.Slug, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.BrandErrors.DuplicateSlug);

        var brand = new Brand(command.Name, command.Slug, command.Display, command.Breadcrumb, command.AnchorText,
            command.AnchorTitle, command.ShortDescription, command.FullDescription, command.IsActive, command.SortOrder,
            command.CreatedBy, command.CreatedByIp);

        var page = new BrandPage($"/{command.Slug}/b", command.MetaTitle, command.MetaDescription, command.MetaKeywords,
            null,
            command.H1, null, null, null, null, null, null, null, null,
            null, null, null, 0, SiteMapFrequency.Yearly, null, true, null
            , null, "en", "UK", 0, true, command.CreatedBy, command.CreatedAt, command.CreatedByIp, brand);
        await repository.AddAsync(brand, cancellationToken);
        await pageRepository.AddAsync(page, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<BrandDTO?> GetByBrandIdAsync(int id, CancellationToken cancellationToken)
    {
        return await queryRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task EditAsync(EditBrandCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.BrandErrors.DuplicateName);

        exists = await queryRepository.ExistsBySlugAsync(command.Slug, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.BrandErrors.DuplicateSlug);

        var brand = await repository.GetByIdAsync(command.Id, BrandInclude.None, true, cancellationToken);
        if (brand is null) throw new DomainException(DomainErrors.BrandErrors.InvalidId);

        var page = await pageRepository.GetByBrandIdAsync(command.Id, BrandPageInclude.None, true, cancellationToken);
        if (page is null) throw new DomainException(DomainErrors.BrandPageErrors.InvalidBrandId);

        brand.Update(command.Name, command.Slug, command.Display, command.Breadcrumb,
            command.AnchorText, command.AnchorTitle, command.ShortDescription, command.FullDescription,
            command.IsActive, command.SortOrder, command.UpdatedBy, command.UpdatedByIp);

        page.Update(command.MetaTitle, command.MetaDescription, command.MetaKeywords, command.H1,
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);
        
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await queryRepository.GetMaxSortOrderAsync(cancellationToken);

    public async Task<IDictionary<int, string>> GetIdAndNameListAsync(CancellationToken cancellationToken)
    {
        return await queryRepository.GetIdAndNameListAsync(cancellationToken);
    }
}