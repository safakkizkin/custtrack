using CustTrack.Models.EntityFramework;
using System.Linq;
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
            user = db.T_Employee.FirstOrDefault(x => x.employee_username.Equals(employee.employee_username) && x.employee_password.Equals(employee.employee_password));
            
            if (user == null)
            {
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya parola";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.employee_username, false);
                switch (user.employee_authority_id)
                {
                    case 1:
                        return RedirectToAction("Index", "Admin");
                    case 2:
                        return RedirectToAction("Index", "Manager");
                    case 3:
                        return RedirectToAction("Index", "Home");
                    default:
                        ViewBag.Mesaj = "Bir hata oluştu";
                        return View();
                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}