using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class PageQueryRepository(AppDbContext dbContext) : IPageQueryRepository
{
    public async Task<(IReadOnlyList<PageListItemDTO>, int)> GetPageListItemsAsync(string? name, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.Pages.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(b => EF.Functions.Like(b.H1, $"%{name}%"));

        var total = await query.CountAsync(cancellationToken);
        var pages = await query
            .OrderBy(b => b.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new PageListItemDTO(x.Id, "", x.Path, x.BrandId, x.CategoryId, x.ProductId, x.ProductGroupId , x.IsActive, x.CreatedByUser!.FullName, x.CreatedAt))
            .ToListAsync(cancellationToken);

        return (pages, total);
    }

    public async Task<PageDTO?> GetByPathAsync(string path, CancellationToken cancellationToken)
    {
        return await dbContext.Pages
            .Where(x => x.Path == path && x.IsActive)
            .Select(FromDomain)
            .FirstOrDefaultAsync(cancellationToken);
    }

    private static Expression<Func<Page, PageDTO>> FromDomain =>
        x => new PageDTO
        {
            Id = x.Id,
            Path = x.Path,
            MetaTitle = x.MetaTitle,
            MetaDescription = x.MetaDescription,
            MetaKeywords = x.MetaKeywords,
            MetaRobots = x.MetaRobots,
            H1 = x.H1,
            CanonicalUrl = x.CanonicalUrl,
            OgTitle = x.OgTitle,
            OgDescription = x.OgDescription,
            OgImageUrl = x.OgImageUrl,
            OgType = x.OgType,
            TwitterCard = x.TwitterCard,
            ContentHtml = x.ContentHtml,
            Summary = x.Summary,
            SchemaJsonLd = x.SchemaJsonLd,
            BreadcrumbsJson = x.BreadcrumbsJson,
            HreflangMapJson = x.HreflangMapJson,
            SitemapPriority = x.SitemapPriority,
            SitemapFrequency = x.SitemapFrequency,
            RedirectFromJson = x.RedirectFromJson,
            IsIndexed = x.IsIndexed,
            HeaderScripts = x.HeaderScripts,
            FooterScripts = x.FooterScripts,
            Language = x.Language,
            Region = x.Region,
            SeoScore = x.SeoScore,
            IsActive = x.IsActive
        };
}