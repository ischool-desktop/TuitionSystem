using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using TuitionSystem.Data;
using Aspose.Cells;
using System.Windows.Forms;
using FISCA.Presentation;
//using MySchoolModule;
using SHSchool.Data;

namespace TuitionSystem
{
    class TuitionReport
    {
        public static void TuitionStdList()
        {
            Workbook wb = new Workbook();
            Style defaultStyle = wb.DefaultStyle;
            defaultStyle.Font.Name = "標楷體";
            defaultStyle.Font.Size = 10;
            wb.DefaultStyle = defaultStyle;
            int row = 2;
            int col = 0;
            int SheetCount = 0;
            int nowSet = 0;
            List<string> PrintTitle = new List<string>();
            List<TuitionStandardRecord> TuitionStd = new List<TuitionStandardRecord>();
            TuitionStd = TuitionStandardDAO.GetTuitionStandardBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
            Dictionary<string, List<TuitionStandardRecord>> TuitionStdList = new Dictionary<string, List<TuitionStandardRecord>>();
            foreach (TuitionStandardRecord tsr in TuitionStd)
            {
                if (!TuitionStdList.ContainsKey(tsr.TSName))
                    TuitionStdList.Add(tsr.TSName, new List<TuitionStandardRecord>());
                TuitionStdList[tsr.TSName].Add(tsr);
            }
            wb.Worksheets[SheetCount].Name = "學費標準一覽表";
            wb.Worksheets[SheetCount].Cells[0, 0].PutValue(K12.Data.School.ChineseName + GlobalValue.CurrentSchoolYear + "學年度" + GlobalValue.CurrentSemester + "學費標準一覽表");
            wb.Worksheets[SheetCount].Cells[1, 0].PutValue("收費標準名稱");
            foreach (string stdName in TuitionStdList.Keys)
            {
                MotherForm.SetStatusBarMessage("正在產生報表", nowSet++ * 100 / TuitionStdList.Count);
                wb.Worksheets[SheetCount].Cells[row, 0].PutValue(stdName);
                if (TuitionStdList.ContainsKey(stdName))
                {
                    foreach (TuitionStandardRecord tr in TuitionStdList[stdName])
                    {
                        if (!PrintTitle.Contains(tr.ChargeItem))
                        {
                            PrintTitle.Add(tr.ChargeItem);
                            wb.Worksheets[SheetCount].Cells[1, PrintTitle.IndexOf(tr.ChargeItem) + 1].PutValue(tr.ChargeItem);
                        }
                        col = PrintTitle.IndexOf(tr.ChargeItem) + 1;
                        wb.Worksheets[SheetCount].Cells[row, col].PutValue(tr.Money);
                        wb.Worksheets[SheetCount].Cells[row, col].Style.Custom = "#,##0_ ";
                    }
                }
                row++;
            }
            col = PrintTitle.Count+1;
            //合併列印
            wb.Worksheets[SheetCount].Cells.Merge(0, 0, 1, col+1);
            wb.Worksheets[SheetCount].Cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[SheetCount].Cells[0, 0].Style.VerticalAlignment = TextAlignmentType.Center;
            wb.Worksheets[SheetCount].Cells[0, 0].Style.Font.Size = 18;
            wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 25);
            wb.Worksheets[SheetCount].Cells[1, col].PutValue("總    計");
            for (int i = 2; i < row; i++)
            {
                wb.Worksheets[SheetCount].Cells[i, col].Formula = "=SUM(B" + (i + 1) + ":" + chr(col) + (i + 1) + ")";
                wb.Worksheets[SheetCount].Cells[i, col].Style.Custom = "#,##0_ ";
            }
            // 畫表

            Style st2 = wb.Styles[wb.Styles.Add()];
            StyleFlag sf2 = new StyleFlag();
            sf2.Borders = true;

            st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            int tmpMaxRow = 0, tmpMaxCol = 0;
            tmpMaxRow = wb.Worksheets[0].Cells.MaxDataRow;
            tmpMaxCol = wb.Worksheets[0].Cells.MaxDataColumn + 1;
            if (tmpMaxRow >= 1) 
               wb.Worksheets[0].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            //自動調整欄寬
            wb.Worksheets[0].AutoFitColumns();
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\學費標準一覽表.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\學費標準一覽表.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "學費標準一覽表.xls";
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

