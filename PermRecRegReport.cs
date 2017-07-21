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

using Aspose.Cells;
//using MySchoolModule;
using FISCA.Presentation;
using K12.Data;
using SHSchool.Data;
namespace TuitionSystem
{
    public partial class PermRecRegReport : BaseForm
    {
        private int ReportKind;
        public PermRecRegReport(int Kind)
        {
            ReportKind = Kind;
            InitializeComponent();
        }

        private void PermRecRegReport_Load(object sender, EventArgs e)
        {
            StartDate.IsEmpty = true;
            EndDate.IsEmpty = true;
            intSchoolYear.Value = GlobalValue.CurrentSchoolYear;
            cboSemester.Text = GlobalValue.CurrentSemester;
            StartDate.Enabled = true;
            EndDate.Enabled = true;
            if (ReportKind == 2)
            {
                chkPrint.Visible = true;
                chkCash.Visible = true;
                chkNotInclude.Visible = true;
            }
            else
            {
                chkPrint.Visible = false;
                chkCash.Visible = false;
                chkNotInclude.Visible = false;
            }
            switch (ReportKind)
            {
                case 1:
                    this.Text = "繳費項目明細清冊列印";
                    break;
                case 2:
                    this.Text = "各項繳費及異動項目統計列印";
                    break;
                case 3:                    
                    this.Text = "註冊及未註冊人數暨註冊金額統計表";
                    break;
                case 4:
                    this.Text = "應收金額與實收金額不符者名冊列印";
                    break;
                case 5:
                    StartDate.Enabled = false;
                    EndDate.Enabled = false;
                    this.Text = "應收金額小於零名冊列印";
                    break;
                case 6:
                    StartDate.Enabled = false;
                    EndDate.Enabled = false;
                    this.Text = "未註冊學生名單列印";
                    break;
            }
        }
        
        //依Number排序副程式
        static int CompareNumber(StudentRecord a, StudentRecord b)
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
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (!StartDate.IsEmpty)
                if (StartDate.Value > EndDate.Value)
                {
                    MessageBox.Show("日期錯誤");
                    return;
                }
            switch (ReportKind)
            {
                case 1:
                    TuitionDetailList();
                    break;
                case 2:
                    ChargeItemStatics();
                    break;
                case 3:
                    RegisterCalc();
                    break;
                case 4:
                    AmountError(1);
                    break;
                case 5:
                    AmountError(2);
                    break;
                case 6:
                    NonRegistered();
                    break;
            }

        }

