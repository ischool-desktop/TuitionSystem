using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuitionSystem.Data
{
    [TableName("異動明細輸入時間")]
    public class TuitionChangeOnlineInputSetting: ActiveRecord
    {
        [Field(Field = "學年度", Indexed = false)]
        public int SchoolYear { get; set; }

        [Field(Field = "學期", Indexed = false)]
        public int Semester { get; set; }

        [Field(Field = "開始時間", Indexed = false)]
        public DateTime BeginTime { get; set; }

        [Field(Field = "結束時間", Indexed = false)]
        public DateTime EndTime { get; set; }
    }
}
