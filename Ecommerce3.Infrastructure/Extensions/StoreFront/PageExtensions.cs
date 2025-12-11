using System.Linq.Expressions;
using Ecommerce3.Contracts.DTO.StoreFront.Page;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Infrastructure.Extensions.StoreFront;

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
        IsActive = x.IsActive,
        Images = x.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
            .Select(ImageExtensions.DTOExpression).ToList()
    };
    
    public static IQueryable<PageDTO> ProjectToDTO(this IQueryable<Page> query) => query.Select(DTOExpression);
}