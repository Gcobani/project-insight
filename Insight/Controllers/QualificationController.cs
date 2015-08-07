﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Insight.Data;
using Insight.BLogic;

namespace Insight.Controllers
{
    public class QualificationController : Controller
    {
        // GET: Qualification
        public ActionResult Index()
        {
            return View();
        }

        // GET: Qualification/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Qualification/Create
        public ActionResult Create()
        {
            Qualification _qualificationModel = new Qualification();
            return View(_qualificationModel);
        }

        // POST: Qualification/Create
        [HttpPost]
        public ActionResult Create(Qualification _qualification)
        {
            try
            {
                BusinessLogicHandler _gateWay = new BusinessLogicHandler();
                if(_gateWay.InsertQualification(_qualification))
                { return RedirectToAction("Index"); }
                else
                { return View(_qualification); }  
            }
            catch
            {
                return View(_qualification);
            }
        }

        // GET: Qualification/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Qualification/Edit/5
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

        // GET: Qualification/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Qualification/Delete/5
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
