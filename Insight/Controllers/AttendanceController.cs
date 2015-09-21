﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Insight.BLogic;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.Util;
using Emgu.CV.Structure;
using Insight.Data;
using Insight.Models;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Emgu.CV;
using System.IO;
using Ionic.Zip;
using System.Drawing;
using Emgu.CV.Face;
using Hangfire;

namespace Insight.Controllers
{
    public class AttendanceController : Controller
    {
        public async Task<ActionResult> Index()
        {
            #region Get Identity
            BusinessLogicHandler _gateWay = new BusinessLogicHandler();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            ApplicationUserManager UserManager = new ApplicationUserManager(myStore);
            var _user = await UserManager.FindByNameAsync(User.Identity.Name);
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

            #region Get The Venues

            List<Venue> _venueList = new List<Venue>();
            List<SelectListItem> _vlist = new List<SelectListItem>();
            _venueList = _gateWay.GetAllVenues();

            foreach (Venue _venue in _venueList)
            {
                _vlist.Add(new SelectListItem { Text = _venue.VenueName, Value = _venue.VenueCode });
            }

            #endregion

            #region Getting the lectures
            List<Lecture> _lectureList = new List<Lecture>();
            List<SelectListItem> _yetAL = new List<SelectListItem>();
            _lectureList = _gateWay.GetLecturesForStaff(_staff.StaffNumber);
            _yetAL.Add(new SelectListItem { Text = "Select Lecture", Value = "0", Selected = true });
            foreach(var _item in _lectureList)
            {
                _yetAL.Add(new SelectListItem { Text = _item.ModuleCode + " " + _item.TimeSlot, Value = _item.LUI.ToString() });
            }
            #endregion

            ViewData["Lecture"] = _yetAL;
            //ViewData["Modules"] = _selectList;
            //ViewData["Venues"] = _vlist;
            //_model.Venues = _vlist;
            _model.Lectures = _yetAL;
            //_model.Modules = _selectList;
            return View(_model);
        }
        public async Task<ActionResult> Schedule()
        {
            #region Get Identity
            BusinessLogicHandler _gateWay = new BusinessLogicHandler();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            ApplicationUserManager UserManager = new ApplicationUserManager(myStore);
            var _user = await UserManager.FindByNameAsync(User.Identity.Name);
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

            #region Get The Venues

            List<Venue> _venueList = new List<Venue>();
            List<SelectListItem> _vlist = new List<SelectListItem>();
            _venueList = _gateWay.GetAllVenues();

            foreach (Venue _venue in _venueList)
            {
                _vlist.Add(new SelectListItem { Text = _venue.VenueName, Value = _venue.VenueCode });
            }

            #endregion

            #region Getting the lectures
            List<Lecture> _lectureList = new List<Lecture>();
            List<SelectListItem> _yetAL = new List<SelectListItem>();
            _lectureList = _gateWay.GetLecturesForStaff(_staff.StaffNumber);
            _yetAL.Add(new SelectListItem { Text = "Select Lecture", Value = "0", Selected = true });
            foreach (var _item in _lectureList)
            {
                _yetAL.Add(new SelectListItem { Text = _item.ModuleCode + " " + _item.TimeSlot, Value = _item.LUI.ToString() });
            }
            #endregion

            ViewData["Lecture"] = _yetAL;
            //ViewData["Modules"] = _selectList;
            //ViewData["Venues"] = _vlist;
            //_model.Venues = _vlist;
            _model.Lectures = _yetAL;
            //_model.Modules = _selectList;
            return View(_model); 
        }
        [HttpPost]
        public ActionResult Schedule(HttpPostedFileBase zip, HttpPostedFileBase img, FormCollection collector)
        {
            #region Declaring Varibles

            string path = Server.MapPath("~/Uploads/");
            string haarcascades = HttpContext.Server.MapPath("~/haarcascades/haarcascade_frontalface_default.xml");
            string theePath = "";
            List<string> User_Id;
            string[] dateSlice = collector.GetValue("dateHoldhidden").AttemptedValue.Split(' ');
            string[] timeSlice = collector.GetValue("timeHoldhidden").AttemptedValue.Split(' ');
            string rawDate = dateSlice[3] + "-" + dateSlice[1] + "-" + dateSlice[2] + " "+timeSlice[0]+" "+timeSlice[1];
            DateTime date = new DateTime();
            date = DateTime.Parse(rawDate);
            DateTimeOffset ofset = new DateTimeOffset(date, new TimeSpan(+1, 0, 0));
            //date = DateTime.ParseExact(rawDate, "yyyy-MMM-dd HH:mm tt", null);
            #endregion

            #region Checking or Creating the directory

            string newPath = Server.MapPath("~/Uploads/Attendance/");
            if (Directory.Exists(newPath))
            {
                theePath = newPath + collector.GetValue("Lecture").AttemptedValue + DateTime.Today.ToString("ddMMMMyyyy");
                if (!Directory.Exists(theePath))
                    Directory.CreateDirectory(theePath);
            }
            else
            { Directory.CreateDirectory(newPath); }

            #endregion

            if (img != null && zip == null)
            {
                User_Id = new List<string>();
                Bitmap _Img = new Bitmap(img.InputStream);

                #region processing the img

                CoreSysFunction _coreFunction = new CoreSysFunction();

                BackgroundJob.Schedule(() => User_Id.AddRange(_coreFunction.DetectAndRecognize(_Img, path, haarcascades)), ofset);

                RecurringJob.AddOrUpdate(() => projectX(path), Cron.Weekly(DayOfWeek.Monday, 21, 08));
                #endregion

                #region object to view page

                Bridge _bridge = new Bridge();
                _bridge.User_Id = new List<string>();
                _bridge.User_Id = User_Id;
                _bridge.dateSlice = collector.GetValue("dateHoldhidden").AttemptedValue.Split(' ');
                _bridge.Lecture_Id = Convert.ToInt32(collector.GetValue("Lecture").AttemptedValue);
                Session["Data"] = _bridge;

                #endregion

                return RedirectToActionPermanent("ViewAttendees");
            }
            else if (zip != null && img != null)
            {
                User_Id = new List<string>();
                Bitmap _Img = new Bitmap(img.InputStream);

                #region processing the img

                CoreSysFunction _coreFunction = new CoreSysFunction();
                User_Id.AddRange(_coreFunction.DetectAndRecognize(_Img, path, haarcascades));

                #endregion

                #region Processing zip
                Stream zipStream = zip.InputStream;
                using (var _img = ZipFile.Read(zipStream))
                {
                    foreach (var file in _img.Entries)
                    {
                        file.Extract(theePath, ExtractExistingFileAction.OverwriteSilently);
                        Image bmp = Image.FromFile(theePath + "/" + file.FileName);
                        Bitmap mbmp = new Bitmap(bmp);
                        User_Id.AddRange(_coreFunction.DetectAndRecognize(mbmp, path, haarcascades));
                    }
                }
                #endregion

                #region object to view

                Bridge _bridge = new Bridge();
                _bridge.User_Id = new List<string>();
                _bridge.User_Id = User_Id.Distinct();
                _bridge.dateSlice = collector.GetValue("dateHoldhidden").AttemptedValue.Split(' ');
                _bridge.Lecture_Id = Convert.ToInt32(collector.GetValue("Lecture").AttemptedValue);
                Session["Data"] = _bridge;

                #endregion

                return RedirectToActionPermanent("ViewAttendees");
            }
            else if (zip != null)
            {
                return RedirectToActionPermanent("ViewAttendees");
            }
            else
            { return RedirectToActionPermanent("Index"); }
        }
        public ActionResult Details(string selectedValue)
        {
            AttendanceViewModel _model = new AttendanceViewModel();
            BusinessLogicHandler _gateWay = new BusinessLogicHandler();
            _model.Lecture = new Lecture();
            _model.Lecture = _gateWay.GetLecture(int.Parse(selectedValue));
            object[] response = {_model.Lecture.ModuleCode, _model.Lecture.VenueCode, _model.Lecture.TimeSlot };
            return Json(response);
        }
        public ActionResult TakeAttendance(HttpPostedFileBase zip, HttpPostedFileBase img, FormCollection collector)
        {
            #region Declaring Varibles

            string path = Server.MapPath("~/Uploads/");
            string haarcascades = HttpContext.Server.MapPath("~/haarcascades/haarcascade_frontalface_default.xml");
            string theePath = "";
            BusinessLogicHandler _gateWay;
            Lecture _mod;
            List<string> User_Id;

            #endregion

            #region getting the modulecode

            _gateWay = new BusinessLogicHandler();
            _mod = new Lecture();
            _mod = _gateWay.GetLecture(Convert.ToInt32(collector.GetValue("Lecture").AttemptedValue));

            #endregion

            #region Checking or Creating the directory

            string newPath = Server.MapPath("~/Uploads/Attendance/");
            if (Directory.Exists(newPath))
            {
                if(Directory.Exists(newPath+_mod.ModuleCode))
                {
                    theePath = newPath + _mod.ModuleCode.Trim() + "/" + _mod.VenueCode.Trim() + "/" + DateTime.Today.ToString("ddMMMMyyyy");
                    if (!Directory.Exists(theePath))
                        Directory.CreateDirectory(theePath);
                }
                else
                {
                    Directory.CreateDirectory(newPath + _mod.ModuleCode);
                    theePath = newPath + _mod.ModuleCode.Trim() + "/" + _mod.VenueCode.Trim() + "/" + DateTime.Today.ToString("ddMMMMyyyy");
                    if (!Directory.Exists(theePath))
                        Directory.CreateDirectory(theePath);
                }
            }
            else
            { Directory.CreateDirectory(newPath); }

            #endregion

            if (img != null && zip == null)
            {
                User_Id = new List<string>();
                Bitmap _Img = new Bitmap(img.InputStream);

                #region processing the img

                CoreSysFunction _coreFunction = new CoreSysFunction();
                User_Id.AddRange(_coreFunction.DetectAndRecognize(_Img, path, haarcascades));
                _coreFunction.IdentifyAndMark(haarcascades, Server.MapPath("~/Uploads/Attendance/" + _mod.ModuleCode.Trim() + "/" + _mod.VenueCode.Trim() + "/" + DateTime.Today.ToString("ddMMMMyyyy")+"/"), _mod.TimeSlot.TrimEnd(), _Img);
                #endregion

                #region object to view page

                Bridge _bridge = new Bridge();
                _bridge.User_Id = new List<string>();
                _bridge.User_Id = User_Id;
                _bridge.dateSlice = collector.GetValue("dateHoldhidden").AttemptedValue.Split(' ');
                _bridge.Lecture_Id = Convert.ToInt32(collector.GetValue("Lecture").AttemptedValue);
                Session["Data"] = _bridge;

                #endregion

                return RedirectToActionPermanent("ViewAttendees");
            }
            else if (zip != null && img != null)
            {
                User_Id = new List<string>();
                Bitmap _Img = new Bitmap(img.InputStream);

                #region processing the img

                CoreSysFunction _coreFunction = new CoreSysFunction();
                User_Id.AddRange(_coreFunction.DetectAndRecognize(_Img, path, haarcascades));

                #endregion

                #region Processing zip
                Stream zipStream = zip.InputStream;
                using (var _img = ZipFile.Read(zipStream))
                {
                    foreach(var file in _img.Entries)
                    {
                        file.Extract(theePath, ExtractExistingFileAction.OverwriteSilently);
                        Image bmp = Image.FromFile(theePath+"/"+file.FileName);
                        Bitmap mbmp = new Bitmap(bmp);
                        User_Id.AddRange(_coreFunction.DetectAndRecognize(mbmp, path, haarcascades));
                        _coreFunction.IdentifyAndMark(haarcascades, Server.MapPath(theePath), _mod.VenueCode, mbmp);
                    }
                }
                #endregion

                #region object to view

                Bridge _bridge = new Bridge();
                _bridge.User_Id = new List<string>();
                _bridge.User_Id = User_Id.Distinct();
                _bridge.dateSlice = collector.GetValue("dateHoldhidden").AttemptedValue.Split(' ');
                _bridge.Lecture_Id = Convert.ToInt32(collector.GetValue("Lecture").AttemptedValue);
                Session["Data"] = _bridge;

                #endregion

                return RedirectToActionPermanent("ViewAttendees");
            }
            else if(zip != null)
            {
                return RedirectToActionPermanent("ViewAttendees");
            }
            else
            { return RedirectToActionPermanent("Index"); }
        }
        public ActionResult ViewAttendees()
        {
            #region Declaring the variables

            BusinessLogicHandler _gateWay = new BusinessLogicHandler();
            List<Student> _studList = new List<Student>();
            Student _student = new Student();
            Lecturer _lecturer = new Lecturer();
            Lecture _lecture = new Lecture();
            CloserViewModel _closer = new CloserViewModel();

            #endregion

            Bridge _bridge = (Bridge)Session["Data"];
            //_lecture = _gateWay.GetLecture(_bridge.Lecture_Id);

            #region Getting The students

            foreach (var item in _bridge.User_Id)
            {
                string[] exString = item.Split('.');
                _student = _gateWay.GetStudent(exString[0]);
                _student.User_Id = item.ToString();
                _studList.Add(_student);
            }
            _closer.Students = _studList;

            #endregion

            #region Getting the lecture details

            _lecture = _gateWay.GetLecture(_bridge.Lecture_Id);
            _closer.Lecture = _lecture;

            #endregion

            #region Setting the date

            _closer.Date = "";
            for (int x = 0; x < 4; x++)
            {
                _closer.Date += _bridge.dateSlice[x]+" ";
            }

            #endregion

            #region Getting Lecturer details

            _lecturer = _gateWay.GetLecturer_StuffNumber(_lecture.StaffNumber);
            _closer.Lecturer = new Lecturer();
            _closer.Lecturer = _lecturer;
            #endregion

            return View(_closer);
        }
        public void projectX(string path)
        {
            BackgroundJobClient client = new BackgroundJobClient();
            RecurringJob.AddOrUpdate(() => zues(path), Cron.Minutely);
            BackgroundJobServer serv = new BackgroundJobServer();
        }
        public void zues(string path)
        {
            BackgroundJobClient client = new BackgroundJobClient();
            DateTime tme = new DateTime(2015, 09, 15, 21, 10, 00);
            while(DateTime.Now > tme)
            {
                Directory.CreateDirectory(path + DateTime.Now.TimeOfDay.Minutes.ToString());
            }
            return;
        }
    }
}
