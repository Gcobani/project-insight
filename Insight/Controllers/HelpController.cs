using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;

namespace Insight.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            #region Authorize Check

            if (HttpContext.User.IsInRole("root"))
            { return RedirectToAction("AdminFAQ"); }

            #endregion

            return View();
        }
        [Authorize(Roles="root")]
        public ActionResult AdminFAQ()
        {
            return View();
        }
    }
}