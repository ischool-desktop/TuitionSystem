using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using FISCA.UDT;

namespace TuitionSystem.Data
{
    public class TuitionStandardDAO
    {
        private static List<TuitionStandardRecord> TSRs=new List<TuitionStandardRecord>();
        private static AccessHelper udtHelper = new AccessHelper();
        private static List<string> ChargeItemlst = new List<string>();
       
        public static List<string> ChargeItemSort()
        {
            List<ChargeItemRecord> ChargeItems = new List<ChargeItemRecord>();
            ChargeItems = ChargeItemDAO.GetChargeItemList();
            ChargeItemlst.Clear();
            foreach (ChargeItemRecord ci in ChargeItems)
                ChargeItemlst.Add(ci.ChargeItem);
            return ChargeItemlst;
        }
        public static List<TuitionStandardRecord> GetTuitionStandardList()
        {
            TSRs = udtHelper.Select<TuitionStandardRecord>();
            TSRs.Sort(CompareItem);
            return TSRs;
        }
        private static object lockKey = new System.Windows.Forms.Form();
        public static List<TuitionStandardRecord> GetTuitionStandardBySS(int SchoolYear, String Semester)
        {
            

            List<TuitionStandardRecord> tss = new List<TuitionStandardRecord>();
            FISCA.UDT.Condition.ICondition condition;
            ChargeItemlst = ChargeItemSort();
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<TuitionStandardRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "'");
            }
            tss = udtHelper.Select<TuitionStandardRecord>(condition);
            tss.Sort(CompareItem);
            //foreach (TuitionStandardRecord tr in TSRs)
            //{
            //    if (tr.SchoolYear  == SchoolYear && tr.Semester==(Semester=="上學期"? 1:2))
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }

        public static List<TuitionStandardRecord> GetTuitionStandardBySST(int SchoolYear, String Semester,string TSName)
        {
            //GetTuitionStandardList();

            List<TuitionStandardRecord> tss = new List<TuitionStandardRecord>();
            FISCA.UDT.Condition.ICondition condition;
            ChargeItemlst = ChargeItemSort();
            lock (lockKey)
            {
                condition = udtHelper
                    .GetCompiler<TuitionStandardRecord>().Compiler("學年度='" + SchoolYear + "'" + " AND 學期='" + (Semester == "上學期" ? 1 : 2) + "' AND 收費標準名稱='"+TSName+"'");
            }
            tss = udtHelper.Select<TuitionStandardRecord>(condition);
            tss.Sort(CompareItem);
            //foreach (TuitionStandardRecord tr in TSRs)
            //{
            //    if (tr.SchoolYear == SchoolYear && tr.Semester == (Semester == "上學期" ? 1 : 2) && tr.TSName==TSName)
            //    {
            //        tss.Add(tr);
            //    }
            //}

            return tss;
        }
        public static string Insert(TuitionStandardRecord tsr)
        {
            List<ActiveRecord> TuitionStandards = new List<ActiveRecord>();
            TuitionStandards.Add(tsr);
            List<string> newIDs = udtHelper.InsertValues(TuitionStandards);
            return newIDs[0];
        }

        public static string Update(TuitionStandardRecord tsr)
        {
            List<ActiveRecord> TuitionStandards = new List<ActiveRecord>();
            TuitionStandards.Add(tsr);
            udtHelper.UpdateValues(TuitionStandards);
            return tsr.UID;
        }

        public static void Delete(TuitionStandardRecord tsr)
        {
            List<ActiveRecord> TuitionStandards = new List<ActiveRecord>();
            TuitionStandards.Add(tsr);
            udtHelper.DeletedValues(TuitionStandards);
        }
        //依收費項目排序副程式
        static int CompareItem(TuitionStandardRecord a, TuitionStandardRecord b)
        {
            
            

            return Framework.StringComparer.Comparer(a.ChargeItem, b.ChargeItem, ChargeItemlst.ToArray());
            //return Framework.StringComparer.Comparer(a.ChargeItem, b.ChargeItem, "學費", "雜費", "實習實驗費", "電腦使用費", "書籍及簿本費",  "校刊費", "學生平安保險費", "班級費", "家長會費","冷氣使用及維護費");
        }
    }
}
