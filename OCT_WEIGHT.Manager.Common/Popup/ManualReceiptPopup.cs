using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.info;
using System.IO.Ports;


namespace OCT_WEIGHT.Manager.Common.Popup
{
    public partial class ManualReceiptPopup : Form
    {
        public string CAR_NO;
        public string VENDOR_NM;
        public string DIS_CLASS;
        public string DRV_NM;
        public string DRV_TEL_NO;
        public string REMARK;

        public string PK_SCD_NO;
        public string SHIP_GB;
        public string ORD_GB;
        public string ITEM_TYPE;
        public string ITEM_CODE;
        public string VENDOR_ID;
        public string TRS_CO_NO;

        public string PLNT_NO;
        public string PK_FST_NO;
        public string ITEM_NM;
        public string ITEM_TYPE_NM;
        public string ORD_WGT;
        public string MEA_DATE;
        public string SITE_NM;
        //변수
        public string tPK_SCD_NO;
        public string tSHIP_GB;
        public string tORD_GB;
        public string tITEM_TYPE;
        public string tITEM_CODE;
        public string tVENDOR_ID;
        public string tTRS_CO_NO;

        public string tPLNT_NO;
        public string tPK_FST_NO;
        public string tITEM_NM;
        public string tDRV_TEL_NO;
        public string tITEM_TYPE_NM;
        public string tORD_WGT;
        public string tMEA_DATE;
        public string tSITE_NM;

        private string temp_crud = string.Empty;

        //보안관리실 RFID카드 태그 시리얼방식 추가(2020-02-12 오창휘 수정)
        private SerialPort serialPort = new SerialPort();

        // RFID 사용 변수
        String HW_RFID_IP = "";
        String HW_RFID_PORT = "";

        public ManualReceiptPopup()
        {
            InitializeComponent();
        }

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Form Load

