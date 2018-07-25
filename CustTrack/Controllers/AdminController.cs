using CustTrack.Models.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace CustTrack.Controllers
{
    public class AdminController : Controller
    {
        SalesManContextEntities db = new SalesManContextEntities(); 
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Departmant()
        {
            var departments = db.T_Department.ToList();
            return View(departments);
        }

        public ActionResult Employee()
        {
            var employees = db.T_Employee.ToList();
            return View(employees);
        }
    }
}