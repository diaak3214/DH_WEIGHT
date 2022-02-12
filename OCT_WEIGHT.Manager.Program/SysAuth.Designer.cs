namespace OCT_WEIGHT.Manager.Program
{
    partial class SysAuth
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
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnClose = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnSave = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnNew = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grCom_M = new JPlatform.Client.Controls.GridControlEx();
            this.gvCom_M = new JPlatform.Client.Controls.GridViewEx();
            this.TYPE_CD = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx1 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx2 = new JPlatform.Client.Controls.GridColumnEx();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.txtNAME = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlEx1 = new JPlatform.Client.Controls.GridControlEx();
            this.gridViewEx1 = new JPlatform.Client.Controls.GridViewEx();
            this.gridColumnEx3 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx4 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx5 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx6 = new JPlatform.Client.Controls.GridColumnEx();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.UPPER_MENU_NM = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grCom_M)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCom_M)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEx1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEx1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1365, 62);
            this.panelControl1.TabIndex = 7;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.btnSearch);
            this.panelControl4.Controls.Add(this.btnClose);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl4.Location = new System.Drawing.Point(1166, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(197, 58);
            this.panelControl4.TabIndex = 24;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowYn = "Y";
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnSearch.Image = global::OCT_WEIGHT.Manager.Program.Properties.Resources.icon01_on;
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(4, 6);
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
            this.btnClose.Location = new System.Drawing.Point(100, 6);
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
            this.btnSave.Location = new System.Drawing.Point(98, 5);
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
            this.btnNew.Location = new System.Drawing.Point(3, 5);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 47);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "신규";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.grCom_M);
            this.groupControl1.Controls.Add(this.panelControl5);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 62);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(471, 628);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "권한그룹 입력";
            // 
            // grCom_M
            // 
            this.grCom_M.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grCom_M.Location = new System.Drawing.Point(2, 166);
            this.grCom_M.MainView = this.gvCom_M;
            this.grCom_M.Name = "grCom_M";
            this.grCom_M.Size = new System.Drawing.Size(467, 460);
            this.grCom_M.TabIndex = 13;
            this.grCom_M.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCom_M});
            // 
            // gvCom_M
            // 
            this.gvCom_M.ActionMode = JPlatform.Client.Controls.ActionMode.View;
            this.gvCom_M.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvCom_M.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvCom_M.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvCom_M.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvCom_M.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvCom_M.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCom_M.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvCom_M.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gvCom_M.ColumnPanelRowHeight = 30;
            this.gvCom_M.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.TYPE_CD,
            this.gridColumnEx1,
            this.gridColumnEx2});
            this.gvCom_M.GridControl = this.grCom_M;
            this.gvCom_M.Name = "gvCom_M";
            this.gvCom_M.OptionsView.ColumnAutoWidth = false;
            this.gvCom_M.OptionsView.ShowGroupPanel = false;
            this.gvCom_M.PaintStyleName = "MixedXP";
            this.gvCom_M.RowHeight = 30;
            this.gvCom_M.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvCom_M_FocusedRowChanged);
            // 
            // TYPE_CD
            // 
            this.TYPE_CD.BindingField = "TYPE_CD";
            this.TYPE_CD.Caption = "TYPE_CD";
            this.TYPE_CD.ColumnEdit = null;
            this.TYPE_CD.FieldName = "TYPE_CD";
            this.TYPE_CD.Name = "TYPE_CD";
            // 
            // gridColumnEx1
            // 
            this.gridColumnEx1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx1.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx1.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx1.BindingField = "CODE";
            this.gridColumnEx1.Caption = "그룹코드";
            this.gridColumnEx1.ColumnEdit = null;
            this.gridColumnEx1.FieldName = "CODE";
            this.gridColumnEx1.Name = "gridColumnEx1";
            this.gridColumnEx1.OptionsColumn.AllowEdit = false;
            this.gridColumnEx1.OptionsColumn.ReadOnly = true;
            this.gridColumnEx1.Visible = true;
            this.gridColumnEx1.VisibleIndex = 0;
            this.gridColumnEx1.Width = 119;
            // 
            // gridColumnEx2
            // 
            this.gridColumnEx2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx2.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx2.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx2.BindingField = "CODE_NAME";
            this.gridColumnEx2.Caption = "코드명";
            this.gridColumnEx2.ColumnEdit = null;
            this.gridColumnEx2.FieldName = "CODE_NAME";
            this.gridColumnEx2.Name = "gridColumnEx2";
            this.gridColumnEx2.OptionsColumn.AllowEdit = false;
            this.gridColumnEx2.OptionsColumn.ReadOnly = true;
            this.gridColumnEx2.Visible = true;
            this.gridColumnEx2.VisibleIndex = 1;
            this.gridColumnEx2.Width = 273;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.textEdit1);
            this.panelControl5.Controls.Add(this.txtNAME);
            this.panelControl5.Controls.Add(this.labelControl1);
            this.panelControl5.Controls.Add(this.labelControl2);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl5.Location = new System.Drawing.Point(2, 88);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(467, 78);
            this.panelControl5.TabIndex = 14;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(106, 42);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.MaxLength = 12;
            this.textEdit1.Size = new System.Drawing.Size(355, 30);
            this.textEdit1.TabIndex = 30;
            // 
            // txtNAME
            // 
            this.txtNAME.Location = new System.Drawing.Point(106, 6);
            this.txtNAME.Name = "txtNAME";
            this.txtNAME.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtNAME.Properties.Appearance.Options.UseFont = true;
            this.txtNAME.Properties.MaxLength = 3;
            this.txtNAME.Size = new System.Drawing.Size(175, 30);
            this.txtNAME.TabIndex = 29;
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
            this.labelControl1.Location = new System.Drawing.Point(8, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(93, 31);
            this.labelControl1.TabIndex = 26;
            this.labelControl1.Text = "그룹명";
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
            this.labelControl2.Location = new System.Drawing.Point(8, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(93, 31);
            this.labelControl2.TabIndex = 25;
            this.labelControl2.Text = "그룹코드";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 27);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(467, 61);
            this.panelControl2.TabIndex = 10;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.btnNew);
            this.panelControl3.Controls.Add(this.btnSave);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl3.Location = new System.Drawing.Point(272, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(193, 57);
            this.panelControl3.TabIndex = 26;
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.groupControl2.Appearance.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.gridControlEx1);
            this.groupControl2.Controls.Add(this.panelControl6);
            this.groupControl2.Controls.Add(this.splitterControl1);
            this.groupControl2.Controls.Add(this.treeList2);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(471, 62);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(894, 628);
            this.groupControl2.TabIndex = 9;
            this.groupControl2.Text = "권한그룹 입력";
            // 
            // gridControlEx1
            // 
            this.gridControlEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlEx1.Location = new System.Drawing.Point(481, 27);
            this.gridControlEx1.MainView = this.gridViewEx1;
            this.gridControlEx1.Name = "gridControlEx1";
            this.gridControlEx1.Size = new System.Drawing.Size(411, 599);
            this.gridControlEx1.TabIndex = 17;
            this.gridControlEx1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewEx1});
            // 
            // gridViewEx1
            // 
            this.gridViewEx1.ActionMode = JPlatform.Client.Controls.ActionMode.View;
            this.gridViewEx1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridViewEx1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridViewEx1.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gridViewEx1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewEx1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewEx1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewEx1.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridViewEx1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gridViewEx1.ColumnPanelRowHeight = 30;
            this.gridViewEx1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnEx3,
            this.gridColumnEx4,
            this.gridColumnEx5,
            this.gridColumnEx6});
            this.gridViewEx1.GridControl = this.gridControlEx1;
            this.gridViewEx1.Name = "gridViewEx1";
            this.gridViewEx1.OptionsView.ColumnAutoWidth = false;
            this.gridViewEx1.OptionsView.ShowGroupPanel = false;
            this.gridViewEx1.PaintStyleName = "MixedXP";
            this.gridViewEx1.RowHeight = 30;
            // 
            // gridColumnEx3
            // 
            this.gridColumnEx3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx3.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx3.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx3.BindingField = "UPPER_MENU_NM";
            this.gridColumnEx3.Caption = "상위그룹";
            this.gridColumnEx3.ColumnEdit = null;
            this.gridColumnEx3.FieldName = "UPPER_MENU_NM";
            this.gridColumnEx3.Name = "gridColumnEx3";
            this.gridColumnEx3.OptionsColumn.AllowEdit = false;
            this.gridColumnEx3.OptionsColumn.ReadOnly = true;
            this.gridColumnEx3.Visible = true;
            this.gridColumnEx3.VisibleIndex = 0;
            this.gridColumnEx3.Width = 119;
            // 
            // gridColumnEx4
            // 
            this.gridColumnEx4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx4.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx4.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx4.BindingField = "MENU_NM";
            this.gridColumnEx4.Caption = "메뉴명";
            this.gridColumnEx4.ColumnEdit = null;
            this.gridColumnEx4.FieldName = "MENU_NM";
            this.gridColumnEx4.Name = "gridColumnEx4";
            this.gridColumnEx4.OptionsColumn.AllowEdit = false;
            this.gridColumnEx4.OptionsColumn.ReadOnly = true;
            this.gridColumnEx4.Visible = true;
            this.gridColumnEx4.VisibleIndex = 1;
            this.gridColumnEx4.Width = 273;
            // 
            // gridColumnEx5
            // 
            this.gridColumnEx5.BindingField = "MENU_ID";
            this.gridColumnEx5.Caption = "MENU_ID";
            this.gridColumnEx5.ColumnEdit = null;
            this.gridColumnEx5.FieldName = "MENU_ID";
            this.gridColumnEx5.Name = "gridColumnEx5";
            // 
            // gridColumnEx6
            // 
            this.gridColumnEx6.BindingField = "AUTH_CD";
            this.gridColumnEx6.Caption = "AUTH_CD";
            this.gridColumnEx6.ColumnEdit = null;
            this.gridColumnEx6.FieldName = "AUTH_CD";
            this.gridColumnEx6.Name = "gridColumnEx6";
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.simpleButton2);
            this.panelControl6.Controls.Add(this.simpleButton1);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl6.Location = new System.Drawing.Point(407, 27);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(74, 599);
            this.panelControl6.TabIndex = 16;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(4, 66);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(66, 70);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "▶";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(5, 375);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(66, 70);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "◀";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(402, 27);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 599);
            this.splitterControl1.TabIndex = 15;
            this.splitterControl1.TabStop = false;
            // 
            // treeList2
            // 
            this.treeList2.ColumnPanelRowHeight = 30;
            this.treeList2.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.UPPER_MENU_NM});
            this.treeList2.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeList2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.treeList2.Location = new System.Drawing.Point(2, 27);
            this.treeList2.Name = "treeList2";
            this.treeList2.RowHeight = 30;
            this.treeList2.Size = new System.Drawing.Size(400, 599);
            this.treeList2.TabIndex = 14;
            this.treeList2.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.Light;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 15F);
            this.treeListColumn1.AppearanceCell.Options.UseFont = true;
            this.treeListColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 15F);
            this.treeListColumn1.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn1.Caption = "메뉴리스트";
            this.treeListColumn1.FieldName = "MENU_NM";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // UPPER_MENU_NM
            // 
            this.UPPER_MENU_NM.Caption = "UPPER_MENU_NM";
            this.UPPER_MENU_NM.FieldName = "UPPER_MENU_NM";
            this.UPPER_MENU_NM.Name = "UPPER_MENU_NM";
            this.UPPER_MENU_NM.Visible = true;
            this.UPPER_MENU_NM.VisibleIndex = 1;
            // 
            // SysAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(1365, 690);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "SysAuth";
            this.Load += new System.EventHandler(this.SysAuth_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grCom_M)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCom_M)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEx1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEx1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private Common.Control.OctoButton btnSearch;
        private Common.Control.OctoButton btnClose;
        private Common.Control.OctoButton btnSave;
        private Common.Control.OctoButton btnNew;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private JPlatform.Client.Controls.GridControlEx grCom_M;
        private JPlatform.Client.Controls.GridViewEx gvCom_M;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx1;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.TextEdit txtNAME;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private JPlatform.Client.Controls.GridControlEx gridControlEx1;
        private JPlatform.Client.Controls.GridViewEx gridViewEx1;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx3;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx4;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx5;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx6;
        private JPlatform.Client.Controls.GridColumnEx TYPE_CD;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn UPPER_MENU_NM;
    }
}
