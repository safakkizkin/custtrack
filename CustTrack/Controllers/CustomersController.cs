using CustTrack.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustTrack.Controllers
{
    public class CustomersController : Controller
    {
        SalesManContextEntities db = new SalesManContextEntities();

        // GET: Customer
        public ActionResult Index()
        {
            var _custmodel = new CustomersModel
            {
                _Custs = db.T_Customer.ToList(),
                _Emps  = db.T_Employee.ToList()
            };
            return View(_custmodel);
        }

        //DIRECT TO UPDATE OR ADD PAGE
        public ActionResult Update(int id)
        {
            if (id == 0)
            {
                /*var viewModel = new CustomerModel
                {
                }
                return View(viewModel);*/
            }
            return View(viewModel);
        }
    }
}