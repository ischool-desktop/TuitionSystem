using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
//using AccountsReceivable.API;
using TuitionSystem.Data;
//using MySchoolModule;

//using AccountsReceivable.ReceiptUtility;
using FISCA.Presentation;
using K12.Data;
using SHSchool.Data;
using Aspose.Cells;

namespace TuitionSystem
{
    public partial class PaymentList : BaseForm
    {
        private int ReportKind;
        public PaymentList(int Kind)
        {
            ReportKind = Kind;
            InitializeComponent();
        }

        private void PaymentList_Load(object sender, EventArgs e)
        {
            intSchoolYear.Value = GlobalValue.CurrentSchoolYear;
            cboSemester.Text = GlobalValue.CurrentSemester;

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (cboSemester.Text == "" || intSchoolYear.Value == 0 )
            {
                MessageBox.Show("資料設定不齊全");
                return;
            }

            List<string> IDs = new List<string>();
            int nowSet = 0;
            //收費標準集合,收費標準名稱為key
            Dictionary<string, List<TuitionStandardRecord>> tsrList = new Dictionary<string, List<TuitionStandardRecord>>();
            //異動明細集合，繳費表為key
            Dictionary<string, List<TuitionDetailRecord>> tdrList = new Dictionary<string, List<TuitionDetailRecord>>();
            //繳費表集合，學生編號為key
            Dictionary<string, StudentTuitionRecord> studentTus = new Dictionary<string, StudentTuitionRecord>();
            //異動明細
            List<TuitionDetailRecord> tdr = new List<TuitionDetailRecord>();
            //收費標準
            List<TuitionStandardRecord> std = new List<TuitionStandardRecord>();
            //繳費表
            List<StudentTuitionRecord> studentTu = new List<StudentTuitionRecord>();
            std = TuitionStandardDAO.GetTuitionStandardBySS(intSchoolYear.Value, cboSemester.Text);
            //可以列印的學生編號
            List<string> PrintIDs = new List<string>();
             //全部收費標準
            foreach (TuitionStandardRecord sd in std)
                if (tsrList.ContainsKey(sd.TSName))
                    tsrList[sd.TSName].Add(sd);
                else
                {
                    tsrList.Add(sd.TSName, new List<TuitionStandardRecord>());
                    tsrList[sd.TSName].Add(sd);
                }

            if (ReportKind == 1)
            {
                //舊生                     
                List<SHStudentRecord> Students = SHStudent.SelectByIDs(K12.Presentation.NLDPanels.Student.SelectedSource);
                foreach (SHStudentRecord sr in Students)
                    if (sr.Status == SHStudentRecord.StudentStatus.一般)
                        IDs.Add(sr.ID);
                studentTu = StudentTuitionDAO.GetStudentTuitionBySSTSL(intSchoolYear.Value, cboSemester.Text, "舊生", IDs);
                IDs.Clear();
                if (studentTu.Count == 0)
                {
                    MsgBox.Show("未設定收費標準");
                    return;
                }
                foreach (StudentTuitionRecord sr in studentTu)
                {
                    if (!studentTus.ContainsKey(sr.TuitionUID))
                        studentTus.Add(sr.TuitionUID, sr);

                    IDs.Add(sr.UID);
                }
                tdr = TuitionDetailDAO.GetTuitionDetailByIDs(IDs);
                //全部異動明細
                foreach (TuitionDetailRecord td in tdr)
                {
                    if (tdrList.ContainsKey(td.STUID))
                        tdrList[td.STUID].Add(td);
                    else
                    {
                        tdrList.Add(td.STUID, new List<TuitionDetailRecord>());
                        tdrList[td.STUID].Add(td);
                    }

                }
                IDs.Clear();
                //依學號排序
                Students.Sort(CompareStudentNumber);
                PrintIDs.Clear();               
                foreach (SHStudentRecord sr in Students)
                {
                    if (studentTus.ContainsKey(sr.ID))
                    {

                       if (!tsrList.ContainsKey(studentTus[sr.ID].TSName))
                            {
                                MsgBox.Show(string.Format("系統中{0}收費標準不存在，無法列印。", sr.Class.Name + sr.StudentNumber + sr.Name));
                                continue;
                            }
                       if (!tdrList.ContainsKey(studentTus[sr.ID].UID))
                           tdrList.Add(studentTus[sr.ID].UID, new List<TuitionDetailRecord>());
                       PrintIDs.Add(sr.ID);
                    }
                    else
                        if (sr.Status == SHStudentRecord.StudentStatus.一般)
                            MsgBox.Show(string.Format("系統中{0}沒有繳費表資料。", sr.Class.Name + sr.StudentNumber + sr.Name));
                }
                   //填入全部學生之PaymentDetail
                
                Workbook wb = new Workbook();
                Style defaultStyle = wb.DefaultStyle;
                defaultStyle.Font.Name = "標楷體";
                defaultStyle.Font.Size = 10;
                wb.DefaultStyle = defaultStyle;
                int row = 1;
                wb.Worksheets[0].Cells[0, 0].PutValue("班級");
                wb.Worksheets[0].Cells[0, 1].PutValue("學號");
                wb.Worksheets[0].Cells[0, 2].PutValue("姓名");
                wb.Worksheets[0].Cells[0, 3].PutValue("性別");
                wb.Worksheets[0].Cells[0, 4].PutValue("收費標準");
                int col = 5;
                int itemID = 1;
                int MaxCol = 0;
                foreach (string StuID in PrintIDs)
                {
                    MotherForm.SetStatusBarMessage("正在產生資料", nowSet++ * 100 / PrintIDs.Count);
                    Application.DoEvents();                   
                    SHStudentRecord Stud = SHStudent.SelectByID(studentTus[StuID].TuitionUID);
                    wb.Worksheets[0].Cells[row, 0].PutValue(Stud.Class.Name);
                    wb.Worksheets[0].Cells[row, 1].PutValue(Stud.StudentNumber);
                    wb.Worksheets[0].Cells[row, 2].PutValue(Stud.Name);
                    wb.Worksheets[0].Cells[row, 3].PutValue(Stud.Gender);
                    wb.Worksheets[0].Cells[row, 4].PutValue(studentTus[StuID].TSName);
                    col = 5;
                    itemID = 1;
                    foreach (TuitionStandardRecord tr in tsrList[studentTus[StuID].TSName])
                    {
                        wb.Worksheets[0].Cells[0, col].PutValue("收費項目"+itemID);
                        wb.Worksheets[0].Cells[0, col + 1].PutValue("收費金額" + itemID);
                        wb.Worksheets[0].Cells[row, col].PutValue(tr.ChargeItem);
                        wb.Worksheets[0].Cells[row, col+1].PutValue(tr.Money.ToString());
                        itemID += 1;
                        col +=2;
                    }
                    if (MaxCol<itemID)
                        MaxCol=itemID;
                    row += 1;
                }
                row = 1;
                
                foreach (string StuID in PrintIDs)
                {
                    itemID = 1;
                    col = 3 + MaxCol * 2;                   
                    foreach (TuitionDetailRecord td in tdrList[studentTus[StuID].UID])
                    {
                        wb.Worksheets[0].Cells[0, col].PutValue("異動項目" + itemID);
                        wb.Worksheets[0].Cells[0, col + 1].PutValue("異動金額" + itemID);
                        wb.Worksheets[0].Cells[row, col].PutValue(td.TCSName);
                        wb.Worksheets[0].Cells[row, col + 1].PutValue(td.ChangeAmount.ToString());
                        itemID += 1;
                        col += 2;
                    }
                    row += 1;
                }
                try
                {
                    wb.Save(Application.StartupPath + "\\Reports\\繳費明細資料.xls", FileFormatType.Excel2003);
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\繳費明細資料.xls");
                }
                catch
                {
                    System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                    sd1.Title = "另存新檔";
                    sd1.FileName = "繳費明細資料.xls";
                    sd1.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                    if (sd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            wb.Save(sd1.FileName, FileFormatType.Excel2003);
                            System.Diagnostics.Process.Start(sd1.FileName);
                        }
                        catch
                        {
                            System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
        }
        //依Number排序副程式
        static int CompareStudentNumber(StudentRecord a, StudentRecord b)
        {
            int c, d;
            if (int.TryParse(a.StudentNumber, out c) && int.TryParse(b.StudentNumber, out d))
                return int.Parse(a.StudentNumber).CompareTo(int.Parse(b.StudentNumber));
            else
                if (!int.TryParse(a.StudentNumber, out c) && !int.TryParse(b.StudentNumber, out d))
                    return 0.CompareTo(0);
                else
                    if (int.TryParse(a.StudentNumber, out c))
                        return int.Parse(a.StudentNumber).CompareTo(0);
                    else
                        return 0.CompareTo(int.Parse(b.StudentNumber));
        }
    }
}
