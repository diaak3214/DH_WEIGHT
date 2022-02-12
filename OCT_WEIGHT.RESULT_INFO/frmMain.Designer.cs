namespace OCT_WEIGHT.RESULT_INFO
{
    partial class frmMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grd_rfid = new JPlatform.Client.Controls.GridControlEx();
            this.gvw_rfid = new JPlatform.Client.Controls.GridViewEx();
            this.gridColumnEx7 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx1 = new JPlatform.Client.Controls.GridColumnEx();
            this.colFIXED_YN = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx4 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx5 = new JPlatform.Client.Controls.GridColumnEx();
            this.colAUTO_YN = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx2 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx3 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx12 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx10 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx11 = new JPlatform.Client.Controls.GridColumnEx();
            this.colRFID_NO = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx6 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx13 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx14 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx8 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx9 = new JPlatform.Client.Controls.GridColumnEx();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPanel = new DevExpress.XtraEditors.PanelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtRFID_NO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.RFID_DATE = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_rfid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvw_rfid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPanel)).BeginInit();
            this.btnPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRFID_NO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RFID_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RFID_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 77);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(808, 334);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grd_rfid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(800, 308);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "실적조회";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grd_rfid
            // 
            this.grd_rfid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_rfid.Location = new System.Drawing.Point(3, 3);
            this.grd_rfid.MainView = this.gvw_rfid;
            this.grd_rfid.Name = "grd_rfid";
            this.grd_rfid.Size = new System.Drawing.Size(794, 302);
            this.grd_rfid.TabIndex = 18;
            this.grd_rfid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvw_rfid});
            // 
            // gvw_rfid
            // 
            this.gvw_rfid.ActionMode = JPlatform.Client.Controls.ActionMode.View;
            this.gvw_rfid.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvw_rfid.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvw_rfid.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gvw_rfid.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvw_rfid.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvw_rfid.Appearance.FocusedRow.Options.UseFont = true;
            this.gvw_rfid.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvw_rfid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gvw_rfid.ColumnPanelRowHeight = 30;
            this.gvw_rfid.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnEx7,
            this.gridColumnEx1,
            this.colFIXED_YN,
            this.gridColumnEx4,
            this.gridColumnEx5,
            this.colAUTO_YN,
            this.gridColumnEx2,
            this.gridColumnEx3,
            this.gridColumnEx12,
            this.gridColumnEx10,
            this.gridColumnEx11,
            this.colRFID_NO,
            this.gridColumnEx6,
            this.gridColumnEx13,
            this.gridColumnEx14,
            this.gridColumnEx8,
            this.gridColumnEx9});
            this.gvw_rfid.GridControl = this.grd_rfid;
            this.gvw_rfid.IndicatorWidth = 35;
            this.gvw_rfid.Name = "gvw_rfid";
            this.gvw_rfid.OptionsView.ColumnAutoWidth = false;
            this.gvw_rfid.OptionsView.ShowGroupPanel = false;
            this.gvw_rfid.PaintStyleName = "MixedXP";
            this.gvw_rfid.RowHeight = 30;
            this.gvw_rfid.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvw_rfid_CustomDrawRowIndicator);
            // 
            // gridColumnEx7
            // 
            this.gridColumnEx7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx7.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEx7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx7.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx7.BindingField = "ITEM_DAE_NM";
            this.gridColumnEx7.Caption = "분류";
            this.gridColumnEx7.ColumnEdit = null;
            this.gridColumnEx7.FieldName = "ITEM_DAE_NM";
            this.gridColumnEx7.Name = "gridColumnEx7";
            this.gridColumnEx7.OptionsColumn.AllowEdit = false;
            this.gridColumnEx7.OptionsColumn.ReadOnly = true;
            this.gridColumnEx7.Visible = true;
            this.gridColumnEx7.VisibleIndex = 0;
            this.gridColumnEx7.Width = 50;
            // 
            // gridColumnEx1
            // 
            this.gridColumnEx1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx1.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEx1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx1.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx1.BindingField = "WGHT_NO";
            this.gridColumnEx1.Caption = "계량번호";
            this.gridColumnEx1.ColumnEdit = null;
            this.gridColumnEx1.FieldName = "WGHT_NO";
            this.gridColumnEx1.Name = "gridColumnEx1";
            this.gridColumnEx1.OptionsColumn.AllowEdit = false;
            this.gridColumnEx1.OptionsColumn.ReadOnly = true;
            this.gridColumnEx1.Visible = true;
            this.gridColumnEx1.VisibleIndex = 1;
            this.gridColumnEx1.Width = 110;
            // 
            // colFIXED_YN
            // 
            this.colFIXED_YN.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFIXED_YN.AppearanceCell.Options.UseFont = true;
            this.colFIXED_YN.AppearanceCell.Options.UseTextOptions = true;
            this.colFIXED_YN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colFIXED_YN.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFIXED_YN.AppearanceHeader.Options.UseFont = true;
            this.colFIXED_YN.AppearanceHeader.Options.UseTextOptions = true;
            this.colFIXED_YN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFIXED_YN.BindingField = "VEHL_NO";
            this.colFIXED_YN.Caption = "차량번호";
            this.colFIXED_YN.ColumnEdit = null;
            this.colFIXED_YN.FieldName = "VEHL_NO";
            this.colFIXED_YN.Name = "colFIXED_YN";
            this.colFIXED_YN.OptionsColumn.AllowEdit = false;
            this.colFIXED_YN.OptionsColumn.ReadOnly = true;
            this.colFIXED_YN.Visible = true;
            this.colFIXED_YN.VisibleIndex = 2;
            this.colFIXED_YN.Width = 100;
            // 
            // gridColumnEx4
            // 
            this.gridColumnEx4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx4.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEx4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx4.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx4.BindingField = "LOAD_DATE";
            this.gridColumnEx4.Caption = "1차계량일시";
            this.gridColumnEx4.ColumnEdit = null;
            this.gridColumnEx4.FieldName = "LOAD_DATE";
            this.gridColumnEx4.Name = "gridColumnEx4";
            this.gridColumnEx4.OptionsColumn.AllowEdit = false;
            this.gridColumnEx4.OptionsColumn.ReadOnly = true;
            this.gridColumnEx4.Visible = true;
            this.gridColumnEx4.VisibleIndex = 3;
            this.gridColumnEx4.Width = 115;
            // 
            // gridColumnEx5
            // 
            this.gridColumnEx5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx5.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEx5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx5.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx5.BindingField = "DOWN_DATE";
            this.gridColumnEx5.Caption = "2차계량일시";
            this.gridColumnEx5.ColumnEdit = null;
            this.gridColumnEx5.FieldName = "DOWN_DATE";
            this.gridColumnEx5.Name = "gridColumnEx5";
            this.gridColumnEx5.OptionsColumn.AllowEdit = false;
            this.gridColumnEx5.OptionsColumn.ReadOnly = true;
            this.gridColumnEx5.Visible = true;
            this.gridColumnEx5.VisibleIndex = 4;
            this.gridColumnEx5.Width = 115;
            // 
            // colAUTO_YN
            // 
            this.colAUTO_YN.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colAUTO_YN.AppearanceCell.Options.UseFont = true;
            this.colAUTO_YN.AppearanceCell.Options.UseTextOptions = true;
            this.colAUTO_YN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAUTO_YN.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colAUTO_YN.AppearanceHeader.Options.UseFont = true;
            this.colAUTO_YN.AppearanceHeader.Options.UseTextOptions = true;
            this.colAUTO_YN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAUTO_YN.BindingField = "LOAD_WEIGHT";
            this.colAUTO_YN.Caption = "1차중량";
            this.colAUTO_YN.ColumnEdit = null;
            this.colAUTO_YN.FieldName = "LOAD_WEIGHT";
            this.colAUTO_YN.Name = "colAUTO_YN";
            this.colAUTO_YN.OptionsColumn.AllowEdit = false;
            this.colAUTO_YN.OptionsColumn.ReadOnly = true;
            this.colAUTO_YN.Visible = true;
            this.colAUTO_YN.VisibleIndex = 5;
            this.colAUTO_YN.Width = 90;
            // 
            // gridColumnEx2
            // 
            this.gridColumnEx2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx2.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEx2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumnEx2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx2.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx2.BindingField = "DOWN_WEIGHT";
            this.gridColumnEx2.Caption = "2차중량";
            this.gridColumnEx2.ColumnEdit = null;
            this.gridColumnEx2.FieldName = "DOWN_WEIGHT";
            this.gridColumnEx2.Name = "gridColumnEx2";
            this.gridColumnEx2.OptionsColumn.AllowEdit = false;
            this.gridColumnEx2.OptionsColumn.ReadOnly = true;
            this.gridColumnEx2.Visible = true;
            this.gridColumnEx2.VisibleIndex = 6;
            this.gridColumnEx2.Width = 90;
            // 
            // gridColumnEx3
            // 
            this.gridColumnEx3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx3.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEx3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumnEx3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx3.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx3.BindingField = "REAL_WGHT";
            this.gridColumnEx3.Caption = "실중량";
            this.gridColumnEx3.ColumnEdit = null;
            this.gridColumnEx3.FieldName = "REAL_WGHT";
            this.gridColumnEx3.Name = "gridColumnEx3";
            this.gridColumnEx3.OptionsColumn.AllowEdit = false;
            this.gridColumnEx3.OptionsColumn.ReadOnly = true;
            this.gridColumnEx3.Visible = true;
            this.gridColumnEx3.VisibleIndex = 7;
            this.gridColumnEx3.Width = 90;
            // 
            // gridColumnEx12
            // 
            this.gridColumnEx12.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx12.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx12.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx12.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx12.BindingField = "PROD_WGHT";
            this.gridColumnEx12.Caption = "이론중량";
            this.gridColumnEx12.ColumnEdit = null;
            this.gridColumnEx12.FieldName = "PROD_WGHT";
            this.gridColumnEx12.Name = "gridColumnEx12";
            this.gridColumnEx12.OptionsColumn.AllowEdit = false;
            this.gridColumnEx12.OptionsColumn.ReadOnly = true;
            this.gridColumnEx12.Visible = true;
            this.gridColumnEx12.VisibleIndex = 8;
            this.gridColumnEx12.Width = 90;
            // 
            // gridColumnEx10
            // 
            this.gridColumnEx10.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx10.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx10.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx10.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx10.BindingField = "ITEM_JUNG_NM";
            this.gridColumnEx10.Caption = "중분류";
            this.gridColumnEx10.ColumnEdit = null;
            this.gridColumnEx10.FieldName = "ITEM_JUNG_NM";
            this.gridColumnEx10.Name = "gridColumnEx10";
            this.gridColumnEx10.OptionsColumn.AllowEdit = false;
            this.gridColumnEx10.OptionsColumn.ReadOnly = true;
            this.gridColumnEx10.Visible = true;
            this.gridColumnEx10.VisibleIndex = 9;
            this.gridColumnEx10.Width = 65;
            // 
            // gridColumnEx11
            // 
            this.gridColumnEx11.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx11.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx11.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx11.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx11.BindingField = "ITEM_SO_NM";
            this.gridColumnEx11.Caption = "품목명";
            this.gridColumnEx11.ColumnEdit = null;
            this.gridColumnEx11.FieldName = "ITEM_SO_NM";
            this.gridColumnEx11.Name = "gridColumnEx11";
            this.gridColumnEx11.OptionsColumn.AllowEdit = false;
            this.gridColumnEx11.OptionsColumn.ReadOnly = true;
            this.gridColumnEx11.Visible = true;
            this.gridColumnEx11.VisibleIndex = 10;
            this.gridColumnEx11.Width = 90;
            // 
            // colRFID_NO
            // 
            this.colRFID_NO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colRFID_NO.AppearanceCell.Options.UseFont = true;
            this.colRFID_NO.AppearanceCell.Options.UseTextOptions = true;
            this.colRFID_NO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colRFID_NO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colRFID_NO.AppearanceHeader.Options.UseFont = true;
            this.colRFID_NO.AppearanceHeader.Options.UseTextOptions = true;
            this.colRFID_NO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRFID_NO.BindingField = "RFID_NO";
            this.colRFID_NO.Caption = "RFID";
            this.colRFID_NO.ColumnEdit = null;
            this.colRFID_NO.FieldName = "RFID_NO";
            this.colRFID_NO.Name = "colRFID_NO";
            this.colRFID_NO.OptionsColumn.AllowEdit = false;
            this.colRFID_NO.OptionsColumn.ReadOnly = true;
            this.colRFID_NO.Visible = true;
            this.colRFID_NO.VisibleIndex = 11;
            this.colRFID_NO.Width = 65;
            // 
            // gridColumnEx6
            // 
            this.gridColumnEx6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx6.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEx6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnEx6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx6.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx6.BindingField = "RFID_SEQ";
            this.gridColumnEx6.Caption = "배차번호";
            this.gridColumnEx6.ColumnEdit = null;
            this.gridColumnEx6.FieldName = "RFID_SEQ";
            this.gridColumnEx6.Name = "gridColumnEx6";
            this.gridColumnEx6.OptionsColumn.AllowEdit = false;
            this.gridColumnEx6.OptionsColumn.ReadOnly = true;
            this.gridColumnEx6.Width = 181;
            // 
            // gridColumnEx13
            // 
            this.gridColumnEx13.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx13.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx13.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx13.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx13.BindingField = "ITEM_SO";
            this.gridColumnEx13.Caption = "ITEM_SO";
            this.gridColumnEx13.ColumnEdit = null;
            this.gridColumnEx13.FieldName = "ITEM_SO";
            this.gridColumnEx13.Name = "gridColumnEx13";
            this.gridColumnEx13.OptionsColumn.AllowEdit = false;
            this.gridColumnEx13.OptionsColumn.ReadOnly = true;
            // 
            // gridColumnEx14
            // 
            this.gridColumnEx14.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx14.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx14.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx14.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx14.BindingField = "RFID_SEQ";
            this.gridColumnEx14.Caption = "RFID_SEQ";
            this.gridColumnEx14.ColumnEdit = null;
            this.gridColumnEx14.FieldName = "RFID_SEQ";
            this.gridColumnEx14.Name = "gridColumnEx14";
            this.gridColumnEx14.OptionsColumn.AllowEdit = false;
            this.gridColumnEx14.OptionsColumn.ReadOnly = true;
            // 
            // gridColumnEx8
            // 
            this.gridColumnEx8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx8.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEx8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnEx8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx8.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx8.BindingField = "DEL_REMARK";
            this.gridColumnEx8.Caption = "삭제사유";
            this.gridColumnEx8.ColumnEdit = null;
            this.gridColumnEx8.FieldName = "DEL_REMARK";
            this.gridColumnEx8.MaxLength = 50;
            this.gridColumnEx8.Name = "gridColumnEx8";
            this.gridColumnEx8.Visible = true;
            this.gridColumnEx8.VisibleIndex = 12;
            this.gridColumnEx8.Width = 200;
            // 
            // gridColumnEx9
            // 
            this.gridColumnEx9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx9.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnEx9.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx9.BindingField = "DEL_USER";
            this.gridColumnEx9.Caption = "삭제자";
            this.gridColumnEx9.ColumnEdit = null;
            this.gridColumnEx9.FieldName = "DEL_USER";
            this.gridColumnEx9.MaxLength = 20;
            this.gridColumnEx9.Name = "gridColumnEx9";
            this.gridColumnEx9.Visible = true;
            this.gridColumnEx9.VisibleIndex = 13;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::OCT_WEIGHT.RESULT_INFO.Properties.Resources.icon09_on;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(318, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 40);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "  닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnDel.Image = global::OCT_WEIGHT.RESULT_INFO.Properties.Resources.icon05_on;
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(249, 14);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(70, 40);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "  삭제";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Image = global::OCT_WEIGHT.RESULT_INFO.Properties.Resources.icon01_on;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(12, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 40);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "   조회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPanel
            // 
            this.btnPanel.Controls.Add(this.textEdit2);
            this.btnPanel.Controls.Add(this.labelControl4);
            this.btnPanel.Controls.Add(this.textEdit1);
            this.btnPanel.Controls.Add(this.labelControl3);
            this.btnPanel.Controls.Add(this.txtRFID_NO);
            this.btnPanel.Controls.Add(this.labelControl2);
            this.btnPanel.Controls.Add(this.RFID_DATE);
            this.btnPanel.Controls.Add(this.labelControl1);
            this.btnPanel.Controls.Add(this.panelControl2);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPanel.Location = new System.Drawing.Point(0, 0);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(808, 77);
            this.btnPanel.TabIndex = 4;
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(288, 41);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textEdit2.Properties.Appearance.Options.UseFont = true;
            this.textEdit2.Properties.MaxLength = 10;
            this.textEdit2.Size = new System.Drawing.Size(120, 22);
            this.textEdit2.TabIndex = 48;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.LightCoral;
            this.labelControl4.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl4.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl4.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl4.Location = new System.Drawing.Point(202, 38);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(80, 31);
            this.labelControl4.TabIndex = 47;
            this.labelControl4.Text = "상차지시번호";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(288, 8);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.MaxLength = 10;
            this.textEdit1.Size = new System.Drawing.Size(120, 22);
            this.textEdit1.TabIndex = 46;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.LightCoral;
            this.labelControl3.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl3.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl3.Location = new System.Drawing.Point(202, 5);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(80, 31);
            this.labelControl3.TabIndex = 45;
            this.labelControl3.Text = "계량번호";
            // 
            // txtRFID_NO
            // 
            this.txtRFID_NO.Location = new System.Drawing.Point(81, 41);
            this.txtRFID_NO.Name = "txtRFID_NO";
            this.txtRFID_NO.Properties.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtRFID_NO.Properties.Appearance.Options.UseFont = true;
            this.txtRFID_NO.Properties.MaxLength = 10;
            this.txtRFID_NO.Size = new System.Drawing.Size(115, 22);
            this.txtRFID_NO.TabIndex = 44;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.LightCoral;
            this.labelControl2.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl2.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl2.Location = new System.Drawing.Point(10, 39);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 31);
            this.labelControl2.TabIndex = 43;
            this.labelControl2.Text = "차량번호";
            // 
            // RFID_DATE
            // 
            this.RFID_DATE.EditValue = null;
            this.RFID_DATE.Location = new System.Drawing.Point(81, 7);
            this.RFID_DATE.Name = "RFID_DATE";
            this.RFID_DATE.Properties.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RFID_DATE.Properties.Appearance.Options.UseFont = true;
            this.RFID_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RFID_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.RFID_DATE.Size = new System.Drawing.Size(115, 22);
            this.RFID_DATE.TabIndex = 42;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.LightCoral;
            this.labelControl1.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl1.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl1.Location = new System.Drawing.Point(10, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 31);
            this.labelControl1.TabIndex = 30;
            this.labelControl1.Text = "계량일자";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.button2);
            this.panelControl2.Controls.Add(this.button1);
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Controls.Add(this.btnDel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(414, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(392, 73);
            this.panelControl2.TabIndex = 27;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Image = global::OCT_WEIGHT.RESULT_INFO.Properties.Resources.icon07_on;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(165, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 40);
            this.button2.TabIndex = 11;
            this.button2.Text = "   확인증";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Image = global::OCT_WEIGHT.RESULT_INFO.Properties.Resources.icon07_on;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(81, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 40);
            this.button1.TabIndex = 10;
            this.button1.Text = "   계량표";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 411);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnPanel);
            this.Name = "frmMain";
            this.Text = "계량실적조회";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_rfid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvw_rfid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPanel)).EndInit();
            this.btnPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRFID_NO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RFID_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RFID_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private JPlatform.Client.Controls.GridControlEx grd_rfid;
        private JPlatform.Client.Controls.GridViewEx gvw_rfid;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx1;
        private JPlatform.Client.Controls.GridColumnEx colRFID_NO;
        private JPlatform.Client.Controls.GridColumnEx colFIXED_YN;
        private JPlatform.Client.Controls.GridColumnEx colAUTO_YN;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx2;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx3;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx4;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx5;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnSearch;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx8;
        private DevExpress.XtraEditors.PanelControl btnPanel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit RFID_DATE;
        private DevExpress.XtraEditors.TextEdit txtRFID_NO;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx7;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx10;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx11;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx12;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx13;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx14;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx9;

    }
}

