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
            
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(RecordsModel record)
        {
            return View(); 
        }
    }
}
