namespace OCT_WEIGHT.Manager.Common.Control
{
    partial class OctoHelper
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.edtKey = new DevExpress.XtraEditors.ButtonEdit();
            this.edtValue = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.edtKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // edtKey
            // 
            this.edtKey.Location = new System.Drawing.Point(0, 0);
            this.edtKey.Name = "edtKey";
            this.edtKey.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edtKey.Size = new System.Drawing.Size(100, 21);
            this.edtKey.TabIndex = 0;
            this.edtKey.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.edtKey_ButtonClick);
            this.edtKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edtKey_KeyDown);
            this.edtKey.TextChanged += new System.EventHandler(this.edtKey_TextChanged);
            // 
            // edtValue
            // 
            this.edtValue.EditValue = "";
            this.edtValue.Location = new System.Drawing.Point(101, 0);
            this.edtValue.Name = "edtValue";
            this.edtValue.Properties.ReadOnly = true;
            this.edtValue.Size = new System.Drawing.Size(200, 21);
            this.edtValue.TabIndex = 1;
            this.edtValue.SizeChanged += new System.EventHandler(this.edtValue_SizeChanged);
            // 
            // OctoHelperV2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.edtValue);
            this.Controls.Add(this.edtKey);
            this.Name = "OctoHelperV2";
            this.Size = new System.Drawing.Size(301, 21);
            this.SizeChanged += new System.EventHandler(this.OctoHelperV2_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.edtKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtValue.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit edtKey;
        private DevExpress.XtraEditors.TextEdit edtValue;
    }
}
