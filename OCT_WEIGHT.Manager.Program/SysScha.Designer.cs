namespace OCT_WEIGHT.Manager.Program
{
    partial class SysScha
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.leSchaFlag = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.START_DATE = new DevExpress.XtraEditors.DateEdit();
            this.txtCEO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtAddr = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSCHANM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.BtnNew = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.BtnSave = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.BtnSearch = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.BtnExcel = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.BtnClose = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.grdScha = new JPlatform.Client.Controls.GridControlEx();
            this.gvwScha = new JPlatform.Client.Controls.GridViewEx();
            this.CUST = new JPlatform.Client.Controls.GridColumnEx();
            this.CUST_SEQ = new JPlatform.Client.Controls.GridColumnEx();
            this.SCHA_NAME = new JPlatform.Client.Controls.GridColumnEx();
            this.SCHA_ADDR = new JPlatform.Client.Controls.GridColumnEx();
            this.SCHA_DPJ = new JPlatform.Client.Controls.GridColumnEx();
            this.SCHA_DATE = new JPlatform.Client.Controls.GridColumnEx();
            this.REJECT_DATE = new JPlatform.Client.Controls.GridColumnEx();
            this.TRP_CMP_TP = new JPlatform.Client.Controls.GridColumnEx();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leSchaFlag.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.START_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.START_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCEO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSCHANM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdScha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwScha)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.textBox1);
            this.panelControl1.Controls.Add(this.leSchaFlag);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.START_DATE);
            this.panelControl1.Controls.Add(this.txtCEO);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtAddr);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtSCHANM);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1317, 61);
            this.panelControl1.TabIndex = 15;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(628, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(108, 22);
            this.textBox1.TabIndex = 65;
            this.textBox1.Visible = false;
            // 
            // leSchaFlag
            // 
            this.leSchaFlag.EditValue = "<Null>";
            this.leSchaFlag.Location = new System.Drawing.Point(628, 7);
            this.leSchaFlag.Name = "leSchaFlag";
            this.leSchaFlag.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.leSchaFlag.Properties.Appearance.Options.UseFont = true;
            this.leSchaFlag.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leSchaFlag.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE", "구분"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE_NAME", "구분명")});
            this.leSchaFlag.Size = new System.Drawing.Size(157, 26);
            this.leSchaFlag.TabIndex = 64;
            this.leSchaFlag.Visible = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl4.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl4.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl4.Location = new System.Drawing.Point(529, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(93, 31);
            this.labelControl4.TabIndex = 58;
            this.labelControl4.Text = "운송업체구분";
            this.labelControl4.Visible = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.LightCoral;
            this.labelControl3.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl3.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl3.Location = new System.Drawing.Point(267, 45);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(93, 31);
            this.labelControl3.TabIndex = 57;
            this.labelControl3.Text = "등록일자";
            this.labelControl3.Visible = false;
            // 
            // START_DATE
            // 
            this.START_DATE.EditValue = null;
            this.START_DATE.Location = new System.Drawing.Point(366, 48);
            this.START_DATE.Name = "START_DATE";
            this.START_DATE.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.START_DATE.Properties.Appearance.Options.UseFont = true;
            this.START_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.START_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.START_DATE.Size = new System.Drawing.Size(158, 26);
            this.START_DATE.TabIndex = 56;
            this.START_DATE.Visible = false;
            // 
            // txtCEO
            // 
            this.txtCEO.Location = new System.Drawing.Point(366, 7);
            this.txtCEO.Name = "txtCEO";
            this.txtCEO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtCEO.Properties.Appearance.Options.UseFont = true;
            this.txtCEO.Size = new System.Drawing.Size(157, 26);
            this.txtCEO.TabIndex = 55;
            this.txtCEO.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl2.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl2.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl2.Location = new System.Drawing.Point(267, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(93, 31);
            this.labelControl2.TabIndex = 54;
            this.labelControl2.Text = "대표자";
            this.labelControl2.Visible = false;
            // 
            // txtAddr
            // 
            this.txtAddr.Location = new System.Drawing.Point(104, 48);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtAddr.Properties.Appearance.Options.UseFont = true;
            this.txtAddr.Size = new System.Drawing.Size(157, 26);
            this.txtAddr.TabIndex = 53;
            this.txtAddr.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl1.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl1.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl1.Location = new System.Drawing.Point(5, 45);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(93, 31);
            this.labelControl1.TabIndex = 52;
            this.labelControl1.Text = "운송사주소";
            this.labelControl1.Visible = false;
            // 
            // txtSCHANM
            // 
            this.txtSCHANM.Location = new System.Drawing.Point(104, 15);
            this.txtSCHANM.Name = "txtSCHANM";
            this.txtSCHANM.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtSCHANM.Properties.Appearance.Options.UseFont = true;
            this.txtSCHANM.Size = new System.Drawing.Size(157, 26);
            this.txtSCHANM.TabIndex = 51;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl8.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl8.Appearance.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl8.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl8.Location = new System.Drawing.Point(5, 12);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(93, 31);
            this.labelControl8.TabIndex = 42;
            this.labelControl8.Text = "운송사명";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.BtnNew);
            this.panelControl4.Controls.Add(this.BtnSave);
            this.panelControl4.Controls.Add(this.BtnSearch);
            this.panelControl4.Controls.Add(this.BtnExcel);
            this.panelControl4.Controls.Add(this.BtnClose);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl4.Location = new System.Drawing.Point(828, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(487, 57);
            this.panelControl4.TabIndex = 41;
            // 
            // BtnNew
            // 
            this.BtnNew.AllowYn = "Y";
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.BtnNew.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon02_on;
            this.BtnNew.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnNew.Location = new System.Drawing.Point(99, 6);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(90, 47);
            this.BtnNew.TabIndex = 17;
            this.BtnNew.Text = "초기화";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.AllowYn = "Y";
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.BtnSave.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon03_on;
            this.BtnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnSave.Location = new System.Drawing.Point(195, 6);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(90, 47);
            this.BtnSave.TabIndex = 16;
            this.BtnSave.Text = "저장";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.AllowYn = "Y";
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.BtnSearch.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon01_on;
            this.BtnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnSearch.Location = new System.Drawing.Point(3, 6);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(90, 47);
            this.BtnSearch.TabIndex = 15;
            this.BtnSearch.Text = "조회";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnExcel
            // 
            this.BtnExcel.AllowYn = "Y";
            this.BtnExcel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BtnExcel.Appearance.Options.UseFont = true;
            this.BtnExcel.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.BtnExcel.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon06_on;
            this.BtnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnExcel.Location = new System.Drawing.Point(291, 6);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(90, 47);
            this.BtnExcel.TabIndex = 14;
            this.BtnExcel.Text = "엑셀";
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.AllowYn = "Y";
            this.BtnClose.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BtnClose.Appearance.Options.UseFont = true;
            this.BtnClose.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.BtnClose.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon09_on;
            this.BtnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnClose.Location = new System.Drawing.Point(387, 6);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(90, 47);
            this.BtnClose.TabIndex = 12;
            this.BtnClose.Text = "닫기";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // grdScha
            // 
            this.grdScha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdScha.Location = new System.Drawing.Point(0, 61);
            this.grdScha.MainView = this.gvwScha;
            this.grdScha.Name = "grdScha";
            this.grdScha.Size = new System.Drawing.Size(1317, 808);
            this.grdScha.TabIndex = 18;
            this.grdScha.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwScha});
            this.grdScha.Click += new System.EventHandler(this.grdScha_Click);
            // 
            // gvwScha
            // 
            this.gvwScha.ActionMode = JPlatform.Client.Controls.ActionMode.View;
            this.gvwScha.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvwScha.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvwScha.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gvwScha.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvwScha.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvwScha.Appearance.FocusedRow.Options.UseFont = true;
            this.gvwScha.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvwScha.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gvwScha.ColumnPanelRowHeight = 30;
            this.gvwScha.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CUST,
            this.CUST_SEQ,
            this.SCHA_NAME,
            this.SCHA_ADDR,
            this.SCHA_DPJ,
            this.SCHA_DATE,
            this.REJECT_DATE,
            this.TRP_CMP_TP});
            this.gvwScha.GridControl = this.grdScha;
            this.gvwScha.IndicatorWidth = 35;
            this.gvwScha.Name = "gvwScha";
            this.gvwScha.OptionsView.ColumnAutoWidth = false;
            this.gvwScha.OptionsView.ShowGroupPanel = false;
            this.gvwScha.PaintStyleName = "MixedXP";
            this.gvwScha.RowHeight = 30;
            // 
            // CUST
            // 
            this.CUST.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.CUST.AppearanceCell.Options.UseFont = true;
            this.CUST.AppearanceCell.Options.UseTextOptions = true;
            this.CUST.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CUST.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CUST.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.CUST.AppearanceHeader.Options.UseFont = true;
            this.CUST.AppearanceHeader.Options.UseTextOptions = true;
            this.CUST.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CUST.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CUST.BindingField = "CUST";
            this.CUST.Caption = "거래처코드";
            this.CUST.ColumnEdit = null;
            this.CUST.FieldName = "CUST";
            this.CUST.Name = "CUST";
            this.CUST.Visible = true;
            this.CUST.VisibleIndex = 0;
            this.CUST.Width = 130;
            // 
            // CUST_SEQ
            // 
            this.CUST_SEQ.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.CUST_SEQ.AppearanceCell.Options.UseFont = true;
            this.CUST_SEQ.AppearanceCell.Options.UseTextOptions = true;
            this.CUST_SEQ.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CUST_SEQ.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CUST_SEQ.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.CUST_SEQ.AppearanceHeader.Options.UseFont = true;
            this.CUST_SEQ.AppearanceHeader.Options.UseTextOptions = true;
            this.CUST_SEQ.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CUST_SEQ.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CUST_SEQ.BindingField = "CUST_SEQ";
            this.CUST_SEQ.Caption = "운송사코드";
            this.CUST_SEQ.ColumnEdit = null;
            this.CUST_SEQ.FieldName = "CUST_SEQ";
            this.CUST_SEQ.Name = "CUST_SEQ";
            this.CUST_SEQ.Visible = true;
            this.CUST_SEQ.VisibleIndex = 1;
            this.CUST_SEQ.Width = 189;
            // 
            // SCHA_NAME
            // 
            this.SCHA_NAME.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.SCHA_NAME.AppearanceCell.Options.UseFont = true;
            this.SCHA_NAME.AppearanceCell.Options.UseTextOptions = true;
            this.SCHA_NAME.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SCHA_NAME.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SCHA_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.SCHA_NAME.AppearanceHeader.Options.UseFont = true;
            this.SCHA_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.SCHA_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SCHA_NAME.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SCHA_NAME.BindingField = "SCHA_NAME";
            this.SCHA_NAME.Caption = "운송사명";
            this.SCHA_NAME.ColumnEdit = null;
            this.SCHA_NAME.FieldName = "SCHA_NAME";
            this.SCHA_NAME.Name = "SCHA_NAME";
            this.SCHA_NAME.Visible = true;
            this.SCHA_NAME.VisibleIndex = 2;
            this.SCHA_NAME.Width = 168;
            // 
            // SCHA_ADDR
            // 
            this.SCHA_ADDR.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.SCHA_ADDR.AppearanceCell.Options.UseFont = true;
            this.SCHA_ADDR.AppearanceCell.Options.UseTextOptions = true;
            this.SCHA_ADDR.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SCHA_ADDR.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SCHA_ADDR.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.SCHA_ADDR.AppearanceHeader.Options.UseFont = true;
            this.SCHA_ADDR.AppearanceHeader.Options.UseTextOptions = true;
            this.SCHA_ADDR.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SCHA_ADDR.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SCHA_ADDR.BindingField = "SCHA_ADDR";
            this.SCHA_ADDR.Caption = "운송사주소";
            this.SCHA_ADDR.ColumnEdit = null;
            this.SCHA_ADDR.FieldName = "SCHA_ADDR";
            this.SCHA_ADDR.Name = "SCHA_ADDR";
            this.SCHA_ADDR.Visible = true;
            this.SCHA_ADDR.VisibleIndex = 3;
            this.SCHA_ADDR.Width = 159;
            // 
            // SCHA_DPJ
            // 
            this.SCHA_DPJ.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.SCHA_DPJ.AppearanceCell.Options.UseFont = true;
            this.SCHA_DPJ.AppearanceCell.Options.UseTextOptions = true;
            this.SCHA_DPJ.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SCHA_DPJ.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SCHA_DPJ.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.SCHA_DPJ.AppearanceHeader.Options.UseFont = true;
            this.SCHA_DPJ.AppearanceHeader.Options.UseTextOptions = true;
            this.SCHA_DPJ.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SCHA_DPJ.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SCHA_DPJ.BindingField = "SCHA_DPJ";
            this.SCHA_DPJ.Caption = "대표자";
            this.SCHA_DPJ.ColumnEdit = null;
            this.SCHA_DPJ.FieldName = "SCHA_DPJ";
            this.SCHA_DPJ.Name = "SCHA_DPJ";
            this.SCHA_DPJ.Visible = true;
            this.SCHA_DPJ.VisibleIndex = 4;
            this.SCHA_DPJ.Width = 104;
            // 
            // SCHA_DATE
            // 
            this.SCHA_DATE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.SCHA_DATE.AppearanceCell.Options.UseFont = true;
            this.SCHA_DATE.AppearanceCell.Options.UseTextOptions = true;
            this.SCHA_DATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SCHA_DATE.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SCHA_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.SCHA_DATE.AppearanceHeader.Options.UseFont = true;
            this.SCHA_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.SCHA_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SCHA_DATE.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SCHA_DATE.BindingField = "SCHA_DATE";
            this.SCHA_DATE.Caption = "등록일";
            this.SCHA_DATE.ColumnEdit = null;
            this.SCHA_DATE.FieldName = "SCHA_DATE";
            this.SCHA_DATE.Name = "SCHA_DATE";
            this.SCHA_DATE.Visible = true;
            this.SCHA_DATE.VisibleIndex = 5;
            this.SCHA_DATE.Width = 169;
            // 
            // REJECT_DATE
            // 
            this.REJECT_DATE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.REJECT_DATE.AppearanceCell.Options.UseFont = true;
            this.REJECT_DATE.AppearanceCell.Options.UseTextOptions = true;
            this.REJECT_DATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.REJECT_DATE.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.REJECT_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.REJECT_DATE.AppearanceHeader.Options.UseFont = true;
            this.REJECT_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.REJECT_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.REJECT_DATE.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.REJECT_DATE.BindingField = "REJECT_DATE";
            this.REJECT_DATE.Caption = "출입정지일자";
            this.REJECT_DATE.ColumnEdit = null;
            this.REJECT_DATE.FieldName = "REJECT_DATE";
            this.REJECT_DATE.Name = "REJECT_DATE";
            this.REJECT_DATE.Visible = true;
            this.REJECT_DATE.VisibleIndex = 6;
            this.REJECT_DATE.Width = 157;
            // 
            // TRP_CMP_TP
            // 
            this.TRP_CMP_TP.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.TRP_CMP_TP.AppearanceCell.Options.UseFont = true;
            this.TRP_CMP_TP.AppearanceCell.Options.UseTextOptions = true;
            this.TRP_CMP_TP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TRP_CMP_TP.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TRP_CMP_TP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.TRP_CMP_TP.AppearanceHeader.Options.UseFont = true;
            this.TRP_CMP_TP.AppearanceHeader.Options.UseTextOptions = true;
            this.TRP_CMP_TP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TRP_CMP_TP.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TRP_CMP_TP.BindingField = "TRP_CMP_TP";
            this.TRP_CMP_TP.Caption = "운송업체구분";
            this.TRP_CMP_TP.ColumnEdit = null;
            this.TRP_CMP_TP.FieldName = "TRP_CMP_TP";
            this.TRP_CMP_TP.Name = "TRP_CMP_TP";
            this.TRP_CMP_TP.Width = 134;
            // 
            // SysScha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 869);
            this.Controls.Add(this.grdScha);
            this.Controls.Add(this.panelControl1);
            this.Name = "SysScha";
            this.Text = "";
            this.Load += new System.EventHandler(this.SysScha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leSchaFlag.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.START_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.START_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCEO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSCHANM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdScha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwScha)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private Common.Control.OctoButton BtnNew;
        private Common.Control.OctoButton BtnSave;
        private Common.Control.OctoButton BtnSearch;
        private Common.Control.OctoButton BtnExcel;
        private Common.Control.OctoButton BtnClose;
        private JPlatform.Client.Controls.GridControlEx grdScha;
        private JPlatform.Client.Controls.GridViewEx gvwScha;
        private JPlatform.Client.Controls.GridColumnEx CUST;
        private JPlatform.Client.Controls.GridColumnEx CUST_SEQ;
        private JPlatform.Client.Controls.GridColumnEx SCHA_NAME;
        private JPlatform.Client.Controls.GridColumnEx SCHA_ADDR;
        private JPlatform.Client.Controls.GridColumnEx SCHA_DPJ;
        private JPlatform.Client.Controls.GridColumnEx SCHA_DATE;
        private JPlatform.Client.Controls.GridColumnEx TRP_CMP_TP;
        private JPlatform.Client.Controls.GridColumnEx REJECT_DATE;
        private DevExpress.XtraEditors.TextEdit txtCEO;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit START_DATE;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtAddr;
        private DevExpress.XtraEditors.TextEdit txtSCHANM;
        private DevExpress.XtraEditors.LookUpEdit leSchaFlag;
        private System.Windows.Forms.TextBox textBox1;
    }
}