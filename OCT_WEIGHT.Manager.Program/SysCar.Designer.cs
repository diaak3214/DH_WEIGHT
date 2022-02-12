namespace OCT_WEIGHT.Manager.Program
{
    partial class SysCar
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
            this._OP_CODE = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.END_DATE = new DevExpress.XtraEditors.DateEdit();
            this.START_DATE = new DevExpress.XtraEditors.DateEdit();
            this.CUST_CD = new DevExpress.XtraEditors.LookUpEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtDISCRIPTION = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtVEHL_NO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.octoButton2 = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnSearch = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnDel = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnClose = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnSave = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnNew = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.grd_car = new JPlatform.Client.Controls.GridControlEx();
            this.gvw_car = new JPlatform.Client.Controls.GridViewEx();
            this.LIMIT_FLAG = new JPlatform.Client.Controls.GridColumnEx();
            this.colLIMIT = new JPlatform.Client.Controls.GridColumnEx();
            this.colSEQ = new JPlatform.Client.Controls.GridColumnEx();
            this.colVEHL_NO = new JPlatform.Client.Controls.GridColumnEx();
            this.colSTART_DATE = new JPlatform.Client.Controls.GridColumnEx();
            this.colEND_DATE = new JPlatform.Client.Controls.GridColumnEx();
            this._CUST_CD = new JPlatform.Client.Controls.GridColumnEx();
            this._CUST_NM = new JPlatform.Client.Controls.GridColumnEx();
            this.OP_CODE = new JPlatform.Client.Controls.GridColumnEx();
            this.OP_NM = new JPlatform.Client.Controls.GridColumnEx();
            this.colDISCRIPTION = new JPlatform.Client.Controls.GridColumnEx();
            this.colCRT_USER = new JPlatform.Client.Controls.GridColumnEx();
            this.colCRT_DTM = new JPlatform.Client.Controls.GridColumnEx();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._OP_CODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.END_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.END_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.START_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.START_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CUST_CD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDISCRIPTION.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVEHL_NO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_car)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvw_car)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this._OP_CODE);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.textEdit1);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.END_DATE);
            this.panelControl1.Controls.Add(this.START_DATE);
            this.panelControl1.Controls.Add(this.CUST_CD);
            this.panelControl1.Controls.Add(this.radioGroup1);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Controls.Add(this.txtDISCRIPTION);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtVEHL_NO);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1351, 124);
            this.panelControl1.TabIndex = 12;
            // 
            // _OP_CODE
            // 
            this._OP_CODE.EditValue = "";
            this._OP_CODE.Location = new System.Drawing.Point(499, 87);
            this._OP_CODE.Name = "_OP_CODE";
            this._OP_CODE.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this._OP_CODE.Properties.Appearance.Options.UseFont = true;
            this._OP_CODE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._OP_CODE.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE", "계량대코드"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE_NAME", "계량대명")});
            this._OP_CODE.Size = new System.Drawing.Size(172, 30);
            this._OP_CODE.TabIndex = 59;
            this._OP_CODE.Visible = false;
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
            this.labelControl3.Location = new System.Drawing.Point(401, 86);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(93, 31);
            this.labelControl3.TabIndex = 58;
            this.labelControl3.Text = "계량대목록";
            this.labelControl3.Visible = false;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(568, 43);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(100, 20);
            this.textEdit1.TabIndex = 57;
            this.textEdit1.Visible = false;
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
            this.labelControl8.Location = new System.Drawing.Point(12, 86);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(93, 31);
            this.labelControl8.TabIndex = 56;
            this.labelControl8.Text = "제한일자";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(232, 93);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(9, 14);
            this.labelControl7.TabIndex = 55;
            this.labelControl7.Text = "~";
            // 
            // END_DATE
            // 
            this.END_DATE.EditValue = null;
            this.END_DATE.Location = new System.Drawing.Point(247, 90);
            this.END_DATE.Name = "END_DATE";
            this.END_DATE.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.END_DATE.Properties.Appearance.Options.UseFont = true;
            this.END_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.END_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.END_DATE.Size = new System.Drawing.Size(113, 26);
            this.END_DATE.TabIndex = 54;
            // 
            // START_DATE
            // 
            this.START_DATE.EditValue = null;
            this.START_DATE.Location = new System.Drawing.Point(110, 90);
            this.START_DATE.Name = "START_DATE";
            this.START_DATE.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.START_DATE.Properties.Appearance.Options.UseFont = true;
            this.START_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.START_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.START_DATE.Size = new System.Drawing.Size(116, 26);
            this.START_DATE.TabIndex = 53;
            // 
            // CUST_CD
            // 
            this.CUST_CD.EditValue = "";
            this.CUST_CD.Location = new System.Drawing.Point(340, 50);
            this.CUST_CD.Name = "CUST_CD";
            this.CUST_CD.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.CUST_CD.Properties.Appearance.Options.UseFont = true;
            this.CUST_CD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CUST_CD.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CUST_CD", "거래처코드"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CUST_NM", "거래처명")});
            this.CUST_CD.Size = new System.Drawing.Size(172, 30);
            this.CUST_CD.TabIndex = 52;
            this.CUST_CD.Visible = false;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(109, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.radioGroup1.Properties.Appearance.Options.UseFont = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("00", "전체"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("01", "차량제한"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("02", "거래처제한")});
            this.radioGroup1.Size = new System.Drawing.Size(280, 31);
            this.radioGroup1.TabIndex = 42;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged_1);
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelControl10.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl10.Appearance.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl10.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl10.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl10.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl10.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl10.Location = new System.Drawing.Point(12, 12);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(93, 31);
            this.labelControl10.TabIndex = 51;
            this.labelControl10.Text = "제한유형";
            // 
            // txtDISCRIPTION
            // 
            this.txtDISCRIPTION.Location = new System.Drawing.Point(109, 124);
            this.txtDISCRIPTION.Name = "txtDISCRIPTION";
            this.txtDISCRIPTION.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtDISCRIPTION.Properties.Appearance.Options.UseFont = true;
            this.txtDISCRIPTION.Properties.MaxLength = 50;
            this.txtDISCRIPTION.Size = new System.Drawing.Size(562, 26);
            this.txtDISCRIPTION.TabIndex = 48;
            this.txtDISCRIPTION.Visible = false;
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
            this.labelControl6.Location = new System.Drawing.Point(12, 123);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(93, 31);
            this.labelControl6.TabIndex = 50;
            this.labelControl6.Text = "사유";
            this.labelControl6.Visible = false;
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
            this.labelControl2.Location = new System.Drawing.Point(242, 49);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(93, 31);
            this.labelControl2.TabIndex = 46;
            this.labelControl2.Text = "거래처목록";
            this.labelControl2.Visible = false;
            // 
            // txtVEHL_NO
            // 
            this.txtVEHL_NO.Location = new System.Drawing.Point(110, 50);
            this.txtVEHL_NO.Name = "txtVEHL_NO";
            this.txtVEHL_NO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtVEHL_NO.Properties.Appearance.Options.UseFont = true;
            this.txtVEHL_NO.Size = new System.Drawing.Size(116, 26);
            this.txtVEHL_NO.TabIndex = 44;
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
            this.labelControl1.Location = new System.Drawing.Point(12, 49);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(93, 31);
            this.labelControl1.TabIndex = 43;
            this.labelControl1.Text = "차량번호";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.octoButton2);
            this.panelControl4.Controls.Add(this.btnSearch);
            this.panelControl4.Controls.Add(this.btnDel);
            this.panelControl4.Controls.Add(this.btnClose);
            this.panelControl4.Controls.Add(this.btnSave);
            this.panelControl4.Controls.Add(this.btnNew);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl4.Location = new System.Drawing.Point(934, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(415, 120);
            this.panelControl4.TabIndex = 41;
            // 
            // octoButton2
            // 
            this.octoButton2.AllowYn = "Y";
            this.octoButton2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.octoButton2.Appearance.Options.UseFont = true;
            this.octoButton2.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.octoButton2.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon06_on;
            this.octoButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.octoButton2.Location = new System.Drawing.Point(202, 8);
            this.octoButton2.Name = "octoButton2";
            this.octoButton2.Size = new System.Drawing.Size(90, 55);
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
            this.btnSearch.Location = new System.Drawing.Point(12, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 55);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDel
            // 
            this.btnDel.AllowYn = "Y";
            this.btnDel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnDel.Appearance.Options.UseFont = true;
            this.btnDel.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnDel.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon05_on;
            this.btnDel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(291, 84);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(90, 55);
            this.btnDel.TabIndex = 10;
            this.btnDel.Text = "삭제";
            this.btnDel.Visible = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowYn = "Y";
            this.btnClose.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnClose.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon09_on;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(296, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 55);
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
            this.btnSave.Location = new System.Drawing.Point(197, 84);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 55);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "저장";
            this.btnSave.Visible = false;
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
            this.btnNew.Location = new System.Drawing.Point(106, 8);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 55);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "초기화";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // grd_car
            // 
            this.grd_car.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_car.Location = new System.Drawing.Point(0, 124);
            this.grd_car.MainView = this.gvw_car;
            this.grd_car.Name = "grd_car";
            this.grd_car.Size = new System.Drawing.Size(1351, 445);
            this.grd_car.TabIndex = 0;
            this.grd_car.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvw_car});
            this.grd_car.Click += new System.EventHandler(this.grd_car_Click);
            // 
            // gvw_car
            // 
            this.gvw_car.ActionMode = JPlatform.Client.Controls.ActionMode.View;
            this.gvw_car.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvw_car.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvw_car.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gvw_car.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvw_car.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvw_car.Appearance.FocusedRow.Options.UseFont = true;
            this.gvw_car.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvw_car.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gvw_car.ColumnPanelRowHeight = 30;
            this.gvw_car.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.LIMIT_FLAG,
            this.colLIMIT,
            this.colSEQ,
            this.colVEHL_NO,
            this.colSTART_DATE,
            this.colEND_DATE,
            this._CUST_CD,
            this._CUST_NM,
            this.OP_CODE,
            this.OP_NM,
            this.colDISCRIPTION,
            this.colCRT_USER,
            this.colCRT_DTM});
            this.gvw_car.GridControl = this.grd_car;
            this.gvw_car.IndicatorWidth = 35;
            this.gvw_car.Name = "gvw_car";
            this.gvw_car.OptionsView.ColumnAutoWidth = false;
            this.gvw_car.OptionsView.ShowGroupPanel = false;
            this.gvw_car.PaintStyleName = "MixedXP";
            this.gvw_car.RowHeight = 30;
            this.gvw_car.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvw_car_CustomDrawRowIndicator);
            this.gvw_car.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvw_car_FocusedRowChanged);
            // 
            // LIMIT_FLAG
            // 
            this.LIMIT_FLAG.BindingField = "LIMIT_FLAG";
            this.LIMIT_FLAG.Caption = "제한여부";
            this.LIMIT_FLAG.ColumnEdit = null;
            this.LIMIT_FLAG.FieldName = "LIMIT_FLAG";
            this.LIMIT_FLAG.Name = "LIMIT_FLAG";
            this.LIMIT_FLAG.OptionsColumn.AllowEdit = false;
            this.LIMIT_FLAG.OptionsColumn.ReadOnly = true;
            this.LIMIT_FLAG.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // colLIMIT
            // 
            this.colLIMIT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colLIMIT.AppearanceCell.Options.UseFont = true;
            this.colLIMIT.AppearanceCell.Options.UseTextOptions = true;
            this.colLIMIT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLIMIT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colLIMIT.AppearanceHeader.Options.UseFont = true;
            this.colLIMIT.AppearanceHeader.Options.UseTextOptions = true;
            this.colLIMIT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLIMIT.BindingField = "LIMIT";
            this.colLIMIT.Caption = "제한유형";
            this.colLIMIT.ColumnEdit = null;
            this.colLIMIT.FieldName = "LIMIT";
            this.colLIMIT.Name = "colLIMIT";
            this.colLIMIT.OptionsColumn.AllowEdit = false;
            this.colLIMIT.OptionsColumn.ReadOnly = true;
            this.colLIMIT.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colLIMIT.Visible = true;
            this.colLIMIT.VisibleIndex = 0;
            this.colLIMIT.Width = 95;
            // 
            // colSEQ
            // 
            this.colSEQ.BindingField = "SEQ";
            this.colSEQ.Caption = "순번";
            this.colSEQ.ColumnEdit = null;
            this.colSEQ.FieldName = "SEQ";
            this.colSEQ.Name = "colSEQ";
            this.colSEQ.OptionsColumn.AllowEdit = false;
            this.colSEQ.OptionsColumn.ReadOnly = true;
            this.colSEQ.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // colVEHL_NO
            // 
            this.colVEHL_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colVEHL_NO.AppearanceCell.Options.UseFont = true;
            this.colVEHL_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colVEHL_NO.AppearanceHeader.Options.UseFont = true;
            this.colVEHL_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.colVEHL_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVEHL_NO.BindingField = "VEHL_NO";
            this.colVEHL_NO.Caption = "차량번호";
            this.colVEHL_NO.ColumnEdit = null;
            this.colVEHL_NO.FieldName = "VEHL_NO";
            this.colVEHL_NO.Name = "colVEHL_NO";
            this.colVEHL_NO.OptionsColumn.AllowEdit = false;
            this.colVEHL_NO.OptionsColumn.ReadOnly = true;
            this.colVEHL_NO.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colVEHL_NO.Visible = true;
            this.colVEHL_NO.VisibleIndex = 1;
            this.colVEHL_NO.Width = 116;
            // 
            // colSTART_DATE
            // 
            this.colSTART_DATE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colSTART_DATE.AppearanceCell.Options.UseFont = true;
            this.colSTART_DATE.AppearanceCell.Options.UseTextOptions = true;
            this.colSTART_DATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTART_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colSTART_DATE.AppearanceHeader.Options.UseFont = true;
            this.colSTART_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.colSTART_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTART_DATE.BindingField = "START_DATE";
            this.colSTART_DATE.Caption = "시작일자";
            this.colSTART_DATE.ColumnEdit = null;
            this.colSTART_DATE.FieldName = "START_DATE";
            this.colSTART_DATE.Name = "colSTART_DATE";
            this.colSTART_DATE.OptionsColumn.AllowEdit = false;
            this.colSTART_DATE.OptionsColumn.ReadOnly = true;
            this.colSTART_DATE.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colSTART_DATE.Visible = true;
            this.colSTART_DATE.VisibleIndex = 2;
            this.colSTART_DATE.Width = 112;
            // 
            // colEND_DATE
            // 
            this.colEND_DATE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colEND_DATE.AppearanceCell.Options.UseFont = true;
            this.colEND_DATE.AppearanceCell.Options.UseTextOptions = true;
            this.colEND_DATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEND_DATE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colEND_DATE.AppearanceHeader.Options.UseFont = true;
            this.colEND_DATE.AppearanceHeader.Options.UseTextOptions = true;
            this.colEND_DATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEND_DATE.BindingField = "END_DATE";
            this.colEND_DATE.Caption = "종료일자";
            this.colEND_DATE.ColumnEdit = null;
            this.colEND_DATE.FieldName = "END_DATE";
            this.colEND_DATE.Name = "colEND_DATE";
            this.colEND_DATE.OptionsColumn.AllowEdit = false;
            this.colEND_DATE.OptionsColumn.ReadOnly = true;
            this.colEND_DATE.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colEND_DATE.Visible = true;
            this.colEND_DATE.VisibleIndex = 3;
            this.colEND_DATE.Width = 111;
            // 
            // _CUST_CD
            // 
            this._CUST_CD.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this._CUST_CD.AppearanceCell.Options.UseFont = true;
            this._CUST_CD.AppearanceCell.Options.UseTextOptions = true;
            this._CUST_CD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._CUST_CD.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this._CUST_CD.AppearanceHeader.Options.UseFont = true;
            this._CUST_CD.AppearanceHeader.Options.UseTextOptions = true;
            this._CUST_CD.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._CUST_CD.BindingField = "CUST_CD";
            this._CUST_CD.Caption = "거래처 코드";
            this._CUST_CD.ColumnEdit = null;
            this._CUST_CD.FieldName = "CUST_CD";
            this._CUST_CD.Name = "_CUST_CD";
            this._CUST_CD.OptionsColumn.AllowEdit = false;
            this._CUST_CD.OptionsColumn.ReadOnly = true;
            this._CUST_CD.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this._CUST_CD.Width = 100;
            // 
            // _CUST_NM
            // 
            this._CUST_NM.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this._CUST_NM.AppearanceCell.Options.UseFont = true;
            this._CUST_NM.AppearanceCell.Options.UseTextOptions = true;
            this._CUST_NM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._CUST_NM.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this._CUST_NM.AppearanceHeader.Options.UseFont = true;
            this._CUST_NM.AppearanceHeader.Options.UseTextOptions = true;
            this._CUST_NM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._CUST_NM.BindingField = "CUST_NM";
            this._CUST_NM.Caption = "거래처명";
            this._CUST_NM.ColumnEdit = null;
            this._CUST_NM.FieldName = "CUST_NM";
            this._CUST_NM.Name = "_CUST_NM";
            this._CUST_NM.OptionsColumn.AllowEdit = false;
            this._CUST_NM.OptionsColumn.ReadOnly = true;
            this._CUST_NM.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this._CUST_NM.Visible = true;
            this._CUST_NM.VisibleIndex = 4;
            this._CUST_NM.Width = 159;
            // 
            // OP_CODE
            // 
            this.OP_CODE.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.OP_CODE.AppearanceCell.Options.UseFont = true;
            this.OP_CODE.AppearanceCell.Options.UseTextOptions = true;
            this.OP_CODE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OP_CODE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.OP_CODE.AppearanceHeader.Options.UseFont = true;
            this.OP_CODE.AppearanceHeader.Options.UseTextOptions = true;
            this.OP_CODE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OP_CODE.BindingField = "OP_CODE";
            this.OP_CODE.Caption = "계량대코드";
            this.OP_CODE.ColumnEdit = null;
            this.OP_CODE.FieldName = "OP_CODE";
            this.OP_CODE.Name = "OP_CODE";
            this.OP_CODE.OptionsColumn.AllowEdit = false;
            this.OP_CODE.OptionsColumn.ReadOnly = true;
            this.OP_CODE.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.OP_CODE.Width = 101;
            // 
            // OP_NM
            // 
            this.OP_NM.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.OP_NM.AppearanceCell.Options.UseFont = true;
            this.OP_NM.AppearanceCell.Options.UseTextOptions = true;
            this.OP_NM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OP_NM.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OP_NM.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.OP_NM.AppearanceHeader.Options.UseFont = true;
            this.OP_NM.AppearanceHeader.Options.UseTextOptions = true;
            this.OP_NM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OP_NM.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OP_NM.BindingField = "OP_NM";
            this.OP_NM.Caption = "계량대명";
            this.OP_NM.ColumnEdit = null;
            this.OP_NM.FieldName = "OP_NM";
            this.OP_NM.Name = "OP_NM";
            this.OP_NM.OptionsColumn.AllowEdit = false;
            this.OP_NM.OptionsColumn.ReadOnly = true;
            this.OP_NM.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.OP_NM.Visible = true;
            this.OP_NM.VisibleIndex = 5;
            this.OP_NM.Width = 202;
            // 
            // colDISCRIPTION
            // 
            this.colDISCRIPTION.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colDISCRIPTION.AppearanceCell.Options.UseFont = true;
            this.colDISCRIPTION.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colDISCRIPTION.AppearanceHeader.Options.UseFont = true;
            this.colDISCRIPTION.AppearanceHeader.Options.UseTextOptions = true;
            this.colDISCRIPTION.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDISCRIPTION.BindingField = "DISCRIPTION";
            this.colDISCRIPTION.Caption = "사유";
            this.colDISCRIPTION.ColumnEdit = null;
            this.colDISCRIPTION.FieldName = "DISCRIPTION";
            this.colDISCRIPTION.Name = "colDISCRIPTION";
            this.colDISCRIPTION.OptionsColumn.AllowEdit = false;
            this.colDISCRIPTION.OptionsColumn.ReadOnly = true;
            this.colDISCRIPTION.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colDISCRIPTION.Visible = true;
            this.colDISCRIPTION.VisibleIndex = 6;
            this.colDISCRIPTION.Width = 490;
            // 
            // colCRT_USER
            // 
            this.colCRT_USER.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCRT_USER.AppearanceCell.Options.UseFont = true;
            this.colCRT_USER.AppearanceCell.Options.UseTextOptions = true;
            this.colCRT_USER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCRT_USER.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCRT_USER.AppearanceHeader.Options.UseFont = true;
            this.colCRT_USER.AppearanceHeader.Options.UseTextOptions = true;
            this.colCRT_USER.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCRT_USER.BindingField = "CRT_USER";
            this.colCRT_USER.Caption = "등록자";
            this.colCRT_USER.ColumnEdit = null;
            this.colCRT_USER.FieldName = "CRT_USER";
            this.colCRT_USER.Name = "colCRT_USER";
            this.colCRT_USER.OptionsColumn.AllowEdit = false;
            this.colCRT_USER.OptionsColumn.ReadOnly = true;
            this.colCRT_USER.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colCRT_USER.Visible = true;
            this.colCRT_USER.VisibleIndex = 7;
            this.colCRT_USER.Width = 87;
            // 
            // colCRT_DTM
            // 
            this.colCRT_DTM.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCRT_DTM.AppearanceCell.Options.UseFont = true;
            this.colCRT_DTM.AppearanceCell.Options.UseTextOptions = true;
            this.colCRT_DTM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCRT_DTM.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCRT_DTM.AppearanceHeader.Options.UseFont = true;
            this.colCRT_DTM.AppearanceHeader.Options.UseTextOptions = true;
            this.colCRT_DTM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCRT_DTM.BindingField = "CRT_DTM";
            this.colCRT_DTM.Caption = "등록일";
            this.colCRT_DTM.ColumnEdit = null;
            this.colCRT_DTM.FieldName = "CRT_DTM";
            this.colCRT_DTM.Name = "colCRT_DTM";
            this.colCRT_DTM.OptionsColumn.AllowEdit = false;
            this.colCRT_DTM.OptionsColumn.ReadOnly = true;
            this.colCRT_DTM.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colCRT_DTM.Visible = true;
            this.colCRT_DTM.VisibleIndex = 8;
            this.colCRT_DTM.Width = 105;
            // 
            // SysCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(1351, 569);
            this.Controls.Add(this.grd_car);
            this.Controls.Add(this.panelControl1);
            this.Name = "SysCar";
            this.Text = "입출차제한관리";
            this.Load += new System.EventHandler(this.SysCar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._OP_CODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.END_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.END_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.START_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.START_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CUST_CD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDISCRIPTION.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVEHL_NO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_car)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvw_car)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private Common.Control.OctoButton octoButton2;
        private Common.Control.OctoButton btnSearch;
        private Common.Control.OctoButton btnDel;
        private Common.Control.OctoButton btnClose;
        private Common.Control.OctoButton btnSave;
        private Common.Control.OctoButton btnNew;
        private JPlatform.Client.Controls.GridControlEx grd_car;
        private JPlatform.Client.Controls.GridViewEx gvw_car;
        private JPlatform.Client.Controls.GridColumnEx colVEHL_NO;
        private JPlatform.Client.Controls.GridColumnEx colSTART_DATE;
        private JPlatform.Client.Controls.GridColumnEx colEND_DATE;
        private JPlatform.Client.Controls.GridColumnEx colDISCRIPTION;
        private JPlatform.Client.Controls.GridColumnEx colCRT_USER;
        private JPlatform.Client.Controls.GridColumnEx colCRT_DTM;
        private JPlatform.Client.Controls.GridColumnEx colSEQ;
        private JPlatform.Client.Controls.GridColumnEx colLIMIT;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtDISCRIPTION;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtVEHL_NO;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit CUST_CD;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.DateEdit END_DATE;
        private DevExpress.XtraEditors.DateEdit START_DATE;
        private JPlatform.Client.Controls.GridColumnEx _CUST_CD;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private JPlatform.Client.Controls.GridColumnEx _CUST_NM;
        private JPlatform.Client.Controls.GridColumnEx OP_CODE;
        private DevExpress.XtraEditors.LookUpEdit _OP_CODE;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private JPlatform.Client.Controls.GridColumnEx OP_NM;
        private JPlatform.Client.Controls.GridColumnEx LIMIT_FLAG;
    }
}
