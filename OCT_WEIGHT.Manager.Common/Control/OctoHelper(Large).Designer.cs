namespace OCT_WEIGHT.Manager.Common.Control
{
    partial class OctoHelper_Large_
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.edtKey = new DevExpress.XtraEditors.ButtonEdit();
            this.edtValue = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.edtKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // edtKey
            // 
            this.edtKey.Dock = System.Windows.Forms.DockStyle.Left;
            this.edtKey.Location = new System.Drawing.Point(0, 0);
            this.edtKey.Name = "edtKey";
            this.edtKey.Properties.Appearance.BorderColor = System.Drawing.Color.Silver;
            this.edtKey.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.edtKey.Properties.Appearance.Options.UseBorderColor = true;
            this.edtKey.Properties.Appearance.Options.UseFont = true;
            this.edtKey.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.edtKey.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 20, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.edtKey.Size = new System.Drawing.Size(131, 32);
            this.edtKey.TabIndex = 2;
            this.edtKey.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.edtKey_ButtonClick);
            this.edtKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edtKey_KeyDown);
            // 
            // edtValue
            // 
            this.edtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtValue.EditValue = "";
            this.edtValue.Location = new System.Drawing.Point(131, 0);
            this.edtValue.Name = "edtValue";
            this.edtValue.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.edtValue.Properties.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.edtValue.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.edtValue.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.edtValue.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.edtValue.Properties.Appearance.Options.UseBackColor = true;
            this.edtValue.Properties.Appearance.Options.UseBorderColor = true;
            this.edtValue.Properties.Appearance.Options.UseFont = true;
            this.edtValue.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.edtValue.Properties.AppearanceReadOnly.BackColor2 = System.Drawing.Color.White;
            this.edtValue.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Silver;
            this.edtValue.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 15F);
            this.edtValue.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.edtValue.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.edtValue.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.edtValue.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.edtValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.edtValue.Properties.ReadOnly = true;
            this.edtValue.Size = new System.Drawing.Size(262, 32);
            this.edtValue.TabIndex = 4;
            // 
            // OctoHelper_Large_
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.edtValue);
            this.Controls.Add(this.edtKey);
            this.Name = "OctoHelper_Large_";
            this.Size = new System.Drawing.Size(393, 33);
            ((System.ComponentModel.ISupportInitialize)(this.edtKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtValue.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit edtKey;
        private DevExpress.XtraEditors.TextEdit edtValue;
    }
}
