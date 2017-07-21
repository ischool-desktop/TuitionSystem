using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using FISCA.UDT;

namespace TuitionSystem.Data
{
    [TableName("收費表")]
    public class StudentTuitionRecord : ActiveRecord
    {
        [Field(Field = "學年度", Indexed = false)]
        public int SchoolYear { get; set; }

        [Field(Field = "學期", Indexed = false)]
        public int Semester { get; set; }

        [Field(Field = "新舊生", Indexed = false)]
        public string StudentType { get; set; }

        [Field(Field = "學生識別號", Indexed = false)]
        public string TuitionUID { get; set; }

        [Field(Field = "收費標準名稱", Indexed = false)]
        public string TSName { get; set; }

        [Field(Field = "異動金額", Indexed = false)]
        public int ChangeMoney { get; set; }

        [Field(Field = "應繳金額", Indexed = false)]
        public int ChargeAmount { get; set; }

        [Field(Field = "實繳金額", Indexed = false)]
        public int Payment { get; set; }

        [Field(Field = "繳費日期", Indexed = false)]
        public DateTime? PayDate { get; set; }

        [Field(Field = "是否上傳", Indexed = false)]
        public Boolean UpLoad { get; set; }

        [Field(Field = "繳費單上傳編號", Indexed = false)]
        public string PaymentID{ get; set; }

        [Field(Field = "繳款處", Indexed = false)]
        public string PayLocation { get; set; }
    }
}
