using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class QualificationHandler
    {
        public bool NewQualification(Qualification _qualification)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@QualificationCode", _qualification.QualificationCode),
                new SqlParameter("@QualificationName", _qualification.QualificationName),
            };
            return DataAccess.ExecuteNonQuery("", CommandType.StoredProcedure,
                Params);
        }

        public Qualification GetQualification(int _qualificationCode)
        {
            Qualification _qualification = null;

            SqlParameter[] Params = { new SqlParameter("@QualificationCode", _qualificationCode) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    _qualification = new Qualification();
                    _qualification.QualificationName = row["QualificationName"].ToString();
                    _qualification.QualificationCode = Convert.ToInt32(row["QualificationCode"]);
                }
            }
            return _qualification;
        }

        public List<Qualification> GetAllQualifications()
        {
            List<Qualification> _qualificationList = null;

            using (DataTable table = DataAccess.ExecuteSelectCommand("",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    _qualificationList = new List<Qualification>();
                    foreach (DataRow row in table.Rows)
                    {
                        Qualification _qualification = new Qualification();
                        _qualification.QualificationName = row["QualificationName"].ToString();
                        _qualification.QualificationCode = Convert.ToInt32(row["QualificationCode"]);
                        _qualificationList.Add(_qualification);
                    }
                }
            }
            return _qualificationList;
        }

        public List<Module> GetModuleForQualification(int QualificationCode)
        {
            List<Module> _moduleList = null;
            SqlParameter[] Params = { new SqlParameter("@QualificationCode", QualificationCode) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    _moduleList = new List<Module>();
                    foreach (DataRow row in table.Rows)
                    {
                        Module _module = new Module();
                        _module.NumberOfScheduledClasses = Convert.ToInt32(row["NumberOfScheduledClasses"]);
                        _module.ModuleCode = row["ModuleCode"].ToString();
                        _module.ModuleName = row["ModuleName"].ToString();
                        _moduleList.Add(_module);
                    }
                }
            }
            return _moduleList;
        }
    }
}
