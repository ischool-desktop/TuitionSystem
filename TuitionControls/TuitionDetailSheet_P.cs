using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
//using MySchoolModule.Data;
//使用Excel之參考
using Aspose.Cells;
using FISCA.UDT;
//using MySchoolModule;
using TuitionSystem.Data;
using FISCA.Presentation;
using K12.Data;

namespace TuitionSystem.TuitionControls
{
    public partial class TuitionDetailSheet_P : BaseForm
    {
        private int ResourceKind=1;
        
        public TuitionDetailSheet_P(int Kind)
        {
            ResourceKind = Kind;
            InitializeComponent();
        }

        private void TuitionDetailSheet_P_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            intSchoolYear.Value = GlobalValue.CurrentSchoolYear;
            cboSemester.Text = GlobalValue.CurrentSemester;           
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
            Workbook wb = new Workbook();
            Style defaultStyle = wb.DefaultStyle;
            defaultStyle.Font.Name = "標楷體";
            defaultStyle.Font.Size = 10;
            wb.DefaultStyle = defaultStyle;
            int row = 0;
            int nowSet=0;
            int SheetCount = 0;
            string PrintTitle;
            List<StudentTuitionRecord> srList = new List<StudentTuitionRecord>();
            List<TuitionDetailRecord> tdr = new List<TuitionDetailRecord>();
            List<string> stuList = new List<string>();
            List<string> strIDs = new List<string>();
            Dictionary<string, List<StudentTuitionRecord>> strList = new Dictionary<string, List<StudentTuitionRecord>>();
            Dictionary<string, List<TuitionDetailRecord>> tdrList = new Dictionary<string, List<TuitionDetailRecord>>();
            switch (ResourceKind)
            {
                case 1:
                    if (K12.Presentation.NLDPanels.Class.SelectedSource.Count == 0)
                    {
                        MessageBox.Show("沒有選擇任何一個班級");
                        return;
                    }
                    
                    foreach (ClassRecord cl in K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
                        foreach (StudentRecord nsr in cl.Students)
                            stuList.Add(nsr.ID);
                    srList = StudentTuitionDAO.GetStudentTuitionBySSTSL(intSchoolYear.Value, cboSemester.Text, "舊生", stuList);
                    foreach (StudentTuitionRecord sr in srList)
                    {
                        if (!strList.ContainsKey(sr.TuitionUID))
                        {
                            strList.Add(sr.TuitionUID, new List<StudentTuitionRecord>());
                            strList[sr.TuitionUID].Add(sr);
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
                    foreach (ClassRecord cl in K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource))
                    {
                        MotherForm.SetStatusBarMessage("正在產生報表", nowSet++*100 / K12.Presentation.NLDPanels.Class.SelectedSource.Count );
                        List<StudentRecord> StudentR = cl.Students;
                        //依學號排序
                        StudentR.Sort(CompareNumber);
                        wb.Worksheets.Add();
                        SheetCount += 1;
                        wb.Worksheets[SheetCount].Name = cl.Name;
                        row = 0;
                        PrintTitle = "  " + K12.Data.School.ChineseName + "  "+intSchoolYear.Value + " 學年度" + cboSemester.Text + "繳費明細表" + "\r班級：" + cl.Name; //+ JHSchool.School.ChineseName
                        wb.Worksheets[SheetCount].PageSetup.SetHeader(1, PrintTitle);

                        //wb.Worksheets[SheetCount].Cells[row, 0].PutValue("科別");
                        //wb.Worksheets[SheetCount].Cells.SetColumnWidth(0, 18);
                        wb.Worksheets[SheetCount].Cells[row, 0].PutValue("學號");
                        wb.Worksheets[SheetCount].Cells.SetColumnWidth(0, 9);
                        wb.Worksheets[SheetCount].Cells[row, 1].PutValue("姓名");
                        wb.Worksheets[SheetCount].Cells.SetColumnWidth(1, 15);
                        wb.Worksheets[SheetCount].Cells[row, 2].PutValue("性別");
                        wb.Worksheets[SheetCount].Cells.SetColumnWidth(2, 4.38);
                        wb.Worksheets[SheetCount].Cells[row, 3].PutValue("註冊費");
                        wb.Worksheets[SheetCount].Cells.SetColumnWidth(3, 14);
                        wb.Worksheets[SheetCount].Cells[row, 4].PutValue("異動金額");
                        wb.Worksheets[SheetCount].Cells.SetColumnWidth(4, 12);
                        wb.Worksheets[SheetCount].Cells[row, 5].PutValue("應收金額");
                        wb.Worksheets[SheetCount].Cells.SetColumnWidth(5, 14);
                        wb.Worksheets[SheetCount].Cells[row, 6].PutValue("實繳金額");
                        wb.Worksheets[SheetCount].Cells.SetColumnWidth(6, 14);
                        wb.Worksheets[SheetCount].Cells[row, 7].PutValue("繳費日期");
                        wb.Worksheets[SheetCount].Cells.SetColumnWidth(7, 12);
                        wb.Worksheets[SheetCount].Cells[row, 8].PutValue("備註");
                        wb.Worksheets[SheetCount].Cells.SetColumnWidth(8, 20);
                        wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 20);
                        wb.Worksheets[SheetCount].PageSetup.PrintTitleRows = "$1:$1"; //設定跨頁標題
                        //設定邊界
                        wb.Worksheets[SheetCount].PageSetup.BottomMargin = 1.5;
                        wb.Worksheets[SheetCount].PageSetup.TopMargin = 2.5;
                        wb.Worksheets[SheetCount].PageSetup.HeaderMargin = 1;
                        wb.Worksheets[SheetCount].PageSetup.LeftMargin = 1;
                        wb.Worksheets[SheetCount].PageSetup.RightMargin = 1;
                        //設定頁尾
                        //wb.Worksheets[SheetCount].PageSetup.SetFooter(0, "導師請填入學生劃撥日期，註冊完畢後簽名繳回會計室，謝謝！");
                        //wb.Worksheets[SheetCount].PageSetup.SetFooter(2, "導師簽章：");
                        row = 1;
                        foreach (StudentRecord nsr in StudentR)
                        {                            
                            if (strList.ContainsKey(nsr.ID))
                               {
                                wb.Worksheets[SheetCount].Cells.SetRowHeight(row, 30);
                                //wb.Worksheets[SheetCount].Cells[row, 0].PutValue(nsr.Dept);
                                wb.Worksheets[SheetCount].Cells[row, 0].PutValue((nsr.Status== StudentRecord.StudentStatus.一般? "":"*")+nsr.StudentNumber);
                                wb.Worksheets[SheetCount].Cells[row, 1].PutValue(nsr.Name);
                                wb.Worksheets[SheetCount].Cells[row, 2].PutValue(nsr.Gender);
                                wb.Worksheets[SheetCount].Cells[row, 3].PutValue(strList[nsr.ID][0].ChargeAmount - strList[nsr.ID][0].ChangeMoney);
                                wb.Worksheets[SheetCount].Cells[row, 3].Style.Custom = "#,##0_ ";
                                wb.Worksheets[SheetCount].Cells[row, 4].PutValue(strList[nsr.ID][0].ChangeMoney);
                                wb.Worksheets[SheetCount].Cells[row, 4].Style.Custom = "#,##0_ ";
                                wb.Worksheets[SheetCount].Cells[row, 5].PutValue(strList[nsr.ID][0].ChargeAmount);
                                wb.Worksheets[SheetCount].Cells[row, 5].Style.Custom = "#,##0_ ";
                                wb.Worksheets[SheetCount].Cells[row, 6].PutValue(strList[nsr.ID][0].Payment);
                                wb.Worksheets[SheetCount].Cells[row, 6].Style.Custom = "#,##0_ ";
                                //wb.Worksheets[SheetCount].Cells[row, 6].PutValue(strList[nsr.ID][0].PayDate);
                                //wb.Worksheets[SheetCount].Cells[row, 6].Style.Custom = "e.mm.dd";
                                if (strList[nsr.ID][0].PayDate != null)
                                    wb.Worksheets[SheetCount].Cells[row, 7].PutValue(strList[nsr.ID][0].PayDate.Value.Year - 1911 + "." + strList[nsr.ID][0].PayDate.Value.Month + "." + strList[nsr.ID][0].PayDate.Value.Day);
                                //if (strList[nsr.ID][0].ChangeMoney != 0)
                                //{
                                    string strChange = "";
                                    if (tdrList.ContainsKey(strList[nsr.ID][0].UID))
                                    {
                                        foreach (TuitionDetailRecord tr in tdrList[strList[nsr.ID][0].UID])
                                            strChange += tr.TCSName + "：" + tr.ChangeAmount + "\n";
                                        if (strChange != "")
                                        {
                                            wb.Worksheets[SheetCount].Cells[row, 8].PutValue(strChange.Substring(0, strChange.Length - 1));
                                            wb.Worksheets[SheetCount].Cells[row, 8].Style.ShrinkToFit = true;
                                            wb.Worksheets[SheetCount].Cells[row, 8].Style.IsTextWrapped = true;
                                            wb.Worksheets[SheetCount].Cells[row, 8].Style.Font.Size = 8;
                                        }
                                    }
                                //}
                                row += 1;
                            }
                        }
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                if (j < 3)
                                    wb.Worksheets[SheetCount].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Center;
                                else
                                    if (j == 8)
                                        wb.Worksheets[SheetCount].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Left;
                                    else
                                        wb.Worksheets[SheetCount].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Right;
                                wb.Worksheets[SheetCount].Cells[i, j].Style.VerticalAlignment = TextAlignmentType.Center;
                                wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                                wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                                wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                                wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                            }
                        }
                        wb.Worksheets[SheetCount].AutoFitColumns();
                        //wb.Worksheets[SheetCount].AutoFitRows();
                        wb.Worksheets[SheetCount].Cells[row + 1, 4].PutValue("註冊總人數：         人");
                    }                    
                    break;
                case 2:
                    if (K12.Presentation.NLDPanels.Student.SelectedSource.Count == 0)
                    {
                        MessageBox.Show("沒有選擇任何一個學生");
                        return;
                    }
                    
