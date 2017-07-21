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
    public partial class TuitionStandardCopy : BaseForm
    {
        private string _TSName;
        public TuitionStandardCopy(string TSName)
        {
            _TSName = TSName;
            InitializeComponent();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            if (txtTSName.Text == "" || cboGender.Text == null || cboClassYear.Text == null || cboDept == null || cboSemester.Text==null)
            {
                MessageBox.Show("資料不齊全，無法複製");
                return;
            }
            List<TuitionStandardRecord> _tsrs = new List<TuitionStandardRecord>();
            List<TuitionStandardRecord> tsrs = new List<TuitionStandardRecord>();
            _tsrs = TuitionStandardDAO.GetTuitionStandardBySST(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, _TSName);
            tsrs = TuitionStandardDAO.GetTuitionStandardBySST(intSchoolYear.Value, cboSemester.Text, txtTSName.Text);
            if (tsrs.Count>1)
            {
                MessageBox.Show("收費標準名稱已存在，無法複製");
                return;
            }
            foreach (TuitionStandardRecord tsr in _tsrs)
            {
                TuitionStandardRecord newtsr = new TuitionStandardRecord();
                newtsr.TSName = txtTSName.Text;
                newtsr.Gender = cboGender.Text;
                newtsr.Dept = cboDept.Text;
                newtsr.ClassYear = cboClassYear.Text;
                newtsr.SchoolYear = intSchoolYear.Value;
                newtsr.Semester = (cboSemester.Text=="上學期"? 1:2);
                newtsr.Money = tsr.Money;
                newtsr.ChargeItem = tsr.ChargeItem;
                newtsr.Save();
            }
            MessageBox.Show("複製完成");
            this.Close();

        }

        private void TuitionStandardCopy_Load(object sender, EventArgs e)
        {
            cboDept.Items.Clear();
            intSchoolYear.Value = GlobalValue.CurrentSchoolYear;
            cboSemester.Text = GlobalValue.CurrentSemester;    
            foreach (SHSchool.Data.SHDepartmentRecord dr in SHSchool.Data.SHDepartment.SelectAll())
                this.cboDept.Items.Add(dr.FullName);
        }
    }
}
