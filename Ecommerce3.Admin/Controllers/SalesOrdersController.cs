using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class SalesOrdersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}