namespace OCT_WEIGHT.Manager.Common.Popup
{
    partial class SeinPopup
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnSave = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnPanel = new DevExpress.XtraEditors.PanelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSEIN_MAX = new DevExpress.XtraEditors.TextEdit();
            this.txtREASON = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtCRT_USER = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSEIN_MIN = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPanel)).BeginInit();
            this.btnPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSEIN_MAX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtREASON.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCRT_USER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSEIN_MIN.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.btnPanel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(406, 259);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(5, 191);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(399, 66);
            this.panelControl2.TabIndex = 28;
            // 
            // btnClose
            // 
            this.btnClose.AllowYn = "Y";
            this.btnClose.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnClose.Image = global::OCT_WEIGHT.Manager.Common.Properties.Resources.icon09_on;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(289, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 47);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "닫기 Esc";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AllowYn = "Y";
            this.btnSave.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.U;
            this.btnSave.Image = global::OCT_WEIGHT.Manager.Common.Properties.Resources.icon03_on;
            this.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(179, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 47);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "등록(F6)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPanel
            // 
            this.btnPanel.Controls.Add(this.label1);
            this.btnPanel.Controls.Add(this.labelControl3);
            this.btnPanel.Controls.Add(this.labelControl2);
            this.btnPanel.Controls.Add(this.txtSEIN_MAX);
            this.btnPanel.Controls.Add(this.txtREASON);
            this.btnPanel.Controls.Add(this.labelControl7);
            this.btnPanel.Controls.Add(this.txtCRT_USER);
            this.btnPanel.Controls.Add(this.labelControl1);
            this.btnPanel.Controls.Add(this.txtSEIN_MIN);
            this.btnPanel.Controls.Add(this.labelControl5);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPanel.Location = new System.Drawing.Point(2, 2);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(402, 189);
            this.btnPanel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(7, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(387, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "최소-100 ~ 최대100 까지 입력 가능합니다.(허용치 입력시)";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Blue;
            this.labelControl3.Appearance.BackColor2 = System.Drawing.Color.Blue;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl3.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl3.Location = new System.Drawing.Point(244, 9);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(43, 31);
            this.labelControl3.TabIndex = 46;
            this.labelControl3.Text = "최대";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Blue;
            this.labelControl2.Appearance.BackColor2 = System.Drawing.Color.Blue;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl2.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl2.Location = new System.Drawing.Point(126, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(43, 31);
            this.labelControl2.TabIndex = 45;
            this.labelControl2.Text = "최소";
            // 
            // txtSEIN_MAX
            // 
            this.txtSEIN_MAX.EditValue = "0";
            this.txtSEIN_MAX.Location = new System.Drawing.Point(293, 9);
            this.txtSEIN_MAX.Name = "txtSEIN_MAX";
            this.txtSEIN_MAX.Properties.Appearance.BackColor = System.Drawing.Color.Beige;
            this.txtSEIN_MAX.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtSEIN_MAX.Properties.Appearance.Options.UseBackColor = true;
            this.txtSEIN_MAX.Properties.Appearance.Options.UseFont = true;
            this.txtSEIN_MAX.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSEIN_MAX.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSEIN_MAX.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSEIN_MAX.Size = new System.Drawing.Size(60, 30);
            this.txtSEIN_MAX.TabIndex = 44;
            this.txtSEIN_MAX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSEIN_MAX_KeyPress);
            // 
            // txtREASON
            // 
            this.txtREASON.Location = new System.Drawing.Point(123, 84);
            this.txtREASON.Name = "txtREASON";
            this.txtREASON.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtREASON.Properties.Appearance.Options.UseFont = true;
            this.txtREASON.Properties.MaxLength = 150;
            this.txtREASON.Size = new System.Drawing.Size(274, 30);
            this.txtREASON.TabIndex = 43;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.BackColor = System.Drawing.Color.Blue;
            this.labelControl7.Appearance.BackColor2 = System.Drawing.Color.Blue;
            this.labelControl7.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl7.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl7.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl7.Location = new System.Drawing.Point(10, 83);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(110, 31);
            this.labelControl7.TabIndex = 42;
            this.labelControl7.Text = "등록사유";
            // 
            // txtCRT_USER
            // 
            this.txtCRT_USER.Location = new System.Drawing.Point(123, 47);
            this.txtCRT_USER.Name = "txtCRT_USER";
            this.txtCRT_USER.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtCRT_USER.Properties.Appearance.Options.UseFont = true;
            this.txtCRT_USER.Properties.MaxLength = 10;
            this.txtCRT_USER.Size = new System.Drawing.Size(124, 30);
            this.txtCRT_USER.TabIndex = 33;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Blue;
            this.labelControl1.Appearance.BackColor2 = System.Drawing.Color.Blue;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl1.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl1.Location = new System.Drawing.Point(10, 46);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(110, 31);
            this.labelControl1.TabIndex = 32;
            this.labelControl1.Text = "등록자";
            // 
            // txtSEIN_MIN
            // 
            this.txtSEIN_MIN.EditValue = "0";
            this.txtSEIN_MIN.Location = new System.Drawing.Point(175, 9);
            this.txtSEIN_MIN.Name = "txtSEIN_MIN";
            this.txtSEIN_MIN.Properties.Appearance.BackColor = System.Drawing.Color.Beige;
            this.txtSEIN_MIN.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtSEIN_MIN.Properties.Appearance.Options.UseBackColor = true;
            this.txtSEIN_MIN.Properties.Appearance.Options.UseFont = true;
            this.txtSEIN_MIN.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSEIN_MIN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSEIN_MIN.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSEIN_MIN.Size = new System.Drawing.Size(60, 30);
            this.txtSEIN_MIN.TabIndex = 0;
            this.txtSEIN_MIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSEIN_MIN_KeyPress);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.Blue;
            this.labelControl5.Appearance.BackColor2 = System.Drawing.Color.Blue;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl5.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl5.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl5.Location = new System.Drawing.Point(10, 9);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(110, 31);
            this.labelControl5.TabIndex = 30;
            this.labelControl5.Text = "출차오차범위";
            // 
            // SeinPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 259);
            this.Controls.Add(this.panelControl1);
            this.Name = "SeinPopup";
            this.Text = "SeinPopup(출하오차범위 저장)";
            this.Load += new System.EventHandler(this.SeinPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPanel)).EndInit();
            this.btnPanel.ResumeLayout(false);
            this.btnPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSEIN_MAX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtREASON.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCRT_USER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSEIN_MIN.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl btnPanel;
        private DevExpress.XtraEditors.TextEdit txtSEIN_MIN;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtCRT_USER;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtREASON;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Control.OctoButton btnClose;
        private Control.OctoButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtSEIN_MAX;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Label label1;
    }
}