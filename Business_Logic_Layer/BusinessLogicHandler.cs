﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight.Data;

namespace Insight.BLogic
{
    public class BusinessLogicHandler
    {
        #region Admin Actions
        public bool InsertStudent(Student _student)
        { StudentHandler myHandler = new StudentHandler(); return myHandler.NewStudent(_student); }
        public bool InsertModule(Module _module)
        { ModuleHandler myHandler = new ModuleHandler(); return myHandler.NewModule(_module); }

        public bool InsertQualification(Qualification _qualification)
        { QualificationHandler myHandler = new QualificationHandler(); return myHandler.NewQualification(_qualification); }

        public bool InsertVenue(Venue _venue)
        { VenueHandler myHandler = new VenueHandler(); return myHandler.NewVenue(_venue); }

        public bool InsertLecture(Lecture _lecture)
        { LectureHandler myHandler = new LectureHandler(); return myHandler.NewLecture(_lecture); }

        public bool InsertLecturer(Lecturer _lecturer)
        { LecturerHandler myHandler = new LecturerHandler(); return myHandler.NewLecturer(_lecturer); }

        public Student GetStudent(string User_Id)
        { StudentHandler myHandler = new StudentHandler(); return myHandler.GetStudent(User_Id); }

        public bool InsertAttendanceRegister(AttendanceRegister _register)
        { AttendanceRegisterHandler myHandler = new AttendanceRegisterHandler(); return myHandler.NewAttendanceRegister(_register); }

        public bool InsertStudentAttendance(StudentAttendance _studentAttendance)
        { StudentAttendanceHandler myHandler =  new StudentAttendanceHandler(); return myHandler.NewStudentAttendance(_studentAttendance);}

        public Lecturer GetLecturer(string User_Id)
        { LecturerHandler myHandler = new LecturerHandler(); return myHandler.GetLecturer(User_Id); }

        public Lecturer GetLecturer_StuffNumber(string StaffNumber)
        { LecturerHandler myHandler = new LecturerHandler(); return myHandler.GetLecturer_StaffNumber(StaffNumber); }

        public Lecture GetLecture(int LUI)
        { LectureHandler myHandler = new LectureHandler(); return myHandler.GetLecture(LUI); }

        public Venue GetVenue(string _venueCode)
        { VenueHandler myHandler = new VenueHandler(); return myHandler.GetVenue(_venueCode); }

        public List<Venue> GetAllVenues()
        { VenueHandler myHandler = new VenueHandler(); return myHandler.GetAllVenues(); }

        public List<Qualification> GetQualifications()
        { QualificationHandler myHandler = new QualificationHandler(); return myHandler.GetAllQualifications(); }
        public List<Lecturer> GetAllLecturers()
        { LecturerHandler myHandler = new LecturerHandler(); return myHandler.GetAllLecturers(); }
        public List<Module> GetModulesForLecturer(string StaffNumber)
        { ModuleHandler myHandler = new ModuleHandler(); return myHandler.GetAllModulesForLecturer(StaffNumber); }
        public List<Lecture> GetLecturesForStaff(string staffNumber)
        { LectureHandler myHandler = new LectureHandler(); return myHandler.GetLectureForStaffMemeber(staffNumber); }
        public List<Module> GetAllModules()
        { ModuleHandler myHandler = new ModuleHandler(); return myHandler.GetAllModules(); }
        #endregion

        #region User Actions
        #endregion
    }
}
