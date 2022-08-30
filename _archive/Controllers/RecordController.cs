using _archive.Models;
using Microsoft.AspNetCore.Mvc;

namespace _archive.Controllers
{
    public class RecordController : Controller
    {

        ApplicationDbContext _db = new ApplicationDbContext();

        public RecordController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {

            IEnumerable<RecordsModel> recordsList = _db.RecordsModel.ToList();

            return View(recordsList);
        }

        public IActionResult Create()
        {
            return View();
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
