using System.Web.Mvc;

namespace TelephoneDirectory.WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ModuleController : Controller
    {
        // GET: Admin/Module
        public ActionResult Index()
        {
            return View();
        }
    }
}