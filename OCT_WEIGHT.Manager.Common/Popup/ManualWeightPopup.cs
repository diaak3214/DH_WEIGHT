using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.info;

namespace OCT_WEIGHT.Manager.Common.Popup
{
    public partial class ManualWeightPopup : Form
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

        public string tPK_SCD_NO;
        public string tSHIP_GB;
        public string tORD_GB;
        public string tITEM_TYPE;
        public string tITEM_CODE;
        public string tVENDOR_ID;
        public string tTRS_CO_NO;

        public string tPLNT_NO;
        public string tPK_FST_NO;   //자재별PK1
        public string tITEM_NM;
        public string tDRV_TEL_NO;

        public string WGT;      //중량
        public string WGT_DT;   //시간
        public string tWGT;
        public string tWGT_DT;

        public string tSEQ_NO;
        public string tDIS_CLASS;
        public string tAW_SEQ;      //계량일련번호

        private string temp_crud = string.Empty;

        public ManualWeightPopup()
        {
            InitializeComponent();
        }

        private void InitInputLayout()
        {
        }

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Form Load

        private void ManualWeightPopup_Load(object sender, EventArgs e)
        {
            QueryClick2();
            txtMEA_DATE.Text = DateTime.Today.ToString("yyyy-MM-dd");	//현재날짜

            tWGT = WGT;
            tWGT_DT = WGT_DT;
            //txtCAR_NO.Text = CAR_NO;
            //txtVENDOR_NM.Text = VENDOR_NM;
            //txtDIS_CLASS.Text = DIS_CLASS;
            //txtDRV_NM.Text = DRV_NM;
            //txtMEA_DATE.Text = DRV_TEL_NO;
            //txtMEA_SEQ.Text = REMARK;

            //tPK_SCD_NO = PK_SCD_NO;
            //tSHIP_GB = SHIP_GB;
            //tORD_GB = ORD_GB;
            //tITEM_TYPE = ITEM_TYPE;
            //tITEM_CODE = ITEM_CODE;
            //tVENDOR_ID = VENDOR_ID;
            //tTRS_CO_NO = TRS_CO_NO;

            //tPLNT_NO = "1200";
            //tPK_FST_NO = PK_FST_NO;
            //tITEM_NM = ITEM_NM;
            //tDRV_TEL_NO = DRV_TEL_NO;
        }

        #endregion

        #region 기능키
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & ~(Keys.Shift | Keys.Control);

