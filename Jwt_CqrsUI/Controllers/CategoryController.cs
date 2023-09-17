using Microsoft.AspNetCore.Mvc;

namespace Jwt_CqrsUI.Controllers;
public class CategoryController : Controller
{
    public IActionResult List()
    {
        return View();
    }
}
