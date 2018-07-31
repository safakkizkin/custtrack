using CustTrack.Models;
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
            var departs = db.T_Department.ToList();
            return View(departs);
        }

        public ActionResult Employee()
        {
            var employee = db.T_Employee.ToList();
            var ViewModel = new EmployeesModel
            {
                Departmans = db.T_Department.ToList(),
                Employees = db.T_Employee.ToList().FindAll(x => x.employee_authority_id != 1),
                Authorities = db.T_Authority.ToList()
            };
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdateDepartmentModel department)
        {
            var a = ViewBag.Employees;
            return RedirectToAction("Index","Admin");
        }

        public ActionResult Update(int id)
        {
            var dep = db.T_Department.Find(id);
            if(dep != null)
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
                ViewBag.Mesaj = "Bu öğe düzenlenemiyor";
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult EmployeeUpdate (int id)
        {
            var emp = db.T_Employee.Find(id);
            if (emp != null)
            {
                var viewModel = new UpdateEmployeeModel
                {
                    Employee = emp,
                    Deparment = db.T_Department.Find(emp.department_id),
                    Managers = db.T_Employee.ToList().FindAll(x => x.department_id == emp.department_id && x.employee_authority_id == 2)
                };
                return View(viewModel);
            }
            else
            {
                ViewBag.Error = "Bu öğe düzenlenemedi";
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var dep = db.T_Department.Find(id);
            if(dep == null)
            {
                ViewBag.Mesaj = "Bu öğe silinemedi";
            }
            else
            {
                db.T_Department.Remove(dep);
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}