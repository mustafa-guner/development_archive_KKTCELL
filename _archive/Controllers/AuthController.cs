using _archive.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            if(HttpContext.Session.GetString("user") != null)
            {
                return RedirectToAction("Index", "Record", null);
            }
            return View();
        }


 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email,string password)
        {
            if(email != null || password != null)
            {
                var user = _db.UsersModel.FirstOrDefault(user=>user.Email == email );

                if(user == null)
                {
                    this.ViewBag.error = "The credentials you entered does not match any user account.";
                    return View("Login");
                }
                if (user.Password != password)
                {
                    this.ViewBag.error = "Password is not correct. Please try again or get in touch with the system admin.";
                    return View("Login");
                }

                HttpContext.Session.SetString("user", JsonSerializer.Serialize(new {username = user.UserName,role=user.Role == Data.Enums.UsersRoles.Admin ?"Admin" :"Developer",id = user.Id }));
                return RedirectToAction("Index", "Record", null);
            }
            else
            {
                this.ViewBag.error = "Please fill all the required fields.";
                return View("Login");
            }
           
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login","Auth",null);
        }
    }
}
