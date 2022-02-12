namespace OCT_WEIGHT.Manager.Program
{
    partial class FrmMENU
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.octoButton1 = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.octoButton2 = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnSearch = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnClose = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnSave = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnNew = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lueUPPER = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.cbUse = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cbDIV = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtSORT = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cbDesk = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtNM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtFORM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCD = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.grd_menu = new JPlatform.Client.Controls.GridControlEx();
            this.gvw_menu = new JPlatform.Client.Controls.GridViewEx();
            this.colCODE = new JPlatform.Client.Controls.GridColumnEx();
            this.colCODE_NAME = new JPlatform.Client.Controls.GridColumnEx();
            this.colREMARK = new JPlatform.Client.Controls.GridColumnEx();
            this.colCODE_VALUE = new JPlatform.Client.Controls.GridColumnEx();
            this.colUSE_YN = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx1 = new JPlatform.Client.Controls.GridColumnEx();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueUPPER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbUse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbDIV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSORT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbDesk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFORM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_menu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvw_menu)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.octoButton1);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1157, 62);
            this.panelControl1.TabIndex = 6;
            // 
            // octoButton1
            // 
            this.octoButton1.AllowYn = "Y";
            this.octoButton1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.octoButton1.Appearance.Options.UseFont = true;
            this.octoButton1.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.octoButton1.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon05_on;
            this.octoButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.octoButton1.Location = new System.Drawing.Point(8, 5);
            this.octoButton1.Name = "octoButton1";
            this.octoButton1.Size = new System.Drawing.Size(90, 47);
            this.octoButton1.TabIndex = 10;
            this.octoButton1.Text = "삭제";
            this.octoButton1.Visible = false;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.octoButton2);
            this.panelControl4.Controls.Add(this.btnSearch);
            this.panelControl4.Controls.Add(this.btnClose);
            this.panelControl4.Controls.Add(this.btnSave);
            this.panelControl4.Controls.Add(this.btnNew);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl4.Location = new System.Drawing.Point(672, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(483, 58);
            this.panelControl4.TabIndex = 24;
            // 
            // octoButton2
            // 
            this.octoButton2.AllowYn = "Y";
            this.octoButton2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.octoButton2.Appearance.Options.UseFont = true;
            this.octoButton2.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.octoButton2.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon06_on;
            this.octoButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.octoButton2.Location = new System.Drawing.Point(294, 7);
            this.octoButton2.Name = "octoButton2";
            this.octoButton2.Size = new System.Drawing.Size(90, 47);
            this.octoButton2.TabIndex = 10;
            this.octoButton2.Text = "엑셀";
            this.octoButton2.Click += new System.EventHandler(this.octoButton2_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AllowYn = "Y";
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnSearch.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon01_on;
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(12, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 47);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowYn = "Y";
            this.btnClose.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnClose.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon09_on;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(387, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 47);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "닫기";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AllowYn = "Y";
            this.btnSave.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnSave.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon03_on;
            this.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(200, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 47);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.AllowYn = "Y";
            this.btnNew.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnNew.Appearance.Options.UseFont = true;
            this.btnNew.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnNew.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon02_on;
            this.btnNew.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(106, 7);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 47);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "초기화";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.lueUPPER);
            this.panelControl2.Controls.Add(this.labelControl8);
            this.panelControl2.Controls.Add(this.cbUse);
            this.panelControl2.Controls.Add(this.labelControl7);
            this.panelControl2.Controls.Add(this.cbDIV);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.txtSORT);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.cbDesk);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.txtNM);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.txtFORM);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.txtCD);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 62);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1157, 80);
            this.panelControl2.TabIndex = 7;
            // 
            // lueUPPER
            // 
            this.lueUPPER.EditValue = "<Null>";
            this.lueUPPER.Location = new System.Drawing.Point(1024, 4);
            this.lueUPPER.Name = "lueUPPER";
            this.lueUPPER.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.lueUPPER.Properties.Appearance.Options.UseFont = true;
            this.lueUPPER.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueUPPER.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MENU_ID", "상위코드"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MENU_NM", "상위메뉴명")});
            this.lueUPPER.Size = new System.Drawing.Size(122, 30);
            this.lueUPPER.TabIndex = 22;
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
            this.labelControl8.Location = new System.Drawing.Point(927, 5);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(93, 31);
            this.labelControl8.TabIndex = 30;
            this.labelControl8.Text = "상위메뉴";
            // 
            // cbUse
            // 
            this.cbUse.EditValue = "Y";
            this.cbUse.Location = new System.Drawing.Point(821, 44);
            this.cbUse.Name = "cbUse";
            this.cbUse.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.cbUse.Properties.Appearance.Options.UseFont = true;
            this.cbUse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbUse.Properties.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.cbUse.Size = new System.Drawing.Size(100, 30);
            this.cbUse.TabIndex = 26;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl7.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl7.Appearance.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl7.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl7.Location = new System.Drawing.Point(725, 43);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(93, 31);
            this.labelControl7.TabIndex = 29;
            this.labelControl7.Text = "사용여부";
            // 
            // cbDIV
            // 
            this.cbDIV.EditValue = "F";
            this.cbDIV.Location = new System.Drawing.Point(821, 6);
            this.cbDIV.Name = "cbDIV";
            this.cbDIV.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.cbDIV.Properties.Appearance.Options.UseFont = true;
            this.cbDIV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbDIV.Properties.Items.AddRange(new object[] {
            "F",
            "I"});
            this.cbDIV.Size = new System.Drawing.Size(100, 30);
            this.cbDIV.TabIndex = 20;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl6.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl6.Appearance.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl6.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl6.Location = new System.Drawing.Point(725, 6);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(93, 31);
            this.labelControl6.TabIndex = 28;
            this.labelControl6.Text = "메뉴구분";
            // 
            // txtSORT
            // 
            this.txtSORT.Location = new System.Drawing.Point(619, 44);
            this.txtSORT.Name = "txtSORT";
            this.txtSORT.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtSORT.Properties.Appearance.Options.UseFont = true;
            this.txtSORT.Size = new System.Drawing.Size(100, 30);
            this.txtSORT.TabIndex = 24;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl5.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl5.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl5.Location = new System.Drawing.Point(505, 43);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(111, 31);
            this.labelControl5.TabIndex = 27;
            this.labelControl5.Text = "정렬순번";
            // 
            // cbDesk
            // 
            this.cbDesk.EditValue = "DESK";
            this.cbDesk.Location = new System.Drawing.Point(619, 6);
            this.cbDesk.Name = "cbDesk";
            this.cbDesk.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.cbDesk.Properties.Appearance.Options.UseFont = true;
            this.cbDesk.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbDesk.Properties.Items.AddRange(new object[] {
            "DESK"});
            this.cbDesk.Size = new System.Drawing.Size(100, 30);
            this.cbDesk.TabIndex = 18;
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
            this.labelControl4.Location = new System.Drawing.Point(505, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(111, 31);
            this.labelControl4.TabIndex = 25;
            this.labelControl4.Text = "DESK/MOBILE";
            // 
            // txtNM
            // 
            this.txtNM.Location = new System.Drawing.Point(313, 7);
            this.txtNM.Name = "txtNM";
            this.txtNM.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtNM.Properties.Appearance.Options.UseFont = true;
            this.txtNM.Size = new System.Drawing.Size(186, 30);
            this.txtNM.TabIndex = 17;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl3.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl3.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl3.Location = new System.Drawing.Point(216, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(93, 31);
            this.labelControl3.TabIndex = 21;
            this.labelControl3.Text = "메뉴명";
            // 
            // txtFORM
            // 
            this.txtFORM.Location = new System.Drawing.Point(100, 44);
            this.txtFORM.Name = "txtFORM";
            this.txtFORM.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtFORM.Properties.Appearance.Options.UseFont = true;
            this.txtFORM.Size = new System.Drawing.Size(399, 30);
            this.txtFORM.TabIndex = 23;
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
            this.labelControl2.Location = new System.Drawing.Point(5, 43);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(93, 31);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "폼명";
            // 
            // txtCD
            // 
            this.txtCD.Location = new System.Drawing.Point(100, 7);
            this.txtCD.Name = "txtCD";
            this.txtCD.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtCD.Properties.Appearance.Options.UseFont = true;
            this.txtCD.Size = new System.Drawing.Size(100, 30);
            this.txtCD.TabIndex = 15;
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
            this.labelControl1.Location = new System.Drawing.Point(5, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(93, 31);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "메뉴코드";
            // 
            // grd_menu
            // 
            this.grd_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_menu.Location = new System.Drawing.Point(0, 142);
            this.grd_menu.MainView = this.gvw_menu;
            this.grd_menu.Name = "grd_menu";
            this.grd_menu.Size = new System.Drawing.Size(1157, 430);
            this.grd_menu.TabIndex = 14;
            this.grd_menu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvw_menu});
            this.grd_menu.Click += new System.EventHandler(this.grd_menu_Click);
            // 
            // gvw_menu
            // 
            this.gvw_menu.ActionMode = JPlatform.Client.Controls.ActionMode.View;
            this.gvw_menu.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvw_menu.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvw_menu.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvw_menu.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvw_menu.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvw_menu.Appearance.FocusedRow.Options.UseFont = true;
            this.gvw_menu.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvw_menu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gvw_menu.ColumnPanelRowHeight = 30;
            this.gvw_menu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCODE,
            this.colCODE_NAME,
            this.colREMARK,
            this.colCODE_VALUE,
            this.colUSE_YN,
            this.gridColumnEx1});
            this.gvw_menu.GridControl = this.grd_menu;
            this.gvw_menu.Name = "gvw_menu";
            this.gvw_menu.OptionsView.ColumnAutoWidth = false;
            this.gvw_menu.OptionsView.ShowGroupPanel = false;
            this.gvw_menu.PaintStyleName = "MixedXP";
            this.gvw_menu.RowHeight = 30;
            this.gvw_menu.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvw_menu_FocusedRowChanged);
            // 
            // colCODE
            // 
            this.colCODE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCODE.AppearanceCell.Options.UseFont = true;
            this.colCODE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCODE.AppearanceHeader.Options.UseFont = true;
            this.colCODE.AppearanceHeader.Options.UseTextOptions = true;
            this.colCODE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCODE.BindingField = "MENU_ID";
            this.colCODE.Caption = "코드";
            this.colCODE.ColumnEdit = null;
            this.colCODE.FieldName = "MENU_ID";
            this.colCODE.Name = "colCODE";
            this.colCODE.OptionsColumn.AllowEdit = false;
            this.colCODE.OptionsColumn.ReadOnly = true;
            this.colCODE.Width = 89;
            // 
            // colCODE_NAME
            // 
            this.colCODE_NAME.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCODE_NAME.AppearanceCell.Options.UseFont = true;
            this.colCODE_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCODE_NAME.AppearanceHeader.Options.UseFont = true;
            this.colCODE_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.colCODE_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCODE_NAME.BindingField = "MENU_NM";
            this.colCODE_NAME.Caption = "메뉴명";
            this.colCODE_NAME.ColumnEdit = null;
            this.colCODE_NAME.FieldName = "MENU_NM";
            this.colCODE_NAME.Name = "colCODE_NAME";
            this.colCODE_NAME.OptionsColumn.AllowEdit = false;
            this.colCODE_NAME.OptionsColumn.ReadOnly = true;
            this.colCODE_NAME.Visible = true;
            this.colCODE_NAME.VisibleIndex = 1;
            this.colCODE_NAME.Width = 205;
            // 
            // colREMARK
            // 
            this.colREMARK.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colREMARK.AppearanceCell.Options.UseFont = true;
            this.colREMARK.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colREMARK.AppearanceHeader.Options.UseFont = true;
            this.colREMARK.AppearanceHeader.Options.UseTextOptions = true;
            this.colREMARK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colREMARK.BindingField = "FORM_NM";
            this.colREMARK.Caption = "품명";
            this.colREMARK.ColumnEdit = null;
            this.colREMARK.FieldName = "FORM_NM";
            this.colREMARK.Name = "colREMARK";
            this.colREMARK.OptionsColumn.AllowEdit = false;
            this.colREMARK.OptionsColumn.ReadOnly = true;
            this.colREMARK.Visible = true;
            this.colREMARK.VisibleIndex = 3;
            this.colREMARK.Width = 415;
            // 
            // colCODE_VALUE
            // 
            this.colCODE_VALUE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCODE_VALUE.AppearanceCell.Options.UseFont = true;
            this.colCODE_VALUE.AppearanceCell.Options.UseTextOptions = true;
            this.colCODE_VALUE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCODE_VALUE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCODE_VALUE.AppearanceHeader.Options.UseFont = true;
            this.colCODE_VALUE.AppearanceHeader.Options.UseTextOptions = true;
            this.colCODE_VALUE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCODE_VALUE.BindingField = "UPPER_MENU_NM";
            this.colCODE_VALUE.Caption = "상위메뉴명";
            this.colCODE_VALUE.ColumnEdit = null;
            this.colCODE_VALUE.FieldName = "UPPER_MENU_NM";
            this.colCODE_VALUE.Name = "colCODE_VALUE";
            this.colCODE_VALUE.OptionsColumn.AllowEdit = false;
            this.colCODE_VALUE.OptionsColumn.ReadOnly = true;
            this.colCODE_VALUE.Visible = true;
            this.colCODE_VALUE.VisibleIndex = 0;
            this.colCODE_VALUE.Width = 104;
            // 
            // colUSE_YN
            // 
            this.colUSE_YN.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colUSE_YN.AppearanceCell.Options.UseFont = true;
            this.colUSE_YN.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colUSE_YN.AppearanceHeader.Options.UseFont = true;
            this.colUSE_YN.AppearanceHeader.Options.UseTextOptions = true;
            this.colUSE_YN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUSE_YN.BindingField = "SORT_NO";
            this.colUSE_YN.Caption = "SORT";
            this.colUSE_YN.ColumnEdit = null;
            this.colUSE_YN.FieldName = "SORT_NO";
            this.colUSE_YN.Name = "colUSE_YN";
            this.colUSE_YN.OptionsColumn.AllowEdit = false;
            this.colUSE_YN.OptionsColumn.ReadOnly = true;
            this.colUSE_YN.Visible = true;
            this.colUSE_YN.VisibleIndex = 2;
            this.colUSE_YN.Width = 100;
            // 
            // gridColumnEx1
            // 
            this.gridColumnEx1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx1.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEx1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx1.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx1.BindingField = "USE_YN";
            this.gridColumnEx1.Caption = "사용여부";
            this.gridColumnEx1.ColumnEdit = null;
            this.gridColumnEx1.FieldName = "USE_YN";
            this.gridColumnEx1.Name = "gridColumnEx1";
            this.gridColumnEx1.OptionsColumn.AllowEdit = false;
            this.gridColumnEx1.OptionsColumn.ReadOnly = true;
            this.gridColumnEx1.Visible = true;
            this.gridColumnEx1.VisibleIndex = 4;
            this.gridColumnEx1.Width = 84;
            // 
            // FrmMENU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(1157, 572);
            this.Controls.Add(this.grd_menu);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmMENU";
            this.Text = "메뉴등록";
            this.Load += new System.EventHandler(this.FrmMENU_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueUPPER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbUse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbDIV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSORT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbDesk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFORM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_menu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvw_menu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Common.Control.OctoButton octoButton1;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private Common.Control.OctoButton btnSearch;
        private Common.Control.OctoButton btnClose;
        private Common.Control.OctoButton btnSave;
        private Common.Control.OctoButton btnNew;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LookUpEdit lueUPPER;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.ComboBoxEdit cbUse;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ComboBoxEdit cbDIV;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtSORT;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit cbDesk;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtNM;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtFORM;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtCD;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private JPlatform.Client.Controls.GridControlEx grd_menu;
        private JPlatform.Client.Controls.GridViewEx gvw_menu;
        private JPlatform.Client.Controls.GridColumnEx colCODE;
        private JPlatform.Client.Controls.GridColumnEx colCODE_NAME;
        private JPlatform.Client.Controls.GridColumnEx colREMARK;
        private JPlatform.Client.Controls.GridColumnEx colCODE_VALUE;
        private JPlatform.Client.Controls.GridColumnEx colUSE_YN;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx1;
        private Common.Control.OctoButton octoButton2;

    }
}
