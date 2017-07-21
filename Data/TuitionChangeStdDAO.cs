using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using FISCA.UDT;

namespace TuitionSystem.Data
{
    public class TuitionChangeStdDAO
    {
        private static List<TuitionChangeStdRecord> TCSRs=new List<TuitionChangeStdRecord>();
        private static AccessHelper udtHelper = new AccessHelper();

        public static List<TuitionChangeStdRecord> GetTuitionChangeStdList()
        {
            TCSRs = udtHelper.Select<TuitionChangeStdRecord>();
            return TCSRs;
        }
        private static object lockKey = new System.Windows.Forms.Form();
        public static List<TuitionChangeStdRecord> GetTuitionChangeStdBySS(int SchoolYear, String Semester)
        {
            //GetTuitionChangeStdList();

            List<TuitionChangeStdRecord> tss = new List<TuitionChangeStdRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<TuitionChangeStdRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "'");
            }
            tss = udtHelper.Select<TuitionChangeStdRecord>(condition);
            //foreach (TuitionChangeStdRecord tr in TCSRs)
            //{
            //    if (tr.SchoolYear  == SchoolYear && tr.Semester==(Semester=="上學期"? 1:2))
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }

        public static List<TuitionChangeStdRecord> GetTuitionChangeStdBySST(int SchoolYear, String Semester,string TCSName)
        {
            //GetTuitionChangeStdList();

            List<TuitionChangeStdRecord> tss = new List<TuitionChangeStdRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<TuitionChangeStdRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "' AND 異動標準名稱='" + TCSName + "'");
            }
            tss = udtHelper.Select<TuitionChangeStdRecord>(condition);
            //foreach (TuitionChangeStdRecord tr in TCSRs)
            //{
            //    if (tr.SchoolYear == SchoolYear && tr.Semester == (Semester == "上學期" ? 1 : 2) && tr.TCSName==TCSName)
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        public static string Insert(TuitionChangeStdRecord tsr)
        {
            List<ActiveRecord> TuitionChangeStds = new List<ActiveRecord>();
            TuitionChangeStds.Add(tsr);
            List<string> newIDs = udtHelper.InsertValues(TuitionChangeStds);
            return newIDs[0];
        }

        public static string Update(TuitionChangeStdRecord tsr)
        {
            List<ActiveRecord> TuitionChangeStds = new List<ActiveRecord>();
            TuitionChangeStds.Add(tsr);
            udtHelper.UpdateValues(TuitionChangeStds);
            return tsr.UID;
        }

        public static void Delete(TuitionChangeStdRecord tsr)
        {
            List<ActiveRecord> TuitionChangeStds = new List<ActiveRecord>();
            TuitionChangeStds.Add(tsr);
            udtHelper.DeletedValues(TuitionChangeStds);
        }

    }
}