                    row = 0;
                    PrintTitle = " "+K12.Data.School.ChineseName+"  " + intSchoolYear.Value + " 學年度" + cboSemester.Text + "繳費明細表"; //+ JHSchool.School.ChineseName
                    wb.Worksheets[SheetCount].PageSetup.SetHeader(1, PrintTitle);
                    wb.Worksheets[SheetCount].Cells[row, 0].PutValue("班級");
                    wb.Worksheets[SheetCount].Cells.SetColumnWidth(0, 10);
                    wb.Worksheets[SheetCount].Cells[row, 1].PutValue("學號");
                    wb.Worksheets[SheetCount].Cells.SetColumnWidth(1, 9);
                    wb.Worksheets[SheetCount].Cells[row, 2].PutValue("姓名");
                    wb.Worksheets[SheetCount].Cells.SetColumnWidth(2, 13);
                    wb.Worksheets[SheetCount].Cells[row, 3].PutValue("性別");
                    wb.Worksheets[SheetCount].Cells.SetColumnWidth(3, 4.38);
                    wb.Worksheets[SheetCount].Cells[row, 4].PutValue("註冊費");
                    wb.Worksheets[SheetCount].Cells.SetColumnWidth(4, 10);
                    wb.Worksheets[SheetCount].Cells[row, 5].PutValue("異動金額");
                    wb.Worksheets[SheetCount].Cells.SetColumnWidth(5, 10);
                    wb.Worksheets[SheetCount].Cells[row, 6].PutValue("應收金額");
                    wb.Worksheets[SheetCount].Cells.SetColumnWidth(6, 10);
                    wb.Worksheets[SheetCount].Cells[row, 7].PutValue("繳費日期");
                    wb.Worksheets[SheetCount].Cells.SetColumnWidth(7, 10);
                    wb.Worksheets[SheetCount].Cells[row, 8].PutValue("備註");
                    wb.Worksheets[SheetCount].Cells.SetColumnWidth(8, 20);
                    wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 20);
                    wb.Worksheets[SheetCount].PageSetup.PrintTitleRows = "$1:$1"; //設定跨頁標題
                    //設定邊界
                    wb.Worksheets[SheetCount].PageSetup.BottomMargin = 1.5;
                    wb.Worksheets[SheetCount].PageSetup.TopMargin = 2.5;
                    wb.Worksheets[SheetCount].PageSetup.HeaderMargin = 1;
                    wb.Worksheets[SheetCount].PageSetup.LeftMargin = 1;
                    wb.Worksheets[SheetCount].PageSetup.RightMargin = 1;
                    row = 1;                    
                    foreach (StudentRecord nsr in K12.Data.Student.SelectByIDs(K12.Presentation.NLDPanels.Student.SelectedSource))
                        stuList.Add(nsr.ID);
                    srList = StudentTuitionDAO.GetStudentTuitionBySSTSL(intSchoolYear.Value, cboSemester.Text, "舊生", stuList);
                    foreach (StudentTuitionRecord sr in srList)
                    {
                        if (!strList.ContainsKey(sr.TuitionUID))
                        {
                            strList.Add(sr.TuitionUID, new List<StudentTuitionRecord>());
                            strList[sr.TuitionUID].Add(sr);
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
                    foreach (StudentRecord nsr in K12.Data.Student.SelectByIDs(K12.Presentation.NLDPanels.Student.SelectedSource))
                    {
                        MotherForm.SetStatusBarMessage("正在產生報表", nowSet++*100 / K12.Presentation.NLDPanels.Student.SelectedSource.Count );                        
                        if (strList.ContainsKey(nsr.ID))
                        {
                            wb.Worksheets[SheetCount].Cells.SetRowHeight(row, 30);
                            //wb.Worksheets[SheetCount].Cells[row, 0].PutValue(nsr.Dept);
                            wb.Worksheets[SheetCount].Cells[row, 0].PutValue(nsr.Class.Name);
                            wb.Worksheets[SheetCount].Cells[row, 1].PutValue((nsr.Status== StudentRecord.StudentStatus.一般? "":"*")+nsr.StudentNumber);
                            wb.Worksheets[SheetCount].Cells[row, 2].PutValue(nsr.Name);
                            wb.Worksheets[SheetCount].Cells[row, 3].PutValue(nsr.Gender);
                            wb.Worksheets[SheetCount].Cells[row, 4].PutValue(strList[nsr.ID][0].ChargeAmount - strList[nsr.ID][0].ChangeMoney);
                            wb.Worksheets[SheetCount].Cells[row, 4].Style.Custom = "#,##0_ ";
                            wb.Worksheets[SheetCount].Cells[row, 5].PutValue(strList[nsr.ID][0].ChangeMoney);
                            wb.Worksheets[SheetCount].Cells[row, 5].Style.Custom = "#,##0_ ";
                            wb.Worksheets[SheetCount].Cells[row, 6].PutValue(strList[nsr.ID][0].ChargeAmount);
                            wb.Worksheets[SheetCount].Cells[row, 6].Style.Custom = "#,##0_ ";
                            //wb.Worksheets[SheetCount].Cells[row, 6].PutValue(strList[nsr.UID][0].PayDate);
                            //wb.Worksheets[SheetCount].Cells[row, 6].Style.Custom = "e.mm.dd";
                            if (strList[nsr.ID][0].PayDate != null)
                                wb.Worksheets[SheetCount].Cells[row, 7].PutValue(strList[nsr.ID][0].PayDate.Value.Year - 1911 + "." + strList[nsr.ID][0].PayDate.Value.Month + "." + strList[nsr.ID][0].PayDate.Value.Day);
                            //if (strList[nsr.ID][0].ChangeMoney != 0) 
                            //{                                
                                string strChange = "";
                                if (tdrList.ContainsKey(strList[nsr.ID][0].UID))
                                {
                                    foreach (TuitionDetailRecord tr in tdrList[strList[nsr.ID][0].UID])
                                        strChange += tr.TCSName + "：" + tr.ChangeAmount + "\n";
                                    if (strChange != "")
                                    {
                                        wb.Worksheets[SheetCount].Cells[row, 8].PutValue(strChange.Substring(0, strChange.Length - 1));
                                        wb.Worksheets[SheetCount].Cells[row, 8].Style.ShrinkToFit = true;
                                        wb.Worksheets[SheetCount].Cells[row, 8].Style.IsTextWrapped = true;
                                        wb.Worksheets[SheetCount].Cells[row, 8].Style.Font.Size = 8;
                                    }
                                }
                            //}
                            row += 1;
                        }
                    }
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (j < 4)
                                wb.Worksheets[SheetCount].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Center;
                            else
                                if (j == 8)
                                    wb.Worksheets[SheetCount].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Left;
                                else
                                    wb.Worksheets[SheetCount].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Right;
                            wb.Worksheets[SheetCount].Cells[i, j].Style.VerticalAlignment = TextAlignmentType.Center;
                            wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                            wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                            wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                            wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                        }

                    }
                    wb.Worksheets[SheetCount].AutoFitColumns();
                    //wb.Worksheets[SheetCount].AutoFitRows();
                    break;
                //    //新生
                //case 3:
                //    if (NewStudent.Instance.SelectedList.Count == 0)
                //    {
                //        MessageBox.Show("沒有選擇任何一個學生");
                //        return;
                //    }                    
                //    row = 0;
                //    PrintTitle = "  " + K12.Data.School.ChineseName + "  " + intSchoolYear.Value + " 學年度繳費明細表"; //+ JHSchool.School.ChineseName
                //    wb.Worksheets[SheetCount].PageSetup.SetHeader(1, PrintTitle);
                //    wb.Worksheets[SheetCount].Cells[row, 0].PutValue("科別");
                //    wb.Worksheets[SheetCount].Cells.SetColumnWidth(0, 10);
                //    wb.Worksheets[SheetCount].Cells[row, 1].PutValue("編號");
                //    wb.Worksheets[SheetCount].Cells.SetColumnWidth(1, 9);
                //    wb.Worksheets[SheetCount].Cells[row, 2].PutValue("姓名");
                //    wb.Worksheets[SheetCount].Cells.SetColumnWidth(2, 13);
                //    wb.Worksheets[SheetCount].Cells[row, 3].PutValue("性別");
                //    wb.Worksheets[SheetCount].Cells.SetColumnWidth(3, 4.38);
                //    wb.Worksheets[SheetCount].Cells[row, 4].PutValue("註冊費");
                //    wb.Worksheets[SheetCount].Cells.SetColumnWidth(4, 10);
                //    wb.Worksheets[SheetCount].Cells[row, 5].PutValue("異動金額");
                //    wb.Worksheets[SheetCount].Cells.SetColumnWidth(5, 10);
                //    wb.Worksheets[SheetCount].Cells[row, 6].PutValue("應收金額");
                //    wb.Worksheets[SheetCount].Cells.SetColumnWidth(6, 10);
                //    wb.Worksheets[SheetCount].Cells[row, 7].PutValue("繳費日期");
                //    wb.Worksheets[SheetCount].Cells.SetColumnWidth(7, 10);
                //    wb.Worksheets[SheetCount].Cells[row, 8].PutValue("備註");
                //    wb.Worksheets[SheetCount].Cells.SetColumnWidth(8, 20);
                //    wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 20);
                //    wb.Worksheets[SheetCount].PageSetup.PrintTitleRows = "$1:$1"; //設定跨頁標題
                //    //設定邊界
                //    wb.Worksheets[SheetCount].PageSetup.BottomMargin = 1.5;
                //    wb.Worksheets[SheetCount].PageSetup.TopMargin = 2.5;
                //    wb.Worksheets[SheetCount].PageSetup.HeaderMargin = 1;
                //    wb.Worksheets[SheetCount].PageSetup.LeftMargin = 1;
                //    wb.Worksheets[SheetCount].PageSetup.RightMargin = 1;
                //    row = 1;                    
                //    foreach (NewStudentRecord nsr in NewStudent.Instance.SelectedList)
                //        stuList.Add(nsr.UID);
                //    srList = StudentTuitionDAO.GetStudentTuitionBySSTSL (intSchoolYear.Value, cboSemester.Text, "新生", stuList);
                //    foreach (StudentTuitionRecord sr in srList)
                //    {
                //        if (!strList.ContainsKey(sr.TuitionUID))
                //        {
                //            strList.Add(sr.TuitionUID, new List<StudentTuitionRecord>());
                //            strList[sr.TuitionUID].Add(sr);
                //        }
                //        strIDs.Add(sr.UID);
                //    }
                //    tdr = TuitionDetailDAO.GetTuitionDetailByIDs(strIDs);
                //    foreach (TuitionDetailRecord tr in tdr)
                //    {
                //        if (tdrList.ContainsKey(tr.STUID))
                //            tdrList[tr.STUID].Add(tr);
                //        else
                //        {
                //            tdrList.Add(tr.STUID, new List<TuitionDetailRecord>());
                //            tdrList[tr.STUID].Add(tr);
                //        }
                //    }
                //    foreach (NewStudentRecord nsr in NewStudent.Instance.SelectedList)
                //    {
                        
