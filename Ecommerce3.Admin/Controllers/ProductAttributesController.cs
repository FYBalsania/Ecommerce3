using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductAttributesController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Title"] = "Product Attributes";
        return View();
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        ViewData["Title"] = "Add Product Attribute";
        return View();
    }
    
    [HttpGet]
    public IActionResult Edit()
    {
        ViewData["Title"] = "Edit Product Attribute - Color";
        return View();
    }
}