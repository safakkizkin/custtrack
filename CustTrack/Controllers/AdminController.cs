using CustTrack.Models;
using CustTrack.Models.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CustTrack.Controllers
{
    public class AdminController : Controller
    {

        SalesManContextEntities db = new SalesManContextEntities();

        #region Page Result

        // Direct to Index Page
        public ActionResult Index()
        {
            var departs = db.T_Department
                            .ToList();
            return View(departs);
        }

        // Direct to Employee Page
        public ActionResult Employees()
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
        public ActionResult Employee(int id)
        {
            // ADD EMPLOYEE
            if (id == 0)
            {
                var addviewmodel = new UpdateEmployeeModel
                {
                    Deparment = new T_Department
                    {
                        department_id = 0,
                        department_name = "Bir Departman Seçiniz"
                    },
                    Departments = db.T_Department
                                    .ToList(),
                    Employee = new T_Employee
                    {
                        employee_id = 0
                    },
                    Managers = db.T_Employee
                                 .Where(m => m.employee_authority_id == 2)
                                 .ToList()
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
                    Deparment = db.T_Department
                                  .Find(employee.department_id),
                    Departments = db.T_Department
                                    .OrderBy(m => m.department_name)
                                    .ToList(),
                    Employee = employee,
                    Managers = db.T_Employee
                                 .Where(m => m.employee_authority_id == 2)
                                 .OrderBy(m => m.employee_name)
                                 .ThenBy(m => m.employee_surname)
                                 .ToList()
                };
                return View(updateviewmodel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // Direct to Departmant Update Page
        public ActionResult Departmant(int id)
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

        #endregion

        #region Department Process

        // Adding and Updating departmant
        [HttpPost]
        public ActionResult Departmant(UpdateDepartmentModel dep)
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
            return RedirectToAction("Index", "Admin");
        }

        // Removing Process
        public ActionResult RemoveDepartmant(int id)
        {
            var dep = db.T_Department
                        .Find(id);

            if (dep == null)
            {
                return HttpNotFound();
            }

            db.T_Department
              .Remove(dep);

            return RedirectToAction("Index", "Admin");
        }

        #endregion

        #region Employees' Process

        [HttpPost]
        public ActionResult Employee(UpdateEmployeeModel updateemployeemodel)
        {
            
            if (updateemployeemodel.Employee.employee_id == 0)
            {
                db.T_Employee
                    .Add(updateemployeemodel.Employee);
            }
            else
            {
                var emp_update = db.T_Employee
                                    .Find(updateemployeemodel.Employee.employee_id);

                if (emp_update == null)
                {
                    return HttpNotFound();
                }
                emp_update.department_id = updateemployeemodel.Employee.department_id;
                emp_update.employee_authority_id = updateemployeemodel.Employee.employee_authority_id;
                emp_update.employee_mail = updateemployeemodel.Employee.employee_mail;
                emp_update.employee_name = updateemployeemodel.Employee.employee_name;
                emp_update.employee_phone = updateemployeemodel.Employee.employee_phone;
                emp_update.employee_surname = updateemployeemodel.Employee.employee_surname;
                emp_update.employee_username = updateemployeemodel.Employee.employee_username;
            }

            db.SaveChanges();

            return RedirectToAction("Employee", "Admin");
        }

        // Removing
        public ActionResult RemoveEmployee(int id)
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

            return RedirectToAction("Index", "Admin");
        }

        #endregion
    }
}