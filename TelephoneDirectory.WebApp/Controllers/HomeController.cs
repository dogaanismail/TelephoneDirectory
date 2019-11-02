using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelephoneDirectory.Business.Abstract.EntityFramework;
using TelephoneDirectory.Business.DependencyResolvers.Ninject.EntityFramework;

namespace TelephoneDirectory.WebApp.Controllers
{
    public class HomeController : Controller
    {
        #region DefinitionofServices
        private readonly IEmployeeService _employeeService = InstanceFactory.GetInstance<IEmployeeService>();
        #endregion

        #region Index
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(_employeeService.GetList());
        }

        #endregion

        #region EmployeeDetail
        [AllowAnonymous]
        public ActionResult EmployeeDetail(int id)
        {
            return View(_employeeService.GetFirstOrDefaultInclude(id));
        }
        #endregion

    }
}