using Microsoft.AspNetCore.Mvc;

namespace _archive.Controllers
{
    
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username != null && username.Equals("acc1") && password.Equals("123"))
            {
                HttpContext.Session.SetString("username", username);
                return View("Home/Index");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Index");
            }
        }


        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }


}
