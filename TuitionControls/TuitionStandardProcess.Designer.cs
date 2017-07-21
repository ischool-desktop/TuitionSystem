namespace TuitionSystem.TuitionControls
{
    partial class TuitionStandardProcess
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lblSchoolYear = new DevComponents.DotNetBar.LabelX();
            this.lblSemester = new DevComponents.DotNetBar.LabelX();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnCopy = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.txtTSName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cboGender = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();            
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChargeItem = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.刪除 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tuitionStandardRecordBindingSource = new System.Windows.Forms.BindingSource(this.components);            
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UID,
            this.ChargeItem,
            this.Money,
            this.刪除});
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.btnAddItem = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cboClassYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.cboDept = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lstStdView = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.btnCopySemData = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tuitionStandardRecordBindingSource)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(125, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(100, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "收費標準一覽表";
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
            this.btnDelete.Location = new System.Drawing.Point(66, 392);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(52, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "刪除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtTSName
            // 
            // 
            // 
            // 
            this.txtTSName.Border.Class = "TextBoxBorder";
            this.txtTSName.Location = new System.Drawing.Point(95, 6);
            this.txtTSName.Name = "txtTSName";
            this.txtTSName.Size = new System.Drawing.Size(143, 25);
            this.txtTSName.TabIndex = 8;
            this.txtTSName.Leave += new System.EventHandler(this.chkValidity);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(4, 9);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(103, 23);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "收費標準名稱：";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.labelX3.Location = new System.Drawing.Point(238, 9);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(72, 23);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "適用性別：";
            // 
            // cboGender
            // 
            this.cboGender.DisplayMember = "Text";
            this.cboGender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGender.FormattingEnabled = true;
            this.cboGender.ItemHeight = 19;
            this.cboGender.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.cboGender.Location = new System.Drawing.Point(305, 6);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(80, 25);
            this.cboGender.TabIndex = 11;
            this.cboGender.SelectedIndexChanged += new System.EventHandler(this.chkValidity);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "全";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "男";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "女";
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AutoGenerateColumns = false;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //this.dataGridViewX1.DataSource = this.tuitionStandardRecordBindingSource;
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
            this.ChargeItem.HeaderText = "繳費項目";
            this.ChargeItem.Name = "ChargeItem";
            this.ChargeItem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ChargeItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ChargeItem.Width = 180;
            this.ChargeItem.Items.AddRange(new object[] {
            this.comboItem1});
            // 
            // Money
            // 
            this.Money.DataPropertyName = "Money";
            this.Money.HeaderText = "金額";
            this.Money.Name = "Money";
            // 
            // 刪除
            // 
            this.刪除.DataPropertyName = "Deleted";
            this.刪除.HeaderText = "刪除";
            this.刪除.Name = "刪除";
            this.刪除.Width = 50;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            this.labelX5.Location = new System.Drawing.Point(9, 65);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(158, 23);
            this.labelX5.TabIndex = 13;
            this.labelX5.Text = "收費標準收費項目明細：";
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
            this.groupPanel1.Controls.Add(this.cboClassYear);
            this.groupPanel1.Controls.Add(this.labelX7);
            this.groupPanel1.Controls.Add(this.cboDept);
            this.groupPanel1.Controls.Add(this.labelX6);
            this.groupPanel1.Controls.Add(this.btnSave);
            this.groupPanel1.Controls.Add(this.btnAddItem);
            this.groupPanel1.Controls.Add(this.dataGridViewX1);
            this.groupPanel1.Controls.Add(this.cboGender);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.txtTSName);
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
            // cboClassYear
            // 
            this.cboClassYear.DisplayMember = "Text";
            this.cboClassYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboClassYear.FormattingEnabled = true;
            this.cboClassYear.ItemHeight = 19;
            this.cboClassYear.Items.AddRange(new object[] {
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10});
            this.cboClassYear.Location = new System.Drawing.Point(305, 30);
            this.cboClassYear.Name = "cboClassYear";
            this.cboClassYear.Size = new System.Drawing.Size(80, 25);
            this.cboClassYear.TabIndex = 19;
            this.cboClassYear.SelectedIndexChanged += new System.EventHandler(this.chkValidity);
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "全年級";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "一年級";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "二年級";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "三年級";
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            this.labelX7.Location = new System.Drawing.Point(238, 33);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(72, 23);
            this.labelX7.TabIndex = 18;
            this.labelX7.Text = "適用年級：";
            // 
            // cboDept
            // 
            this.cboDept.DisplayMember = "Text";
            this.cboDept.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDept.FormattingEnabled = true;
            this.cboDept.ItemHeight = 19;
            this.cboDept.Items.AddRange(new object[] {
            this.comboItem4,
            this.comboItem5,
            this.comboItem6});
            this.cboDept.Location = new System.Drawing.Point(76, 33);
            this.cboDept.Name = "cboDept";
            this.cboDept.Size = new System.Drawing.Size(162, 25);
            this.cboDept.TabIndex = 17;
            this.cboDept.SelectedIndexChanged += new System.EventHandler(this.chkValidity);
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "全";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "男";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "女";
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            this.labelX6.Location = new System.Drawing.Point(5, 36);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(72, 23);
            this.labelX6.TabIndex = 16;
            this.labelX6.Text = "適用科別：";
            // 
            // lstStdView
            //             
            this.lstStdView.Location = new System.Drawing.Point(8, 39);
            this.lstStdView.Name = "lstStdView";
            this.lstStdView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstStdView.MultiSelect = false;
            this.lstStdView.Size = new System.Drawing.Size(208, 344);
            this.lstStdView.TabIndex = 18;
            this.lstStdView.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lstStdView.SelectedIndexChanged+=new System.EventHandler(lstStdView_SelectedIndexChanged);
            this.lstStdView.View = System.Windows.Forms.View.Details;
            this.lstStdView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lstStdView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "收費標準名稱";
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
            // TuitionStandardProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 421);
            this.Controls.Add(this.lstStdView);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lblSemester);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lblSchoolYear);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.btnCopySemData);
            this.Name = "TuitionStandardProcess";
            this.Text = "收費標準維護";
            this.Load += new System.EventHandler(this.TuitionStandardProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tuitionStandardRecordBindingSource)).EndInit();
            this.groupPanel1.ResumeLayout(false);
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
        private DevComponents.DotNetBar.Controls.TextBoxX txtTSName;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGender;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;       
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnAddItem;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDept;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboClassYear;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ListViewEx lstStdView;
        private DevComponents.Editors.ComboItem comboItem10;
        private System.Windows.Forms.BindingSource tuitionStandardRecordBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        private System.Windows.Forms.DataGridViewComboBoxColumn ChargeItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Money;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 刪除;
        private DevComponents.DotNetBar.ButtonX btnCopySemData;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}