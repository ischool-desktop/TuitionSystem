using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using FISCA.UDT;

namespace TuitionSystem.Data
{
    public class ChargeItemDAO
    {
        private static List<ChargeItemRecord> CIs=new List<ChargeItemRecord>();
        private static AccessHelper udtHelper = new AccessHelper();

        public static List<ChargeItemRecord> GetChargeItemList()
        {
            CIs = udtHelper.Select<ChargeItemRecord>();
            CIs.Sort();
            return CIs;
        }

        
        public static string Insert(ChargeItemRecord tsr)
        {
            List<ActiveRecord> ChargeItems = new List<ActiveRecord>();
            ChargeItems.Add(tsr);
            List<string> newIDs = udtHelper.InsertValues(ChargeItems);
            return newIDs[0];
        }

        public static string Update(ChargeItemRecord tsr)
        {
            List<ActiveRecord> ChargeItems = new List<ActiveRecord>();
            ChargeItems.Add(tsr);
            udtHelper.UpdateValues(ChargeItems);
            return tsr.UID;
        }

        public static void Delete(ChargeItemRecord tsr)
        {
            List<ActiveRecord> ChargeItems = new List<ActiveRecord>();
            ChargeItems.Add(tsr);
            udtHelper.DeletedValues(ChargeItems);
        }

    }
}
