using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using TuitionSystem.Data;
using TuitionSystem.TuitionControls;
using FISCA.UDT;
using Framework;
//using MySchoolModule;
using SmartSchool;
namespace TuitionSystem
{
    public partial class StudentTuitionProcess : UserControl
    {
        private OpenMode openMode;                     //學生類別
        private StudentTuitionRecord TuitionRecord;   //目前處理的繳費表
        private List<TuitionDetailRecord> _Source;     //目前處理的異動記錄
        private List<TuitionStandardRecord> TSRs;      //目前使用的收費標準
        private int TuitionCharge;                     //目前使用的收費標準收費總計
        private string stUID;                          //學生識別碼
        private string _Gender;
        private Boolean userChange;
        private string _Dept;
        private List<TuitionStandardRecord> _TuitionStandard; //全部收費標準
        public OpenMode OpenMode
        {
            get { return this.openMode; }
            set
            {
                this.openMode = value;
            }
        }
        public StudentTuitionProcess()
        {
            InitializeComponent();
           
            Program.TuitionChanged += delegate
            {
                if (this.TuitionRecord != null)
                {
                    AccessHelper accessHelper = new AccessHelper();
                    if (accessHelper.Select<StudentTuitionRecord>("UID=" + this.TuitionRecord.UID).Count>0)
                        FillStudentTuition(accessHelper.Select<StudentTuitionRecord>("UID=" + this.TuitionRecord.UID)[0]);
                }
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _Source = new List<TuitionDetailRecord>(_Source);
            _Source.Add(new TuitionDetailRecord());
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
            chkError();
            if (btnSave.Enabled == false)
            {
                MsgBox.Show("資料有錯，不能儲存"); 
                return;
            }           
            int ChangeMoney = 0;
            foreach (TuitionDetailRecord tsr in _Source)
            {
                if (tsr.TCSName == "")
                    tsr.Deleted = true;
                if (tsr.Deleted == false )
                {
                    tsr.SchoolYear = intSchoolYear.Value;
                    tsr.Semester = (cboSemester.Text == "上學期" ? 1 : 2);
                    ChangeMoney += tsr.ChangeAmount;
                    tsr.STUID = TuitionRecord.UID;
                }
                
            }
            //List<TuitionStandardRecord> TSRs = TuitionStandardDAO.GetTuitionStandardBySST(TuitionRecord.SchoolYear,cboSemester.Text,cboTSName.Text);

            if ((TuitionCharge + ChangeMoney) < 0)
                MessageBox.Show("應繳金額小於0，不能儲存");
            else
            {
               
                TuitionRecord.ChangeMoney = ChangeMoney;
                TuitionRecord.ChargeAmount = TuitionCharge + ChangeMoney;
                TuitionRecord.TSName = cboTSName.Text;
                TuitionRecord.UpLoad = false;
                
                TuitionRecord.Save();
                _Source.SaveAll();
                MessageBox.Show("儲存完成");
            }
            if (TuitionRecord.StudentType == "舊生")
                Program.OnTuitionChanged();
            //else
            //{
            //    NewStudent.Instance.Items[TuitionRecord.TuitionUID].Registered = (TuitionRecord.PayDate == null ? false : true);
            //    NewStudent.Instance.Items[TuitionRecord.TuitionUID].Save();
            //    NewStudent.Instance.SyncDataBackground(TuitionRecord.TuitionUID);
            //}
            //FillStudentTuition(TuitionRecord);
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
                    if (""+dataGridViewX1[2,i].Value=="" || (!int.TryParse(dataGridViewX1[2, i].Value.ToString(), out result)))
                    {
                        dataGridViewX1[2, i].ErrorText = "!";
                        dataGridViewX1.UpdateCellErrorText(2, i);
                        IsError = true;
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

            if (IsError)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要刪除繳費表嗎?","警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {                
                    foreach (TuitionDetailRecord tsr in _Source)
                        tsr.Deleted = true;
                    _Source.SaveAll();
                    TuitionRecord.Deleted = true;
                    TuitionRecord.Save();
                    if (TuitionRecord.StudentType == "舊生")
                        Program.OnTuitionChanged();
                    //else
                    //    NewStudent.Instance.SyncDataBackground(TuitionRecord.TuitionUID);
                    MessageBox.Show("繳費表已刪除");
                    Empty_Form();
                
            }
        }
        public void SetStudentTuitionRecord(StudentTuitionRecord sr, string Gender,string Dept)
        {
            this.TuitionRecord = sr;
            stUID = sr.TuitionUID;
            _Gender = Gender;
            _Dept = Dept;
            FillStudentTuition(sr);
        }
        private void FillStudentTuition(StudentTuitionRecord sr)
        {

            userChange = false;
            _Source = null;
            //string username = Framework.DSAServices.UserAccount;
            //List<string> allowusername = new List<string>(new string[] { "emywang", "bw2014", "mimimi" });
            if (CurrentUser.Acl["Tuition026"].Executable)                
                btnDelete.Visible = true;
            else
                btnDelete.Visible = false;
            cboTSName.Items.Clear();
            cboTSName.Items.Add("");
            foreach (TuitionStandardRecord ts in _TuitionStandard)
                if (!cboTSName.Items.Contains(ts.TSName))
                    if ((ts.Gender == "全" || ts.Gender == _Gender) && ts.Dept == _Dept)
                        cboTSName.Items.Add(ts.TSName);
            this.TuitionRecord = sr;
            if (!cboTSName.Items.Contains(sr.TSName))
                cboTSName.Items.Add(sr.TSName);
            cboTSName.Text = sr.TSName;
            txtChangeMoney.Text = sr.ChangeMoney.ToString();
            txtChargeAmount.Text = sr.ChargeAmount.ToString();
            intSchoolYear.Value = sr.SchoolYear;
            cboSemester.Text = (sr.Semester == 1 ? "上學期" : "下學期");
            
            cboTSName.Text = sr.TSName;
            _Source = TuitionDetailDAO.GetTuitionDetailByUID(sr.UID);
            dataGridViewX1.DataSource = _Source;
            TSRs = TuitionStandardDAO.GetTuitionStandardBySST(sr.SchoolYear, cboSemester.Text, cboTSName.Text);
            TuitionCharge = SetTuition.CalcTuition(TSRs);
            txtChangeMoney.Enabled = false;
            txtChargeAmount.Enabled = false;
            btnSave.Enabled = CurrentUser.Acl["Tuition024"].Editable;
           
            
            if (sr.PayDate != null)
            {
                cboTSName.Enabled = false;
               
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                if (CurrentUser.Acl["Tuition024"].Editable)
                {
                    cboTSName.Enabled = true;                                
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
            
            if (!CurrentUser.Acl["Tuition024"].Editable)
                foreach (DataGridViewColumn col in dataGridViewX1.Columns)
                    col.ReadOnly = true;
           
            
            userChange = true;
        }
        void cboSemester_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            RefreshStardand();
            List<StudentTuitionRecord> sr = StudentTuitionDAO.GetStudentTuitionBySSTS(intSchoolYear.Value, cboSemester.Text, (openMode == OpenMode.PermRec ? "舊生" : "新生"), stUID);
            if (sr.Count > 0)
                FillStudentTuition(sr[0]);
            else
                Empty_Form();

        }
        void intSchoolYear_ValueChanged(object sender, System.EventArgs e)
        {
            RefreshStardand();
            List<StudentTuitionRecord> sr = StudentTuitionDAO.GetStudentTuitionBySSTS(intSchoolYear.Value, cboSemester.Text, (openMode == OpenMode.PermRec ? "舊生" : "新生"), stUID);
            if (sr.Count > 0)
                FillStudentTuition(sr[0]);
            else
                Empty_Form();


        }
        public void Empty_Form()
        {
            //intSchoolYear.Value = GlobalValue.CurrentSchoolYear;
            //cboSemester.Text = GlobalValue.CurrentSemester;
            userChange = false;
            TuitionRecord = null;
            txtChangeMoney.Text = "";
            txtChargeAmount.Text = "";
           
            _Source = null;
            dataGridViewX1.DataSource = null;
            cboTSName.Text = "";
           
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnDelete.Visible = false;
            userChange = true;
            
        }
        void dataGridViewX1_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            int Money = 0;
            //自動計算異動項目金額
            if (_Source != null)
            {
                chkError();
                if (e.ColumnIndex == 1)
                {
                    if (("" + dataGridViewX1[e.ColumnIndex, e.RowIndex].Value) != "")
                    {
                        Money = SetTuition.CalcChangeMoneyByTCSName(TSRs, TuitionRecord.UID, dataGridViewX1[e.ColumnIndex, e.RowIndex].Value.ToString());
                        dataGridViewX1[2, e.RowIndex].Value = Money;
                    }
                }
                Money = 0;

                foreach (TuitionDetailRecord tdr in _Source)
                    if (tdr.Deleted == false)
                        Money += tdr.ChangeAmount;
                    
            }
            txtChangeMoney.Text = Money.ToString();
            txtChargeAmount.Text = (TuitionCharge + Money).ToString();
            if ((TuitionCharge + Money) < 0)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;


        }

        void cboTSName_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboTSName.Text == "" || TuitionRecord == null)
            {
                btnSave.Enabled = false;
                btnAdd.Enabled = false;
                return;
            }
            //更改收費標準，重新計算所有繳費資料
            //取得新的繳費表
            if (userChange)
            {
                if (TuitionRecord != null)
                {
                    TSRs = TuitionStandardDAO.GetTuitionStandardBySST(TuitionRecord.SchoolYear, (TuitionRecord.Semester == 1 ? "上學期" : "下學期"), cboTSName.Text);
                    //計算新的費用
                    TuitionCharge = SetTuition.CalcTuition(TSRs);
                }
                int Money = 0;
                int ChangeAmount = 0;
                //更新所有異動項目金額 
                if (_Source != null && _Source.Count != 0)
                {
                    foreach (TuitionDetailRecord tdr in _Source)
                        if (tdr.Deleted == false)
                        {
                            ChangeAmount = SetTuition.CalcChangeMoneyByTCSName(TSRs, TuitionRecord.UID, tdr.TCSName);
                            //異動金額計算結果為0時，不更正原金額,因金額是使用者輸入的
                            if (ChangeAmount != 0)
                                tdr.ChangeAmount = ChangeAmount;
                            Money += tdr.ChangeAmount;
                        }
                }
                //更改繳費金額
                txtChargeAmount.Text = (TuitionCharge + Money).ToString();
                txtChangeMoney.Text = Money.ToString();
                dataGridViewX1.Refresh();
                if ((TuitionCharge + Money) < 0)
                    btnSave.Enabled = false;
                else
                    btnSave.Enabled = true;
            }
        }
       
