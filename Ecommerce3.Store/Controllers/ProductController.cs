using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Store.Controllers;

public class ProductController : Controller
{
    public ProductController()
    {
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}