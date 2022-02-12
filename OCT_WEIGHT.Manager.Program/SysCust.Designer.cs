namespace OCT_WEIGHT.Manager.Program
{
    partial class SysCust
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
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtNAME = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.octoButton2 = new OCT_WEIGHT.Manager.Common.Control.OctoButton(this.components);
            this.btnSearch = new OCT_WEIGHT.Manager.Common.Control.OctoButton(this.components);
            this.btnDel = new OCT_WEIGHT.Manager.Common.Control.OctoButton(this.components);
            this.btnClose = new OCT_WEIGHT.Manager.Common.Control.OctoButton(this.components);
            this.btnSave = new OCT_WEIGHT.Manager.Common.Control.OctoButton(this.components);
            this.btnNew = new OCT_WEIGHT.Manager.Common.Control.OctoButton(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtCUST_SAUP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtREMARK = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtCUST_CEO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCUST_NM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCUST_CD = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.grd_cust = new JPlatform.Client.Controls.GridControlEx();
            this.gvw_cust = new JPlatform.Client.Controls.GridViewEx();
            this.colCUST_NM = new JPlatform.Client.Controls.GridColumnEx();
            this.colREMARK = new JPlatform.Client.Controls.GridColumnEx();
            this.colCUST_CD = new JPlatform.Client.Controls.GridColumnEx();
            this.colCUST_FG_NAME = new JPlatform.Client.Controls.GridColumnEx();
            this.colCUST_CEO = new JPlatform.Client.Controls.GridColumnEx();
            this.colCUST_SAUP = new JPlatform.Client.Controls.GridColumnEx();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCUST_SAUP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtREMARK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCUST_CEO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCUST_NM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCUST_CD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_cust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvw_cust)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtNAME);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1313, 62);
            this.panelControl1.TabIndex = 7;
            // 
            // txtNAME
            // 
            this.txtNAME.Location = new System.Drawing.Point(107, 15);
            this.txtNAME.Name = "txtNAME";
            this.txtNAME.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtNAME.Properties.Appearance.Options.UseFont = true;
            this.txtNAME.Size = new System.Drawing.Size(158, 30);
            this.txtNAME.TabIndex = 34;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.BackColor = System.Drawing.Color.LightCoral;
            this.labelControl9.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl9.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl9.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl9.Location = new System.Drawing.Point(12, 15);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(93, 31);
            this.labelControl9.TabIndex = 33;
            this.labelControl9.Text = "거래처명";
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
            this.panelControl4.Location = new System.Drawing.Point(726, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(585, 58);
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
            this.octoButton2.Location = new System.Drawing.Point(388, 7);
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
            // btnDel
            // 
            this.btnDel.AllowYn = "Y";
            this.btnDel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnDel.Appearance.Options.UseFont = true;
            this.btnDel.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnDel.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon05_on;
            this.btnDel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(294, 7);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(90, 47);
            this.btnDel.TabIndex = 10;
            this.btnDel.Text = "삭제";
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
            this.btnClose.Location = new System.Drawing.Point(482, 7);
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
            this.btnSave.TabIndex = 0;
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
            this.panelControl2.Controls.Add(this.txtCUST_SAUP);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.txtREMARK);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.txtCUST_CEO);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.txtCUST_NM);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.txtCUST_CD);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 62);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1313, 85);
            this.panelControl2.TabIndex = 9;
            // 
            // txtCUST_SAUP
            // 
            this.txtCUST_SAUP.Location = new System.Drawing.Point(107, 44);
            this.txtCUST_SAUP.Name = "txtCUST_SAUP";
            this.txtCUST_SAUP.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtCUST_SAUP.Properties.Appearance.Options.UseFont = true;
            this.txtCUST_SAUP.Size = new System.Drawing.Size(158, 30);
            this.txtCUST_SAUP.TabIndex = 3;
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
            this.labelControl6.Location = new System.Drawing.Point(12, 43);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(93, 31);
            this.labelControl6.TabIndex = 34;
            this.labelControl6.Text = "사업자번호";
            // 
            // txtREMARK
            // 
            this.txtREMARK.Location = new System.Drawing.Point(366, 45);
            this.txtREMARK.Name = "txtREMARK";
            this.txtREMARK.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtREMARK.Properties.Appearance.Options.UseFont = true;
            this.txtREMARK.Size = new System.Drawing.Size(417, 30);
            this.txtREMARK.TabIndex = 4;
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
            this.labelControl4.Location = new System.Drawing.Point(271, 43);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(93, 31);
            this.labelControl4.TabIndex = 30;
            this.labelControl4.Text = "비고";
            // 
            // txtCUST_CEO
            // 
            this.txtCUST_CEO.Location = new System.Drawing.Point(625, 7);
            this.txtCUST_CEO.Name = "txtCUST_CEO";
            this.txtCUST_CEO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtCUST_CEO.Properties.Appearance.Options.UseFont = true;
            this.txtCUST_CEO.Size = new System.Drawing.Size(158, 30);
            this.txtCUST_CEO.TabIndex = 2;
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
            this.labelControl3.Location = new System.Drawing.Point(530, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(93, 31);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "대표자";
            // 
            // txtCUST_NM
            // 
            this.txtCUST_NM.Location = new System.Drawing.Point(366, 7);
            this.txtCUST_NM.Name = "txtCUST_NM";
            this.txtCUST_NM.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtCUST_NM.Properties.Appearance.Options.UseFont = true;
            this.txtCUST_NM.Size = new System.Drawing.Size(158, 30);
            this.txtCUST_NM.TabIndex = 1;
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
            this.labelControl1.Location = new System.Drawing.Point(271, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(93, 31);
            this.labelControl1.TabIndex = 26;
            this.labelControl1.Text = "거래처명";
            // 
            // txtCUST_CD
            // 
            this.txtCUST_CD.Location = new System.Drawing.Point(107, 7);
            this.txtCUST_CD.Name = "txtCUST_CD";
            this.txtCUST_CD.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtCUST_CD.Properties.Appearance.Options.UseFont = true;
            this.txtCUST_CD.Size = new System.Drawing.Size(158, 30);
            this.txtCUST_CD.TabIndex = 0;
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
            this.labelControl2.Location = new System.Drawing.Point(12, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(93, 31);
            this.labelControl2.TabIndex = 24;
            this.labelControl2.Text = "거래처코드";
            // 
            // grd_cust
            // 
            this.grd_cust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_cust.Location = new System.Drawing.Point(0, 147);
            this.grd_cust.MainView = this.gvw_cust;
            this.grd_cust.Name = "grd_cust";
            this.grd_cust.Size = new System.Drawing.Size(1313, 448);
            this.grd_cust.TabIndex = 16;
            this.grd_cust.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvw_cust});
            this.grd_cust.Click += new System.EventHandler(this.grd_cust_Click);
            // 
            // gvw_cust
            // 
            this.gvw_cust.ActionMode = JPlatform.Client.Controls.ActionMode.View;
            this.gvw_cust.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvw_cust.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvw_cust.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gvw_cust.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvw_cust.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvw_cust.Appearance.FocusedRow.Options.UseFont = true;
            this.gvw_cust.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvw_cust.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gvw_cust.ColumnPanelRowHeight = 30;
            this.gvw_cust.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCUST_NM,
            this.colREMARK,
            this.colCUST_CD,
            this.colCUST_FG_NAME,
            this.colCUST_CEO,
            this.colCUST_SAUP});
            this.gvw_cust.GridControl = this.grd_cust;
            this.gvw_cust.IndicatorWidth = 35;
            this.gvw_cust.Name = "gvw_cust";
            this.gvw_cust.OptionsView.ColumnAutoWidth = false;
            this.gvw_cust.OptionsView.ShowGroupPanel = false;
            this.gvw_cust.PaintStyleName = "MixedXP";
            this.gvw_cust.RowHeight = 30;
            this.gvw_cust.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvw_cust_CustomDrawRowIndicator);
            this.gvw_cust.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvw_cust_FocusedRowChanged);
            // 
            // colCUST_NM
            // 
            this.colCUST_NM.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCUST_NM.AppearanceCell.Options.UseFont = true;
            this.colCUST_NM.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCUST_NM.AppearanceHeader.Options.UseFont = true;
            this.colCUST_NM.AppearanceHeader.Options.UseTextOptions = true;
            this.colCUST_NM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCUST_NM.BindingField = "CUST_NM";
            this.colCUST_NM.Caption = "거래처명";
            this.colCUST_NM.ColumnEdit = null;
            this.colCUST_NM.FieldName = "CUST_NM";
            this.colCUST_NM.Name = "colCUST_NM";
            this.colCUST_NM.OptionsColumn.AllowEdit = false;
            this.colCUST_NM.OptionsColumn.ReadOnly = true;
            this.colCUST_NM.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colCUST_NM.Visible = true;
            this.colCUST_NM.VisibleIndex = 1;
            this.colCUST_NM.Width = 250;
            // 
            // colREMARK
            // 
            this.colREMARK.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colREMARK.AppearanceCell.Options.UseFont = true;
            this.colREMARK.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colREMARK.AppearanceHeader.Options.UseFont = true;
            this.colREMARK.AppearanceHeader.Options.UseTextOptions = true;
            this.colREMARK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colREMARK.BindingField = "REMARK";
            this.colREMARK.Caption = "비고";
            this.colREMARK.ColumnEdit = null;
            this.colREMARK.FieldName = "REMARK";
            this.colREMARK.Name = "colREMARK";
            this.colREMARK.OptionsColumn.AllowEdit = false;
            this.colREMARK.OptionsColumn.ReadOnly = true;
            this.colREMARK.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colREMARK.Visible = true;
            this.colREMARK.VisibleIndex = 4;
            this.colREMARK.Width = 415;
            // 
            // colCUST_CD
            // 
            this.colCUST_CD.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCUST_CD.AppearanceCell.Options.UseFont = true;
            this.colCUST_CD.AppearanceCell.Options.UseTextOptions = true;
            this.colCUST_CD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCUST_CD.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCUST_CD.AppearanceHeader.Options.UseFont = true;
            this.colCUST_CD.AppearanceHeader.Options.UseTextOptions = true;
            this.colCUST_CD.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCUST_CD.BindingField = "CUST_CD";
            this.colCUST_CD.Caption = "거래처코드";
            this.colCUST_CD.ColumnEdit = null;
            this.colCUST_CD.FieldName = "CUST_CD";
            this.colCUST_CD.Name = "colCUST_CD";
            this.colCUST_CD.OptionsColumn.AllowEdit = false;
            this.colCUST_CD.OptionsColumn.ReadOnly = true;
            this.colCUST_CD.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colCUST_CD.Visible = true;
            this.colCUST_CD.VisibleIndex = 0;
            this.colCUST_CD.Width = 104;
            // 
            // colCUST_FG_NAME
            // 
            this.colCUST_FG_NAME.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCUST_FG_NAME.AppearanceCell.Options.UseFont = true;
            this.colCUST_FG_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCUST_FG_NAME.AppearanceHeader.Options.UseFont = true;
            this.colCUST_FG_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.colCUST_FG_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCUST_FG_NAME.BindingField = "CUST_FG_NAME";
            this.colCUST_FG_NAME.Caption = "거래처구분";
            this.colCUST_FG_NAME.ColumnEdit = null;
            this.colCUST_FG_NAME.FieldName = "CUST_FG_NAME";
            this.colCUST_FG_NAME.Name = "colCUST_FG_NAME";
            this.colCUST_FG_NAME.OptionsColumn.AllowEdit = false;
            this.colCUST_FG_NAME.OptionsColumn.ReadOnly = true;
            this.colCUST_FG_NAME.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colCUST_FG_NAME.Width = 113;
            // 
            // colCUST_CEO
            // 
            this.colCUST_CEO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCUST_CEO.AppearanceCell.Options.UseFont = true;
            this.colCUST_CEO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCUST_CEO.AppearanceHeader.Options.UseFont = true;
            this.colCUST_CEO.AppearanceHeader.Options.UseTextOptions = true;
            this.colCUST_CEO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCUST_CEO.BindingField = "CUST_CEO";
            this.colCUST_CEO.Caption = "대표자명";
            this.colCUST_CEO.ColumnEdit = null;
            this.colCUST_CEO.FieldName = "CUST_CEO";
            this.colCUST_CEO.Name = "colCUST_CEO";
            this.colCUST_CEO.OptionsColumn.AllowEdit = false;
            this.colCUST_CEO.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colCUST_CEO.Visible = true;
            this.colCUST_CEO.VisibleIndex = 2;
            this.colCUST_CEO.Width = 120;
            // 
            // colCUST_SAUP
            // 
            this.colCUST_SAUP.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCUST_SAUP.AppearanceCell.Options.UseFont = true;
            this.colCUST_SAUP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colCUST_SAUP.AppearanceHeader.Options.UseFont = true;
            this.colCUST_SAUP.AppearanceHeader.Options.UseTextOptions = true;
            this.colCUST_SAUP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCUST_SAUP.BindingField = "CUST_SAUP";
            this.colCUST_SAUP.Caption = "사업자번호";
            this.colCUST_SAUP.ColumnEdit = null;
            this.colCUST_SAUP.FieldName = "CUST_SAUP";
            this.colCUST_SAUP.Name = "colCUST_SAUP";
            this.colCUST_SAUP.OptionsColumn.AllowEdit = false;
            this.colCUST_SAUP.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colCUST_SAUP.Visible = true;
            this.colCUST_SAUP.VisibleIndex = 3;
            this.colCUST_SAUP.Width = 180;
            // 
            // SysCust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(1313, 595);
            this.Controls.Add(this.grd_cust);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "SysCust";
            this.Text = "거래처관리";
            this.Load += new System.EventHandler(this.SysCust_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCUST_SAUP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtREMARK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCUST_CEO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCUST_NM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCUST_CD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_cust)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvw_cust)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtNAME;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private Common.Control.OctoButton btnDel;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private Common.Control.OctoButton octoButton2;
        private Common.Control.OctoButton btnSearch;
        private Common.Control.OctoButton btnClose;
        private Common.Control.OctoButton btnSave;
        private Common.Control.OctoButton btnNew;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.TextEdit txtCUST_SAUP;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtREMARK;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtCUST_CEO;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtCUST_NM;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCUST_CD;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private JPlatform.Client.Controls.GridControlEx grd_cust;
        private JPlatform.Client.Controls.GridViewEx gvw_cust;
        private JPlatform.Client.Controls.GridColumnEx colCUST_NM;
        private JPlatform.Client.Controls.GridColumnEx colREMARK;
        private JPlatform.Client.Controls.GridColumnEx colCUST_CD;
        private JPlatform.Client.Controls.GridColumnEx colCUST_FG_NAME;
        private JPlatform.Client.Controls.GridColumnEx colCUST_CEO;
        private JPlatform.Client.Controls.GridColumnEx colCUST_SAUP;

    }
}
