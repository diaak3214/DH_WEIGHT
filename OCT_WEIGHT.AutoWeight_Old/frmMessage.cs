using System;
using System.Windows.Forms;

namespace DK_WEIGHT.AutoWeight
{
    public partial class frmMessage : Form
    {
        public String Message;

        public frmMessage()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // 확인
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 취소
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close(); 
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            label1.Text = Message; 
        }
    }
}
