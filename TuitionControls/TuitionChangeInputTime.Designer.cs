namespace TuitionSystem.TuitionControls
{
    partial class TuitionChangeInputTime
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lblSemester = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.lblSchoolYear = new DevComponents.DotNetBar.LabelX();
            this.txtBegin = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtEnd = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(128, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(100, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "線上輸入時間";
            // 
            // lblSemester
            // 
            this.lblSemester.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblSemester.BackgroundStyle.Class = "";
            this.lblSemester.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSemester.Location = new System.Drawing.Point(85, 12);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(46, 23);
            this.lblSemester.TabIndex = 7;
            this.lblSemester.Text = "上學期";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(40, 12);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(49, 23);
            this.labelX4.TabIndex = 8;
            this.labelX4.Text = "學年度";
            // 
            // lblSchoolYear
            // 
            this.lblSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblSchoolYear.BackgroundStyle.Class = "";
            this.lblSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSchoolYear.Location = new System.Drawing.Point(12, 12);
            this.lblSchoolYear.Name = "lblSchoolYear";
            this.lblSchoolYear.Size = new System.Drawing.Size(38, 23);
            this.lblSchoolYear.TabIndex = 6;
            this.lblSchoolYear.Text = "98";
            // 
            // txtBegin
            // 
            // 
            // 
            // 
            this.txtBegin.Border.Class = "TextBoxBorder";
            this.txtBegin.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBegin.Location = new System.Drawing.Point(86, 39);
            this.txtBegin.Name = "txtBegin";
            this.txtBegin.Size = new System.Drawing.Size(199, 25);
            this.txtBegin.TabIndex = 10;
            this.txtBegin.WatermarkText = "2017/08/01 08:00:00";
            this.txtBegin.TextChanged += new System.EventHandler(this.verify);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 41);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(74, 21);
            this.labelX2.TabIndex = 11;
            this.labelX2.Text = "開始時間：";
            // 
            // txtEnd
            // 
            // 
            // 
            // 
            this.txtEnd.Border.Class = "TextBoxBorder";
            this.txtEnd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEnd.Location = new System.Drawing.Point(86, 66);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(199, 25);
            this.txtEnd.TabIndex = 12;
            this.txtEnd.WatermarkText = "2017/08/20 23:59:59";
            this.txtEnd.TextChanged += new System.EventHandler(this.verify);
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 68);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(74, 21);
            this.labelX3.TabIndex = 13;
            this.labelX3.Text = "結束時間：";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(233, 97);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(52, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TuitionChangeInputTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 130);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtBegin);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lblSemester);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lblSchoolYear);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.Name = "TuitionChangeInputTime";
            this.Text = "設定輸入時間";
            this.Load += new System.EventHandler(this.TuitionChangeInputTime_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lblSemester;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX lblSchoolYear;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBegin;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEnd;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnSave;
    }
}