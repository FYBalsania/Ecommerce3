using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }
}