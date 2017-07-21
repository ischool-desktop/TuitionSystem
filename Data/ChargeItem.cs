using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using FISCA.UDT;

namespace TuitionSystem.Data
{
    [TableName("收費項目")]
    public class ChargeItemRecord : ActiveRecord, IComparable<ChargeItemRecord>
    {
        [Field(Field = "項目", Indexed = false)]
        public string ChargeItem { get; set; }

        [Field(Field = "項目順序", Indexed = false)]
        public int ChargeItemOrder { get; set; }


        #region IComparable<ChargeItemRecord> 成員

        public int CompareTo(ChargeItemRecord other)
        {
            return this.ChargeItemOrder.CompareTo(other.ChargeItemOrder);
        }

        #endregion
    }

     
}