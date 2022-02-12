using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.info;
using System.IO.Ports;

namespace OCT_WEIGHT.Manager.Common.Popup
{
    public partial class ReturnPopup : Form
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

        public string SEQ_NO;
        public string CARD_NO;


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

        public string tSEQ_NO;

        private string temp_crud = string.Empty;

        //보안관리실 RFID카드 태그 시리얼방식 추가(2020-02-12 오창휘 수정)
        private SerialPort serialPort = new SerialPort();

        public ReturnPopup()
        {
            InitializeComponent();
        }

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Form Load

        private void ReturnPopup_Load(object sender, EventArgs e)
        {
            txtCAR_NO.Text = CAR_NO;
            txtVENDOR_NM.Text = VENDOR_NM;
            txtDIS_CLASS.Text = DIS_CLASS;
            txtDRV_NM.Text = DRV_NM;
            txtDRV_TEL_NO.Text = DRV_TEL_NO;
            txtREMARK.Text = REMARK;

            txtCARD_NO.Text = CARD_NO;
            if (CARD_NO != "")  //카드번호가 있으면 비고 번호 수정
            {
                txtDRV_TEL_NO.Properties.ReadOnly = false;
                txtREMARK.Properties.ReadOnly = false;
                txtDRV_TEL_NO.Focus();
            }

            tPK_SCD_NO = PK_SCD_NO;
            tSHIP_GB = SHIP_GB;
            tORD_GB = ORD_GB;
            tITEM_TYPE = ITEM_TYPE;
            tITEM_CODE = ITEM_CODE;
            tVENDOR_ID = VENDOR_ID;
            tTRS_CO_NO = TRS_CO_NO;

            tPLNT_NO = "1200";
            tPK_FST_NO = PK_FST_NO;
            tITEM_NM = ITEM_NM;
            tDRV_TEL_NO = DRV_TEL_NO;

            tSEQ_NO = SEQ_NO;

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
            if (txtCARD_NO.Text == "")
            {
                MessageBox.Show("카드번호를 입력하세요");
                txtCARD_NO.Focus();
                return;
            }

            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                //String Query = "UPDATE WMS_MEASURE_RST SET REMARK = '" + txtREMARK.Text + "', DRV_TEL_NO = '" + txtDRV_TEL_NO.Text + "' WHERE SEQ_NO = '" + tSEQ_NO + "' ";
                //DBConn._ExecuteDataSet(Query);
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", "U"); //U:수정

                p.Add("P_SEQ_NO", tSEQ_NO);
                p.Add("P_PLNT_NO", clsUserInfo.Place);
                p.Add("P_MEA_DATE", "");
                p.Add("P_PK_FST_NO", tPK_FST_NO);
                p.Add("P_PK_SCD_NO", tPK_SCD_NO);

                p.Add("P_SHIP_GB", tSHIP_GB);
                p.Add("P_ORD_GB", tORD_GB);
                p.Add("P_ITEM_TYPE", tITEM_TYPE);
                p.Add("P_ITEM_TYPE_NM", "");
                //p.Add("P_ITEM_TYPE_NM", txtUSER_CD.Text);
                p.Add("P_ITEM_CODE", tITEM_CODE);

                p.Add("P_ITEM_NM", tITEM_NM);
                p.Add("P_CARD_NO", txtCARD_NO.Text);
                p.Add("P_TRS_CO_NO", tTRS_CO_NO);
                p.Add("P_VENDOR_ID", tVENDOR_ID);
                p.Add("P_VENDOR_NM", "");
                //p.Add("P_VENDOR_NM", txtUSER_CD.Text);

                p.Add("P_CAR_NO", txtCAR_NO.Text);
                p.Add("P_DRV_NM", txtDRV_NM.Text);
                p.Add("P_DRV_TEL_NO", txtDRV_TEL_NO.Text);
                p.Add("P_ORD_WGT", "");
                p.Add("P_REG_ID", clsUserInfo.User_id);
                p.Add("P_REMARK", txtREMARK.Text);
                //납품처명 추가(2020-03-12 한민호)
                p.Add("P_SITE_NM", "");

                DBConn.ExecuteQuerySPR2("SP_MU_WEIGHT_RECEPT_S", p);
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

        //카드반납 조회

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

                    ////2019-12-21 대한제강
                    ////String Query = " SELECT A.CAR_NO, '' AS VENDOR_NM, '' AS DIS_CLASS, A.DRV_NM, A.DRV_TEL_NO, A.REMARK"
                    ////             + "       ,A.SEQ_NO"
                    ////             + "   FROM WMS_MEASURE_RST A"
                    ////             + "  WHERE A.PLNT_NO =  '1200'"
                    ////             //임시(2019-12-31 한민호)
                    ////             + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '20191226'"
                    ////             //+ "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = TO_CHAR(SYSDATE,'YYYYMMDD')"
                    ////             + "    AND A.CARD_NO = '" + txtCARD_NO.Text + "'"
                    ////             + "    AND A.CARD_RTN_DT IS NULL"
                    ////             + "    AND A.IN_WGT_DT IS NOT NULL"
                    ////             ;
                    ////DataSet ds = DBConn._ExecuteDataSet(Query);
                    //Dictionary<string, string> dict = new Dictionary<string, string>();
                    //dict.Add("P_MEA_DATE", "");
                    //dict.Add("P_PLNT_NO", clsUserInfo.Place);   //사업장_계량대    
                    //dict.Add("P_CARD_NO", txtCARD_NO.Text);     //카드번호    
                    //DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_RETURN_R", dict);
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    txtCAR_NO.Text = ds.Tables[0].Rows[0]["CAR_NO"].ToString();
                    //    txtVENDOR_NM.Text = ds.Tables[0].Rows[0]["VENDOR_NM"].ToString();
                    //    txtDIS_CLASS.Text = ds.Tables[0].Rows[0]["DIS_CLASS"].ToString();
                    //    txtDRV_NM.Text = ds.Tables[0].Rows[0]["DRV_NM"].ToString();
                    //    txtDRV_TEL_NO.Text = ds.Tables[0].Rows[0]["DRV_TEL_NO"].ToString();
                    //    txtREMARK.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                    //    tSEQ_NO = ds.Tables[0].Rows[0]["SEQ_NO"].ToString();
                    //}
                    //txtCARD_NO.SelectAll();
                }
            }
            catch
            {
                return;
            }
        }

        //카드반납
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (txtCARD_NO.Text == "")
            {
                MessageBox.Show("카드번호를 입력하세요");
                txtCARD_NO.Focus();
                return;
            }

            //카드반납은 1차 계량 후 에는 불가(2020-03-04 한민호)
            
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("P_MEA_DATE", "");
            dict.Add("P_PLNT_NO", clsUserInfo.Place);   //사업장_계량대    
            dict.Add("P_CARD_NO", txtCARD_NO.Text);     //카드번호    
            DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_RETURN_R", dict);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OUT_WGT_DT"].ToString() == "")  //2차계량
                {
                    MessageBox.Show("1차계량 후에는 카드반납 불가");
                    txtCARD_NO.Focus();
                    return;
                }
            }

            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", "R"); // R:반납

                p.Add("P_SEQ_NO", tSEQ_NO);
                p.Add("P_PLNT_NO", clsUserInfo.Place);
                p.Add("P_MEA_DATE", "");
                p.Add("P_PK_FST_NO", tPK_FST_NO);
                p.Add("P_PK_SCD_NO", tPK_SCD_NO);

                p.Add("P_SHIP_GB", tSHIP_GB);
                p.Add("P_ORD_GB", tORD_GB);
                p.Add("P_ITEM_TYPE", tITEM_TYPE);
                p.Add("P_ITEM_TYPE_NM", "");
                //p.Add("P_ITEM_TYPE_NM", txtUSER_CD.Text);
                p.Add("P_ITEM_CODE", tITEM_CODE);

                p.Add("P_ITEM_NM", tITEM_NM);
                p.Add("P_CARD_NO", txtCARD_NO.Text);
                p.Add("P_TRS_CO_NO", tTRS_CO_NO);
                p.Add("P_VENDOR_ID", tVENDOR_ID);
                p.Add("P_VENDOR_NM", "");
                //p.Add("P_VENDOR_NM", txtUSER_CD.Text);

                p.Add("P_CAR_NO", txtCAR_NO.Text);
                p.Add("P_DRV_NM", txtDRV_NM.Text);
                p.Add("P_DRV_TEL_NO", txtDRV_TEL_NO.Text);
                p.Add("P_ORD_WGT", "");

                p.Add("P_REG_ID", clsUserInfo.User_id);
                p.Add("P_REMARK", txtREMARK.Text);
                //납품처명 추가(2020-03-12 한민호)
                p.Add("P_SITE_NM", "");

                DBConn.ExecuteQuerySPR2("SP_MU_WEIGHT_RECEPT_S", p);
                //String Query = "UPDATE WMS_MEASURE_RST SET CARD_RTN_DT = SYSDATE WHERE SEQ_NO = '" + tSEQ_NO + "' ";
                //DBConn._ExecuteDataSet(Query);
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

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("P_MEA_DATE", "");
            dict.Add("P_PLNT_NO", clsUserInfo.Place);   //사업장_계량대    
            dict.Add("P_CARD_NO", txtCARD_NO.Text);     //카드번호    
            DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_RETURN_R", dict);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCAR_NO.Text = ds.Tables[0].Rows[0]["CAR_NO"].ToString();
                txtVENDOR_NM.Text = ds.Tables[0].Rows[0]["VENDOR_NM"].ToString();
                txtDIS_CLASS.Text = ds.Tables[0].Rows[0]["DIS_CLASS"].ToString();
                txtDRV_NM.Text = ds.Tables[0].Rows[0]["DRV_NM"].ToString();
                txtDRV_TEL_NO.Text = ds.Tables[0].Rows[0]["DRV_TEL_NO"].ToString();
                txtREMARK.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                tSEQ_NO = ds.Tables[0].Rows[0]["SEQ_NO"].ToString();
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

                    txtCARD_NO.Text = sRead.Substring(1, sRead.Length - 2);

                    if (txtCARD_NO.Text.Length > 0)
                        enterCard();
                }
            }
            catch
            {
            }
            timer_rfid.Enabled = true;
        }

        private void ReturnPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort.Close();
            serialPort.Dispose();
        }


    }
}