        private void NonRegistered()
        {
            Workbook wb = new Workbook();
            Style defaultStyle = wb.DefaultStyle;
            defaultStyle.Font.Name = "標楷體";
            defaultStyle.Font.Size = 10;
            wb.DefaultStyle = defaultStyle;
            Style st2 = wb.Styles[wb.Styles.Add()];
            StyleFlag sf2 = new StyleFlag();
            sf2.Borders = true;

            st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            int tmpMaxRow = 0, tmpMaxCol = 0;
            //學生ID
            List<string> StudentID = new List<string>();
            //繳費表
            List<StudentTuitionRecord> StuTuRs = new List<StudentTuitionRecord>();
            //班級
            List<ClassRecord> ClassRs = new List<ClassRecord>();
            //所選班級
            ClassRs = K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource);
            int nowSet = 0;
            //找出全部學生ID
            foreach (ClassRecord cr in ClassRs)
                foreach (StudentRecord sr in cr.Students)
                    StudentID.Add(sr.ID);
            //找出全部繳費表
            StuTuRs = StudentTuitionDAO.GetStudentTuitionBySSTSL(intSchoolYear.Value, cboSemester.Text, "舊生", StudentID);
            wb.Worksheets[0].Cells[0, 0].PutValue("班級");
            wb.Worksheets[0].Cells[0, 1].PutValue("學號");
            wb.Worksheets[0].Cells[0, 2].PutValue("姓名");
            wb.Worksheets[0].Cells[0, 3].PutValue("住址");
            wb.Worksheets[0].Cells[0, 4].PutValue("電話");
            wb.Worksheets[0].Cells[0, 5].PutValue("家長姓名");
            int row = 1;
            SHAddressRecord AddressR = new SHAddressRecord();
            SHParentRecord ParentR = new SHParentRecord();
            SHPhoneRecord PhoneR = new SHPhoneRecord();
            string Address = "";
            string parent_s = "";
            string phone;
            foreach (StudentTuitionRecord sr in StuTuRs)
            {
                MotherForm.SetStatusBarMessage("正在產生報表", nowSet++ * 100 / StuTuRs.Count);
                if (sr.PayDate == null && K12.Data.Student.SelectByID(sr.TuitionUID).Status==K12.Data.StudentRecord.StudentStatus.一般)
                {
                    wb.Worksheets[0].Cells[row, 0].PutValue(K12.Data.Student.SelectByID(sr.TuitionUID).Class.Name);
                    wb.Worksheets[0].Cells[row, 1].PutValue(K12.Data.Student.SelectByID(sr.TuitionUID).StudentNumber);
                    wb.Worksheets[0].Cells[row, 2].PutValue(K12.Data.Student.SelectByID(sr.TuitionUID).Name);                    
                    AddressR = SHAddress.SelectByStudentID(sr.TuitionUID);
                    Address = "";
                    if (AddressR != null)
                    {
                        if (AddressR.Mailing.ToString() != "")
                            Address = AddressR.Mailing.ToString();
                        else
                            Address = AddressR.Permanent.ToString();
                    }
                    ParentR = SHParent.SelectByStudentID(sr.TuitionUID);
                    parent_s = "";
                    if (ParentR != null)
                    {
                        if (ParentR.Custodian.Name != "")
                            parent_s = ParentR.Custodian.Name;
                        else
                            if (ParentR.Father.Name != "")
                                parent_s = ParentR.Father.Name;
                            else
                                if (ParentR.Mother.Name != "")
                                    parent_s = ParentR.Mother.Name;                            
                    }
                    PhoneR = SHPhone.SelectByStudentID(sr.TuitionUID);
                    phone = "";
                    if (PhoneR != null)
                    {
                        if (PhoneR.Contact != "")
                            phone = PhoneR.Contact;
                        else
                            if (PhoneR.Cell != "")
                                phone = PhoneR.Cell;
                            else
                                if (PhoneR.Permanent != "")
                                    phone = PhoneR.Permanent;
                    }
                    wb.Worksheets[0].Cells[row, 3].PutValue(Address);
                    wb.Worksheets[0].Cells[row, 4].PutValue(phone);
                    wb.Worksheets[0].Cells[row, 5].PutValue(parent_s);
                    row++;
                }
            }
            tmpMaxRow = wb.Worksheets[0].Cells.MaxDataRow;
            tmpMaxCol = wb.Worksheets[0].Cells.MaxDataColumn + 1;
            if (tmpMaxRow>=1) 
                wb.Worksheets[0].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);          
            
            //設定紙張大小
            wb.Worksheets[0].PageSetup.PaperSize = PaperSizeType.PaperA4;
            //設定紙張方向
            wb.Worksheets[0].PageSetup.Orientation = PageOrientationType.Portrait;
            //自動調整欄寬
            wb.Worksheets[0].AutoFitColumns();
            //自動調整列高
            wb.Worksheets[0].AutoFitRows();           
            //設定邊界
            wb.Worksheets[0].PageSetup.BottomMargin = 1.5;
            wb.Worksheets[0].PageSetup.TopMargin = 1.5;
            wb.Worksheets[0].PageSetup.HeaderMargin = 1;
            wb.Worksheets[0].PageSetup.LeftMargin = 1;
            wb.Worksheets[0].PageSetup.RightMargin = 1;
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\未註冊學生名冊.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\未註冊學生名冊.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "未註冊學生名冊.xls";
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
            MotherForm.SetStatusBarMessage("");
        
        }
        private void TuitionDetailList()
        {
            Workbook wb = new Workbook();
            Style defaultStyle = wb.DefaultStyle;
            defaultStyle.Font.Name = "標楷體";
            defaultStyle.Font.Size = 10;
            wb.DefaultStyle = defaultStyle;
            Style st2 = wb.Styles[wb.Styles.Add()];
            StyleFlag sf2 = new StyleFlag();
            sf2.Borders = true;

            st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            int tmpMaxRow = 0, tmpMaxCol = 0;
            int row = 2;
            int col = 3;
            int TitleCol=3;
            int SheetCount = 1;
            int Money = 0;
            //找出標題
            List<string> PrintTitle = new List<string>();
            //繳費表
            List<StudentTuitionRecord> StuTuRs = new List<StudentTuitionRecord>();
            //班級
            List<ClassRecord> ClassRs = new List<ClassRecord>();
            //學生
            List<StudentRecord> StudentRs = new List<StudentRecord>();
            //所選班級
            ClassRs = K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource);            
            //收費標準
            List<TuitionStandardRecord> TuitionStd = new List<TuitionStandardRecord>();
            //學生ID
            List<string> StudentID=new List<string>();
            //每班所有收費標準
            Dictionary<string, List<TuitionStandardRecord>> TuitionStds = new Dictionary<string, List<TuitionStandardRecord>>();
            //每班所有學生繳費表
            Dictionary<string, List<StudentTuitionRecord>> StudentTus = new Dictionary<string, List<StudentTuitionRecord>>();           
            //異動明細
            List<TuitionDetailRecord> tdr = new List<TuitionDetailRecord>();
            //每科所有學生異動明細
            Dictionary<string, List<TuitionDetailRecord>> tdrList = new Dictionary<string, List<TuitionDetailRecord>>();
            
            //所有繳費表ID
            List<string> strIDs = new List<string>();
            int nowSet = 0;
            //找出全部學生ID
            foreach (ClassRecord cr in ClassRs)
                foreach (StudentRecord sr in cr.Students)
                    StudentID.Add(sr.ID);
            //找出全部繳費表
            StuTuRs = StudentTuitionDAO.GetStudentTuitionBySSTSL(intSchoolYear.Value, cboSemester.Text, "舊生", StudentID);
            foreach (StudentTuitionRecord sr in StuTuRs)
            {
                if (!StudentTus.ContainsKey(sr.TuitionUID))
                {
                    StudentTus.Add(sr.TuitionUID, new List<StudentTuitionRecord>());
                    StudentTus[sr.TuitionUID].Add(sr);
                }
                strIDs.Add(sr.UID);                
            }
            tdr = TuitionDetailDAO.GetTuitionDetailByIDs(strIDs);
            foreach (TuitionDetailRecord tr in tdr)
            {
                if (tdrList.ContainsKey(tr.STUID))
                    tdrList[tr.STUID].Add(tr);
                else
                {
                    tdrList.Add(tr.STUID, new List<TuitionDetailRecord>());
                    tdrList[tr.STUID].Add(tr);
                }
            }
            TuitionStd = TuitionStandardDAO.GetTuitionStandardBySS(intSchoolYear.Value, cboSemester.Text);
            foreach (TuitionStandardRecord ts in TuitionStd)
            {
                if (TuitionStds.ContainsKey(ts.TSName))
                    TuitionStds[ts.TSName].Add(ts);
                else
                {
                    TuitionStds.Add(ts.TSName, new List<TuitionStandardRecord>());
                    TuitionStds[ts.TSName].Add(ts);
                }                
            }
            foreach (ClassRecord cr in ClassRs)
            {
                MotherForm.SetStatusBarMessage("正在產生報表", nowSet++*100 / ClassRs.Count );                
                StudentRs = cr.Students;
                StudentRs.Sort(CompareNumber);                
                row = 2;
                //找出所有收費標準的繳費項目 
                PrintTitle.Clear();                
                foreach (StudentRecord sr in StudentRs)
                {
                    if (StudentTus.ContainsKey(sr.ID))
                    {
                        if (TuitionStds.ContainsKey(StudentTus[sr.ID][0].TSName))
                        {
                            foreach (TuitionStandardRecord tr in TuitionStds[StudentTus[sr.ID][0].TSName])
                                if (!PrintTitle.Contains(tr.ChargeItem))
                                    PrintTitle.Add(tr.ChargeItem);
                        }
                    }
                }                    
                
                wb.Worksheets.Add();
                wb.Worksheets[SheetCount].Name = cr.Name;
                if (StartDate.IsEmpty)
                    wb.Worksheets[SheetCount].Cells[0, 0].PutValue(K12.Data.School.ChineseName + intSchoolYear.Value + "學年度" + cboSemester.Text  +cr.Name + "繳費明細清冊");
                else
                    wb.Worksheets[SheetCount].Cells[0, 0].PutValue(K12.Data.School.ChineseName + intSchoolYear.Value + "學年度" + cboSemester.Text + cr.Name + "繳費明細清冊" + "\n" + "日期區間" + StartDate.Value.ToLongDateString() + "至" + EndDate.Value.ToLongDateString());
                wb.Worksheets[SheetCount].Cells[0, 0].Style.IsTextWrapped = true;
                wb.Worksheets[SheetCount].Cells[0, 0].Style.Font.Size = 18;
                wb.Worksheets[SheetCount].Cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                wb.Worksheets[SheetCount].Cells[0, 0].Style.VerticalAlignment = TextAlignmentType.Center;
                if (StartDate.IsEmpty)
                    wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 25);
                else
                    wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 50);
                wb.Worksheets[SheetCount].Cells[1, 0].PutValue("學號");
                wb.Worksheets[SheetCount].Cells[1, 1].PutValue("姓名");
                wb.Worksheets[SheetCount].Cells[1, 2].PutValue("班級座號");
                wb.Worksheets[SheetCount].Cells[1, 3].PutValue("性別");
                TitleCol = 4;
                //放置繳費項目
                foreach (string ChargeItem in PrintTitle)
                {
                    if (ChargeItem.Length > 4)
                    {
                        int len=ChargeItem.Length/ 2;
                        wb.Worksheets[SheetCount].Cells[1, TitleCol].PutValue(ChargeItem.Substring(0, len) + "\n" + ChargeItem.Substring(len, ChargeItem.Length - len));
                        wb.Worksheets[SheetCount].Cells[1, TitleCol].Style.IsTextWrapped = true;
                    }
                    else
                        wb.Worksheets[SheetCount].Cells[1, TitleCol].PutValue(ChargeItem);                   
                    TitleCol++;
                }

                wb.Worksheets[SheetCount].Cells[1, TitleCol].PutValue("總計金額");
                wb.Worksheets[SheetCount].Cells[1, TitleCol+1].PutValue("異動金額");
                wb.Worksheets[SheetCount].Cells[1, TitleCol+2].PutValue("應收金額");
                wb.Worksheets[SheetCount].Cells[1, TitleCol+3].PutValue("實收金額");
                wb.Worksheets[SheetCount].Cells[1, TitleCol+4].PutValue("繳費日期");
                wb.Worksheets[SheetCount].Cells[1, TitleCol+5].PutValue("備    註");
                for (int i = 0; i < TitleCol + 6; i++)
                {
                    wb.Worksheets[SheetCount].Cells[1, i].Style.HorizontalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[SheetCount].Cells[1, i].Style.VerticalAlignment = TextAlignmentType.Center;
                }
                //合併儲存格
                wb.Worksheets[SheetCount].Cells.Merge(0, 0, 1, TitleCol + 6);
                foreach (StudentRecord sr in StudentRs)
                {
                    if (StudentTus.ContainsKey(sr.ID))
                    {
                        if (StartDate.IsEmpty || (StudentTus[sr.ID][0].PayDate >= StartDate.Value && StudentTus[sr.ID][0].PayDate <= EndDate.Value))
                        {
                            wb.Worksheets[SheetCount].Cells.Rows[row].Style.VerticalAlignment = TextAlignmentType.Center;
                            wb.Worksheets[SheetCount].Cells[row, 0].PutValue((sr.Status== StudentRecord.StudentStatus.一般? "":"*")+sr.StudentNumber);
                            wb.Worksheets[SheetCount].Cells[row, 1].PutValue(sr.Name);
                            wb.Worksheets[SheetCount].Cells[row, 2].PutValue(sr.Class.Name+(sr.SeatNo<10? "0":"")+sr.SeatNo.ToString());
                            wb.Worksheets[SheetCount].Cells[row, 3].PutValue(sr.Gender);
                            Money = 0;
                            if (TuitionStds.ContainsKey(StudentTus[sr.ID][0].TSName))
                            {
                                foreach (TuitionStandardRecord tr in TuitionStds[StudentTus[sr.ID][0].TSName])
                                {
                                    col = PrintTitle.IndexOf(tr.ChargeItem) + 4;
                                    wb.Worksheets[SheetCount].Cells[row, col].PutValue(tr.Money);
                                    wb.Worksheets[SheetCount].Cells[row, col].Style.Custom = "#,##0_ ";
                                    Money += tr.Money;
                                }
                            }
                            wb.Worksheets[SheetCount].Cells[row, TitleCol].PutValue(Money);
                            wb.Worksheets[SheetCount].Cells[row, TitleCol].Style.Custom = "#,##0_ ";
                            wb.Worksheets[SheetCount].Cells[row, TitleCol + 1].PutValue(StudentTus[sr.ID][0].ChangeMoney);
                            wb.Worksheets[SheetCount].Cells[row, TitleCol + 1].Style.Custom = "#,##0_ ";
                            wb.Worksheets[SheetCount].Cells[row, TitleCol + 2].PutValue(StudentTus[sr.ID][0].ChargeAmount);
                            wb.Worksheets[SheetCount].Cells[row, TitleCol + 2].Style.Custom = "#,##0_ ";
                            wb.Worksheets[SheetCount].Cells[row, TitleCol + 3].PutValue(StudentTus[sr.ID][0].Payment);
                            wb.Worksheets[SheetCount].Cells[row, TitleCol + 3].Style.Custom = "#,##0_ ";
                            if (StudentTus[sr.ID][0].PayDate != null)
                                wb.Worksheets[SheetCount].Cells[row, TitleCol + 4].PutValue(StudentTus[sr.ID][0].PayDate.Value.Year - 1911 + "." + StudentTus[sr.ID][0].PayDate.Value.Month + "." + StudentTus[sr.ID][0].PayDate.Value.Day);
                            //if (StudentTus[sr.ID][0].ChangeMoney != 0)
                            //{                                
                                string strChange = "";
                                if (tdrList.ContainsKey(StudentTus[sr.ID][0].UID))
                                {
                                    foreach (TuitionDetailRecord tr in tdrList[StudentTus[sr.ID][0].UID])
                                        strChange += tr.TCSName + "：" + tr.ChangeAmount + "\n";
                                }
                                if (strChange != "")
                                {
                                    wb.Worksheets[SheetCount].Cells[row, TitleCol + 5].PutValue(strChange.Substring(0, strChange.Length - 1));
                                    wb.Worksheets[SheetCount].Cells[row, TitleCol + 5].Style.ShrinkToFit = true;
                                    wb.Worksheets[SheetCount].Cells[row, TitleCol + 5].Style.IsTextWrapped = true;
                                    wb.Worksheets[SheetCount].Cells[row, TitleCol + 5].Style.Font.Size = 8;
                                }
                            //}                            
                            row++;
                        }
                    }
                }
                if (row > 2)
                {
                    wb.Worksheets[SheetCount].Cells[row, 0].PutValue("總    計");
                    wb.Worksheets[SheetCount].Cells[row, 2].PutValue(row - 2 + "人");
                    wb.Worksheets[SheetCount].Cells.Merge(row, 0, 1, 2);
                    for (int i = 3; i < TitleCol + 4; i++)
                    {
                        wb.Worksheets[SheetCount].Cells[row, i].Formula = "=SUM(" + chr(i + 1) + "3" + ":" + chr(i + 1) + (row) + ")";
                        wb.Worksheets[SheetCount].Cells[row, i].Style.Custom = "#,##0_ ";
                    }
                }
                tmpMaxRow = wb.Worksheets[SheetCount].Cells.MaxDataRow;
                tmpMaxCol = wb.Worksheets[SheetCount].Cells.MaxDataColumn + 1;
                if (tmpMaxRow >= 1) 
                   wb.Worksheets[SheetCount].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
                
                
                //設定紙張大小
                wb.Worksheets[SheetCount].PageSetup.PaperSize = PaperSizeType.PaperB4;
                //設定紙張方向
                wb.Worksheets[SheetCount].PageSetup.Orientation = PageOrientationType.Landscape;
                //自動調整欄寬
                wb.Worksheets[SheetCount].AutoFitColumns();
                //自動調整列高
                wb.Worksheets[SheetCount].AutoFitRows();
                //設定備註欄寬
                wb.Worksheets[SheetCount].Cells.SetColumnWidth(TitleCol + 5, 20);
                wb.Worksheets[SheetCount].PageSetup.PrintTitleRows = "$1:$2"; //設定跨頁標題
                //設定邊界
                wb.Worksheets[SheetCount].PageSetup.BottomMargin = 1.5;
                wb.Worksheets[SheetCount].PageSetup.TopMargin = 1.5;
                wb.Worksheets[SheetCount].PageSetup.HeaderMargin = 1;
                wb.Worksheets[SheetCount].PageSetup.LeftMargin = 1;
                wb.Worksheets[SheetCount].PageSetup.RightMargin = 1;
                SheetCount++;
            }
            
            //刪除第一張工作表
            if (wb.Worksheets.Count > 1)
                wb.Worksheets.RemoveAt(0);
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\繳費項目明細清冊.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\繳費項目明細清冊.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "繳費項目明細清冊.xls";
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
            MotherForm.SetStatusBarMessage("");
        }
        private void ChargeItemStatics()
        {
            Workbook wb = new Workbook();
            Style defaultStyle = wb.DefaultStyle;
            defaultStyle.Font.Name = "標楷體";
            defaultStyle.Font.Size = 14;
            wb.DefaultStyle = defaultStyle;
            Style st2 = wb.Styles[wb.Styles.Add()];
            StyleFlag sf2 = new StyleFlag();
            sf2.Borders = true;

            st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            int tmpMaxRow = 0, tmpMaxCol = 0;
            int row = 2;
            int col = 3;            
            int SheetCount = 1;
            
            //繳費表
            List<StudentTuitionRecord> StuTuRs = new List<StudentTuitionRecord>();
            //班級
            List<ClassRecord> ClassRs = new List<ClassRecord>();           
            //所選班級
            ClassRs = K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource);           
            //收費標準
            List<TuitionStandardRecord> TuitionStd = new List<TuitionStandardRecord>();
            //學生ID
            List<string> StudentID = new List<string>();
            //所有收費標準
            Dictionary<string, List<TuitionStandardRecord>> TuitionStds = new Dictionary<string, List<TuitionStandardRecord>>();
            //所有學生繳費表
            Dictionary<string, List<StudentTuitionRecord>> StudentTus = new Dictionary<string, List<StudentTuitionRecord>>();
           
            //異動明細
            List<TuitionDetailRecord> tdr = new List<TuitionDetailRecord>();
            //繳費各項細目統計
            Dictionary<string, Dictionary<int, int>> ItemStatic = new Dictionary<string, Dictionary<int, int>>();
            StuTuRs.Clear();
            StudentID.Clear();
            TuitionStds.Clear();
            StudentTus.Clear(); 
            //找出全班學生ID
            foreach (ClassRecord cr in ClassRs)                  
                foreach (StudentRecord sr in cr.Students)
                    StudentID.Add(sr.ID);
            //找出全部繳費表
            StuTuRs = StudentTuitionDAO.GetStudentTuitionBySSTSL(intSchoolYear.Value, cboSemester.Text, "舊生", StudentID);
          
            List<string> IDKeys = new List<string>();
            //找出所有收費標準
            List<TuitionStandardRecord> TuitionSRs = TuitionStandardDAO.GetTuitionStandardBySS(intSchoolYear.Value, cboSemester.Text);
            foreach (TuitionStandardRecord sr in TuitionSRs)
            {
                if (!TuitionStds.ContainsKey(sr.TSName))
                    TuitionStds.Add(sr.TSName, new List<TuitionStandardRecord>());
                TuitionStds[sr.TSName].Add(sr);
            }
            IDKeys.Clear();
            ItemStatic.Add("總計金額", new Dictionary<int, int>());
            ItemStatic.Add("應收金額", new Dictionary<int, int>());
            ItemStatic.Add("異動金額", new Dictionary<int, int>());
            ItemStatic.Add("實收金額", new Dictionary<int, int>());
            foreach (StudentTuitionRecord sr in StuTuRs)
            {
                if (sr.PayDate != null && (StartDate.IsEmpty || (sr.PayDate >= StartDate.Value && sr.PayDate <= EndDate.Value)))
                {
                    if ((chkNotInclude.Checked == true && sr.PayLocation != "出納組") || (chkCash.Checked == true && sr.PayLocation == "出納組") || (chkCash.Checked==false && chkNotInclude.Checked==false))
                    {
                        if (!IDKeys.Contains(sr.UID))
                            IDKeys.Add(sr.UID);
                        if (TuitionStds.ContainsKey(sr.TSName))
                        {
                            foreach (TuitionStandardRecord tr in TuitionStds[sr.TSName])
                            {
                                if (!ItemStatic.ContainsKey(tr.ChargeItem))
                                    ItemStatic.Add(tr.ChargeItem, new Dictionary<int, int>());
                                if (!ItemStatic[tr.ChargeItem].ContainsKey(tr.Money))
                                    ItemStatic[tr.ChargeItem].Add(tr.Money, 0);
                                ItemStatic[tr.ChargeItem][tr.Money]++;
                            }
                        }
                        if (!ItemStatic["總計金額"].ContainsKey(sr.ChargeAmount - sr.ChangeMoney))
                            ItemStatic["總計金額"].Add(sr.ChargeAmount - sr.ChangeMoney, 0);
                        ItemStatic["總計金額"][sr.ChargeAmount - sr.ChangeMoney]++;
                        if (!ItemStatic["應收金額"].ContainsKey(sr.ChargeAmount))
                            ItemStatic["應收金額"].Add(sr.ChargeAmount, 0);
                        ItemStatic["應收金額"][sr.ChargeAmount]++;
                        if (!ItemStatic["異動金額"].ContainsKey(sr.ChangeMoney))
                            ItemStatic["異動金額"].Add(sr.ChangeMoney, 0);
                        ItemStatic["異動金額"][sr.ChangeMoney]++;
                        if (!ItemStatic["實收金額"].ContainsKey(sr.Payment))
                            ItemStatic["實收金額"].Add(sr.Payment, 0);
                        ItemStatic["實收金額"][sr.Payment]++;
                    }
                }
            }
            tdr = TuitionDetailDAO.GetTuitionDetailByIDs(IDKeys);
            foreach (TuitionDetailRecord tr in tdr)
            {
                if (!ItemStatic.ContainsKey(tr.TCSName))
                    ItemStatic.Add(tr.TCSName, new Dictionary<int, int>());
                if (!ItemStatic[tr.TCSName].ContainsKey(tr.ChangeAmount))
                    ItemStatic[tr.TCSName].Add(tr.ChangeAmount, 0);
                ItemStatic[tr.TCSName][tr.ChangeAmount]++;
            } 
            if (chkPrint.Checked)
            {
                foreach (string ItemName in ItemStatic.Keys)
                {
                    wb.Worksheets.Add();
                    wb.Worksheets[SheetCount].Name = ItemName;
                    if (StartDate.IsEmpty)
                        wb.Worksheets[SheetCount].Cells[0, 0].PutValue(K12.Data.School.ChineseName + intSchoolYear.Value +"學年度"+ cboSemester.Text + ItemName + "統計資料");
                    else
                        wb.Worksheets[SheetCount].Cells[0, 0].PutValue(K12.Data.School.ChineseName + intSchoolYear.Value + "學年度" + cboSemester.Text + ItemName + "統計資料" + "\n" + "日期區間" + StartDate.Value.ToLongDateString() + "至" + EndDate.Value.ToLongDateString());
                    wb.Worksheets[SheetCount].Cells[0, 0].Style.IsTextWrapped = true;
                    wb.Worksheets[SheetCount].Cells[0, 0].Style.Font.Size = 18;
                    wb.Worksheets[SheetCount].Cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[SheetCount].Cells[0, 0].Style.VerticalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[SheetCount].Cells.Merge(0, 0, 1, 9);
                    if (StartDate.IsEmpty)
                        wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 25);
                    else
                        wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 50);
                    wb.Worksheets[SheetCount].Cells[1, 0].PutValue("金額");
                    wb.Worksheets[SheetCount].Cells[1, 1].PutValue("人數");
                    wb.Worksheets[SheetCount].Cells[1, 2].PutValue("總計金額");
                    wb.Worksheets[SheetCount].Cells[1, 3].PutValue("金額");
                    wb.Worksheets[SheetCount].Cells[1, 4].PutValue("人數");
                    wb.Worksheets[SheetCount].Cells[1, 5].PutValue("總計金額");
                    wb.Worksheets[SheetCount].Cells[1, 6].PutValue("金額");
                    wb.Worksheets[SheetCount].Cells[1, 7].PutValue("人數");
                    wb.Worksheets[SheetCount].Cells[1, 8].PutValue("總計金額");
                    row = 2; col = 0;
                    foreach (int Money in ItemStatic[ItemName].Keys)
                    {
                        if (col > 6)
                        {
                            row++;
                            col = 0;
                        }
                        wb.Worksheets[SheetCount].Cells[row, col].PutValue(Money);
                        wb.Worksheets[SheetCount].Cells[row, col].Style.Custom = "#,##0_ ";
                        wb.Worksheets[SheetCount].Cells[row, col + 1].PutValue(ItemStatic[ItemName][Money]);
                        wb.Worksheets[SheetCount].Cells[row, col + 1].Style.Custom = "#,##0_ ";
                        wb.Worksheets[SheetCount].Cells[row, col + 2].PutValue(Money * ItemStatic[ItemName][Money]);
                        wb.Worksheets[SheetCount].Cells[row, col + 2].Style.Custom = "#,##0_ ";
                        col += 3;
                    }
                    tmpMaxRow = wb.Worksheets[SheetCount].Cells.MaxDataRow;
                    tmpMaxCol = wb.Worksheets[SheetCount].Cells.MaxDataColumn + 1;
                    if (tmpMaxRow >= 1) 
                       wb.Worksheets[SheetCount].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);


                    //設定紙張大小
                    wb.Worksheets[SheetCount].PageSetup.PaperSize = PaperSizeType.PaperA4;
                    //設定紙張方向
                    wb.Worksheets[SheetCount].PageSetup.Orientation = PageOrientationType.Portrait;
                    //自動調整欄寬
                    wb.Worksheets[SheetCount].AutoFitColumns();
                    //自動調整列高
                    wb.Worksheets[SheetCount].AutoFitRows();
                    wb.Worksheets[SheetCount].PageSetup.PrintTitleRows = "$1:$2"; //設定跨頁標題
                    //設定邊界
                    wb.Worksheets[SheetCount].PageSetup.BottomMargin = 1.5;
                    wb.Worksheets[SheetCount].PageSetup.TopMargin = 1.5;
                    wb.Worksheets[SheetCount].PageSetup.HeaderMargin = 1;
                    wb.Worksheets[SheetCount].PageSetup.LeftMargin = 1;
                    wb.Worksheets[SheetCount].PageSetup.RightMargin = 1;
                    SheetCount++;
                }
            }
            else
            {
                int TotalMoney = 0;
                int TotalPeople = 0;
                SheetCount = 0;
                row = 2; col = 0;
                wb.Worksheets[SheetCount].Name = "各項繳費項目及異動項目統計清冊";
                if (StartDate.IsEmpty)
                    wb.Worksheets[SheetCount].Cells[0, 0].PutValue(K12.Data.School.ChineseName + intSchoolYear.Value +"學年度"+ cboSemester.Text  + "各項繳費項目及異動項目統計清冊");
                else
                    wb.Worksheets[SheetCount].Cells[0, 0].PutValue(K12.Data.School.ChineseName + intSchoolYear.Value + "學年度" + cboSemester.Text + "各項繳費項目及異動項目統計清冊" + "\n" + "日期區間" + StartDate.Value.ToLongDateString() + "至" + EndDate.Value.ToLongDateString());
                wb.Worksheets[SheetCount].Cells[0, 0].Style.IsTextWrapped = true;
                wb.Worksheets[SheetCount].Cells[0, 0].Style.Font.Size = 14;
                wb.Worksheets[SheetCount].Cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                wb.Worksheets[SheetCount].Cells[0, 0].Style.VerticalAlignment = TextAlignmentType.Center;
                wb.Worksheets[SheetCount].Cells.Merge(0, 0, 1, 4);
                if (StartDate.IsEmpty)
                    wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 25);
                else
                    wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 50);
                wb.Worksheets[SheetCount].Cells[1, 0].PutValue("項目名稱");
                wb.Worksheets[SheetCount].Cells[1, 1].PutValue("金額");
                wb.Worksheets[SheetCount].Cells[1, 2].PutValue("人數");
                wb.Worksheets[SheetCount].Cells[1, 3].PutValue("總計金額");
                wb.Worksheets[SheetCount].Cells.Rows[1].Style.HorizontalAlignment = TextAlignmentType.Center;
                wb.Worksheets[SheetCount].Cells.Rows[1].Style.VerticalAlignment = TextAlignmentType.Center;
                foreach (string ItemName in ItemStatic.Keys)
                {
                    wb.Worksheets[SheetCount].Cells[row, col].PutValue(ItemName);
                    foreach (int Money in ItemStatic[ItemName].Keys)
                    {
                        wb.Worksheets[SheetCount].Cells[row, col + 1].PutValue(Money);
                        wb.Worksheets[SheetCount].Cells[row, col + 1].Style.Custom = "#,##0_ ";
                        wb.Worksheets[SheetCount].Cells[row, col + 2].PutValue(ItemStatic[ItemName][Money]);
                        wb.Worksheets[SheetCount].Cells[row, col + 2].Style.Custom = "#,##0_ ";
                        wb.Worksheets[SheetCount].Cells[row, col + 3].PutValue(Money * ItemStatic[ItemName][Money]);
                        wb.Worksheets[SheetCount].Cells[row, col + 3].Style.Custom = "#,##0_ ";
                        TotalMoney += Money * ItemStatic[ItemName][Money];
                        TotalPeople += ItemStatic[ItemName][Money];
                        row++;
                    }
                    wb.Worksheets[SheetCount].Cells[row, col].PutValue("總計");
                    wb.Worksheets[SheetCount].Cells[row, col + 2].PutValue(TotalPeople);
                    wb.Worksheets[SheetCount].Cells[row, col + 3].PutValue(TotalMoney);
                    wb.Worksheets[SheetCount].Cells[row, col + 3].Style.Custom = "#,##0_ ";
                    row++;
                    TotalMoney = 0;
                    TotalPeople = 0;
                }
                tmpMaxRow = wb.Worksheets[SheetCount].Cells.MaxDataRow;
                tmpMaxCol = wb.Worksheets[SheetCount].Cells.MaxDataColumn + 1;
                if (tmpMaxRow >= 1) 
                    wb.Worksheets[SheetCount].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
                //設定紙張大小
                wb.Worksheets[SheetCount].PageSetup.PaperSize = PaperSizeType.PaperA4;
                //設定紙張方向
                wb.Worksheets[SheetCount].PageSetup.Orientation = PageOrientationType.Portrait;
                //自動調整欄寬
                wb.Worksheets[SheetCount].AutoFitColumns();
                //自動調整列高
                wb.Worksheets[SheetCount].AutoFitRows();
                wb.Worksheets[SheetCount].PageSetup.PrintTitleRows = "$1:$2"; //設定跨頁標題
                //設定邊界
                wb.Worksheets[SheetCount].PageSetup.BottomMargin = 1.5;
                wb.Worksheets[SheetCount].PageSetup.TopMargin = 1.5;
                wb.Worksheets[SheetCount].PageSetup.HeaderMargin = 1;
                wb.Worksheets[SheetCount].PageSetup.LeftMargin = 1;
                wb.Worksheets[SheetCount].PageSetup.RightMargin = 1; 
            }
            //刪除第一張工作表
            if (wb.Worksheets.Count > 1)
                wb.Worksheets.RemoveAt(0);
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\各項繳費項目及異動項目統計清冊.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\各項繳費項目及異動項目統計清冊.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "各項繳費項目及異動項目統計清冊.xls";
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
        private void RegisterCalc()
        {
            Workbook wb = new Workbook();
            Style defaultStyle = wb.DefaultStyle;
            defaultStyle.Font.Name = "標楷體";
            defaultStyle.Font.Size = 10;
            wb.DefaultStyle = defaultStyle;
            Style st2 = wb.Styles[wb.Styles.Add()];
            StyleFlag sf2 = new StyleFlag();
            sf2.Borders = true;
            
            st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            int tmpMaxRow = 0, tmpMaxCol = 0;
            int row1 = 2;
            int col1 = 0;
            int row2 = 2;
            int col2 = 6;
            int row3 = 2;
            int col3 = 12;
            int SheetCount = 0;
            
            //繳費表
            List<StudentTuitionRecord> StuTuRs = new List<StudentTuitionRecord>();
            //班級
            List<ClassRecord> ClassRs = new List<ClassRecord>();            
            //所選班級
            ClassRs = K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource);         
            //學生ID
            List<string> StudentID = new List<string>();           
            //所有學生繳費表
            Dictionary<string, List<StudentTuitionRecord>> StudentTus = new Dictionary<string, List<StudentTuitionRecord>>();
           
            //統計註冊人數;
            SortedList<ClassRecord, Dictionary<string, int>> RegStatic = new SortedList<ClassRecord, Dictionary<string, int>>();
            StuTuRs.Clear();
            StudentID.Clear();           
            StudentTus.Clear();
            foreach (ClassRecord cr in ClassRs) 
            {               
                //找出全班學生ID
                foreach (StudentRecord sr in cr.Students)
                    StudentID.Add(sr.ID);
            }
            //找出全部繳費表
            StuTuRs = StudentTuitionDAO.GetStudentTuitionBySSTSL(intSchoolYear.Value, cboSemester.Text, "舊生", StudentID);
            int nowSet = 0;
            foreach (StudentTuitionRecord sr in StuTuRs)
            {
                MotherForm.SetStatusBarMessage("正在產生報表", nowSet++*100 / StuTuRs.Count );
                //找出班級
                if (!RegStatic.ContainsKey(SHStudent.SelectByID(sr.TuitionUID).Class))
                {
                    RegStatic.Add(SHStudent.SelectByID(sr.TuitionUID).Class, new Dictionary<string, int>());
                    RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class].Add("註冊人數", 0);
                    RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class].Add("註冊人數(學籍註銷)", 0);
                    RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class].Add("未註冊人數", 0);
                    RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class].Add("未註冊人數(學籍註銷)", 0);
                    RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class].Add("註冊金額", 0);
                }
                if (SHStudent.SelectByID(sr.TuitionUID).Status ==  SHStudentRecord.StudentStatus.一般 && sr.PayDate != null)
                {
                    if (StartDate.IsEmpty == false)
                    {
                        if (sr.PayDate >= StartDate.Value && sr.PayDate <= EndDate.Value)
                        RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class]["註冊人數"]++;
                    }
                    else
                        RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class]["註冊人數"]++;
                }
                if (SHStudent.SelectByID(sr.TuitionUID).Status != SHStudentRecord.StudentStatus.一般 && sr.PayDate != null)
                {
                    if (StartDate.IsEmpty == false)
                    {
                        if (sr.PayDate >= StartDate.Value && sr.PayDate <= EndDate.Value)
                            RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class]["註冊人數(學籍註銷)"]++;
                    }
                    else
                        RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class]["註冊人數(學籍註銷)"]++;
                }
                if (SHStudent.SelectByID(sr.TuitionUID).Status == SHStudentRecord.StudentStatus.一般 && sr.PayDate == null)
                    RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class]["未註冊人數"]++;
                if (SHStudent.SelectByID(sr.TuitionUID).Status != SHStudentRecord.StudentStatus.一般 && sr.PayDate == null)
                    RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class]["未註冊人數(學籍註銷)"]++;
                if (StartDate.IsEmpty == false)
                    {
                        if (sr.PayDate >= StartDate.Value && sr.PayDate <= EndDate.Value)
                            RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class]["註冊金額"] += sr.Payment;
                    }
                else
                    RegStatic[SHStudent.SelectByID(sr.TuitionUID).Class]["註冊金額"] += sr.Payment;
                
                
            }
            wb.Worksheets[SheetCount].Name = "註冊統計";
            wb.Worksheets[SheetCount].Cells[0, 0].PutValue(K12.Data.School.ChineseName + intSchoolYear.Value +"學年度"+ cboSemester.Text + "班級註冊統計資料");            
            wb.Worksheets[SheetCount].Cells[0, 0].Style.Font.Size = 18;
            wb.Worksheets[SheetCount].Cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[SheetCount].Cells[0, 0].Style.VerticalAlignment = TextAlignmentType.Center;
            wb.Worksheets[SheetCount].Cells.Merge(0, 0, 1, 18);            
            wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 25);            
            wb.Worksheets[SheetCount].Cells[1, 0].PutValue("班級名稱");
            wb.Worksheets[SheetCount].Cells[1, 1].PutValue("註冊\n人數");
            wb.Worksheets[SheetCount].Cells[1, 2].PutValue("註冊人數\n(學籍註銷)");
            wb.Worksheets[SheetCount].Cells[1, 3].PutValue("未註冊\n人數");
            wb.Worksheets[SheetCount].Cells[1, 4].PutValue("未註冊人數\n(學籍註銷)");
            wb.Worksheets[SheetCount].Cells[1, 5].PutValue("註冊金額");
            wb.Worksheets[SheetCount].Cells[1, 6].PutValue("班級名稱");
            wb.Worksheets[SheetCount].Cells[1, 7].PutValue("註冊\n人數");
            wb.Worksheets[SheetCount].Cells[1, 8].PutValue("註冊人數\n(學籍註銷)");
            wb.Worksheets[SheetCount].Cells[1, 9].PutValue("未註冊\n人數");
            wb.Worksheets[SheetCount].Cells[1, 10].PutValue("未註冊人數\n(學籍註銷)");
            wb.Worksheets[SheetCount].Cells[1, 11].PutValue("註冊金額");
            wb.Worksheets[SheetCount].Cells[1, 12].PutValue("班級名稱");
            wb.Worksheets[SheetCount].Cells[1, 13].PutValue("註冊\n人數");
            wb.Worksheets[SheetCount].Cells[1, 14].PutValue("註冊人數\n(學籍註銷)");           
            wb.Worksheets[SheetCount].Cells[1, 15].PutValue("未註冊\n人數");
            wb.Worksheets[SheetCount].Cells[1, 16].PutValue("未註冊人數\n(學籍註銷)");            
            wb.Worksheets[SheetCount].Cells[1, 17].PutValue("註冊金額");
            wb.Worksheets[SheetCount].Cells.Rows[1].Style.IsTextWrapped = true;
            wb.Worksheets[SheetCount].Cells.Rows[1].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[SheetCount].Cells.Rows[1].Style.VerticalAlignment = TextAlignmentType.Center;
            foreach (ClassRecord cr in RegStatic.Keys) 
            {
                switch (cr.GradeYear)
                {
                    case 1:
                        wb.Worksheets[SheetCount].Cells[row1, col1 ].PutValue(cr.Name);
                        wb.Worksheets[SheetCount].Cells[row1, col1 + 1].PutValue(RegStatic[cr]["註冊人數"]);
                        wb.Worksheets[SheetCount].Cells[row1, col1 + 2].PutValue(RegStatic[cr]["註冊人數(學籍註銷)"]);
                        wb.Worksheets[SheetCount].Cells[row1, col1 + 3].PutValue(RegStatic[cr]["未註冊人數"]);
                        wb.Worksheets[SheetCount].Cells[row1, col1 + 4].PutValue(RegStatic[cr]["未註冊人數(學籍註銷)"]);
                        wb.Worksheets[SheetCount].Cells[row1, col1 + 5].PutValue(RegStatic[cr]["註冊金額"]);
                        row1++;
                        break;
                    case 2:
                        wb.Worksheets[SheetCount].Cells[row2, col2].PutValue(cr.Name);
                        wb.Worksheets[SheetCount].Cells[row2, col2 + 1].PutValue(RegStatic[cr]["註冊人數"]);
                        wb.Worksheets[SheetCount].Cells[row2, col2 + 2].PutValue(RegStatic[cr]["註冊人數(學籍註銷)"]);
                        wb.Worksheets[SheetCount].Cells[row2, col2 + 3].PutValue(RegStatic[cr]["未註冊人數"]);
                        wb.Worksheets[SheetCount].Cells[row1, col2 + 4].PutValue(RegStatic[cr]["未註冊人數(學籍註銷)"]);
                        wb.Worksheets[SheetCount].Cells[row2, col2 + 5].PutValue(RegStatic[cr]["註冊金額"]);
                        row2++;
                        break;
                    case 3:
                        wb.Worksheets[SheetCount].Cells[row3, col3].PutValue(cr.Name);
                        wb.Worksheets[SheetCount].Cells[row3, col3 + 1].PutValue(RegStatic[cr]["註冊人數"]);
                        wb.Worksheets[SheetCount].Cells[row3, col3 + 2].PutValue(RegStatic[cr]["註冊人數(學籍註銷)"]);
                        wb.Worksheets[SheetCount].Cells[row3, col3 + 3].PutValue(RegStatic[cr]["未註冊人數"]);
                        wb.Worksheets[SheetCount].Cells[row1, col3 + 4].PutValue(RegStatic[cr]["未註冊人數(學籍註銷)"]);
                        wb.Worksheets[SheetCount].Cells[row3, col3 + 5].PutValue(RegStatic[cr]["註冊金額"]);
                        row3++;
                        break;
                }
            }
            tmpMaxRow = wb.Worksheets[SheetCount].Cells.MaxDataRow;
            tmpMaxCol = wb.Worksheets[SheetCount].Cells.MaxDataColumn + 1;
            if (tmpMaxRow >= 1) 
               wb.Worksheets[SheetCount].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);


            //設定紙張大小
            wb.Worksheets[SheetCount].PageSetup.PaperSize = PaperSizeType.PaperA4;
            wb.Worksheets[SheetCount].PageSetup.Zoom = 85;
            //設定紙張方向
            wb.Worksheets[SheetCount].PageSetup.Orientation = PageOrientationType.Landscape;
            //自動調整欄寬
            wb.Worksheets[SheetCount].AutoFitColumns();
            //自動調整列高
            wb.Worksheets[SheetCount].AutoFitRows();
            wb.Worksheets[SheetCount].PageSetup.PrintTitleRows = "$1:$2"; //設定跨頁標題
            //設定邊界
            wb.Worksheets[SheetCount].PageSetup.BottomMargin = 1.5;
            wb.Worksheets[SheetCount].PageSetup.TopMargin = 1.5;
            wb.Worksheets[SheetCount].PageSetup.HeaderMargin = 1;
            wb.Worksheets[SheetCount].PageSetup.LeftMargin = 1;
            wb.Worksheets[SheetCount].PageSetup.RightMargin = 1;
            wb.Worksheets[SheetCount].PageSetup.CenterHorizontally = true;
            
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\註冊及未註冊人數暨註冊金額統計表.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\註冊及未註冊人數暨註冊金額統計表.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "註冊及未註冊人數暨註冊金額統計表.xls";
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
            MotherForm.SetStatusBarMessage("");
        }
        private void AmountError(int Kind)
        {
            Workbook wb = new Workbook();
            Style defaultStyle = wb.DefaultStyle;
            defaultStyle.Font.Name = "標楷體";
            defaultStyle.Font.Size = 12;
            wb.DefaultStyle = defaultStyle;
            Style st2 = wb.Styles[wb.Styles.Add()];
            StyleFlag sf2 = new StyleFlag();
            sf2.Borders = true;

            st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            int tmpMaxRow = 0, tmpMaxCol = 0;
            int row = 2;            
            string PrintTitle;
            
            List<StudentTuitionRecord> Strs = new List<StudentTuitionRecord>();
            List<TuitionDetailRecord> Tdrs = new List<TuitionDetailRecord>();
            Strs = StudentTuitionDAO.GetStudentTuitionBySS(intSchoolYear.Value, cboSemester.Text);
            if (Kind==1)
                PrintTitle = K12.Data.School.ChineseName + intSchoolYear.Value + "學年度" + cboSemester.Text + "應收金額與實收金額不符者名冊列印";
            else
                PrintTitle = K12.Data.School.ChineseName + intSchoolYear.Value + "學年度" + cboSemester.Text + "應收金額小於零名冊列印";
            if (!StartDate.IsEmpty)
                PrintTitle+= "\n" + "日期區間" + StartDate.Value.ToLongDateString() + "至" + EndDate.Value.ToLongDateString();
            wb.Worksheets[0].Cells[0, 0].PutValue(PrintTitle);
            wb.Worksheets[0].Cells.Merge(0, 0, 1, 11);
            wb.Worksheets[0].Cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[0, 0].Style.VerticalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[0, 0].Style.Font.Size = 16;
            wb.Worksheets[0].Cells[0, 0].Style.IsTextWrapped=true;
            if (StartDate.IsEmpty)
               wb.Worksheets[0].Cells.SetRowHeight(0, 25); 
            else
                wb.Worksheets[0].Cells.SetRowHeight(0, 50);
            wb.Worksheets[0].Name = "名冊";
            wb.Worksheets[0].Cells[1, 0].PutValue("學生類別");
            wb.Worksheets[0].Cells[1, 1].PutValue("科別/班別");
            wb.Worksheets[0].Cells[1, 2].PutValue("編號/學號");
            wb.Worksheets[0].Cells[1, 3].PutValue("姓名");
            wb.Worksheets[0].Cells[1, 4].PutValue("性別");
            wb.Worksheets[0].Cells[1, 5].PutValue("總計金額");
            wb.Worksheets[0].Cells[1, 6].PutValue("異動金額");
            wb.Worksheets[0].Cells[1, 7].PutValue("應收金額");
            wb.Worksheets[0].Cells[1, 8].PutValue("實收金額");
            wb.Worksheets[0].Cells[1, 9].PutValue("繳費日期");
            wb.Worksheets[0].Cells[1, 10].PutValue("備    註");
            wb.Worksheets[0].Cells.SetRowHeight(1, 20);
            row = 2;
            Boolean PrintCheck;
            int nowSet = 0;
            foreach (StudentTuitionRecord sr in Strs)
            {
                MotherForm.SetStatusBarMessage("正在產生報表", nowSet++*100 / Strs.Count );
                PrintCheck = false;
                if (Kind == 1 && sr.Payment!=sr.ChargeAmount && sr.PayDate!=null)
                    if (StartDate.IsEmpty || (sr.PayDate >= StartDate.Value && sr.PayDate <= EndDate.Value))
                       PrintCheck = true;
                if (Kind == 2 && sr.ChargeAmount < 0)
                    PrintCheck = true;
                if (PrintCheck)
                {
                    wb.Worksheets[0].Cells[row, 0].PutValue(sr.StudentType);
                    if (sr.StudentType == "舊生")
                    {
                        wb.Worksheets[0].Cells[row, 1].PutValue(SHStudent.SelectByID(sr.TuitionUID).Class.Name);
                        wb.Worksheets[0].Cells[row, 2].PutValue((SHStudent.SelectByID(sr.TuitionUID).Status== SHStudentRecord.StudentStatus.一般? "":"*")+SHStudent.SelectByID(sr.TuitionUID).StudentNumber);
                        wb.Worksheets[0].Cells[row, 3].PutValue(SHStudent.SelectByID(sr.TuitionUID).Name);
                        wb.Worksheets[0].Cells[row, 4].PutValue(SHStudent.SelectByID(sr.TuitionUID).Gender);                        
                    }
                    //else
                    //{
                    //    wb.Worksheets[0].Cells[row, 1].PutValue(NewStudent.Instance.Items[sr.TuitionUID].Dept);
                    //    wb.Worksheets[0].Cells[row, 2].PutValue((NewStudent.Instance.Items[sr.TuitionUID].Active? "":"*")+NewStudent.Instance.Items[sr.TuitionUID].Number);
                    //    wb.Worksheets[0].Cells[row, 3].PutValue(NewStudent.Instance.Items[sr.TuitionUID].Name);
                    //    wb.Worksheets[0].Cells[row, 4].PutValue(NewStudent.Instance.Items[sr.TuitionUID].Gender);
                    //}
                    wb.Worksheets[0].Cells[row, 5].PutValue(sr.ChargeAmount - sr.ChangeMoney);
                    wb.Worksheets[0].Cells[row, 6].PutValue(sr.ChangeMoney);
                    wb.Worksheets[0].Cells[row, 7].PutValue(sr.ChargeAmount);
                    wb.Worksheets[0].Cells[row, 8].PutValue(sr.Payment);
                    if (sr.PayDate != null)
                        wb.Worksheets[0].Cells[row, 9].PutValue(sr.PayDate.Value.Year - 1911 + "." + sr.PayDate.Value.Month + "." + sr.PayDate.Value.Day);
                    //if (sr.ChangeMoney != 0)
                    //{
                        Tdrs = TuitionDetailDAO.GetTuitionDetailByUID(sr.UID);
                        string strChange = "";
                        foreach (TuitionDetailRecord tr in Tdrs)
                            strChange += tr.TCSName + "：" + tr.ChangeAmount + "\n";
                        if (strChange != "")
                        {
                            wb.Worksheets[0].Cells[row, 10].PutValue(strChange.Substring(0, strChange.Length - 1));
                            wb.Worksheets[0].Cells[row, 10].Style.ShrinkToFit = true;
                            wb.Worksheets[0].Cells[row, 10].Style.IsTextWrapped = true;
                            wb.Worksheets[0].Cells[row, 10].Style.Font.Size = 8;
                        }
                    //}
                    row += 1;                
                 }
            
            }
            // 畫表

            tmpMaxRow = wb.Worksheets[0].Cells.MaxDataRow;
            tmpMaxCol = wb.Worksheets[0].Cells.MaxDataColumn + 1;
            if (tmpMaxCol != 0 && tmpMaxRow != 0)
                wb.Worksheets[0].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            //自動調整欄寬
            wb.Worksheets[0].AutoFitColumns();
            
            try
            {
                if (Kind == 1)
                {
                    wb.Save(Application.StartupPath + "\\Reports\\應收金額與實收金額不符者名冊列印.xls", FileFormatType.Excel2003);
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\應收金額與實收金額不符者名冊列印.xls");
                }
                else
                {
                    wb.Save(Application.StartupPath + "\\Reports\\應收金額小於零名冊列印.xls", FileFormatType.Excel2003);
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\應收金額小於零名冊列印.xls");
                }
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                if (Kind == 1)
                    sd1.FileName = "應收金額與實收金額不符者名冊列印.xls";
                else
                    sd1.FileName = "應收金額小於零名冊列印.xls";
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
            MotherForm.SetStatusBarMessage("");
        }
        private static string chr(int p)
        {
            string English = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (p < 27)
                return English.Substring(p - 1, 1);
            else
                return "";
        }
        }
}
