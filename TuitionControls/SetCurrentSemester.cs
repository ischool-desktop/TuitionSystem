using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TuitionSystem.TuitionControls
{
    public partial class SetCurrentSemester : UserControl
    {
        public SetCurrentSemester()
        {
            InitializeComponent();
        }

        private void SetCurrentSemester_Load(object sender, EventArgs e)
        {
            if (DateTime.Today.Month < 6)
            {
                intSchoolYear.Value = DateTime.Today.Year - 1912;
                cboSemester.Text = "下學期";
            }
            if (DateTime.Today.Month >= 6 && DateTime.Today.Month < 12)
            {
                intSchoolYear.Value = DateTime.Today.Year-1911;
                cboSemester.Text = "上學期";
            }
            if (DateTime.Today.Month ==12)
            {
                intSchoolYear.Value = DateTime.Today.Year-1911 ;
                cboSemester.Text = "下學期";
            }
            GlobalValue.CurrentSchoolYear = intSchoolYear.Value;
            GlobalValue.CurrentSemester = cboSemester.Text;
        }

        private void intSchoolYear_ValueChanged(object sender, EventArgs e)
        {
            GlobalValue.CurrentSchoolYear = intSchoolYear.Value;            
        }

        private void cboSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalValue.CurrentSemester = cboSemester.Text;
        }
    }
}
