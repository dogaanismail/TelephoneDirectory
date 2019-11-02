using System.Web.Mvc;

namespace TelephoneDirectory.WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        // GET: Admin/Employee
        public ActionResult Index()
        {
            return View();
        }
    }
}