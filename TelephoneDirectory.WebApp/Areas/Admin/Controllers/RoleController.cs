﻿using System.Web.Mvc;

namespace TelephoneDirectory.WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        // GET: Admin/Role
        public ActionResult Index()
        {
            return View();
        }
    }
}