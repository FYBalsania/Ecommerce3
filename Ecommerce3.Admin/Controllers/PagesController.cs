using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class PagesController : Controller
{
    // private readonly IPageService _pageService;
    //
    // public PagesController(IPageService pageService)
    // {
    //     _pageService = pageService;
    // }
    //
    // [HttpGet]
    // public async Task<IActionResult> Index(string? name, int pageNumber, int pageSize, CancellationToken cancellationToken)
    // {
    //     var (pages, total) = await _pageService.GetPageListItemsAsync(name, 1, 10, cancellationToken);
    //     var response = new PagesIndexResponse
    //     {
    //         PageName = name,
    //         PageNumber = pageNumber,
    //         PageSize = pageSize,
    //         PageListItems = pages,
    //         PageListItemsCount = total
    //     };
    //     ViewData["Title"] = "Pages";
    //     return View(response);
    // }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View();   
    }
}