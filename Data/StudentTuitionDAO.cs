using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using FISCA.UDT;

namespace TuitionSystem.Data
{
    public class StudentTuitionDAO
    {
        private static List<StudentTuitionRecord> STRs=new List<StudentTuitionRecord>();
        private static AccessHelper udtHelper = new AccessHelper();

        public static List<StudentTuitionRecord> GetStudentTuitionList()
        {
            STRs = udtHelper.Select<StudentTuitionRecord>();
            return STRs;
        }

        public static List<StudentTuitionRecord> GetStudentTuitionBySSTName(int SchoolYear, String Semester, string TSName)
        {
           // GetStudentTuitionList();

            List<StudentTuitionRecord> tss = new List<StudentTuitionRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<StudentTuitionRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "'AND 收費標準名稱='" + TSName + "'");
            }
            tss = udtHelper.Select<StudentTuitionRecord>(condition);
            //foreach (StudentTuitionRecord tr in STRs)
            //{
            //    if (tr.SchoolYear == SchoolYear && tr.Semester == (Semester == "上學期" ? 1 : 2) && tr.TSName == TSName)
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        public static List<StudentTuitionRecord> GetStudentTuitionByUID(string UID)
        {
            //GetStudentTuitionList();

            List<StudentTuitionRecord> tss = new List<StudentTuitionRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<StudentTuitionRecord>().Compiler("UID='" + UID + "'");
            }
            tss = udtHelper.Select<StudentTuitionRecord>(condition);
            //foreach (StudentTuitionRecord tr in STRs)
            //{
            //    if (tr.UID == UID)
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        public static List<StudentTuitionRecord> GetStudentTuitionBySS(int SchoolYear, String Semester)
        {
           // GetStudentTuitionList();

            List<StudentTuitionRecord> tss = new List<StudentTuitionRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<StudentTuitionRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "'");
            }
            tss = udtHelper.Select<StudentTuitionRecord>(condition);
            //foreach (StudentTuitionRecord tr in STRs)
            //{
            //    if (tr.SchoolYear == SchoolYear && tr.Semester == (Semester == "上學期" ? 1 : 2) )
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        public static List<StudentTuitionRecord> GetStudentTuitionBySST(int SchoolYear, String Semester, string ST)
        {
            //GetStudentTuitionList();

            List<StudentTuitionRecord> tss = new List<StudentTuitionRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<StudentTuitionRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "'AND 新舊生='" + ST + "'");
            }
            tss = udtHelper.Select<StudentTuitionRecord>(condition);
            //foreach (StudentTuitionRecord tr in STRs)
            //{
            //    if (tr.SchoolYear == SchoolYear && tr.Semester == (Semester == "上學期" ? 1 : 2) && tr.StudentType == ST)
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }

        public static List<StudentTuitionRecord> GetStudentTuitionBySSTS(int SchoolYear, String Semester, string ST, string TuitionUID)
        {
            //GetStudentTuitionList();

            List<StudentTuitionRecord> tss = new List<StudentTuitionRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<StudentTuitionRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "'AND 新舊生='" + ST + "' AND 學生識別號='" + TuitionUID + "'");
            }
            tss = udtHelper.Select<StudentTuitionRecord>(condition);
            //foreach (StudentTuitionRecord tr in STRs)
            //{
            //    if (tr.SchoolYear == SchoolYear && tr.Semester == (Semester == "上學期" ? 1 : 2) && tr.StudentType == ST && tr.TuitionUID == TuitionUID)
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        private static object lockKey = new System.Windows.Forms.Form();
        public static List<StudentTuitionRecord> GetStudentTuitionBySSTSL(int SchoolYear, String Semester, string ST, List<string> TuitionUID)
        {
            //GetStudentTuitionList();
            List<StudentTuitionRecord> tss = new List<StudentTuitionRecord>();
            if (TuitionUID.Count <= 0)
                return tss;
            string stringTU="";
            foreach (string UID in TuitionUID)
                if (UID!="")
                    stringTU += "'"+UID + "',";
            stringTU = stringTU.Substring(0, stringTU.Length - 1);
            
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<StudentTuitionRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "'  AND  新舊生='" + ST + "' AND 學生識別號 IN ( " + stringTU + " ) ");
            }
            tss = udtHelper.Select<StudentTuitionRecord>(condition);
             //foreach (StudentTuitionRecord tr in STRs)
             //{
             //    if (tr.SchoolYear == SchoolYear && tr.Semester == (Semester == "上學期" ? 1 : 2) && tr.StudentType == ST && TuitionUID.Contains(tr.TuitionUID))
             //    {
             //        tss.Add(tr);
             //    }
             //}

            return tss;
        }
        public static string Insert(StudentTuitionRecord tsr)
        {
            List<ActiveRecord> StudentTuitions = new List<ActiveRecord>();
            StudentTuitions.Add(tsr);
            List<string> newIDs = udtHelper.InsertValues(StudentTuitions);
            return newIDs[0];
        }

        public static string Update(StudentTuitionRecord tsr)
        {
            List<ActiveRecord> StudentTuitions = new List<ActiveRecord>();
            StudentTuitions.Add(tsr);
            udtHelper.UpdateValues(StudentTuitions);
            return tsr.UID;
        }

        public static void Delete(StudentTuitionRecord tsr)
        {
            List<ActiveRecord> StudentTuitions = new List<ActiveRecord>();
            StudentTuitions.Add(tsr);
            udtHelper.DeletedValues(StudentTuitions);
        }

    }
}
