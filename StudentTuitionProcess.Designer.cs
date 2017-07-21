namespace TuitionSystem
{
    partial class StudentTuitionProcess
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.intSchoolYear = new DevComponents.Editors.IntegerInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cboTSName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtChargeAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtChangeMoney = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.tuitionDetailRecordBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sTUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TCSName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ChargeAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.刪除 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tuitionDetailRecordBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cboSemester
            // 
            this.cboSemester.DisplayMember = "Text";
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.ItemHeight = 19;
            this.cboSemester.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cboSemester.Location = new System.Drawing.Point(177, 14);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(73, 25);
            this.cboSemester.TabIndex = 36;
            this.cboSemester.SelectedIndexChanged += new System.EventHandler(this.cboSemester_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "上學期";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "下學期";
            // 
            // intSchoolYear
            // 
            this.intSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intSchoolYear.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSchoolYear.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSchoolYear.Location = new System.Drawing.Point(72, 16);
            this.intSchoolYear.MaxValue = 120;
            this.intSchoolYear.MinValue = 98;
            this.intSchoolYear.Name = "intSchoolYear";
            this.intSchoolYear.ShowUpDown = true;
            this.intSchoolYear.Size = new System.Drawing.Size(54, 22);
            this.intSchoolYear.TabIndex = 35;
            this.intSchoolYear.Value = 98;
            this.intSchoolYear.ValueChanged += new System.EventHandler(this.intSchoolYear_ValueChanged);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(18, 20);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(63, 23);
            this.labelX1.TabIndex = 37;
            this.labelX1.Text = "學年度：";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(133, 20);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(51, 23);
            this.labelX4.TabIndex = 38;
            this.labelX4.Text = "學期：";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(256, 18);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(103, 23);
            this.labelX2.TabIndex = 34;
            this.labelX2.Text = "收費標準名稱：";
            // 
            // cboTSName
            // 
            this.cboTSName.DisplayMember = "Text";
            this.cboTSName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTSName.FormattingEnabled = true;
            this.cboTSName.ItemHeight = 16;
            this.cboTSName.Location = new System.Drawing.Point(347, 15);
            this.cboTSName.Name = "cboTSName";
            this.cboTSName.Size = new System.Drawing.Size(176, 22);
            this.cboTSName.TabIndex = 39;
            this.cboTSName.SelectedIndexChanged += new System.EventHandler(this.cboTSName_SelectedIndexChanged);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(16, 47);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(103, 23);
            this.labelX3.TabIndex = 40;
            this.labelX3.Text = "應繳總金額：";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(280, 47);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(103, 23);
            this.labelX5.TabIndex = 41;
            this.labelX5.Text = "異動金額：";
            // 
            // txtChargeAmount
            // 
            // 
            // 
            // 
            this.txtChargeAmount.Border.Class = "TextBoxBorder";
            this.txtChargeAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtChargeAmount.Location = new System.Drawing.Point(94, 46);
            this.txtChargeAmount.Name = "txtChargeAmount";
            this.txtChargeAmount.Size = new System.Drawing.Size(156, 22);
            this.txtChargeAmount.TabIndex = 44;
            // 
            // txtChangeMoney
            // 
            // 
            // 
            // 
            this.txtChangeMoney.Border.Class = "TextBoxBorder";
            this.txtChangeMoney.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtChangeMoney.Location = new System.Drawing.Point(347, 44);
            this.txtChangeMoney.Name = "txtChangeMoney";
            this.txtChangeMoney.Size = new System.Drawing.Size(174, 22);
            this.txtChangeMoney.TabIndex = 46;
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AutoGenerateColumns = false;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sTUIDDataGridViewTextBoxColumn,
            this.TCSName,
            this.ChargeAmount,
            this.刪除});
            this.dataGridViewX1.DataSource = this.tuitionDetailRecordBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(18, 160);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.Size = new System.Drawing.Size(392, 200);
            this.dataGridViewX1.TabIndex = 48;
            this.dataGridViewX1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridViewX1_CellEndEdit);
            this.dataGridViewX1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataGridViewX1_DataError);
            // 
            // tuitionDetailRecordBindingSource
            // 
            //this.tuitionDetailRecordBindingSource.DataSource = typeof(TuitionSystem.Data.TuitionDetailRecord);
            // 
            // sTUIDDataGridViewTextBoxColumn
            // 
            this.sTUIDDataGridViewTextBoxColumn.DataPropertyName = "STUID";
            this.sTUIDDataGridViewTextBoxColumn.HeaderText = "STUID";
            this.sTUIDDataGridViewTextBoxColumn.Name = "sTUIDDataGridViewTextBoxColumn";
            this.sTUIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // TCSName
            // 
            this.TCSName.DataPropertyName = "TCSName";
            this.TCSName.HeaderText = "異動名稱";
            this.TCSName.Name = "TCSName";
            this.TCSName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TCSName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TCSName.Width = 180;
            // 
            // ChargeAmount
            // 
            this.ChargeAmount.DataPropertyName = "ChangeAmount";
            this.ChargeAmount.HeaderText = "異動金額";
            this.ChargeAmount.Name = "ChargeAmount";
            // 
            // 刪除
            // 
            this.刪除.DataPropertyName = "Deleted";
            this.刪除.HeaderText = "刪除";
            this.刪除.Name = "刪除";
            this.刪除.Width = 50;

            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd.Location = new System.Drawing.Point(425, 250);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 49;
            this.btnAdd.Text = "新增異動";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(425, 300);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 50;
            this.btnSave.Text = "儲存繳費表";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Location = new System.Drawing.Point(425, 197);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 49;
            this.btnDelete.Text = "刪除繳費表";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(18, 136);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(130, 23);
            this.labelX8.TabIndex = 51;
            this.labelX8.Text = "繳費表異動明細：";
            // 
            // StudentTuitionProcess
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.txtChangeMoney);
            this.Controls.Add(this.txtChargeAmount);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.cboTSName);
            this.Controls.Add(this.cboSemester);
            this.Controls.Add(this.intSchoolYear);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.labelX8);
            this.Name = "StudentTuitionProcess";
            this.Size = new System.Drawing.Size(527, 380);
            this.Load += new System.EventHandler(this.StudentTuitionProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tuitionDetailRecordBindingSource)).EndInit();
            this.ResumeLayout(false);

        }




        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.IntegerInput intSchoolYear;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTSName;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtChargeAmount;
        private DevComponents.DotNetBar.Controls.TextBoxX txtChangeMoney;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sTUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn TCSName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChargeAmount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 刪除;
        private System.Windows.Forms.BindingSource tuitionDetailRecordBindingSource;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.LabelX labelX8;
    }
}
