﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Insight.BLogic;
using Insight.Data;
using Insight.Models;
using System.Web.Mvc;

namespace Insight.Controllers
{
    public class VenueController : Controller
    {
        // GET: Venue
        public ActionResult Index()
        {
            return View();
        }

        // GET: Venue/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Venue/Create
        public ActionResult Create()
        {
            Venue _venue = new Venue();
            return View(_venue);
        }

        // POST: Venue/Create
        [HttpPost]
        public ActionResult Create(Venue _venue)
        {
            BusinessLogicHandler _gateWay = new BusinessLogicHandler();
            try
            {
                if (_gateWay.InsertVenue(_venue))
                { return RedirectToAction("Index"); }
                else { return View(_venue); }
            }
            catch
            {
                return View(_venue);
            }
        }

        // GET: Venue/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Venue/Edit/5
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

        // GET: Venue/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Venue/Delete/5
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
