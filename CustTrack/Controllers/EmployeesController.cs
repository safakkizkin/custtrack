using CustTrack.Models;
using CustTrack.Models.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace CustTrack.Controllers
{
    public class EmployeesController : Controller
    {

        SalesManContextEntities db = new SalesManContextEntities();

        // Direct to Employee Page
        public ActionResult Index()
        {
            var employee = db.T_Employee
                             .ToList();
            var ViewModel = new EmployeesModel
            {
                Departmans = db.T_Department
                               .ToList(),
                Employees = db.T_Employee.ToList()
                              .FindAll(x => x.employee_authority_id != 1),
                Authorities = db.T_Authority
                                .ToList()
            };
            return View(ViewModel);
        }

        // ADDING and UPDATE EMPLOYEE
        public ActionResult Update(int id)
        {
            // ADD EMPLOYEE
            if (id == 0)
            {
                var addviewmodel = new UpdateEmployeeModel
                {
                    _Department = new T_Department
                    {
                        department_id = 0,
                        department_name = "Bir Departman Seçiniz"
                    },
                    _Departments = db.T_Department
                                    .ToList(),
                    _Employee = new T_Employee
                    {
                        employee_id = 0
                    },
                    _Managers = db.T_Employee
                                 .Where(m => m.employee_authority_id == 2)
                                 .ToList(),
                    _Authority = new T_Authority
                    {
                        authority_id = 0,
                        authority_name = "Yetki Düzeyi"
                    },
                    _Authorities = db.T_Authority.ToList()
                };
                return View(addviewmodel);
            }

            // UPDATE EMPLOYEE
            var employee = db.T_Employee
                             .Find(id);
            if (employee != null)
            {
                var updateviewmodel = new UpdateEmployeeModel
                {
                    _Department = db.T_Department
                                  .Find(employee.department_id),
                    _Departments = db.T_Department
                                    .OrderBy(m => m.department_name)
                                    .ToList(),
                    _Employee = employee,
                    _Managers = db.T_Employee
                                 .Where(m => m.employee_authority_id == 2)
                                 .OrderBy(m => m.employee_name)
                                 .ThenBy(m => m.employee_surname)
                                 .ToList(),
                    _Authority = db.T_Authority
                                    .Find(employee.employee_authority_id),
                    _Authorities = db.T_Authority.ToList()
                };
                return View(updateviewmodel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateEmployeeModel updateemployeemodel, FormCollection form)
        {
            if (updateemployeemodel._Employee.employee_id == 0)
            {
                updateemployeemodel._Employee.department_id = int.Parse(Request.Form["DropDownListDepartmants"].ToString());
                updateemployeemodel._Employee.employee_authority_id = int.Parse(Request.Form["DropDownListAuthorities"].ToString());
                db.T_Employee
                    .Add(updateemployeemodel._Employee);
            }
            else
            {
                var emp_update = db.T_Employee
                                    .Find(updateemployeemodel._Employee.employee_id);

                if (emp_update == null)
                {
                    return HttpNotFound();
                }
                var dp_id = emp_update.department_id;
                var ea_id = emp_update.employee_authority_id;
                emp_update = updateemployeemodel._Employee;
                string selected_auth = form["DropDownListAuthorities"].ToString();

                emp_update.department_id = form["DropDownListDepartmants"].ToString() == "" ? dp_id : int.Parse(form["DropDownListDepartmants"]);
                emp_update.employee_authority_id = form["DropDownListAuthorities"].ToString() == "" ? ea_id : int.Parse(form["DropDownListAuthorities"]);
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Employees");
        }

        // REMOVING
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id)
        {
            var emp = db.T_Employee
                        .Find(id);
            if (emp == null)
            {
                ViewBag.Mesaj = "Bu öğe silinemedi";
            }
            else
            {
                db.T_Employee
                  .Remove(emp);
            }

            return RedirectToAction("Index", "Employees");
        }
    }
}