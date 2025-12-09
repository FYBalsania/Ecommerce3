using System.Diagnostics;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.StoreFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.StoreFront.Controllers;

public class HomeController(IPageService pageService, IConfiguration configuration, ILogger<HomeController> logger)
    : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var page = await pageService.GetByPathAsync("/", cancellationToken);
        return View(page);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}