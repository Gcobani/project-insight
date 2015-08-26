using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class LecturerHandler
    {
        public Lecturer GetLecturer(string User_Id)
        {
            Lecturer _lecturer = null;

            SqlParameter[] Params = { new SqlParameter("@User_Id", User_Id) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("sp_GetLecturer",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                { 
                    DataRow row = table.Rows[0];
                    _lecturer = new Lecturer();
                    _lecturer.Name = row["Name"].ToString();
                    _lecturer.Surname = row["Surname"].ToString();
                    _lecturer.User_Id = row["User_Id"].ToString();
                    _lecturer.StaffNumber = row["StaffNumber"].ToString();
                }
            }
            return _lecturer;
        }

        public List<Lecturer> GetAllLecturers()
        {
            List<Lecturer> _staffList = null;

            using (DataTable table = DataAccess.ExecuteSelectCommand("sp_GetAllLecturers",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    _staffList = new List<Lecturer>();
                    foreach (DataRow row in table.Rows)
                    {
                        Lecturer _staff = new Lecturer();
                        _staff.Name = row["Name"].ToString();
                        _staff.StaffNumber = row["StaffNumber"].ToString();
                        _staff.Surname = row["Surname"].ToString();
                        _staff.User_Id = row["User_Id"].ToString();
                        _staffList.Add(_staff);
                    }
                }
            }
            return _staffList;
        }

        public bool NewLecturer(Lecturer _lecturer)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Name", _lecturer.Name),
                new SqlParameter("@StaffNumber", _lecturer.StaffNumber),
                new SqlParameter("@Surname",_lecturer.Surname), 
                new SqlParameter("@User_Id", _lecturer.User_Id),
            };
            return DataAccess.ExecuteNonQuery("sp_InsertLecturer", CommandType.StoredProcedure,
                Params);
        }

    }
}
