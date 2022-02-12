using System;
using System.Windows.Forms;

namespace OCT_WEIGHT.AutoWeight
{
    public partial class frmMessage : Form
    {
        public String Message;

        public frmMessage()
        {
            InitializeComponent();
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            label1.Text = Message; 
        }

        private void simpleButton1_DoubleClick(object sender, EventArgs e)
        {
            // 확인
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(); 
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            // 취소
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close(); 
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void simpleButton1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close(); 
            }
        }
    }
}
