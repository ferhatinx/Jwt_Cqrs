using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jwt_CqrsUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public string AdminPage()
        {
            return "ADMİN";
        }
        [Authorize(Roles ="Member")]
        public string MemberPage()
        {
            return "MEMBER";
        }
    }
}
