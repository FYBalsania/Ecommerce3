using System.Linq.Expressions;
using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions;

public static class PageExtensions
{
    private static readonly Expression<Func<Page, PageDTO>> DTOExpression = x => new PageDTO
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
    
    public static IQueryable<PageDTO> ProjectToDTO(this IQueryable<Page> query) => query.Select(DTOExpression);
}