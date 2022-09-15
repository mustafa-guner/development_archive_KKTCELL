using _archive.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; //required for "Include" chain function.
using Newtonsoft.Json.Linq;

namespace _archive.Controllers
{
    public class RecordController : Controller
    {

        private readonly ApplicationDbContext _db;
        

        public RecordController(ApplicationDbContext db)
        {
            _db = db;
        }

 

        public IActionResult Index(string sort_Order,string key_search, string Filter_Value,int Page_No,int pg =1)
        {
            var sessionString = HttpContext.Session.GetString("user");
    

            if (sessionString == null) return RedirectToAction("Login", "Auth", null);

            var sessionUser = JObject.Parse(sessionString)!;
           
       


            ViewBag.ArchiveID = String.IsNullOrEmpty(sort_Order) ? "ArchiveID" : "";
            ViewBag.ChangesetID = String.IsNullOrEmpty(sort_Order) ? "ChangesetID" : "";
            ViewBag.BPMNo = String.IsNullOrEmpty(sort_Order) ? "BPMNo" : "";
            ViewBag.UserName = String.IsNullOrEmpty(sort_Order) ? "UserName" : "";
            ViewBag.Title = String.IsNullOrEmpty(sort_Order) ? "Title" : "";


            if (key_search != null)
            {
                pg = 1;
            }
            else
            {
                key_search = Filter_Value;
            }



            IEnumerable<RecordsModel> records = _db.RecordsModel.Include(records => records.User).ToList();


            if (!String.IsNullOrEmpty(key_search))
            {
                records = records.Where(record => record.BPMNo.ToString().ToLower().Contains(key_search.ToLower()) || record.User.UserName.ToLower().Contains(key_search.ToLower()) || record.ArchiveID.ToString().ToLower().Contains(key_search.ToLower()) || record.Title.ToLower().Contains(key_search.ToLower())
                || record.Status.ToLower().Contains(key_search.ToLower()) || record.ChangesetID.ToString().ToLower().Contains(key_search.ToLower()));
            }

            switch (sort_Order)
            {
                case "ArchiveID":
                    records = records.OrderByDescending(record => record.ArchiveID);
                    break;
                case "ChangesetID":
                    records = records.OrderByDescending(record => record.ChangesetID);
                    break;
                case "BPMNo":
                    records = records.OrderByDescending(record => record.BPMNo);
                    break;
                case "UserName":
                    records = records.OrderByDescending(record => record.User.UserName);
                    break;
                case "Title":
                    records = records.OrderByDescending(record => record.Title);
                    break;
                default:
                    records = records.OrderBy(record => record.ArchiveID);
                    break;

            }

    




            const int pageSize = 4;
            if (pg < 1) pg = 1;

            int recordsCount = records.Count();

            var pagination = new Pagination(recordsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;
            /*
            If page no is 2 and page size 10 then;
               recSkip = (2-1)*10
            */

            var data = records.Skip(recSkip).Take(pagination.PageSize).ToList();
            var currentUser = _db.UsersModel.SingleOrDefault(user => user.UserName == sessionUser["username"].ToString() && user.Id.ToString() == sessionUser["id"].ToString());
            var isCurrentUserAdmin = currentUser.Role == Data.Enums.UsersRoles.Admin;

            this.ViewBag.Pagination = pagination;
            this.ViewBag.IsCurrentUserAdmin = isCurrentUserAdmin;
            this.ViewBag.CurrentUser = currentUser;

            return View(data);
           
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {

            var sessionString = HttpContext.Session.GetString("user");

            var sessionUser = JObject.Parse(sessionString);

            var currentUser = _db.UsersModel.SingleOrDefault(user => user.UserName == sessionUser["username"].ToString() && user.Id.ToString() == sessionUser["id"].ToString());

            if (currentUser == null) return RedirectToAction("Login", "Auth", null);

            IEnumerable<RecordsModel> records = _db.RecordsModel.Include(records => records.User);

            var record = records.FirstOrDefault(r => r.ArchiveID == id);

           
            if (record == null) return NotFound();

            this.ViewBag.CurrentUser = currentUser;

            return View(record);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind]RecordsModel record)
        {
            var sessionString = HttpContext.Session.GetString("user");

            var sessionUser = JObject.Parse(sessionString);

            var currentUser = _db.UsersModel.SingleOrDefault(user => user.UserName == sessionUser["username"].ToString() && user.Id.ToString() == sessionUser["id"].ToString());

            if (currentUser == null) return RedirectToAction("Login", "Auth", null);

         
            var newRecord = new RecordsModel()
            {
                ChangesetID = record.ChangesetID,
                Title = record.Title,
                UpdateDetails = record.UpdateDetails,
                Analysis = record.Analysis,
                BPMNo = record.BPMNo,
                TestResults = record.TestResults,
                StartTime = record.StartTime,
                EndTime = record.EndTime,
                ReleaseTime = record.ReleaseTime,
                Status = record.Status,
                RecordsCategory = record.RecordsCategory,
                User = currentUser,
                UserID = currentUser.Id
            };

            _db.RecordsModel.Add(newRecord);
            await _db.SaveChangesAsync();
          

            var message = string.Join(" | ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));

            this.ViewBag.ErrorMessage = message;
           



            return RedirectToAction("Index");

            //return View(record); 
        }


        public IActionResult Edit(int id)
        {
            var record = _db.RecordsModel.Find(id);
            if (record == null) return NotFound();
            return View(record);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind]RecordsModel record)

        {

            var sessionString = HttpContext.Session.GetString("user");

            var sessionUser = JObject.Parse(sessionString);

            var currentUser = _db.UsersModel.SingleOrDefault(user => user.UserName == sessionUser["username"].ToString() && user.Id.ToString() == sessionUser["id"].ToString());

            if (currentUser == null) return RedirectToAction("Login", "Auth", null);

         

            var updatedRecord = new RecordsModel()
            {
                ArchiveID = id,
                ChangesetID = record.ChangesetID,
                Title = record.Title,
                UpdateDetails = record.UpdateDetails,
                Analysis = record.Analysis,
                BPMNo = record.BPMNo,
                TestResults = record.TestResults,
                StartTime = record.StartTime,
                EndTime = record.EndTime,
                ReleaseTime = record.ReleaseTime,
                Status = record.Status,
                RecordsCategory = record.RecordsCategory,
                User = currentUser,
                UserID = currentUser.Id

            };

        

                _db.RecordsModel.Update(updatedRecord);
                await _db.SaveChangesAsync();
             

            var message = string.Join(" | ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));


            this.ViewBag.ErrorMessage = message;

            return RedirectToAction("Index");

        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var sessionString = HttpContext.Session.GetString("user");

            if (sessionString == null) return RedirectToAction("Login", "Auth", null);

            var sessionUser = JObject.Parse(sessionString);

            //var user = _db.UsersModel.SingleOrDefault(user=>user.UserName == sessionUser["username"].ToString()); 

     
        
            if (sessionUser["role"].ToString() == Data.Enums.UsersRoles.Admin.ToString())
            {
                var record = await _db.RecordsModel.FindAsync(id);
              
                if (record == null) return NotFound();
                _db.RecordsModel.Remove(record);
                await _db.SaveChangesAsync();
            }   
          
            return RedirectToAction("Index");
        }
    }
}
