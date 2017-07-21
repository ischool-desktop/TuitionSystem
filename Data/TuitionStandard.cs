using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using FISCA.UDT;

namespace TuitionSystem.Data
{
    [TableName("收費標準")]
    public class TuitionStandardRecord : ActiveRecord, IComparable<TuitionStandardRecord>
    {
        [Field(Field = "學年度", Indexed = false)]
        public int SchoolYear { get; set; }

        [Field(Field = "學期", Indexed = false)]
        public int Semester { get; set; }

        [Field(Field = "收費標準名稱", Indexed = false)]
        public string TSName { get; set; }

        [Field(Field = "性別", Indexed = false)]
        public string Gender { get; set; }

        [Field(Field = "科別名稱", Indexed = false)]
        public string Dept { get; set; }

        [Field(Field = "年級", Indexed = false)]
        public string ClassYear { get; set; }

        [Field(Field = "項目", Indexed = false)]
        public string ChargeItem { get; set; }
        
        [Field(Field = "金額", Indexed = false)]
        public int Money { get; set; }

        #region IComparable<TuitionStandardRecord> 成員

        public int CompareTo(TuitionStandardRecord other)
        {
            return int.Parse(this.UID).CompareTo(int.Parse(other.UID));
        }

        #endregion
    }
    
}
