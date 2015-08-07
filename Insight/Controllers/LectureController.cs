using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Insight.Data;
using Insight.BLogic;
using Insight.Models;
using System.Web.Mvc;

namespace Insight.Controllers
{
    public class LectureController : Controller
    {
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
        public ActionResult Create(Lecture _lecture)
        {
            BusinessLogicHandler _gateWay = new BusinessLogicHandler();
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
