using _archive.Models;
using Microsoft.AspNetCore.Mvc;

namespace _archive.Controllers
{
    public class AuthController : Controller

    {
        private readonly ApplicationDbContext _db;

        public AuthController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Login()
        {
            return View();
        }


 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email,string password)
        {
            if(email != null || password != null)
            {
                var user = _db.UsersModel.FirstOrDefault(user=>user.Email == email || user.Password == password);

                if(user == null)
                {
                    ViewBag.error = "The credentials you entered does not match any user.";
                    return View("Login");
                }
                var username = user.UserName;
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Record", null);
            }
            else
            {
                ViewBag.error = "Invalid credentials. Please get in touch with admin.";
                return View("Login");
            }
           
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
