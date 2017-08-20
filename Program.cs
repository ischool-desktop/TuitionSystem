using System;
using System.Collections.Generic;
using System.Text;
using FISCA.Presentation;
using System.ComponentModel;
using FISCA.UDT;
using System.Windows.Forms;
using System.Drawing;
//using MySchoolModule;
using TuitionSystem.Data;
using TuitionSystem.TuitionControls;
using FISCA;
using K12.Data;
using SHSchool.Data;
using FISCA.Permission;
using SmartSchool;
using System.Reflection;

namespace TuitionSystem
{
    public class Program
    {
        [MainMethod()]
        public static void Main()
        {
            if (CurrentUser.Acl["Tuition023"].Executable)
            {
                if (DateTime.Today.Month < 6)
                {
                    GlobalValue.CurrentSchoolYear = DateTime.Today.Year - 1912;
                    GlobalValue.CurrentSemester = "下學期";
                }
                if (DateTime.Today.Month >= 6 && DateTime.Today.Month < 12)
                {
                    GlobalValue.CurrentSchoolYear = DateTime.Today.Year - 1911;
                    GlobalValue.CurrentSemester = "上學期";
                }
                if (DateTime.Today.Month == 12)
                {
                    GlobalValue.CurrentSchoolYear = DateTime.Today.Year - 1911;
                    GlobalValue.CurrentSemester = "下學期";
                }

                bool isAE = false;

                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.GetName().Name == "FISCA")
                    {
                        if (assembly.GetName().Version.Major >= 2)
                            isAE = true;
                        break;
                    }
                    //Trace.WriteLine("name:" + assembly.GetName().Name + "  Version.Major:" + assembly.GetName().Version.Major);
                }



