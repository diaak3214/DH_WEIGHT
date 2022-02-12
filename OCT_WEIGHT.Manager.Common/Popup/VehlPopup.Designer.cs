namespace OCT_WEIGHT.Manager.Common.Popup
{
    partial class VehlPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VehlPopup));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txVEHLNO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.octoButton1 = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.BtnNew = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.BtnSearch = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.BtnClose = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.grd_carsearch = new JPlatform.Client.Controls.GridControlEx();
            this.gvw_carsearch = new JPlatform.Client.Controls.GridViewEx();
            this.VEHL_NO1 = new JPlatform.Client.Controls.GridColumnEx();
            this.CARRI_NM = new JPlatform.Client.Controls.GridColumnEx();
            this.DRVR_NM = new JPlatform.Client.Controls.GridColumnEx();
            this.DRVR_PHON = new JPlatform.Client.Controls.GridColumnEx();
            this.VEHL_KIND = new JPlatform.Client.Controls.GridColumnEx();
            this.VEHL_FLAG = new JPlatform.Client.Controls.GridColumnEx();
            this.CAR_WK_TP = new JPlatform.Client.Controls.GridColumnEx();
            this.FIX_YN = new JPlatform.Client.Controls.GridColumnEx();
            this.PROD_WGHT = new JPlatform.Client.Controls.GridColumnEx();
            this.USE_YN = new JPlatform.Client.Controls.GridColumnEx();
            this.REMARK = new JPlatform.Client.Controls.GridColumnEx();
            this.VEHL_CD = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx2 = new JPlatform.Client.Controls.GridColumnEx();
            this.EMPTY_WEIGHT = new JPlatform.Client.Controls.GridColumnEx();
            this.PALLET = new JPlatform.Client.Controls.GridColumnEx();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txVEHLNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_carsearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvw_carsearch)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txVEHLNO);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1265, 68);
            this.panelControl1.TabIndex = 16;
            // 
            // txVEHLNO
            // 
            this.txVEHLNO.Location = new System.Drawing.Point(111, 9);
            this.txVEHLNO.Name = "txVEHLNO";
            this.txVEHLNO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txVEHLNO.Properties.Appearance.Options.UseFont = true;
            this.txVEHLNO.Size = new System.Drawing.Size(249, 26);
            this.txVEHLNO.TabIndex = 52;
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
            this.labelControl2.Size = new System.Drawing.Size(93, 36);
            this.labelControl2.TabIndex = 46;
            this.labelControl2.Text = "차량번호";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.octoButton1);
            this.panelControl4.Controls.Add(this.BtnNew);
            this.panelControl4.Controls.Add(this.BtnSearch);
            this.panelControl4.Controls.Add(this.BtnClose);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl4.Location = new System.Drawing.Point(880, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(383, 64);
            this.panelControl4.TabIndex = 41;
            // 
            // octoButton1
            // 
            this.octoButton1.AllowYn = "Y";
            this.octoButton1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.octoButton1.Appearance.Options.UseFont = true;
            this.octoButton1.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.octoButton1.Image = ((System.Drawing.Image)(resources.GetObject("octoButton1.Image")));
            this.octoButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.octoButton1.Location = new System.Drawing.Point(195, 5);
            this.octoButton1.Name = "octoButton1";
            this.octoButton1.Size = new System.Drawing.Size(90, 55);
            this.octoButton1.TabIndex = 18;
            this.octoButton1.Text = "확인";
            this.octoButton1.Click += new System.EventHandler(this.octoButton1_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.AllowYn = "Y";
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.BtnNew.Image = ((System.Drawing.Image)(resources.GetObject("BtnNew.Image")));
            this.BtnNew.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnNew.Location = new System.Drawing.Point(99, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(90, 55);
            this.BtnNew.TabIndex = 17;
            this.BtnNew.Text = "초기화";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.AllowYn = "Y";
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.BtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnSearch.Image")));
            this.BtnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnSearch.Location = new System.Drawing.Point(3, 5);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(90, 55);
            this.BtnSearch.TabIndex = 15;
            this.BtnSearch.Text = "조회";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.AllowYn = "Y";
            this.BtnClose.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BtnClose.Appearance.Options.UseFont = true;
            this.BtnClose.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.BtnClose.Image = ((System.Drawing.Image)(resources.GetObject("BtnClose.Image")));
            this.BtnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnClose.Location = new System.Drawing.Point(291, 5);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(90, 55);
            this.BtnClose.TabIndex = 12;
            this.BtnClose.Text = "닫기";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // grd_carsearch
            // 
            this.grd_carsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_carsearch.Location = new System.Drawing.Point(0, 68);
            this.grd_carsearch.MainView = this.gvw_carsearch;
            this.grd_carsearch.Name = "grd_carsearch";
            this.grd_carsearch.Size = new System.Drawing.Size(1265, 698);
            this.grd_carsearch.TabIndex = 17;
            this.grd_carsearch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvw_carsearch});
            // 
            // gvw_carsearch
            // 
            this.gvw_carsearch.ActionMode = JPlatform.Client.Controls.ActionMode.View;
            this.gvw_carsearch.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvw_carsearch.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvw_carsearch.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gvw_carsearch.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvw_carsearch.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvw_carsearch.Appearance.FocusedRow.Options.UseFont = true;
            this.gvw_carsearch.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvw_carsearch.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gvw_carsearch.ColumnPanelRowHeight = 30;
            this.gvw_carsearch.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.VEHL_NO1,
            this.CARRI_NM,
            this.DRVR_NM,
            this.DRVR_PHON,
            this.VEHL_KIND,
            this.VEHL_FLAG,
            this.CAR_WK_TP,
            this.FIX_YN,
            this.PROD_WGHT,
            this.USE_YN,
            this.REMARK,
            this.VEHL_CD,
            this.gridColumnEx2,
            this.EMPTY_WEIGHT,
            this.PALLET});
            this.gvw_carsearch.GridControl = this.grd_carsearch;
            this.gvw_carsearch.IndicatorWidth = 35;
            this.gvw_carsearch.Name = "gvw_carsearch";
            this.gvw_carsearch.OptionsView.ColumnAutoWidth = false;
            this.gvw_carsearch.OptionsView.ShowGroupPanel = false;
            this.gvw_carsearch.PaintStyleName = "MixedXP";
            this.gvw_carsearch.RowHeight = 30;
            this.gvw_carsearch.DoubleClick += new System.EventHandler(this.gvw_carsearch_DoubleClick);
            // 
            // VEHL_NO1
            // 
            this.VEHL_NO1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.VEHL_NO1.AppearanceCell.Options.UseFont = true;
            this.VEHL_NO1.AppearanceCell.Options.UseTextOptions = true;
            this.VEHL_NO1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.VEHL_NO1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.VEHL_NO1.AppearanceHeader.Options.UseFont = true;
            this.VEHL_NO1.AppearanceHeader.Options.UseTextOptions = true;
            this.VEHL_NO1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.VEHL_NO1.BindingField = "VEHL_NO";
            this.VEHL_NO1.Caption = "차량번호";
            this.VEHL_NO1.ColumnEdit = null;
            this.VEHL_NO1.FieldName = "VEHL_NO";
            this.VEHL_NO1.Name = "VEHL_NO1";
            this.VEHL_NO1.OptionsColumn.AllowEdit = false;
            this.VEHL_NO1.OptionsColumn.ReadOnly = true;
            this.VEHL_NO1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.VEHL_NO1.Visible = true;
            this.VEHL_NO1.VisibleIndex = 0;
            this.VEHL_NO1.Width = 120;
            // 
            // CARRI_NM
            // 
            this.CARRI_NM.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.CARRI_NM.AppearanceCell.Options.UseFont = true;
            this.CARRI_NM.AppearanceCell.Options.UseTextOptions = true;
            this.CARRI_NM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CARRI_NM.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.CARRI_NM.AppearanceHeader.Options.UseFont = true;
            this.CARRI_NM.AppearanceHeader.Options.UseTextOptions = true;
            this.CARRI_NM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CARRI_NM.BindingField = "CARRI_NM";
            this.CARRI_NM.Caption = "운송사명";
            this.CARRI_NM.ColumnEdit = null;
            this.CARRI_NM.FieldName = "CARRI_NM";
            this.CARRI_NM.Name = "CARRI_NM";
            this.CARRI_NM.OptionsColumn.AllowEdit = false;
            this.CARRI_NM.OptionsColumn.ReadOnly = true;
            this.CARRI_NM.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.CARRI_NM.Visible = true;
            this.CARRI_NM.VisibleIndex = 1;
            this.CARRI_NM.Width = 116;
            // 
            // DRVR_NM
            // 
            this.DRVR_NM.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.DRVR_NM.AppearanceCell.Options.UseFont = true;
            this.DRVR_NM.AppearanceCell.Options.UseTextOptions = true;
            this.DRVR_NM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DRVR_NM.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.DRVR_NM.AppearanceHeader.Options.UseFont = true;
            this.DRVR_NM.AppearanceHeader.Options.UseTextOptions = true;
            this.DRVR_NM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DRVR_NM.BindingField = "DRVR_NM";
            this.DRVR_NM.Caption = "운전자명";
            this.DRVR_NM.ColumnEdit = null;
            this.DRVR_NM.FieldName = "DRVR_NM";
            this.DRVR_NM.Name = "DRVR_NM";
            this.DRVR_NM.OptionsColumn.AllowEdit = false;
            this.DRVR_NM.OptionsColumn.ReadOnly = true;
            this.DRVR_NM.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.DRVR_NM.Visible = true;
            this.DRVR_NM.VisibleIndex = 2;
            this.DRVR_NM.Width = 130;
            // 
            // DRVR_PHON
            // 
            this.DRVR_PHON.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.DRVR_PHON.AppearanceCell.Options.UseFont = true;
            this.DRVR_PHON.AppearanceCell.Options.UseTextOptions = true;
            this.DRVR_PHON.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DRVR_PHON.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.DRVR_PHON.AppearanceHeader.Options.UseFont = true;
            this.DRVR_PHON.AppearanceHeader.Options.UseTextOptions = true;
            this.DRVR_PHON.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DRVR_PHON.BindingField = "DRVR_PHON";
            this.DRVR_PHON.Caption = "운전자연락처";
            this.DRVR_PHON.ColumnEdit = null;
            this.DRVR_PHON.FieldName = "DRVR_PHON";
            this.DRVR_PHON.Name = "DRVR_PHON";
            this.DRVR_PHON.OptionsColumn.AllowEdit = false;
            this.DRVR_PHON.OptionsColumn.ReadOnly = true;
            this.DRVR_PHON.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.DRVR_PHON.Visible = true;
            this.DRVR_PHON.VisibleIndex = 3;
            this.DRVR_PHON.Width = 120;
            // 
            // VEHL_KIND
            // 
            this.VEHL_KIND.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.VEHL_KIND.AppearanceCell.Options.UseFont = true;
            this.VEHL_KIND.AppearanceCell.Options.UseTextOptions = true;
            this.VEHL_KIND.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.VEHL_KIND.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.VEHL_KIND.AppearanceHeader.Options.UseFont = true;
            this.VEHL_KIND.AppearanceHeader.Options.UseTextOptions = true;
            this.VEHL_KIND.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.VEHL_KIND.BindingField = "VEHL_KIND";
            this.VEHL_KIND.Caption = "차량종류";
            this.VEHL_KIND.ColumnEdit = null;
            this.VEHL_KIND.FieldName = "VEHL_KIND";
            this.VEHL_KIND.Name = "VEHL_KIND";
            this.VEHL_KIND.OptionsColumn.AllowEdit = false;
            this.VEHL_KIND.OptionsColumn.ReadOnly = true;
            this.VEHL_KIND.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.VEHL_KIND.Visible = true;
            this.VEHL_KIND.VisibleIndex = 4;
            this.VEHL_KIND.Width = 87;
            // 
            // VEHL_FLAG
            // 
            this.VEHL_FLAG.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.VEHL_FLAG.AppearanceCell.Options.UseFont = true;
            this.VEHL_FLAG.AppearanceCell.Options.UseTextOptions = true;
            this.VEHL_FLAG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.VEHL_FLAG.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.VEHL_FLAG.AppearanceHeader.Options.UseFont = true;
            this.VEHL_FLAG.AppearanceHeader.Options.UseTextOptions = true;
            this.VEHL_FLAG.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.VEHL_FLAG.BindingField = "VEHL_FLAG";
            this.VEHL_FLAG.Caption = "차량용도";
            this.VEHL_FLAG.ColumnEdit = null;
            this.VEHL_FLAG.FieldName = "VEHL_FLAG";
            this.VEHL_FLAG.Name = "VEHL_FLAG";
            this.VEHL_FLAG.OptionsColumn.AllowEdit = false;
            this.VEHL_FLAG.OptionsColumn.ReadOnly = true;
            this.VEHL_FLAG.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.VEHL_FLAG.Visible = true;
            this.VEHL_FLAG.VisibleIndex = 5;
            this.VEHL_FLAG.Width = 119;
            // 
            // CAR_WK_TP
            // 
            this.CAR_WK_TP.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.CAR_WK_TP.AppearanceCell.Options.UseFont = true;
            this.CAR_WK_TP.AppearanceCell.Options.UseTextOptions = true;
            this.CAR_WK_TP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CAR_WK_TP.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CAR_WK_TP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.CAR_WK_TP.AppearanceHeader.Options.UseFont = true;
            this.CAR_WK_TP.AppearanceHeader.Options.UseTextOptions = true;
            this.CAR_WK_TP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CAR_WK_TP.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CAR_WK_TP.BindingField = "CAR_WK_TP";
            this.CAR_WK_TP.Caption = "차량구분";
            this.CAR_WK_TP.ColumnEdit = null;
            this.CAR_WK_TP.FieldName = "CAR_WK_TP";
            this.CAR_WK_TP.Name = "CAR_WK_TP";
            this.CAR_WK_TP.OptionsColumn.AllowEdit = false;
            this.CAR_WK_TP.OptionsColumn.ReadOnly = true;
            this.CAR_WK_TP.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.CAR_WK_TP.Visible = true;
            this.CAR_WK_TP.VisibleIndex = 6;
            // 
            // FIX_YN
            // 
            this.FIX_YN.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.FIX_YN.AppearanceCell.Options.UseFont = true;
            this.FIX_YN.AppearanceCell.Options.UseTextOptions = true;
            this.FIX_YN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FIX_YN.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.FIX_YN.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.FIX_YN.AppearanceHeader.Options.UseFont = true;
            this.FIX_YN.AppearanceHeader.Options.UseTextOptions = true;
            this.FIX_YN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FIX_YN.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.FIX_YN.BindingField = "FIX_YN";
            this.FIX_YN.Caption = "고정유무";
            this.FIX_YN.ColumnEdit = null;
            this.FIX_YN.FieldName = "FIX_YN";
            this.FIX_YN.Name = "FIX_YN";
            this.FIX_YN.OptionsColumn.AllowEdit = false;
            this.FIX_YN.OptionsColumn.ReadOnly = true;
            this.FIX_YN.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.FIX_YN.Visible = true;
            this.FIX_YN.VisibleIndex = 7;
            // 
            // PROD_WGHT
            // 
            this.PROD_WGHT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.PROD_WGHT.AppearanceCell.Options.UseFont = true;
            this.PROD_WGHT.AppearanceCell.Options.UseTextOptions = true;
            this.PROD_WGHT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PROD_WGHT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.PROD_WGHT.AppearanceHeader.Options.UseFont = true;
            this.PROD_WGHT.AppearanceHeader.Options.UseTextOptions = true;
            this.PROD_WGHT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PROD_WGHT.BindingField = "PROD_WGHT";
            this.PROD_WGHT.Caption = "이론중량";
            this.PROD_WGHT.ColumnEdit = null;
            this.PROD_WGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.PROD_WGHT.FieldName = "PROD_WGHT";
            this.PROD_WGHT.Name = "PROD_WGHT";
            this.PROD_WGHT.OptionsColumn.AllowEdit = false;
            this.PROD_WGHT.OptionsColumn.ReadOnly = true;
            this.PROD_WGHT.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.PROD_WGHT.Visible = true;
            this.PROD_WGHT.VisibleIndex = 8;
            // 
            // USE_YN
            // 
            this.USE_YN.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.USE_YN.AppearanceCell.Options.UseFont = true;
            this.USE_YN.AppearanceCell.Options.UseTextOptions = true;
            this.USE_YN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.USE_YN.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.USE_YN.AppearanceHeader.Options.UseFont = true;
            this.USE_YN.AppearanceHeader.Options.UseTextOptions = true;
            this.USE_YN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.USE_YN.BindingField = "USE_YN";
            this.USE_YN.Caption = "사용여부";
            this.USE_YN.ColumnEdit = null;
            this.USE_YN.FieldName = "USE_YN";
            this.USE_YN.Name = "USE_YN";
            this.USE_YN.OptionsColumn.AllowEdit = false;
            this.USE_YN.OptionsColumn.ReadOnly = true;
            this.USE_YN.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.USE_YN.Visible = true;
            this.USE_YN.VisibleIndex = 9;
            // 
            // REMARK
            // 
            this.REMARK.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.REMARK.AppearanceCell.Options.UseFont = true;
            this.REMARK.AppearanceCell.Options.UseTextOptions = true;
            this.REMARK.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.REMARK.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.REMARK.AppearanceHeader.Options.UseFont = true;
            this.REMARK.AppearanceHeader.Options.UseTextOptions = true;
            this.REMARK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.REMARK.BindingField = "REMARK";
            this.REMARK.Caption = "비고";
            this.REMARK.ColumnEdit = null;
            this.REMARK.FieldName = "REMARK";
            this.REMARK.Name = "REMARK";
            this.REMARK.OptionsColumn.AllowEdit = false;
            this.REMARK.OptionsColumn.ReadOnly = true;
            this.REMARK.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.REMARK.Visible = true;
            this.REMARK.VisibleIndex = 10;
            this.REMARK.Width = 180;
            // 
            // VEHL_CD
            // 
            this.VEHL_CD.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.VEHL_CD.AppearanceCell.Options.UseFont = true;
            this.VEHL_CD.AppearanceCell.Options.UseTextOptions = true;
            this.VEHL_CD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.VEHL_CD.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.VEHL_CD.AppearanceHeader.Options.UseFont = true;
            this.VEHL_CD.AppearanceHeader.Options.UseTextOptions = true;
            this.VEHL_CD.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.VEHL_CD.BindingField = "VEHL_CD";
            this.VEHL_CD.Caption = "차량코드";
            this.VEHL_CD.ColumnEdit = null;
            this.VEHL_CD.FieldName = "VEHL_CD";
            this.VEHL_CD.Name = "VEHL_CD";
            this.VEHL_CD.OptionsColumn.AllowEdit = false;
            this.VEHL_CD.OptionsColumn.ReadOnly = true;
            this.VEHL_CD.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.VEHL_CD.Width = 95;
            // 
            // gridColumnEx2
            // 
            this.gridColumnEx2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx2.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEx2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnEx2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx2.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnEx2.BindingField = "CAR_WK_TP";
            this.gridColumnEx2.Caption = "차량종류코드";
            this.gridColumnEx2.ColumnEdit = null;
            this.gridColumnEx2.FieldName = "CAR_WK_TP";
            this.gridColumnEx2.Name = "gridColumnEx2";
            this.gridColumnEx2.OptionsColumn.AllowEdit = false;
            this.gridColumnEx2.OptionsColumn.ReadOnly = true;
            this.gridColumnEx2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // EMPTY_WEIGHT
            // 
            this.EMPTY_WEIGHT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.EMPTY_WEIGHT.AppearanceCell.Options.UseFont = true;
            this.EMPTY_WEIGHT.AppearanceCell.Options.UseTextOptions = true;
            this.EMPTY_WEIGHT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.EMPTY_WEIGHT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.EMPTY_WEIGHT.AppearanceHeader.Options.UseFont = true;
            this.EMPTY_WEIGHT.AppearanceHeader.Options.UseTextOptions = true;
            this.EMPTY_WEIGHT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.EMPTY_WEIGHT.BindingField = "EMPTY_WEIGHT";
            this.EMPTY_WEIGHT.Caption = "공차중량";
            this.EMPTY_WEIGHT.ColumnEdit = null;
            this.EMPTY_WEIGHT.FieldName = "EMPTY_WEIGHT";
            this.EMPTY_WEIGHT.Name = "EMPTY_WEIGHT";
            this.EMPTY_WEIGHT.OptionsColumn.AllowEdit = false;
            this.EMPTY_WEIGHT.OptionsColumn.ReadOnly = true;
            this.EMPTY_WEIGHT.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.EMPTY_WEIGHT.Width = 105;
            // 
            // PALLET
            // 
            this.PALLET.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.PALLET.AppearanceCell.Options.UseFont = true;
            this.PALLET.AppearanceCell.Options.UseTextOptions = true;
            this.PALLET.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PALLET.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.PALLET.AppearanceHeader.Options.UseFont = true;
            this.PALLET.AppearanceHeader.Options.UseTextOptions = true;
            this.PALLET.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PALLET.BindingField = "PALLET";
            this.PALLET.Caption = "PALLET";
            this.PALLET.ColumnEdit = null;
            this.PALLET.FieldName = "PALLET";
            this.PALLET.Name = "PALLET";
            this.PALLET.OptionsColumn.AllowEdit = false;
            this.PALLET.OptionsColumn.ReadOnly = true;
            this.PALLET.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            // 
            // VehlPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 766);
            this.Controls.Add(this.grd_carsearch);
            this.Controls.Add(this.panelControl1);
            this.Name = "VehlPopup";
            this.Text = "VehlPopup";
            this.Load += new System.EventHandler(this.VehlPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txVEHLNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_carsearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvw_carsearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txVEHLNO;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private Control.OctoButton BtnNew;
        private Control.OctoButton BtnSearch;
        private Control.OctoButton BtnClose;
        private Control.OctoButton octoButton1;
        private JPlatform.Client.Controls.GridControlEx grd_carsearch;
        private JPlatform.Client.Controls.GridViewEx gvw_carsearch;
        private JPlatform.Client.Controls.GridColumnEx VEHL_CD;
        private JPlatform.Client.Controls.GridColumnEx CARRI_NM;
        private JPlatform.Client.Controls.GridColumnEx DRVR_NM;
        private JPlatform.Client.Controls.GridColumnEx DRVR_PHON;
        private JPlatform.Client.Controls.GridColumnEx VEHL_KIND;
        private JPlatform.Client.Controls.GridColumnEx VEHL_FLAG;
        private JPlatform.Client.Controls.GridColumnEx CAR_WK_TP;
        private JPlatform.Client.Controls.GridColumnEx FIX_YN;
        private JPlatform.Client.Controls.GridColumnEx PROD_WGHT;
        private JPlatform.Client.Controls.GridColumnEx USE_YN;
        private JPlatform.Client.Controls.GridColumnEx REMARK;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx2;
        private JPlatform.Client.Controls.GridColumnEx EMPTY_WEIGHT;
        private JPlatform.Client.Controls.GridColumnEx PALLET;
        private JPlatform.Client.Controls.GridColumnEx VEHL_NO1;
    }
}