using System.Web.Mvc;

namespace CustTrack.Controllers.Admin
{
    public class EmployeeController : Controller
    {
        // GET: Guncelle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update(int id)
        {
            return View();
        }
    }
}