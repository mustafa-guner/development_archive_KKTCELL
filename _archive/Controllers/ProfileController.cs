using _archive.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace _archive.Controllers
{
    public class ProfileController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ProfileController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Profile(int? id)
        {
            var sessionString = HttpContext.Session.GetString("user");


            if (sessionString == null) return RedirectToAction("Login", "Auth", null);

            var sessionUser = JObject.Parse(sessionString)!;

            var user = await _db.UsersModel.FindAsync(id);

            if (sessionUser["role"].ToString() == Data.Enums.UsersRoles.Admin.ToString() || sessionUser["id"].ToString() == user.Id.ToString())
            {

                IEnumerable<RecordsModel> records = _db.RecordsModel.Include(records => records.User).ToList();
                var currentUser = _db.UsersModel.SingleOrDefault(user => user.UserName == sessionUser["username"].ToString() && user.Id.ToString() == sessionUser["id"].ToString());
                var isCurrentUserAdmin = currentUser.Role == Data.Enums.UsersRoles.Admin;

                records = records.Where(record => record.UserID == id);

                //this.ViewBag.Pagination = pagination;
                this.ViewBag.IsCurrentUserAdmin = isCurrentUserAdmin;
                this.ViewBag.CurrentUser = currentUser;
                this.ViewBag.Records = records;
                this.ViewBag.RecordsCount = records.Count();

                return View(user);
            }
            return RedirectToAction("Index", "Record", null);

        }


    }
}
