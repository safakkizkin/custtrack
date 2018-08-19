using CustTrack.Models;
using CustTrack.Models.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace CustTrack.Controllers
{
    public class CustomersController : Controller
    {
        SalesManContextEntities db = new SalesManContextEntities();

        // GET: Customer
        public ActionResult Index()
        {
            return View(db.T_Customer.ToList());
        }

        //DIRECT TO UPDATE OR ADD PAGE
        public ActionResult Update(int id)
        {
            if (id == 0)
            {
                var _custModel = new CustomerModel
                {
                    _Emp = db.T_Employee.First(x => x.employee_username == User.Identity.Name),
                    _Cust = new T_Customer
                    {
                        customer_id = 0
                    }
                };
                return View(_custModel);
            }
            else
            {
                var _custModel = new CustomerModel
                {
                    _Emp = db.T_Employee.First(x => x.employee_username == User.Identity.Name),
                    _Cust= db.T_Customer.Find(id)
                };
                return View(_custModel);
            }
            
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public ActionResult Update(CustomerModel _cus)
        {
            if(_cus._Cust.customer_id == 0)
            {

            }
            else
            {

            }
            return RedirectToAction("Index", "Customers");
        }
    }
}