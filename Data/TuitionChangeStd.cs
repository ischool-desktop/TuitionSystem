using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using FISCA.UDT;

namespace TuitionSystem.Data
{
    [TableName("異動標準")]
    public class TuitionChangeStdRecord : ActiveRecord, IComparable<TuitionChangeStdRecord>
    {
        [Field(Field = "學年度", Indexed = false)]
        public int SchoolYear { get; set; }

        [Field(Field = "學期", Indexed = false)]
        public int Semester { get; set; }

        [Field(Field = "異動標準名稱", Indexed = false)]
        public string TCSName { get; set; }

        [Field(Field = "類別", Indexed = false)]
        public string MoneyType { get; set; }

        [Field(Field = "項目", Indexed = false)]
        public string ChargeItem { get; set; }

        [Field(Field = "百分比", Indexed = false)]
        public int Percent { get; set; }

        [Field(Field = "金額", Indexed = false)]
        public int Money { get; set; }

        #region IComparable<TuitionChangeStdRecord> 成員

        public int CompareTo(TuitionChangeStdRecord other)
        {
            return int.Parse(this.UID).CompareTo(int.Parse(other.UID));
        }

        #endregion
    }
}
