using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.Page;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Admin.ViewModels.Page;

public class AddPageViewModel
{
    [Required(ErrorMessage = $"{nameof(Type)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Type)} must be between 1 and 256 characters.")]
    public string Type { get; set; }

    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Path)} must be between 1 and 256 characters.")]
    public string? Path { get; set; }

    // Content
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(H1)} must be between 1 and 256 characters.")]
    public string? H1 { get; set; }
    
    [StringLength(2048, MinimumLength = 1, ErrorMessage = $"Canonical url must be between 1 and 2048 characters.")]
    [Display(Name = "Canonical url")]
    public string? CanonicalUrl { get; set; }

    [Required(ErrorMessage = "Meta title is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Meta title must be between 1 and 256 characters.")]
    [Display(Name = "Meta title")]
    public string MetaTitle { get; set; }

    [StringLength(2048, MinimumLength = 1, ErrorMessage = "Meta description must be between 1 and 2048 characters.")]
    [Display(Name = "Meta description")]
    public string? MetaDescription { get; set; }

    [StringLength(1024, MinimumLength = 1, ErrorMessage = "Meta keywords must be between 1 and 1024 characters.")]
    [Display(Name = "Meta keywords")]
    public string? MetaKeywords { get; set; }

    [StringLength(32, MinimumLength = 1, ErrorMessage = "Meta robots must be between 1 and 32 characters.")]
    [Display(Name = "Meta robots")]
    public string? MetaRobots { get; set; }

    // Social
    [StringLength(256, MinimumLength = 1, ErrorMessage = "Og title must be between 1 and 256 characters.")]
    [Display(Name = "Og title")]
    public string? OgTitle { get; set; }
    
    [StringLength(2048, MinimumLength = 1, ErrorMessage = "Og description must be between 1 and 2048 characters.")]
    [Display(Name = "Og description")]
    public string? OgDescription { get; set; }
    
    [StringLength(2048, MinimumLength = 1, ErrorMessage = "Og image url must be between 1 and 2048 characters.")]
    [Display(Name = "Og image url")]
    public string? OgImageUrl { get; set; }
    
    [StringLength(16, MinimumLength = 1, ErrorMessage = "Og type must be between 1 and 16 characters.")]
    [Display(Name = "Og type")]
    public string? OgType { get; set; }
    
    [StringLength(64, MinimumLength = 1, ErrorMessage = "Twitter card must be between 1 and 64 characters.")]
    [Display(Name = "Twitter card")]
    public string? TwitterCard { get; set; }

    // Body
    [Display(Name = "Content html")]
    public string? ContentHtml { get; set; }
    
    [StringLength(1024, MinimumLength = 1, ErrorMessage = $"{nameof(Summary)} must be between 1 and 1024 characters.")]
    public string? Summary { get; set; }

    // SEO
    [Display(Name = "Schema json ld")]
    public string? SchemaJsonLd { get; set; }
    
    [Display(Name = "Breadcrumbs json")]
    public string? BreadcrumbsJson { get; set; }
    
    [Display(Name = "Href lang map json")]
    public string? HreflangMapJson { get; set; }
    
    [Display(Name = "SEO Score")]
    public int? SeoScore { get; set; }

    // Sitemap
    [Required(ErrorMessage = "Sitemap priority is required.")]
    [Display(Name = "Sitemap priority")]
    public decimal SitemapPriority { get; set; }
    
    [Required(ErrorMessage = "Sitemap frequency is required.")]
    [Display(Name = "Sitemap frequency")]
    public SiteMapFrequency SitemapFrequency { get; set; }

    // Redirects
    [Display(Name = "Redirect from json")]
    public string? RedirectFromJson { get; set; }

    // Flags
    [Required(ErrorMessage = "Is indexed is required.")]
    [Display(Name = "Is indexed")]
    public bool IsIndexed { get; set; }
    
    [Required(ErrorMessage = "Is active is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }

    // Locale
    [StringLength(8, MinimumLength = 1, ErrorMessage = $"{nameof(Language)} must be between 1 and 8 characters.")]
    [Required(ErrorMessage = "Language is required.")]
    public string Language { get; set; }
    
    [StringLength(8, MinimumLength = 1, ErrorMessage = $"{nameof(Region)} must be between 1 and 8 characters.")]
    public string? Region { get; set; }
    
    //Scripts
    [Display(Name = "Header scripts")]
    public string? HeaderScripts { get; set; }
    
    [Display(Name = "Footer scripts")]
    public string? FooterScripts { get; set; }
    
    public AddPageCommand ToCommand(int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        return new AddPageCommand()
        {
            Type = Type,
            Path = Path,
            H1 = H1,
            MetaTitle = MetaTitle,
            MetaDescription = MetaDescription,
            MetaKeywords = MetaKeywords,
            MetaRobots = MetaRobots,
            OgTitle = OgTitle,
            OgDescription = OgDescription,
            OgImageUrl = OgImageUrl,
            OgType = OgType,
            TwitterCard = TwitterCard,
            ContentHtml = ContentHtml,
            Summary = Summary,
            SchemaJsonLd = SchemaJsonLd,
            BreadcrumbsJson = BreadcrumbsJson,
            HreflangMapJson = HreflangMapJson,
            SeoScore = SeoScore,
            SitemapPriority = SitemapPriority,
            SitemapFrequency = SitemapFrequency,
            RedirectFromJson = RedirectFromJson,
            IsIndexed = IsIndexed,
            IsActive = IsActive,
            Language = Language,
            Region = Region,
            HeaderScripts = HeaderScripts,
            FooterScripts = FooterScripts,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            CreatedByIp = createdByIp
        };
    }
}