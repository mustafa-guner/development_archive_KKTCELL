using Microsoft.AspNetCore.Mvc;

namespace _archive.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
