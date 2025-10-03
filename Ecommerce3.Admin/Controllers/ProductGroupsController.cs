using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductGroupsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Title"] = "Product Groups";
        return View();
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        ViewData["Title"] = "Add Product Group";
        return View();
    }
    
    [HttpGet]
    public IActionResult Edit()
    {
        ViewData["Title"] = "Edit Product Group - IPhone 17";
        return View();
    }
}