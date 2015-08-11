﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Insight.Models;
using Insight.Data;
using Insight.BLogic;

namespace Insight.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult studReg(string User)
        {
            return View();
        }

        [HttpPost]
        public ActionResult studReg(FormCollection collector)
        {
            return View();
        }

        // GET: Module/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Module/Create
        public ActionResult Create()
        {
            BusinessLogicHandler _gateWay = new BusinessLogicHandler();

            #region Get Qualifications

            List<Qualification> _qualifications = new List<Qualification>();
            _qualifications = _gateWay.GetQualifications();

            #endregion

            CreateModuleViewModel _model = new CreateModuleViewModel();
            List<SelectListItem> _qualList = new List<SelectListItem>();

            foreach( var _qualification in _qualifications){
                _qualList.Add(new SelectListItem { Text=_qualification.QualificationName, Value=_qualification.QualificationCode.ToString() });
            }
            ViewData["Qualifications"] = _qualList;
            _model.Qualifications = _qualList;
            return View(_model);
        }

        // POST: Module/Create
        [HttpPost]
        public ActionResult Create(CreateModuleViewModel _model, FormCollection collector)
        {
            BusinessLogicHandler _gateWay = new BusinessLogicHandler();
            Module _module = new Module();
            _module.ModuleCode = _model.ModuleCode;
            _module.ModuleName = _model.ModuleName;
            _module.NumberOfScheduledClasses = _model.NumberOfScheduledClasses;
            _module.QualificationCode = Convert.ToInt32(collector.GetValue("Qualifications").AttemptedValue);

            try
            {
                
                if(_gateWay.InsertModule(_module))
                { return RedirectToAction("Index");/*CHANGE THIS HERE*/ }
                else
                {

                    #region Load Dropdown

                    List<Qualification> _qualifications = new List<Qualification>();
                    _qualifications = _gateWay.GetQualifications();

                    List<SelectListItem> _qualList = new List<SelectListItem>();

                    foreach (var _qualification in _qualifications)
                    {
                        _qualList.Add(new SelectListItem { Text = _qualification.QualificationName, Value = _qualification.QualificationCode.ToString() });
                    }
                    ViewData["Qualifications"] = _qualList;
                    _model.Qualifications = _qualList;

                    #endregion

                    return View(_model); 
                }
            }
            catch (Exception ex)
            {

                #region Load Dropdown

                List<Qualification> _qualifications = new List<Qualification>();
                _qualifications = _gateWay.GetQualifications();

                List<SelectListItem> _qualList = new List<SelectListItem>();

                foreach (var _qualification in _qualifications)
                {
                    _qualList.Add(new SelectListItem { Text = _qualification.QualificationName, Value = _qualification.QualificationCode.ToString() });
                }
                ViewData["Qualifications"] = _qualList;
                _model.Qualifications = _qualList;

                #endregion

                return View(_model);
            }
        }

        // GET: Module/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Module/Edit/5
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

        // GET: Module/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Module/Delete/5
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
