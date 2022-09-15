using _archive.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace _archive.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }



        public IActionResult Users(string sort_Order, string key_search, string Filter_Value, int Page_No, int pg = 1)
        {

            var sessionString = HttpContext.Session.GetString("user");


            if (sessionString == null) return RedirectToAction("Login", "Auth", null);

            var sessionUser = JObject.Parse(sessionString);

            if (sessionUser["role"].ToString() != Data.Enums.UsersRoles.Admin.ToString())
            {
                return RedirectToAction("Index", "Record", null);
            }

            ViewBag.UserName = String.IsNullOrEmpty(sort_Order) ? "UserName" : "";
            ViewBag.Email = String.IsNullOrEmpty(sort_Order) ? "Email" : "";

            if (key_search != null)
            {
                pg = 1;
            }
            else
            {
                key_search = Filter_Value;
            }




            IEnumerable<UsersModel> users = _db.UsersModel.ToList();


            if (!String.IsNullOrEmpty(key_search))
            {
                users = users.Where(user => user.Email.ToLower().Contains(key_search.ToLower()) || user.UserName.ToLower().Contains(key_search.ToLower()));
            }

            switch (sort_Order)
            {
                case "UserName":
                    users = users.OrderByDescending(user => user.UserName);
                    break;
                case "Email":
                    users = users.OrderByDescending(user => user.Email);
                    break;
                default:
                    users = users.OrderBy(user => user.Id);
                    break;

            }


            const int pageSize = 4;
            if (pg < 1) pg = 1;

            int usersCount = users.Count();

            var pagination = new Pagination(usersCount, pg, pageSize);

            int usrSkip = (pg - 1) * pageSize;
            /*
            If page no is 2 and page size 10 then;
               recSkip = (2-1)*10
            */

            var data = users.Skip(usrSkip).Take(pagination.PageSize).ToList();
            var currentUser = _db.UsersModel.SingleOrDefault(user => user.UserName == sessionUser["username"].ToString()  && user.Id.ToString() == sessionUser["id"].ToString());
            var isCurrentUserAdmin = currentUser.Role == Data.Enums.UsersRoles.Admin;

            this.ViewBag.Pagination = pagination;
            this.ViewBag.IsCurrentUserAdmin = isCurrentUserAdmin;
            this.ViewBag.CurrentUserID = currentUser.Id;
            this.ViewBag.CurrentUserName = currentUser.UserName;
            return View(data);

        }


        public IActionResult CreateUser()
        {

            var sessionString = HttpContext.Session.GetString("user");


            if (sessionString == null) return RedirectToAction("Login", "Auth", null);

            var sessionUser = JObject.Parse(sessionString)!;

            if (sessionUser["role"].ToString() != Data.Enums.UsersRoles.Admin.ToString())
            {
                return RedirectToAction("Index", "Record", null);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UsersModel user)
        {

            var sessionString = HttpContext.Session.GetString("user");

            var sessionUser = JObject.Parse(sessionString);



            var currentUser = _db.UsersModel.SingleOrDefault(user => user.UserName == sessionUser["username"].ToString() && user.Id.ToString() == sessionUser["id"].ToString());

            if (sessionString == null || currentUser == null) return RedirectToAction("Login", "Auth", null);

            var isCurrentUserAdmin = currentUser.Role == Data.Enums.UsersRoles.Admin;

            if (!isCurrentUserAdmin) return RedirectToAction("Index", "Record", null);


            var existedUser = _db.UsersModel.SingleOrDefault(u => u.Email == user.Email);

            if (existedUser != null)
            {
                this.ViewBag.ErrorMessage = existedUser.Email + " " + " is already exists.";
                return View();
            }


            var newUser = new UsersModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                RegisteredAt = DateTime.Now,
                LastLogin = DateTime.Now,
                Role = user.Role
            };


            _db.UsersModel.Add(newUser);
            await _db.SaveChangesAsync();


            //var message = string.Join(" | ", ModelState.Values
            //.SelectMany(v => v.Errors)
            //.Select(e => e.ErrorMessage));



            //if(message != null)
            //{
            //    this.ViewBag.ErrorMessage = message;
            //    return View();
            //}


            return RedirectToAction("Users");
        }

        public IActionResult EditUser(int id)
        {
            var user = _db.UsersModel.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, [Bind] UsersModel user)

        {
            var sessionString = HttpContext.Session.GetString("user");

            var sessionUser = JObject.Parse(sessionString);

            var currentUser = _db.UsersModel.SingleOrDefault(user => user.UserName == sessionUser["username"].ToString() && user.Id.ToString() == sessionUser["id"].ToString());

            if (currentUser == null) return RedirectToAction("Login", "Auth", null);



            //var updatedUser = new UsersModel()
            //{
            //   UserName = user.UserName,
            //   Email = user.Email,
            //   Password = user.Password,
            //   Role = user.Role
            //};



            _db.UsersModel.Update(user);
            await _db.SaveChangesAsync();


            var message = string.Join(" | ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));


            this.ViewBag.ErrorMessage = message;

            return RedirectToAction("Users");
         
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var sessionString = HttpContext.Session.GetString("user");

            if (sessionString == null) return RedirectToAction("Login", "Auth", null);

            var sessionUser = JObject.Parse(sessionString);

       
            if (sessionUser["role"].ToString() == Data.Enums.UsersRoles.Admin.ToString())
            {
                var user = await _db.UsersModel.FindAsync(id);

                if (user == null) return NotFound();
                _db.UsersModel.Remove(user);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Users");
            
        }
    }
}
