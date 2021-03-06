﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class StudentHandler
    {
        public bool NewStudent(Student _student)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@User_Id", _student.User_Id),
                new SqlParameter("@StudentNumber", _student.StudentNumber),
                new SqlParameter("@Name",_student.Name),
                new SqlParameter("@Surname", _student.Surname), 
                new SqlParameter("@QualificationCode", _student.QualificationCode)
            };
            return DataAccess.ExecuteNonQuery("sp_InsertStudent", CommandType.StoredProcedure,
                Params);
        }
        public Student GetStudent(string Id)
        {
            Student _student = null;

            SqlParameter[] Params = { new SqlParameter("@User_Id", Id) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("sp_GetStudents",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    _student = new Student();
                    _student.StudentNumber = row["StudentNumber"].ToString();
                    _student.Name = row["Name"].ToString();
                    _student.Surname = row["Surname"].ToString();
                }
            }
            return _student;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> _studentList = null;

            using (DataTable table = DataAccess.ExecuteSelectCommand("",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    _studentList = new List<Student>();
                    foreach (DataRow row in table.Rows)
                    {
                        Student _student = new Student();
                        _student.User_Id = row["Id"].ToString();
                        _student.Name = row["Name"].ToString();
                        _student.Surname = row["Surname"].ToString();
                        _student.StudentNumber = row["StudentNumber"].ToString();
                        _student.QualificationCode = Convert.ToInt32(row["QualificationCode"]);
                        _studentList.Add(_student);
                    }
                }
            }
            return _studentList;
        }

        public List<Student> GetStudentsForQualification(int QualificationCode)
        {
            List<Student> _studentList = null;
            SqlParameter[] Params = { new SqlParameter("@QualificationCode", QualificationCode) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    _studentList = new List<Student>();
                    foreach (DataRow row in table.Rows)
                    {
                        Student _student = new Student();
                        _student.StudentNumber = row["StudentNumber"].ToString();
                        _student.Name = row["Name"].ToString();
                        _student.Surname = row["Surname"].ToString();
                        _student.User_Id = row["Id"].ToString();
                        _studentList.Add(_student);
                    }
                }
            }
            return _studentList;
        }

        //public List<Student> GetStudentsForModule(string _moduleCode)
        //{
        //    List<Student> _studentList = null;
        //    SqlParameter[] Params = { new SqlParameter("@ModuleCode", _moduleCode) };
        //    using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("", CommandType.StoredProcedure, Params))
        //    {
        //        if (table.Rows.Count > 0)
        //        {
        //            _studentList = new List<Student>();
        //            foreach (DataRow row in table.Rows)
        //            {
        //                Student _student = new Student();
        //                _student.StudentNumber = row["StudentNumber"].ToString();
        //                _student.Name = row["Name"].ToString();
        //                _student.Surname = row["Surname"].ToString();
        //                _student.Id = row["Id"].ToString();
        //                _studentList.Add(_student);
        //            }
        //        }
        //    }
        //    return _studentList;
        //}
    }
}
