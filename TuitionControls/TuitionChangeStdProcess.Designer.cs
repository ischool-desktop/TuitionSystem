namespace TuitionSystem.TuitionControls
{
    partial class TuitionChangeStdProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lblSchoolYear = new DevComponents.DotNetBar.LabelX();
            this.lblSemester = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnCopy = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.txtTCSName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.btnAddItem = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtMoney = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cboType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lstStdView = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.btnCopySemData = new DevComponents.DotNetBar.ButtonX();
            this.tuitionChangeStdRecordBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChargeItem = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.刪除 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tuitionChangeStdRecordBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(125, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(100, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "異動標準一覽表";
            // 
            // lblSchoolYear
            // 
            this.lblSchoolYear.BackColor = System.Drawing.Color.Transparent;
            this.lblSchoolYear.Location = new System.Drawing.Point(9, 11);
            this.lblSchoolYear.Name = "lblSchoolYear";
            this.lblSchoolYear.Size = new System.Drawing.Size(38, 23);
            this.lblSchoolYear.TabIndex = 2;
            this.lblSchoolYear.Text = "98";
            // 
            // lblSemester
            // 
            this.lblSemester.BackColor = System.Drawing.Color.Transparent;
            this.lblSemester.Location = new System.Drawing.Point(82, 11);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(46, 23);
            this.lblSemester.TabIndex = 3;
            this.lblSemester.Text = "上學期";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            this.labelX4.Location = new System.Drawing.Point(37, 11);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(49, 23);
            this.labelX4.TabIndex = 4;
            this.labelX4.Text = "學年度";
            // 
            // btnCopy
            // 
            this.btnCopy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCopy.BackColor = System.Drawing.Color.Transparent;
            this.btnCopy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCopy.Location = new System.Drawing.Point(9, 392);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(52, 23);
            this.btnCopy.TabIndex = 6;
            this.btnCopy.Text = "複製";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Location = new System.Drawing.Point(67, 392);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(52, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "刪除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtTCSName
            // 
            // 
            // 
            // 
            this.txtTCSName.Border.Class = "TextBoxBorder";
            this.txtTCSName.Location = new System.Drawing.Point(95, 6);
            this.txtTCSName.Name = "txtTCSName";
            this.txtTCSName.Size = new System.Drawing.Size(143, 25);
            this.txtTCSName.TabIndex = 8;
            this.txtTCSName.Leave += new System.EventHandler(this.chkValidity);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(4, 9);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(103, 23);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "異動標準名稱：";
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AutoGenerateColumns = false;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UID,
            this.ChargeItem,
            this.Percent,
            this.刪除});
            //this.dataGridViewX1.DataSource = this.tuitionChangeStdRecordBindingSource;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(9, 87);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.Size = new System.Drawing.Size(376, 241);
            this.dataGridViewX1.TabIndex = 12;
            this.dataGridViewX1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellValueChanged);
            this.dataGridViewX1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewX1_DataError);
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            this.labelX5.Location = new System.Drawing.Point(9, 65);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(158, 23);
            this.labelX5.TabIndex = 13;
            this.labelX5.Text = "異動標準項目明細：";
            // 
            // btnAddItem
            // 
            this.btnAddItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddItem.BackColor = System.Drawing.Color.Transparent;
            this.btnAddItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddItem.Location = new System.Drawing.Point(16, 343);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(52, 23);
            this.btnAddItem.TabIndex = 14;
            this.btnAddItem.Text = "新增";
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(95, 343);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(52, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtMoney);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.cboType);
            this.groupPanel1.Controls.Add(this.labelX6);
            this.groupPanel1.Controls.Add(this.btnSave);
            this.groupPanel1.Controls.Add(this.btnAddItem);
            this.groupPanel1.Controls.Add(this.dataGridViewX1);
            this.groupPanel1.Controls.Add(this.txtTCSName);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Location = new System.Drawing.Point(231, 39);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(405, 376);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel1.TabIndex = 16;
            // 
            // txtMoney
            // 
            // 
            // 
            // 
            this.txtMoney.Border.Class = "TextBoxBorder";
            this.txtMoney.Location = new System.Drawing.Point(312, 6);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(73, 25);
            this.txtMoney.TabIndex = 18;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.labelX3.Location = new System.Drawing.Point(244, 9);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(103, 23);
            this.labelX3.TabIndex = 19;
            this.labelX3.Text = "固定金額：";
            // 
            // cboType
            // 
            this.cboType.DisplayMember = "Text";
            this.cboType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboType.FormattingEnabled = true;
            this.cboType.ItemHeight = 19;
            this.cboType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cboType.Location = new System.Drawing.Point(94, 36);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(60, 25);
            this.cboType.TabIndex = 17;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.chkValidity);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "＋";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "－";
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            this.labelX6.Location = new System.Drawing.Point(5, 36);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(83, 23);
            this.labelX6.TabIndex = 16;
            this.labelX6.Text = "類別(加減)：";
            // 
            // lstStdView
            // 
            // 
            // 
            // 
            this.lstStdView.Border.Class = "ListViewBorder";
            this.lstStdView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lstStdView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstStdView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstStdView.Location = new System.Drawing.Point(8, 39);
            this.lstStdView.MultiSelect = false;
            this.lstStdView.Name = "lstStdView";
            this.lstStdView.Size = new System.Drawing.Size(208, 344);
            this.lstStdView.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lstStdView.TabIndex = 18;
            this.lstStdView.UseCompatibleStateImageBehavior = false;
            this.lstStdView.View = System.Windows.Forms.View.Details;
            this.lstStdView.SelectedIndexChanged += new System.EventHandler(this.lstStdView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "異動標準名稱";
            this.columnHeader1.Width = 200;          
            
            // 
            // btnCopySemData
            // 
            this.btnCopySemData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCopySemData.BackColor = System.Drawing.Color.Transparent;
            this.btnCopySemData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCopySemData.Location = new System.Drawing.Point(125, 393);
            this.btnCopySemData.Name = "btnCopySemData";
            this.btnCopySemData.Size = new System.Drawing.Size(90, 23);
            this.btnCopySemData.TabIndex = 19;
            this.btnCopySemData.Text = "複製上期資料";
            this.btnCopySemData.Click += new System.EventHandler(btnCopySemData_Click);
            // 
            // tuitionChangeStdRecordBindingSource
            // 
            //this.tuitionChangeStdRecordBindingSource.DataSource = typeof(TuitionSystem.Data.TuitionChangeStdRecord);
            // 
            // UID
            // 
            this.UID.DataPropertyName = "UID";
            this.UID.HeaderText = "UID";
            this.UID.Name = "UID";
            this.UID.ReadOnly = true;
            this.UID.Visible = false;
            // 
            // ChargeItem
            // 
            this.ChargeItem.DataPropertyName = "ChargeItem";
            this.ChargeItem.HeaderText = "異動收費項目";
            this.ChargeItem.Name = "ChargeItem";
            this.ChargeItem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ChargeItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ChargeItem.Width = 180;
            this.ChargeItem.Items.AddRange(new object[] {
            this.comboItem1});            
            // 
            // 刪除
            // 
            this.刪除.DataPropertyName = "Deleted";
            this.刪除.HeaderText = "刪除";
            this.刪除.Name = "刪除";
            this.刪除.Width = 50;
            
            // 
            // Percent
            // 
            this.Percent.DataPropertyName = "Percent";
            this.Percent.HeaderText = "百分比(1-100)";
            this.Percent.Name = "Percent";
            this.Percent.Width = 70;
            // 
            // TuitionChangeStdProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 421);
            this.Controls.Add(this.btnCopySemData);
            this.Controls.Add(this.lstStdView);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lblSemester);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lblSchoolYear);
            this.Controls.Add(this.groupPanel1);
            this.Name = "TuitionChangeStdProcess";
            this.Text = "異動標準維護";
            this.Load += new System.EventHandler(this.TuitionChangeStdProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tuitionChangeStdRecordBindingSource)).EndInit();
            this.ResumeLayout(false);

        }       
       

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lblSchoolYear;
        private DevComponents.DotNetBar.LabelX lblSemester;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnCopy;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTCSName;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;       
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnAddItem;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.ListViewEx lstStdView;        
        private DevComponents.DotNetBar.ButtonX btnCopySemData;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMoney;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboType;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.LabelX labelX6;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        private System.Windows.Forms.DataGridViewComboBoxColumn ChargeItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 刪除;
        private System.Windows.Forms.BindingSource tuitionChangeStdRecordBindingSource;
        private System.Windows.Forms.ColumnHeader columnHeader1;
       
       
    }
}