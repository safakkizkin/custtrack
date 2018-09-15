using CustTrack.Models;
using CustTrack.Models.EntityFramework;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace CustTrack.Controllers
{
    public class DepartmantsController : Controller
    {
        SalesManContextEntities db = new SalesManContextEntities();

        // GET: Departmant
        public ActionResult Index()
        {
            var departs = db.T_Department
                            .ToList();
            return View(departs);
        }

        // Direct to Departmant Update Page
        public ActionResult Update(int id)
        {
            if (id == 0)
            {
                var viewModel = new UpdateDepartmentModel
                {
                    _Department = new T_Department() { department_id = 0, department_name = "" },
                    _Employees = db.T_Employee.Where(m => m.employee_authority_id != 1).ToList(),
                    _Authorities = db.T_Authority.ToList()
                };
                return View(viewModel);
            }
            var dep = db.T_Department.Find(id);
            if (dep != null)
            {
                var viewModel = new UpdateDepartmentModel
                {
                    _Department = dep,
                    _Employees = db.T_Employee.Where(m => m.employee_authority_id != 1).ToList(),
                    _Authorities = db.T_Authority.ToList()
                };
                return View(viewModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // Adding and Updating departmant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateDepartmentModel dep, string submit)
        {
            switch (submit)
            {
                case "Sil":
                    {
                        var _dep = db.T_Department.Find(dep._Department.department_id);
                        if (_dep == null)
                        {
                            return HttpNotFound();
                        }
                        db.T_Department.Remove(_dep);
                        break;
                    }
                default:
                    {
                        db.T_Department.AddOrUpdate(dep._Department);
                        break;
                    }
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Departmants");
        }
        
    }
}