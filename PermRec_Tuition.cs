using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.UDT;
using TuitionSystem.Data;
using TuitionSystem;
using SHSchool.Data;
namespace TuitionSystem
{
    public partial class PermRec_Tuition : FISCA.Presentation.DetailContent
    {
        private BackgroundWorker _Loader = new BackgroundWorker();
        private string _RunningKey = "";
        private List<StudentTuitionRecord> sr = null;

        public PermRec_Tuition()
        {
            InitializeComponent();
            this.Group = "舊生繳費表";  //Title
            _Loader.DoWork += new DoWorkEventHandler(_Loader_DoWork);
            _Loader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_Loader_RunWorkerCompleted);
            Program.DataChanged += new EventHandler<Program.DataChangedEventArgs>(Program_DataChanged);
            this.studentTuitionProcess1.OpenMode = OpenMode.PermRec;
        }

        void Program_DataChanged(object sender, Program.DataChangedEventArgs e)
        {
            if (e.primaryKeys.Contains(PrimaryKey)) 
                OnPrimaryKeyChanged(new EventArgs());
           
            
        }

        void _Loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_RunningKey != PrimaryKey)
            {
                _RunningKey = PrimaryKey;
                _Loader.RunWorkerAsync();
            }
            else
            {
                SHStudentRecord Stud = SHStudent.SelectByID(PrimaryKey);
                string dept = (Stud.Department == null ? "" : Stud.Department.FullName);
                if (sr.Count > 0)
                    studentTuitionProcess1.SetStudentTuitionRecord(sr[0], SHStudent.SelectByID(PrimaryKey).Gender,dept);
                else
                    studentTuitionProcess1.Empty_Form();
                this.Loading = false;
            }
        }

        void _Loader_DoWork(object sender, DoWorkEventArgs e)
        {
            sr = StudentTuitionDAO.GetStudentTuitionBySSTS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, "舊生", PrimaryKey);
        }
        /// <summary>
        /// 當主畫面檢視不同學生時，此毛毛蟲就被指定新的學生ID，就會呼叫此方法。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;    //畫面會呈現資料下載中的狀態(圓圈一直轉)

            //List<StudentTuitionRecord> sr = StudentTuitionDAO.GetStudentTuitionBySSTS(GlobalValue.CurrentSchoolYear, GlobalValue.CurrentSemester, "舊生", PrimaryKey);
            //if (sr.Count > 0)
            //    studentTuitionProcess1.SetStudentTuitionRecord(sr[0], SHStudent.SelectByID(PrimaryKey).Gender);
            //else
            //    studentTuitionProcess1.Empty_Form();

            //this.Loading = false;
            if (!_Loader.IsBusy)
            {
                _RunningKey = PrimaryKey;
                _Loader.RunWorkerAsync();                
            }
        }
    }
}
