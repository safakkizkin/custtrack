using CustTrack.Models;
using CustTrack.Models.EntityFramework;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
                    _Cust = new T_Customer
                    {
                        customer_id = 0
                    },
                    _City = new T_City
                    {
                        city_id = 0,
                        city_name = "Şehir Seçiniz"
                    },
                    _Cities = db.T_City.ToList(),
                    _District = new T_District
                    {
                        city_id = 0,
                        district_id = 0,
                        district_name = "İlk Şehir Seçiniz"
                    },
                    _Districts = db.T_District.Where(m => m.city_id == 0).ToList()
                };
                return View(_custModel);
            }
            else
            {
                var _cus = db.T_Customer.Find(id);
                var _custModel = new CustomerModel
                {
                    _Cust = _cus,
                    _City = db.T_City.First(m => m.city_id == _cus.customer_city_id),
                    _Cities = db.T_City.OrderBy(x => x.city_name).ToList(),
                    _District = db.T_District.First(d => d.district_id == _cus.customer_district_id),
                    _Districts = db.T_District.Where(d => d.city_id == _cus.customer_city_id).OrderBy(x => x.district_name).ToList()
                };



                return View(_custModel);
            }
            
        }

        [HttpPost]
        public ActionResult GetDistrictById(string stateId)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var id = Convert.ToInt32(stateId);
            string result = javaScriptSerializer.Serialize(db.T_District.Where(x => x.city_id == id).OrderBy(x => x.district_name).ToList());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public ActionResult Update(CustomerModel _cus, FormCollection form, string submit)
        {
            switch (submit)
            {
                case "Sil":
                    { 
                        var cust = db.T_Customer.Find(_cus._Cust.customer_id);
                        if (cust == null)
                        {
                            return HttpNotFound();
                        }db.T_Customer.Remove(cust);
                        break;
                    }
                default:
                    {
                        
                        if (_cus._Cust.customer_id == 0)
                        {
                            _cus._Cust.customer_city_id = int.Parse(form["DropDownListCities"]);
                            _cus._Cust.customer_district_id = int.Parse(form["DropDownListDistricts"]);
                        }
                        else
                        {
                            db.T_Customer.Find(_cus._Cust.customer_id).customer_city_id = form["DropDownListCities"].ToString() == "" ? db.T_Customer.Find(_cus._Cust.customer_id).customer_city_id : int.Parse(form["DropDownListCities"]);
                            db.T_Customer.Find(_cus._Cust.customer_id).customer_district_id =form["DropDownListDistricts"].ToString() == "" ? db.T_Customer.Find(_cus._Cust.customer_id).customer_district_id : int.Parse(form["DropDownListDistricts"]);
                        }
                        db.T_Customer.AddOrUpdate(_cus._Cust);
                        break;
                    }
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Customers");

        }
    }
}