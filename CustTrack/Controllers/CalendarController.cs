using CustTrack.Models;
using CustTrack.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CustTrack.Controllers
{
    public class CalendarController : Controller
    {
        SalesManContextEntities db = new SalesManContextEntities();

        private DateTime sd, ed;

        #region Index method  
        
        public ActionResult Index()
        {
            // Info.  
            return View();
        }

        public ActionResult Create(int id)
        {
            if(id == 0)
            {

            }
            return View();
        }

        #endregion 
        
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
    }
}