                //        MotherForm.SetStatusBarMessage("正在產生報表", nowSet++*100 / NewStudent.Instance.SelectedList.Count );
                //        if (strList.ContainsKey(nsr.UID))
                //        {
                //            wb.Worksheets[SheetCount].Cells.SetRowHeight(row, 30);                            
                //            wb.Worksheets[SheetCount].Cells[row, 0].PutValue(nsr.Dept);
                //            wb.Worksheets[SheetCount].Cells[row, 1].PutValue((nsr.Active ? "" : "*") + nsr.Number);
                //            wb.Worksheets[SheetCount].Cells[row, 2].PutValue(nsr.Name);
                //            wb.Worksheets[SheetCount].Cells[row, 3].PutValue(nsr.Gender);
                //            wb.Worksheets[SheetCount].Cells[row, 4].PutValue(strList[nsr.UID][0].ChargeAmount - strList[nsr.UID][0].ChangeMoney);
                //            wb.Worksheets[SheetCount].Cells[row, 4].Style.Custom = "#,##0_ ";
                //            wb.Worksheets[SheetCount].Cells[row, 5].PutValue(strList[nsr.UID][0].ChangeMoney);
                //            wb.Worksheets[SheetCount].Cells[row, 5].Style.Custom = "#,##0_ ";
                //            wb.Worksheets[SheetCount].Cells[row, 6].PutValue(strList[nsr.UID][0].ChargeAmount);
                //            wb.Worksheets[SheetCount].Cells[row, 6].Style.Custom = "#,##0_ ";
                //            //wb.Worksheets[SheetCount].Cells[row, 6].PutValue(strList[nsr.UID][0].PayDate);
                //            //wb.Worksheets[SheetCount].Cells[row, 6].Style.Custom = "e.mm.dd";
                //            if (strList[nsr.UID][0].PayDate != null)
                //                wb.Worksheets[SheetCount].Cells[row, 7].PutValue(strList[nsr.UID][0].PayDate.Value.Year - 1911 + "." + strList[nsr.UID][0].PayDate.Value.Month + "." + strList[nsr.UID][0].PayDate.Value.Day);
                //            //if (strList[nsr.UID][0].ChangeMoney != 0)
                //            //{                                
                //                string strChange = "";
                //                if (tdrList.ContainsKey(strList[nsr.UID][0].UID))
                //                {
                //                    foreach (TuitionDetailRecord tr in tdrList[strList[nsr.UID][0].UID])
                //                        strChange += tr.TCSName + "：" + tr.ChangeAmount + "\n";
                //                    if (strChange != "")
                //                    {
                //                        wb.Worksheets[SheetCount].Cells[row, 8].PutValue(strChange.Substring(0, strChange.Length - 1));
                //                        wb.Worksheets[SheetCount].Cells[row, 8].Style.ShrinkToFit = true;
                //                        wb.Worksheets[SheetCount].Cells[row, 8].Style.IsTextWrapped = true;
                //                        wb.Worksheets[SheetCount].Cells[row, 8].Style.Font.Size = 8;
                //                    }
                //                }
                //            //}
                //            row += 1;
                //        }
                //    }
                //    for (int i = 0; i < row; i++)
                //    {
                //        for (int j = 0; j < 9; j++)
                //        {
                //            if (j < 4)
                //                wb.Worksheets[SheetCount].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Center;
                //            else
                //                if (j == 8)
                //                    wb.Worksheets[SheetCount].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Left;
                //                else
                //                    wb.Worksheets[SheetCount].Cells[i, j].Style.HorizontalAlignment = TextAlignmentType.Right;
                //            wb.Worksheets[SheetCount].Cells[i, j].Style.VerticalAlignment = TextAlignmentType.Center;
                //            wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                //            wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                //            wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                //            wb.Worksheets[SheetCount].Cells[i, j].Style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                //        }

                //    }
                //    wb.Worksheets[SheetCount].AutoFitColumns();
                //    //wb.Worksheets[SheetCount].AutoFitRows();
                //    break;               
            }            
            //刪除第一張工作表
            if (wb.Worksheets.Count > 1)
                wb.Worksheets.RemoveAt(0);
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\繳費明細表.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\繳費明細表.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "繳費明細表.xls";
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
        
    }
}
