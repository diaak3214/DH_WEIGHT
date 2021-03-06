namespace OCT_WEIGHT.Manager.Common.Popup
{
    partial class ReceiptPopup
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
            this.btnEnterClear = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnClose = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnSave = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnPanel = new DevExpress.XtraEditors.PanelControl();
            this.txtITEM_NM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtVENDOR_NM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtDIS_CLASS = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtDRV_NM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDRV_TEL_NO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtREMARK = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCAR_NO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCARD_NO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.timer_rfid = new System.Windows.Forms.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPanel)).BeginInit();
            this.btnPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVENDOR_NM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDIS_CLASS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRV_NM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRV_TEL_NO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtREMARK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCAR_NO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCARD_NO.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.btnPanel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(406, 351);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnEnterClear);
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(5, 294);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(399, 55);
            this.panelControl2.TabIndex = 28;
            // 
            // btnEnterClear
            // 
            this.btnEnterClear.AllowYn = "Y";
            this.btnEnterClear.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnEnterClear.Appearance.Options.UseFont = true;
            this.btnEnterClear.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnEnterClear.Image = global::OCT_WEIGHT.Manager.Common.Properties.Resources.icon04_on;
            this.btnEnterClear.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnEnterClear.Location = new System.Drawing.Point(22, 5);
            this.btnEnterClear.Name = "btnEnterClear";
            this.btnEnterClear.Size = new System.Drawing.Size(120, 47);
            this.btnEnterClear.TabIndex = 225;
            this.btnEnterClear.Text = "출입해제";
            this.btnEnterClear.Visible = false;
            this.btnEnterClear.Click += new System.EventHandler(this.btnEnterClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowYn = "Y";
            this.btnClose.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnClose.Image = global::OCT_WEIGHT.Manager.Common.Properties.Resources.icon09_on;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(274, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 47);
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
            this.btnSave.Location = new System.Drawing.Point(148, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 47);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "저장(F6)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPanel
            // 
            this.btnPanel.Controls.Add(this.txtITEM_NM);
            this.btnPanel.Controls.Add(this.labelControl8);
            this.btnPanel.Controls.Add(this.txtVENDOR_NM);
            this.btnPanel.Controls.Add(this.labelControl7);
            this.btnPanel.Controls.Add(this.txtDIS_CLASS);
            this.btnPanel.Controls.Add(this.labelControl6);
            this.btnPanel.Controls.Add(this.txtDRV_NM);
            this.btnPanel.Controls.Add(this.labelControl4);
            this.btnPanel.Controls.Add(this.txtDRV_TEL_NO);
            this.btnPanel.Controls.Add(this.labelControl3);
            this.btnPanel.Controls.Add(this.txtREMARK);
            this.btnPanel.Controls.Add(this.labelControl2);
            this.btnPanel.Controls.Add(this.txtCAR_NO);
            this.btnPanel.Controls.Add(this.labelControl1);
            this.btnPanel.Controls.Add(this.txtCARD_NO);
            this.btnPanel.Controls.Add(this.labelControl5);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPanel.Location = new System.Drawing.Point(2, 2);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(402, 292);
            this.btnPanel.TabIndex = 2;
            // 
            // txtITEM_NM
            // 
            this.txtITEM_NM.Location = new System.Drawing.Point(123, 150);
            this.txtITEM_NM.Name = "txtITEM_NM";
            this.txtITEM_NM.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtITEM_NM.Properties.Appearance.Options.UseFont = true;
            this.txtITEM_NM.Properties.MaxLength = 10;
            this.txtITEM_NM.Properties.ReadOnly = true;
            this.txtITEM_NM.Size = new System.Drawing.Size(274, 30);
            this.txtITEM_NM.TabIndex = 45;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.BackColor = System.Drawing.Color.Blue;
            this.labelControl8.Appearance.BackColor2 = System.Drawing.Color.Blue;
            this.labelControl8.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl8.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl8.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl8.Location = new System.Drawing.Point(10, 149);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(110, 31);
            this.labelControl8.TabIndex = 44;
            this.labelControl8.Text = "품명구분";
            // 
            // txtVENDOR_NM
            // 
            this.txtVENDOR_NM.Location = new System.Drawing.Point(123, 80);
            this.txtVENDOR_NM.Name = "txtVENDOR_NM";
            this.txtVENDOR_NM.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtVENDOR_NM.Properties.Appearance.Options.UseFont = true;
            this.txtVENDOR_NM.Properties.MaxLength = 10;
            this.txtVENDOR_NM.Properties.ReadOnly = true;
            this.txtVENDOR_NM.Size = new System.Drawing.Size(274, 30);
            this.txtVENDOR_NM.TabIndex = 43;
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
            this.labelControl7.Location = new System.Drawing.Point(10, 79);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(110, 31);
            this.labelControl7.TabIndex = 42;
            this.labelControl7.Text = "소속회사";
            // 
            // txtDIS_CLASS
            // 
            this.txtDIS_CLASS.Location = new System.Drawing.Point(123, 115);
            this.txtDIS_CLASS.Name = "txtDIS_CLASS";
            this.txtDIS_CLASS.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtDIS_CLASS.Properties.Appearance.Options.UseFont = true;
            this.txtDIS_CLASS.Properties.MaxLength = 10;
            this.txtDIS_CLASS.Properties.ReadOnly = true;
            this.txtDIS_CLASS.Size = new System.Drawing.Size(274, 30);
            this.txtDIS_CLASS.TabIndex = 41;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.BackColor = System.Drawing.Color.Blue;
            this.labelControl6.Appearance.BackColor2 = System.Drawing.Color.Blue;
            this.labelControl6.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl6.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl6.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl6.Location = new System.Drawing.Point(10, 114);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(110, 31);
            this.labelControl6.TabIndex = 40;
            this.labelControl6.Text = "물류구분";
            // 
            // txtDRV_NM
            // 
            this.txtDRV_NM.Location = new System.Drawing.Point(123, 185);
            this.txtDRV_NM.Name = "txtDRV_NM";
            this.txtDRV_NM.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtDRV_NM.Properties.Appearance.Options.UseFont = true;
            this.txtDRV_NM.Properties.MaxLength = 20;
            this.txtDRV_NM.Properties.ReadOnly = true;
            this.txtDRV_NM.Size = new System.Drawing.Size(274, 30);
            this.txtDRV_NM.TabIndex = 39;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.Blue;
            this.labelControl4.Appearance.BackColor2 = System.Drawing.Color.Blue;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl4.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl4.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl4.Location = new System.Drawing.Point(10, 184);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(110, 31);
            this.labelControl4.TabIndex = 38;
            this.labelControl4.Text = "운 전 자";
            // 
            // txtDRV_TEL_NO
            // 
            this.txtDRV_TEL_NO.Location = new System.Drawing.Point(123, 220);
            this.txtDRV_TEL_NO.Name = "txtDRV_TEL_NO";
            this.txtDRV_TEL_NO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtDRV_TEL_NO.Properties.Appearance.Options.UseFont = true;
            this.txtDRV_TEL_NO.Properties.MaxLength = 20;
            this.txtDRV_TEL_NO.Properties.ReadOnly = true;
            this.txtDRV_TEL_NO.Size = new System.Drawing.Size(274, 30);
            this.txtDRV_TEL_NO.TabIndex = 37;
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
            this.labelControl3.Location = new System.Drawing.Point(10, 219);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(110, 31);
            this.labelControl3.TabIndex = 36;
            this.labelControl3.Text = "전화번호";
            // 
            // txtREMARK
            // 
            this.txtREMARK.Location = new System.Drawing.Point(123, 255);
            this.txtREMARK.Name = "txtREMARK";
            this.txtREMARK.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtREMARK.Properties.Appearance.Options.UseFont = true;
            this.txtREMARK.Properties.MaxLength = 150;
            this.txtREMARK.Size = new System.Drawing.Size(274, 30);
            this.txtREMARK.TabIndex = 35;
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
            this.labelControl2.Location = new System.Drawing.Point(10, 254);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(110, 31);
            this.labelControl2.TabIndex = 34;
            this.labelControl2.Text = "적      요";
            // 
            // txtCAR_NO
            // 
            this.txtCAR_NO.Location = new System.Drawing.Point(123, 45);
            this.txtCAR_NO.Name = "txtCAR_NO";
            this.txtCAR_NO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtCAR_NO.Properties.Appearance.Options.UseFont = true;
            this.txtCAR_NO.Properties.MaxLength = 11;
            this.txtCAR_NO.Properties.ReadOnly = true;
            this.txtCAR_NO.Size = new System.Drawing.Size(274, 30);
            this.txtCAR_NO.TabIndex = 33;
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
            this.labelControl1.Location = new System.Drawing.Point(10, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(110, 31);
            this.labelControl1.TabIndex = 32;
            this.labelControl1.Text = "차량번호";
            // 
            // txtCARD_NO
            // 
            this.txtCARD_NO.Location = new System.Drawing.Point(123, 10);
            this.txtCARD_NO.Name = "txtCARD_NO";
            this.txtCARD_NO.Properties.Appearance.BackColor = System.Drawing.Color.Beige;
            this.txtCARD_NO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtCARD_NO.Properties.Appearance.Options.UseBackColor = true;
            this.txtCARD_NO.Properties.Appearance.Options.UseFont = true;
            this.txtCARD_NO.Properties.MaxLength = 20;
            this.txtCARD_NO.Size = new System.Drawing.Size(275, 30);
            this.txtCARD_NO.TabIndex = 31;
            this.txtCARD_NO.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCARD_NO_KeyUp);
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
            this.labelControl5.Text = "카드번호";
            // 
            // timer_rfid
            // 
            this.timer_rfid.Interval = 500;
            this.timer_rfid.Tick += new System.EventHandler(this.timer_rfid_Tick);
            // 
            // ReceiptPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 351);
            this.Controls.Add(this.panelControl1);
            this.Name = "ReceiptPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReceiptPopup(접수)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReceiptPopup_FormClosed);
            this.Load += new System.EventHandler(this.ReceiptPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPanel)).EndInit();
            this.btnPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVENDOR_NM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDIS_CLASS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRV_NM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRV_TEL_NO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtREMARK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCAR_NO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCARD_NO.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl btnPanel;
        private DevExpress.XtraEditors.TextEdit txtCARD_NO;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtCAR_NO;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtVENDOR_NM;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtDIS_CLASS;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtDRV_NM;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtDRV_TEL_NO;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtREMARK;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Control.OctoButton btnClose;
        private Control.OctoButton btnSave;
        private System.Windows.Forms.Timer timer_rfid;
        private DevExpress.XtraEditors.TextEdit txtITEM_NM;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private Control.OctoButton btnEnterClear;
    }
}