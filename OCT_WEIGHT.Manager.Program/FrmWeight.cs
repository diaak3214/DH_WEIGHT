using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.util;
using OCT_WEIGHT.Manager.Common;
using DevExpress.XtraEditors;

using System.Drawing;
using OCT_WEIGHT.Manager.Common.Popup;
using OCT_WEIGHT.Manager.Common.info;
//인디게이터
using log4net;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;
//영상
using AxAXISMEDIACONTROLLib;
//프린터
using OCT_WEIGHT.AutoWeight.Report;
using DevExpress.XtraReports.UI;
//SMS전송을 위해 SQL연결(2020-02-20 한민호)
using System.Data.SqlClient;
//출입관리 접속을 위해 추가(2020-05-04 한민호)
using MySql.Data.MySqlClient;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class FrmWeight : OCT_WEIGHT.Manager.Common.FrmBase
    {
        DataTable dt = new DataTable();
        DataRow row = null;

        #region 하드웨어 변수
        
        protected static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // 시리얼포트
        // 0 : 인디게이터
        private SerialPort[] serialPort = new SerialPort[2];

        public int indicator_read_old = 0; // 이전 Indicator 수신값
        public int indicator_read_cnt = 0; // 이상 Data 읽은 수
        public int indicator_read_max = 3; // 3번 수신까지 이상시 0으로 값 설정
        
        Boolean weight_fg = false; //1차, 2차 계량 저장 여부
        String tmp_down = "0";
        Boolean CardRead = false;
        Int32 Iny_cnt = 0;

        String HW_COMPORT = "";
        private string WEIGHT_NO = string.Empty;
        String Weight_Area; // 계량대 코드 
        private String Weight_Knd = string.Empty;
        String HW_LPR_IP = "";
        String HW_LPR_PORT = "";
        private String SENSOR_PORT = "0";
        #endregion

        //계량표 출력 datatable 전역변수 추가(2020-02-20 한민호 수정))
        DataTable dt_pnt_qty;


        public FrmWeight()
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


        #endregion

        #region Load

        private void FrmWeight_Load(object sender, EventArgs e)
        {
            btnReceipt.Enabled = true;      //차량접수
            btnCarCall.Enabled = false;     //차량호출
            btnPrint.Enabled = false;       //전표출력

            btnItemType1.ForeColor = Color.Red;
            //임시(2019-12-23 한민호)
            //dtMEA_DATE.Text = "2019-12-04";
            dtMEA_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtMEA_DATE.Focus();
            //this.Text = "운송사정보 조회";
            //BtnNew.PerformClick();

            //디지털시계
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss");
            timer1.Enabled = true;
            timer1.Start();

            //1차계량, 2차계량 조회 분리(2020-03-19 한민호)
            timer2.Enabled = true;
            timer2.Start();

            //영상
            //AMC1.MediaUsername = "root";
            //AMC1.MediaPassword = "kiscovideo";
            //AMC2.MediaUsername = "root";
            //AMC2.MediaPassword = "kiscovideo";
            //AMC3.MediaUsername = "root";
            //AMC3.MediaPassword = "kiscovideo";
            //AMC4.MediaUsername = "root";
            //AMC4.MediaPassword = "kiscovideo";
            AMC1.MediaUsername = "root";
            AMC1.MediaPassword = "zaq1@wsx";
            AMC2.MediaUsername = "root";
            AMC2.MediaPassword = "zaq1@wsx";
            AMC3.MediaUsername = "root";
            AMC3.MediaPassword = "zaq1@wsx";
            AMC4.MediaUsername = "root";
            AMC4.MediaPassword = "zaq1@wsx";

            //평택만 출입관리, 전체조회 활성화(2020-05-07 한민호)
            if (clsUserInfo.Place == "1300")
            {
                panelControl9.Size = new System.Drawing.Size(1207, 400);
                btnItemType13.Visible = true;
                btnItemType12.Visible = true;
                btnEnterClear.Visible = true;
                btnEnterClear.Enabled = false;  //접수대기는 비활성화
            }
            else
            {
                panelControl9.Size = new System.Drawing.Size(1207, 341);
                btnItemType13.Visible = false;
                btnItemType12.Visible = false;
                btnEnterClear.Visible = false;
            }
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
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
            //출입관리 추가(2020-05-14 한민호)
            btnItemType13.ForeColor = Color.Black;
            QueryClick();
        }

        //출입관리 추가(2020-05-14 한민호)
        private void btnItemType13_Click(object sender, EventArgs e)
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
            btnItemType11.ForeColor = Color.Black;
            btnItemType12.ForeColor = Color.Black;
            btnItemType13.ForeColor = Color.Red;
            QueryClick();

        }
        #endregion

        #region 조회
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            QueryClick();
            //QueryClick2();  //1차계량 조회
            //QueryClick3();  //2차계량 조회
            //QueryClick4();  //자동계량항목 조회 
        }

        #region 차량 조회
        public void QueryClick()
        {
            try
            {
                //String Query = null;
                //if (rdReceiptWait.Checked == true)  //접수대기
                //{
                //    if (btnItemType1.ForeColor == Color.Red)        //원자재
                //    {
                //        Query = " SELECT '원자재' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,A.VENDOR_NM ,A.DRV_NM ,A.DRV_PHN AS DRV_TEL_NO ,A.SHIP_VENDOR_NM AS SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,A.ITEM_NAME AS ITEM_NM ,NULL AS REMARK ,A.DELIVERY_NUMBER AS PK_FST_NO ,NULL AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, A.SHIP_GB, NULL AS ORD_GB, '101' AS ITEM_TYPE, A.ITEM_CODE"
                //              + "       ,A.VENDOR_ID, A.VENDOR_ID AS TRS_CO_NO, B.SEQ_NO"
                //              + "   FROM VI_GATE_SCRAP_ORD A"
                //              + "   LEFT JOIN WMS_MEASURE_RST B"
                //              + "     ON A.DELIVERY_NUMBER = B.PK_FST_NO"
                //              + "    AND B.USE_YN = 'Y'"
                //              + "  WHERE A.PLNT_NO =  '1200'"
                //              + "    AND A.DELIVERY_DATE = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND B.SEQ_NO IS NULL"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //    else if (btnItemType2.ForeColor == Color.Red)   //수입원자재
                //    {
                //        Query = " SELECT '수입원자재' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,A.VENDOR_NAME AS VENDOR_NM ,A.DRIVER_NM AS DRV_NM ,A.DRIVER_TEL_NO AS DRV_TEL_NO,A.SHIP_NM AS SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,NULL AS REMARK ,A.BL_NO AS PK_FST_NO ,NULL AS WMSSEQNO ,NULL AS PLNT_NO"
                //              + "       ,A.BL_ITEM_NO AS PK_SCD_NO, A.SHIP_GB, NULL AS ORD_GB, A.ITEM_TYPE, A.ITEM_CODE"
                //              + "       , A.VENDOR_ID, VENDOR_ID AS TRS_CO_NO, NULL AS SEQ_NO"
                //              + "   FROM VI_GATE_IMSCRAP_ORD A"
                //              + "  WHERE A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //    else if (btnItemType3.ForeColor == Color.Red)   //철근판매
                //    {
                //        Query = " SELECT '철근판매' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,A.TRS_CO_NM AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,A.SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,A.ITEM_DESCRIPTION AS ITEM_NM ,DELIVERY_TERMS2 AS REMARK ,A.TRS_PLAN_NO AS PK_FST_NO ,NULL AS WMSSEQNO ,A.FROM_PLNT_NO AS PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, NULL AS ITEM_CODE"
                //              + "       ,A.TRS_CO_NO AS VENDOR_ID, A.TRS_CO_NO, B.SEQ_NO"
                //              + "   FROM VI_GATE_SHIP_ORD A"
                //              + "   LEFT JOIN WMS_MEASURE_RST B"
                //              + "     ON A.TRS_PLAN_NO = B.PK_FST_NO"
                //              + "    AND B.USE_YN = 'Y'"
                //              + "  WHERE A.FROM_PLNT_NO =  '1200'"
                //              + "    AND A.SHIP_GB =  'S'"
                //              + "    AND A.ORD_GB =  'S'"
                //              + "    AND A.ITEM_TYPE =  '10'"
                //              + "    AND A.ORD_DT = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND B.SEQ_NO IS NULL"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //    else if (btnItemType4.ForeColor == Color.Red)   //가공철근
                //    {
                //        Query = " SELECT '가공철근' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,A.CUST_NM AS VENDOR_NM ,A.DRV_NM ,A.DRV_PHN AS DRV_TEL_NO ,A.SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,NULL AS ITEM_NM ,NULL AS REMARK ,A.SHIP_PLAN_NO AS PK_FST_NO ,NULL AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, '0' AS SHIP_GB,  '0' AS ORD_GB, A.ITEM_TYPE, NULL AS ITEM_CODE"
                //              + "       ,A.TRS_CO_NO AS VENDOR_ID, A.TRS_CO_NO, B.SEQ_NO"
                //              + "   FROM VI_GATE_RBSHIP_ORD A"
                //              + "   LEFT JOIN WMS_MEASURE_RST B"
                //              + "     ON A.SHIP_PLAN_NO = B.PK_FST_NO"
                //              + "    AND B.USE_YN = 'Y'"
                //              + "  WHERE A.PLNT_NO = '1210'"
                //              + "    AND A.SHIP_SCH_DT = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND B.SEQ_NO IS NULL"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //    else if (btnItemType5.ForeColor == Color.Red)   //빌렛출하
                //    {
                //        Query = " SELECT '빌렛출하' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,A.TRS_CO_NM AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,A.SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,A.ITEM_DESCRIPTION AS ITEM_NM ,DELIVERY_TERMS2 AS REMARK ,A.TRS_PLAN_NO AS PK_FST_NO ,NULL AS WMSSEQNO ,A.FROM_PLNT_NO AS PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, NULL AS ITEM_CODE"
                //              + "       ,A.TRS_CO_NO AS VENDOR_ID, A.TRS_CO_NO, B.SEQ_NO"
                //              + "   FROM VI_GATE_SHIP_ORD A"
                //              + "   LEFT JOIN WMS_MEASURE_RST B"
                //              + "     ON A.TRS_PLAN_NO = B.PK_FST_NO"
                //              + "    AND B.USE_YN = 'Y'"
                //              + "  WHERE A.FROM_PLNT_NO =  '1200'"
                //              + "    AND A.SHIP_GB = 'S'"
                //              + "    AND A.ORD_GB = 'S'"
                //              + "    AND A.ITEM_TYPE = '30'"
                //              + "    AND A.ORD_DT = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND B.SEQ_NO IS NULL"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //    else if (btnItemType6.ForeColor == Color.Red)   //이송출하
                //    {
                //        Query = " SELECT '이송출하' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,A.TRS_CO_NM AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,A.SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,A.ITEM_DESCRIPTION AS ITEM_NM ,DELIVERY_TERMS2 AS REMARK ,A.TRS_PLAN_NO AS PK_FST_NO ,NULL AS WMSSEQNO ,A.FROM_PLNT_NO AS PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, NULL AS ITEM_CODE"
                //              + "       ,A.TRS_CO_NO AS VENDOR_ID, A.TRS_CO_NO, B.SEQ_NO"
                //              + "   FROM VI_GATE_SHIP_ORD A"
                //              + "   LEFT JOIN WMS_MEASURE_RST B"
                //              + "     ON A.TRS_PLAN_NO = B.PK_FST_NO"
                //              + "    AND B.USE_YN = 'Y'"
                //              + "  WHERE A.FROM_PLNT_NO =  '1200'"
                //              + "    AND A.SHIP_GB =  'S'"
                //              + "    AND A.ORD_GB =  'M'"
                //              + "    AND A.ITEM_TYPE IN ('10','30','50')"
                //              + "    AND A.ORD_DT = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND B.SEQ_NO IS NULL"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //    else if (btnItemType7.ForeColor == Color.Red)   //이송입고
                //    {
                //        Query = " SELECT '이송입고' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,A.TRS_CO_NM AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,A.SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,A.ITEM_DESCRIPTION AS ITEM_NM ,DELIVERY_TERMS2 AS REMARK ,A.TRS_PLAN_NO AS PK_FST_NO ,NULL AS WMSSEQNO ,A.FROM_PLNT_NO AS PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, NULL AS ITEM_CODE"
                //              + "       ,A.TRS_CO_NO AS VENDOR_ID, A.TRS_CO_NO, B.SEQ_NO"
                //              + "   FROM VI_GATE_STR_ORD A"
                //              + "   LEFT JOIN WMS_MEASURE_RST B"
                //              + "     ON A.TRS_PLAN_NO = B.PK_FST_NO"
                //              + "    AND B.USE_YN = 'Y'"
                //              + "  WHERE A.FROM_PLNT_NO =  '1200'"
                //              + "    AND A.SHIP_GB =  'S'"
                //              + "    AND A.ORD_GB =  'M'"
                //              + "    AND A.ITEM_TYPE IN ('10','30','50')"
                //              + "    AND A.ORD_DT = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND B.SEQ_NO IS NULL"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //    else if (btnItemType8.ForeColor == Color.Red)   //부자재/저장품
                //    {
                //        Query = " SELECT '부자재/저장품' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,A.VENDOR_NAME AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,NULL AS SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,NULL AS REMARK ,A.DLV_DOC_NO AS PK_FST_NO ,NULL AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,A.DLV_DOC_ITEM_NO AS PK_SCD_NO, 'S' AS SHIP_GB, '0' AS ORD_GB, A.ITEM_TYPE, A.ITEM_CODE"
                //              + "       ,A.VENDOR_ID, A.VENDOR_ID AS TRS_CO_NO, B.SEQ_NO"
                //              + "   FROM VI_GATE_PO_ORD A"
                //              + "   LEFT JOIN WMS_MEASURE_RST B"
                //              + "     ON A.DLV_DOC_NO = B.PK_FST_NO"
                //              + "    AND A.DLV_DOC_ITEM_NO = B.PK_SCD_NO"
                //              + "    AND B.USE_YN = 'Y'"
                //              + "  WHERE A.PLNT_NO = '1200'"
                //              + "    AND A.DELIVERY_DT = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND B.SEQ_NO IS NULL"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //    else if (btnItemType9.ForeColor == Color.Red)   //수입부/저장품
                //    {
                //        Query = " SELECT '수입부/저장품' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,A.VENDOR_NAME AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,NULL AS SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,NULL AS REMARK ,A.DELIVERY_NO AS PK_FST_NO ,NULL AS WMSSEQNO ,NULL AS PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, 'E' AS SHIP_GB, '0' AS ORD_GB, A.ITEM_TYPE, A.ITEM_CODE"
                //              + "       ,A.VENDOR_ID AS VENDOR_ID, A.VENDOR_ID, B.SEQ_NO"
                //              + "   FROM VI_GATE_IMPO_ORD A"
                //              + "   LEFT JOIN WMS_MEASURE_RST B"
                //              + "     ON A.DELIVERY_NO = B.PK_FST_NO"
                //              + "    AND B.USE_YN = 'Y'"
                //            //+ "  WHERE A.VI_GATE_IMPO_ORD = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "   WHERE B.SEQ_NO IS NULL"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //    else if (btnItemType10.ForeColor == Color.Red)   //폐기물/부산물
                //    {
                //        Query = " SELECT '폐기물/부산물' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,A.VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,NULL AS SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,A.WASTE_ITEM_NM AS ITEM_NM ,NULL AS REMARK ,A.WASTE_NO AS PK_FST_NO ,NULL AS WMSSEQNO ,NULL AS PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, '0' AS SHIP_GB, '0' AS ORD_GB, A.ITEM_TYPE, NULL AS ITEM_CODE"
                //              + "       ,A.VENDOR_ID, A.VENDOR_ID AS TRS_CO_NO, NULL AS SEQ_NO"
                //              + "   FROM VI_GATE_WASTE_REG A"
                //              + "  WHERE '1' = '1'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //    else if (btnItemType11.ForeColor == Color.Red)   //자가고철
                //    {
                //        Query = " SELECT '자가고철' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,A.CAR_NO"
                //              + "       ,NULL AS VENDOR_NM ,NULL AS DRV_NM ,NULL AS DRV_TEL_NO ,NULL AS SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                //              + "       ,A.WASTE_ITEM_NM AS ITEM_NM ,NULL AS REMARK ,A.KEY_NO AS PK_FST_NO ,NULL AS WMSSEQNO ,NULL AS PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, NULL AS SHIP_GB, NULL AS ORD_GB, A.ITEM_TYPE, '자가고철' AS ITEM_CODE"
                //              + "       ,NULL AS VENDOR_ID, NULL AS TRS_CO_NO, NULL AS SEQ_NO"
                //              + "   FROM VI_GATE_SELFCAR_INFO A"
                //              + "  WHERE '1' = '1'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //    }
                //}
                //else if (rdWeightWait.Checked == true || rdWeightComplete.Checked == true)  //계량대기 또는 계량완료
                //{
                //    if (btnItemType1.ForeColor == Color.Red)        //원자재
                //    {
                //        Query = " SELECT '원자재' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,B.VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO, B.SHIP_VENDOR_NM AS SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,A.REMARK ,B.DELIVERY_NUMBER AS PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, A.ITEM_CODE, A.VENDOR_ID, A.VENDOR_ID AS TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_SCRAP_ORD B"
                //              + "     ON A.PK_FST_NO = B.DELIVERY_NUMBER"
                //              + "  WHERE A.PLNT_NO =  '1200'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.SHIP_GB = 'S'"
                //              + "    AND A.ITEM_TYPE = '101'"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //    else if (btnItemType2.ForeColor == Color.Red)   //수입원자재
                //    {
                //        Query = " SELECT '수입원자재' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,B.VENDOR_NAME AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,B.SHIP_NM AS SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,A.REMARK ,B.BL_NO AS PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,B.BL_ITEM_NO AS PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, A.ITEM_CODE, A.VENDOR_ID, A.VENDOR_ID AS TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_IMSCRAP_ORD B"
                //              + "     ON A.PK_FST_NO = B.BL_NO"
                //              + "    AND A.PK_SCD_NO = B.BL_ITEM_NO"
                //              + "    AND A.CAR_NO = B.CAR_NO"
                //              + "  WHERE A.PLNT_NO =  '1200'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.SHIP_GB = 'E'"
                //              + "    AND A.ITEM_TYPE = '101'"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //    else if (btnItemType3.ForeColor == Color.Red)   //철근판매
                //    {
                //        Query = " SELECT '철근판매' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,B.TRS_CO_NM AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,B.SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,A.REMARK ,A.PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,A.PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, A.ITEM_CODE, A.VENDOR_ID, A.TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_SHIP_ORD B"
                //              + "     ON A.PK_FST_NO = B.TRS_PLAN_NO"
                //              + "  WHERE A.PLNT_NO =  '1200'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.SHIP_GB =  'S'"
                //              + "    AND A.ORD_GB =  'S'"
                //              + "    AND A.ITEM_TYPE =  '10'"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //    else if (btnItemType4.ForeColor == Color.Red)   //가공철근
                //    {
                //        Query = " SELECT '가공철근' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,B.CUST_NM AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,B.SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,A.REMARK ,A.PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,NULL AS PK_SCD_NO, 'E' AS SHIP_GB,  '0' AS ORD_GB, A.ITEM_TYPE, NULL AS ITEM_CODE, A.VENDOR_ID, A.TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_RBSHIP_ORD B"
                //              + "     ON A.PK_FST_NO = B.SHIP_PLAN_NO"
                //              + "  WHERE A.PLNT_NO = '1210'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.SHIP_GB =  '0'"
                //              + "    AND A.ORD_GB =  '0'"
                //              + "    AND A.ITEM_TYPE =  '20'"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //    else if (btnItemType5.ForeColor == Color.Red)   //빌렛출하
                //    {
                //        Query = " SELECT '빌렛출하' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,B.TRS_CO_NM AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,B.SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,A.REMARK ,A.PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,A.PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, A.ITEM_CODE, A.VENDOR_ID, A.TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_SHIP_ORD B"
                //              + "     ON A.PK_FST_NO = B.TRS_PLAN_NO"
                //              + "  WHERE A.PLNT_NO =  '1200'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.SHIP_GB =  'S'"
                //              + "    AND A.ORD_GB =  'S'"
                //              + "    AND A.ITEM_TYPE =  '30'"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //    else if (btnItemType6.ForeColor == Color.Red)   //이송출하
                //    {
                //        Query = " SELECT '빌렛출하' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,B.TRS_CO_NM AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,B.SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,A.REMARK ,A.PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,A.PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, A.ITEM_CODE, A.VENDOR_ID, A.TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_SHIP_ORD B"
                //              + "     ON A.PK_FST_NO = B.TRS_PLAN_NO"
                //              + "  WHERE A.PLNT_NO =  '1200'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.SHIP_GB =  'S'"
                //              + "    AND A.ORD_GB =  'M'"
                //              + "    AND A.ITEM_TYPE IN ('10','30','50')"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //    else if (btnItemType7.ForeColor == Color.Red)   //이송입고
                //    {
                //        Query = " SELECT '이송입고' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,B.TRS_CO_NM AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,B.SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,A.REMARK ,A.PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,A.PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, A.ITEM_CODE, A.VENDOR_ID, A.TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_STR_ORD B"
                //              + "     ON A.PK_FST_NO = B.TRS_PLAN_NO"
                //              + "  WHERE A.PLNT_NO =  '1200'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.SHIP_GB =  'S'"
                //              + "    AND A.ORD_GB =  'M'"
                //              + "    AND A.ITEM_TYPE IN ('10','30','50')"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //    else if (btnItemType8.ForeColor == Color.Red)   //부자재/저장품
                //    {
                //        Query = " SELECT '부자재/저장품' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,B.VENDOR_NAME AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,NULL AS SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,A.REMARK ,A.PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,A.PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, A.ITEM_CODE, A.VENDOR_ID, A.TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_PO_ORD B"
                //              + "     ON A.PK_FST_NO = B.DLV_DOC_NO"
                //              + "    AND A.PK_SCD_NO = B.DLV_DOC_ITEM_NO"
                //              + "  WHERE A.PLNT_NO = '1200'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.SHIP_GB =  'S'"
                //              + "    AND A.ORD_GB =  '0'"
                //              + "    AND A.ITEM_TYPE IN ('201','301','401','501')"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //    else if (btnItemType9.ForeColor == Color.Red)   //수입부/저장품
                //    {
                //        Query = " SELECT '수입부/저장품' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,B.VENDOR_NAME AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,NULL AS SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM ,A.REMARK ,A.PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,A.PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, A.ITEM_CODE, A.VENDOR_ID, A.TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_IMPO_ORD B"
                //              + "     ON A.PK_FST_NO = B.DELIVERY_NO"
                //              + "  WHERE A.PLNT_NO = '1200'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.SHIP_GB =  'E'"
                //              + "    AND A.ORD_GB =  '0'"
                //              + "    AND A.ITEM_TYPE IN ('201','301','401','501')"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //    else if (btnItemType10.ForeColor == Color.Red)   //폐기물/부산물
                //    {
                //        Query = " SELECT '폐기물/부산물' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,B.VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,NULL AS SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM,A.REMARK ,A.PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,A.PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, A.ITEM_CODE, A.VENDOR_ID, A.TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_WASTE_REG B"
                //              + "     ON A.PK_FST_NO = B.WASTE_NO"
                //              + "  WHERE A.PLNT_NO = '1200'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.ITEM_TYPE = '998'"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //    else if (btnItemType11.ForeColor == Color.Red)   //자가고철
                //    {
                //        Query = " SELECT '자가고철' AS DIS_CLASS, TO_CHAR(A.CARD_REG_DT,'YYYY-MM-DD HH24:MI:SS') AS RECEPT_TIME "
                //              + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '접수' WHEN OUT_WGT_DT IS NULL THEN '입차' ELSE '완료' END AS STATUS, A.CARD_NO ,A.CAR_NO"
                //              + "       ,NULL AS VENDOR_NM ,A.DRV_NM ,A.DRV_TEL_NO ,NULL AS SITE_NM, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS LOAD_TIME ,TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS DOWN_TIME"
                //              + "       ,A.ITEM_NM,A.REMARK ,A.PK_FST_NO ,A.SEQ_NO AS WMSSEQNO ,A.PLNT_NO"
                //              + "       ,A.PK_SCD_NO, A.SHIP_GB, A.ORD_GB, A.ITEM_TYPE, A.ITEM_CODE, A.VENDOR_ID, A.TRS_CO_NO, A.SEQ_NO"
                //              + "   FROM WMS_MEASURE_RST A"
                //              + "   LEFT JOIN VI_GATE_SELFCAR_INFO B"
                //              + "     ON A.PK_FST_NO = B.KEY_NO"
                //              + "  WHERE A.PLNT_NO = '1200'"
                //              + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //              + "    AND A.ITEM_TYPE = '998'"
                //              + "    AND A.USE_YN = 'Y'"
                //              + "    AND NVL(A.CAR_NO,NULL) LIKE '%'|| '" + txtVEHL_NO.Text + "' ||'%'"
                //              ;
                //        if (rdWeightWait.Checked == true)           //계량대기
                //            Query = Query + " AND A.OUT_WGT_DT IS NULL";
                //        if (rdWeightComplete.Checked == true)        //계량완료
                //            Query = Query + " AND A.OUT_WGT_DT IS NOT NULL";
                //    }
                //}                   
                //DataSet ds = DBConn._ExecuteDataSet(Query);
                //출입관리 추가(2020-05-04 한민호)
                if (btnItemType13.ForeColor == Color.Red && rdReceiptWait.Checked == true)
                {
                    //한글 깨짐 현상을 해결하기 위해 Charset=utf8 추가
                    string ConnStr = "server=pipe.idaehan.com;database=pipe;uid=pipe_lpr;pwd=lpr_2020;Charset=utf8";
                    //string ConnStr = "server=pipe.idaehan.com;database=pipe;uid=pipe_lpr;pwd=lpr_2020";
                    MySqlConnection sCon = new MySqlConnection(ConnStr);
                    sCon.Open();

                    String strSQL = " SELECT PIPE_TB.* FROM ("
                                     + " SELECT '일반방문' AS DIS_CLASS ,NULL AS RECEPT_TIME ,NULL AS STATUS ,NULL AS CARD_NO ,AA.inv_car_num as CAR_NO"
                                     + "        ,AA.inv_comp AS VENDOR_NM ,AA.inv_name AS DRV_NM ,AA.inv_hp AS DRV_TEL_NO, AA.inv_loc AS SITE_NM ,NULL AS LOAD_TIME ,NULL AS DOWN_TIME"
                                     + "        ,CONCAT(AA.charge_name, IF(AA.charge_dept = NULL, '', '('), AA.charge_dept,IF(AA.charge_dept = NULL, '', ')')) AS ITEM_NM "
                                     + "        ,AA.inv_type AS REMARK ,AA.charge_hp AS PK_FST_NO ,NULL AS WMSSEQNO ,'1300' AS PLNT_NO "
                                     + "        ,NULL AS PK_SCD_NO, NULL AS SHIP_GB, NULL AS ORD_GB, NULL AS ITEM_TYPE, NULL AS ITEM_CODE "
                                     + "        ,NULL AS VENDOR_ID, NULL AS TRS_CO_NO, NULL AS SEQ_NO, 0 AS ORD_WGT, NULL AS CARD_RTN_DT "
                                     + "        ,DATE_FORMAT(AA.inv_in_day, '%Y%m%d') AS MEA_DATE, NULL AS SMS_REG_DT "
                                     + "        ,IF(DATE_FORMAT(AA.inv_in_time,'%Y') = 0000 || AA.inv_in_time is null,'0', IF(DATE_FORMAT(AA.inv_out_time,'%Y') = 0000 || AA.inv_out_time is null,'1', '2')) as status2 "
                                     //+ " SELECT AA.*, IF(DATE_FORMAT(AA.inv_in_time,'%Y') = 0000 || AA.inv_in_time is null,'0', IF(DATE_FORMAT(AA.inv_out_time,'%Y') = 0000 || AA.inv_out_time is null,'1', '2')) as status "
                                     + " FROM ( "

                                     + " SELECT "
                                     + " '0' as gubun                 "
                                     + "  , B.gs_co_idx as idx        "
                                     + "  , B.gs_co_cardnum as cardnum "
                                     + "  , A.gs_name as charge_name   "
                                     + " , A.gs_dept as charge_dept   "
                                     + " , A.gs_hp as charge_hp    "
                                     + " , A.gs_inv_comp as inv_comp  "
                                     + " , B.gs_co_name as inv_name   "
                                     + " , A.gs_inv_type as inv_type  "
                                     + " , B.gs_co_hp as inv_hp       "
                                     + " , A.gs_inv_loc as inv_loc    "
                                     + " , DATE_FORMAT(A.gs_inv_in_day, '%Y-%m-%d') as inv_in_day "
                                     + " ,(SELECT IF(B.gs_co_carinfo = '', (select GROUP_CONCAT(gs_car_number) from keis_gs_car_info where gs_idx = A.gs_idx),B.gs_co_carinfo) FROM DUAL) as inv_car_num  "
                                     + " , DATE_FORMAT(B.gs_co_in_date, '%Y-%m-%d %h:%i:%S') as inv_in_time  "
                                     + " , DATE_FORMAT(B.gs_co_out_date, '%Y-%m-%d %h:%i:%S') as inv_out_time  "
                                     + " FROM keis_gs_task A INNER JOIN keis_gs_co_visit B  "
                                     + "   ON A.gs_idx = B.gs_idx "
                                     + " WHERE A.gs_status in ('1', '4', '6') "
                                     + " AND A.gs_inv_in_day =  '" + dtMEA_DATE.Text + "'"
                                    + "  AND A.gs_inv_loc = '평택공장' "

                                     + " UNION ALL "
                                     + " SELECT  "
                                     + " '1' as gubun "
                                     + " ,gs_mis_idx as idx  "
                                     + " ,gs_co_cardnum as cardnum  "
                                     + " ,gs_mis_name as charge_name "
                                     + " ,gs_mis_dept as charge_dept "
                                     + " ,gs_mis_hp as charge_hp     "
                                     + " ,gs_inv_comp as inv_comp    "
                                     + " ,gs_mis_visit_name as inv_name  "
                                     + " ,gs_mis_visit_goal as inv_type  "
                                     + " ,concat(gs_mis_visit_hp1, '-',gs_mis_visit_hp2, '-',gs_mis_visit_hp3) as inv_hp "
                                     + " ,(SELECT CASE gs_mis_visit_loc "
                                     + " WHEN 0 THEN '신평공장' "
                                     + " WHEN 1 THEN '녹산공장' "
                                     + " WHEN 2 THEN '녹산가공2공장' "
                                     + " WHEN 3 THEN '녹산1야드장' "
                                     + " WHEN 4 THEN '평택공장' "
                                     + " WHEN 5 THEN '부산지사' "
                                     + " ELSE '' "
                                     + " END AS gs_mis_visit_loc "
                                     + " FROM DUAL) as inv_loc "
                                     + " , DATE_FORMAT(gs_mis_visit_in_day, '%Y-%m-%d') as inv_in_day  "
                                     + " , gs_mis_car_info as inv_car_num  "
                                    //+ " , DATE_FORMAT(concat(DATE_FORMAT(gs_mis_visit_in_day, '%Y-%m-%d'),gs_mis_visit_in_time), '%Y-%m-%d %h:%i:%S') as inv_in_time  "
                                    //+ " , DATE_FORMAT(concat(DATE_FORMAT(gs_mis_visit_out_day, '%Y-%m-%d'),gs_mis_visit_out_time), '%Y-%m-%d %h:%i:%S') as inv_out_time  "
                                    //+ " , '0000-00-00 12:00:00' as inv_in_time  "
                                     + " , if(gs_co_cardnum='999','2015-00-00 12:00:00','0000-00-00 12:00:00') as inv_in_time  "
                                     + " , '0000-00-00 12:00:00' as inv_out_time  "
                                     + " FROM keis_gs_task_mis "
                                     + " WHERE gs_mis_gubun = 'N' "
                                     + " and gs_mis_visit_in_day =  '" + dtMEA_DATE.Text + "'"
                                     + "  and gs_mis_visit_loc = '4' "
                                     + "  and gs_co_cardnum != '999' "

                                     + " UNION ALL "
                                    + " SELECT "
                                    + " '2' as gubun "
                                    + " , gs_repair_task_idx as idx  "
                                    + " , gs_repair_card_num as cardnum  "
                                    + " , gs_repair_charge as charge_name "
                                    + ", gs_repair_dept as charge_dept  "
                                    + " , gs_repair_charge_num as charge_hp  "
                                    + " , gs_repair_work_company as inv_comp  "
                                    + " , gs_repair_work_charge as inv_name "
                                    + " , gs_repair_content as inv_type     "
                                    + " , gs_repair_work_charge_num as inv_hp "
                                    + " ,(SELECT CASE gs_repair_factory "
                                    + " WHEN 0 THEN '신평공장' "
                                    + " WHEN 1 THEN '녹산공장' "
                                    + " WHEN 2 THEN '녹산가공2공장' "
                                    + " WHEN 3 THEN '녹산1야드장' "
                                    + " WHEN 4 THEN '평택공장' "
                                    + " WHEN 5 THEN '부산지사' "
                                    + " ELSE '' "
                                    + " END AS gs_repair_factory "
                                    + " FROM DUAL) as inv_loc "
                                    + " , DATE_FORMAT(gs_repair_reserv_date, '%Y-%m-%d') as inv_in_day "
                                    + " , gs_repair_car_num as inv_car_num  "
                                    + " , DATE_FORMAT((SELECT min(gs_repair_task_checktime) from keis_gs_repair_task_inout_time where a.gs_repair_task_idx = gs_repair_task_idx and gs_repair_task_checktype = 0), '%Y-%m-%d %h:%i:%S') as inv_in_time "
                                    + " , (SELECT IF((sum(gs_repair_task_headcount) = a.gs_repair_headcount),DATE_FORMAT(max(gs_repair_task_checktime), '%Y-%m-%d %h:%i:%S'), NULL)  "
                                    + " from keis_gs_repair_task_inout_time  "
                                    + " where a.gs_repair_task_idx = gs_repair_task_idx  "
                                    + " and gs_repair_task_checktype = 1 "
                                    + " group by gs_repair_task_idx) as inv_out_time "
                                    + " FROM keis_gs_repair_task_info a "
                                    + " WHERE gs_repair_reserv_date =  '" + dtMEA_DATE.Text + "'"
                                    + "  AND gs_repair_factory = '4' "

                                     + ") AA where IF(DATE_FORMAT(AA.inv_in_time,'%Y') = 0000 || AA.inv_in_time is null,'0', IF(DATE_FORMAT(AA.inv_out_time,'%Y') = 0000 || AA.inv_out_time is null,'1', '2')) ='0' "
                                     + ") PIPE_TB ";
                                    
                                    if (txtDRV_NM2.Text.Trim().Length > 0 && txtVEHL_NO.Text.Trim().Length > 0)
                                    {
                                        strSQL = strSQL + " WHERE right(CAR_NO,4) = '" + txtVEHL_NO.Text.Trim() + "' AND DRV_NM like '" + txtDRV_NM2.Text.Trim() + "%' AND status2 = '0' ";
                                    }
                                    else if (txtDRV_NM2.Text.Trim().Length > 0 && txtVEHL_NO.Text.Trim().Length == 0)
                                    {
                                        strSQL = strSQL + " WHERE DRV_NM like '" + txtDRV_NM2.Text.Trim() + "%' AND status2 = '0' ";
                                    }
                                    else if (txtDRV_NM2.Text.Trim().Length == 0 && txtVEHL_NO.Text.Trim().Length > 0)
                                    {
                                        strSQL = strSQL + " WHERE right(CAR_NO,4) = '" + txtVEHL_NO.Text.Trim() + "' AND status2 = '0' ";
                                    }
                    MySqlDataAdapter adpt = new MySqlDataAdapter(strSQL, sCon);                    
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdMaster.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        grdMaster.DataSource = null;
                        InitInputLayout();
                    }

                    sCon.Close();
                }
                else
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("P_MEA_DATE", dtMEA_DATE.Text.Replace("-", ""));
                    dict.Add("P_VEHL_NO", txtVEHL_NO.Text);
                    dict.Add("P_DRV_NM", txtDRV_NM2.Text);  //검색조건 운전자
                    dict.Add("P_CARD_NO", null);

                    dict.Add("P_SITE_NM", null); //검색조건에 도(상)착 추가(2020-04-02 한민호)

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
                    //출입관리 추가(2020-05-04 한민호)
                    else if (btnItemType13.ForeColor == Color.Red)   //출입관리
                        dict.Add("P_ITEM_TYPE", "13");

                    if (rdReceiptWait.Checked == true)          //접수대기
                        dict.Add("P_STATUS", "1");
                    else if (rdWeightWait.Checked == true)      //계량대기
                        dict.Add("P_STATUS", "2");
                    else if (rdWeightComplete.Checked == true)  //계량완료
                        dict.Add("P_STATUS", "3");

                    DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_CAR_R", dict);
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

        #region 1차계량 조회
        public void QueryClick2()
        {
            try
            {
                //String Query = null;
                //Query = " SELECT TO_CHAR(A.MEA_DATE,'YYYY-MM-DD') AS MEA_DATE, A.MEA_SEQ, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS IN_WGT_DT, A.CAR_NO, TO_CHAR(A.IN_WGT,'99,990') AS IN_WGT"
                //      + "       ,A.CARD_NO, NULL AS VENDOR_NM, A.DRV_NM, NULL AS DIS_CLASS, A.ITEM_NM, A.SEQ_NO"
                //      + "   FROM WMS_MEASURE_RST A"
                //      + "  WHERE A.PLNT_NO =  '1200'"
                //      + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //      + "    AND A.USE_YN = 'Y'"
                //      + "    AND A.IN_WGT_DT IS NOT NULL"
                //      + "    AND A.OUT_WGT_DT IS NULL"
                //      + "  ORDER BY A.MEA_SEQ"
                //      ;
                //DataSet ds = DBConn._ExecuteDataSet(Query);
                //녹산정문(BL), 녹산후문(CL)
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_MEA_DATE", dtMEA_DATE.Text.Replace("-", ""));
                dict.Add("P_PLNT_NO", clsUserInfo.Place); //사업장_계량대    
                DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_01_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //현재 그리드 갯수와 ds테이블의 갯수가 다리면 조회되게 수정(2020-03-20 한민호)
                    //동일한 건수 일 경우 조회 타지 않는 문제 때문에 막음(2020-03-25 한민호)
                    //if (gvwMaster2.RowCount != ds.Tables[0].Rows.Count)
                    //{
                        grdMaster2.DataSource = ds.Tables[0];
                        gvwMaster2.MoveBy(ds.Tables[0].Rows.Count); //포커스가 제일 마지막 행에 오게
                    //}
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
        #endregion

        #region 2차계량 조회
        public void QueryClick3()
        {
            try
            {
                //String Query = null;
                //Query = " SELECT A.MEA_SEQ, A.CAR_NO, NULL AS DIS_CLASS, NULL AS VENDOR_NM, A.ITEM_NM"
                //      + "       ,A.CARD_NO, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS IN_WGT_DT, TO_CHAR(A.IN_WGT,'99,990') || 'kg' AS IN_WGT, TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS OUT_WGT_DT, TO_CHAR(A.OUT_WGT,'99,990')|| 'kg' AS OUT_WGT"
                //      + "       ,TO_CHAR(A.REAL_WGT,'99,990')|| 'kg' AS REAL_WGT, A.SEQ_NO"
                //      + "   FROM WMS_MEASURE_RST A"
                //      + "  WHERE A.PLNT_NO =  '1200'"
                //      + "    AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //      + "    AND A.USE_YN = 'Y'"
                //      + "    AND A.OUT_WGT_DT IS NOT NULL"
                //      + "  ORDER BY A.MEA_SEQ"
                //      ;
                //DataSet ds = DBConn._ExecuteDataSet(Query);
                //녹산정문(BL), 녹산후문(CL)
                string LR = "";
                if (pnlEntry.BackColor == Color.Yellow)             //진입계량기
                {
                    LR = "L";
                }
                else if (pnlAdvance.BackColor == Color.Yellow)      //진출계량기
                {
                    LR = "R";
                }
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_MEA_DATE", dtMEA_DATE.Text.Replace("-", ""));
                dict.Add("P_PLNT_NO", clsUserInfo.Place);   //사업장_계량대 
                dict.Add("P_LR_ENTRY", LR);                 //진입이면 1차계량, 진출이면 2차계량으로 ORDER BY
                DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_02_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //현재 그리드 갯수와 ds테이블의 갯수가 다리면 조회되게 수정(2020-03-20 한민호)
                    if (gvwMaster3.RowCount != ds.Tables[0].Rows.Count)
                    {
                        grdMaster3.DataSource = ds.Tables[0];
                        gvwMaster3.MoveBy(ds.Tables[0].Rows.Count); //포커스가 제일 마지막 행에 오게
                    }
                }
                else
                {
                    grdMaster3.DataSource = null;
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
        #endregion

        #region 자동계량항목 조회 
        public void QueryClick4()
        {
            try
            {
                //String Query = null;
                //Query = " SELECT TO_CHAR(A.MEA_DATE,'YYYY-MM-DD') AS MEA_DATE, A.MEA_SEQ, A.CAR_NO, A.CARD_NO, NULL AS VENDOR_NM"
                //      + "       ,A.ITEM_NM, NULL AS DIS_CLASS, TO_CHAR(A.IN_WGT_DT,'HH24:MI:SS') AS IN_WGT_DT, TO_CHAR(A.IN_WGT,'99,990') AS IN_WGT, TO_CHAR(A.OUT_WGT_DT,'HH24:MI:SS') AS OUT_WGT_DT"
                //      + "       ,TO_CHAR(A.OUT_WGT,'99,990') AS OUT_WGT, TO_CHAR(A.REAL_WGT,'99,990') AS REAL_WGT, A.DRV_NM, A.REMARK"
                //      + "   FROM WMS_MEASURE_RST A"
                //      + "  WHERE A.SEQ_NO=(SELECT SEQ_NO"
                //      + "                   FROM"
                //      + "                     ("
                //      + "                     SELECT "
                //      + "                           SEQ_NO, RANK() OVER(ORDER BY WGT_DT) AS WGT_RANK"
                //      + "                      FROM ("
                //      + "                            SELECT A.SEQ_NO, A.IN_WGT_DT AS WGT_DT"
                //      + "                              FROM WMS_MEASURE_RST A"
                //      + "                             WHERE A.PLNT_NO =  '1200'"SEIN
                //      + "                               AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //      + "                               AND A.IN_WGT_DT IS NOT NULL"
                //      + "                             UNION ALL"
                //      + "                            SELECT A.SEQ_NO, A.OUT_WGT_DT AS WGT_DT"
                //      + "                              FROM WMS_MEASURE_RST A"
                //      + "                             WHERE A.PLNT_NO =  '1200'"
                //      + "                               AND TO_CHAR(A.MEA_DATE,'YYYYMMDD') = '" + dtMEA_DATE.Text.Replace("-", "") + "'"
                //      + "                               AND A.OUT_WGT_DT IS NOT NULL"
                //      + "                           )"
                //      + "                     ) WHERE WGT_RANK = 1  "
                //      + "                 )"
                //      ;
                //DataSet ds = DBConn._ExecuteDataSet(Query);
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_MEA_DATE", dtMEA_DATE.Text.Replace("-", ""));
                dict.Add("P_PLNT_NO", clsUserInfo.Place); //사업장_계량대    
                DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_AUTO_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMEA_DATE.Text = ds.Tables[0].Rows[0]["MEA_DATE"].ToString();
                    txtMEA_SEQ.Text = ds.Tables[0].Rows[0]["MEA_SEQ"].ToString();
                    txtCAR_NO.Text = ds.Tables[0].Rows[0]["CAR_NO"].ToString();
                    txtCARD_NO.Text = ds.Tables[0].Rows[0]["CARD_NO"].ToString();
                    txtVENDOR_NM.Text = ds.Tables[0].Rows[0]["VENDOR_NM"].ToString();
                    txtITEM_NM.Text = ds.Tables[0].Rows[0]["ITEM_NM"].ToString();
                    txtDIS_CLASS.Text = ds.Tables[0].Rows[0]["DIS_CLASS"].ToString();
                    txtIN_WGT_DT.Text = ds.Tables[0].Rows[0]["IN_WGT_DT"].ToString();
                    txtIN_WGT.Text = ds.Tables[0].Rows[0]["IN_WGT"].ToString();
                    txtOUT_WGT_DT.Text = ds.Tables[0].Rows[0]["OUT_WGT_DT"].ToString();
                    txtOUT_WGT.Text = ds.Tables[0].Rows[0]["OUT_WGT"].ToString();
                    txtREAL_WGT.Text = ds.Tables[0].Rows[0]["REAL_WGT"].ToString();
                    txtDRV_NM.Text = ds.Tables[0].Rows[0]["DRV_NM"].ToString();
                    txtREMARK.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                }
                
                //인디게이터 값 바뀔때 마다 계량중량 업데이트(대한제강 2020-01-22)
                String Query = " SELECT SUBSTR(WEIGHT_NO,5) AS WEIGHT_NO, IND_WEIGHT FROM TB_WS02_0003 WHERE SUBSTR(WEIGHT_NO,1,4) = '" + clsUserInfo.Place + "' ";
                DataSet ds2 = DBConn._ExecuteDataSet(Query);
                DataTable dt2 = ds2.Tables[0];
                for (int Cnt = 0; Cnt < dt2.Rows.Count; Cnt++)
                {
                    if (dt2.Rows[Cnt]["WEIGHT_NO"].ToString() == "1")  //입문
                        lblWeight_value_IN.Text = txtsetting(dt2.Rows[Cnt]["IND_WEIGHT"].ToString());
                    else if (dt2.Rows[Cnt]["WEIGHT_NO"].ToString() == "2")  //출문
                        lblWeight_value_OUT.Text = txtsetting(dt2.Rows[Cnt]["IND_WEIGHT"].ToString());
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
        #endregion

        #region 출하오차범위 조회
        public void QueryClick5()
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_PLNT_NO", clsUserInfo.Place); //사업장_계량대    
                DataSet ds = DBConn.ExecuteDataSet2("SP_MU_WEIGHT_SEIN_NEW_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSEIN_MIN.Text = ds.Tables[0].Rows[0]["SEIN_MIN"].ToString();
                    txtSEIN_MAX.Text = ds.Tables[0].Rows[0]["SEIN_MAX"].ToString();
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
        #endregion

        #endregion

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
                txtVEHL_NO.Text = "";

                QueryClick();
            }
        }

        //수동차량접수 추가(2020-05-01 한민호)
        private void btnManualReceipt_Click(object sender, EventArgs e)
        {
            ManualReceiptPopup Receipt = new ManualReceiptPopup();
            if (Receipt.ShowDialog() == DialogResult.Yes)
            {

            }

            //저장 후 전체조회, 계량대기, 차량번호 공백(2020-03-26 한민호)
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
            btnItemType13.ForeColor = Color.Black;
            rdWeightWait.Checked = true;
            btnReceipt.Enabled = false;     //차량접수
            btnCarCall.Enabled = true;      //차량호출
            txtVEHL_NO.Text = "";

            QueryClick();

        }

        //접수대기
        private void rdReceiptWait_Click(object sender, EventArgs e)
        {
            btnReceipt.Enabled = true;      //차량접수
            btnCarCall.Enabled = false;     //차량호출
            btnPrint.Enabled = false;       //전표출력            
            //평택만 출입관리, 전체조회 활성화(2020-05-07 한민호)
            if (clsUserInfo.Place == "1300")
            {
                btnEnterClear.Enabled = false;
            }
            QueryClick();
        }

        //계량대기
        private void rdWeightWait_Click(object sender, EventArgs e)
        {
            btnReceipt.Enabled = false;     //차량접수
            btnCarCall.Enabled = true;      //차량호출
            btnPrint.Enabled = false;       //전표출력
            //평택만 출입관리, 전체조회 활성화(2020-05-07 한민호)
            if (clsUserInfo.Place == "1300")
            {
                btnEnterClear.Enabled = true;
            }
            QueryClick();
        }

        //계량완료
        private void rdWeightComplete_Click(object sender, EventArgs e)
        {
            btnReceipt.Enabled = false;     //차량접수
            btnCarCall.Enabled = false;     //차량호출
            btnPrint.Enabled = true;       //전표출력
            //평택만 출입관리, 전체조회 활성화(2020-05-07 한민호)
            if (clsUserInfo.Place == "1300")
            {
                btnEnterClear.Enabled = false;
            }
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

        //출입관리 추가(2020-05-07 한민호)
        private void btnEnterClear_Click(object sender, EventArgs e)
        {
            ReceiptPopup Receipt = new ReceiptPopup();
            Receipt.DIS_CLASS = "일반방문";
            Receipt.PLNT_NO = clsUserInfo.Place;
            Receipt.FRM_CLASS = "2";    //1:차량접수, 2:출입해제
            if (Receipt.ShowDialog() == DialogResult.Yes)
            {

            }

            //저장 후 전체조회, 계량대기, 차량번호 공백(2020-03-26 한민호)
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
            btnItemType13.ForeColor = Color.Black;
            rdWeightComplete.Checked = true;
            btnReceipt.Enabled = false;     //차량접수
            btnCarCall.Enabled = false;     //차량호출
            btnPrint.Enabled = true;       //전표출력
            txtVEHL_NO.Text = "";

            QueryClick();

        }       

        //수동계량입력 팝업
        private void btnManualWeight_Click(object sender, EventArgs e)
        {
            if (pnlEntry.BackColor != Color.Yellow && pnlAdvance.BackColor != Color.Yellow)   
            {
                MessageBox.Show("진입 계량기 나 진출 계량기 둘 중 하나는 선택 되어야 합니다.");
                return;
            }

            ManualWeightPopup ManualWeight = new ManualWeightPopup();
            if (pnlEntry.BackColor == Color.Yellow)             //진입계량기
            {
                ManualWeight.WGT = lblWeight_value_IN.Text;
            }
            else if (pnlAdvance.BackColor == Color.Yellow)      //진출계량기
            {
                ManualWeight.WGT = lblWeight_value_OUT.Text;
            }
            ManualWeight.WGT_DT = DateTime.Now.ToString("HH:mm:ss");  

            if (ManualWeight.ShowDialog() == DialogResult.Yes)
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
                MessageBox.Show(carno + " 차량은 2차계량 되었습니다.", "취소불가");
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

            if (DBConn.IsOpenDB())
            {
                //QueryClick2();  //1차계량 조회
                QueryClick3();  //2차계량 조회
                QueryClick4();  //자동계량항목 조회 
                QueryClick5();  //출하오차범위 조회 
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (DBConn.IsOpenDB())
            {
                QueryClick2();  //1차계량 조회
                //QueryClick3();  //2차계량 조회
            }
        }
        private void dtMEA_DATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryClick();
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

        #region 카메라

        private void FrmWeight_Shown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Application.DoEvents();
                SetLoc();

                Application.DoEvents();
                tabMain.SelectedTabPageIndex = 0;
                Application.DoEvents();
                timer1.Interval = 2000;
                timer1.Enabled = true;
                Application.DoEvents();
                timer1_Tick(timer1, new EventArgs());
            }
            finally
            {

                this.Cursor = Cursors.Default;
            }

        }

        #region 지번
        
        private void SetLoc()
        {
            //////////////////
            // DB 읽어서 카운트 수만큼 생성 (하화지 검색 lwh)
            //////////////////
            StringBuilder QueryString = new StringBuilder();
            //2분할로 변경(2020-02-12 한민호)
            QueryString.Append(" SELECT A.LOC_CD, A.LOC_NM, A.CAMERA_CD1, A.CAMERA_CD2");
            QueryString.Append("       ,B.CAMERA_NAME AS CAMERA_NAME1, C.CAMERA_NAME AS CAMERA_NAME2");
            //QueryString.Append(" SELECT A.LOC_CD, A.LOC_NM, A.CAMERA_CD1, A.CAMERA_CD2, A.CAMERA_CD3, A.CAMERA_CD4");
            //QueryString.Append("       ,B.CAMERA_NAME AS CAMERA_NAME1, C.CAMERA_NAME AS CAMERA_NAME2, D.CAMERA_NAME AS CAMERA_NAME3, E.CAMERA_NAME AS CAMERA_NAME4");
            QueryString.Append("   FROM TB_WS01_0020 A");
            QueryString.Append("   LEFT JOIN TB_WS01_0021 B");
            QueryString.Append("     ON A.CAMERA_CD1 = B.CAMERA_CD");
            QueryString.Append("   LEFT JOIN TB_WS01_0021 C");
            QueryString.Append("     ON A.CAMERA_CD2 = C.CAMERA_CD");
            QueryString.Append("   LEFT JOIN TB_WS01_0021 D");
            QueryString.Append("     ON A.CAMERA_CD3 = D.CAMERA_CD");
            QueryString.Append("   LEFT JOIN TB_WS01_0021 E");
            QueryString.Append("     ON A.CAMERA_CD4 = E.CAMERA_CD");
            QueryString.Append("  WHERE A.LOC_CD = '" + clsUserInfo.Place + "'");
            //QueryString.Append(" SELECT 'A1' AS LOC_CD,'신평공장' AS LOC_NM,'CM2011120014' AS CAMERA_CD1,'CM2011120026' AS CAMERA_CD2,'CM2011120025' AS CAMERA_CD3,'CM2011120031' AS CAMERA_CD4");
            //QueryString.Append("       ,'상A1' AS CAMERA_NAME1,'상A2' AS CAMERA_NAME2,'상0' AS CAMERA_NAME3,'G/S1' AS CAMERA_NAME4");
            //QueryString.Append("   FROM DUAL");
            DataSet ds = DBConn._ExecuteDataSet(QueryString.ToString());
            DataTable dt = ds.Tables[0];

            dtSetCam = dt.Copy();
            listTabPages.Clear();

            //하화지별로 4채널의 지번생성 LWH
            for (int Cnt = 0; Cnt < dt.Rows.Count; Cnt++)
            {
                DevExpress.XtraEditors.PanelControl PnlAxis = new DevExpress.XtraEditors.PanelControl();
                DevExpress.XtraTab.XtraTabPage CTab = new DevExpress.XtraTab.XtraTabPage();
                PnlAxis.Dock = DockStyle.Fill;
                PnlAxis.Appearance.BackColor = Color.Black;
                PnlAxis.Appearance.Options.UseBackColor = true;

                fnSetCam(Cnt, CTab, PnlAxis);

                CTab.Text = dt.Rows[Cnt]["LOC_NM"].ToString();
                CTab.Appearance.Header.Font = new Font("HY견고딕", 12, FontStyle.Bold);
                CTab.Controls.Add(PnlAxis);
                CTab.Name = dt.Rows[Cnt]["LOC_CD"].ToString();
                CTab.ImageIndex = 1;
                CTab.BackColor = Color.Black;
                tabMain.TabPages.Add(CTab);
                tabMain.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.True;

                listTabPages.Add(CTab);
            }
            //, 19pt, style=Bold
        }

        #endregion

        #region 카메라 설정

        List<DevExpress.XtraTab.XtraTabPage> listTabPages = new List<DevExpress.XtraTab.XtraTabPage>();
        DataTable dtSetCam = null;
        void fnSetCam(int Cnt, DevExpress.XtraTab.XtraTabPage CTab, DevExpress.XtraEditors.PanelControl PnlAxis)
        {
            DataTable dt = dtSetCam;
            if (dt == null)
                return;

            AxAXISMEDIACONTROLLib.AxAxisMediaControl Ax1 = new AxAXISMEDIACONTROLLib.AxAxisMediaControl();
            AxAXISMEDIACONTROLLib.AxAxisMediaControl Ax2 = new AxAXISMEDIACONTROLLib.AxAxisMediaControl();
            //2분할로 변경(2020-02-12 한민호)
            //AxAXISMEDIACONTROLLib.AxAxisMediaControl Ax3 = new AxAXISMEDIACONTROLLib.AxAxisMediaControl();
            //AxAXISMEDIACONTROLLib.AxAxisMediaControl Ax4 = new AxAXISMEDIACONTROLLib.AxAxisMediaControl();
            Label lbl1 = new Label();
            Label lbl2 = new Label();
            //2분할로 변경(2020-02-12 한민호)
            //Label lbl3 = new Label();
            //Label lbl4 = new Label();

            Ax1.Name = dt.Rows[Cnt]["CAMERA_CD1"].ToString();
            Ax2.Name = dt.Rows[Cnt]["CAMERA_CD2"].ToString();
            //2분할로 변경(2020-02-12 한민호)
            //Ax3.Name = dt.Rows[Cnt]["CAMERA_CD3"].ToString();
            //Ax4.Name = dt.Rows[Cnt]["CAMERA_CD4"].ToString();

            Ax1.Tag = "STOP";
            Ax2.Tag = "STOP";
            //2분할로 변경(2020-02-12 한민호)
            //Ax3.Tag = "STOP";
            //Ax4.Tag = "STOP";

            Ax1.TabIndex = 0;
            Ax2.TabIndex = 1;
            //2분할로 변경(2020-02-12 한민호)
            //Ax3.TabIndex = 2;
            //Ax4.TabIndex = 3;

            lbl1.Name = dt.Rows[Cnt]["CAMERA_CD1"].ToString();
            lbl2.Name = dt.Rows[Cnt]["CAMERA_CD2"].ToString();
            //2분할로 변경(2020-02-12 한민호)
            //lbl3.Name = dt.Rows[Cnt]["CAMERA_CD3"].ToString();
            //lbl4.Name = dt.Rows[Cnt]["CAMERA_CD4"].ToString();

            lbl1.Text = dt.Rows[Cnt]["CAMERA_NAME1"].ToString();
            lbl2.Text = dt.Rows[Cnt]["CAMERA_NAME2"].ToString();
            //2분할로 변경(2020-02-12 한민호)
            //lbl3.Text = dt.Rows[Cnt]["CAMERA_NAME3"].ToString();
            //lbl4.Text = dt.Rows[Cnt]["CAMERA_NAME4"].ToString();

            //Ax1.OnClick += new _IAxisMediaControlEvents_OnClickEventHandler(Ax_OnClick);
            //Ax2.OnClick += new _IAxisMediaControlEvents_OnClickEventHandler(Ax_OnClick);
            //Ax3.OnClick += new _IAxisMediaControlEvents_OnClickEventHandler(Ax_OnClick);
            //Ax4.OnClick += new _IAxisMediaControlEvents_OnClickEventHandler(Ax_OnClick);


            //Ax1.OnDoubleClick += new _IAxisMediaControlEvents_OnDoubleClickEventHandler(myAMC_OnDoubleClick);
            //Ax2.OnDoubleClick += new _IAxisMediaControlEvents_OnDoubleClickEventHandler(myAMC_OnDoubleClick);
            //Ax3.OnDoubleClick += new _IAxisMediaControlEvents_OnDoubleClickEventHandler(myAMC_OnDoubleClick);
            //Ax4.OnDoubleClick += new _IAxisMediaControlEvents_OnDoubleClickEventHandler(myAMC_OnDoubleClick);

            //Ax1.OnJoyDown += new _IAxisMediaControlEvents_OnJoyDownEventHandler(Ax_OnJoyDown);
            //Ax2.OnJoyDown += new _IAxisMediaControlEvents_OnJoyDownEventHandler(Ax_OnJoyDown);
            //Ax3.OnJoyDown += new _IAxisMediaControlEvents_OnJoyDownEventHandler(Ax_OnJoyDown);
            //Ax4.OnJoyDown += new _IAxisMediaControlEvents_OnJoyDownEventHandler(Ax_OnJoyDown);

            //Ax1.OnKeyDown += new _IAxisMediaControlEvents_OnKeyDownEventHandler(Ax_OnKeyDown);
            //Ax2.OnKeyDown += new _IAxisMediaControlEvents_OnKeyDownEventHandler(Ax_OnKeyDown);
            //Ax3.OnKeyDown += new _IAxisMediaControlEvents_OnKeyDownEventHandler(Ax_OnKeyDown);
            //Ax4.OnKeyDown += new _IAxisMediaControlEvents_OnKeyDownEventHandler(Ax_OnKeyDown);

            //Ax1.OnKeyUp += new _IAxisMediaControlEvents_OnKeyUpEventHandler(Ax_OnKeyUp);
            //Ax2.OnKeyUp += new _IAxisMediaControlEvents_OnKeyUpEventHandler(Ax_OnKeyUp);
            //Ax3.OnKeyUp += new _IAxisMediaControlEvents_OnKeyUpEventHandler(Ax_OnKeyUp);
            //Ax4.OnKeyUp += new _IAxisMediaControlEvents_OnKeyUpEventHandler(Ax_OnKeyUp);


            PnlAxis.Controls.Add(Ax1);
            Size_Loc_Set(Ax1, 1);
            PnlAxis.Controls.Add(Ax2);
            Size_Loc_Set(Ax2, 1);
            //2분할로 변경(2020-02-12 한민호)
            //PnlAxis.Controls.Add(Ax3);
            //Size_Loc_Set(Ax3, 1);
            //PnlAxis.Controls.Add(Ax4);
            //Size_Loc_Set(Ax4, 1);

            lbl1.Visible = false;
            lbl2.Visible = false;
            //2분할로 변경(2020-02-12 한민호)
            //lbl3.Visible = false;
            //lbl4.Visible = false;

            pnllbl.Controls.Add(lbl1);
            pnllbl.Controls.Add(lbl2);
            //2분할로 변경(2020-02-12 한민호)
            //pnllbl.Controls.Add(lbl3);
            //pnllbl.Controls.Add(lbl4);
        }

        private void Size_Loc_Set(AxAxisMediaControl Ax, int Cnt) //카메라 탭인덱스 별로 정렬
        {
            Ax.Width = (tabMain.Width / 2) - 10;

            //2분할로 변경(2020-02-12 한민호)
            Ax.Height = (tabMain.Height) - 30;
            //Ax.Height = (tabMain.Height / 2) - 30;

            switch (Ax.TabIndex)
            {
                case 0:
                    Ax.Top = 5;
                    Ax.Left = 3;
                    Application.DoEvents();
                    break;
                case 1:
                    Ax.Top = 5;
                    Ax.Left = (tabMain.Width / 2) - 3;
                    Application.DoEvents();
                    break;
                //2분할로 변경(2020-02-12 한민호)
                //case 2:
                //    Ax.Top = (tabMain.Height / 2) - 20;
                //    Ax.Left = 3;
                //    Application.DoEvents();
                //    break;
                //case 3:
                //    Ax.Top = (tabMain.Height / 2) - 20;
                //    Ax.Left = (tabMain.Width / 2) - 3;
                //    Application.DoEvents();
                //    break;
            }
        }

        #endregion
        
        #region 하화지 변경시 이벤트
        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            DevExpress.XtraTab.XtraTabPage tbPrv = e.PrevPage;
            DevExpress.XtraTab.XtraTabPage tbCur = e.Page;

            if (tbPrv != null)
            {
                Control.ControlCollection listCon = tbPrv.Controls[0].Controls;
                while (listCon.Count != 0)
                {
                    AxAXISMEDIACONTROLLib.AxAxisMediaControl ax = listCon[0] as AxAXISMEDIACONTROLLib.AxAxisMediaControl;

                    if (ax != null)
                    {
                        try
                        {
                            ax.Dispose();
                        }
                        catch { }
                    }
                }
            }
            if (tbCur != null)
            {
                int idx = listTabPages.IndexOf(tbCur);
                if (idx >= 0)
                    fnSetCam(idx, tbCur, tbCur.Controls[0] as DevExpress.XtraEditors.PanelControl);
            }

            SetCamera(e.Page.Name); //카메라 새로고침

            StringBuilder QueryString = new StringBuilder();
            int ChkCnt = 0;
            if (e.PrevPage != null)
            {
                //e.PrevPage.Controls.Clear();
                //foreach (DevExpress.XtraEditors.PanelControl pnl in e.PrevPage.Controls)
                //{
                //    foreach (AxAxisMediaControl axx in pnl.Controls)
                //    {
                //        if (axx.Tag.ToString() == "PLAY")
                //        {
                //            axx.Stop();
                //            axx.Tag = "STOP";
                //        }
                //    }
                //}
            }


            foreach (DevExpress.XtraEditors.PanelControl pnl in e.Page.Controls)
            {
                foreach (AxAxisMediaControl axx in pnl.Controls)
                {
                    ChkCnt += 1;
                    Size_Loc_Set(axx, ChkCnt);
                }
            }

            foreach (DevExpress.XtraEditors.SimpleButton btn in grdCarmera.Controls)
            {
                Enable_click(btn);//카메라 재생 처리 
            }

        }
        #endregion

        #region 페이지 이동시 카메라 활성 비활성 적용 이벤트
        private void Enable_click(DevExpress.XtraEditors.SimpleButton btn)
        {
            //int PlayCnt = 0;

            try
            {
                foreach (Control pnl in tabMain.SelectedTabPage.Controls)
                {

                    DevExpress.XtraEditors.PanelControl Getpnl = new DevExpress.XtraEditors.PanelControl();
                    if (pnl.GetType() == Getpnl.GetType())
                    {

                        foreach (Control axx in pnl.Controls)
                        {
                            if (axx.GetType() == typeof(AxAxisMediaControl))
                            {
                                AxAxisMediaControl Tempaxx = (AxAxisMediaControl)axx;
                                if (axx.Name == btn.Name.ToString())
                                {
                                    if (Tempaxx.Tag.ToString() == "STOP")
                                    {
                                        try
                                        {
                                            btn.ForeColor = Color.Red;
                                            btn.Appearance.BorderColor = Color.Red;
                                            btn.BackColor = Color.DarkSalmon;
                                            Tempaxx.Stop();
                                            // Set properties, deciding what url completion to use by MediaType.
                                            Tempaxx.MediaUsername = "root";
                                            Tempaxx.MediaPassword = "zaq1@wsx";
                                            Tempaxx.PTZControlURL = "http://" + btn.Tag.ToString() + "/axis-cgi/com/ptz.cgi";

                                            // StringBuilder QueryString = new StringBuilder();

                                            string Query1 = "SELECT REC_FLAG,REC_USR from TB_CCTV_4D202 WHERE CAMERA_CD = '" + Tempaxx.Name + "' AND NVL(PLACE_FG, 'P') = 'P'"; // 허용범 수정(테스트 보류)
                                            /*DataSet Findds = OctoCommonV2.CompressionData.DeCompressData(Convert.FromBase64String(svcInspectHandling1.GetQuery(Query1.ToString())));
                                            DataTable Finddt = Findds.Tables[0];*/

                                            DataSet Findds = DBConn._ExecuteDataSet(Query1.ToString());
                                            DataTable Finddt = Findds.Tables[0];

                                            string REC_FLAG = Finddt.Rows[0]["REC_FLAG"].ToString();
                                            string REC_USR = Finddt.Rows[0]["REC_USR"].ToString();



                                            Tempaxx.ShowStatusBar = false;
                                            Tempaxx.EnableContextMenu = true;
                                            Tempaxx.StretchToFit = true;
                                            Tempaxx.MediaType = "h264";
                                            Tempaxx.MediaURL = CompleteURL(btn.Tag.ToString(), "h264");

                                            //Tempaxx.Tag = "PLAY";

                                            Tempaxx.AudioReceiveURL = null;
                                            Tempaxx.AudioTransmitURL = "http://" + btn.Tag.ToString() + "/axis-cgi/audio/transmit.cgi";
                                            Tempaxx.AudioReceiveStop();
                                            Tempaxx.StopTransmitMedia();

                                            Tempaxx.H264VideoRenderer = 0x1000;
                                            Tempaxx.H264VideoDecodingMode = 3;
                                            // Start the streaming

                                            Tempaxx.Play();



                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(this, "Unable to play stream: " + ex.Message);
                                        }
                                    }
                                    else if (Tempaxx.Tag.ToString() == "PLAY")
                                    {
                                        btn.ForeColor = Color.Red;
                                        btn.Appearance.BorderColor = Color.Red;
                                        btn.BackColor = Color.DarkSalmon;

                                        Tempaxx.Stop();
                                        // Set properties, deciding what url completion to use by MediaType.
                                        Tempaxx.MediaUsername = "root";
                                        Tempaxx.MediaPassword = "zaq1@wsx";
                                        Tempaxx.PTZControlURL = "http://" + btn.Tag.ToString() + "/axis-cgi/com/ptz.cgi";



                                        Tempaxx.Tag = "PLAY";
                                        Tempaxx.ShowStatusBar = false;
                                        Tempaxx.EnableContextMenu = true;
                                        Tempaxx.StretchToFit = true;
                                        Tempaxx.MediaType = "h264";
                                        Tempaxx.MediaURL = CompleteURL(btn.Tag.ToString(), "h264");

                                        Tempaxx.H264VideoRenderer = 0x1000;
                                        Tempaxx.H264VideoDecodingMode = 3;
                                        // Start the streaming

                                        Tempaxx.Play();

                                        Tempaxx.AudioTransmitURL = null;
                                        Tempaxx.AudioReceiveStop();
                                        Tempaxx.AudioTransmitStop();

                                    }
                                    else
                                    {
                                        string Query1 = "SELECT REC_FLAG,REC_USR from TB_CCTV_4D202 WHERE CAMERA_CD = '" + Tempaxx.Name + "' AND NVL(PLACE_FG, 'P') = 'P'"; // 허용범 수정
                                        /*DataSet Findds = OctoCommonV2.CompressionData.DeCompressData(Convert.FromBase64String(svcInspectHandling1.GetQuery(Query1.ToString())));
                                        DataTable Finddt = Findds.Tables[0];*/

                                        DataSet Findds = DBConn._ExecuteDataSet(Query1.ToString());
                                        DataTable Finddt = Findds.Tables[0];

                                        string REC_FLAG = Finddt.Rows[0]["REC_FLAG"].ToString();
                                        string REC_USR = Finddt.Rows[0]["REC_USR"].ToString();


                                        Tempaxx.UIMode = "ptz-relative-no-cross";
                                        Tempaxx.Stop();
                                        // Set properties, deciding what url completion to use by MediaType.
                                        Tempaxx.MediaUsername = "root";
                                        Tempaxx.MediaPassword = "zaq1@wsx";
                                        Tempaxx.PTZControlURL = "http://" + btn.Tag.ToString() + "/axis-cgi/com/ptz.cgi";



                                        Tempaxx.Tag = "REC";
                                        Tempaxx.ShowStatusBar = false;
                                        Tempaxx.EnableContextMenu = true;
                                        Tempaxx.StretchToFit = true;
                                        Tempaxx.MediaType = "h264";
                                        Tempaxx.MediaURL = CompleteURL(btn.Tag.ToString(), "h264");

                                        Tempaxx.H264VideoRenderer = 0x1000;
                                        Tempaxx.H264VideoDecodingMode = 3;
                                        // Start the streaming

                                        Tempaxx.Play();

                                        Tempaxx.AudioTransmitURL = null;
                                        Tempaxx.AudioReceiveStop();
                                        Tempaxx.AudioTransmitStop();
                                    }
                                }
                            }
                        }


                    }


                }

            }
            catch (ArgumentException ArgEx)
            {
                MessageBox.Show(ArgEx.Message, "Error");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }
        }
        #endregion

        #region 카메라 연결 주소 생성 이벤트
        private string CompleteURL(string theMediaURL, string theMediaType)
        {
            string anURL = theMediaURL;
            if (!anURL.EndsWith("/")) anURL += "/";

            if (theMediaType == "mjpeg")
            {
                anURL += "axis-cgi/mjpg/video.cgi";
            }
            else if (theMediaType == "mpeg4")
            {
                anURL += "mpeg4/media.amp";
            }
            else if (theMediaType == "h264")
            {
                anURL += "axis-media/media.amp?videocodec=h264";
            }
            else if (theMediaType == "mpeg2-unicast")
            {
                anURL += "axis-cgi/mpeg2/video.cgi";
            }
            else if (theMediaType == "mpeg2-multicast")
            {
                anURL += "axis-cgi/mpeg2/video.cgi";
            }

            anURL = CompleteProtocol(anURL, theMediaType);
            return anURL;
        }
        private string CompleteProtocol(string theMediaURL, string theMediaType)
        {
            if (theMediaURL.IndexOf("://") >= 0) return theMediaURL;

            string anURL = theMediaURL;

            if (theMediaType == "mjpeg")
            {
                anURL = "http://" + anURL;
            }
            else if (theMediaType == "mpeg4" || theMediaType == "h264")
            {
                anURL = "axrtsp://" + anURL;
            }
            else if (theMediaType == "mpeg2-unicast")
            {
                anURL = "http://" + anURL;
            }
            else if (theMediaType == "mpeg2-multicast")
            {
                anURL = "axsdp" + anURL;
            }

            return anURL;
        }
        #endregion

        #region 새로고칠때 쓰는 카메라 설정
        private void SetCamera(string Name)
        {
            StringBuilder QueryString = new StringBuilder();
            QueryString.Append("     SELECT  *   ");
            QueryString.Append("     FROM  (    SELECT CAMERA_CD1 CAMERA_CD,B.IP_ADDR,B.CAMERA_NAME,B.REC_FLAG,B.REC_USR, '' AS REC_VWNO FROM TB_WS01_0020 A  ");
            QueryString.Append("                INNER JOIN TB_WS01_0021 B ");
            QueryString.Append("                ON CAMERA_CD1 = B.CAMERA_CD    ");
            QueryString.Append("                AND NVL(A.PLACE_FG, 'P') = 'P' AND NVL(B.PLACE_FG, 'P') = 'P'    ");
            QueryString.Append("                WHERE  NVL(CAMERA_CD1,' ') <> ' ' AND  A.LOC_CD='" + clsUserInfo.Place + "'");
            QueryString.Append("     UNION ALL  SELECT CAMERA_CD2 CAMERA_CD,B.IP_ADDR,B.CAMERA_NAME,B.REC_FLAG,B.REC_USR, '' AS REC_VWNO FROM TB_WS01_0020 A ");
            QueryString.Append("                INNER JOIN TB_WS01_0021 B ");
            QueryString.Append("                ON CAMERA_CD2 = B.CAMERA_CD    ");
            QueryString.Append("                AND NVL(A.PLACE_FG, 'P') = 'P' AND NVL(B.PLACE_FG, 'P') = 'P'    ");
            QueryString.Append("                WHERE  NVL(CAMERA_CD2,' ') <> ' ' AND  A.LOC_CD='" + clsUserInfo.Place + "'");
            //2분할로 변경(2020-02-12 한민호)
            //QueryString.Append("     UNION ALL  SELECT CAMERA_CD3 CAMERA_CD,B.IP_ADDR,B.CAMERA_NAME,B.REC_FLAG,B.REC_USR, '' AS REC_VWNO FROM TB_WS01_0020 A  ");
            //QueryString.Append("                INNER JOIN TB_WS01_0021 B ");
            //QueryString.Append("                ON CAMERA_CD3 = B.CAMERA_CD    ");
            //QueryString.Append("                AND NVL(A.PLACE_FG, 'P') = 'P' AND NVL(B.PLACE_FG, 'P') = 'P'    ");
            //QueryString.Append("                WHERE  NVL(CAMERA_CD3,' ') <> ' ' AND  A.LOC_CD='" + clsUserInfo.Place + "'");
            //QueryString.Append("      UNION ALL SELECT CAMERA_CD4 CAMERA_CD,B.IP_ADDR,B.CAMERA_NAME,B.REC_FLAG,B.REC_USR, '' AS REC_VWNO FROM TB_WS01_0020 A  ");
            //QueryString.Append("                 INNER JOIN TB_WS01_0021 B ");
            //QueryString.Append("                ON CAMERA_CD4 = B.CAMERA_CD    ");
            //QueryString.Append("                AND NVL(A.PLACE_FG, 'P') = 'P' AND NVL(B.PLACE_FG, 'P') = 'P'    ");
            //QueryString.Append("                WHERE  NVL(CAMERA_CD4,' ') <> ' ' AND  A.LOC_CD='" + clsUserInfo.Place + "'");
            QueryString.Append("           ) B ");

            //QueryString.Append("     SELECT  *   ");
            //QueryString.Append("     FROM  (    SELECT 'CM2011120014' AS CAMERA_CD, '192.168.10.22' AS IP_ADDR,'상A1' AS CAMERA_NAME, 'N' AS REC_FLAG, '' AS REC_USR, '' AS REC_VWNO FROM DUAL");
            //QueryString.Append("     UNION ALL  SELECT 'CM2011120026' AS CAMERA_CD, '192.168.10.22' AS IP_ADDR,'상A1' AS CAMERA_NAME, 'N' AS REC_FLAG, '' AS REC_USR, '' AS REC_VWNO FROM DUAL");
            //QueryString.Append("     UNION ALL  SELECT 'CM2011120025' AS CAMERA_CD, '192.168.10.22' AS IP_ADDR,'상A1' AS CAMERA_NAME, 'N' AS REC_FLAG, '' AS REC_USR, '' AS REC_VWNO FROM DUAL");
            //QueryString.Append("     UNION ALL  SELECT 'CM2011120031' AS CAMERA_CD, '192.168.10.22' AS IP_ADDR,'상A1' AS CAMERA_NAME, 'N' AS REC_FLAG, '' AS REC_USR, '' AS REC_VWNO FROM DUAL) B");

            //2019-04-22
            DataSet ds = DBConn._ExecuteDataSet(QueryString.ToString());
            //DataSet ds = db.ExecuteDataSet(QueryString.ToString());
            DataTable dt = ds.Tables[0];
            //db.DBClose();

            grdCarmera.Controls.Clear();

            for (int Cnt = 0; Cnt < dt.Rows.Count; Cnt++)
            {
                int chkint = 0;
                DevExpress.XtraEditors.SimpleButton btn = null;
                foreach (DevExpress.XtraEditors.SimpleButton bbtn in grdCarmera.Controls)
                {
                    if (bbtn.Name == dt.Rows[Cnt]["CAMERA_CD"].ToString())
                    {
                        chkint += 1;
                    }
                }

                if (chkint == 0)
                {
                    btn = new DevExpress.XtraEditors.SimpleButton();
                    btn.Name = dt.Rows[Cnt]["CAMERA_CD"].ToString();
                    btn.Font = new Font("맑은 굴림", 12, FontStyle.Bold);
                    btn.Text = dt.Rows[Cnt]["CAMERA_NAME"].ToString();

                    //btn.ImageList = imageList3;
                    btn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                    btn.Tag = dt.Rows[Cnt]["IP_ADDR"].ToString();
                    btn.ToolTip = dt.Rows[Cnt]["REC_FLAG"].ToString();


                    btn.ForeColor = Color.Black;
                    btn.BackColor = Color.Wheat;

                    btn.Appearance.Options.UseBackColor = true;
                    grdCarmera.Controls.Add(btn);
                }
                else
                {
                    foreach (DevExpress.XtraEditors.SimpleButton bbtn in grdCarmera.Controls)
                    {
                        if (bbtn.Name == dt.Rows[Cnt]["CAMERA_CD"].ToString())
                        {
                            btn = bbtn;
                        }
                    }
                }

                foreach (Control pnl in tabMain.SelectedTabPage.Controls)
                {
                    DevExpress.XtraEditors.PanelControl Getpnl = new DevExpress.XtraEditors.PanelControl();
                    if (pnl.GetType() == Getpnl.GetType())
                    {
                        foreach (Control axx in pnl.Controls)
                        {
                            if (axx.GetType() == typeof(AxAxisMediaControl))
                            {
                                if (btn.Name.ToString() == axx.Name)
                                {

                                    btn.Enabled = true;
                                    btn.ForeColor = Color.Red;
                                    btn.Appearance.BorderColor = Color.Red;
                                    btn.BackColor = Color.DarkSalmon;
                                    if (axx.Dock == DockStyle.Fill)
                                    {
                                        btn.BackColor = Color.YellowGreen;
                                    }
                                    //왜 막혀 있지(2019-08-17 한민호 수정)
                                    //여기서부터
                                    if (dt.Rows[Cnt]["REC_FLAG"].ToString() == "Y")
                                    {
                                        axx.Tag = "REC";
                                        btn.Enabled = true;
                                        btn.ForeColor = Color.Red;
                                        btn.Appearance.BorderColor = Color.Red;
                                        btn.BackColor = Color.DarkSalmon;

                                        ((AxAxisMediaControl)axx).UIMode = "ptz-relative-no-cross";
                                        if (axx.Dock == DockStyle.Fill)
                                        {
                                            btn.BackColor = Color.YellowGreen;
                                        }

                                    }
                                    else
                                    {
                                        axx.Tag = "PLAY";
                                        ((AxAxisMediaControl)axx).UIMode = "ptz-relative-no-cross";
                                        btn.Enabled = true;
                                        btn.ForeColor = Color.Red;
                                        btn.Appearance.BorderColor = Color.Red;

                                        if (btn.BackColor == Color.YellowGreen)
                                        {
                                            btn.BackColor = Color.YellowGreen;
                                        }
                                        else
                                        {
                                            btn.BackColor = Color.DarkSalmon;
                                        }

                                    }
                                }
                                else if (btn.Name.ToString() == axx.Name && (axx.Tag.ToString() == "STOP"))
                                {
                                    btn.Appearance.BorderColor = Color.White;
                                    btn.ForeColor = Color.Black;
                                    btn.BackColor = Color.Wheat;
                                    ((AxAxisMediaControl)axx).UIMode = "ptz-relative-no-cross";
                                    if (axx.Dock == DockStyle.Fill)
                                    {
                                        btn.ForeColor = Color.Red;
                                        btn.Appearance.BorderColor = Color.Red;
                                        btn.BackColor = Color.YellowGreen;
                                    }
                                    btn.Image = null;
                                }
                            }

                        }

                    }
                }

            }
        }
        #endregion

        #endregion

        private void pnlEntryTitle_Click(object sender, EventArgs e)
        {
            pnlEntry.BackColor = Color.Yellow;
            pnlAdvance.BackColor = Color.Gainsboro;
        }
        private void pnlAdvanceTitle_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pnlAdvanceTitle_Click(object sender, EventArgs e)
        {
            pnlEntry.BackColor = Color.Gainsboro;
            pnlAdvance.BackColor = Color.Yellow;
        }

        private void lblWeight_value_IN_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblWeight_value_IN.Text.Replace(",","")) > 0)
                lblWeight_kg_IN.ForeColor = Color.Red;
            else
                lblWeight_kg_IN.ForeColor = Color.Blue;
        }

        private void lblWeight_value_OUT_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblWeight_value_OUT.Text.Replace(",", "")) > 0)
                lblWeight_kg_OUT.ForeColor = Color.Red;
            else
                lblWeight_kg_OUT.ForeColor = Color.Blue;
        }

        private void btnSeinSave_Click(object sender, EventArgs e)
        {
            SeinPopup Return = new SeinPopup();
            if (Return.ShowDialog() == DialogResult.Yes)
            {

            }
            if (!(btnItemType2.ForeColor == Color.Red && rdReceiptWait.Checked == true))    //수입고철은 날짜 구분이 없어서 제외
                QueryClick();
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
                //String Query = " SELECT CODE_VALUE1 AS CODE FROM TB_WS01_0002 WHERE TYPE_CD = 'WS_006' AND CODE = '" + clsUserInfo.Place.Substring(0, 2) + "' AND USE_YN = 'Y' AND DEL_YN = 'N' ";
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
                    //출입관리 추가(2020-05-07 한민호)
                    case "일반방문":
                        e.Appearance.BackColor = Color.PaleTurquoise;
                        break;
                }
            }
            //입차 된 건은 호출완료 색깔로 되어 버리기 때문에 호출완료 색깔 따로 지정(2020-03-26 한민호)
            string smsret = gvwMaster.GetRowCellValue(e.RowHandle, "SMS_REG_DT").ToString();
            if (smsret != "" && rdWeightWait.Checked == true)
                e.Appearance.BackColor = Color.LightCyan;
        }

        private void gvwMaster3_DoubleClick(object sender, EventArgs e)
        {
            if (gvwMaster3.RowCount == 0)
            {
                return;
            }

            VehlimgPopup vehlpop = new VehlimgPopup();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_SEQ_NO", gvwMaster3.GetFocusedRowCellValue("SEQ_NO").ToString());

                DataSet ds = DBConn.ExecuteDataSet2("SP_WEIGHT_IMG_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    vehlpop.IN_IMG_PATH = ds.Tables[0].Rows[0]["IN_IMG_PATH"].ToString();
                    vehlpop.OUT_IMG_PATH = ds.Tables[0].Rows[0]["OUT_IMG_PATH"].ToString();
                    //차량번호, 물류구분, 품명구분 추가(2020-02-18 한민호)
                    vehlpop.CAR_NO = ds.Tables[0].Rows[0]["CAR_NO"].ToString();
                    vehlpop.ITEM_TYPE_NM = ds.Tables[0].Rows[0]["ITEM_TYPE_NM"].ToString();
                    vehlpop.ITEM_NM = ds.Tables[0].Rows[0]["ITEM_NM"].ToString();
                }
                else
                {
                    vehlpop.IN_IMG_PATH = "";
                    vehlpop.OUT_IMG_PATH = "";
                }
            }
            catch (Exception ex)
            {
                vehlpop.IN_IMG_PATH = "";
                vehlpop.OUT_IMG_PATH = "";
            }
            finally
            {
                vehlpop.getImg();
                vehlpop.ShowDialog();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (pnlEntry.BackColor != Color.Yellow && pnlAdvance.BackColor != Color.Yellow)
            {
                MessageBox.Show("진입 계량기 나 진출 계량기 둘 중 하나는 선택 되어야 합니다.");
                return;
            }
            if (rdWeightComplete.Checked == false)
            {
                MessageBox.Show("계량완료만 전표출력 할 수 있습니다.");
                return;
            }
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("P_SEQ_NO", gvwMaster.GetFocusedRowCellValue("SEQ_NO").ToString());
            DataSet ds = DBConn.ExecuteDataSet2("SP_MU_PRINT_SO_NEW", dict);
            dt_pnt_qty = ds.Tables[0];

            if (dt_pnt_qty.Rows[0]["ITEM_TYPE_NM"].ToString() == "원자재")
            {
                SubMatrial_원자재 Print_etc = new SubMatrial_원자재(dt_pnt_qty);

                //네트워크 프린트 출력(2020-03-28 한민호);
                string LR = "";
                if (pnlEntry.BackColor == Color.Yellow)             //진입계량기
                {
                    LR = "11";
                    //Print_etc.PrinterName = "\\\\192.168.20.42\\120011";
                }
                else if (pnlAdvance.BackColor == Color.Yellow)      //진출계량기
                {
                    LR = "12";
                    //Print_etc.PrinterName = "\\\\192.168.20.41\\120012";
                }
                ////녹산정문입문 : \\\\192.168.20.42\\120011 
                ////녹산정문출문 : \\\\192.168.20.41\\120012
                String Query = " SELECT CODE_VALUE2 AS CODE FROM TB_WS01_0002 WHERE TYPE_CD = 'WS_005' AND CODE_VALUE1 = '" + clsUserInfo.Place + LR + "' AND USE_YN = 'Y' AND DEL_YN = 'N' ";
                DataSet ds1 = DBConn._ExecuteDataSet(Query);
                DataTable dt1 = ds1.Tables[0];
                string PrintName = dt1.Rows[0]["CODE"].ToString();
                Print_etc.PrinterName = PrintName;

                //미리보기 화면
                //Print_etc.ShowPreview();
                //인쇄
                Print_etc.Print();
                Print_etc.Dispose();
            }
            else
            {
                SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);

                //네트워크 프린트 출력(2020-03-28 한민호);
                string LR = "";
                if (pnlEntry.BackColor == Color.Yellow)             //진입계량기
                {
                    LR = "11";
                    //Print_etc.PrinterName = "\\\\192.168.20.42\\120011";
                }
                else if (pnlAdvance.BackColor == Color.Yellow)      //진출계량기
                {
                    LR = "12";
                    //Print_etc.PrinterName = "\\\\192.168.20.41\\120012";
                }
                ////녹산정문입문 : \\\\192.168.20.42\\120011 
                ////녹산정문출문 : \\\\192.168.20.41\\120012
                String Query = " SELECT CODE_VALUE2 AS CODE FROM TB_WS01_0002 WHERE TYPE_CD = 'WS_005' AND CODE_VALUE1 = '" + clsUserInfo.Place + LR + "' AND USE_YN = 'Y' AND DEL_YN = 'N' ";
                DataSet ds1 = DBConn._ExecuteDataSet(Query);
                DataTable dt1 = ds1.Tables[0];
                string PrintName = dt1.Rows[0]["CODE"].ToString();
                Print_etc.PrinterName = PrintName;

                //미리보기 화면
                //Print_etc.ShowPreview();
                //인쇄
                Print_etc.Print();
                Print_etc.Dispose();
            }


            //DB_Process.PRINT_PAGE_CNT_SET(Weight_Area, 1);  //출력매수1장
        }

        private void btnPrint1_Click(object sender, EventArgs e)
        {
            if (pnlEntry.BackColor != Color.Yellow && pnlAdvance.BackColor != Color.Yellow)
            {
                MessageBox.Show("진입 계량기 나 진출 계량기 둘 중 하나는 선택 되어야 합니다.");
                return;
            }

            //if (!(gvwMaster2.GetFocusedRowCellValue("DIS_CLASS").ToString() == "원자재" || gvwMaster2.GetFocusedRowCellValue("DIS_CLASS").ToString() == "수입원자재"))
            //{
            //    MessageBox.Show("원자재 및 수입원자재 만 1차계량전표 출력 가능합니다.");
            //    return;
            //}

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("P_AW_SEQ", gvwMaster2.GetFocusedRowCellValue("AW_SEQ").ToString());
            DataSet ds = DBConn.ExecuteDataSet2("SP_MU_PRINT_SO_MANUAL", dict);
            dt_pnt_qty = ds.Tables[0];

            if (dt_pnt_qty.Rows[0]["ITEM_TYPE_NM"].ToString() == "원자재")
            {
                SubMatrial_원자재 Print_etc = new SubMatrial_원자재(dt_pnt_qty);

                //네트워크 프린트 출력(2020-03-28 한민호);
                string LR = "";
                if (pnlEntry.BackColor == Color.Yellow)             //진입계량기
                {
                    LR = "11";
                    //Print_etc.PrinterName = "\\\\192.168.20.42\\120011";
                }
                else if (pnlAdvance.BackColor == Color.Yellow)      //진출계량기
                {
                    LR = "12";
                    //Print_etc.PrinterName = "\\\\192.168.20.41\\120012";
                }
                ////녹산정문입문 : \\\\192.168.20.42\\120011 
                ////녹산정문출문 : \\\\192.168.20.41\\120012
                String Query = " SELECT CODE_VALUE2 AS CODE FROM TB_WS01_0002 WHERE TYPE_CD = 'WS_005' AND CODE_VALUE1 = '" + clsUserInfo.Place + LR + "' AND USE_YN = 'Y' AND DEL_YN = 'N' ";
                DataSet ds1 = DBConn._ExecuteDataSet(Query);
                DataTable dt1 = ds1.Tables[0];
                string PrintName = dt1.Rows[0]["CODE"].ToString();
                Print_etc.PrinterName = PrintName;

                //미리보기 화면
                //Print_etc.ShowPreview();
                //인쇄
                Print_etc.Print();
                Print_etc.Dispose();

                //DB_Process.PRINT_PAGE_CNT_SET(Weight_Area, 1);  //출력매수1장
            }
            else
            {
                SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);

                //네트워크 프린트 출력(2020-03-28 한민호);
                string LR = "";
                if (pnlEntry.BackColor == Color.Yellow)             //진입계량기
                {
                    LR = "11";
                    //Print_etc.PrinterName = "\\\\192.168.20.42\\120011";
                }
                else if (pnlAdvance.BackColor == Color.Yellow)      //진출계량기
                {
                    LR = "12";
                    //Print_etc.PrinterName = "\\\\192.168.20.41\\120012";
                }
                ////녹산정문입문 : \\\\192.168.20.42\\120011 
                ////녹산정문출문 : \\\\192.168.20.41\\120012
                String Query = " SELECT CODE_VALUE2 AS CODE FROM TB_WS01_0002 WHERE TYPE_CD = 'WS_005' AND CODE_VALUE1 = '" + clsUserInfo.Place + LR + "' AND USE_YN = 'Y' AND DEL_YN = 'N' ";
                DataSet ds1 = DBConn._ExecuteDataSet(Query);
                DataTable dt1 = ds1.Tables[0];
                string PrintName = dt1.Rows[0]["CODE"].ToString();
                Print_etc.PrinterName = PrintName;

                //미리보기 화면
                //Print_etc.ShowPreview();
                //인쇄
                Print_etc.Print();
                Print_etc.Dispose();

                //DB_Process.PRINT_PAGE_CNT_SET(Weight_Area, 1);  //출력매수1장
            }

        }


        //계량완료전표 출력
        private void btnPrint2_Click(object sender, EventArgs e)
        {
            if (pnlEntry.BackColor != Color.Yellow && pnlAdvance.BackColor != Color.Yellow)
            {
                MessageBox.Show("진입 계량기 나 진출 계량기 둘 중 하나는 선택 되어야 합니다.");
                return;
            }

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("P_AW_SEQ", gvwMaster3.GetFocusedRowCellValue("AW_SEQ").ToString());
            DataSet ds = DBConn.ExecuteDataSet2("SP_MU_PRINT_SO_MANUAL", dict);
            dt_pnt_qty = ds.Tables[0];

            if (dt_pnt_qty.Rows[0]["ITEM_TYPE_NM"].ToString() == "원자재")
            {
                SubMatrial_원자재 Print_etc = new SubMatrial_원자재(dt_pnt_qty);

                string LR = "";
                if (pnlEntry.BackColor == Color.Yellow)             //진입계량기
                {
                    LR = "11";
                    //Print_etc.PrinterName = "\\\\192.168.20.42\\120011";
                }
                else if (pnlAdvance.BackColor == Color.Yellow)      //진출계량기
                {
                    LR = "12";
                    //Print_etc.PrinterName = "\\\\192.168.20.41\\120012";
                }
                ////녹산정문입문 : \\\\192.168.20.42\\120011 
                ////녹산정문출문 : \\\\192.168.20.41\\120012
                String Query = " SELECT CODE_VALUE2 AS CODE FROM TB_WS01_0002 WHERE TYPE_CD = 'WS_005' AND CODE_VALUE1 = '" + clsUserInfo.Place + LR + "' AND USE_YN = 'Y' AND DEL_YN = 'N' ";
                DataSet ds1 = DBConn._ExecuteDataSet(Query);
                DataTable dt1 = ds1.Tables[0];
                string PrintName = dt1.Rows[0]["CODE"].ToString();
                Print_etc.PrinterName = PrintName;

                //미리보기 화면
                //Print_etc.ShowPreview();
                //인쇄
                Print_etc.Print();
                Print_etc.Dispose();
            }
            else
            {
                SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);

                string LR = "";
                if (pnlEntry.BackColor == Color.Yellow)             //진입계량기
                {
                    LR = "11";
                    //Print_etc.PrinterName = "\\\\192.168.20.42\\120011";
                }
                else if (pnlAdvance.BackColor == Color.Yellow)      //진출계량기
                {
                    LR = "12";
                    //Print_etc.PrinterName = "\\\\192.168.20.41\\120012";
                }
                ////녹산정문입문 : \\\\192.168.20.42\\120011 
                ////녹산정문출문 : \\\\192.168.20.41\\120012
                String Query = " SELECT CODE_VALUE2 AS CODE FROM TB_WS01_0002 WHERE TYPE_CD = 'WS_005' AND CODE_VALUE1 = '" + clsUserInfo.Place + LR + "' AND USE_YN = 'Y' AND DEL_YN = 'N' ";
                DataSet ds1 = DBConn._ExecuteDataSet(Query);
                DataTable dt1 = ds1.Tables[0];
                string PrintName = dt1.Rows[0]["CODE"].ToString();
                Print_etc.PrinterName = PrintName;

                //미리보기 화면
                //Print_etc.ShowPreview();
                //인쇄
                Print_etc.Print();
                Print_etc.Dispose();
            }


            //네트워크 프린트 출력(2020-03-28 한민호);
            //참조 프린터 찾기
            //int pkInstalledPrinters;            
            //for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            //{
            //    pkInstalledPrinters = PrinterSettings.InstalledPrinters[i].IndexOf("\\192.168.20.41\\120011");
            //    if (pkInstalledPrinters >= 0)
            //    {
            //        //rClean_End_Label.PrintOptions.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
            //        //break;
            //    }
            //}

            //임시 막기(2020-04-07 한민호)
            

            //DB_Process.PRINT_PAGE_CNT_SET(Weight_Area, 1);  //출력매수1장


            //String Query = null; ;
            ////계량대코드로 마지막 일련번호 찾기(clsUserInfo.Place 는 계량대위치로 1100, 1200, 1210, 1300 임. 진입계량기는 WS_001의 2번째 자리가 L은 입문, R은 출문 임)
            ////공통코드 WS_001 의 코드값1의 앞자리4자리가 사업장 계량대 임(1100, 1200, 1210, 1300)
            //Query = " SELECT DISTINCT SUBSTR(CODE,1,1) AS CODE FROM TB_WS01_0002 WHERE TYPE_CD = 'WS_001' AND SUBSTR(CODE_VALUE1,1,4) = '" + clsUserInfo.Place + "' AND USE_YN = 'Y' AND DEL_YN = 'N' ";
            //DataSet ds1 = DBConn._ExecuteDataSet(Query);
            //DataTable dt1 = ds1.Tables[0];
            //string CD = dt1.Rows[0]["CODE"].ToString(); //사업장 A:신평, B:녹산정문, C:녹산후문, D:평택

            ////녹산정문(BL), 녹산후문(CL)
            //string LR = "";
            //if (pnlEntry.BackColor == Color.Yellow)             //진입계량기
            //{
            //    LR = "L";
            //}
            //else if (pnlAdvance.BackColor == Color.Yellow)      //진출계량기
            //{
            //    LR = "R";
            //}
            ////해당계량대의 2차계량된 마지막 값을 찾아야 함
            //Query = " SELECT MAX(SEQ_NO) AS SEQ_NO FROM TB_WS02_0002 WHERE PLNT_NO = '" + clsUserInfo.Place + "' AND SUBSTR(OUT_STATE,1,2) = '" + CD + LR + "' AND OUT_WGT_DT IS NOT NULL ";
            //DataSet ds2 = DBConn._ExecuteDataSet(Query);
            //DataTable dt2 = ds2.Tables[0];
            //string seq_no = dt2.Rows[0]["SEQ_NO"].ToString();
            //if (seq_no == "" || seq_no == null)
            //{
            //    MessageBox.Show("해당 계량대로 계량표 출력할 정보가 없습니다.");
            //    return;
            //}
            //Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("P_SEQ_NO", seq_no);
            //DataSet ds = DBConn.ExecuteDataSet2("SP_MU_PRINT_SO_NEW", dict);
            //dt_pnt_qty = ds.Tables[0];
            //SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);
            ////미리보기 화면
            //Print_etc.ShowPreview();
            ////인쇄
            ////Print_etc.Print();
            ////Print_etc.Dispose();

            ////DB_Process.PRINT_PAGE_CNT_SET(Weight_Area, 1);  //출력매수1장
        }
 
    }
}