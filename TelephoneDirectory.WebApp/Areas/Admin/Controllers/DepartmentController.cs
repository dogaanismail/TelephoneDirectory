using System.Web.Mvc;

namespace TelephoneDirectory.WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        // GET: Admin/Department
        public ActionResult Index()
        {
            return View();
        }
    }
}