using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Store.Controllers;

public class BrandCategoryController : Controller
{
    public BrandCategoryController()
    {
        
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}