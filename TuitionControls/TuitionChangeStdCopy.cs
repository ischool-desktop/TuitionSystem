using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using TuitionSystem.Data;

namespace TuitionSystem.TuitionControls
{
    public partial class TuitionChangeStdCopy : BaseForm
    {
        private string _TCSName;
        public TuitionChangeStdCopy(string TCSName)
        {
            _TCSName = TCSName;
            InitializeComponent();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            if (txtTCSName.Text == "" || cboSemester.Text==null)
            {
                MessageBox.Show("資料不齊全，無法複製");
                return;
            }
            List<TuitionChangeStdRecord> _tsrs = new List<TuitionChangeStdRecord>();
            List<TuitionChangeStdRecord> tsrs = new List<TuitionChangeStdRecord>();
            _tsrs = TuitionChangeStdDAO.GetTuitionChangeStdBySST(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, _TCSName);
            tsrs = TuitionChangeStdDAO.GetTuitionChangeStdBySST(intSchoolYear.Value, cboSemester.Text, txtTCSName.Text);
            if (tsrs.Count>1)
            {
                MessageBox.Show("異動標準名稱已存在，無法複製");
                return;
            }
            foreach (TuitionChangeStdRecord tsr in _tsrs)
            {
                TuitionChangeStdRecord newtsr = new TuitionChangeStdRecord();
                newtsr.TCSName = txtTCSName.Text;
                newtsr.MoneyType = tsr.MoneyType;
                newtsr.Percent = tsr.Percent;
                newtsr.SchoolYear = intSchoolYear.Value;
                newtsr.Semester = (cboSemester.Text == "上學期" ? 1 : 2);
                newtsr.Money = tsr.Money;
                newtsr.ChargeItem = tsr.ChargeItem;
                newtsr.Save();
            }
            MessageBox.Show("複製完成");
            this.Close();

        }

        private void TuitionChangeStdCopy_Load(object sender, EventArgs e)
        {
            
            intSchoolYear.Value = GlobalValue.CurrentSchoolYear;
            cboSemester.Text = GlobalValue.CurrentSemester;            
        }
    }
}
