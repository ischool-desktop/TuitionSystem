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


namespace TuitionSystem.TuitionControls
{
    public partial class TuitionChangeStdProcess : BaseForm
    {
        private List<TuitionChangeStdRecord> _Source = new List<TuitionChangeStdRecord>();
        private List<ChargeItemRecord> _ChargeItem = new List<ChargeItemRecord>();
        public TuitionChangeStdProcess()
        {
            InitializeComponent();
        }

        private void lstStdView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstStdView.SelectedItems.Count > 0)
            {
                btnCopy.Enabled = false;
                btnDelete.Enabled = false;
                btnAddItem.Enabled = false;
                btnSave.Enabled = false;
            }

            if (lstStdView.SelectedItems.Count == 1)
            {
                btnCopy.Enabled = true;
                btnDelete.Enabled = true;
                for (int i=0;i<lstStdView.Items.Count;i++)
                    if (lstStdView.Items[i].Selected)
                        _Source = TuitionChangeStdDAO.GetTuitionChangeStdBySST(int.Parse(lblSchoolYear.Text), lblSemester.Text, lstStdView.Items[i].Text);

                dataGridViewX1.DataSource = null;
                if (_Source.Count>1 || _Source[0].ChargeItem!="")
                   dataGridViewX1.DataSource = _Source;
                TuitionChangeStdRecord tsr = _Source[0];
                txtTCSName.Text = tsr.TCSName;
                cboType.Text  = tsr.MoneyType;
                txtMoney.Text = tsr.Money.ToString();
                btnAddItem.Enabled = true;
                btnSave.Enabled = true;
            }
         }

       

        private void btnCopy_Click(object sender, EventArgs e)
        {
            (new TuitionChangeStdCopy(txtTCSName.Text)).ShowDialog();
            RefreshlstStdView();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<TuitionDetailRecord> TDRs = TuitionDetailDAO.GetTuitionDetailBySST(int.Parse(lblSchoolYear.Text), lblSemester.Text, txtTCSName.Text);
            if (TDRs.Count > 0)
            {
                MessageBox.Show("異動標準有人使用，不能刪除");
                return;
            }
            foreach (TuitionChangeStdRecord tsr in _Source)
                   tsr.Deleted = true;
            _Source.SaveAll();

            RefreshlstStdView();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (_Source==null)
            {
                _Source = new List<TuitionChangeStdRecord>();
                _Source.Add(new TuitionChangeStdRecord());
            }
            if (_Source.Count == 0)
            {
                _Source = new List<TuitionChangeStdRecord>(_Source);
                _Source.Add(new TuitionChangeStdRecord());
            }
            if (_Source.Count >= 1 && _Source[0].ChargeItem != "" && _Source[0].ChargeItem !=null)
            {
                _Source = new List<TuitionChangeStdRecord>(_Source);
                _Source.Add(new TuitionChangeStdRecord());
            }

            dataGridViewX1.EndEdit();
            dataGridViewX1.CancelEdit();            
            //DataBinding至dataGridView1
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = _Source;
            chkError();            
            dataGridViewX1.CurrentCell = dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 1].Cells[1];
            dataGridViewX1.CurrentCell.Selected = true;
            dataGridViewX1.BeginEdit(true);
            btnSave.Enabled = false;            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataGridViewX1.EndEdit();
            dataGridViewX1.CancelEdit();
            if (_Source == null || _Source.Count==0)
            {
                _Source = new List<TuitionChangeStdRecord>();
                _Source.Add(new TuitionChangeStdRecord());
            }
            foreach (TuitionChangeStdRecord tsr in _Source)
              {
                  if (_Source.Count == 1 && tsr.Deleted == true)
                  {
                      tsr.ChargeItem = null;
                      tsr.Percent = 0;
                      tsr.Deleted = false;
                  }
                tsr.SchoolYear = int.Parse(lblSchoolYear.Text);
                tsr.Semester=(lblSemester.Text=="上學期"? 1:2);
                tsr.MoneyType=cboType.Text;
                int abc;
                if (int.TryParse(txtMoney.Text, out abc))
                    tsr.Money = abc;
                else
                {
                    MessageBox.Show("異動金額設定錯誤");
                    return;
                }
                //tsr.Money=(txtMoney.Text==""? 0:int.Parse(txtMoney.Text));
                tsr.TCSName=txtTCSName.Text;                
            }
            _Source.SaveAll();
            //更正使用到此異動項目之繳費表金額，異動金額
            if (MessageBox.Show("儲存異動標準，是否更改相關資料?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<TuitionDetailRecord> TDRs = TuitionDetailDAO.GetTuitionDetailBySST(int.Parse(lblSchoolYear.Text), lblSemester.Text, txtTCSName.Text);
                foreach (TuitionDetailRecord tdr in TDRs)
                {
                    //繳費表
                    List<StudentTuitionRecord> sr = StudentTuitionDAO.GetStudentTuitionByUID(tdr.STUID);
                    if (sr.Count != 0)
                    {
                        if (sr[0].PayDate != null)
                            MessageBox.Show("有人使用本異動標準且已註冊，不修改該繳費表");
                        else
                        {
                            //收費標準
                            List<TuitionStandardRecord> TSRs = TuitionStandardDAO.GetTuitionStandardBySST(int.Parse(lblSchoolYear.Text), lblSemester.Text, sr[0].TSName);
                            //異動金額
                            tdr.ChangeAmount = SetTuition.CalcChangeMoneyByTCSName(TSRs, sr[0].UID, txtTCSName.Text);
                            tdr.Save();
                            //計算繳費表費用
                            int Money = SetTuition.CalcTuition(TSRs);
                            //計算異動金額
                            int ChangeMoney = SetTuition.CalcChangeMoney(TSRs, tdr.STUID);
                            //更改繳費表資料
                            sr[0].ChangeMoney = ChangeMoney;
                            sr[0].ChargeAmount = Money + ChangeMoney;
                            sr[0].UpLoad = false;
                            sr[0].Save();
                        }
                    }
                }
            }
            _Source = null;
            RefreshlstStdView();
        }

        private void TuitionChangeStdProcess_Load(object sender, EventArgs e)
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
            dataGridViewX1.EndEdit();
            dataGridViewX1.CancelEdit();
            _ChargeItem = ChargeItemDAO.GetChargeItemList();
            ChargeItem.Items.Clear();
            foreach (ChargeItemRecord cr in _ChargeItem)
                ChargeItem.Items.Add(cr.ChargeItem);
            RefreshlstStdView();
            
        }
        private void RefreshlstStdView()
        {
            List<TuitionChangeStdRecord> TSRs = TuitionChangeStdDAO.GetTuitionChangeStdBySS(int.Parse(lblSchoolYear.Text), lblSemester.Text);
            //List<string> TSName=new List<string>();
            lstStdView.Items.Clear();            
            foreach (var tsr in TSRs)
                if (!lstStdView.Items.ContainsKey(tsr.TCSName))
                    lstStdView.Items.Add(tsr.TCSName,tsr.TCSName,"");
            btnCopy.Enabled = false;
            btnDelete.Enabled = false;
            btnAddItem.Enabled = false;
            btnSave.Enabled = false;
            cboType.Text = null;            
            txtMoney.Text = "0";
            txtTCSName.Text = "";            
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
        private void chkError()
        {
            Boolean IsError;
            IsError = false;
            int result;
            for (int i = dataGridViewX1.Rows.Count - 1; i >= 0; i--)
            {
                if (""+dataGridViewX1.Rows[i].Cells[1].Value == "")
                    IsError = true;
                else
                {
                    dataGridViewX1[2, i].ErrorText = "";
                    dataGridViewX1.UpdateCellErrorText(2, i);                    
                    if (!int.TryParse(""+dataGridViewX1[2, i].Value, out result))
                    {
                        dataGridViewX1[2, i].ErrorText = "!";
                        dataGridViewX1.UpdateCellErrorText(2, i);
                        IsError = true;
                    }
                    else
                    {
                        result = int.Parse(""+dataGridViewX1[2, i].Value);
                        if (result> 100 || result < 1)
                        {
                            dataGridViewX1[2, i].ErrorText = "!";
                            dataGridViewX1.UpdateCellErrorText(2, i);
                            IsError = true;
                        }
                    } 
                    dataGridViewX1[1, i].ErrorText = "";
                    dataGridViewX1.UpdateCellErrorText(1, i);
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (("" + dataGridViewX1.Rows[i].Cells[1].Value).Trim() == ("" + dataGridViewX1.Rows[j].Cells[1].Value).Trim())
                        {
                            IsError = true;
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
            if (IsError)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

        }

        private void chkValidity(object sender, EventArgs e)
        {
            int intMoney;
            if (txtTCSName.Text == "" || cboType.Text == "" || !int.TryParse(txtMoney.Text, out intMoney))
            {
                btnAddItem.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                //判斷異動標準是否存在
                if (lstStdView.SelectedItems.Count == 1)
                {
                    for (int i = 0; i < lstStdView.Items.Count; i++)
                        if (lstStdView.Items[i].Selected)
                            if (txtTCSName.Text == lstStdView.Items[i].Text)
                                return;
                }
                List<TuitionChangeStdRecord> tsrs = new List<TuitionChangeStdRecord>();
                tsrs = TuitionChangeStdDAO.GetTuitionChangeStdBySST(int.Parse(lblSchoolYear.Text), lblSemester.Text, txtTCSName.Text);
                if (tsrs.Count > 1 && _Source.Count == 0)
                {
                    MessageBox.Show("異動標準名稱已存在，請重新命名");
                    return;
                }
                btnAddItem.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        void btnCopySemData_Click(object sender, System.EventArgs e)
        {
            int cpSchoolYear;
            string cpSemester;
            if (lblSemester.Text == "上學期")
            {
                cpSemester = "下學期";
                cpSchoolYear = int.Parse(lblSchoolYear.Text) - 1;
            }
            else
            {
                cpSemester = "上學期";
                cpSchoolYear = int.Parse(lblSchoolYear.Text);
            }
            List<TuitionChangeStdRecord> TSRs = TuitionChangeStdDAO.GetTuitionChangeStdBySS(int.Parse(lblSchoolYear.Text), lblSemester.Text);
            if (TSRs.Count == 0)
            {
                TSRs = TuitionChangeStdDAO.GetTuitionChangeStdBySS(cpSchoolYear, cpSemester);
                {
                    Framework.MultiThreadBackgroundWorker<TuitionChangeStdRecord> mBKW = new Framework.MultiThreadBackgroundWorker<TuitionChangeStdRecord>();

                    mBKW.DoWork += delegate(object sender2, Framework.PackageDoWorkEventArgs<TuitionChangeStdRecord> e2)
                    {
                        List<TuitionChangeStdRecord> tcsr = new List<TuitionChangeStdRecord>();
                        foreach (var tsr in e2.Items)
                        {
                            TuitionChangeStdRecord newtsr = new TuitionChangeStdRecord();
                            newtsr.TCSName = tsr.TCSName;
                            newtsr.MoneyType = tsr.MoneyType;
                            newtsr.Percent = tsr.Percent;
                            newtsr.SchoolYear = int.Parse(lblSchoolYear.Text);
                            newtsr.Semester = (lblSemester.Text == "上學期" ? 1 : 2);
                            newtsr.Money = tsr.Money;
                            newtsr.ChargeItem = tsr.ChargeItem;
                            tcsr.Add(newtsr);  
                        }
                        tcsr.SaveAll();
                    };
                    mBKW.ProgressChanged += delegate(object sender3, ProgressChangedEventArgs e3)
                    {
                        FISCA.Presentation.MotherForm.SetStatusBarMessage("正在複製異動標準...", e3.ProgressPercentage);
                    };
                    mBKW.RunWorkerCompleted += delegate(object sender4, RunWorkerCompletedEventArgs e4)
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
       
    }
}
