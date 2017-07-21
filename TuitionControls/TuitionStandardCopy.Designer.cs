namespace TuitionSystem.TuitionControls
{
    partial class TuitionStandardCopy
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
            this.cboGender = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtTSName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnSure = new DevComponents.DotNetBar.ButtonX();
            this.cboSemester = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem11 = new DevComponents.Editors.ComboItem();
            this.comboItem12 = new DevComponents.Editors.ComboItem();
            this.intSchoolYear = new DevComponents.Editors.IntegerInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolYear)).BeginInit();
            this.SuspendLayout();
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
            this.cboClassYear.Location = new System.Drawing.Point(73, 132);
            this.cboClassYear.Name = "cboClassYear";
            this.cboClassYear.Size = new System.Drawing.Size(81, 25);
            this.cboClassYear.TabIndex = 27;
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
            this.labelX7.Location = new System.Drawing.Point(1, 132);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(72, 23);
            this.labelX7.TabIndex = 26;
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
            this.cboDept.Location = new System.Drawing.Point(73, 72);
            this.cboDept.Name = "cboDept";
            this.cboDept.Size = new System.Drawing.Size(162, 25);
            this.cboDept.TabIndex = 25;
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
            this.labelX6.Location = new System.Drawing.Point(1, 72);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(72, 23);
            this.labelX6.TabIndex = 24;
            this.labelX6.Text = "適用科別：";
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
            this.cboGender.Location = new System.Drawing.Point(73, 101);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(81, 25);
            this.cboGender.TabIndex = 23;
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
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.labelX3.Location = new System.Drawing.Point(1, 101);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(72, 23);
            this.labelX3.TabIndex = 22;
            this.labelX3.Text = "適用性別：";
            // 
            // txtTSName
            // 
            // 
            // 
            // 
            this.txtTSName.Border.Class = "TextBoxBorder";
            this.txtTSName.Location = new System.Drawing.Point(92, 40);
            this.txtTSName.Name = "txtTSName";
            this.txtTSName.Size = new System.Drawing.Size(143, 25);
            this.txtTSName.TabIndex = 20;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(1, 43);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(103, 23);
            this.labelX2.TabIndex = 21;
            this.labelX2.Text = "收費標準名稱：";
            // 
            // btnSure
            // 
            this.btnSure.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSure.BackColor = System.Drawing.Color.Transparent;
            this.btnSure.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSure.Location = new System.Drawing.Point(55, 163);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 28;
            this.btnSure.Text = "確定";
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // cboSemester
            // 
            this.cboSemester.DisplayMember = "Text";
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.ItemHeight = 19;
            this.cboSemester.Items.AddRange(new object[] {
            this.comboItem11,
            this.comboItem12});
            this.cboSemester.Location = new System.Drawing.Point(170, 12);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(65, 25);
            this.cboSemester.TabIndex = 30;
            // 
            // comboItem11
            // 
            this.comboItem11.Text = "上學期";
            // 
            // comboItem12
            // 
            this.comboItem12.Text = "下學期";
            // 
            // intSchoolYear
            // 
            this.intSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intSchoolYear.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSchoolYear.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSchoolYear.Location = new System.Drawing.Point(58, 12);
            this.intSchoolYear.MaxValue = 120;
            this.intSchoolYear.MinValue = 98;
            this.intSchoolYear.Name = "intSchoolYear";
            this.intSchoolYear.ShowUpDown = true;
            this.intSchoolYear.Size = new System.Drawing.Size(54, 25);
            this.intSchoolYear.TabIndex = 29;
            this.intSchoolYear.Value = 98;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(1, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(63, 23);
            this.labelX1.TabIndex = 31;
            this.labelX1.Text = "學年度：";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            this.labelX4.Location = new System.Drawing.Point(128, 14);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(51, 23);
            this.labelX4.TabIndex = 32;
            this.labelX4.Text = "學期：";
            // 
            // TuitionStandardCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 194);
            this.Controls.Add(this.cboSemester);
            this.Controls.Add(this.intSchoolYear);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.cboClassYear);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.cboDept);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.cboGender);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtTSName);
            this.Controls.Add(this.labelX2);
            this.Name = "TuitionStandardCopy";
            this.Text = "收費標準複製";
            this.Load += new System.EventHandler(this.TuitionStandardCopy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolYear)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cboClassYear;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDept;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGender;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTSName;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnSure;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSemester;
        private DevComponents.Editors.ComboItem comboItem11;
        private DevComponents.Editors.ComboItem comboItem12;
        private DevComponents.Editors.IntegerInput intSchoolYear;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX4;
    }
}