            switch (key)
            {
                case Keys.F8:
                    btnSave_Click(btnSave, new EventArgs());    //저장
                    return true;
                case Keys.F4:
                    btnCancel_Click(btnCancel, new EventArgs());//취소
                    return true;
                case Keys.Escape:
                    btnClose_Click(btnClose, new EventArgs());  //닫기
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
        //1차계량 조회
        public void QueryClick2()
        {
            try
            {
                String Query = null;

                //Query = " SELECT TO_CHAR(A.MEA_DATE,'YYYY-MM-DD') AS MEA_DATE, A.MEA_SEQ, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS IN_WGT_DT, A.CAR_NO, TO_CHAR(A.IN_WGT,'99,990') AS IN_WGT"
                //      + "       ,A.CARD_NO, NULL AS VENDOR_NM, A.DRV_NM, NULL AS DIS_CLASS, A.ITEM_NM, A.SEQ_NO"
                //      + "   FROM WMS_MEASURE_RST A"
                //      + "  WHERE A.PLNT_NO =  '1200'"
                //      //+ "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = TO_CHAR(SYSDATE,'YYYYMMDD')"
                //      + "    AND A.USE_YN = 'Y'"
                //      + "    AND A.IN_WGT_DT IS NOT NULL"
                //      + "    AND A.OUT_WGT_DT IS NULL"
                //      + "  ORDER BY A.MEA_SEQ"
                //      ;
                //DataSet ds = DBConn._ExecuteDataSet(Query);
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_MEA_DATE", DateTime.Today.ToString("yyyyMMdd"));
                dict.Add("P_PLNT_NO", clsUserInfo.Place);   //사업장_계량대    
                dict.Add("P_VEHL_NO", txtVEHL_NO.Text);     //검색조건에 차량번호 추가(2020-03-30 한민호)     
                DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_MANUAL_01_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdMaster2.DataSource = ds.Tables[0];
                    gvwMaster2.MoveBy(ds.Tables[0].Rows.Count); //포커스가 제일 마지막 행에 오게
                }
                else
                {
                    grdMaster2.DataSource = null;
                    InitInputLayout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (txtCARD_NO.Text == "")
            //{
            //    MessageBox.Show("카드번호를 입력하세요");
            //    txtCARD_NO.Focus();
            //    return;
            //}

            if (txtCAR_NO.Text == "")
            {
                MessageBox.Show("차량번호를 입력하세요");
                txtCAR_NO.Focus();
                return;
            }

            tDIS_CLASS = txtDIS_CLASS.Text;
            if (tDIS_CLASS == "")
            {
                MessageBox.Show("품명을 입력하세요");
                txtDIS_CLASS.Focus();
                return;
            }
            //품목구분 없이 계량(2020-03-25 한민호)
            //if (!(tDIS_CLASS == "원자재" || tDIS_CLASS == "수입원자재" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "부자재/저장품" || tDIS_CLASS == "수입부/저장품" || tDIS_CLASS == "철근판매"
            // || tDIS_CLASS == "가공철근" || tDIS_CLASS == "빌렛출하" || tDIS_CLASS == "이송출하" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "폐기물/부산물" || tDIS_CLASS == "자가고철"))
            //{
            //    MessageBox.Show("품명을 정확히 입력하세요(원자재, 수입원자재..)");
            //    txtDIS_CLASS.Focus();
            //    return;
            //}
            //if (txtIN_OUT.Text == "")
            //{
            //    MessageBox.Show("구분을 입력하세요");
            //    txtIN_OUT.Focus();
            //    return;
            //}

            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                if (btnOUT_WGT.Enabled == false)        //2차계량 비활성화면 1차계량
                {
                    //입문없는 수동계량 로직 추가(2020-02-28 한민호))
                    if (tPK_FST_NO == "" || tPK_FST_NO == null)
                        p.Add("P_FG", "C1");     //C1:입문없는1차계량
                    else
                        p.Add("P_FG", "U1");     //U1:1차계량
                }
                else if (btnIN_WGT.Enabled == false)    //1차계량 비활성화면 2차계량
                {
                    //입문없는 수동계량 로직 추가(2020-02-28 한민호))
                    if (tPK_FST_NO == "" || tPK_FST_NO == null)
                        p.Add("P_FG", "C2");     //C2:입문없는2차계량
                    else
                        p.Add("P_FG", "U2");     //U2:2차계량                      
                }

                p.Add("P_AW_SEQ", tAW_SEQ);
                p.Add("P_SEQ_NO", tSEQ_NO);
                p.Add("P_PLNT_NO", clsUserInfo.Place);
                p.Add("P_ITEM_CODE", tITEM_CODE);

                p.Add("P_CARD_NO", txtCARD_NO.Text);
                p.Add("P_CAR_NO", txtCAR_NO.Text);
                p.Add("P_VENDOR_NM", txtVENDOR_NM.Text);
                p.Add("P_ITEM_TYPE_NM", txtDIS_CLASS.Text);
                p.Add("P_ITEM_NM", txtITEM_NM.Text);
                p.Add("P_AGENT_NM", txtAGENT_NM.Text);
                p.Add("P_IN_WGT_DT", txtIN_WGT_DT.Text);
                p.Add("P_IN_WGT", txtIN_WGT.Text.Replace(",", ""));
                p.Add("P_OUT_WGT_DT", txtOUT_WGT_DT.Text);
                p.Add("P_OUT_WGT", txtOUT_WGT.Text.Replace(",", ""));
                p.Add("P_DRV_NM", txtDRV_NM.Text);

                p.Add("P_REG_ID", clsUserInfo.User_id);

                DBConn.ExecuteQuerySPR2("SP_MU_WEIGHT_MANUAL_S", p);
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
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void gvwMaster2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //2차계량 클릭 시 값 던져줌
            //try
            //{
            //    if (gvwMaster2.DataRowCount == 0)
            //        InitInputLayout();
            //    else if (btnOUT_WGT.Enabled == false)
            //        InitInputLayout();
            //    else
            //    {
            //        txtMEA_DATE.Text = gvwMaster2.GetFocusedRowCellValue("MEA_DATE").ToString();
            //        txtMEA_SEQ.Text = gvwMaster2.GetFocusedRowCellValue("MEA_SEQ").ToString();
            //        txtCAR_NO.Text = gvwMaster2.GetFocusedRowCellValue("CAR_NO").ToString();
            //        txtCARD_NO.Text = gvwMaster2.GetFocusedRowCellValue("CARD_NO").ToString();
            //        txtVENDOR_NM.Text = gvwMaster2.GetFocusedRowCellValue("VENDOR_NM").ToString();
            //        tDIS_CLASS = gvwMaster2.GetFocusedRowCellValue("DIS_CLASS").ToString();
            //        txtDIS_CLASS.Text = tDIS_CLASS;
            //        if (tDIS_CLASS == "원자재" || tDIS_CLASS == "수입원자재" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "부자재/저장품" || tDIS_CLASS == "수입부/저장품")
            //            txtIN_OUT.Text = "입고";
            //        else if (tDIS_CLASS == "철근판매" || tDIS_CLASS == "가공철근" || tDIS_CLASS == "빌렛출하" || tDIS_CLASS == "이송출하" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "폐기물/부산물" || tDIS_CLASS == "자가고철")
            //            txtIN_OUT.Text = "출고";

            //        txtITEM_NM.Text = gvwMaster2.GetFocusedRowCellValue("ITEM_NM").ToString();
            //        txtIN_WGT_DT.Text = gvwMaster2.GetFocusedRowCellValue("IN_WGT_DT").ToString();
            //        txtIN_WGT.Text = gvwMaster2.GetFocusedRowCellValue("IN_WGT").ToString().Replace(" ","");
            //        txtDRV_NM.Text = gvwMaster2.GetFocusedRowCellValue("DRV_NM").ToString();
            //    }
            //}
            //catch (Exception exception)
            //{

            //}
        }

        //구분 클릭 이벤트
        private void btnInOut_Click(object sender, EventArgs e)
        {
            if (txtIN_OUT.Text == "" || txtIN_OUT.Text == "출고")
                txtIN_OUT.Text = "입고";
            else
                txtIN_OUT.Text = "출고";

        }

        //취소
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnIN_WGT.Enabled = true;
            btnOUT_WGT.Enabled = true;

            txtCARD_NO.Properties.ReadOnly = true;
            txtCAR_NO.Properties.ReadOnly = true;
            txtVENDOR_NM.Properties.ReadOnly = true;
            txtDIS_CLASS.Properties.ReadOnly = true;
            txtITEM_NM.Properties.ReadOnly = true;
            txtDRV_NM.Properties.ReadOnly = true;

            txtMEA_SEQ.Text = "";
            txtCAR_NO.Text = "";
            txtCARD_NO.Text = "";
            txtVENDOR_NM.Text = "";
            txtDIS_CLASS.Text = "";
            txtITEM_NM.Text = "";

            txtIN_OUT.Text = "";
            txtIN_WGT_DT.Text = "";
            txtIN_WGT_DT.Properties.ReadOnly = true;    //계량대에서 던져준 값은 수정 못함
            txtIN_WGT.Text = "0";
            txtOUT_WGT_DT.Text = "";
            txtOUT_WGT_DT.Properties.ReadOnly = true;    //계량대에서 던져준 값은 수정 못함
            txtOUT_WGT.Text = "0";
            txtREAL_WGT.Text = "0";
            txtDRV_NM.Text = "";
        }

        //1차계량 클릭 이벤트
        private void btnIN_WGT_Click(object sender, EventArgs e)
        {
            btnOUT_WGT.Enabled = false;

            txtCARD_NO.Properties.ReadOnly = false;
            txtCAR_NO.Properties.ReadOnly = false;
            txtVENDOR_NM.Properties.ReadOnly = false;
            txtDIS_CLASS.Properties.ReadOnly = false;
            txtITEM_NM.Properties.ReadOnly = false;
            txtDRV_NM.Properties.ReadOnly = false;

            txtMEA_SEQ.Text = "";
            txtCAR_NO.Text = "";
            txtCARD_NO.Text = "";
            txtVENDOR_NM.Text = "";
            txtDIS_CLASS.Text = "";
            txtITEM_NM.Text = "";

            txtIN_WGT_DT.Text = tWGT_DT;
            txtIN_WGT_DT.Properties.ReadOnly = true;    //계량대에서 던져준 값은 수정 못함
            txtIN_WGT.Text = tWGT; ;
            txtDRV_NM.Text = "";

            txtCARD_NO.Focus();
        }

        //2차계량 클릭 이벤트
        private void btnOUT_WGT_Click(object sender, EventArgs e)
        {
            btnIN_WGT.Enabled = false;

            tAW_SEQ = gvwMaster2.GetFocusedRowCellValue("AW_SEQ").ToString();
            tSEQ_NO = gvwMaster2.GetFocusedRowCellValue("SEQ_NO").ToString();
            txtMEA_DATE.Text = gvwMaster2.GetFocusedRowCellValue("MEA_DATE").ToString();
            txtMEA_SEQ.Text = gvwMaster2.GetFocusedRowCellValue("MEA_SEQ").ToString();
            txtCAR_NO.Text = gvwMaster2.GetFocusedRowCellValue("CAR_NO").ToString();
            txtCARD_NO.Text = gvwMaster2.GetFocusedRowCellValue("CARD_NO").ToString();
            txtVENDOR_NM.Text = gvwMaster2.GetFocusedRowCellValue("VENDOR_NM").ToString();

            tDIS_CLASS = gvwMaster2.GetFocusedRowCellValue("DIS_CLASS").ToString();
            txtDIS_CLASS.Text = tDIS_CLASS;
            if (tDIS_CLASS == "원자재" || tDIS_CLASS == "수입원자재" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "부자재/저장품" || tDIS_CLASS == "수입부/저장품")
                txtIN_OUT.Text = "입고";
            else if (tDIS_CLASS == "철근판매" || tDIS_CLASS == "가공철근" || tDIS_CLASS == "빌렛출하" || tDIS_CLASS == "이송출하" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "폐기물/부산물" || tDIS_CLASS == "자가고철")
                txtIN_OUT.Text = "출고";
            //txtDIS_CLASS.Text = gvwMaster2.GetFocusedRowCellValue("DIS_CLASS").ToString();
            txtITEM_NM.Text = gvwMaster2.GetFocusedRowCellValue("ITEM_NM").ToString();

            txtIN_WGT_DT.Text = gvwMaster2.GetFocusedRowCellValue("IN_WGT_DT").ToString();
            txtIN_WGT.Text = gvwMaster2.GetFocusedRowCellValue("IN_WGT").ToString().Replace(" ", "");
            txtIN_WGT.Properties.ReadOnly = false;    
            txtOUT_WGT_DT.Text = tWGT_DT;
            txtOUT_WGT.Text = tWGT;
            txtOUT_WGT.Properties.ReadOnly = true;    //계량대에서 던져준 값은 수정 못함
            txtDRV_NM.Text = gvwMaster2.GetFocusedRowCellValue("DRV_NM").ToString();
            realweight_cal();   //실중량 계산 로직
            //자재별PK1 추가(2020-02-28 한민호))
            tPK_FST_NO = gvwMaster2.GetFocusedRowCellValue("PK_FST_NO").ToString();
        }

        //실중량 계산 로직
        private void realweight_cal()
        {
            if (txtIN_WGT.Text == "") return;
            if (txtOUT_WGT.Text == "") return;
            int tIN_WGT = Convert.ToInt32(txtIN_WGT.Text.Replace(",",""));
            int tOUT_WGT = Convert.ToInt32(txtOUT_WGT.Text.Replace(",", ""));
            if (tIN_WGT > 0 && tOUT_WGT > 0)
            {
                if (tDIS_CLASS == "원자재" || tDIS_CLASS == "수입원자재" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "부자재/저장품" || tDIS_CLASS == "수입부/저장품")
                    txtREAL_WGT.Text = txtsetting(Convert.ToString(tIN_WGT - tOUT_WGT));
                else if (tDIS_CLASS == "철근판매" || tDIS_CLASS == "가공철근" || tDIS_CLASS == "빌렛출하" || tDIS_CLASS == "이송출하" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "폐기물/부산물" || tDIS_CLASS == "자가고철")
                    txtREAL_WGT.Text = txtsetting(Convert.ToString(tOUT_WGT - tIN_WGT));
            }
        }

        #region 중량값 콤마 표시

        private string txtsetting(string data)
        {
            string ret = string.Empty;
            if (CheckingSpecialText(data) == false) // 숫자를 뺀 나머지 제외
            {
                if (data == "0")
                {
                    return data;
                }

                int cdata = Convert.ToInt32(data);
                ret = string.Format("{0:#,##0}", cdata);
            }
            else
            {
                ret = data;
            }

            return ret;
        }

        public bool CheckingSpecialText(string txt)
        {
            string str = @"[~!@\#$%^&*\()\=+|\\/:;?""<>']";
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(str);
            return rex.IsMatch(txt);
        }

        #endregion

        private void txtDIS_CLASS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                tDIS_CLASS = txtDIS_CLASS.Text;
                if (tDIS_CLASS.Length > 0)
                {
                    if (tDIS_CLASS == "원자재" || tDIS_CLASS == "수입원자재" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "부자재/저장품" || tDIS_CLASS == "수입부/저장품")
                        txtIN_OUT.Text = "입고";
                    else if (tDIS_CLASS == "철근판매" || tDIS_CLASS == "가공철근" || tDIS_CLASS == "빌렛출하" || tDIS_CLASS == "이송출하" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "폐기물/부산물" || tDIS_CLASS == "자가고철")
                        txtIN_OUT.Text = "출고";
                }
            }
        }

        private void txtCARD_NO_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCARD_NO.Text == "")
                return;

            try
            {
                if (e.KeyCode == Keys.Enter)
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
                    DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_MANUAL_R", dict);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //일련번호 추가(2020-02-24 한민호)
                        //txtSEQ_NO.Text = ds.Tables[0].Rows[0]["SEQ_NO"].ToString();

                        txtCAR_NO.Text = ds.Tables[0].Rows[0]["CAR_NO"].ToString();
                        txtVENDOR_NM.Text = ds.Tables[0].Rows[0]["VENDOR_NM"].ToString();
                        tDIS_CLASS = ds.Tables[0].Rows[0]["DIS_CLASS"].ToString();
                        txtDIS_CLASS.Text = tDIS_CLASS;
                        txtDRV_NM.Text = ds.Tables[0].Rows[0]["DRV_NM"].ToString();
                        txtITEM_NM.Text = ds.Tables[0].Rows[0]["ITEM_NM"].ToString();                        
                        if (tDIS_CLASS == "원자재" || tDIS_CLASS == "수입원자재" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "부자재/저장품" || tDIS_CLASS == "수입부/저장품")
                            txtIN_OUT.Text = "입고";
                        else if (tDIS_CLASS == "철근판매" || tDIS_CLASS == "가공철근" || tDIS_CLASS == "빌렛출하" || tDIS_CLASS == "이송출하" || tDIS_CLASS == "이송입고" || tDIS_CLASS == "폐기물/부산물" || tDIS_CLASS == "자가고철")
                            txtIN_OUT.Text = "출고";
                        tSEQ_NO = ds.Tables[0].Rows[0]["SEQ_NO"].ToString();
                        //자재별PK1 추가(2020-02-28 한민호))
                        tPK_FST_NO = ds.Tables[0].Rows[0]["PK_FST_NO"].ToString();
                    }
                    txtCARD_NO.SelectAll();
                }
            }
            catch
            {
                return;
            }
        }

        private void txtIN_WGT_EditValueChanged(object sender, EventArgs e)
        {
            realweight_cal();   //실중량 계산 로직
        }

        private void txtOUT_WGT_EditValueChanged(object sender, EventArgs e)
        {
            realweight_cal();   //실중량 계산 로직
        }

        private void txtIN_WGT_Leave(object sender, EventArgs e)
        {
            txtIN_WGT.Text = txtsetting(txtIN_WGT.Text);
        }

        private void txtVEHL_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryClick2();
            }
        }

    }
}