﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
        }

        // POST: Lecture/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