        private static string chr(int p)
        {
            string English = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (p < 27)
                return English.Substring(p - 1, 1);
            else
                return "";

        }
        public static void ChangeMoneyErrorList()
        {
            List<StudentTuitionRecord> TuitionRs = new List<StudentTuitionRecord>();
            TuitionRs = StudentTuitionDAO.GetStudentTuitionBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
            List<TuitionDetailRecord> ChangeRs = new List<TuitionDetailRecord>();
            ChangeRs = TuitionDetailDAO.GetTuitionDetailBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
            Dictionary<string, List<TuitionDetailRecord>> dicTD = new Dictionary<string, List<TuitionDetailRecord>>();
            foreach (TuitionDetailRecord tr in ChangeRs)
            {
                if (!dicTD.ContainsKey(tr.STUID))
                    dicTD.Add(tr.STUID, new List<TuitionDetailRecord>());
                dicTD[tr.STUID].Add(tr);
            }
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

            PrintTitle = K12.Data.School.ChineseName + GlobalValue.CurrentSchoolYear + "學年度" + GlobalValue.CurrentSemester + "異動金額總計錯誤名冊";
           
            wb.Worksheets[0].Cells[0, 0].PutValue(PrintTitle);
            wb.Worksheets[0].Cells.Merge(0, 0, 1, 11);
            wb.Worksheets[0].Cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[0, 0].Style.VerticalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[0, 0].Style.Font.Size = 16;
            wb.Worksheets[0].Cells[0, 0].Style.IsTextWrapped = true;
            wb.Worksheets[0].Cells.SetRowHeight(0, 25);           
            wb.Worksheets[0].Name = "名冊";
            wb.Worksheets[0].Cells[1, 0].PutValue("學生類別");
            wb.Worksheets[0].Cells[1, 1].PutValue("科別/班別");
            wb.Worksheets[0].Cells[1, 2].PutValue("編號/學號");
            wb.Worksheets[0].Cells[1, 3].PutValue("姓名");
            wb.Worksheets[0].Cells[1, 4].PutValue("性別");
            wb.Worksheets[0].Cells[1, 5].PutValue("總計金額");
            wb.Worksheets[0].Cells[1, 6].PutValue("異動金額");
            wb.Worksheets[0].Cells[1, 7].PutValue("正確異動金額");
            wb.Worksheets[0].Cells[1, 8].PutValue("應收金額");
            wb.Worksheets[0].Cells[1, 9].PutValue("正確應收金額");            
            wb.Worksheets[0].Cells[1, 10].PutValue("備    註");
            wb.Worksheets[0].Cells.SetRowHeight(1, 20);
            row = 2;            
            int nowSet = 0;
            int ChangeMoney=0;
            foreach (StudentTuitionRecord sr in TuitionRs)
            {
                MotherForm.SetStatusBarMessage("正在產生報表", nowSet++ * 100 / TuitionRs.Count);
                ChangeMoney=0;
                if (!dicTD.ContainsKey(sr.UID))
                    continue;
                foreach (TuitionDetailRecord tr in dicTD[sr.UID])                
                    ChangeMoney+=tr.ChangeAmount;
                if (sr.ChangeMoney!=ChangeMoney)
                {
                    wb.Worksheets[0].Cells[row, 0].PutValue(sr.StudentType);
                    if (sr.StudentType == "舊生")
                    {
                        wb.Worksheets[0].Cells[row, 1].PutValue(SHStudent.SelectByID(sr.TuitionUID).Class.Name);
                        wb.Worksheets[0].Cells[row, 2].PutValue((SHStudent.SelectByID(sr.TuitionUID).Status == SHStudentRecord.StudentStatus.一般 ? "" : "*") + SHStudent.SelectByID(sr.TuitionUID).StudentNumber);
                        wb.Worksheets[0].Cells[row, 3].PutValue(SHStudent.SelectByID(sr.TuitionUID).Name);
                        wb.Worksheets[0].Cells[row, 4].PutValue(SHStudent.SelectByID(sr.TuitionUID).Gender);
                    }
                    //else
                    //{
                    //    wb.Worksheets[0].Cells[row, 1].PutValue(NewStudent.Instance.Items[sr.TuitionUID].Dept);
                    //    wb.Worksheets[0].Cells[row, 2].PutValue((NewStudent.Instance.Items[sr.TuitionUID].Active ? "" : "*") + NewStudent.Instance.Items[sr.TuitionUID].Number);
                    //    wb.Worksheets[0].Cells[row, 3].PutValue(NewStudent.Instance.Items[sr.TuitionUID].Name);
                    //    wb.Worksheets[0].Cells[row, 4].PutValue(NewStudent.Instance.Items[sr.TuitionUID].Gender);
                    //}
                    wb.Worksheets[0].Cells[row, 5].PutValue(sr.ChargeAmount - sr.ChangeMoney);
                    wb.Worksheets[0].Cells[row, 6].PutValue(sr.ChangeMoney);
                    wb.Worksheets[0].Cells[row, 7].PutValue(ChangeMoney);
                    wb.Worksheets[0].Cells[row, 8].PutValue(sr.ChargeAmount);
                    wb.Worksheets[0].Cells[row, 9].PutValue(sr.ChargeAmount-sr.ChangeMoney+ChangeMoney);
                    
                    string strChange = "";
                    if (dicTD.ContainsKey(sr.UID))
                    {
                        foreach (TuitionDetailRecord tr in dicTD[sr.UID])
                            strChange += tr.TCSName + "：" + tr.ChangeAmount + "\n";
                    }
                    if (strChange != "")
                    {
                        wb.Worksheets[0].Cells[row, 10].PutValue(strChange.Substring(0, strChange.Length - 1));
                        wb.Worksheets[0].Cells[row, 10].Style.ShrinkToFit = true;
                        wb.Worksheets[0].Cells[row, 10].Style.IsTextWrapped = true;
                        wb.Worksheets[0].Cells[row, 10].Style.Font.Size = 8;
                    }
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
                wb.Save(Application.StartupPath + "\\Reports\\異動金額總計錯誤名冊列印.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\異動金額總計錯誤名冊列印.xls");            
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";               
                sd1.FileName = "異動金額總計錯誤名冊列印.xls";
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
