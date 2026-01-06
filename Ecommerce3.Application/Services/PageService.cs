using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Page;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services;

internal sealed class PageService(
    IUnitOfWork unitOfWork,
    IPageQueryRepository queryRepository,
    IPageRepository<Page> pageRepository,
    IEnumerable<IPageQueryRepository> pageQueryRepositories) : IPageService
{
    public async Task<PagedResult<PageListItemDTO>> GetListItemsAsync(PageFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await queryRepository.GetListItemsAsync(filter, pageNumber, pageSize, cancellationToken);
    
    public async Task AddAsync(AddPageCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByPathAsync(command.Path, null, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.PageErrors.DuplicatePath);
        
        var page = new Page(command.Path, command.MetaTitle, command.MetaDescription, command.MetaKeywords, 
            command.MetaRobots, command.H1, command.CanonicalUrl, command.OgTitle, command.OgDescription, 
            command.OgImageUrl, command.OgType, command.TwitterCard, command.ContentHtml, command.Summary, 
            command.SchemaJsonLd, command.BreadcrumbsJson, command.HreflangMapJson, command.SitemapPriority, 
            command.SitemapFrequency, command.RedirectFromJson, command.IsIndexed, command.HeaderScripts, 
            command.FooterScripts, command.Language, command.Region, command.SeoScore, command.IsActive, 
            command.CreatedBy, command.CreatedAt, command.CreatedByIp);
        
        await pageRepository.AddAsync(page, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task EditAsync(EditPageCommand command, CancellationToken cancellationToken)
    {
        var exists = await queryRepository.ExistsByPathAsync(command.Path!, command.Id, cancellationToken);
        if (exists) throw new DomainException(DomainErrors.PageErrors.DuplicatePath);

        var page = await pageRepository.GetByIdAsync(command.Id, PageInclude.None, true, cancellationToken);
        if (page is null) throw new DomainException(DomainErrors.PageErrors.InvalidId);

        page.Update(command.Path!, command.MetaTitle, command.MetaDescription, command.MetaKeywords, 
            command.MetaRobots, command.H1, command.CanonicalUrl, command.OgTitle, command.OgDescription, 
            command.OgImageUrl, command.OgType, command.TwitterCard, command.ContentHtml, command.Summary, 
            command.SchemaJsonLd, command.BreadcrumbsJson, command.HreflangMapJson, command.SitemapPriority, 
            command.SitemapFrequency, command.RedirectFromJson, command.IsIndexed, command.HeaderScripts, 
            command.FooterScripts, command.Language, command.Region, command.SeoScore, command.IsActive, 
            command.UpdatedBy, command.UpdatedAt, command.UpdatedByIp);
        
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<PageDTO?> GetByIdAsync(int id, Type entity, CancellationToken cancellationToken)
    {
        var pageQueryRepository = pageQueryRepositories.FirstOrDefault(x => x.PageType == entity);
        if (pageQueryRepository is null)
            throw new DomainException(DomainErrors.PageErrors.PageQueryRepositoryNotFound);

        return await pageQueryRepository.GetByIdAsync(id, cancellationToken);
    }
}