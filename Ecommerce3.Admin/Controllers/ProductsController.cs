using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Title"] = "Products";
        return View();
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        ViewData["Title"] = "Add Product";
        return View();
    }
    
    [HttpGet]
    public IActionResult Edit()
    {
        ViewData["Title"] = "Edit Product - IPhone 17";
        return View();
    }
}