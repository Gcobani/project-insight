using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class LectureHandler
    {
        public bool NewLecture(Lecture _lecture)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ModuleCode", _lecture.ModuleCode),
                new SqlParameter("@StaffNumber", _lecture.StaffNumber),
                new SqlParameter("@VenueCode",_lecture.VenueCode), 
                new SqlParameter("@TimeSlot", _lecture.TimeSlot)
            };
            return DataAccess.ExecuteNonQuery("sp_InsertLecture", CommandType.StoredProcedure,
                Params);
        }

        public Lecture GetLecture(int LUI)
        {
            Lecture _lecture = null;

            SqlParameter[] Params = { new SqlParameter("@LUI", LUI) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("sp_GetLecture",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    _lecture = new Lecture();
                    _lecture.TimeSlot = row["TimeSlot"].ToString();
                    _lecture.ModuleCode = row["ModuleCode"].ToString();
                    _lecture.StaffNumber = row["StaffNumber"].ToString();
                    _lecture.VenueCode = row["VenueCode"].ToString();
                }
            }
            return _lecture;
        }

        public List<Lecture> GetAllLectures()
        {
            List<Lecture> _lectureList = null;

            using (DataTable table = DataAccess.ExecuteSelectCommand("sp_GetAllLectures",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    _lectureList = new List<Lecture>();
                    foreach (DataRow row in table.Rows)
                    {
                        Lecture _lecture = new Lecture();
                        _lecture.TimeSlot = row["TimeSlot"].ToString();
                        _lecture.ModuleCode = row["ModuleCode"].ToString();
                        _lecture.StaffNumber = row["StaffNumber"].ToString();
                        _lecture.VenueCode = row["VenueCode"].ToString();
                        _lectureList.Add(_lecture);
                    }
                }
            }
            return _lectureList;
        }

        public bool UpdateLecture(Lecture _lecture)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@LUI", _lecture.LUI),
                new SqlParameter("@TimeSlot", _lecture.TimeSlot ),
                new SqlParameter("@ModuleCode", _lecture.ModuleCode),
                new SqlParameter("@StaffNumber", _lecture.StaffNumber),
                new SqlParameter("@VenueCode", _lecture.VenueCode)
            };
            return DataAccess.ExecuteNonQuery("sp_UpdateLecture", CommandType.StoredProcedure,
                Params);
        }

        public List<Lecture> GetLectureForStaffMemeber(string StaffNumber)
        {
            List<Lecture> _lectureList = null;
            SqlParameter[] Params = { new SqlParameter("@StaffNumber", StaffNumber) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("sp_GetLectures4Staff", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    _lectureList = new List<Lecture>();
                    foreach (DataRow row in table.Rows)
                    {
                        Lecture _lecture = new Lecture();
                        _lecture.LUI = Convert.ToInt32(row["LUI"]);
                        _lecture.TimeSlot = row["TimeSlot"].ToString();
                        _lecture.ModuleCode = row["ModuleCode"].ToString();
                        _lecture.StaffNumber = row["StaffNumber"].ToString();
                        _lecture.VenueCode = row["VenueCode"].ToString();
                        _lectureList.Add(_lecture);
                    }
                }
            }
            return _lectureList;
        }
    }
}
