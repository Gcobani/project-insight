using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class StudentAttendanceHandler
    {

        public bool NewStudentAttendance(StudentAttendance _registerAttendance)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ModuleCode", _registerAttendance.ModuleCode),
                new SqlParameter("@Present", _registerAttendance.Present),
                new SqlParameter("@VenueCode",_registerAttendance.VenueCode), 
                new SqlParameter("@DateTime", _registerAttendance.DateTime),
                new SqlParameter("@StudentNumber", _registerAttendance.StudentNumber)
            };
            return DataAccess.ExecuteNonQuery("sp_InsertStudentAttendance", CommandType.StoredProcedure,
                Params);
        }

        public StudentAttendance GetStudentAttendance(int USAI)
        {
            StudentAttendance _registerAttendance = null;

            SqlParameter[] Params = { new SqlParameter("@USAI", USAI) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("sp_GetStudentAttendance",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    _registerAttendance = new StudentAttendance();
                    _registerAttendance.DateTime = Convert.ToDateTime(row["DateTime"]);
                    _registerAttendance.ModuleCode = row["ModuleCode"].ToString();
                    _registerAttendance.StudentNumber = row["StudentNumber"].ToString();
                    _registerAttendance.Present = Convert.ToBoolean(row["Present"]);
                    _registerAttendance.VenueCode = row["VenueCode"].ToString();
                }
            }
            return _registerAttendance;
        }

        public List<StudentAttendance> GetAllStudentAttendance()
        {
            List<StudentAttendance> _registerAttendanceList = null;

            using (DataTable table = DataAccess.ExecuteSelectCommand("sp_GetAllStudentAttendance",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    _registerAttendanceList = new List<StudentAttendance>();
                    foreach (DataRow row in table.Rows)
                    {
                        StudentAttendance _attendance = new StudentAttendance();
                        _attendance.DateTime = Convert.ToDateTime(row["DateTime"]);
                        _attendance.ModuleCode = row["ModuleCode"].ToString();
                        _attendance.Present = Convert.ToBoolean(row["Present"]);
                        _attendance.StudentNumber = row["StudentNumber"].ToString();
                        _attendance.VenueCode = row["VenueCode"].ToString();
                        _registerAttendanceList.Add(_attendance);
                    }
                }
            }
            return _registerAttendanceList;
        }

    }
}
