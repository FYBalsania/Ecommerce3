using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class CategoriesController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Title"] = "Categories";
        return View();
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        ViewData["Title"] = "Add Category";
        return View();
    }
    
    [HttpGet]
    public IActionResult Edit()
    {
        ViewData["Title"] = "Edit Category - Electronics";
        return View();
    }
}