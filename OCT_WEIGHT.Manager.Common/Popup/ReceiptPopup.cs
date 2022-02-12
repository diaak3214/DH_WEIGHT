using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.info;
using System.IO.Ports;


namespace OCT_WEIGHT.Manager.Common.Popup
{
    public partial class ReceiptPopup : Form
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
        //출입관리 추가(2020-05-07 한민호)
        public string FRM_CLASS;   //차량접수, 출입해제 구분
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
        //출입관리 추가(2020-05-07 한민호)
        public string tSEQ_NO;
        public string tFRM_CLASS;   //차량접수, 출입해제 구분

        private string temp_crud = string.Empty;

        //보안관리실 RFID카드 태그 시리얼방식 추가(2020-02-12 오창휘 수정)
        private SerialPort serialPort = new SerialPort();

        // RFID 사용 변수
        String HW_RFID_IP = "";
        String HW_RFID_PORT = "";

        public ReceiptPopup()
        {
            InitializeComponent();
        }

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Form Load

        private void ReceiptPopup_Load(object sender, EventArgs e)
        {
            //출입관리 추가(2020-05-07 한민호)     
            if (CAR_NO == null || CAR_NO == "")
                txtCAR_NO.Text = "";
            else
                txtCAR_NO.Text = CAR_NO.Substring(CAR_NO.Length-4) ; //오른쪽 4자리 짜르기
            //txtCAR_NO.Text = CAR_NO;
            txtVENDOR_NM.Text = VENDOR_NM;
            txtDIS_CLASS.Text = DIS_CLASS;
            txtDRV_NM.Text = DRV_NM;
            txtDRV_TEL_NO.Text = DRV_TEL_NO;
            txtREMARK.Text = REMARK;

            tPK_SCD_NO = PK_SCD_NO;
            tSHIP_GB = SHIP_GB;
            tORD_GB = ORD_GB;
            tITEM_TYPE = ITEM_TYPE;
            tITEM_CODE = ITEM_CODE;
            tVENDOR_ID = VENDOR_ID;
            tTRS_CO_NO = TRS_CO_NO;

            //WMS_PLNT_NO 에서 가져가게 수정(2020-04-20 한민호)
            tPLNT_NO = PLNT_NO;
            //tPLNT_NO = "1200";
            tPK_FST_NO = PK_FST_NO;
            tITEM_NM = ITEM_NM;
            //품명구분 추가(2020-04-20 한민호)
            txtITEM_NM.Text = ITEM_NM;

            tDRV_TEL_NO = DRV_TEL_NO;
            tITEM_TYPE_NM = ITEM_TYPE_NM;
            tORD_WGT = ORD_WGT;
            tMEA_DATE = MEA_DATE;

            tSITE_NM = SITE_NM;

            if (FRM_CLASS == "2")
            {
                btnEnterClear.Visible = true;
                btnSave.Enabled = false;
            }
            else
            {
                btnEnterClear.Visible = false;
                btnSave.Enabled = true;
            }

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

            //출입관리 추가(2020-05-07 한민호)
            if (DIS_CLASS == "일반방문")
            {
                //txtCAR_NO.Enabled = true;
                this.txtCAR_NO.Properties.ReadOnly = false;
                //txtDRV_NM.Enabled = true;
                this.txtDRV_NM.Properties.ReadOnly = false;
                //txtDRV_TEL_NO.Enabled = true;
                this.txtDRV_TEL_NO.Properties.ReadOnly = false;
                btnEnterClear.Visible = true;
            }
            else
            {
                //txtCAR_NO.Enabled = false;
                this.txtCAR_NO.Properties.ReadOnly = true;
                //txtDRV_NM.Enabled = false;
                this.txtDRV_NM.Properties.ReadOnly = true;
                //txtDRV_TEL_NO.Enabled = false;
                this.txtDRV_TEL_NO.Properties.ReadOnly = true;
                btnEnterClear.Visible = false;
            }
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

            if (txtCAR_NO.Text == "")
            {
                MessageBox.Show("차량번호를 입력하세요");
                txtCAR_NO.Focus();
                return;
            }
            if (txtCAR_NO.Text.Length > 11)
            {
                MessageBox.Show("차량번호는 11자리 까지 가능합니다.");
                txtCAR_NO.Focus();
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
                //String Query = "INSERT INTO WMS_MEASURE_RST (SEQ_NO, PLNT_NO, MEA_DATE, MEA_SEQ, PK_FST_NO "
                //              + "                           ,PK_SCD_NO, SHIP_GB, ORD_GB, ITEM_TYPE, ITEM_CODE "
                //              + "                           ,ITEM_NM, CARD_NO, TRS_CO_NO, VENDOR_ID, CAR_NO "
                //              + "                           ,DRV_NM, DRV_TEL_NO, CARD_REG_DT, REG_ID, REG_DT)"
                //              + "                     SELECT (SELECT MAX(SEQ_NO)+1 FROM WMS_MEASURE_RST), '" + tPLNT_NO + "' ,SYSDATE "
                //              + "                           ,(SELECT NVL(MAX(MEA_SEQ),0)+1 FROM WMS_MEASURE_RST WHERE SUBSTR('1200',1,2) = '" + tPLNT_NO.Substring(0, 2) + "' AND TO_CHAR(MEA_DATE,'YYYY-MM-DD') = TO_CHAR(SYSDATE,'YYYY-MM-DD')) "
                //              + "                           ,'" + tPK_FST_NO + "' "
                //              + "                           ,'" + tPK_SCD_NO + "' ,'" + tSHIP_GB + "' ,'" + tORD_GB + "' ,'" + tITEM_TYPE + "' ,'" + tITEM_CODE + "' "
                //              + "                           ,'" + tITEM_NM + "' ,'" + txtCARD_NO.Text + "' ,'" + tTRS_CO_NO + "' ,'" + tVENDOR_ID + "' ,'" + txtCAR_NO.Text + "' "
                //              + "                           ,'" + txtDRV_NM.Text + "' ,'" + tDRV_TEL_NO + "' ,SYSDATE, 'octosys' ,SYSDATE "
                //              + "                       FROM DUAL ";
                //DBConn._ExecuteDataSet(Query);
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
                p.Add("P_MEA_DATE", tMEA_DATE);
                p.Add("P_PK_FST_NO", tPK_FST_NO);
                p.Add("P_PK_SCD_NO", tPK_SCD_NO);

                p.Add("P_SHIP_GB", tSHIP_GB);
                p.Add("P_ORD_GB", tORD_GB);
                p.Add("P_ITEM_TYPE", tITEM_TYPE);
                p.Add("P_ITEM_TYPE_NM", tITEM_TYPE_NM);
                p.Add("P_ITEM_CODE", tITEM_CODE);

                p.Add("P_ITEM_NM", tITEM_NM);
                p.Add("P_CARD_NO", txtCARD_NO.Text);
                p.Add("P_TRS_CO_NO", tTRS_CO_NO);
                p.Add("P_VENDOR_ID", tVENDOR_ID);
                p.Add("P_VENDOR_NM", txtVENDOR_NM.Text);

                p.Add("P_CAR_NO", txtCAR_NO.Text);
                p.Add("P_DRV_NM", txtDRV_NM.Text);
                p.Add("P_DRV_TEL_NO", tDRV_TEL_NO);
                p.Add("P_ORD_WGT", tORD_WGT);

                p.Add("P_REG_ID", clsUserInfo.User_id);
                p.Add("P_REMARK", txtREMARK.Text);

                p.Add("P_SITE_NM", tSITE_NM);

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

            //출입관리 추가(2020-05-07 한민호)
            if (txtDIS_CLASS.Text == "일반방문")
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_MEA_DATE", "");
                dict.Add("P_PLNT_NO", clsUserInfo.Place);   //사업장_계량대    
                dict.Add("P_CARD_NO", txtCARD_NO.Text);     //카드번호    
                DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_RETURN_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCAR_NO.Text = ds.Tables[0].Rows[0]["CAR_NO"].ToString();
                    txtVENDOR_NM.Text = ds.Tables[0].Rows[0]["VENDOR_NM"].ToString();
                    //txtDIS_CLASS.Text = ds.Tables[0].Rows[0]["DIS_CLASS"].ToString();
                    txtITEM_NM.Text = ds.Tables[0].Rows[0]["ITEM_NM"].ToString();
                    txtDRV_NM.Text = ds.Tables[0].Rows[0]["DRV_NM"].ToString();
                    txtDRV_TEL_NO.Text = ds.Tables[0].Rows[0]["DRV_TEL_NO"].ToString();
                    txtREMARK.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                    tSEQ_NO = ds.Tables[0].Rows[0]["SEQ_NO"].ToString();
                }
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

        private void ReceiptPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort.Close();
            serialPort.Dispose();
        }

        //출입관리 추가(2020-05-07 한민호)
        private void btnEnterClear_Click(object sender, EventArgs e)
        {
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

            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", "E"); // E:출입해제

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

    }
}