using _archive.Models;
using Microsoft.AspNetCore.Mvc;

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
            ViewBag.ArchiveID = String.IsNullOrEmpty(sort_Order) ? "ArchiveID" : "";
            ViewBag.ChangesetID = String.IsNullOrEmpty(sort_Order) ? "ChangesetID" : "";
            ViewBag.BPMNo = String.IsNullOrEmpty(sort_Order) ? "BPMNo" : "";
            ViewBag.UserID = String.IsNullOrEmpty(sort_Order) ? "UserID" : "";
            ViewBag.Title = String.IsNullOrEmpty(sort_Order) ? "Title" : "";


            if (key_search != null)
            {
                pg = 1;
            }
            else
            {
                key_search = Filter_Value;
            }



            IEnumerable<RecordsModel> records = _db.RecordsModel.ToList();


            if (!String.IsNullOrEmpty(key_search))
            {
                records = records.Where(record => record.BPMNo.ToString().Contains(key_search) || record.ArchiveID.ToString().Contains(key_search) || record.Title.Contains(key_search)
                || record.Status.Contains(key_search) || record.ChangesetID.ToString().Contains(key_search) );
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
                case "UserID":
                    records = records.OrderByDescending(record => record.UserID);
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

            this.ViewBag.Pagination = pagination;

            return View(data);
           
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var record = _db.RecordsModel.Find(id);
            if (record == null) return NotFound();

            return View(record);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RecordsModel record)
        {

            if (ModelState.IsValid)
            {
                _db.RecordsModel.Add(record);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(record); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RecordsModel record)
        {
            if (ModelState.IsValid)
            {
                _db.RecordsModel.Update(record);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(record);

        }


        public IActionResult Delete(int id)
        {
            var record = _db.RecordsModel.Find(id);
            if (record == null) return NotFound();
            _db.RecordsModel.Remove(record);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
