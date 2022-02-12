using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.util;
using OCT_WEIGHT.Manager.Common;
using DevExpress.XtraEditors;

using System.Drawing;
using OCT_WEIGHT.Manager.Common.Popup;
using OCT_WEIGHT.Manager.Common.info;

//SMS전송을 위해 SQL연결(2020-02-20 한민호)
using System.Data.SqlClient;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class FrmWeightList : OCT_WEIGHT.Manager.Common.FrmBase
    {
        DataTable dt = new DataTable();
        DataRow row = null;
        public FrmWeightList()
        {
            InitializeComponent();
        }

        #region 입력화면초기화

        private void InitInputLayout()
        {
        }

        #endregion
        #region New

        private void BtnNew_Click(object sender, EventArgs e)
        {
            //txtSCHANM.Text = string.Empty;
            //txtCEO.Text = string.Empty;
            //txtAddr.Text = string.Empty;
            //START_DATE.DateTime = DateTime.Now;
        }

        private void SelectUpper(LookUpEdit LookUp, string Value, string Display, string fg, string sp_name,
            string colnm)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                DataSet ds = null;
                Dictionary<string, string> dict = new Dictionary<string, string>();

                if (colnm.Length > 0)
                {
                    dict.Add(colnm, fg);
                    ds = DBConn.ExecuteDataSet2(sp_name, dict);
                }
                else
                {
                    ds = DBConn._ExDataSet(sp_name);
                }

                DataTable dt = ds.Tables[0];

                //룩업 바인딩은 Datatable 로 해야 바인딤 됨. 
                LookUp.Properties.DataSource = dt;

                LookUp.Properties.DisplayMember = Display;
                LookUp.Properties.ValueMember = Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        #endregion

        #region Search

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            //try
            //{
            //    Dictionary<string, string> dict = new Dictionary<string, string>();
            //    dict.Add("V_SCHA_NAME", txtSCHANM.Text.Trim());
            //    //dict.Add("V_SCHA_DPJ", txtCEO.Text.Trim());
            //    //dict.Add("V_SCHA_ADDR", txtAddr.Text.Trim());
            //    //dict.Add("V_SCHA_DATE", START_DATE.Text.Trim());
            //    //dict.Add("V_CARRY_CD", leSchaFlag.GetColumnValue("CODE").ToString());
            //    //올바른 SP명으로 수정(2019-10-05 한민호)
            //    DataSet ds = DBConn.ExecuteDataSet2("SP_FrmWeightList_R", dict);
            //    //DataSet ds = DBConn.ExecuteDataSet2("SP_TBWS03_0004_R", dict);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        grdScha.DataSource = ds.Tables[0];
            //    }
            //    else
            //    {
            //        MsgBoxUtil.AlertInformation("조회된 자료가 없습니다.");
            //        grdScha.DataSource = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("차량 조회중 ERROR:" + ex.Message.ToString().Trim());
            //}
            //finally
            //{
            //    gvwScha.BestFitColumns();
            //}
        }

        #endregion

        #region Close

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Excel

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            //if (gvwScha.RowCount == 0) return;
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //    //바탕화면 경로 + 화면명 
            //    gvwScha.ExportToXls(desktop + "\\" + this.Text + ".xls");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    BtnSearch.PerformClick();
            //    this.Cursor = Cursors.Default;
            //}
        }

        #endregion

        #region Save

        private void BtnSave_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Load

        private void FrmWeightList_Load(object sender, EventArgs e)
        {
            btnReceipt.Enabled = true;      //차량접수
            btnCarCall.Enabled = false;     //차량호출
            btnRemark.Enabled = false;      //비고수정

            btnItemType1.ForeColor = Color.Red;
            //임시(2019-12-23 한민호)
            //dtMEA_DATE.Text = "2019-12-04";
            dtMEA_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtMEA_DATE.Focus();
            //this.Text = "운송사정보 조회";
            //BtnNew.PerformClick();

            //신평경비실은 이송입고를 빌렛입고로 수정(2020-02-19 한민호)
            if (clsUserInfo.Place == "1100")
                btnItemType7.Text = "빌렛입고";

            //디지털시계
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss");
            timer1.Enabled = true;
            timer1.Start();
        }

        #endregion

        #region Grid Click

        private void grdScha_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region 품목구분 클릭 이벤트
        private void btnItemType1_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Red;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = true;
            QueryClick();
        }

        private void btnItemType2_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Red;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = true;
            QueryClick();
        }

        private void btnItemType3_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Red;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = true;
            QueryClick();
        }

        private void btnItemType4_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Red;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = true;
            QueryClick();
        }

        private void btnItemType5_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Red;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = true;
            QueryClick();
        }

        private void btnItemType6_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Red;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = true;
            QueryClick();
        }

        private void btnItemType7_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Red;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = true;
            QueryClick();
        }

        private void btnItemType8_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Red;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = true;
            QueryClick();
        }

        private void btnItemType9_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Red;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = true;
            QueryClick();
        }

        private void btnItemType10_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Red;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = true;
            QueryClick();
        }

        private void btnItemType11_Click(object sender, EventArgs e)
        {
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Red;
            btnItemType12.ForeColor = Color.Black;
            btnReceipt.Enabled = false;
            QueryClick();
        }

        private void btnItemType12_Click(object sender, EventArgs e)
        {
            if (rdReceiptWait.Checked == true)
            {
                MessageBox.Show("접수대기는 전체조회 할 수 없습니다.");
                return;
            }
            btnItemType1.ForeColor = Color.Black;
            btnItemType2.ForeColor = Color.Black;
            btnItemType3.ForeColor = Color.Black;
            btnItemType4.ForeColor = Color.Black;
            btnItemType5.ForeColor = Color.Black;
            btnItemType6.ForeColor = Color.Black;
            btnItemType7.ForeColor = Color.Black;
            btnItemType8.ForeColor = Color.Black;
            btnItemType9.ForeColor = Color.Black;
            btnItemType10.ForeColor = Color.Black;
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Red;
            QueryClick();
        }
        #endregion

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            QueryClick();
        }

        public void QueryClick()
        {
            try
            {              
                //DataSet ds = DBConn._ExecuteDataSet(Query);                
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_MEA_DATE", dtMEA_DATE.Text.Replace("-", ""));
                dict.Add("P_VEHL_NO", txtVEHL_NO.Text);
                dict.Add("P_DRV_NM", txtDRV_NM.Text);
                dict.Add("P_CARD_NO", txtCARD_NO.Text);
                dict.Add("P_SITE_NM", txtSITE_NM.Text);
                dict.Add("P_PLNT_NO", clsUserInfo.Place); //사업장_계량대    
            
                if (btnItemType1.ForeColor == Color.Red)        //원자재
                    dict.Add("P_ITEM_TYPE", "01");  
                else if (btnItemType2.ForeColor == Color.Red)   //수입원자재
                    dict.Add("P_ITEM_TYPE", "02");  
                else if (btnItemType3.ForeColor == Color.Red)   //철근판매
                    dict.Add("P_ITEM_TYPE", "03");  
                else if (btnItemType4.ForeColor == Color.Red)   //가공철근
                    dict.Add("P_ITEM_TYPE", "04");  
                else if (btnItemType5.ForeColor == Color.Red)   //빌렛출하
                    dict.Add("P_ITEM_TYPE", "05");  
                else if (btnItemType6.ForeColor == Color.Red)   //이송출하
                    dict.Add("P_ITEM_TYPE", "06");  
                else if (btnItemType7.ForeColor == Color.Red)   //이송입고
                    dict.Add("P_ITEM_TYPE", "07");  
                else if (btnItemType8.ForeColor == Color.Red)   //부자재/저장품
                    dict.Add("P_ITEM_TYPE", "08");  
                else if (btnItemType9.ForeColor == Color.Red)   //수입부/저장품
                    dict.Add("P_ITEM_TYPE", "09");
                else if (btnItemType10.ForeColor == Color.Red)   //폐기물/부산물
                    dict.Add("P_ITEM_TYPE", "10");
                else if (btnItemType11.ForeColor == Color.Red)   //자가고철
                    dict.Add("P_ITEM_TYPE", "11");
                else if (btnItemType12.ForeColor == Color.Red)   //전체조회
                    dict.Add("P_ITEM_TYPE", "12");  

                if (rdReceiptWait.Checked == true)          //접수대기
                    dict.Add("P_STATUS", "1");  
                else if (rdWeightWait.Checked == true)      //계량대기
                    dict.Add("P_STATUS", "2");  
                else if (rdWeightComplete.Checked == true)  //계량완료
                    dict.Add("P_STATUS", "3");

                DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_LIST_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdMaster.DataSource = ds.Tables[0];
                }
                else
                {
                    grdMaster.DataSource = null;
                    InitInputLayout();
                }

                //DBLK_LIVE는 조회 후 링크 기능을 종료 해야 함(2020-03-06 한민호)
                if ((rdReceiptWait.Checked == true) && (!(btnItemType12.ForeColor == Color.Red)))    //접수대기이고 전체조회 아닌것 
                {
                    //임시로 AWDBO의 VIEW 사용(2020-03-11 한민호)
                    if (!(btnItemType4.ForeColor == Color.Red))
                    {
                        String Query1 = "COMMIT ";
                        DBConn._ExecuteDataSet(Query1);
                        String Query2 = "ALTER SESSION CLOSE DATABASE LINK DBLK_LIVE ";
                        DBConn._ExecuteDataSet(Query2);
                    }
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

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            if (gvwMaster.FocusedRowHandle < 0) return;

            ReceiptPopup Receipt = new ReceiptPopup();
            Receipt.CAR_NO = gvwMaster.GetFocusedRowCellValue("CAR_NO").ToString();
            Receipt.VENDOR_NM = gvwMaster.GetFocusedRowCellValue("VENDOR_NM").ToString();
            Receipt.DIS_CLASS = gvwMaster.GetFocusedRowCellValue("DIS_CLASS").ToString();
            Receipt.DRV_NM = gvwMaster.GetFocusedRowCellValue("DRV_NM").ToString();
            Receipt.DRV_TEL_NO = gvwMaster.GetFocusedRowCellValue("DRV_TEL_NO").ToString();
            Receipt.REMARK = gvwMaster.GetFocusedRowCellValue("REMARK").ToString();

            Receipt.PK_SCD_NO = gvwMaster.GetFocusedRowCellValue("PK_SCD_NO").ToString();
            Receipt.SHIP_GB = gvwMaster.GetFocusedRowCellValue("SHIP_GB").ToString();
            Receipt.ORD_GB = gvwMaster.GetFocusedRowCellValue("ORD_GB").ToString();
            Receipt.ITEM_TYPE = gvwMaster.GetFocusedRowCellValue("ITEM_TYPE").ToString();
            Receipt.ITEM_CODE = gvwMaster.GetFocusedRowCellValue("ITEM_CODE").ToString();
            Receipt.VENDOR_ID = gvwMaster.GetFocusedRowCellValue("VENDOR_ID").ToString();
            Receipt.TRS_CO_NO = gvwMaster.GetFocusedRowCellValue("TRS_CO_NO").ToString();

            Receipt.PLNT_NO = gvwMaster.GetFocusedRowCellValue("PLNT_NO").ToString();
            Receipt.PK_FST_NO = gvwMaster.GetFocusedRowCellValue("PK_FST_NO").ToString();
            Receipt.ITEM_NM = gvwMaster.GetFocusedRowCellValue("ITEM_NM").ToString();
            Receipt.DRV_TEL_NO = gvwMaster.GetFocusedRowCellValue("DRV_TEL_NO").ToString();
            Receipt.ITEM_TYPE_NM = gvwMaster.GetFocusedRowCellValue("DIS_CLASS").ToString();
            Receipt.ORD_WGT = gvwMaster.GetFocusedRowCellValue("ORD_WGT").ToString();
            Receipt.MEA_DATE = gvwMaster.GetFocusedRowCellValue("MEA_DATE").ToString();
            //납품처명 추가(2020-03-12 한민호))
            Receipt.SITE_NM = gvwMaster.GetFocusedRowCellValue("SITE_NM").ToString();
            if (Receipt.ShowDialog() == DialogResult.Yes)
            {

            }

            if (!(btnItemType2.ForeColor == Color.Red && rdReceiptWait.Checked == true))    //수입고철은 날짜 구분이 없어서 제외
            {
                //저장 후 계량대기, 차량번호 공백(2020-03-26 한민호)
                rdWeightWait.Checked = true;
                btnReceipt.Enabled = false;     //차량접수
                btnCarCall.Enabled = true;      //차량호출
                btnRemark.Enabled = true;      //비고수정
                txtVEHL_NO.Text = "";

                QueryClick();
            }
        }

        //접수대기
        private void rdReceiptWait_Click(object sender, EventArgs e)
        {
            if (btnItemType12.ForeColor == Color.Red)
            {
                MessageBox.Show("접수대기는 전체조회 할 수 없습니다.");
                return;
            }
            btnReceipt.Enabled = true;      //차량접수
            btnCarCall.Enabled = false;     //차량호출
            btnRemark.Enabled = false;      //비고수정
            QueryClick();
        }

        //계량대기
        private void rdWeightWait_Click(object sender, EventArgs e)
        {
            btnReceipt.Enabled = false;     //차량접수
            btnCarCall.Enabled = true;      //차량호출
            btnRemark.Enabled = true;      //비고수정
            QueryClick();
        }

        //계량완료
        private void rdWeightComplete_Click(object sender, EventArgs e)
        {
            btnReceipt.Enabled = false;     //차량접수
            btnCarCall.Enabled = false;     //차량호출
            btnRemark.Enabled = false;      //비고수정
            QueryClick();
        }

        //카드반납
        private void btnReturn_Click(object sender, EventArgs e)
        {
            ReturnPopup Return = new ReturnPopup();
            if (Return.ShowDialog() == DialogResult.Yes)
            {

            }
            if (!(btnItemType2.ForeColor == Color.Red && rdReceiptWait.Checked == true))    //수입고철은 날짜 구분이 없어서 제외
                QueryClick();
        }

        //비고 전화 수정
        private void btnRemark_Click(object sender, EventArgs e)
        {
            ReturnPopup Return = new ReturnPopup();
            Return.CAR_NO = gvwMaster.GetFocusedRowCellValue("CAR_NO").ToString();
            Return.VENDOR_NM = gvwMaster.GetFocusedRowCellValue("VENDOR_NM").ToString();
            Return.DIS_CLASS = gvwMaster.GetFocusedRowCellValue("DIS_CLASS").ToString();
            Return.DRV_NM = gvwMaster.GetFocusedRowCellValue("DRV_NM").ToString();
            Return.DRV_TEL_NO = gvwMaster.GetFocusedRowCellValue("DRV_TEL_NO").ToString();
            Return.REMARK = gvwMaster.GetFocusedRowCellValue("REMARK").ToString();

            Return.PK_SCD_NO = gvwMaster.GetFocusedRowCellValue("PK_SCD_NO").ToString();
            Return.SHIP_GB = gvwMaster.GetFocusedRowCellValue("SHIP_GB").ToString();
            Return.ORD_GB = gvwMaster.GetFocusedRowCellValue("ORD_GB").ToString();
            Return.ITEM_TYPE = gvwMaster.GetFocusedRowCellValue("ITEM_TYPE").ToString();
            Return.ITEM_CODE = gvwMaster.GetFocusedRowCellValue("ITEM_CODE").ToString();
            Return.VENDOR_ID = gvwMaster.GetFocusedRowCellValue("VENDOR_ID").ToString();
            Return.TRS_CO_NO = gvwMaster.GetFocusedRowCellValue("TRS_CO_NO").ToString();

            Return.PLNT_NO = gvwMaster.GetFocusedRowCellValue("PLNT_NO").ToString();
            Return.PK_FST_NO = gvwMaster.GetFocusedRowCellValue("PK_FST_NO").ToString();
            Return.ITEM_NM = gvwMaster.GetFocusedRowCellValue("ITEM_NM").ToString();
            Return.DRV_TEL_NO = gvwMaster.GetFocusedRowCellValue("DRV_TEL_NO").ToString();

            Return.SEQ_NO = gvwMaster.GetFocusedRowCellValue("SEQ_NO").ToString();
            Return.CARD_NO = gvwMaster.GetFocusedRowCellValue("CARD_NO").ToString();
            if (Return.ShowDialog() == DialogResult.Yes)
            {
                
            }

            if (!(btnItemType2.ForeColor == Color.Red && rdReceiptWait.Checked == true))    //수입고철은 날짜 구분이 없어서 제외
                QueryClick();
        }

        //접수및계량 취소
        private void btnReceiptCancel_Click(object sender, EventArgs e)
        {
            if (gvwMaster.FocusedRowHandle < 0) return;
            //2차계량은 취소불가, 1차계량은 멘트 추가(2020-04-02 한민호)
            string carno = gvwMaster.GetFocusedRowCellValue("CAR_NO").ToString();
            String Query = " SELECT NVL(IN_WGT,0) AS IN_WGT, NVL(OUT_WGT,0) AS OUT_WGT FROM TB_WS02_0002 WHERE SEQ_NO = '" + gvwMaster.GetFocusedRowCellValue("SEQ_NO").ToString() + "' ";
            DataSet ds1 = DBConn._ExecuteDataSet(Query);
            DataTable dt1 = ds1.Tables[0];
            if (Convert.ToInt32(dt1.Rows[0]["OUT_WGT"].ToString()) > 0)
            {
                MessageBox.Show(carno + " 차량은 2차계량 되었습니다.","취소불가");
                return;
            }

            string massageMain = "";
            string massageTitle = "";
            if (Convert.ToInt32(dt1.Rows[0]["IN_WGT"].ToString()) > 0)
            {
                massageMain = " 차량은 이미 1차계량이 되었습니다.\r\n그래도 접수를 취소하시겠습니까?";
                massageTitle = "1차계량 취소";
            }
            else
            {
                massageMain = " 차량의 접수를 취소하시겠습니까?";
                massageTitle = "접수자료 취소";
            }
            if (MessageBox.Show(carno + massageMain, massageTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //if (MessageBox.Show(gvwMaster.GetFocusedRowCellValue("CAR_NO").ToString() + " 차량의 접수를 취소하시겠습니까?", "접수자료 취소", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    //String Query = "UPDATE WMS_MEASURE_RST SET USE_YN = 'N' WHERE SEQ_NO = '" + gvwMaster.GetFocusedRowCellValue("SEQ_NO").ToString() + "' ";
                    //DBConn._ExecuteDataSet(Query);
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Add("P_FG", "D"); // D:삭제(취소)

                    p.Add("P_SEQ_NO", gvwMaster.GetFocusedRowCellValue("SEQ_NO").ToString());
                    p.Add("P_PLNT_NO", clsUserInfo.Place);
                    p.Add("P_MEA_DATE", "");
                    p.Add("P_PK_FST_NO", "");
                    p.Add("P_PK_SCD_NO", "");

                    p.Add("P_SHIP_GB", "");
                    p.Add("P_ORD_GB", "");
                    p.Add("P_ITEM_TYPE", "");
                    p.Add("P_ITEM_TYPE_NM", "");
                    //p.Add("P_ITEM_TYPE_NM", txtUSER_CD.Text);
                    p.Add("P_ITEM_CODE", "");

                    p.Add("P_ITEM_NM", "");
                    p.Add("P_CARD_NO", txtCARD_NO.Text);
                    p.Add("P_TRS_CO_NO", "");
                    p.Add("P_VENDOR_ID", "");
                    p.Add("P_VENDOR_NM", "");
                    //p.Add("P_VENDOR_NM", txtUSER_CD.Text);

                    p.Add("P_CAR_NO", "");
                    p.Add("P_DRV_NM", "");
                    p.Add("P_DRV_TEL_NO", "");
                    p.Add("P_ORD_WGT", "");
                    p.Add("P_REG_ID", clsUserInfo.User_id);
                    p.Add("P_REMARK", "");
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
                    if (!(btnItemType2.ForeColor == Color.Red && rdReceiptWait.Checked == true))    //수입고철은 날짜 구분이 없어서 제외
                        QueryClick();
                }
            }
        }

        //차량번호 엔터
        private void txtVEHL_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryClick();
            }
        }

        //검색조건에 도(상)착 추가(2020-04-02 한민호)
        private void txtSITE_NM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryClick();
            }

        }

        //순번
        private void gvwMaster_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss");
        }

        private void dtMEA_DATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryClick();
            }
        }

        //차량호출
        private void btnCarCall_Click(object sender, EventArgs e)
        {
            if (gvwMaster.FocusedRowHandle < 0) return;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                //차량호출시 메시지 내용이 길어져서 MMS전송 방식으로 변경(2022-02-09 정성호)
                String Query = " SELECT CODE_VALUE1 AS CODE FROM TB_WS01_0002 WHERE TYPE_CD = 'WS_006' AND CODE = '" + clsUserInfo.Place.Substring(0, 2) + "' AND USE_YN = 'Y' AND DEL_YN = 'N' ";
                DataSet ds1 = DBConn._ExecuteDataSet(Query);
                DataTable dt1 = ds1.Tables[0];
                string CalllingNumber = dt1.Rows[0]["CODE"].ToString();

                StringBuilder msg = new StringBuilder();

                msg.AppendLine(string.Format("카드번호:{0} 성명:{1}님 입차하십시오 [대한제강]", gvwMaster.GetFocusedRowCellValue("CARD_NO").ToString(), gvwMaster.GetFocusedRowCellValue("DRV_NM").ToString()));
                msg.AppendLine("▶ 대한제강 출입차량의 안전 운행 수칙 ◀");
                msg.AppendLine("1. 안전장구류 (안전화,안전모)필히 착용");
                msg.AppendLine("2. 사내속도 10km 이하 및 차량 비상등 점등");
                msg.AppendLine("3. 제품/게이트별 지정된 대기장소에 정차");
                msg.AppendLine("4. 게이트 진출입시 경보시스템 및 반사경 확인");
                msg.AppendLine("5. 제품 상차작업 외 차량 이탈금지");
                msg.AppendLine("6. 상차 작업 시 크레인/출하인원 신호/지시 준수");
                msg.AppendLine("7. 주.정차 후 차량의 전.후.측면 확인 후 출발");
                msg.AppendLine("8. 제품 결박 작업 시 추락전도 예방 철저");

                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_SUBJECT", "[차세대MMS 전송테스트]"); //제목
                p.Add("P_PHONE", gvwMaster.GetFocusedRowCellValue("DRV_TEL_NO").ToString()); //수신번호
                p.Add("P_CALLBACK", CalllingNumber); //발신번호
                p.Add("P_STATUS", "0"); //상태 0:전송대기, 2:결과수신대기, 3:결과수신완료
                p.Add("P_MSG", msg.ToString()); //메시지
                p.Add("P_TYPE", "0"); //타입 0:MMS, 1:MMSURL, 2:국제SMS, 3:국제MMS, 4:PUSH, 7:HTML

                DBConn.ExecuteQuerySPR2("SP_MU_MSG_SEND", p);

                ////오픈 시 풀어야 함( 2020-02-20 한민호)
                ////SQL접속
                //string ConnStr = "server=192.168.10.7;database=TMS_SMS;uid=tms;pwd=tms50";
                //SqlConnection sCon = new SqlConnection(ConnStr);
                //sCon.Open();
                
                ////녹산, 신평, 평택 발신번호 달리 표시(2020-03-31 한민호)
                //String Query = " SELECT CODE_VALUE1 AS CODE FROM TB_WS01_0002 WHERE TYPE_CD = 'WS_006' AND CODE = '" + clsUserInfo.Place.Substring(0,2) + "' AND USE_YN = 'Y' AND DEL_YN = 'N' ";
                //DataSet ds1 = DBConn._ExecuteDataSet(Query);
                //DataTable dt1 = ds1.Tables[0];
                //string CalllingNumber = dt1.Rows[0]["CODE"].ToString();

                //string strSQL = string.Empty;
                //strSQL = "insert into EM_TRAN( "
                //        + "   TRAN_PHONE "
                //        + " , TRAN_CALLBACK"
                //        + " , TRAN_STATUS "
                //        + " , TRAN_DATE "
                //        + " , TRAN_MSG ) "
                //        + " Values( "
                //    //테스트 시 내 번호(2020-02-20 한민호)
                //    //+ "   '" + "010-2445-4497" + "'"
                //        + "   '" + gvwMaster.GetFocusedRowCellValue("DRV_TEL_NO").ToString() + "'"
                //        //녹산번호는 330-9258 임(2020-03-26)
                //        //녹산, 신평, 평택 발신번호 달리 표시(2020-03-31 한민호)
                //        + " , '" + CalllingNumber + "'"
                //        //+ " , '" + "051-330-9258" + "'"
                //        //+ " , '" + "051-220-3318" + "'"
                //        + " , '1' "
                //        + " , '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                //        + " , '카드번호:" + gvwMaster.GetFocusedRowCellValue("CARD_NO").ToString() + "  성명:" + gvwMaster.GetFocusedRowCellValue("DRV_NM").ToString() + "님           입차하십시오    [대한제강]') ";

                //SqlCommand cmd = new SqlCommand(strSQL, sCon);

                //int rtn = -1;
                //rtn = cmd.ExecuteNonQuery();
                //if (rtn < 0)
                //{
                //    MessageBox.Show("SMS전송오류");
                //    sCon.Close();
                //    return;
                //}
                //sCon.Close();

                p = new Dictionary<string, string>();
                p.Add("P_FG", "S"); // S:차량호출

                p.Add("P_SEQ_NO", gvwMaster.GetFocusedRowCellValue("SEQ_NO").ToString());
                p.Add("P_PLNT_NO", clsUserInfo.Place);
                p.Add("P_MEA_DATE", "");
                p.Add("P_PK_FST_NO", "");
                p.Add("P_PK_SCD_NO", "");

                p.Add("P_SHIP_GB", "");
                p.Add("P_ORD_GB", "");
                p.Add("P_ITEM_TYPE", "");
                p.Add("P_ITEM_TYPE_NM", "");
                //p.Add("P_ITEM_TYPE_NM", txtUSER_CD.Text);
                p.Add("P_ITEM_CODE", "");

                p.Add("P_ITEM_NM", "");
                p.Add("P_CARD_NO", txtCARD_NO.Text);
                p.Add("P_TRS_CO_NO", "");
                p.Add("P_VENDOR_ID", "");
                p.Add("P_VENDOR_NM", "");
                //p.Add("P_VENDOR_NM", txtUSER_CD.Text);

                p.Add("P_CAR_NO", "");
                p.Add("P_DRV_NM", "");
                p.Add("P_DRV_TEL_NO", "");
                p.Add("P_ORD_WGT", "");
                p.Add("P_REG_ID", clsUserInfo.User_id);
                p.Add("P_REMARK", "");
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
                if (!(btnItemType2.ForeColor == Color.Red && rdReceiptWait.Checked == true))    //수입고철은 날짜 구분이 없어서 제외
                    QueryClick();
            }
        }

        //그리드 색깔 지정
        private void gvwMaster_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //호출완료, 입차, 완료만 색깔 넣기(2020-02-20 한민호)
            string status = gvwMaster.GetRowCellValue(e.RowHandle, "STATUS").ToString();
            if (status == "호출완료" || status == "입차" || status == "완료")
            {
                switch (gvwMaster.GetRowCellValue(e.RowHandle, "DIS_CLASS").ToString())
                {
                    case "철근판매":
                        e.Appearance.BackColor = Color.MistyRose;
                        break;
                    case "빌렛출하":
                        e.Appearance.BackColor = Color.MistyRose;
                        break;
                    case "이송출하":
                        e.Appearance.BackColor = Color.MistyRose;
                        break;
                    case "이송입고":
                        e.Appearance.BackColor = Color.Yellow;
                        break;
                }
            }
            //입차 된 건은 호출완료 색깔로 되어 버리기 때문에 호출완료 색깔 따로 지정(2020-03-26 한민호)
            string smsret = gvwMaster.GetRowCellValue(e.RowHandle, "SMS_REG_DT").ToString();
            if (smsret != "" && rdWeightWait.Checked == true)
                e.Appearance.BackColor = Color.LightCyan;
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}