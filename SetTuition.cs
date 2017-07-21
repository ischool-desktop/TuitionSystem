using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using TuitionSystem.Data;
using FISCA.UDT;
using System.Windows.Forms;
//using MySchoolModule;
using SHSchool.Data;
using System.ComponentModel;
namespace TuitionSystem
{
    public class SetTuition
    {
        //設定收費標準副程式，傳入學生類別,收費標準名稱，學生識別碼
        public static Boolean SetTuitionStandard(string StudentType,string TuitionStdName,string TuitionUID,string Gender)
        {
              //找出收費標準
              List<TuitionStandardRecord> TSRs = TuitionStandardDAO.GetTuitionStandardBySST(GlobalValue.CurrentSchoolYear,GlobalValue.CurrentSemester,TuitionStdName) ;
              if (TSRs.Count == 0)
                  return false;
              
              if (TSRs[0].Gender == "全" || TSRs[0].Gender == Gender)
              {
                  //計算繳費金額
                  int Money = CalcTuition(TSRs);
                  //檢查是否已有繳費表
                  List<StudentTuitionRecord> Strs = StudentTuitionDAO.GetStudentTuitionBySSTS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, StudentType, TuitionUID);
                  if (Strs.Count == 0)
                  {
                      //無繳費表，新增繳費表
                      StudentTuitionRecord sr = new StudentTuitionRecord();
                      sr.SchoolYear = GlobalValue.CurrentSchoolYear;
                      sr.Semester = (GlobalValue.CurrentSemester == "上學期" ? 1 : 2);
                      sr.StudentType = StudentType;
                      sr.TSName = TuitionStdName;
                      sr.TuitionUID = TuitionUID;
                      sr.PayDate = null;
                      sr.Payment = 0;
                      sr.ChargeAmount = Money;
                      sr.ChangeMoney = 0;
                      sr.UpLoad = false;
                      sr.Save();
                      return true;
                  }
                  else
                  {
                      //有繳費表
                      foreach (StudentTuitionRecord pstr in Strs)
                      {
                          //檢查是否註冊
                          if (pstr.PayDate == null)
                          {
                              //計算異動金額
                              int ChangeMoney = CalcChangeMoney(TSRs, pstr.UID);
                              //判斷應繳金額是否小於0
                              if ((Money + ChangeMoney) >= 0)
                              {
                                  pstr.TSName = TuitionStdName;
                                  pstr.ChargeAmount = Money + ChangeMoney;
                                  pstr.ChangeMoney = ChangeMoney;
                                  pstr.UpLoad = false;
                                  pstr.Save();
                                  //更正所有異動金額
                                  ChangeTuitionDetail(TSRs, pstr.UID);
                                  return true;
                              }
                              else
                              {
                                  MessageBox.Show("應繳金額小於0，不能修改收費標準");
                                  return false;
                              }
                          }
                          else
                          {
                              MessageBox.Show("已註冊不可更改收費標準");
                              return false;
                          }
                      }
                  }
              }
              else
              {
                  MessageBox.Show("收費標準內性別不符合");
                  return false;
              }
              return true;         
        }
        //計算收費標準內學費金額總計，傳入收費標準
        public static int CalcTuition(List<TuitionStandardRecord> TSRs)
        {
            int Money = 0;
            foreach (TuitionStandardRecord tsr in TSRs)
                Money += tsr.Money;
            return Money;
        }
        //設定繳費表副程式，傳入學生類別,收費標準，學生識別碼
        public static void SetStudentTuition(string StudentType, List<TuitionStandardRecord> TuitionStd, string TuitionUID)
        {
            //計算繳費金額
            int Money = CalcTuition(TuitionStd);
            StudentTuitionRecord sr = new StudentTuitionRecord();
            sr.SchoolYear = GlobalValue.CurrentSchoolYear;
            sr.Semester = (GlobalValue.CurrentSemester == "上學期" ? 1 : 2);
            sr.StudentType = StudentType;
            sr.TSName = TuitionStd[0].TSName;
            sr.TuitionUID = TuitionUID;
            sr.PayDate = null;
            sr.Payment = 0;
            sr.ChargeAmount = Money;
            sr.ChangeMoney = 0;
            sr.UpLoad = false;
            sr.Save();
        }
        //重新計算繳費表內所有異動金額總計，傳入收費標準及繳費表UID
        public static int CalcChangeMoney(List<TuitionStandardRecord> TSRs, string STUID)
        {
            List<TuitionDetailRecord> TDRs = TuitionDetailDAO.GetTuitionDetailByUID(STUID);
            int ChangeMoney=0;
            decimal thisChangeMoney = 0;
            foreach (TuitionDetailRecord tdr in TDRs)
            {
                thisChangeMoney = 0;
                List<TuitionChangeStdRecord> TCSRs = TuitionChangeStdDAO.GetTuitionChangeStdBySST(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, tdr.TCSName);
                foreach (TuitionChangeStdRecord tcsr in TCSRs)
                    foreach (TuitionStandardRecord tsr in TSRs)
                        if (tcsr.ChargeItem == tsr.ChargeItem)
                            if (tcsr.MoneyType == "＋")
                                thisChangeMoney = thisChangeMoney + (decimal)(tsr.Money * tcsr.Percent) / 100;
                            else
                                thisChangeMoney = thisChangeMoney - (decimal)(tsr.Money * tcsr.Percent) / 100;
                thisChangeMoney = Math.Round(thisChangeMoney, 0,MidpointRounding.AwayFromZero);               
                if (TCSRs.Count > 0)
                    if (TCSRs[0].MoneyType == "＋")
                        thisChangeMoney += TCSRs[0].Money;
                    else
                        thisChangeMoney -= TCSRs[0].Money;
                //異動金額計算結果為0時，不更正原金額,因金額是使用者輸入的
                if (thisChangeMoney == 0)
                    ChangeMoney += tdr.ChangeAmount;
                else
                    ChangeMoney += decimal.ToInt32(thisChangeMoney);
            }           
            return ChangeMoney; 

        }
        //更正所有繳費表內異動金額，傳入收費標準及繳費表UID
        public static void ChangeTuitionDetail(List<TuitionStandardRecord> TSRs,string STUID)
        {
            List<TuitionDetailRecord> TDRs = TuitionDetailDAO.GetTuitionDetailByUID(STUID);
            decimal ChangeMoney = 0;
            foreach (TuitionDetailRecord tdr in TDRs)
            {
                List<TuitionChangeStdRecord> TCSRs = TuitionChangeStdDAO.GetTuitionChangeStdBySST(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, tdr.TCSName);
                foreach (TuitionChangeStdRecord tcsr in TCSRs)
                    foreach (TuitionStandardRecord tsr in TSRs)
                        if (tcsr.ChargeItem == tsr.ChargeItem)
                            if (tcsr.MoneyType == "＋")
                                ChangeMoney = ChangeMoney + (decimal)(tsr.Money * tcsr.Percent) / 100;
                            else
                                ChangeMoney = ChangeMoney - (decimal)(tsr.Money * tcsr.Percent) / 100;
                ChangeMoney = Math.Round(ChangeMoney, 0, MidpointRounding.AwayFromZero);
                if (TCSRs.Count > 0)
                    if (TCSRs[0].MoneyType == "＋")
                        ChangeMoney += TCSRs[0].Money;
                    else
                        ChangeMoney -= TCSRs[0].Money;
                //異動金額計算結果為0時，不更正原金額,因金額是使用者輸入的
                if (ChangeMoney!=0)
                   tdr.ChangeAmount = decimal.ToInt32(ChangeMoney);
                ChangeMoney = 0;
            }
            TDRs.SaveAll();   //using Framework.Data
        }
        //設定異動標準，傳入收費標準，異動名稱，繳費表
        public static Boolean SetChangeStd(List<TuitionStandardRecord> TSRs,string TCSName, StudentTuitionRecord sr)
        {
            
            List<TuitionDetailRecord> TDRs = TuitionDetailDAO.GetTuitionDetailByUT(sr.UID,TCSName);
            if (TDRs.Count == 0)
            {
                TuitionDetailRecord tdr = new TuitionDetailRecord();
                tdr.SchoolYear = GlobalValue.CurrentSchoolYear;
                tdr.Semester = (GlobalValue.CurrentSemester=="上學期" ? 1:2);
                tdr.TCSName = TCSName;
                tdr.ChangeAmount = CalcChangeMoneyByTCSName(TSRs, sr.UID, TCSName);
                tdr.STUID = sr.UID;
                if ((sr.ChargeAmount + tdr.ChangeAmount) < 0)
                {
                    MessageBox.Show("異動將造成負值，不更正");
                    return false;
                }
                else
                {
                    tdr.Save();
                    sr.ChangeMoney = sr.ChangeMoney + tdr.ChangeAmount;
                    sr.ChargeAmount = sr.ChargeAmount + tdr.ChangeAmount;
                    sr.UpLoad = false;
                    sr.Save();
                    return true;
                }
            }
            else
            {
                MessageBox.Show("異動名稱已存在");
                return false;
            }

            
        }
        //計算異動項目金額
        public static int CalcChangeMoneyByTCSName(List<TuitionStandardRecord> TSRs, string STUID,string TCSName)
        {
            List<TuitionDetailRecord> TDRs = TuitionDetailDAO.GetTuitionDetailByUID(STUID);
            decimal ChangeMoney = 0;            
            List<TuitionChangeStdRecord> TCSRs = TuitionChangeStdDAO.GetTuitionChangeStdBySST(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, TCSName);
            foreach (TuitionChangeStdRecord tcsr in TCSRs)
                 foreach (TuitionStandardRecord tsr in TSRs)
                     if (tcsr.ChargeItem == tsr.ChargeItem)
                         if (tcsr.MoneyType == "＋")
                             ChangeMoney = ChangeMoney + (decimal)(tsr.Money * tcsr.Percent) / 100;
                         else
                             ChangeMoney = ChangeMoney - (decimal)(tsr.Money * tcsr.Percent) / 100;
            ChangeMoney = Math.Round(ChangeMoney,0,MidpointRounding.AwayFromZero);
            if (TCSRs.Count > 0)
                if (TCSRs[0].MoneyType == "＋")
                    ChangeMoney += TCSRs[0].Money;
                else
                    ChangeMoney -= TCSRs[0].Money;
            
            return decimal.ToInt32(ChangeMoney);

        }
        //加總繳費表內異動金額，傳入繳費表UID
        public static int ReCalcChangeMoney(string STUID)
        {
            List<TuitionDetailRecord> TDRs = TuitionDetailDAO.GetTuitionDetailByUID(STUID);
            decimal ChangeMoney = 0;
            foreach (TuitionDetailRecord tdr in TDRs)
            {
                ChangeMoney += tdr.ChangeAmount;
            }
            return decimal.ToInt32(ChangeMoney);

        }
        public static void AutoSetTuitionStandard(int Kind)
        {

            if (MessageBox.Show("自動產生繳費表工作費時，確定嗎?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<string> IDs = new List<string>();
                //找出收費標準
                List<TuitionStandardRecord> trs = TuitionStandardDAO.GetTuitionStandardBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
                Dictionary<string, List<TuitionStandardRecord>> DeptTuitSDs = new Dictionary<string, List<TuitionStandardRecord>>();
                Dictionary<string, List<TuitionStandardRecord>> TSRDics=new Dictionary<string,List<TuitionStandardRecord>>();
                foreach (TuitionStandardRecord tr in trs)
                {
                    if (!TSRDics.ContainsKey(tr.TSName))
                    {
                        if (DeptTuitSDs.ContainsKey(tr.Dept))
                        {
                            DeptTuitSDs[tr.Dept].Add(tr);
                        }
                        else
                        {
                            DeptTuitSDs.Add(tr.Dept, new List<TuitionStandardRecord>());
                            DeptTuitSDs[tr.Dept].Add(tr);
                        }
                        TSRDics.Add(tr.TSName, new List<TuitionStandardRecord>());
                    } 
                    TSRDics[tr.TSName].Add(tr);
                }                
                switch (Kind)
                {
                    case 1:
                        if (K12.Presentation.NLDPanels.Student.SelectedSource.Count == 0)
                        {
                            MessageBox.Show("沒有選擇任一個學生，請選擇學生");
                            return;
                        }
                        //找出選擇的學生
                        List<SHSchool.Data.SHStudentRecord> students = SHStudent.SelectByIDs(K12.Presentation.NLDPanels.Student.SelectedSource); 
                        //找出繳費表
                        List<StudentTuitionRecord> sts = StudentTuitionDAO.GetStudentTuitionBySST(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, "舊生");
                        //紀錄已有繳費表之學生
                        foreach (StudentTuitionRecord st in sts)
                            if (!IDs.Contains(st.TuitionUID))
                                IDs.Add(st.TuitionUID);
                        {
                            Framework.MultiThreadBackgroundWorker<SHStudentRecord> mBKW = new Framework.MultiThreadBackgroundWorker<SHStudentRecord>();
                            FISCA.Presentation.MotherForm.SetStatusBarMessage("正在設定收費標準...", 0);
                            mBKW.DoWork += delegate(object sender2, Framework.PackageDoWorkEventArgs<SHStudentRecord> e2)
                            {
                                lock (mBKW)
                                {
                                    foreach (SHSchool.Data.SHStudentRecord stu in e2.Items)
                                    {
                                        if (stu.Status == SHSchool.Data.SHStudentRecord.StudentStatus.一般)
                                        {
                                            //自動設定沒有繳費表之同學
                                            if (!IDs.Contains(stu.ID))
                                            {
                                                string dept = (stu.Department == null ? "" : stu.Department.FullName);
                                                string ClassYear = "";
                                                switch (stu.Class.GradeYear)
                                                {
                                                    case 1:
                                                        ClassYear = "一年級";
                                                        break;
                                                    case 2:
                                                        ClassYear = "二年級";
                                                        break;
                                                    case 3:
                                                        ClassYear = "三年級";
                                                        break;
                                                }
                                                if (DeptTuitSDs.ContainsKey(dept))
                                                {
                                                    foreach (TuitionStandardRecord tr in DeptTuitSDs[dept])
                                                        if ((stu.Gender == tr.Gender || tr.Gender == "全") && ClassYear == tr.ClassYear)
                                                        {
                                                            SetTuition.SetStudentTuition("舊生", TSRDics[tr.TSName], stu.ID);
                                                            IDs.Add(stu.ID);
                                                            break;
                                                        }
                                                }
                                            }
                                        }
                                    }
                                }
                            };
                            mBKW.ProgressChanged += delegate(object sender3, ProgressChangedEventArgs e3)
                            {
                                FISCA.Presentation.MotherForm.SetStatusBarMessage("設定收費標準中...", e3.ProgressPercentage);
                            };
                            mBKW.RunWorkerCompleted += delegate(object sender4, RunWorkerCompletedEventArgs e4)
                            {
                                Program.OnTuitionChanged();
                                FISCA.Presentation.MotherForm.SetStatusBarMessage("");
                                MessageBox.Show("設定完成");
                            };
                            //設定包的大小,1為人數
                            mBKW.PackageSize = 10;
                            mBKW.RunWorkerAsync(students);
                        }
                        //foreach (SHSchool.Data.SHStudentRecord stu in students)
                        //{
                        //    FISCA.Presentation.MotherForm.SetStatusBarMessage("設定收費標準中....", nowSet++ * 100 / students.Count);
                        //    if (stu.Status == SHSchool.Data.SHStudentRecord.StudentStatus.一般)
                        //    {
                        //        //自動設定沒有繳費表之同學
                        //        if (!IDs.Contains(stu.ID))
                        //        {
                        //            string dept = (stu.Department == null ? "" : stu.Department.FullName);
                        //            string ClassYear="";
                        //            switch (stu.Class.GradeYear)
                        //            {
                        //                case 1:
                        //                    ClassYear = "一年級";
                        //                    break;
                        //                case 2:
                        //                    ClassYear = "二年級";
                        //                    break;
                        //                case 3:
                        //                    ClassYear = "三年級";
                        //                    break;
                        //            }                                    
                        //            foreach (TuitionStandardRecord tr in DeptTuitSDs[dept])
                        //                if ((stu.Gender == tr.Gender || tr.Gender == "全") && ClassYear == tr.ClassYear)
                        //                {
                        //                    SetTuition.SetStudentTuition("舊生", TSRDics[tr.TSName], stu.ID);
                        //                    IDs.Add(stu.ID);
                        //                    break;
                        //                }
                        //        }
                        //    }
                        //}
                        //FISCA.Presentation.MotherForm.SetStatusBarMessage("設定完成");
                        //Program.OnTuitionChanged();
                        break;
                //    case 2:
                //        if (NewStudent.Instance.SelectedList.Count == 0)
                //        {
                //            MessageBox.Show("沒有選擇任何一個學生");
                //            return;
                //        } 
                //        //找出繳費表
                //        sts = StudentTuitionDAO.GetStudentTuitionBySST(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, "新生");
                //        foreach (StudentTuitionRecord st in sts)
                //            if (!IDs.Contains(st.TuitionUID))
                //                IDs.Add(st.TuitionUID);
                //        {
                //            Framework.MultiThreadBackgroundWorker<NewStudentRecord> mBKW = new Framework.MultiThreadBackgroundWorker<NewStudentRecord>();
                //            FISCA.Presentation.MotherForm.SetStatusBarMessage("正在設定收費標準...", 0);
                //            mBKW.DoWork += delegate(object sender2, Framework.PackageDoWorkEventArgs<NewStudentRecord> e2)
                //            {
                //                lock (mBKW)
                //                {
                //                    foreach (NewStudentRecord nsr in e2.Items)
                //                    {
                //                        if (nsr.Active)
                //                        {
                //                            //自動設定沒有繳費表之同學
                //                            if (!IDs.Contains(nsr.UID))
                //                            {
                //                                if (DeptTuitSDs.ContainsKey(nsr.Dept))
                //                                {
                //                                    foreach (TuitionStandardRecord tr in DeptTuitSDs[nsr.Dept])
                //                                        if ((nsr.Gender == tr.Gender || tr.Gender == "全") && tr.ClassYear == "一年級")
                //                                        {
                //                                            SetTuition.SetStudentTuition("新生", TSRDics[tr.TSName], nsr.UID);
                //                                            IDs.Add(nsr.UID);
                //                                            break;
                //                                        }
                //                                }
                //                            }
                //                        }
                //                    }
                //                }
                //            };
                //            mBKW.ProgressChanged += delegate(object sender3, ProgressChangedEventArgs e3)
                //            {
                //                FISCA.Presentation.MotherForm.SetStatusBarMessage("設定收費標準中...", e3.ProgressPercentage);
                //            };
                //            mBKW.RunWorkerCompleted += delegate(object sender4, RunWorkerCompletedEventArgs e4)
                //            {
                //                Program.OnTuitionChanged();
                //                FISCA.Presentation.MotherForm.SetStatusBarMessage("");
                //                MessageBox.Show("設定完成");
                //            };
                //            //設定包的大小,1為人數
                //            mBKW.PackageSize = 10;
                //            mBKW.RunWorkerAsync(NewStudent.Instance.SelectedList);
                //        }
                //        //foreach (NewStudentRecord nsr in NewStudent.Instance.SelectedList)
                //        //{
                //        //    FISCA.Presentation.MotherForm.SetStatusBarMessage("設定收費標準中....", nowSet++ * 100 / NewStudent.Instance.SelectedList.Count);
                //        //    if (nsr.Active)
                //        //    {
                //        //        //自動設定沒有繳費表之同學
                //        //        if (!IDs.Contains(nsr.UID))
                //        //        {
                //        //            foreach (TuitionStandardRecord tr in DeptTuitSDs[nsr.Dept])
                //        //                if (nsr.Gender == tr.Gender && tr.ClassYear == "一年級")
                //        //                {
                //        //                    SetTuition.SetStudentTuition("新生", TSRDics[tr.TSName], nsr.UID);
                //        //                    IDs.Add(nsr.UID);
                //        //                    break;                                            
                //        //                }
                //        //        }
                //        //    }
                //        //}
                //        //FISCA.Presentation.MotherForm.SetStatusBarMessage("設定完成");
                //        //NewStudent.Instance.SyncDataBackground(IDs);
                //        break;
                }
            }
        }
    }
}
