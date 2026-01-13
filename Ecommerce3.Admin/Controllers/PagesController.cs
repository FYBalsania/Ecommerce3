using System.Net;
using Ecommerce3.Admin.ViewModels.Page;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class PagesController(
    IPageService pageService,
    IIPAddressService ipAddressService,
    IConfiguration configuration) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(PageFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await pageService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new PagesIndexViewModel()
        {
            Filter = filter,
            Pages = result
        };
        ViewData["Title"] = "Pages";
        return View(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Page";
        return View(new AddPageViewModel
        {
            IsActive = true,
            IsIndexed = true,
            Language = "en",
            Region = "GB",
            SitemapPriority = 0.7m
        });
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddPageViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }
        
        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        try
        {
            await pageService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(Page)}.Type":
                    ModelState.AddModelError(nameof(model.Type), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.Path)}":
                    ModelState.AddModelError(nameof(model.Path), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.H1)}":
                    ModelState.AddModelError(nameof(model.H1), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.CanonicalUrl)}":
                    ModelState.AddModelError(nameof(model.CanonicalUrl), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaTitle)}":
                    ModelState.AddModelError(nameof(model.MetaTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaDescription)}":
                    ModelState.AddModelError(nameof(model.MetaDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaKeywords)}":
                    ModelState.AddModelError(nameof(model.MetaKeywords), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaRobots)}":
                    ModelState.AddModelError(nameof(model.MetaRobots), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.OgTitle)}":
                    ModelState.AddModelError(nameof(model.OgTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.OgDescription)}":
                    ModelState.AddModelError(nameof(model.OgDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.OgImageUrl)}":
                    ModelState.AddModelError(nameof(model.OgImageUrl), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.OgType)}":
                    ModelState.AddModelError(nameof(model.OgType), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.TwitterCard)}":
                    ModelState.AddModelError(nameof(model.TwitterCard), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.ContentHtml)}":
                    ModelState.AddModelError(nameof(model.ContentHtml), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.Summary)}":
                    ModelState.AddModelError(nameof(model.Summary), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.SchemaJsonLd)}":
                    ModelState.AddModelError(nameof(model.SchemaJsonLd), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.BreadcrumbsJson)}":
                    ModelState.AddModelError(nameof(model.BreadcrumbsJson), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.HreflangMapJson)}":
                    ModelState.AddModelError(nameof(model.HreflangMapJson), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.SeoScore)}":
                    ModelState.AddModelError(nameof(model.SeoScore), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.SitemapPriority)}":
                    ModelState.AddModelError(nameof(model.SitemapPriority), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.SitemapFrequency)}":
                    ModelState.AddModelError(nameof(model.SitemapFrequency), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.RedirectFromJson)}":
                    ModelState.AddModelError(nameof(model.RedirectFromJson), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.IsIndexed)}":
                    ModelState.AddModelError(nameof(model.IsIndexed), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.Language)}":
                    ModelState.AddModelError(nameof(model.Language), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.Region)}":
                    ModelState.AddModelError(nameof(model.Region), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.HeaderScripts)}":
                    ModelState.AddModelError(nameof(model.HeaderScripts), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.FooterScripts)}":
                    ModelState.AddModelError(nameof(model.FooterScripts), domainException.Message);
                    return View(model);
            }

            return View(model);
        }


        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.MetaTitle);
        return LocalRedirect("/Pages/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, string type, CancellationToken cancellationToken)
    {
        var pageType = typeof(Page).Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == type && typeof(Page).IsAssignableFrom(t));
        
        var page = await pageService.GetByIdAsync(id, pageType, cancellationToken);
        if (page is null) return NotFound();

        ViewData["Title"] = $"Edit Page";
        return View(EditPageViewModel.FromDTO(page));
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditPageViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await pageService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(Page)}.Type":
                    ModelState.AddModelError(nameof(model.Type), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.Path)}":
                    ModelState.AddModelError(nameof(model.Path), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.H1)}":
                    ModelState.AddModelError(nameof(model.H1), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.CanonicalUrl)}":
                    ModelState.AddModelError(nameof(model.CanonicalUrl), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaTitle)}":
                    ModelState.AddModelError(nameof(model.MetaTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaDescription)}":
                    ModelState.AddModelError(nameof(model.MetaDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaKeywords)}":
                    ModelState.AddModelError(nameof(model.MetaKeywords), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaRobots)}":
                    ModelState.AddModelError(nameof(model.MetaRobots), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.OgTitle)}":
                    ModelState.AddModelError(nameof(model.OgTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.OgDescription)}":
                    ModelState.AddModelError(nameof(model.OgDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.OgImageUrl)}":
                    ModelState.AddModelError(nameof(model.OgImageUrl), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.OgType)}":
                    ModelState.AddModelError(nameof(model.OgType), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.TwitterCard)}":
                    ModelState.AddModelError(nameof(model.TwitterCard), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.ContentHtml)}":
                    ModelState.AddModelError(nameof(model.ContentHtml), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.Summary)}":
                    ModelState.AddModelError(nameof(model.Summary), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.SchemaJsonLd)}":
                    ModelState.AddModelError(nameof(model.SchemaJsonLd), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.BreadcrumbsJson)}":
                    ModelState.AddModelError(nameof(model.BreadcrumbsJson), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.HreflangMapJson)}":
                    ModelState.AddModelError(nameof(model.HreflangMapJson), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.SeoScore)}":
                    ModelState.AddModelError(nameof(model.SeoScore), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.SitemapPriority)}":
                    ModelState.AddModelError(nameof(model.SitemapPriority), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.SitemapFrequency)}":
                    ModelState.AddModelError(nameof(model.SitemapFrequency), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.RedirectFromJson)}":
                    ModelState.AddModelError(nameof(model.RedirectFromJson), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.IsIndexed)}":
                    ModelState.AddModelError(nameof(model.IsIndexed), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.Language)}":
                    ModelState.AddModelError(nameof(model.Language), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.Region)}":
                    ModelState.AddModelError(nameof(model.Region), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.HeaderScripts)}":
                    ModelState.AddModelError(nameof(model.HeaderScripts), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.FooterScripts)}":
                    ModelState.AddModelError(nameof(model.FooterScripts), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Path);
        return LocalRedirect("/Pages/Index");
    }
}