                //string username = Framework.DSAServices.UserAccount;
                //List<string> allowusername = new List<string>(new string[] { "emywang", "bw2014", "guest1", "mimimi" });
                //if (allowusername.Contains(username))
                //{
                var botton2 = MotherForm.RibbonBarItems["學費", "目前學期"].Controls["目前學期"].Control = new TuitionSystem.TuitionControls.SetCurrentSemester();
                var botton1 = MotherForm.RibbonBarItems["學費", "學費資料管理"];
                Catalog ribbon = RoleAclSource.Instance["學費"]["學費資料管理"];
                ribbon.Add(new RibbonFeature("Tuition001", "收費項目維護"));
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費項目維護"].Enable = CurrentUser.Acl["Tuition001"].Executable;
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費項目維護"].Image = Properties.Resources.j0439824;
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費項目維護"].Click += delegate
                {
                    (new ChargeItem_Process()).ShowDialog();
                };
                ribbon = RoleAclSource.Instance["學費"]["學費資料管理"];
                ribbon.Add(new RibbonFeature("Tuition002", "收費標準維護"));
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費標準維護"].Enable = CurrentUser.Acl["Tuition002"].Executable;
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費標準維護"].Image = Properties.Resources.Cash_sheet;
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費標準維護"].Click += delegate
                {
                    (new TuitionStandardProcess()).ShowDialog();
                };
                ribbon = RoleAclSource.Instance["學費"]["學費資料管理"];
                ribbon.Add(new RibbonFeature("Tuition003", "收費異動維護"));
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費異動維護"].Enable = CurrentUser.Acl["Tuition003"].Executable;
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費異動維護"].Image = Properties.Resources.Money;
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費異動維護"].Click += delegate
                {
                    (new TuitionChangeStdProcess()).ShowDialog();
                };
                ribbon = RoleAclSource.Instance["學費"]["學費資料管理"];
                ribbon.Add(new RibbonFeature("Tuition設定線上輸入時間", "設定線上輸入時間"));
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["設定線上輸入時間"].Enable = CurrentUser.Acl["Tuition設定線上輸入時間"].Executable;
                //MotherForm.RibbonBarItems["學費", "學費資料管理"]["設定線上輸入時間"].Image = Properties.Resources.Money;
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["設定線上輸入時間"].Click += delegate
                {
                    (new TuitionChangeInputTime()).ShowDialog();
                };
                ribbon = RoleAclSource.Instance["學費"]["學費資料管理"];
                ribbon.Add(new RibbonFeature("Tuition計算繳費金額", "計算繳費金額"));
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["計算繳費金額"].Enable = CurrentUser.Acl["Tuition計算繳費金額"].Executable;
                //MotherForm.RibbonBarItems["學費", "學費資料管理"]["計算繳費金額"].Image = Properties.Resources.Money;
                MotherForm.RibbonBarItems["學費", "學費資料管理"]["計算繳費金額"].Click += delegate
                {
                    if (GlobalValue.CurrentSchoolYear == 0 || GlobalValue.CurrentSemester == null)
                    {
                        MessageBox.Show("未設定學年度學期");
                    }
                    else
                    {
                        int schoolYear = GlobalValue.CurrentSchoolYear;
                        int semester = GlobalValue.CurrentSemester == "上學期" ? 1 : 2;
                        BackgroundWorker bkw = new BackgroundWorker();
                        bkw.WorkerReportsProgress = true;
                        bkw.DoWork += delegate
                        {
                            bkw.ReportProgress(1);
                            AccessHelper accessHelper = new AccessHelper();
                            var tuitionStandardList = accessHelper.Select<TuitionStandardRecord>("學年度=" + schoolYear + " AND 學期=" + semester);
                            var tuitionChangeStdList = accessHelper.Select<TuitionChangeStdRecord>("學年度=" + schoolYear + " AND 學期=" + semester);
                            var studentTuitionList = accessHelper.Select<StudentTuitionRecord>("學年度=" + schoolYear + " AND 學期=" + semester);

                            bkw.ReportProgress(10);
                            List<StudentTuitionRecord> package = new List<StudentTuitionRecord>();
                            for (int i = 0; i < studentTuitionList.Count; i++)
                            {
                                package.Add(studentTuitionList[i]);
                                if (i + 1 == studentTuitionList.Count || package.Count == 200)
                                {
                                    //整理收費表收費標準資料
                                    Dictionary<string, Dictionary<string, int>> moneyDetail = new Dictionary<string, Dictionary<string, int>>();
                                    foreach (var item in package)
                                    {
                                        moneyDetail.Add(item.UID, new Dictionary<string, int>());
                                        foreach (var stdItem in tuitionStandardList)
                                        {
                                            if (stdItem.TSName == item.TSName)
                                            {
                                                if (!moneyDetail[item.UID].ContainsKey(stdItem.ChargeItem))
                                                {
                                                    moneyDetail[item.UID].Add(stdItem.ChargeItem, 0);
                                                }
                                                moneyDetail[item.UID][stdItem.ChargeItem] += stdItem.Money;
                                            }
                                        }
                                    };
                                    //整理收費異動資料
                                    var packageTuituin = accessHelper.Select<TuitionDetailRecord>("收費表 in ('" + string.Join("','", moneyDetail.Keys) + "')");
                                    Dictionary<string, List<TuitionDetailRecord>> tuitionDetail = new Dictionary<string, List<TuitionDetailRecord>>();
                                    foreach (var item in packageTuituin)
                                    {
                                        if (!tuitionDetail.ContainsKey(item.STUID))
                                            tuitionDetail.Add(item.STUID, new List<TuitionDetailRecord>());
                                        tuitionDetail[item.STUID].Add(item);
                                    }
                                    //計算異動金額、收費表金額
                                    foreach (var item in package)
                                    {
                                        //計算原始金額
                                        int moneyCount = 0;
                                        foreach (var key in moneyDetail[item.UID].Keys)
                                        {
                                            moneyCount += moneyDetail[item.UID][key];
                                        }
                                        item.ChargeAmount = moneyCount;
                                        //計算異動項目金額
                                        if (tuitionDetail.ContainsKey(item.UID))
                                        {
                                            //減免金額直接扣抵
                                            foreach (var changeItem in tuitionDetail[item.UID])
                                            {
                                                foreach (var changeSTDItem in tuitionChangeStdList)
                                                {
                                                    if (changeSTDItem.TCSName == changeItem.TCSName)
                                                    {
                                                        //異動項目直接扣錢
                                                        if (changeSTDItem.Money != 0)
                                                        {
                                                            //正負值校正
                                                            if (changeSTDItem.MoneyType != "＋" && changeItem.ChangeAmount > 0)
                                                                changeItem.ChangeAmount = changeItem.ChangeAmount * -1;
                                                            if (changeSTDItem.MoneyType == "＋" && changeItem.ChangeAmount < 0)
                                                                changeItem.ChangeAmount = changeItem.ChangeAmount * -1;
                                                            if (moneyDetail[item.UID].ContainsKey(changeSTDItem.ChargeItem))
                                                            {
                                                                moneyDetail[item.UID][changeSTDItem.ChargeItem] += changeItem.ChangeAmount;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            //減免金額百分比計算
                                            foreach (var changeItem in tuitionDetail[item.UID])
                                            {
                                                bool isPercent = false;
                                                int changeMoney = 0;
                                                foreach (var changeSTDItem in tuitionChangeStdList)
                                                {
                                                    if (changeSTDItem.TCSName == changeItem.TCSName)
                                                    {
                                                        //異動項目使用百分比
                                                        if (changeSTDItem.Money == 0)
                                                        {
                                                            isPercent = true;
                                                            if (moneyDetail[item.UID].ContainsKey(changeSTDItem.ChargeItem))
                                                            {
                                                                changeMoney += moneyDetail[item.UID][changeSTDItem.ChargeItem] * changeSTDItem.Percent / 100 * (changeSTDItem.MoneyType == "＋" ? 1 : -1);
                                                            }
                                                        }
                                                    }
                                                }
                                                if (isPercent)
                                                    changeItem.ChangeAmount = changeMoney;
                                            }
                                            //總計異動金額
                                            int changeCount = 0;
                                            foreach (var changeItem in tuitionDetail[item.UID])
                                            {
                                                changeCount += changeItem.ChangeAmount;
                                            }
                                            item.ChangeMoney = changeCount;
                                            item.ChargeAmount += changeCount;
                                        }
                                    }
                                    bkw.ReportProgress(10 + 90 * (i + 1) / studentTuitionList.Count);
                                    package.SaveAll();
                                    packageTuituin.SaveAll();
                                    package.Clear();
                                }
                            }
                        };
                        bkw.ProgressChanged += delegate (object sender, ProgressChangedEventArgs e)
                        {
                            MotherForm.SetStatusBarMessage("繳費金額計算中...", e.ProgressPercentage);
                        };
                        bkw.RunWorkerCompleted += delegate
                        {
                            MotherForm.SetStatusBarMessage("繳費金額計算完成。");
                            MessageBox.Show("繳費金額計算完成。");
                        };
                        bkw.RunWorkerAsync();
                    }
                };

                //ribbon = RoleAclSource.Instance["學費"]["學費資料管理"];
                //ribbon.Add(new RibbonFeature("Tuition004", "收費模組設定"));
                //MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費模組設定"].Enable = CurrentUser.Acl["Tuition004"].Executable;
                //MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費模組設定"].Image = Properties.Resources.Bank_64;
                //MotherForm.RibbonBarItems["學費", "學費資料管理"]["收費模組設定"].Click += delegate
                //{
                //    (new PaymentSet()).ShowDialog();
                //};


                //舊生
                ribbon = RoleAclSource.Instance["學費"]["設定繳費資料"];
                ribbon.Add(new RibbonFeature("Tuition005", "設定收費標準"));
                RibbonBarButton rbi1 = K12.Presentation.NLDPanels.Student.RibbonBarItems["設定繳費資料"]["設定收費標準"];
                rbi1.Enable = CurrentUser.Acl["Tuition005"].Executable;
                rbi1.Image = Properties.Resources.list_icon;
                //設定動態的按鈕
                rbi1.SupposeHasChildern = true;
                //           然候註冊PopupOpen事件
                //PopupOpen事件裡頭有e.VitrulButtons
                //把動態要加的項目加在e.VitrulButtons裡
                //var btn1=e.VitrulButtons["Btn1"];
                rbi1.PopupOpen += new EventHandler<PopupOpenEventArgs>(rbi1_PopupOpen);
                ribbon = RoleAclSource.Instance["學費"]["設定繳費資料"];
                ribbon.Add(new RibbonFeature("Tuition006", "設定異動標準"));
                RibbonBarButton rbi2 = K12.Presentation.NLDPanels.Student.RibbonBarItems["設定繳費資料"]["設定異動標準"];
                rbi2.Enable = CurrentUser.Acl["Tuition006"].Executable;
                rbi2.Image = Properties.Resources.Tag_ToDo_List_icon;
                rbi2.SupposeHasChildern = true;
                rbi2.PopupOpen += new EventHandler<PopupOpenEventArgs>(rbi2_PopupOpen);

                ////新生
                //RibbonBarButton rbi3 = NewStudent.Instance.RibbonBarItems["設定繳費資料"]["設定收費標準"];
                //rbi3.Enable = CurrentUser.Acl["Tuition005"].Executable;
                //rbi3.Image = Properties.Resources.list_icon;
                //rbi3.SupposeHasChildern = true;
                //rbi3.PopupOpen += new EventHandler<PopupOpenEventArgs>(rbi3_PopupOpen);

                //RibbonBarButton rbi4 = NewStudent.Instance.RibbonBarItems["設定繳費資料"]["設定異動標準"];
                //rbi4.Enable = CurrentUser.Acl["Tuition006"].Executable;
                //rbi4.Image = Properties.Resources.Tag_ToDo_List_icon;
                //rbi4.SupposeHasChildern = true;
                //rbi4.PopupOpen += new EventHandler<PopupOpenEventArgs>(rbi4_PopupOpen);
                //ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                //ribbon.Add(new RibbonFeature("Tuition007", "繳費明細單"));
                //MenuButton rbi5 = NewStudent.Instance.RibbonBarItems["報表及統計"]["報表"]["學費相關報表"]["繳費明細單"];
                //rbi5.Enable = CurrentUser.Acl["Tuition007"].Executable;
                //rbi5.Click += delegate
                //{
                //    (new TuitionDetailSheet()).ShowDialog();
                //};
                MenuButton rbi6 = null;
                if (isAE)
                    rbi6 = K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["總務相關報表"]["繳費明細單"];
                else
                    rbi6 = K12.Presentation.NLDPanels.Class.RibbonBarItems["統計報表"]["報表"]["總務相關報表"]["繳費明細單"];
                rbi6.Enable = CurrentUser.Acl["Tuition007"].Executable;
                rbi6.Click += delegate
                {
                    (new TuitionDetailSheet_P(1)).ShowDialog();
                };
                //ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                //ribbon.Add(new RibbonFeature("Tuition008", "繳費明細單(依選取學生)"));
                //MenuButton rbi7 = NewStudent.Instance.RibbonBarItems["報表及統計"]["報表"]["學費相關報表"]["繳費明細單(依選取學生)"];
                //rbi7.Enable = CurrentUser.Acl["Tuition008"].Executable;
                //rbi7.Click += delegate
                //{
                //    (new TuitionDetailSheet_P(3)).ShowDialog();
                //};
                MenuButton rbi8 = null;
                if (isAE)
                    rbi8 = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["報表"]["總務相關報表"]["繳費明細單(依選取學生)"];
                else
                    rbi8 = K12.Presentation.NLDPanels.Student.RibbonBarItems["統計報表"]["報表"]["總務相關報表"]["繳費明細單(依選取學生)"];
                rbi8.Enable = CurrentUser.Acl["Tuition008"].Executable;
                rbi8.Click += delegate
                {

                    (new TuitionDetailSheet_P(2)).ShowDialog();
                };

                MotherForm.RibbonBarItems["學費", "報表及統計"]["報表"].Image = Properties.Resources.folder_red_mydocuments;
                ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                ribbon.Add(new RibbonFeature("Tuition009", "收費標準一覽表"));
                MenuButton rbi10 = MotherForm.RibbonBarItems["學費", "報表及統計"]["報表"]["收費標準一覽表"];
                rbi10.Enable = CurrentUser.Acl["Tuition009"].Executable;
                rbi10.Click += delegate
                {
                    TuitionReport.TuitionStdList();
                };
                ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                ribbon.Add(new RibbonFeature("Tuition010", "異動項目清冊"));
                MenuButton rbi11 = MotherForm.RibbonBarItems["學費", "報表及統計"]["報表"]["異動項目清冊"];
                rbi11.Enable = CurrentUser.Acl["Tuition010"].Executable;
                rbi11.Click += delegate
                {
                    (new TuitionChangeList()).ShowDialog();
                };
                ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                ribbon.Add(new RibbonFeature("Tuition011", "繳費項目明細清冊"));
                MenuButton rbi12 = null;
                if (isAE)
                    rbi12 = K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["總務相關報表"]["繳費項目明細清冊"];
                else
                    rbi12 = K12.Presentation.NLDPanels.Class.RibbonBarItems["統計報表"]["報表"]["總務相關報表"]["繳費項目明細清冊"];
                rbi12.Enable = CurrentUser.Acl["Tuition011"].Executable;
                rbi12.Click += delegate
                {
                    (new PermRecRegReport(1)).ShowDialog();
                };
                ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                ribbon.Add(new RibbonFeature("Tuition012", "各項繳費及異動項目統計清冊"));
                MenuButton rbi13 = null;
                if (isAE)
                    rbi13 = K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["總務相關報表"]["各項繳費及異動項目統計清冊"];
                else
                    rbi13 = K12.Presentation.NLDPanels.Class.RibbonBarItems["統計報表"]["報表"]["總務相關報表"]["各項繳費及異動項目統計清冊"];
                rbi13.Enable = CurrentUser.Acl["Tuition012"].Executable;
                rbi13.Click += delegate
                {
                    (new PermRecRegReport(2)).ShowDialog();
                };
                //ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                //ribbon.Add(new RibbonFeature("Tuition013", "註冊及未註冊人數暨註冊金額統計表"));
                //MenuButton rbi14 = null;
                //if (isAE)
                //    rbi14 = K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["總務相關報表"]["註冊及未註冊人數暨註冊金額統計表"];
                //else
                //    rbi14 = K12.Presentation.NLDPanels.Class.RibbonBarItems["統計報表"]["報表"]["總務相關報表"]["註冊及未註冊人數暨註冊金額統計表"];
                //rbi14.Enable = CurrentUser.Acl["Tuition013"].Executable;
                //rbi14.Click += delegate
                //{
                //    (new PermRecRegReport(3)).ShowDialog();
                //};
                //ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                //ribbon.Add(new RibbonFeature("Tuition014", "應收金額與實收金額不符者名冊列印"));
                //MenuButton rbi15 = MotherForm.RibbonBarItems["學費", "報表及統計"]["報表"]["應收金額與實收金額不符者名冊列印"];
                //rbi15.Enable = CurrentUser.Acl["Tuition014"].Executable;
                //rbi15.Click += delegate
                //{
                //    (new PermRecRegReport(4)).ShowDialog();
                //};
                ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                ribbon.Add(new RibbonFeature("Tuition015", "應收金額小於零名冊列印"));
                MenuButton rbi16 = MotherForm.RibbonBarItems["學費", "報表及統計"]["報表"]["應收金額小於零名冊列印"];
                rbi16.Enable = CurrentUser.Acl["Tuition015"].Executable;
                rbi16.Click += delegate
                {
                    (new PermRecRegReport(5)).ShowDialog();
                };

                //MenuButton rbi17 = NewStudent.Instance.RibbonBarItems["報表及統計"]["報表"]["學費相關報表"]["繳費項目明細清冊"];
                //rbi17.Enable = CurrentUser.Acl["Tuition011"].Executable;
                //rbi17.Click += delegate
                //{
                //    (new NewStudentRegReport(1)).ShowDialog();
                //};

                //MenuButton rbi18 = NewStudent.Instance.RibbonBarItems["報表及統計"]["報表"]["學費相關報表"]["各項繳費及異動項目統計清冊"];
                //rbi18.Enable = CurrentUser.Acl["Tuition012"].Executable;
                //rbi18.Click += delegate
                //{
                //    (new NewStudentRegReport(2)).ShowDialog();
                //};

                //MenuButton rbi19 = NewStudent.Instance.RibbonBarItems["報表及統計"]["報表"]["學費相關報表"]["註冊及未註冊人數暨註冊金額統計表"];
                //rbi19.Enable = CurrentUser.Acl["Tuition013"].Executable;
                //rbi19.Click += delegate
                //{
                //    (new NewStudentRegReport(3)).ShowDialog();
                //};
                //ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                //ribbon.Add(new RibbonFeature("Tuition016", "繳費單列印"));
                //MenuButton rbi20 = null;
                //if (isAE)
                //    rbi20 = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["報表"]["總務相關報表"]["繳費單列印"];
                //else
                //    rbi20 = K12.Presentation.NLDPanels.Student.RibbonBarItems["統計報表"]["報表"]["總務相關報表"]["繳費單列印"];
                //rbi20.Enable = CurrentUser.Acl["Tuition016"].Executable;
                //rbi20.Click += delegate
                //{
                //    (new PaymentSheet(1)).ShowDialog();
                //};
                //MenuButton rbi21 = NewStudent.Instance.RibbonBarItems["報表及統計"]["報表"]["學費相關報表"]["繳費單列印"];
                //rbi21.Enable = CurrentUser.Acl["Tuition016"].Executable;
                //rbi21.Click += delegate
                //{
                //    (new PaymentSheet(2)).ShowDialog();
                //};
                //ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                //ribbon.Add(new RibbonFeature("Tuition017", "繳費收據"));
                //MenuButton rbi22 = NewStudent.Instance.RibbonBarItems["報表及統計"]["報表"]["學費相關報表"]["繳費收據"];
                //rbi22.Enable = CurrentUser.Acl["Tuition017"].Executable;
                //rbi22.Click += delegate
                //{
                //    (new ReceiptReports(2)).ShowDialog();
                //};

                //MenuButton rbi23 = null;
                //if (isAE)
                //    rbi23 = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["報表"]["總務相關報表"]["繳費收據"];
                //else
                //    rbi23 = K12.Presentation.NLDPanels.Student.RibbonBarItems["統計報表"]["報表"]["總務相關報表"]["繳費收據"];
                //rbi23.Enable = CurrentUser.Acl["Tuition017"].Executable;
                //rbi23.Click += delegate
                //{
                //    (new ReceiptReports(1)).ShowDialog();
                //};
                //ribbon = RoleAclSource.Instance["學費"]["設定繳費資料"];
                //ribbon.Add(new RibbonFeature("Tuition018", "自動設定收費標準"));
                //MenuButton rbi24 = NewStudent.Instance.RibbonBarItems["設定繳費資料"]["自動設定收費標準"];
                //rbi24.Image = Properties.Resources.MoneyTransportation;
                //rbi24.Enable = CurrentUser.Acl["Tuition018"].Executable;
                //rbi24.Click += delegate
                //{
                //    SetTuition.AutoSetTuitionStandard(2);
                //};
                MenuButton rbi25 = K12.Presentation.NLDPanels.Student.RibbonBarItems["設定繳費資料"]["自動設定收費標準"];
                rbi25.Image = Properties.Resources.MoneyTransportation;
                rbi25.Enable = CurrentUser.Acl["Tuition018"].Executable;
                rbi25.Click += delegate
                {
                    SetTuition.AutoSetTuitionStandard(1);
                };
                //ribbon = RoleAclSource.Instance["學費"]["帳務管理"];
                //ribbon.Add(new RibbonFeature("Tuition019", "對帳處理"));
                //MenuButton rbi27 = MotherForm.RibbonBarItems["學費", "帳務管理"]["對帳處理"];
                //rbi27.Image = Properties.Resources.記帳_小;
                //rbi27.Enable = CurrentUser.Acl["Tuition019"].Executable;
                //rbi27.Click += delegate
                //{
                //    (new TuitionAccount()).ShowDialog();
                //};
                ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                ribbon.Add(new RibbonFeature("Tuition020", "異動金額總計錯誤名冊列印"));
                MenuButton rbi26 = MotherForm.RibbonBarItems["學費", "報表及統計"]["報表"]["異動金額總計錯誤名冊列印"];
                rbi26.Enable = CurrentUser.Acl["Tuition020"].Executable;
                rbi26.Click += delegate
                {
                    TuitionReport.ChangeMoneyErrorList();
                };
                //ribbon = RoleAclSource.Instance["學費"]["註冊處理"];
                //ribbon.Add(new RibbonFeature("Tuition021", "手動批次註冊"));
                //MenuButton rbi28 = K12.Presentation.NLDPanels.Student.RibbonBarItems["註冊處理"]["手動批次註冊"];
                //rbi28.Enable = CurrentUser.Acl["Tuition021"].Executable;
                //rbi28.Click += delegate
                //{
                //    (new AutoRegisted(1)).ShowDialog();
                //};
                //MenuButton rbi29 = NewStudent.Instance.RibbonBarItems["報到及註冊處理"]["手動批次註冊"];
                //rbi29.Enable = CurrentUser.Acl["Tuition021"].Executable;
                //rbi29.Click += delegate
                //{
                //    (new AutoRegisted(2)).ShowDialog();
                //};
                //ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                //ribbon.Add(new RibbonFeature("Tuition022", "未註冊學生名單列印"));
                //MenuButton rbi30 = null;
                //if (isAE)
                //    rbi30 = K12.Presentation.NLDPanels.Class.RibbonBarItems["資料統計"]["報表"]["總務相關報表"]["未註冊學生名單列印"];
                //else
                //    rbi30 = K12.Presentation.NLDPanels.Class.RibbonBarItems["統計報表"]["報表"]["總務相關報表"]["未註冊學生名單列印"];
                //rbi30.Enable = CurrentUser.Acl["Tuition022"].Executable;
                //rbi30.Click += delegate
                //{
                //    (new PermRecRegReport(6)).ShowDialog();
                //};
                ribbon = RoleAclSource.Instance["學費"]["學費基本資料"];
                ribbon.Add(new DetailItemFeature("Tuition024", "學費基本資料"));
                if (CurrentUser.Acl["Tuition024"].Viewable || CurrentUser.Acl["Tuition024"].Editable)
                {
                    ////Add 毛毛蟲
                    //NewStudent.Instance.AddDetailBulider<NewStudent_Tuition>();
                    //Add 毛毛蟲
                    K12.Presentation.NLDPanels.Student.AddDetailBulider<PermRec_Tuition>();
                }
                //在新生系統增加一欄收費標準
                ribbon = RoleAclSource.Instance["學費"]["學費相關欄位"];
                ribbon.Add(new RibbonFeature("Tuition025", "學費相關欄位"));
                if (CurrentUser.Acl["Tuition025"].Executable)
                {
                    ListPaneField nameField = new ListPaneField("收費標準");
                    Dictionary<string, StudentTuitionRecord> items = new Dictionary<string, StudentTuitionRecord>();
                    //nameField.PreloadVariableBackground += delegate(object sender, PreloadVariableEventArgs e)
                    //{
                    //    items.Clear();
                    //    List<string> ids = new List<string>();
                    //    foreach (var item in e.Keys)
                    //        ids.Add(item);
                    //    foreach (var item in StudentTuitionDAO.GetStudentTuitionBySSTSL(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, "新生", ids))
                    //    {
                    //        if (!items.ContainsKey(item.TuitionUID))
                    //           items.Add(item.TuitionUID, item);
                    //    }
                    //    System.Threading.Thread.Sleep(5000);
                    //};
                    //nameField.GetVariable += delegate(object sender, GetVariableEventArgs e)
                    //{
                    //    if (items.ContainsKey(e.Key))
                    //        e.Value = items[e.Key].TSName;
                    //    else
                    //        e.Value = "";
                    //};
                    //nameField.MinimumWidth = 120;
                    //NewStudent.Instance.AddListPaneField(nameField);
                    ////在新生系統增加一欄應繳金額
                    //nameField = new ListPaneField("應繳金額");
                    //Dictionary<string, StudentTuitionRecord> items1 = new Dictionary<string, StudentTuitionRecord>();
                    //nameField.PreloadVariableBackground += delegate(object sender, PreloadVariableEventArgs e)
                    //{
                    //    items1.Clear();
                    //    List<string> ids = new List<string>();
                    //    foreach (var item in e.Keys)
                    //        ids.Add(item);
                    //    foreach (var item in StudentTuitionDAO.GetStudentTuitionBySSTSL(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, "新生", ids))
                    //    {
                    //        if (!items1.ContainsKey(item.TuitionUID))
                    //             items1.Add(item.TuitionUID, item);
                    //        else
                    //            MessageBox.Show(NewStudent.Instance.Items[item.TuitionUID].Name + "繳費表重覆");
                    //    }
                    //    System.Threading.Thread.Sleep(5000);
                    //};
                    //nameField.GetVariable += delegate(object sender, GetVariableEventArgs e)
                    //{
                    //    if (items1.ContainsKey(e.Key))
                    //        e.Value = items1[e.Key].ChargeAmount;
                    //    else
                    //        e.Value = "";
                    //};
                    //nameField.MinimumWidth = 120;
                    //NewStudent.Instance.AddListPaneField(nameField);
                    //在學生增加一欄收費標準
                    nameField = new ListPaneField("收費標準");
                    Dictionary<string, StudentTuitionRecord> items2 = new Dictionary<string, StudentTuitionRecord>();
                    nameField.PreloadVariableBackground += delegate (object sender, PreloadVariableEventArgs e)
                    {
                        items2.Clear();
                        List<string> ids = new List<string>();
                        foreach (var item in e.Keys)
                            ids.Add(item);
                        foreach (var item in StudentTuitionDAO.GetStudentTuitionBySSTSL(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, "舊生", ids))
                        {
                            if (!items2.ContainsKey(item.TuitionUID))
                                items2.Add(item.TuitionUID, item);
                        }
                    };
                    nameField.GetVariable += delegate (object sender, GetVariableEventArgs e)
                    {
                        if (items2.ContainsKey(e.Key))
                            e.Value = items2[e.Key].TSName;
                        else
                            e.Value = "";
                    };
                    nameField.MinimumWidth = 120;
                    K12.Presentation.NLDPanels.Student.AddListPaneField(nameField);
                    DataChanged += delegate (object sender, Program.DataChangedEventArgs e)
                    {
                        nameField.Reload();
                    };
                    TuitionChanged += delegate (object sender, EventArgs e)
                    {
                        nameField.Reload();
                    };
                    //在學生增加一欄應繳金額
                    ListPaneField nameField1 = new ListPaneField("應繳金額");
                    Dictionary<string, StudentTuitionRecord> items3 = new Dictionary<string, StudentTuitionRecord>();
                    nameField1.PreloadVariableBackground += delegate (object sender, PreloadVariableEventArgs e)
                    {
                        items3.Clear();
                        List<string> ids = new List<string>();
                        foreach (var item in e.Keys)
                            ids.Add(item);
                        foreach (var item in StudentTuitionDAO.GetStudentTuitionBySSTSL(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, "舊生", ids))
                        {
                            if (!items3.ContainsKey(item.TuitionUID))
                                items3.Add(item.TuitionUID, item);
                            else
                            {
                                MessageBox.Show(Student.SelectByID(item.TuitionUID).Name + "繳費表重覆");
                                //item.Deleted = true;
                                //item.Save();
                            }
                        }
                    };
                    nameField1.GetVariable += delegate (object sender, GetVariableEventArgs e)
                    {
                        if (items3.ContainsKey(e.Key))
                            e.Value = items3[e.Key].ChargeAmount;
                        else
                            e.Value = "";
                    };
                    nameField1.MinimumWidth = 120;
                    K12.Presentation.NLDPanels.Student.AddListPaneField(nameField1);
                    DataChanged += delegate (object sender, Program.DataChangedEventArgs e)
                    {
                        nameField1.Reload();
                    };
                    TuitionChanged += delegate (object sender, EventArgs e)
                    {
                        nameField1.Reload();
                    };
                }
                ribbon = RoleAclSource.Instance["學費"]["學費基本資料"];
                ribbon.Add(new RibbonFeature("Tuition026", "刪除繳費表"));
                ribbon = RoleAclSource.Instance["學費"]["報表及統計"];
                ribbon.Add(new RibbonFeature("Tuition027", "繳費明細資料匯出"));
                MenuButton rbi31 = null;
                if (isAE)
                    rbi31 = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["報表"]["總務相關報表"]["繳費明細資料匯出(依選取學生)"];
                else
                    rbi31 = K12.Presentation.NLDPanels.Student.RibbonBarItems["統計報表"]["報表"]["總務相關報表"]["繳費明細資料匯出(依選取學生)"];
                rbi31.Enable = CurrentUser.Acl["Tuition027"].Executable;
                rbi31.Click += delegate
                {
                    (new PaymentList(1)).ShowDialog();
                };
            }
            Catalog ribbon1 = RoleAclSource.Instance["學費"]["學費系統"];
            ribbon1.Add(new RibbonFeature("Tuition023", "學費系統"));
            if (CurrentUser.Acl["Tuition023"].Executable)
                MotherForm.AddPanel(new 學費Division());
        }
        //static bool rbi4FunctionOnProcess = false;//預防使用者連按二次
        //static void rbi4_PopupOpen(object sender, PopupOpenEventArgs e)
        //{
        //    List<TuitionChangeStdRecord> tcsrs = TuitionChangeStdDAO.GetTuitionChangeStdBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
        //    //已加入的botton
        //    List<string> botton = new List<string>();
        //    var btn2 = e.VirtualButtons[GlobalValue.CurrentSchoolYear.ToString() + "學年度" + GlobalValue.CurrentSemester];
        //    btn2.Enable = false;
        //    botton.Clear();
        //    //int nowSet = 0;
        //    foreach (TuitionChangeStdRecord tcsr in tcsrs)
        //    {
        //        //判斷是否已加入此PopUpBotton
        //        if (!botton.Contains(tcsr.TCSName))
        //        {
        //            botton.Add(tcsr.TCSName);
        //            var btn1 = e.VirtualButtons[tcsr.TCSName];
        //            if (botton.Count == 1)
        //                btn1.BeginGroup = true;
        //            btn1.Text = tcsr.TCSName;  //在click 的event handler 中可以識別要處理的對象 
        //            //bool onprocess = ;
        //            btn1.Click += delegate(Object sender1, EventArgs e1)
        //            {
        //                if (rbi4FunctionOnProcess) 
        //                    return;
        //                else
        //                    rbi4FunctionOnProcess = true;
        //                MenuButton btn = (MenuButton)sender1;
        //                List<NewStudentRecord> nsrs = NewStudent.Instance.SelectedList;
        //                List<string> pks = new List<string>();
        //                Dictionary<string, List<StudentTuitionRecord>> strList = new Dictionary<string, List<StudentTuitionRecord>>();
        //                foreach (NewStudentRecord nsr in nsrs)
        //                    pks.Add(nsr.UID);
        //                //取得繳費表
        //                List<StudentTuitionRecord> studentTus = StudentTuitionDAO.GetStudentTuitionBySSTSL(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, "新生", pks);
        //                foreach (StudentTuitionRecord sr in studentTus)
        //                    if (!strList.ContainsKey(sr.TuitionUID))
        //                    {
        //                        strList.Add(sr.TuitionUID, new List<StudentTuitionRecord>());
        //                        strList[sr.TuitionUID].Add(sr);
        //                    }
        //                pks.Clear();
        //                //取得收費標準
        //                List<TuitionStandardRecord> TSRs = TuitionStandardDAO.GetTuitionStandardBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
        //                Dictionary<string, List<TuitionStandardRecord>> tsrList = new Dictionary<string, List<TuitionStandardRecord>>();
        //                foreach (TuitionStandardRecord tsr in TSRs)
        //                    if (tsrList.ContainsKey(tsr.TSName))
        //                        tsrList[tsr.TSName].Add(tsr);
        //                    else
        //                    {
        //                        tsrList.Add(tsr.TSName, new List<TuitionStandardRecord>());
        //                        tsrList[tsr.TSName].Add(tsr);
        //                    }
        //                //foreach (NewStudentRecord nsr in nsrs)
        //                //{
        //                //    MotherForm.SetStatusBarMessage("正在設定異動標準", (nowSet++ * 100 / nsrs.Count));
        //                //    if (nsr.Active)
        //                //    {                               
        //                //        if (!strList.ContainsKey(nsr.UID))
        //                //            MessageBox.Show(nsr.Number + nsr.Name + "尚未設定收費標準，無法設定異動");
        //                //        else
        //                //        {
        //                //            pks.Add(nsr.UID);                                   
        //                //            if (!SetTuition.SetChangeStd(tsrList[strList[nsr.UID][0].TSName], btn.Text,strList[nsr.UID][0] ))
        //                //                MessageBox.Show(nsr.Number + nsr.Name + "異動標準設定不成功");
        //                //        }
        //                //    }
        //                //}
        //                //MotherForm.SetStatusBarMessage("");
        //                //NewStudent.Instance.SyncDataBackground(pks);
        //                //MessageBox.Show("設定完成");
        //                {
        //                    Framework.MultiThreadBackgroundWorker<NewStudentRecord> mBKW = new Framework.MultiThreadBackgroundWorker<NewStudentRecord>();
        //                    FISCA.Presentation.MotherForm.SetStatusBarMessage("正在設定異動標準...", 0);
        //                    mBKW.DoWork += delegate(object sender2, Framework.PackageDoWorkEventArgs<NewStudentRecord> e2)
        //                    {
        //                       lock(mBKW)
        //                       {
        //                        foreach (NewStudentRecord nsr in e2.Items)
        //                        {
        //                            bool contains = false;
        //                            //鎖定pks確保一個人只執行一次
        //                            lock (pks)
        //                            {
        //                                contains = pks.Contains(nsr.UID);
        //                                if (nsr.Active && !contains) 
        //                                    pks.Add(nsr.UID);
        //                            }
        //                            if (nsr.Active && (!contains))                                    
        //                               {
        //                                if (!strList.ContainsKey(nsr.UID))
        //                                    MessageBox.Show(nsr.Number + nsr.Name + "尚未設定收費標準，無法設定異動");
        //                                else
        //                                {
        //                                    if (strList[nsr.UID][0].PayDate == null)        
        //                                       if (!SetTuition.SetChangeStd(tsrList[strList[nsr.UID][0].TSName], btn.Text, strList[nsr.UID][0]))
        //                                           MessageBox.Show(nsr.Number + nsr.Name + "異動標準設定不成功");
        //                                }
        //                            }
        //                        }
        //                      }
        //                    };
        //                    mBKW.ProgressChanged += delegate(object sender3, ProgressChangedEventArgs e3)
        //                    {
        //                        FISCA.Presentation.MotherForm.SetStatusBarMessage("正在設定異動標準...", e3.ProgressPercentage);
        //                    };
        //                    mBKW.RunWorkerCompleted += delegate(object sender4, RunWorkerCompletedEventArgs e4)
        //                    {
        //                        NewStudent.Instance.SyncDataBackground(pks);
        //                        FISCA.Presentation.MotherForm.SetStatusBarMessage("");
        //                        MessageBox.Show("設定完成");
        //                        rbi4FunctionOnProcess = false;//執行完畢才放掉
        //                    };
        //                    //設定包的大小,1為人數
        //                    mBKW.PackageSize = 10;
        //                    mBKW.RunWorkerAsync(nsrs);
        //                }

        //            };
        //        }
        //    }
        //}

        //static void rbi3_PopupOpen(object sender, PopupOpenEventArgs e)
        //{
        //    List<TuitionStandardRecord> tsrs = TuitionStandardDAO.GetTuitionStandardBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
        //    List<string> botton = new List<string>();
        //    botton.Clear();
        //    var btn2 = e.VirtualButtons[GlobalValue.CurrentSchoolYear.ToString() + "學年度" + GlobalValue.CurrentSemester];
        //    btn2.Enable = false;
        //    //int nowSet = 0;
        //    List<NewStudentRecord> nsrs = NewStudent.Instance.SelectedList;
        //    List<string> deptname = new List<string>();
        //    //找出選取學生科別
        //    foreach (NewStudentRecord nsr in nsrs)
        //       if (!deptname.Contains(nsr.Dept))
        //           deptname.Add(nsr.Dept);
        //    //適用收費標準
        //    foreach (TuitionStandardRecord tsr in tsrs)
        //    {
        //        if (!botton.Contains(tsr.TSName) && tsr.ClassYear=="一年級")
        //           if (deptname.Contains(tsr.Dept))                    
        //               botton.Add(tsr.TSName);
        //    }
        //    foreach (string TSName in botton)
        //    {
        //        var btn1 = e.VirtualButtons[TSName];
        //        if (botton.Count == 1)
        //            btn1.BeginGroup = true;
        //        btn1.Text = TSName;  //在click 的event handler 中可以識別要處理的對象
        //        btn1.Click += delegate(Object sender1, EventArgs e1)
        //        {
        //            MenuButton btn = (MenuButton)sender1;
        //            List<string> pks = new List<string>();
        //            nsrs = NewStudent.Instance.SelectedList;
        //            //foreach (NewStudentRecord nsr in nsrs)
        //            //{
        //            //    MotherForm.SetStatusBarMessage("正在設定收費標準", (nowSet++ * 100 / nsrs.Count));
        //            //    if (nsr.Active)
        //            //    {
        //            //        if (!SetTuition.SetTuitionStandard("新生", btn.Text, nsr.UID.ToString(), nsr.Gender))
        //            //            MessageBox.Show(nsr.Number + nsr.Name + "收費標準設定不成功");
        //            //        pks.Add(nsr.UID);
        //            //    }
        //            //}
        //            //MotherForm.SetStatusBarMessage("");
        //            //NewStudent.Instance.SyncDataBackground(pks);
        //            //MessageBox.Show("設定完成");
        //            {
        //                Framework.MultiThreadBackgroundWorker<NewStudentRecord> mBKW = new Framework.MultiThreadBackgroundWorker<NewStudentRecord>();
        //                FISCA.Presentation.MotherForm.SetStatusBarMessage("正在設定收費標準...", 0);
        //                mBKW.DoWork += delegate(object sender2, Framework.PackageDoWorkEventArgs<NewStudentRecord> e2)
        //                {
        //                    lock (mBKW)
        //                    {
        //                        foreach (NewStudentRecord nsr in e2.Items)
        //                        {
        //                            if (nsr.Active && (!pks.Contains(nsr.UID)))
        //                            {
        //                                pks.Add(nsr.UID);
        //                                if (!SetTuition.SetTuitionStandard("新生", btn.Text, nsr.UID.ToString(), nsr.Gender))
        //                                    MessageBox.Show(nsr.Number + nsr.Name + "收費標準設定不成功");

        //                            }
        //                        }
        //                    }
        //                };
        //                mBKW.ProgressChanged += delegate(object sender3, ProgressChangedEventArgs e3)
        //                {
        //                    FISCA.Presentation.MotherForm.SetStatusBarMessage("正在設定收費標準...", e3.ProgressPercentage);
        //                };
        //                mBKW.RunWorkerCompleted += delegate(object sender4, RunWorkerCompletedEventArgs e4)
        //                {
        //                    NewStudent.Instance.SyncDataBackground(pks);
        //                    FISCA.Presentation.MotherForm.SetStatusBarMessage("");
        //                    MessageBox.Show("設定完成");
        //                };
        //                //設定包的大小,1為人數
        //                mBKW.PackageSize=100;
        //                mBKW.RunWorkerAsync(nsrs);
        //            }
        //        };
        //    }

        //}
        static bool rbi2FunctionOnProcess = false;  //預防使用者連按二次
        static void rbi2_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            List<TuitionChangeStdRecord> tcsrs = TuitionChangeStdDAO.GetTuitionChangeStdBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
            List<string> botton = new List<string>();
            var btn2 = e.VirtualButtons[GlobalValue.CurrentSchoolYear.ToString() + "學年度" + GlobalValue.CurrentSemester];
            btn2.Enable = false;
            botton.Clear();
            //int nowSet = 0;
            foreach (TuitionChangeStdRecord tcsr in tcsrs)
            {
                if (!botton.Contains(tcsr.TCSName))
                {
                    botton.Add(tcsr.TCSName);
                    var btn1 = e.VirtualButtons[tcsr.TCSName];
                    if (botton.Count == 1)
                        btn1.BeginGroup = true;
                    btn1.Text = tcsr.TCSName;  //在click 的event handler 中可以識別要處理的對象              
                    btn1.Click += delegate (Object sender1, EventArgs e1)
                    {
                        if (rbi2FunctionOnProcess)
                            return;
                        else
                            rbi2FunctionOnProcess = true;
                        MenuButton btn = (MenuButton)sender1;
                        List<SHStudentRecord> nsrs = SHStudent.SelectByIDs(K12.Presentation.NLDPanels.Student.SelectedSource);
                        List<string> pks = new List<string>();
                        Dictionary<string, List<StudentTuitionRecord>> strList = new Dictionary<string, List<StudentTuitionRecord>>();
                        foreach (SHStudentRecord nsr in nsrs)
                            pks.Add(nsr.ID);
                        //取得繳費表
                        List<StudentTuitionRecord> studentTus = StudentTuitionDAO.GetStudentTuitionBySSTSL(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, "舊生", pks);
                        foreach (StudentTuitionRecord sr in studentTus)
                            if (!strList.ContainsKey(sr.TuitionUID))
                            {
                                strList.Add(sr.TuitionUID, new List<StudentTuitionRecord>());
                                strList[sr.TuitionUID].Add(sr);
                            }
                        pks.Clear();
                        //取得收費標準
                        List<TuitionStandardRecord> TSRs = TuitionStandardDAO.GetTuitionStandardBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
                        Dictionary<string, List<TuitionStandardRecord>> tsrList = new Dictionary<string, List<TuitionStandardRecord>>();
                        foreach (TuitionStandardRecord tsr in TSRs)
                            if (tsrList.ContainsKey(tsr.TSName))
                                tsrList[tsr.TSName].Add(tsr);
                            else
                            {
                                tsrList.Add(tsr.TSName, new List<TuitionStandardRecord>());
                                tsrList[tsr.TSName].Add(tsr);
                            }
                        //foreach (SHStudentRecord nsr in nsrs)
                        //{
                        //    MotherForm.SetStatusBarMessage("正在設定異動標準", (nowSet++ * 100 / nsrs.Count));
                        //    if (nsr.Status ==SHStudentRecord.StudentStatus.一般)
                        //    {                                
                        //        if (!strList.ContainsKey(nsr.ID))
                        //            MessageBox.Show(nsr.StudentNumber + nsr.Name + "尚未設定收費標準，無法設定異動");
                        //        else
                        //        {
                        //            pks.Add(nsr.ID);                                    
                        //            if (!SetTuition.SetChangeStd(tsrList[strList[nsr.ID][0].TSName], btn.Text,strList[nsr.ID][0]))
                        //                MessageBox.Show(nsr.StudentNumber + nsr.Name + "異動標準設定不成功");
                        //        }
                        //    }
                        //}
                        //MotherForm.SetStatusBarMessage("");
                        ////JHSchool.Student.Instance.SyncDataBackground(pks);
                        //if (DataChanged != null)
                        //    DataChanged(null, new DataChangedEventArgs(pks));
                        //MessageBox.Show("設定完成");
                        {
                            Framework.MultiThreadBackgroundWorker<SHStudentRecord> mBKW = new Framework.MultiThreadBackgroundWorker<SHStudentRecord>();
                            FISCA.Presentation.MotherForm.SetStatusBarMessage("正在設定異動標準...", 0);
                            mBKW.DoWork += delegate (object sender2, Framework.PackageDoWorkEventArgs<SHStudentRecord> e2)
                            {
                                lock (mBKW)
                                {
                                    foreach (SHStudentRecord nsr in e2.Items)
                                    {

                                        if (nsr.Status == SHStudentRecord.StudentStatus.一般 && (!pks.Contains(nsr.ID)))
                                        {
                                            if (!strList.ContainsKey(nsr.ID))
                                                MessageBox.Show(nsr.StudentNumber + nsr.Name + "尚未設定收費標準，無法設定異動");
                                            else
                                            {
                                                pks.Add(nsr.ID);
                                                if (strList[nsr.ID][0].PayDate == null)
                                                    if (!SetTuition.SetChangeStd(tsrList[strList[nsr.ID][0].TSName], btn.Text, strList[nsr.ID][0]))
                                                        MessageBox.Show(nsr.StudentNumber + nsr.Name + "異動標準設定不成功");
                                            }
                                        }

                                    }
                                }
                            };
                            mBKW.ProgressChanged += delegate (object sender3, ProgressChangedEventArgs e3)
                            {
                                FISCA.Presentation.MotherForm.SetStatusBarMessage("正在設定異動標準...", e3.ProgressPercentage);
                            };
                            mBKW.RunWorkerCompleted += delegate (object sender4, RunWorkerCompletedEventArgs e4)
                            {
                                if (DataChanged != null)
                                    DataChanged(null, new DataChangedEventArgs(pks));
                                FISCA.Presentation.MotherForm.SetStatusBarMessage("");
                                MessageBox.Show("設定完成");
                                rbi2FunctionOnProcess = false;
                            };
                            //設定包的大小,1為人數
                            mBKW.PackageSize = 10;
                            mBKW.RunWorkerAsync(nsrs);
                        }

                    };
                }
            }
        }
        public class DataChangedEventArgs : EventArgs
        {
            public List<string> primaryKeys { get; private set; }
            public DataChangedEventArgs(List<string> ids)
            {
                primaryKeys = ids;
            }
        }
        static internal event EventHandler<DataChangedEventArgs> DataChanged;
        static internal event EventHandler TuitionChanged;
        static internal void OnTuitionChanged()
        {
            if (TuitionChanged != null)
                TuitionChanged(null, new EventArgs());
        }
        static void rbi1_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            List<TuitionStandardRecord> tsrs = TuitionStandardDAO.GetTuitionStandardBySS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester);
            List<string> botton = new List<string>();
            var btn2 = e.VirtualButtons[GlobalValue.CurrentSchoolYear.ToString() + "學年度" + GlobalValue.CurrentSemester];
            btn2.Enable = false;
            botton.Clear();
            //int nowSet = 0;
            List<string> deptname = new List<string>();
            List<SHStudentRecord> nsrs = SHStudent.SelectByIDs(K12.Presentation.NLDPanels.Student.SelectedSource);
            string dept = "";
            //找出所有學生科別

            foreach (SHStudentRecord nsr in nsrs)
            {
                if (nsr.Department != null)
                {
                    dept = nsr.Department.FullName;
                    if (!string.IsNullOrEmpty(dept) && !deptname.Contains(dept))
                        deptname.Add(dept);
                }
            }


            foreach (TuitionStandardRecord tsr in tsrs)
            {
                if (!botton.Contains(tsr.TSName))
                    if (deptname.Contains(tsr.Dept))
                        botton.Add(tsr.TSName);
            }
            foreach (string TSName in botton)
            {
                var btn1 = e.VirtualButtons[TSName];
                if (botton.Count == 1)
                    btn1.BeginGroup = true;
                btn1.Text = TSName;  //在click 的event handler 中可以識別要處理的對象              
                btn1.Click += delegate (Object sender1, EventArgs e1)
                {
                    MenuButton btn = (MenuButton)sender1;
                    List<string> pks = new List<string>();
                    //foreach (SHStudentRecord nsr in nsrs)
                    //{
                    //    MotherForm.SetStatusBarMessage("正在設定收費標準", (nowSet++ * 100 / nsrs.Count));
                    //    if (nsr.Status == SHStudentRecord.StudentStatus.一般)
                    //    {
                    //        if (!SetTuition.SetTuitionStandard("舊生", btn.Text, nsr.ID.ToString(), nsr.Gender))
                    //            MessageBox.Show(nsr.StudentNumber + nsr.Name + "收費標準設定不成功");
                    //        pks.Add(nsr.ID);
                    //    }
                    //}
                    //MotherForm.SetStatusBarMessage("");
                    ////JHSchool.Student.Instance.SyncDataBackground(pks);
                    //if (DataChanged != null)
                    //    DataChanged(null, new DataChangedEventArgs(pks));
                    //MessageBox.Show("設定完成");
                    {
                        Framework.MultiThreadBackgroundWorker<SHStudentRecord> mBKW = new Framework.MultiThreadBackgroundWorker<SHStudentRecord>();
                        FISCA.Presentation.MotherForm.SetStatusBarMessage("正在設定收費標準...", 0);
                        mBKW.DoWork += delegate (object sender2, Framework.PackageDoWorkEventArgs<SHStudentRecord> e2)
                        {
                            lock (mBKW)
                            {
                                foreach (SHStudentRecord nsr in e2.Items)
                                {
                                    if (nsr.Status == SHStudentRecord.StudentStatus.一般 && (!pks.Contains(nsr.ID)))
                                    {
                                        if (!SetTuition.SetTuitionStandard("舊生", btn.Text, nsr.ID.ToString(), nsr.Gender))
                                            MessageBox.Show(nsr.StudentNumber + nsr.Name + "收費標準設定不成功");
                                        pks.Add(nsr.ID);
                                    }
                                }
                            }
                        };
                        mBKW.ProgressChanged += delegate (object sender3, ProgressChangedEventArgs e3)
                        {
                            FISCA.Presentation.MotherForm.SetStatusBarMessage("正在設定收費標準...", e3.ProgressPercentage);
                        };
                        mBKW.RunWorkerCompleted += delegate (object sender4, RunWorkerCompletedEventArgs e4)
                        {
                            if (DataChanged != null)
                                DataChanged(null, new DataChangedEventArgs(pks));
                            FISCA.Presentation.MotherForm.SetStatusBarMessage("");
                            MessageBox.Show("設定完成");
                        };
                        //設定包的大小,1為人數
                        mBKW.PackageSize = 10;
                        mBKW.RunWorkerAsync(nsrs);
                    }
                };
            }

        }

        //空白Division
        public class 學費Division : IBlankPanel
        {
            public 學費Division() { ContentPane = new Control(); }
            public virtual string Group { get { return "學費"; } }
            public virtual Control ContentPane { get; private set; }
            public virtual Image Picture { get { return null; } }
        }

    }
}
