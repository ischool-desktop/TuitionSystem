using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using FISCA.UDT;

namespace TuitionSystem.Data
{
    public class TuitionDetailDAO
    {
        private static List<TuitionDetailRecord> TDRs=new List<TuitionDetailRecord>();
        private static AccessHelper udtHelper = new AccessHelper();

        public static List<TuitionDetailRecord> GetTuitionDetailList()
        {
            TDRs = udtHelper.Select<TuitionDetailRecord>();
            return TDRs;
        }
        private static object lockKey = new System.Windows.Forms.Form();
        public static List<TuitionDetailRecord> GetTuitionDetailByUID(string stUID)
        {
            GetTuitionDetailList();

            List<TuitionDetailRecord> tss = new List<TuitionDetailRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<TuitionDetailRecord>().Compiler("收費表='" + stUID + "'");
            }
            tss = udtHelper.Select<TuitionDetailRecord>(condition);
            //foreach (TuitionDetailRecord tr in TDRs)
            //{
            //    if (tr.STUID==stUID)
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        public static List<TuitionDetailRecord> GetTuitionDetailBySST(int SchoolYear,string Semester,string TCSName)
        {
            //GetTuitionDetailList();

            List<TuitionDetailRecord> tss = new List<TuitionDetailRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<TuitionDetailRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "' AND 異動標準名稱='" + TCSName + "'");
            }
            tss = udtHelper.Select<TuitionDetailRecord>(condition);
            
            //foreach (TuitionDetailRecord tr in TDRs)
            //{
            //    if (tr.TCSName == TCSName && tr.SchoolYear == SchoolYear && tr.Semester == (Semester == "上學期" ? 1 : 2))
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        public static List<TuitionDetailRecord> GetTuitionDetailByIDs(List<string> stIDs)
        {
            //GetTuitionDetailList();
            
            List<TuitionDetailRecord> tss = new List<TuitionDetailRecord>();
            if (stIDs.Count <= 0)
                return tss;
            string IDkeys="";
            foreach (string id in stIDs)
                IDkeys +="'"+ id + "',";
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<TuitionDetailRecord>().Compiler("收費表 IN ("+IDkeys.Substring(0,IDkeys.Length-1)+")");
            }
            tss = udtHelper.Select<TuitionDetailRecord>(condition);
            //foreach (TuitionDetailRecord tr in TDRs)
            //{
            //    if (stIDs.Contains(tr.STUID))
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        public static List<TuitionDetailRecord> GetTuitionDetailBySS(int SchoolYear, string Semester)
        {           
            List<TuitionDetailRecord> tss = new List<TuitionDetailRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<TuitionDetailRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "'");
            }
            tss = udtHelper.Select<TuitionDetailRecord>(condition);
            //foreach (TuitionDetailRecord tr in TDRs)
            //{
            //    if (tr.SchoolYear == SchoolYear && tr.Semester == (Semester == "上學期" ? 1 : 2))
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        public static List<TuitionDetailRecord> GetTuitionDetailByUT(string stUID,string TCSName)
        {
            //GetTuitionDetailList();

            List<TuitionDetailRecord> tss = new List<TuitionDetailRecord>();
            FISCA.UDT.Condition.ICondition condition;
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<TuitionDetailRecord>().Compiler("收費表='" + stUID + "' AND 異動標準名稱='" + TCSName + "'");
            }
            tss = udtHelper.Select<TuitionDetailRecord>(condition);
            //foreach (TuitionDetailRecord tr in TDRs)
            //{
            //    if (tr.STUID == stUID && tr.TCSName==TCSName)
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        public static string Insert(TuitionDetailRecord tsr)
        {
            List<ActiveRecord> TuitionDetails = new List<ActiveRecord>();
            TuitionDetails.Add(tsr);
            List<string> newIDs = udtHelper.InsertValues(TuitionDetails);
            return newIDs[0];
        }

        public static string Update(TuitionDetailRecord tsr)
        {
            List<ActiveRecord> TuitionDetails = new List<ActiveRecord>();
            TuitionDetails.Add(tsr);
            udtHelper.UpdateValues(TuitionDetails);
            return tsr.UID;
        }

        public static void Delete(TuitionDetailRecord tsr)
        {
            List<ActiveRecord> TuitionDetails = new List<ActiveRecord>();
            TuitionDetails.Add(tsr);
            udtHelper.DeletedValues(TuitionDetails);
        }

    }
}
