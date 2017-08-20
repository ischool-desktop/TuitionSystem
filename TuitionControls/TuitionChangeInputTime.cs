using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TuitionSystem.Data;

namespace TuitionSystem.TuitionControls
{
    public partial class TuitionChangeInputTime : FISCA.Presentation.Controls.BaseForm
    {
        AccessHelper _AccessHelper = new AccessHelper();
        List<TuitionChangeOnlineInputSetting> _Source = null;

        public TuitionChangeInputTime()
        {
            InitializeComponent();
        }

        private void TuitionChangeInputTime_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;
            if (GlobalValue.CurrentSchoolYear == 0 || GlobalValue.CurrentSemester == null)
            {
                MessageBox.Show("未設定學年度學期");
                this.Close();
            }
            lblSchoolYear.Text = GlobalValue.CurrentSchoolYear.ToString();
            lblSemester.Text = GlobalValue.CurrentSemester;

            _Source = _AccessHelper.Select<TuitionChangeOnlineInputSetting>("學年度="+ GlobalValue.CurrentSchoolYear+ " AND 學期=" + (GlobalValue.CurrentSemester == "上學期" ? 1 : 2));
            if(_Source.Count == 0)
            {
                txtBegin.Text = txtEnd.Text = "";
            }
            else
            {
                txtBegin.Text = _Source[0].BeginTime.ToString("yyyy/MM/dd HH:mm:ss");
                txtEnd.Text = _Source[0].EndTime.ToString("yyyy/MM/dd HH:mm:ss");
            }
            //verify(null, null);
        }

        private void verify(object sender, EventArgs e)
        {
            bool pass = true;
            DateTime dt1;
            DateTime dt2;
            if (DateTime.TryParse(txtBegin.Text, out dt1)&& DateTime.TryParse(txtEnd.Text, out dt2) && dt2>dt1)
                pass = true;
            else
                pass = false;
            btnSave.Enabled = pass;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (var item in _Source)
            {
                item.Deleted = true;
            }
            if (_Source.Count > 0)
            {
                _Source[0].Deleted = false;
                _Source[0].BeginTime = DateTime.Parse(txtBegin.Text);
                _Source[0].EndTime = DateTime.Parse(txtEnd.Text);
            }
            else
            {
                _Source.Add(new TuitionChangeOnlineInputSetting());
                _Source[0].SchoolYear = GlobalValue.CurrentSchoolYear;
                _Source[0].Semester = (GlobalValue.CurrentSemester == "上學期" ? 1 : 2);
                _Source[0].BeginTime = DateTime.Parse(txtBegin.Text);
                _Source[0].EndTime = DateTime.Parse(txtEnd.Text);
            }
            _Source.SaveAll();
            this.Close();
        }
    }
}
