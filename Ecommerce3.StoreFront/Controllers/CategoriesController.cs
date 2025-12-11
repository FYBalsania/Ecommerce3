using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.StoreFront.Controllers;

[Route("{category0Slug}")]
public class CategoriesController : Controller
{
    [HttpGet("c")]
    public async Task<IActionResult> CategoryLevel0(string category0Slug, CancellationToken cancellationToken)
    {
        await Task.Delay(0, cancellationToken);
        return View();
    }

    [HttpGet("{category1Slug}/c")]
    public async Task<IActionResult> CategoryLevel1(string category0Slug, string category1Slug,
        CancellationToken cancellationToken)
    {
        await Task.Delay(0, cancellationToken);
        return View();
    }

    [HttpGet("{category1Slug}/{category2Slug}/c")]
    public async Task<IActionResult> CategoryLevel2(string category0Slug, string category1Slug, string category2Slug,
        CancellationToken cancellationToken)
    {
        await Task.Delay(0, cancellationToken);
        return View();
    }

    [HttpGet("{category1Slug}/{category2Slug}/{category3Slug}/c")]
    public async Task<IActionResult> CategoryLevel3(string category0Slug, string category1Slug, string category2Slug,
        string category3Slug, CancellationToken cancellationToken)
    {
        await Task.Delay(0, cancellationToken);
        return View();
    }
}