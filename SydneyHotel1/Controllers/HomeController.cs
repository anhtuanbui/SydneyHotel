using SydneyHotel1.Data;
using System.Web.Mvc;

namespace SydneyHotel1.Controllers
{
    public class HomeController : Controller
    {
        SydneyHotel1Context db = new SydneyHotel1Context();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Document()
        {
            var docs = db.Documentations;
            return View(docs);
        }

    }
}