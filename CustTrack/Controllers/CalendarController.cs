using CustTrack.Models;
using CustTrack.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace CustTrack.Controllers
{
    public class CalendarController : Controller
    {
        SalesManContextEntities db = new SalesManContextEntities();

        private static DateTime sd, ed;

        #region Index method  
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id)
        {
            if(id == 0)
            {
                var t_ = new AppointmentModel
                {
                    _Appointment = new T_Appointment()
                    {
                        appointment_start_date = sd,
                        appointment_end_date = ed,
                        employee_id = db.T_Employee.First(x => x.employee_username.Equals(User.Identity.Name)).employee_id,
                        appointment_before_note = "Randevu öncesi not",
                        appointment_after_note = "Randevu sonrası not"
                    },
                    _Customers = db.T_Customer.ToList(),
                    _AppointmentTypes = db.T_AppointmentColor.ToList()
                };
                return View(t_);
            }
            else
            {
                var t_ = new AppointmentModel
                {
                    _Appointment = db.T_Appointment.Find(id),
                    _Customers = db.T_Customer.ToList(),
                    _AppointmentTypes = db.T_AppointmentColor.ToList()
                };
                return View(t_);
            }
        }

        #endregion

        #region Reaching Values

        public ActionResult GetCalendarData()
        {
            JsonResult result = new JsonResult();
            try
            {
                List<T_Appointment> data = db.T_Appointment.ToList();
                result = this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }

        [HttpPost]
        public ActionResult CreateAppointmentAll(string startDate, string endDate)
        {
            sd = DateTime.Parse(startDate);
            ed = DateTime.Parse(endDate);
            return Json(true);
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentModel appointmentmodel, FormCollection form, string submit)
        {
            if (appointmentmodel._Appointment.appointment_id== 0)
            {
                appointmentmodel._Appointment.appointment_color_value = form["DropDownListColors"].ToString();
                appointmentmodel._Appointment.customer_id = int.Parse(form["DropDownListCustomers"].ToString());
            }
            else
            {
                appointmentmodel._Appointment.appointment_color_value = form["DropDownListColors"].ToString();
                appointmentmodel._Appointment.customer_id = form["DropDownListCustomers"].ToString() == "" ? db.T_Appointment.Find(appointmentmodel._Appointment.appointment_id).customer_id : int.Parse(form["DropDownListCustomers"]);
            }
            

            db.T_Appointment.AddOrUpdate(appointmentmodel._Appointment);
            db.SaveChanges();
            return RedirectToAction("Index", "Calendar");
        }
    }
}