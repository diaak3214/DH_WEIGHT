using System;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace OCT_WEIGHT.Manager.Common.Control
{
    public partial class OctoButton : SimpleButton
    {
        private System.Drawing.Color defColor;
        public OctoButton()
        {
            InitializeComponent();
            UserInitialize();
        }

        public OctoButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            UserInitialize();
        }

        private void UserInitialize()
        {
            defColor = this.BackColor;
            this.Font = new System.Drawing.Font("나눔고딕", 9);
        }
        private string _AllowYn = "Y";
        public string AllowYn
        {
            get { return _AllowYn; }
            set { _AllowYn = value; }
        }

        private OCT_WEIGHT.Manager.Common.info.CRUDType _crud = OCT_WEIGHT.Manager.Common.info.CRUDType.R;
        public OCT_WEIGHT.Manager.Common.info.CRUDType CRUD_type
        {
            get { return _crud; }
            set { _crud = value; }
        }
        protected override void OnClick(EventArgs e)
        {
            if (this.AllowYn == "N")
            {
                System.Windows.Forms.MessageBox.Show("권한이 없습니다");
                return;
            }
            base.OnClick(e);
        }

    }
}
