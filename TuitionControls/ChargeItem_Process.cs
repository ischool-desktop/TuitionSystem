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
    public partial class ChargeItem_Process : BaseForm
    {
        private List<ChargeItemRecord> _Source = new List<ChargeItemRecord>();
        public ChargeItem_Process()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _Source = new List<ChargeItemRecord>(_Source);
            _Source.Add(new ChargeItemRecord() );
            dataGridViewX1.EndEdit();
            dataGridViewX1.CancelEdit();
            //DataBinding至dataGridView1
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = _Source;            
            chkRepeat();
            dataGridViewX1.CurrentCell = dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 1].Cells[1];
            dataGridViewX1.CurrentCell.Selected = true;
            dataGridViewX1.BeginEdit(true);
            btnSave.Enabled = false;
        }       
       

        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool hasChanged = false;            
            foreach (var item in _Source)
            {
                if (item.RecordStatus != RecordStatus.NoChange)
                    hasChanged = true;
            }
            btnSave.Visible= hasChanged;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataGridViewX1.EndEdit();
            dataGridViewX1.CancelEdit();            
            _Source.SaveAll();
            RefreshDataGrid();
        }

        private void ChargeItem_Process_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;
            RefreshDataGrid();            
        }
        private void RefreshDataGrid()
        {
            AccessHelper udtHelper = new AccessHelper();
            _Source = udtHelper.Select<ChargeItemRecord>();
            _Source.Sort();
            dataGridViewX1.DataSource = _Source;
        }

        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            chkRepeat();
        }
        private void chkRepeat()
        {
            Boolean IsRepeat;
            IsRepeat = false;
            Boolean IsBlank;
            IsBlank=false;
            Boolean IsError;
            IsError = false;
            for (int i = dataGridViewX1.Rows.Count-1; i>=0 ; i--)
            {
                if (dataGridViewX1.Rows[i].Cells[1].Value == null || dataGridViewX1.Rows[i].Cells[2].Value == null)
                    IsBlank = true;
                else
                {
                    dataGridViewX1[1, i].ErrorText = "";
                    dataGridViewX1.UpdateCellErrorText(1, i);
                    dataGridViewX1[2, i].ErrorText = "";
                    dataGridViewX1.UpdateCellErrorText(2, i);
                    for (int j = i - 1; j >=0; j--)
                    {
                        if (("" + dataGridViewX1.Rows[i].Cells[1].Value).Trim() == ("" + dataGridViewX1.Rows[j].Cells[1].Value).Trim())
                        {
                            IsRepeat = true;
                            dataGridViewX1[1, i].ErrorText = "!";
                            dataGridViewX1.UpdateCellErrorText(1, i);
                        }
                        if (("" + dataGridViewX1.Rows[i].Cells[2].Value).Trim() == ("" + dataGridViewX1.Rows[j].Cells[2].Value).Trim())
                        {
                            IsRepeat = true;
                            dataGridViewX1[2, i].ErrorText = "!";
                            dataGridViewX1.UpdateCellErrorText(2, i);
                        }
                        else
                        {
                            int abc;
                            if ( !int.TryParse(("" + dataGridViewX1.Rows[i].Cells[2].Value).Trim(),out abc))
                            {
                               IsError = true;
                               dataGridViewX1[2, i].ErrorText = "!";
                               dataGridViewX1.UpdateCellErrorText(2, i);
                            }
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
            if (IsRepeat || IsBlank || IsError)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;
            
        }
    }
}
