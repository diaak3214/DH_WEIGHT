namespace OCT_WEIGHT.Manager.Common.Popup
{
    partial class UserPopup
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
            this.txtNewPw = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtNewPw_CHK = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.octoButton3 = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.octoButton4 = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtOldPw = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPw.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPw_CHK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPw.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNewPw
            // 
            this.txtNewPw.Location = new System.Drawing.Point(117, 59);
            this.txtNewPw.Name = "txtNewPw";
            this.txtNewPw.Size = new System.Drawing.Size(203, 20);
            this.txtNewPw.TabIndex = 4;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.txtNewPw_CHK);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Controls.Add(this.txtNewPw);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.txtOldPw);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 38);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(346, 197);
            this.panelControl2.TabIndex = 3;
            // 
            // txtNewPw_CHK
            // 
            this.txtNewPw_CHK.Location = new System.Drawing.Point(118, 93);
            this.txtNewPw_CHK.Name = "txtNewPw_CHK";
            this.txtNewPw_CHK.Size = new System.Drawing.Size(203, 20);
            this.txtNewPw_CHK.TabIndex = 44;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl4.Location = new System.Drawing.Point(13, 92);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(89, 19);
            this.labelControl4.TabIndex = 43;
            this.labelControl4.Text = "비밀번호 확인";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.octoButton3);
            this.panelControl4.Controls.Add(this.octoButton4);
            this.panelControl4.Location = new System.Drawing.Point(59, 127);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(194, 58);
            this.panelControl4.TabIndex = 42;
            // 
            // octoButton3
            // 
            this.octoButton3.AllowYn = "Y";
            this.octoButton3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.octoButton3.Appearance.Options.UseFont = true;
            this.octoButton3.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.octoButton3.Image = global::OCT_WEIGHT.Manager.Common.Properties.Resources.icon03_on;
            this.octoButton3.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.octoButton3.Location = new System.Drawing.Point(4, 6);
            this.octoButton3.Name = "octoButton3";
            this.octoButton3.Size = new System.Drawing.Size(90, 47);
            this.octoButton3.TabIndex = 17;
            this.octoButton3.Text = "확인";
            this.octoButton3.Click += new System.EventHandler(this.octoButton3_Click);
            // 
            // octoButton4
            // 
            this.octoButton4.AllowYn = "Y";
            this.octoButton4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.octoButton4.Appearance.Options.UseFont = true;
            this.octoButton4.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.octoButton4.Image = global::OCT_WEIGHT.Manager.Common.Properties.Resources.icon09_on;
            this.octoButton4.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.octoButton4.Location = new System.Drawing.Point(100, 5);
            this.octoButton4.Name = "octoButton4";
            this.octoButton4.Size = new System.Drawing.Size(90, 47);
            this.octoButton4.TabIndex = 16;
            this.octoButton4.Text = "닫기";
            this.octoButton4.Click += new System.EventHandler(this.octoButton4_Click);
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
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.panelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Appearance.Options.UseFont = true;
            this.panelControl1.Appearance.Options.UseForeColor = true;
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(346, 38);
            this.panelControl1.TabIndex = 2;
            this.panelControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelControl1_MouseDoubleClick);
            this.panelControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelControl1_MouseDown);
            this.panelControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelControl1_MouseMove);
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
            // UserPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 235);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserPopup";
            this.Text = "UserPopup";
            this.Load += new System.EventHandler(this.UserPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPw.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPw_CHK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPw.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtNewPw;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtOldPw;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Control.OctoButton octoButton3;
        private Control.OctoButton octoButton4;
        private DevExpress.XtraEditors.TextEdit txtNewPw_CHK;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}