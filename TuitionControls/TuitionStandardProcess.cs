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
using FISCA.UDT;
using System.Collections;


namespace TuitionSystem.TuitionControls
{
    public partial class TuitionStandardProcess : BaseForm
    {
        private List<TuitionStandardRecord> _Source = new List<TuitionStandardRecord>();
        private List<ChargeItemRecord> _ChargeItem = new List<ChargeItemRecord>();
        public TuitionStandardProcess()
        {
            InitializeComponent();
        }

        private void lstStdView_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnCopy.Enabled = false;
            btnDelete.Enabled = false;
            btnAddItem.Enabled = true;

            dataGridViewX1.Rows.Clear();
            if (lstStdView.SelectedItems.Count == 1)
            {
                btnCopy.Enabled = true;
                btnDelete.Enabled = true;
                for (int i = 0; i < lstStdView.Items.Count; i++)
                    if (lstStdView.Items[i].Selected)
                        _Source = TuitionStandardDAO.GetTuitionStandardBySST(int.Parse(lblSchoolYear.Text), lblSemester.Text, lstStdView.Items[i].Text);

                foreach (TuitionStandardRecord tcsr in _Source)
                    if (tcsr.ChargeItem != null)
                        if (!ChargeItem.Items.Contains(tcsr.ChargeItem))
                            ChargeItem.Items.Add(tcsr.ChargeItem);
                TuitionStandardRecord tsr = _Source[0];
                txtTSName.Text = tsr.TSName;
                cboClassYear.Text = tsr.ClassYear;
                cboDept.Text = tsr.Dept;
                cboGender.Text = tsr.Gender;
            }
            else
            {
                txtTSName.Text = "";
                cboClassYear.Text = "";
                cboDept.Text = "";
                cboGender.Text = "";
            }
            chkError();
        }



        private void btnCopy_Click(object sender, EventArgs e)
        {
            (new TuitionStandardCopy(txtTSName.Text)).ShowDialog();
            RefreshlstStdView();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<StudentTuitionRecord> sr = StudentTuitionDAO.GetStudentTuitionBySSTName(int.Parse(lblSchoolYear.Text), lblSemester.Text, txtTSName.Text);
            if (sr.Count > 0)
            {
                MessageBox.Show("此收費標準已有人使用，不能刪除");
                return;
            }
            foreach (TuitionStandardRecord tsr in _Source)
                tsr.Deleted = true;
            _Source.SaveAll();

            RefreshlstStdView();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Add();
            chkError();
            //_Source = new List<TuitionStandardRecord>(_Source);
            //_Source.Add(new TuitionStandardRecord() );
            //dataGridViewX1.EndEdit();
            //dataGridViewX1.CancelEdit();            
            ////DataBinding至dataGridView1
            //dataGridViewX1.DataSource = null;
            //dataGridViewX1.DataSource = _Source;
            //chkError();            
            //dataGridViewX1.CurrentCell = dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 1].Cells[1];
            //dataGridViewX1.CurrentCell.Selected = true;
            //dataGridViewX1.BeginEdit(true);
            //btnSave.Enabled = false;            
        }

        private void TuitionStandardProcess_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;
            if (GlobalValue.CurrentSchoolYear == 0 || GlobalValue.CurrentSemester == null)
            {
                MessageBox.Show("未設定學年度學期");
                this.Close();
            }
            cboDept.Items.Clear();
            foreach (SHSchool.Data.SHDepartmentRecord dr in SHSchool.Data.SHDepartment.SelectAll())
                this.cboDept.Items.Add(dr.FullName);


