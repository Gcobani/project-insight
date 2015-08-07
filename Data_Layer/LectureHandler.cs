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
                new SqlParameter("@TimeOfDay", _lecture.TimeOfDay)
            };
            return DataAccess.ExecuteNonQuery("", CommandType.StoredProcedure,
                Params);
        }

        public Lecture GetLecture(int LUI)
        {
            Lecture _lecture = null;

            SqlParameter[] Params = { new SqlParameter("@LUI", LUI) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    _lecture = new Lecture();
                    _lecture.TimeOfDay = Convert.ToDateTime(row["TimeOfDay"]);
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

            using (DataTable table = DataAccess.ExecuteSelectCommand("",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    _lectureList = new List<Lecture>();
                    foreach (DataRow row in table.Rows)
                    {
                        Lecture _lecture = new Lecture();
                        _lecture.TimeOfDay = Convert.ToDateTime(row["TimeOfDay"]);
                        _lecture.ModuleCode = row["ModuleCode"].ToString();
                        _lecture.StaffNumber = row["StaffNumber"].ToString();
                        _lecture.VenueCode = row["VenueCode"].ToString();
                        _lectureList.Add(_lecture);
                    }
                }
            }
            return _lectureList;
        }

        public List<Lecture> GetRangeOfLectures(DateTime startDate, DateTime endDate)
        {
            List<Lecture> _lectureList = null;
            SqlParameter[] Params = { new SqlParameter("@startDate", startDate), new SqlParameter("@endDate", endDate) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    _lectureList = new List<Lecture>();
                    foreach (DataRow row in table.Rows)
                    {
                        Lecture _lecture = new Lecture();
                        _lecture.TimeOfDay = Convert.ToDateTime(row["TimeOfday"]);
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
