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
using FISCA.UDT;

using FISCA.Presentation;
using SHSchool.Data;

namespace TuitionSystem.TuitionControls
{
    public partial class TuitionChangeList : BaseForm
    {
        public TuitionChangeList()
        {
            InitializeComponent();
        }

        private void lnkSelAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < SelectView.Items.Count; i++)
            {
                SelectView.Items[i].Checked = true;
            }
        }

        private void lnkCancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < SelectView.Items.Count; i++)
            {
                SelectView.Items[i].Checked = false;
            }
        }

        private void TuitionChangeList_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;           
            this.SelectView.Items.Clear();
            List<TuitionChangeStdRecord> tcrs= new List<TuitionChangeStdRecord>();
            tcrs = TuitionChangeStdDAO.GetTuitionChangeStdBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
            foreach (TuitionChangeStdRecord tcr in tcrs)
                if (!SelectView.Items.ContainsKey(tcr.TCSName))
                    SelectView.Items.Add(tcr.TCSName, tcr.TCSName,"");

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            List<String> PrintList = new List<string>();
            for (int i = 0; i < SelectView.Items.Count; i++)
            {
                if (SelectView.Items[i].Checked)
                    PrintList.Add(SelectView.Items[i].Text);
            }
            if (PrintList.Count == 0)
            {
                MessageBox.Show("沒有選擇任何一項異動");
                return;
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
            int Totrow = 2;
            int SheetCount = 1;
            int nowSet = 0;
            string PrintTitle;
            List<TuitionDetailRecord> Tdrs = new List<TuitionDetailRecord>();
            List<StudentTuitionRecord> StudentTU = new List<StudentTuitionRecord>();
            Dictionary<string, List<TuitionDetailRecord>> TDRDic = new Dictionary<string, List<TuitionDetailRecord>>();
            Dictionary<string, List<StudentTuitionRecord>> strList = new Dictionary<string, List<StudentTuitionRecord>>();
            StudentTU = StudentTuitionDAO.GetStudentTuitionBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
            //先Cache
            SHStudent.SelectAll();
            foreach (StudentTuitionRecord sr in StudentTU)
            {
                if (!strList.ContainsKey(sr.UID))
                {
                    strList.Add(sr.UID, new List<StudentTuitionRecord>());
                    strList[sr.UID].Add(sr);
                }                
            }
            Tdrs = TuitionDetailDAO.GetTuitionDetailBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
            foreach (TuitionDetailRecord tdr in Tdrs)
            {
                if (TDRDic.ContainsKey(tdr.TCSName))
                    TDRDic[tdr.TCSName].Add(tdr);
                else
                {
                    TDRDic.Add(tdr.TCSName, new List<TuitionDetailRecord>());
                    TDRDic[tdr.TCSName].Add(tdr);
                }
            }
            PrintTitle = K12.Data.School.ChineseName + GlobalValue.CurrentSchoolYear + "學年度" + GlobalValue.CurrentSemester + "異動清冊總表";
            wb.Worksheets[0].Cells[0, 0].PutValue(PrintTitle);
            wb.Worksheets[0].Cells.Merge(0, 0, 1, 9);
            wb.Worksheets[0].Cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[0, 0].Style.VerticalAlignment = TextAlignmentType.Center;
            wb.Worksheets[0].Cells[0, 0].Style.Font.Size = 16;
            wb.Worksheets[0].Cells.SetRowHeight(0, 25);
            wb.Worksheets[0].Name = "總表";
            wb.Worksheets[0].Cells[1, 0].PutValue("學生類別");
            wb.Worksheets[0].Cells[1, 1].PutValue("科別/班別");
            wb.Worksheets[0].Cells[1, 2].PutValue("編號/學號");
            wb.Worksheets[0].Cells[1, 3].PutValue("姓名");
            wb.Worksheets[0].Cells[1, 4].PutValue("性別");
            wb.Worksheets[0].Cells[1, 5].PutValue("身份證字號");
            wb.Worksheets[0].Cells[1, 6].PutValue("異動金額");
            wb.Worksheets[0].Cells[1, 7].PutValue("註冊");
            wb.Worksheets[0].Cells[1, 8].PutValue("異動項目名稱");
            wb.Worksheets[0].Cells.SetRowHeight(1, 20);
            foreach (string str in PrintList)
            {
                MotherForm.SetStatusBarMessage("正在產生報表", nowSet++ * 100 / PrintList.Count);
                if (!TDRDic.ContainsKey(str))
                    continue;
                wb.Worksheets.Add();
                wb.Worksheets[SheetCount].Name = str;
                PrintTitle = K12.Data.School.ChineseName + GlobalValue.CurrentSchoolYear + "學年度" + GlobalValue.CurrentSemester + str + "異動清冊";
                wb.Worksheets[SheetCount].Cells[0, 0].PutValue(PrintTitle);
                wb.Worksheets[SheetCount].Cells.Merge(0, 0, 1, 8);
                wb.Worksheets[SheetCount].Cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                wb.Worksheets[SheetCount].Cells[0, 0].Style.VerticalAlignment = TextAlignmentType.Center;
                wb.Worksheets[SheetCount].Cells[0, 0].Style.Font.Size = 16;
                wb.Worksheets[SheetCount].Cells.SetRowHeight(0, 25);                
                wb.Worksheets[SheetCount].Cells[1,0].PutValue("學生類別");
                wb.Worksheets[SheetCount].Cells[1,1].PutValue("科別/班別");
                wb.Worksheets[SheetCount].Cells[1,2].PutValue("編號/學號");
                wb.Worksheets[SheetCount].Cells[1,3].PutValue("姓名");
                wb.Worksheets[SheetCount].Cells[1,4].PutValue("性別");
                wb.Worksheets[SheetCount].Cells[1,5].PutValue("身份證字號");
                wb.Worksheets[SheetCount].Cells[1,6].PutValue("異動金額");
                wb.Worksheets[SheetCount].Cells[1,7].PutValue("註冊");
                wb.Worksheets[SheetCount].Cells.SetRowHeight(1, 20);
                row = 2;
                foreach (TuitionDetailRecord tdr in TDRDic[str])
                {
                    if (strList.ContainsKey(tdr.STUID))
                    { 
                        wb.Worksheets[SheetCount].Cells[row, 0].PutValue(strList[tdr.STUID][0].StudentType);
                        wb.Worksheets[SheetCount].Cells.SetRowHeight(row, 20);
                        //總表
                        wb.Worksheets[0].Cells[Totrow, 0].PutValue(strList[tdr.STUID][0].StudentType);
                        wb.Worksheets[0].Cells.SetRowHeight(Totrow, 20);
                        //if (strList[tdr.STUID][0].StudentType == "新生")
                        //{
                        //    //總表
                        //    wb.Worksheets[0].Cells[Totrow, 1].PutValue(NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].Dept);
                        //    wb.Worksheets[0].Cells[Totrow, 2].PutValue((NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].Active? "":"*")+NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].Number);
                        //    wb.Worksheets[0].Cells[Totrow, 3].PutValue(NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].Name);
                        //    wb.Worksheets[0].Cells[Totrow, 4].PutValue(NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].Gender);
                        //    wb.Worksheets[0].Cells[Totrow, 5].PutValue(NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].IDNumber);
                        //    wb.Worksheets[0].Cells[Totrow, 6].PutValue(tdr.ChangeAmount);
                        //    if (strList[tdr.STUID][0].PayDate == null)
                        //        wb.Worksheets[0].Cells[Totrow, 7].PutValue("未註冊");
                        //    else
                        //        wb.Worksheets[0].Cells[Totrow, 7].PutValue("註冊");
                            
                        //    //個別表
                        //    wb.Worksheets[SheetCount].Cells[row, 1].PutValue(NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].Dept);
                        //    wb.Worksheets[SheetCount].Cells[row, 2].PutValue((NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].Active? "":"*")+NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].Number);
                        //    wb.Worksheets[SheetCount].Cells[row, 3].PutValue(NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].Name);
                        //    wb.Worksheets[SheetCount].Cells[row, 4].PutValue(NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].Gender);
                        //    wb.Worksheets[SheetCount].Cells[row, 5].PutValue(NewStudent.Instance.Items[strList[tdr.STUID][0].TuitionUID].IDNumber);
                        //    wb.Worksheets[SheetCount].Cells[row, 6].PutValue(tdr.ChangeAmount);
                        //    if (strList[tdr.STUID][0].PayDate == null)
                        //        wb.Worksheets[SheetCount].Cells[row, 7].PutValue("未註冊");
                        //    else
                        //        wb.Worksheets[SheetCount].Cells[row, 7].PutValue("註冊");
                        //}
                        //else
                        //{
                            //總表
                            if (SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).Class!=null)
                               wb.Worksheets[0].Cells[Totrow, 1].PutValue(SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).Class.Name);
                            wb.Worksheets[0].Cells[Totrow, 2].PutValue((SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).Status== SHStudentRecord.StudentStatus.一般? "":"*")+SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).StudentNumber);
                            wb.Worksheets[0].Cells[Totrow, 3].PutValue(SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).Name);
                            wb.Worksheets[0].Cells[Totrow, 4].PutValue(SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).Gender);
                            wb.Worksheets[0].Cells[Totrow, 5].PutValue(SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).IDNumber);
                            wb.Worksheets[0].Cells[Totrow, 6].PutValue(tdr.ChangeAmount);
                            if (strList[tdr.STUID][0].PayDate == null)
                                wb.Worksheets[0].Cells[Totrow, 7].PutValue("未註冊");
                            else
                                wb.Worksheets[0].Cells[Totrow, 7].PutValue("註冊");

                            if (SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).Class != null)
                                wb.Worksheets[SheetCount].Cells[row, 1].PutValue(SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).Class.Name);
                            wb.Worksheets[SheetCount].Cells[row, 2].PutValue((SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).Status==SHStudentRecord.StudentStatus.一般? "":"*")+SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).StudentNumber);
                            wb.Worksheets[SheetCount].Cells[row, 3].PutValue(SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).Name);
                            wb.Worksheets[SheetCount].Cells[row, 4].PutValue(SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).Gender);
                            wb.Worksheets[SheetCount].Cells[row, 5].PutValue(SHStudent.SelectByID(strList[tdr.STUID][0].TuitionUID).IDNumber);
                            wb.Worksheets[SheetCount].Cells[row, 6].PutValue(tdr.ChangeAmount);
                            if (strList[tdr.STUID][0].PayDate == null)
                                wb.Worksheets[SheetCount].Cells[row, 7].PutValue("未註冊");
                            else
                                wb.Worksheets[SheetCount].Cells[row, 7].PutValue("註冊");

                        //}
                        //總表
                        wb.Worksheets[0].Cells[Totrow, 8].PutValue(str);
                        row++;
                        Totrow++;                        
                    }
                }
                // 畫表
                
                tmpMaxRow = wb.Worksheets[SheetCount].Cells.MaxDataRow;
                tmpMaxCol = wb.Worksheets[SheetCount].Cells.MaxDataColumn + 1;
                if (tmpMaxCol!=0 && tmpMaxRow!=0)
                   wb.Worksheets[SheetCount].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
                //自動調整欄寬
                wb.Worksheets[SheetCount].AutoFitColumns();
                SheetCount++;
            }
            // 畫表
            
            tmpMaxRow = wb.Worksheets[0].Cells.MaxDataRow;
            tmpMaxCol = wb.Worksheets[0].Cells.MaxDataColumn + 1;
            if (tmpMaxCol != 0 && tmpMaxRow != 0)
                wb.Worksheets[0].Cells.CreateRange(1, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
            wb.Worksheets[0].PageSetup.Orientation = PageOrientationType.Landscape;//設定橫向列印
            wb.Worksheets[0].PageSetup.PrintTitleRows = "$1:$2"; //設定跨頁標題
            wb.Worksheets[0].PageSetup.FitToPagesWide = 1;   //調整為一頁寬 
            wb.Worksheets[0].PageSetup.FitToPagesTall = (row - (row % 35)) / 35 + 1;   //調整頁高
            //設定邊界
            wb.Worksheets[0].PageSetup.BottomMargin = 1;
            wb.Worksheets[0].PageSetup.TopMargin = 1;
            wb.Worksheets[0].PageSetup.LeftMargin = 1;
            wb.Worksheets[0].PageSetup.RightMargin = 1;
            //自動調整欄寬
            wb.Worksheets[0].AutoFitColumns();            
            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\異動項目清冊.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\異動項目清冊.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "異動項目清冊.xls";
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
