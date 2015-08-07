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
        public bool InsertModule(Module _module)
        { ModuleHandler myHandler = new ModuleHandler(); return myHandler.NewModule(_module); }

        public bool InsertQualification(Qualification _qualification)
        { QualificationHandler myHandler = new QualificationHandler(); return myHandler.NewQualification(_qualification); }

        public bool InsertVenue(Venue _venue)
        { VenueHandler myHandler = new VenueHandler(); return myHandler.NewVenue(_venue); }

        public bool InsertLecture(Lecture _lecture)
        { LectureHandler myHandler = new LectureHandler(); return myHandler.NewLecture(_lecture); }

        public bool InsertAttendanceRegister(AttendanceRegister _register)
        { AttendanceRegisterHandler myHandler = new AttendanceRegisterHandler(); return myHandler.NewAttendanceRegister(_register); }

        public bool InsertStudentAttendance(StudentAttendance _studentAttendance)
        { StudentAttendanceHandler myHandler =  new StudentAttendanceHandler(); return myHandler.NewStudentAttendance(_studentAttendance);}


        #endregion

        #region User Actions
        #endregion
    }
}
