using CustTrack.Models;
using CustTrack.Models.EntityFramework;
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
                    Department = new T_Department() { department_id = 0, department_name = "" },
                    EmployeeModel = db.T_Employee.ToList(),
                    ManagerModel = db.T_Employee.ToList()
                };
                return View(viewModel);
            }
            var dep = db.T_Department.Find(id);
            if (dep != null)
            {
                var viewModel = new UpdateDepartmentModel
                {
                    Department = dep,
                    EmployeeModel = db.T_Employee.ToList().FindAll(m => m.department_id == dep.department_id && m.employee_authority_id == 3),
                    ManagerModel = db.T_Employee.ToList().FindAll(m => m.department_id == dep.department_id && m.employee_authority_id == 2)
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
        public ActionResult Update(UpdateDepartmentModel dep)
        {
            // New Departmant
            if (dep.Department.department_id == 0)
            {
                db.T_Department
                  .Add(dep.Department);
            }
            // Update Departman
            else
            {
                var dep_update = db.T_Department
                                   .Find(dep.Department.department_id);
                if (dep_update == null)
                {
                    return HttpNotFound();
                }
                dep_update.department_name = dep.Department.department_name;
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Departmants");
        }

        // Removing Process
        public ActionResult Remove(int id)
        {
            var dep = db.T_Department
                        .Find(id);

            if (dep == null)
            {
                return HttpNotFound();
            }

            db.T_Department
              .Remove(dep);

            return RedirectToAction("Index", "Departmants");
        }
    }
}