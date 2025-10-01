using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class CategoriesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}