using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Store.Controllers;

public class CategoryController : Controller
{
    public CategoryController()
    {
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}