﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Insight.BLogic;
using Insight.Data;
using Insight.Models;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Insight.Controllers
{
    public class AttendanceController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TakeAttendance()
        {

            #region Get Identity
            BusinessLogicHandler _gateWay = new BusinessLogicHandler();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            ApplicationUserManager UserManager = new ApplicationUserManager(myStore);
            var _user = UserManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            Lecturer _staff = new Lecturer();
            _staff = _gateWay.GetLecturer(_user.Id.ToString());
            #endregion

            #region Get The Modules
            
            AttendanceViewModel _model = new AttendanceViewModel();
            List<Module> _modList = new List<Module>();
            List<SelectListItem> _selectList = new List<SelectListItem>();
            _modList = _gateWay.GetModulesForLecturer(_staff.StaffNumber);

            foreach (var _mod in _modList)
            {
                _selectList.Add(new SelectListItem { Text = _mod.ModuleName, Value = _mod.ModuleCode });
            }
            #endregion

            ViewData["Modules"] = _selectList;
            _model.Modules = _selectList;

            return View(_model);
        }
    }
}
