using System;
using System.Windows.Forms;

namespace DK_WEIGHT.AutoWeight
{
    public partial class frmChkCAR : Form
    {
        public String CAR_NO1;
        public String CAR_NO2;
        public String RFID_SEQ;
        public String RFID_CODE;


        public frmChkCAR()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (CAR_NO1 != textBox1.Text)
            {
                // 배차 정보 업데이트
                //String VHEL_UPDATE = " UPDATE TB_WG02_0001 SET "
                //                    + "   VEHL_NO = '" + textBox1.Text + "' "
                //                    + " WHERE RFID_SEQ = '" + RFID_SEQ + "' ";
                //ServiceAdapter _svc = new ServiceAdapter();
                //_svc.SetQuery(VHEL_UPDATE);

                CAR_NO1 = textBox1.Text;
            }

            if (textBox2.Text.Trim() != textBox1.Text.Trim())
            {

                label4.Text = " 배차된 챠랑번호와 LPR 인식번호가 다릅니다";
                return;
            }

            if (radioButton1.Checked == true)
            {
                RFID_CODE = "1";
            }
            if (radioButton2.Checked == true)
            {
                RFID_CODE = "2";
            }
            if (radioButton3.Checked == true)
            {
                RFID_CODE = "3";
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(); 
        }

        private void frmChkCARcs_Load(object sender, EventArgs e)
        {
            textBox1.Text = CAR_NO1;
            textBox2.Text = CAR_NO2;

            if (CAR_NO2.Trim() == "?")
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
                radioButton3.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text;
        }

        private void btnConfirmCancel_Click(object sender, EventArgs e)
        {
            // 배차 정보 확정 취소
            String VHEL_UPDATE = " UPDATE TB_WG03_0001 SET "
                                + "   CONFIRM_YN = 'N', CONFIRM_DATE = NULL "
                                + " WHERE RFID_SEQ_LINK = '" + RFID_SEQ + "' ";
            ServiceAdapter _svc = new ServiceAdapter();
            _svc.SetQuery(VHEL_UPDATE);

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close(); 
        }
    }
}
