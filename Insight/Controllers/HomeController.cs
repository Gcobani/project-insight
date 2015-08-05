using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Insight.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region Authorize Check

            if(HttpContext.User.IsInRole("root"))
            { return RedirectToAction("Dashboard"); }

            #endregion

            return View();
        }
        [Authorize(Roles="root")]
        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dashboard(FormCollection collector)
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}