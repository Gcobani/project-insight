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
                    _lecturer.Name = row["Name"].ToString();
                    _lecturer.Surname = row["Surname"].ToString();
                    _lecturer.User_Id = row["User_Id"].ToString();
                    _lecturer.StaffNumber = row["StaffNumber"].ToString();
                }
            }
            return _lecturer;
        }

    }
}
