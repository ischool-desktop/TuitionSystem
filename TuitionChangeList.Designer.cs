namespace TuitionSystem.TuitionControls
{
    partial class TuitionChangeList
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
            this.btnReport = new DevComponents.DotNetBar.ButtonX();
            this.lnkCancel = new System.Windows.Forms.LinkLabel();
            this.lnkSelAll = new System.Windows.Forms.LinkLabel();
            this.SelectView = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnReport
            // 
            this.btnReport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReport.Location = new System.Drawing.Point(302, 264);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 34;
            this.btnReport.Text = "產生報表";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // lnkCancel
            // 
            this.lnkCancel.AutoSize = true;
            this.lnkCancel.BackColor = System.Drawing.Color.Transparent;
            this.lnkCancel.LinkColor = System.Drawing.SystemColors.ControlText;
            this.lnkCancel.Location = new System.Drawing.Point(75, 52);
            this.lnkCancel.Name = "lnkCancel";
            this.lnkCancel.Size = new System.Drawing.Size(60, 17);
            this.lnkCancel.TabIndex = 33;
            this.lnkCancel.TabStop = true;
            this.lnkCancel.Text = "全部取消";
            this.lnkCancel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCancel_LinkClicked);
            // 
            // lnkSelAll
            // 
            this.lnkSelAll.AutoSize = true;
            this.lnkSelAll.BackColor = System.Drawing.Color.Transparent;
            this.lnkSelAll.LinkColor = System.Drawing.SystemColors.ControlText;
            this.lnkSelAll.Location = new System.Drawing.Point(11, 52);
            this.lnkSelAll.Name = "lnkSelAll";
            this.lnkSelAll.Size = new System.Drawing.Size(60, 17);
            this.lnkSelAll.TabIndex = 32;
            this.lnkSelAll.TabStop = true;
            this.lnkSelAll.Text = "全部選取";
            this.lnkSelAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelAll_LinkClicked);
            // 
            // SelectView
            // 
            this.SelectView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            // 
            // 
            // 
            this.SelectView.Border.Class = "ListViewBorder";
            this.SelectView.CheckBoxes = true;
            this.SelectView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelectView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.SelectView.LabelWrap = false;
            this.SelectView.Location = new System.Drawing.Point(13, 71);
            this.SelectView.Name = "SelectView";
            this.SelectView.Size = new System.Drawing.Size(364, 169);
            this.SelectView.TabIndex = 31;
            this.SelectView.UseCompatibleStateImageBehavior = false;
            this.SelectView.View = System.Windows.Forms.View.List;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(14, 26);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(215, 23);
            this.labelX1.TabIndex = 35;
            this.labelX1.Text = "請選擇欲列印異動項目";
            // 
            // TuitionChangeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 308);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.lnkCancel);
            this.Controls.Add(this.lnkSelAll);
            this.Controls.Add(this.SelectView);
            this.Name = "TuitionChangeList";
            this.Text = "異動清單列印";
            this.Load += new System.EventHandler(this.TuitionChangeList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnReport;
        private System.Windows.Forms.LinkLabel lnkCancel;
        private System.Windows.Forms.LinkLabel lnkSelAll;
        private DevComponents.DotNetBar.Controls.ListViewEx SelectView;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}