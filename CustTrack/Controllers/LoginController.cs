using CustTrack.Models.EntityFramework;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace CustTrack.Controllers
{
    public class LoginController : Controller
    {
        SalesManContextEntities db = new SalesManContextEntities();
    
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
            var user = db.T_Employee.FirstOrDefault(x => x.employee_username.Equals(employee.employee_username) && x.employee_password.Equals(employee.employee_password));
            
            if (user == null)
            {
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya parola";
                return View();
            }
            else
            {
                switch (user.employee_authority_id)
                {
                    case 1:
                        FormsAuthentication.SetAuthCookie(user.employee_username, false);
                        return RedirectToAction("Index", "Admin");
                        break;
                    case 2:
                        FormsAuthentication.SetAuthCookie(user.employee_username, false);
                        return RedirectToAction("Index", "Home");
                        break;
                    default:
                        return View();
                        break;
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