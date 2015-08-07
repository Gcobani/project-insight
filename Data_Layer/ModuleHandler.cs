using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class ModuleHandler
    {
        public bool NewModule(Module _module)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ModuleCode", _module.ModuleCode),
                new SqlParameter("@ModuleName", _module.ModuleName),
                new SqlParameter("@NumberOfScheduledClasses",_module.NumberOfScheduledClasses),
                new SqlParameter("@QualificationCode", _module.QualificationCode)
            };
            return DataAccess.ExecuteNonQuery("", CommandType.StoredProcedure,
                Params);
        }

        public Module GetModule(string ModuleCode)
        {
            Module _module = null;

            SqlParameter[] Params = { new SqlParameter("@ModuleCode", ModuleCode) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    _module = new Module();
                    _module.NumberOfScheduledClasses = Convert.ToInt32(row["NumberOfScheduledClasses"]);
                    _module.ModuleCode = row["ModuleCode"].ToString();
                    _module.ModuleName = row["ModuleName"].ToString();
                    _module.QualificationCode = Convert.ToInt32(row["QualificationCode"]);
                }
            }
            return _module;
        }

        public List<Module> GetAllModules()
        {
            List<Module> _moduleList = null;

            using (DataTable table = DataAccess.ExecuteSelectCommand("",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    _moduleList = new List<Module>();
                    foreach (DataRow row in table.Rows)
                    {
                        Module _module = new Module();
                        _module.NumberOfScheduledClasses = Convert.ToInt32(row["NumberofScheduledClasses"]);
                        _module.ModuleCode = row["ModuleCode"].ToString();
                        _module.ModuleName = row["ModuleName"].ToString();
                        _module.QualificationCode = Convert.ToInt32(row["QualificationCode"]);
                        _moduleList.Add(_module);
                    }
                }
            }
            return _moduleList;
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