            lblSchoolYear.Text = GlobalValue.CurrentSchoolYear.ToString();
            lblSemester.Text = GlobalValue.CurrentSemester;
            dataGridViewX1.EndEdit();
            dataGridViewX1.CancelEdit();
            _ChargeItem = ChargeItemDAO.GetChargeItemList();
            ChargeItem.Items.Clear();
            ChargeItem.Items.Add("");
            foreach (ChargeItemRecord cr in _ChargeItem)
                ChargeItem.Items.Add(cr.ChargeItem);
            RefreshlstStdView();

        }
        private void RefreshlstStdView()
        {
            List<TuitionStandardRecord> TSRs = TuitionStandardDAO.GetTuitionStandardBySS(int.Parse(lblSchoolYear.Text), lblSemester.Text);
            //List<string> TSName=new List<string>();
            lstStdView.Items.Clear();
            foreach (var tsr in TSRs)
                if (!lstStdView.Items.ContainsKey(tsr.TSName))
                    lstStdView.Items.Add(tsr.TSName, tsr.TSName, "");
            lstStdView.ListViewItemSorter = new ListViewItemComparer();
            btnCopy.Enabled = false;
            btnDelete.Enabled = false;
            btnAddItem.Enabled = false;
            btnSave.Enabled = false;
            cboDept.Text = null;
            cboGender.Text = null;
            cboClassYear.Text = null;
            txtTSName.Text = "";
            dataGridViewX1.DataSource = null;
        }
        private void dataGridViewX1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            if (e.Exception != null)
                dataGridViewX1[e.ColumnIndex, e.RowIndex].ErrorText = e.Exception.Message;
            dataGridViewX1.UpdateCellErrorText(e.ColumnIndex, e.RowIndex);
            dataGridViewX1[e.ColumnIndex, e.RowIndex].Value = null;
        }
        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            chkError();
        }
        private void chkValidity(object sender, EventArgs e)
        {
            chkError();
        }
        private void chkError()
        {
            bool hasError = false;
            if (txtTSName.Text == "") { 
                hasError = true;
            }
            int result;
            for (int i = dataGridViewX1.Rows.Count - 1; i >= 0; i--)
            {
                if ("" + dataGridViewX1.Rows[i].Cells[1].Value == "")
                    hasError = true;
                else
                {
                    dataGridViewX1[2, i].ErrorText = "";
                    dataGridViewX1.UpdateCellErrorText(2, i);
                    if (!int.TryParse("" + dataGridViewX1[2, i].Value, out result))
                    {
                        dataGridViewX1[2, i].ErrorText = "!";
                        dataGridViewX1.UpdateCellErrorText(2, i);
                        hasError = true;
                    }
                    dataGridViewX1[1, i].ErrorText = "";
                    dataGridViewX1.UpdateCellErrorText(1, i);
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (("" + dataGridViewX1.Rows[i].Cells[1].Value).Trim() == ("" + dataGridViewX1.Rows[j].Cells[1].Value).Trim())
                        {
                            hasError = true;
                            dataGridViewX1[1, i].ErrorText = "!";
                            dataGridViewX1.UpdateCellErrorText(1, i);
                        }
                    }
                }

            }
            //for (int i = 0; i < dataGridViewX1.Rows.Count;i++ )
            //    for (int j= 0; j < dataGridViewX1.Rows.Count; j++)
            //        if (dataGridViewX1.Rows[i].Cells[1].Value.ToString().Trim()==dataGridViewX1.Rows[j].Cells[1].Value.ToString().Trim() && i!=j) 
            //        {
            //            IsRepeat = true;
            //            dataGridViewX1[1, i].ErrorText = "!";
            //            dataGridViewX1.UpdateCellErrorText(1,i);                    
            //        }
            if (hasError)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

        }
        void btnCopySemData_Click(object sender, System.EventArgs e)
        {
            int cpSchoolYear;
            string cpSemester;

            List<TuitionStandardRecord> TSRs = TuitionStandardDAO.GetTuitionStandardBySS(int.Parse(lblSchoolYear.Text), lblSemester.Text);
            if (TSRs.Count == 0)
            {
                cpSchoolYear = int.Parse(lblSchoolYear.Text) - 1;
                cpSemester = lblSemester.Text;
                TSRs = TuitionStandardDAO.GetTuitionStandardBySS(cpSchoolYear, cpSemester);
                if (TSRs.Count == 0)
                {
                    if (lblSemester.Text == "上學期")
                    {
                        cpSemester = "下學期";
                    }
                    else
                    {
                        cpSemester = "上學期";
                        cpSchoolYear = int.Parse(lblSchoolYear.Text);
                    }
                    TSRs = TuitionStandardDAO.GetTuitionStandardBySS(cpSchoolYear, cpSemester);
                }

                {
                    Framework.MultiThreadBackgroundWorker<TuitionStandardRecord> mBKW = new Framework.MultiThreadBackgroundWorker<TuitionStandardRecord>();

                    mBKW.DoWork += delegate (object sender2, Framework.PackageDoWorkEventArgs<TuitionStandardRecord> e2)
                    {
                        List<TuitionStandardRecord> trs = new List<TuitionStandardRecord>();
                        foreach (var tsr in e2.Items)
                        {
                            TuitionStandardRecord newtsr = new TuitionStandardRecord();
                            newtsr.TSName = tsr.TSName;
                            newtsr.Gender = tsr.Gender;
                            newtsr.Dept = tsr.Dept;
                            newtsr.ClassYear = tsr.ClassYear;
                            newtsr.SchoolYear = int.Parse(lblSchoolYear.Text);
                            newtsr.Semester = (lblSemester.Text == "上學期" ? 1 : 2);
                            newtsr.Money = tsr.Money;
                            newtsr.ChargeItem = tsr.ChargeItem;
                            trs.Add(newtsr);
                        }
                        trs.SaveAll();
                    };
                    mBKW.ProgressChanged += delegate (object sender3, ProgressChangedEventArgs e3)
                    {
                        FISCA.Presentation.MotherForm.SetStatusBarMessage("正在複製收費標準...", e3.ProgressPercentage);
                    };
                    mBKW.RunWorkerCompleted += delegate (object sender4, RunWorkerCompletedEventArgs e4)
                    {
                        FISCA.Presentation.MotherForm.SetStatusBarMessage("");
                        MessageBox.Show("複製完成");
                        RefreshlstStdView();
                    };
                    //設定包的大小,1為人數
                    mBKW.PackageSize = 20;
                    mBKW.RunWorkerAsync(TSRs);
                }
            }
            else
            {
                MessageBox.Show("已有資料，不能複製");
            }


        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            dataGridViewX1.EndEdit();
            dataGridViewX1.CancelEdit();
            int Money = 0;
            foreach (TuitionStandardRecord tsr in _Source)
            {
                tsr.SchoolYear = int.Parse(lblSchoolYear.Text);
                tsr.Semester = (lblSemester.Text == "上學期" ? 1 : 2);
                tsr.ClassYear = cboClassYear.Text;
                tsr.Dept = cboDept.Text;
                tsr.TSName = txtTSName.Text;
                tsr.Gender = cboGender.Text;
                Money += tsr.Money;
            }
            _Source.SaveAll();
            if (MessageBox.Show("儲存收費標準，是否更改相關資料?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //更改使用到本收費標準的所有繳費表
                List<StudentTuitionRecord> STRs = StudentTuitionDAO.GetStudentTuitionBySSTName(int.Parse(lblSchoolYear.Text), lblSemester.Text, txtTSName.Text);
                foreach (StudentTuitionRecord sr in STRs)
                {
                    if (sr.PayDate != null)
                        MessageBox.Show("有人使用本收費標準且已註冊，不修改該繳費表");
                    else
                    {
                        SetTuition.ChangeTuitionDetail(_Source, sr.UID);
                        sr.ChangeMoney = SetTuition.ReCalcChangeMoney(sr.UID);
                        sr.ChargeAmount = Money + sr.ChangeMoney;
                        sr.UpLoad = false;
                    }
                }
                STRs.SaveAll();
            }
            RefreshlstStdView();
        }


        // Implements the manual sorting of items by columns.
        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                int g1 = 0, g2 = 0;
                if (((ListViewItem)x).SubItems[col].Text.Contains("日"))
                {
                    if (((ListViewItem)x).SubItems[col].Text.Contains("一年級")) g1 = 1;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("二年級")) g1 = 2;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("三年級")) g1 = 3;
                };
                if (((ListViewItem)x).SubItems[col].Text.Contains("建"))
                {
                    if (((ListViewItem)x).SubItems[col].Text.Contains("一年級")) g1 = 4;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("二年級")) g1 = 5;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("三年級")) g1 = 6;
                };
                if (((ListViewItem)x).SubItems[col].Text.Contains("日實"))
                {
                    if (((ListViewItem)x).SubItems[col].Text.Contains("一年級")) g1 = 7;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("二年級")) g1 = 8;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("三年級")) g1 = 9;
                };
                if (((ListViewItem)x).SubItems[col].Text.Contains("綜"))
                {
                    if (((ListViewItem)x).SubItems[col].Text.Contains("一年級")) g1 = 10;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("二年級")) g1 = 11;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("三年級")) g1 = 12;
                };
                if (((ListViewItem)x).SubItems[col].Text.Contains("夜實"))
                {
                    if (((ListViewItem)x).SubItems[col].Text.Contains("一年級")) g1 = 13;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("二年級")) g1 = 14;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("三年級")) g1 = 15;
                };
                if (((ListViewItem)x).SubItems[col].Text.Contains("進"))
                {
                    if (((ListViewItem)x).SubItems[col].Text.Contains("一年級")) g1 = 16;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("二年級")) g1 = 17;
                    if (((ListViewItem)x).SubItems[col].Text.Contains("三年級")) g1 = 18;
                };
                if (((ListViewItem)y).SubItems[col].Text.Contains("日"))
                {
                    if (((ListViewItem)y).SubItems[col].Text.Contains("一年級")) g2 = 1;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("二年級")) g2 = 2;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("三年級")) g2 = 3;
                };
                if (((ListViewItem)y).SubItems[col].Text.Contains("建"))
                {
                    if (((ListViewItem)y).SubItems[col].Text.Contains("一年級")) g2 = 4;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("二年級")) g2 = 5;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("三年級")) g2 = 6;
                };
                if (((ListViewItem)y).SubItems[col].Text.Contains("日實"))
                {
                    if (((ListViewItem)y).SubItems[col].Text.Contains("一年級")) g2 = 7;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("二年級")) g2 = 8;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("三年級")) g2 = 9;
                };
                if (((ListViewItem)y).SubItems[col].Text.Contains("綜"))
                {
                    if (((ListViewItem)y).SubItems[col].Text.Contains("一年級")) g2 = 10;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("二年級")) g2 = 11;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("三年級")) g2 = 12;
                };
                if (((ListViewItem)y).SubItems[col].Text.Contains("夜實"))
                {
                    if (((ListViewItem)y).SubItems[col].Text.Contains("一年級")) g2 = 13;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("二年級")) g2 = 14;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("三年級")) g2 = 15;
                };
                if (((ListViewItem)y).SubItems[col].Text.Contains("進"))
                {
                    if (((ListViewItem)y).SubItems[col].Text.Contains("一年級")) g2 = 16;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("二年級")) g2 = 17;
                    if (((ListViewItem)y).SubItems[col].Text.Contains("三年級")) g2 = 18;
                };
                return g1.CompareTo(g2);

            }
        }

    }
}
