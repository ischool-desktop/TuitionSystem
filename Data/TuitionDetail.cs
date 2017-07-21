using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using FISCA.UDT;

namespace TuitionSystem.Data
{
    [TableName("收費表異動明細")]
    public class TuitionDetailRecord : ActiveRecord
    {
        [Field(Field = "學年度", Indexed = false)]
        public int SchoolYear { get; set; }

        [Field(Field = "學期", Indexed = false)]
        public int Semester { get; set; }

        [Field(Field = "收費表", Indexed = false)]
        public string STUID { get; set; }

        [Field(Field = "異動標準名稱", Indexed = false)]
        public string TCSName { get; set; }

        [Field(Field = "異動金額", Indexed = false)]
        public int ChangeAmount { get; set; }
    }
}