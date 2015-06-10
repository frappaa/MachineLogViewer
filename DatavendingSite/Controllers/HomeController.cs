using System.Web.Mvc;

namespace DatavendingSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var controllerName = User.IsInRole("admin") ? "Account" : "Machine";
                return RedirectToAction("Index", controllerName);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}