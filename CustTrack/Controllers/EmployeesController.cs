using CustTrack.Models;
using CustTrack.Models.EntityFramework;
using System.Data.Entity.Migrations;
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
                _Departmans = db.T_Department
                               .ToList(),
                _Employees = db.T_Employee
                                .Where(x => x.employee_authority_id != 1).ToList(),
                _Authorities = db.T_Authority
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
        public ActionResult Update(UpdateEmployeeModel updateemployeemodel, FormCollection form, string submit)
        {
            switch (submit)
            {
                case "Sil":
                    {
                        var emp = db.T_Employee.Find(updateemployeemodel._Employee.employee_id);
                        if (emp == null)
                        {
                            return HttpNotFound();
                        }
                        db.T_Employee.Remove(emp);
                        break;
                    }
                default:
                    {
                        var hash = new HashIt();

                        if (updateemployeemodel._Employee.employee_id == 0)
                        {
                            updateemployeemodel._Employee.department_id = int.Parse(form["DropDownListDepartmants"].ToString());
                            updateemployeemodel._Employee.employee_authority_id = int.Parse(form["DropDownListAuthorities"].ToString());
                            updateemployeemodel._Employee.employee_password = hash.Hashit(updateemployeemodel._Employee.employee_password);
                        }
                        else
                        {
                            if (updateemployeemodel._Employee.employee_password == null)
                            {
                                updateemployeemodel._Employee.employee_password = db.T_Employee.Find(updateemployeemodel._Employee.employee_id).employee_password;
                            }
                            else
                            {
                                updateemployeemodel._Employee.employee_password = hash.Hashit(updateemployeemodel._Employee.employee_password);
                            }

                            db.T_Employee.Find(updateemployeemodel._Employee.employee_id).department_id = form["DropDownListDepartmants"].ToString() == "" ? db.T_Employee.Find(updateemployeemodel._Employee.employee_id).department_id : int.Parse(form["DropDownListDepartmants"]);
                            db.T_Employee.Find(updateemployeemodel._Employee.employee_id).employee_authority_id = form["DropDownListAuthorities"].ToString() == "" ? db.T_Employee.Find(updateemployeemodel._Employee.employee_id).employee_authority_id : int.Parse(form["DropDownListAuthorities"]);
                        }

                        db.T_Employee.AddOrUpdate(updateemployeemodel._Employee);
                        break;
                    }
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Employees");
        }
    }
}