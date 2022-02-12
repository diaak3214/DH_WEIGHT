using System;
using System.Windows.Forms;

namespace OCT_WEIGHT.AutoWeight
{
    public partial class RedWindow : DevExpress.XtraEditors.XtraForm
    {
        private int timer = 0;
        public string ret = String.Empty;
        public string msg = String.Empty;
        public RedWindow()
        {
            InitializeComponent();
        }

        public void timer1_kill()
        {
            if (timer == 10)
            {
                timer1.Enabled = false;
            }
        }

        private void RedWindow_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Close();
        }

        private void RedWindow_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            timer1.Enabled = true;
            
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.Opacity == 0.7)
                {
                    this.Opacity = 0;
                }
                else
                {
                    this.Opacity = 0.7;
                }
                timer++;
                labelControl1.Text = msg;
                if (timer == 6)
                {
                    timer1.Enabled = false;
                    this.Close();
                }
            }
            catch
            {
            }

        }

        private void RedWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1_kill();
        }
    }
}