namespace OCT_WEIGHT.Manager.Common.Popup
{
    partial class MasterCode
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
            this.grdMaster = new JPlatform.Client.Controls.GridControlEx();
            this.gvCom_M = new JPlatform.Client.Controls.GridViewEx();
            this.gridColumnEx1 = new JPlatform.Client.Controls.GridColumnEx();
            this.gridColumnEx2 = new JPlatform.Client.Controls.GridColumnEx();
            this.btnPanel = new DevExpress.XtraEditors.PanelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnUse = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnSave = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            this.btnNew = new OCT_WEIGHT.Manager.Common.Control.OctoButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCom_M)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPanel)).BeginInit();
            this.btnPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grdMaster);
            this.panelControl1.Controls.Add(this.btnPanel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(979, 442);
            this.panelControl1.TabIndex = 0;
            // 
            // grdMaster
            // 
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.Location = new System.Drawing.Point(2, 64);
            this.grdMaster.MainView = this.gvCom_M;
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.Size = new System.Drawing.Size(975, 376);
            this.grdMaster.TabIndex = 14;
            this.grdMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCom_M});
            // 
            // gvCom_M
            // 
            this.gvCom_M.ActionMode = JPlatform.Client.Controls.ActionMode.View;
            this.gvCom_M.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvCom_M.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gvCom_M.Appearance.FocusedRow.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gvCom_M.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvCom_M.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvCom_M.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCom_M.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvCom_M.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gvCom_M.ColumnPanelRowHeight = 30;
            this.gvCom_M.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnEx1,
            this.gridColumnEx2});
            this.gvCom_M.GridControl = this.grdMaster;
            this.gvCom_M.Name = "gvCom_M";
            this.gvCom_M.OptionsView.ColumnAutoWidth = false;
            this.gvCom_M.OptionsView.ShowGroupPanel = false;
            this.gvCom_M.PaintStyleName = "MixedXP";
            this.gvCom_M.RowHeight = 30;
            this.gvCom_M.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvCom_M_FocusedRowChanged);
            // 
            // gridColumnEx1
            // 
            this.gridColumnEx1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx1.AppearanceCell.Options.UseFont = true;
            this.gridColumnEx1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridColumnEx1.AppearanceHeader.Options.UseFont = true;
            this.gridColumnEx1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEx1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEx1.BindingField = "TYPE_CD";
            this.gridColumnEx1.Caption = "코드";
            this.gridColumnEx1.ColumnEdit = null;
            this.gridColumnEx1.FieldName = "TYPE_CD";
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
            this.gridColumnEx2.BindingField = "TYPE_DESC";
            this.gridColumnEx2.Caption = "코드명";
            this.gridColumnEx2.ColumnEdit = null;
            this.gridColumnEx2.FieldName = "TYPE_DESC";
            this.gridColumnEx2.Name = "gridColumnEx2";
            this.gridColumnEx2.OptionsColumn.AllowEdit = false;
            this.gridColumnEx2.OptionsColumn.ReadOnly = true;
            this.gridColumnEx2.Visible = true;
            this.gridColumnEx2.VisibleIndex = 1;
            this.gridColumnEx2.Width = 273;
            // 
            // btnPanel
            // 
            this.btnPanel.Controls.Add(this.txtName);
            this.btnPanel.Controls.Add(this.labelControl1);
            this.btnPanel.Controls.Add(this.txtCode);
            this.btnPanel.Controls.Add(this.labelControl5);
            this.btnPanel.Controls.Add(this.panelControl2);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPanel.Location = new System.Drawing.Point(2, 2);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(975, 62);
            this.btnPanel.TabIndex = 2;
            this.btnPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.btnPanel_Paint);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(382, 11);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.Properties.MaxLength = 10;
            this.txtName.Size = new System.Drawing.Size(175, 30);
            this.txtName.TabIndex = 33;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.LightCoral;
            this.labelControl1.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl1.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl1.Location = new System.Drawing.Point(286, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(93, 31);
            this.labelControl1.TabIndex = 32;
            this.labelControl1.Text = "코드명";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(105, 10);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtCode.Properties.Appearance.Options.UseFont = true;
            this.txtCode.Properties.MaxLength = 10;
            this.txtCode.Size = new System.Drawing.Size(175, 30);
            this.txtCode.TabIndex = 31;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.LightCoral;
            this.labelControl5.Appearance.BackColor2 = System.Drawing.Color.White;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.labelControl5.LineColor = System.Drawing.Color.DarkSlateGray;
            this.labelControl5.Location = new System.Drawing.Point(9, 9);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(93, 31);
            this.labelControl5.TabIndex = 30;
            this.labelControl5.Text = "코드";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Controls.Add(this.btnUse);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Controls.Add(this.btnNew);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(574, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(399, 58);
            this.panelControl2.TabIndex = 27;
            // 
            // btnClose
            // 
            this.btnClose.AllowYn = "Y";
            this.btnClose.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnClose.Image = global::OCT_WEIGHT.Manager.Common.Properties.Resources.icon09_on;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(304, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 47);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "닫기";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUse
            // 
            this.btnUse.AllowYn = "Y";
            this.btnUse.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUse.Appearance.Options.UseFont = true;
            this.btnUse.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnUse.Image = global::OCT_WEIGHT.Manager.Common.Properties.Resources.icon05_on;
            this.btnUse.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnUse.Location = new System.Drawing.Point(195, 3);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(103, 47);
            this.btnUse.TabIndex = 3;
            this.btnUse.Text = "삭제";
            this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
            // 
            // btnSave
            // 
            this.btnSave.AllowYn = "Y";
            this.btnSave.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnSave.Image = global::OCT_WEIGHT.Manager.Common.Properties.Resources.icon03_on;
            this.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(99, 3);
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
            this.btnNew.Image = global::OCT_WEIGHT.Manager.Common.Properties.Resources.icon02_on1;
            this.btnNew.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 47);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "신규";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // MasterCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 442);
            this.Controls.Add(this.panelControl1);
            this.Name = "MasterCode";
            this.Text = "공통코드 마스터 코드 변경";
            this.Load += new System.EventHandler(this.MasterCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCom_M)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPanel)).EndInit();
            this.btnPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl btnPanel;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Control.OctoButton btnClose;
        private Control.OctoButton btnUse;
        private Control.OctoButton btnSave;
        private Control.OctoButton btnNew;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private JPlatform.Client.Controls.GridControlEx grdMaster;
        private JPlatform.Client.Controls.GridViewEx gvCom_M;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx1;
        private JPlatform.Client.Controls.GridColumnEx gridColumnEx2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}