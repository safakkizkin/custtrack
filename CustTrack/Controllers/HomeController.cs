using CustTrack.Models.EntityFramework;
using System.Web.Mvc;
using System.Linq;

namespace CustTrack.Controllers
{
    public class HomeController : Controller
    {

        SalesManContextEntities db = new SalesManContextEntities();

        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = db.T_Employee.FirstOrDefault(x => x.employee_username == User.Identity.Name);

                switch (user.employee_authority_id)
                {
                    case 1:
                        return RedirectToAction("Index", "Admin");
                    case 2:
                        return RedirectToAction("Index", "Manager");
                    case 3:
                        return View();
                }
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Viewx()
        {
            return View();
        }
    }
}