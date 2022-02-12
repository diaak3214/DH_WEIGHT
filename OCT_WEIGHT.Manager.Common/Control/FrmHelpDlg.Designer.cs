namespace OCT_WEIGHT.Manager.Common.Control
{
    partial class FrmHelpDlg
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
            this.grdSearch = new DevExpress.XtraGrid.GridControl();
            this.gvSearch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.edtSearch = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new OCT_WEIGHT.Manager.Common.Control.OctoButton(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnOK = new OCT_WEIGHT.Manager.Common.Control.OctoButton(this.components);
            this.btnClose = new OCT_WEIGHT.Manager.Common.Control.OctoButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdSearch
            // 
            this.grdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSearch.EmbeddedNavigator.Name = "";
            this.grdSearch.FormsUseDefaultLookAndFeel = false;
            this.grdSearch.Location = new System.Drawing.Point(0, 36);
            this.grdSearch.MainView = this.gvSearch;
            this.grdSearch.Name = "grdSearch";
            this.grdSearch.Size = new System.Drawing.Size(382, 286);
            this.grdSearch.TabIndex = 0;
            this.grdSearch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSearch,
            this.gridView2});
            // 
            // gvSearch
            // 
            this.gvSearch.GridControl = this.grdSearch;
            this.gvSearch.Name = "gvSearch";
            this.gvSearch.OptionsView.ShowGroupPanel = false;
            this.gvSearch.DoubleClick += new System.EventHandler(this.gvSearch_DoubleClick);
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdSearch;
            this.gridView2.Name = "gridView2";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "검색조건";
            // 
            // edtSearch
            // 
            this.edtSearch.Location = new System.Drawing.Point(59, 8);
            this.edtSearch.Name = "edtSearch";
            this.edtSearch.Size = new System.Drawing.Size(216, 21);
            this.edtSearch.TabIndex = 2;
            this.edtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.AllowYn = "Y";
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Appearance.Font = new System.Drawing.Font("나눔고딕", 9F);
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnSearch.Location = new System.Drawing.Point(295, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "검색";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.edtSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(382, 36);
            this.panelControl1.TabIndex = 4;
            // 
            // panelControl2
            // 
            this.panelControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 322);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(382, 36);
            this.panelControl2.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.AllowYn = "Y";
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Appearance.Font = new System.Drawing.Font("나눔고딕", 9F);
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnOK.Location = new System.Drawing.Point(214, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "선택";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowYn = "Y";
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Appearance.Font = new System.Drawing.Font("나눔고딕", 9F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.CRUD_type = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
            this.btnClose.Location = new System.Drawing.Point(295, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmHelpDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 358);
            this.Controls.Add(this.grdSearch);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.Name = "FrmHelpDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "코드검색";
            ((System.ComponentModel.ISupportInitialize)(this.grdSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdSearch;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSearch;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit edtSearch;
        private OctoButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private OctoButton btnOK;
        private OctoButton btnClose;
    }
}