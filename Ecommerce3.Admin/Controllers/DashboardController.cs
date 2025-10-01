using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class DashboardController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Title"] = "Dashboard";
        return View();
    }
}