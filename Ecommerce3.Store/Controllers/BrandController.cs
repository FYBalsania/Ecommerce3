using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Store.Controllers;

public class BrandController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}