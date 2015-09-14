using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Insight.Data;
using Insight.BLogic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Insight.Models;
using System.Web.Mvc;

namespace Insight.Controllers
{
    public class LectureController : Controller
    {
        ApplicationUserManager UserManager;
        // GET: Lecture
        public ActionResult Index()
        {
            return View();
        }

        // GET: Lecture/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lecture/Create
        public ActionResult Create()
        {
            Lecture _lecture = new Lecture();
            return View(_lecture);
        }

        // POST: Lecture/Create
        [HttpPost]
        public ActionResult Create(Lecture _lecture, FormCollection collector)
        {
            BusinessLogicHandler _gateWay = new BusinessLogicHandler();

            #region Identity

            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            UserManager = new ApplicationUserManager(myStore);
            var user = UserManager.FindByName(HttpContext.User.Identity.Name);

            #endregion

            #region Get Lecturer
            //Lecturer staffMember = new Lecturer();
            //staffMember = _gateWay.GetLecturer(user.Id);
            #endregion

            #region Setting things up
            try
            {
                string[] dateSlice = collector.GetValue("dateHoldhidden").AttemptedValue.Split(' ');
                string timeSlice = collector.GetValue("timeHoldhidden").AttemptedValue;
                _lecture.TimeSlot = dateSlice[0] + " " + timeSlice;
            }
            catch
            {
                return View(_lecture);
            }


            #endregion
            try
            {
                if(_gateWay.InsertLecture(_lecture))
                { return RedirectToAction("Index");}
                else
                { return View(_lecture); }
            }
            catch
            {
                return View(_lecture);
            }
        }

        // GET: Lecture/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lecture/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lecture/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lecture/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