        private void ManualReceiptPopup_Load(object sender, EventArgs e)
        {
            tPLNT_NO = clsUserInfo.Place;

            //보안관리실 RFID카드 태그 시리얼방식 추가(2020-02-12 오창휘 수정) START
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("P_USER", clsUserInfo.User_id); // 사용자 아이디 
            DataSet ds = DBConn.ExecuteDataSet2("SP_SYSSERIALPORT_R", dict);
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    string[] portInfo = ds.Tables[0].Rows[0][0].ToString().Split('^');
                    serialPort = new SerialPort();
                    serialPort.PortName = portInfo[0];
                    serialPort.BaudRate = Convert.ToInt32(portInfo[1]);

                    if (portInfo[2] == "EVEN")
                        serialPort.Parity = Parity.Even;
                    else if (portInfo[2] == "MARK")
                        serialPort.Parity = Parity.Mark;
                    else if (portInfo[2] == "NONE")
                        serialPort.Parity = Parity.None;
                    else if (portInfo[2] == "ODD")
                        serialPort.Parity = Parity.Odd;
                    else if (portInfo[2] == "SPACE")
                        serialPort.Parity = Parity.Space;

                    serialPort.DataBits = Convert.ToInt32(portInfo[3]);
                    
                    if (portInfo[4] == "NONE")
                        serialPort.StopBits = StopBits.None;
                    else if (portInfo[4] == "ONE")
                        serialPort.StopBits = StopBits.One;
                    else if (portInfo[4] == "ONEPOINTFIVE")
                        serialPort.StopBits = StopBits.OnePointFive;
                    else if (portInfo[4] == "TWO")
                        serialPort.StopBits = StopBits.Two;

                    serialPort.Open();
                    timer_rfid.Enabled = true;
                }
                catch
                {
                    timer_rfid.Enabled = false;
                }
            }
            //보안관리실 RFID카드 태그 시리얼방식 추가(2020-02-12 오창휘 수정) END
        }

        #endregion

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (btnSave.CRUD_type != CRUDType.C) return;

            if (txtCARD_NO.Text == "")
            {
                MessageBox.Show("카드번호를 입력하세요");
                txtCARD_NO.Focus();
                return;
            }

            if (txtCARD_NO.Text.Length != 5)
            {
                MessageBox.Show("카드번호가 잘못 되었습니다.");
                txtCARD_NO.Focus();
                return;
            }

            int i = 0;
            bool result = int.TryParse(txtCARD_NO.Text, out i);
            if (result == false)
            {
                MessageBox.Show("카드번호가 잘못 되었습니다.(문자)");
                txtCARD_NO.Focus();
                return;
            }

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("P_RFID_CARD", txtCARD_NO.Text);
            dict.Add("P_ITEM_TYPE_NM", tITEM_TYPE_NM);  //품목유형(품명)
            dict.Add("P_PK_FST_NO", tPK_FST_NO);        //자재별PK1
            dict.Add("P_PLNT_NO", tPLNT_NO);            //입고공장(1100:신평, 1200:녹산정문, 1210:가공공장, 1300:평택)
            DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_CARD_CHK", dict);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("해당 카드번호는 현재 사용중입니다.");
                txtCARD_NO.Focus();
                return;
            }


            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", "C");

                p.Add("P_SEQ_NO", "");
                //WMS_PLNT_NO 에서 가져가게 수정_로그인이 1210 이라도 1200으로 변환(2020-04-20 한민호)
                if (tPLNT_NO == "")
                {
                    if (clsUserInfo.Place == "1210")
                        p.Add("P_PLNT_NO", "1200");
                    else
                        p.Add("P_PLNT_NO", clsUserInfo.Place);
                }
                else
                    p.Add("P_PLNT_NO", tPLNT_NO);
                //p.Add("P_PLNT_NO", clsUserInfo.Place);
                p.Add("P_MEA_DATE", DateTime.Today.ToString("yyyyMMdd"));
                p.Add("P_PK_FST_NO", null);
                p.Add("P_PK_SCD_NO", null);

                p.Add("P_SHIP_GB", null);
                p.Add("P_ORD_GB", null);
                p.Add("P_ITEM_TYPE", null);
                p.Add("P_ITEM_TYPE_NM", txtDIS_CLASS.Text);
                p.Add("P_ITEM_CODE", null);

                p.Add("P_ITEM_NM", null);
                p.Add("P_CARD_NO", txtCARD_NO.Text);
                p.Add("P_TRS_CO_NO", null);
                p.Add("P_VENDOR_ID", null);
                p.Add("P_VENDOR_NM", txtVENDOR_NM.Text);

                p.Add("P_CAR_NO", txtCAR_NO.Text);
                p.Add("P_DRV_NM", txtDRV_NM.Text);
                p.Add("P_DRV_TEL_NO", txtDRV_TEL_NO.Text);
                p.Add("P_ORD_WGT", null);

                p.Add("P_REG_ID", clsUserInfo.User_id);
                p.Add("P_REMARK", txtREMARK.Text);
                p.Add("P_SITE_NM", null);

                p.Add("P_NON_REQ_RAASON", txtNON_REQ_RAASON.Text);
                p.Add("P_PERSON_CHARGE", txtPERSON_CHARGE.Text);

                DBConn.ExecuteQuerySPR2("SP_MU_WEIGHT_MANUAL_RECEPT_S", p);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.Close();
            }
        }

        #endregion

        #region 기능키
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & ~(Keys.Shift | Keys.Control);

            switch (key)
            {
                case Keys.F6:
                    btnSave_Click(btnSave, new EventArgs());    //저장
                    return true;
                case Keys.Escape:
                    btnClose_Click(btnClose, new EventArgs());  //닫기
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCARD_NO_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCARD_NO.Text == "")
                return;
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //카드번호 엔터 메서드 추가(2020-02-14 오창휘 수정)
                    enterCard();
                    //string ManualRfid = txtCARD_NO.Text; //RFID 풀번호
                    //string RfidNo = "";
                    //if (ManualRfid.Length == 8)
                    //{
                    //    RfidNo = ManualRfid.Substring(3, 5);
                    //    txtCARD_NO.Text = RfidNo;
                    //}
                    //txtCARD_NO.SelectAll();
                }
            }
            catch
            {
                return;
            }
        }

        //카드번호 엔터 메서드 추가(2020-02-14 오창휘 수정)
        private void enterCard()
        {
            string ManualRfid = txtCARD_NO.Text; //RFID 풀번호
            string RfidNo = "";
            if (ManualRfid.Length == 8)
            {
                RfidNo = ManualRfid.Substring(3, 5);
                txtCARD_NO.Text = RfidNo;
            }
            txtCARD_NO.SelectAll();
        }

        private void timer_rfid_Tick(object sender, EventArgs e)
        {
            if (txtCARD_NO.Text.Length > 0)
                return;

            timer_rfid.Enabled = false;

            try
            {
                if (serialPort.IsOpen)
                {
                    string sRead = serialPort.ReadExisting();
                    serialPort.DiscardInBuffer();

                    sRead = sRead.ToUpper();    // 전체 대문자로 변경

                    txtCARD_NO.Text = sRead.Substring(1,sRead.Length-2);

                    if (txtCARD_NO.Text.Length > 0)
                        enterCard();
                }
            }
            catch
            {
            }
            timer_rfid.Enabled = true;
        }

        private void ManualReceiptPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort.Close();
            serialPort.Dispose();
        }

    }
}