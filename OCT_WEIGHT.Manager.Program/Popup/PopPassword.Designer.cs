namespace DK_WEIGHT.Manager.Program.Popup
{
    partial class PopPassword
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.octoButton3 = new DK_WEIGHT.Manager.Common.Control.OctoButton();
            this.octoButton4 = new DK_WEIGHT.Manager.Common.Control.OctoButton();
            this.txtNewPw = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtOldPw = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPw.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPw.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.SkinName = "Office 2013";
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(354, 38);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Location = new System.Drawing.Point(13, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(89, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "비밀번호 변경";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Controls.Add(this.txtNewPw);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.txtOldPw);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 38);
            this.panelControl2.LookAndFeel.SkinName = "Office 2013";
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(354, 192);
            this.panelControl2.TabIndex = 1;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.octoButton3);
            this.panelControl4.Controls.Add(this.octoButton4);
            this.panelControl4.Location = new System.Drawing.Point(59, 94);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(194, 58);
            this.panelControl4.TabIndex = 42;
            // 
            // octoButton3
            // 
            this.octoButton3.AllowYn = "Y";
            this.octoButton3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.octoButton3.Appearance.Options.UseFont = true;
            this.octoButton3.CRUD_type = DK_WEIGHT.Manager.Common.info.CRUDType.R;
            this.octoButton3.Image = global::DK_WEIGHT.Manager.Program.Properties.Resources.icon03_on;
            this.octoButton3.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.octoButton3.Location = new System.Drawing.Point(3, 5);
            this.octoButton3.Name = "octoButton3";
            this.octoButton3.Size = new System.Drawing.Size(90, 47);
            this.octoButton3.TabIndex = 15;
            this.octoButton3.Text = "확인";
            this.octoButton3.Click += new System.EventHandler(this.octoButton3_Click);
            // 
            // octoButton4
            // 
            this.octoButton4.AllowYn = "Y";
            this.octoButton4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.octoButton4.Appearance.Options.UseFont = true;
            this.octoButton4.CRUD_type = DK_WEIGHT.Manager.Common.info.CRUDType.R;
            this.octoButton4.Image = global::DK_WEIGHT.Manager.Program.Properties.Resources.icon09_on;
            this.octoButton4.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.octoButton4.Location = new System.Drawing.Point(99, 4);
            this.octoButton4.Name = "octoButton4";
            this.octoButton4.Size = new System.Drawing.Size(90, 47);
            this.octoButton4.TabIndex = 12;
            this.octoButton4.Text = "닫기";
            // 
            // txtNewPw
            // 
            this.txtNewPw.Location = new System.Drawing.Point(117, 59);
            this.txtNewPw.Name = "txtNewPw";
            this.txtNewPw.Size = new System.Drawing.Size(203, 20);
            this.txtNewPw.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl3.Location = new System.Drawing.Point(12, 58);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(89, 19);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "신규 비밀번호";
            // 
            // txtOldPw
            // 
            this.txtOldPw.Location = new System.Drawing.Point(117, 23);
            this.txtOldPw.Name = "txtOldPw";
            this.txtOldPw.Size = new System.Drawing.Size(203, 20);
            this.txtOldPw.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Location = new System.Drawing.Point(13, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(89, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "기존 비밀번호";
            // 
            // PopPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 230);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PopPassword";
            this.Text = "PopPassword";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPw.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPw.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtNewPw;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtOldPw;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private Common.Control.OctoButton octoButton3;
        private Common.Control.OctoButton octoButton4;
    }
}