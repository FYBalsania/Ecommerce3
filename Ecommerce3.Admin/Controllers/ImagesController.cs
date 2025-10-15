using Ecommerce3.Admin.ViewModels.Image;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ImagesController : Controller
{
    public ImagesController()
    {
        
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddImageViewModel model, CancellationToken cancellationToken)
    {
        return View();
    }
    
}