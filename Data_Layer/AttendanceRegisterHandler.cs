using Insight.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class AttendanceRegisterHandler
    {
        public bool NewAttendanceRegister(AttendanceRegister _register )
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ModuleCode", _register.ModuleCode),
                new SqlParameter("@StaffNumber", _register.StaffNumber),
                new SqlParameter("@VenueCode",_register.VenueCode), 
                new SqlParameter("@DateTime", _register.DateTime)
            };
            return DataAccess.ExecuteNonQuery("sp_InsertAttendanceRegister", CommandType.StoredProcedure,
                Params);
        }

        public AttendanceRegister GetAttendanceRegister(int AttendanceRegNumber)
        {
            AttendanceRegister reg = null;

            SqlParameter[] Params = { new SqlParameter("@AttendanceRegNumber", AttendanceRegNumber) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("sp_GetAttendanceRegister",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    reg = new AttendanceRegister();
                    reg.DateTime = Convert.ToDateTime(row["DateTime"]);
                    reg.ModuleCode = row["ModuleCode"].ToString();
                    reg.StaffNumber = row["StaffNumber"].ToString();
                    reg.VenueCode = row["VenueCode"].ToString();
                }
            }
            return reg;
        }

        public List<AttendanceRegister> GetAllAttendanceRegisters()
        {
            List<AttendanceRegister> _regList = null;

            using (DataTable table = DataAccess.ExecuteSelectCommand("sp_GetAllAttendanceRegisters",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    _regList = new List<AttendanceRegister>();
                    foreach (DataRow row in table.Rows)
                    {
                        AttendanceRegister _reg = new AttendanceRegister();
                        _reg.DateTime = Convert.ToDateTime(row["DateTime"]);
                        _reg.ModuleCode = row["ModuleCode"].ToString();
                        _reg.StaffNumber = row["StaffNumber"].ToString();
                        _reg.VenueCode = row["VenueCode"].ToString();
                        _regList.Add(_reg);
                    }
                }
            }
            return _regList;
        }

        public bool UpdateAttendanceRegister(AttendanceRegister _register)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@AttendanceRegNumber", _register.AttendanceRegNumber),
                new SqlParameter("@DateTime", _register.DateTime ),
                new SqlParameter("@ModuleCode", _register.ModuleCode),
                new SqlParameter("@StaffNumber", _register.StaffNumber),
                new SqlParameter("@VenueCode", _register.VenueCode)
            };
            return DataAccess.ExecuteNonQuery("sp_UpdateAttendanceRegister", CommandType.StoredProcedure,
                Params);
        }

        public List<AttendanceRegister> GetRangeOfAttendanceRegisters(DateTime startDate, DateTime endDate)
        {
            List<AttendanceRegister> _regList = null;
            SqlParameter[] Params = { new SqlParameter("@startDate", startDate), new SqlParameter("@endDate", endDate) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    _regList = new List<AttendanceRegister>();
                    foreach (DataRow row in table.Rows)
                    {
                        AttendanceRegister _reg = new AttendanceRegister();
                        _reg.DateTime = Convert.ToDateTime(row["DateTime"]);
                        _reg.ModuleCode = row["ModuleCode"].ToString();
                        _reg.StaffNumber = row["StaffNumber"].ToString();
                        _reg.VenueCode = row["VenueCode"].ToString();
                        _regList.Add(_reg);
                    }
                }
            }
            return _regList;
        }
    }
}
