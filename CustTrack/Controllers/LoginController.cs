using CustTrack.Models.EntityFramework;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace CustTrack.Controllers
{
    public class LoginController : Controller
    {
        SalesManContextEntities db = new SalesManContextEntities();
        public T_Employee user;
        // GET: Security
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(T_Employee employee)
        {
            var hash = new HashIt();
            var result = hash.Hashit(employee.employee_password);

            user = db.T_Employee.FirstOrDefault(x => x.employee_username.Equals(employee.employee_username) && x.employee_password.Equals(result));
            
            if (user == null)
            {
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya parola";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.employee_username, false);
                return RedirectToAction("Index", "Calendar");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}