        //避免輸入不當,造成當機
        private void dataGridViewX1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            if (e.Exception != null)           
                dataGridViewX1[e.ColumnIndex, e.RowIndex].ErrorText = e.Exception.Message;

            dataGridViewX1.UpdateCellErrorText(e.ColumnIndex, e.RowIndex);
            dataGridViewX1[e.ColumnIndex, e.RowIndex].Value = null;
            
        }

        private void StudentTuitionProcess_Load(object sender, EventArgs e)
        {

            RefreshStardand();
        }
        private void RefreshStardand()
        {
            List<TuitionChangeStdRecord> _TuitionChange = new List<TuitionChangeStdRecord>();
            _TuitionChange = TuitionChangeStdDAO.GetTuitionChangeStdBySS(intSchoolYear.Value, cboSemester.Text);
            if (TCSName.Items == null)
               TCSName.Items.Clear();
            TCSName.Items.Add("");
            foreach (TuitionChangeStdRecord cr in _TuitionChange)
                if (!TCSName.Items.Contains(cr.TCSName))
                    TCSName.Items.Add(cr.TCSName);

            _TuitionStandard = new List<TuitionStandardRecord>();

            _TuitionStandard = TuitionStandardDAO.GetTuitionStandardBySS(intSchoolYear.Value, cboSemester.Text);
        }
    }
}
