using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using OCT_WEIGHT.Manager.Common.util;
using DevExpress.XtraEditors;
using OCT_WEIGHT.Manager.Common.Popup;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysInnerMng : OCT_WEIGHT.Manager.Common.FrmBase
    {
        public SysInnerMng()
        {
            InitializeComponent();
        }

        #region Search

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                this.Cursor = Cursors.WaitCursor;
                //rfid, 차량번호, 품목명, 거래처, 운전자명, 운전자 연락처, 상차지, 하차지
                /*
                 *     V_RFID_NO      IN VARCHAR2,
    V_VEHL_NO      IN VARCHAR2,
    V_ITEM_JUNG    IN VARCHAR2,
    V_ITEM_SO      IN VARCHAR2,
    V_CUST_CD      IN VARCHAR2,
    V_DRVR_NM      IN VARCHAR2,
    V_DRVR_PHON    IN VARCHAR2,
    V_UP_SITE_CD   IN VARCHAR2,
    V_DOWN_SITE_CD IN VARCHAR2,
                */
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("V_RFID_NO", txtRfidNo_SC.Text.Trim());
                dict.Add("V_VEHL_NO", txtVehlNo_SC.Text.Trim());
                //콤보박스는 다른 방식으로 해야 함(2019-09-28 한민호)
                dict.Add("V_ITEM_JUNG", IProductNM_SC.EditValue.ToString().Replace("<Null>", ""));
                dict.Add("V_ITEM_SO", lITEMSO_SC.EditValue.ToString().Replace("<Null>", ""));
                //dict.Add("V_ITEM_JUNG", IProductNM_SC.GetColumnValue("ITEM_JUNG").ToString().Trim());
                //dict.Add("V_ITEM_SO", lITEMSO_SC.GetColumnValue("CODE").ToString().Trim());

                //dict.Add("V_CUST_CD", lCust.GetColumnValue("CUST_CD").ToString().Trim());
                //dict.Add("V_DRVR_NM", txtDrvrNM.Text.Trim());
                //dict.Add("V_DRVR_PHON", txtDrvrHP.Text.Trim());
                //dict.Add("V_UP_SITE_CD", lUpSite.GetColumnValue("CODE").ToString().Trim());
                //dict.Add("V_DOWN_SITE_CD", lDownSite.GetColumnValue("CODE").ToString().Trim());
                //dict.Add("V_CRT_USER",clsUserInfo.User_id);

                DataSet ds = DBConn.ExecuteDataSet2("SP_InnerMove_R", dict);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdInner.DataSource = ds.Tables[0];
                }
                else
                {
                    MsgBoxUtil.AlertInformation("조회된 데이터가 없습니다.");
                    grdInner.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.AlertError(ex.Message);
                this.Cursor = Cursors.Default;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                //gvwInner.BestFitColumns();
            }
        }

        #endregion

        #region Load

        private void SysInnerMng_Load(object sender, EventArgs e)
        {
            //폼 로드 시 바인딩 1번말 할 것(2019-09-28 한민호)
            SelectUpper(IProductNM, "ITEM_JUNG", "ITEM_JUNG_NM", "6", "SP_ITEMLIST", ""); //사내이송 RM_R02
            SelectUpper(IProductNM_SC, "ITEM_JUNG", "ITEM_JUNG_NM", "6", "SP_ITEMLIST", ""); //사내이송 RM_R02
            //운송사코드 임(2019-10-05 한민호)
            //운송사 TRP_CMP_TP = 'H' 만 가져오게 수정(2019-10-05 한민호)
            SelectUpper(lCust, "SCHA_CD", "SCHA_NAME", "", "SP_SYSSCHA_R2", "V_SCHA_NAME"); //운송사
            //SelectUpper(lCust, "SCHA_CD", "SCHA_NAME", "", "SP_SYSSCHA_R", "V_SCHA_NAME"); //운송사
            //SelectUpper(lCust, "CUST_CD", "CUST_NM", "", "SP_SYSCUST_R", "P_CUST_NAME"); //거래처
            SelectUpper(lUpSite, "CODE", "CODE_NAME", "RM_R02", "SP_COMBOX_R", "P_CUST_NAME"); //상차지
            //행선지 추가(2019-11-11 한민호)
            //빈거 하나 넣기(2019-11-30 한민호)
            SelectUpper(lDownSite, "CODE", "CODE_NAME", "VC_027", "SP_COMBOX_R2", "P_CUST_NAME"); //행선지
            //SelectUpper(lDownSite, "CODE", "CODE_NAME", "VC_027", "SP_COMBOX_R", "P_CUST_NAME"); //행선지
            //SelectUpper(lDownSite, "CODE", "CODE_NAME", "RM_043", "SP_COMBOX_R", "P_CUST_NAME"); //상차지
            //사내이송은 품목분류 6,7을 조회 함(2019-11-06 한민호)
            SelectUpper(lITEMSO, "CODE", "CODE_NAME", "", "SP_ITEMSOLIST", "P_TYPE_CD");
            SelectUpper(lITEMSO_SC, "CODE", "CODE_NAME", "", "SP_ITEMSOLIST", "P_TYPE_CD");
            //SelectUpper(lITEMSO, "CODE", "CODE_NAME", Convert.ToString(6), "SP_ITEMSOLIST", "P_TYPE_CD");
            //SelectUpper(lITEMSO_SC, "CODE", "CODE_NAME", Convert.ToString(6), "SP_ITEMSOLIST", "P_TYPE_CD");
            BtnNew.PerformClick();
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

        #region New

        private void BtnNew_Click(object sender, EventArgs e)
        {
            txtVehlNo.Text = string.Empty;
            txtVehlNo_SC.Text = string.Empty;
            txtRfidNo.Text = string.Empty;
            txtRfidNo_SC.Text = string.Empty;
            txtDrvrNM.Text = string.Empty;
            txtDrvrHP.Text = string.Empty;
            //폼 로드 시 바인딩 1번말 할 것(2019-09-28 한민호)
            //SelectUpper(IProductNM, "ITEM_JUNG", "ITEM_JUNG_NM", "6", "SP_ITEMLIST", ""); //사내이송 RM_R02
            //SelectUpper(IProductNM_SC, "ITEM_JUNG", "ITEM_JUNG_NM", "6", "SP_ITEMLIST", ""); //사내이송 RM_R02
            //SelectUpper(lCust, "CUST_CD", "CUST_NM", "", "SP_SYSCUST_R", "P_CUST_NAME"); //거래처
            //SelectUpper(lUpSite, "CODE", "CODE_NAME", "RM_R02", "SP_COMBOX_R", "P_CUST_NAME"); //상차지
            //SelectUpper(lDownSite, "CODE", "CODE_NAME", "RM_043", "SP_COMBOX_R", "P_CUST_NAME"); //상차지
            //SelectUpper(lITEMSO, "CODE", "CODE_NAME", Convert.ToString(6), "SP_ITEMSOLIST", "P_TYPE_CD");
            //SelectUpper(lITEMSO_SC, "CODE", "CODE_NAME", Convert.ToString(6), "SP_ITEMSOLIST", "P_TYPE_CD");
            lITEMSO.ItemIndex = 0;
            //lITEMSO_SC.ItemIndex = 0;
            IProductNM.ItemIndex = 5;
            IProductNM_SC.ItemIndex = 5;
            lCust.ItemIndex = 0;
            lUpSite.ItemIndex = 0;
            lDownSite.ItemIndex = 0;
            IProductNM.Enabled = false;
            IProductNM_SC.Enabled = false;
            grdInner.DataSource = null;
        }

        #endregion

        #region Save

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                this.Cursor = Cursors.WaitCursor;

                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("V_RFID_NO", txtRfidNo.Text.Trim()); // RFID 카드 번호         
                dict.Add("V_VEHL_CD", txtVehlNo.Text.Trim()); // 차량번호
                dict.Add("V_ITEM_DAE", "1"); // 품목대분류
                dict.Add("V_INOUT_GUBUN", "2"); // 인아웃구분 <- 2 사내이송
                dict.Add("V_ITEM_JUNG", IProductNM.GetColumnValue("ITEM_JUNG").ToString()); // 품목 : 사내이송
                dict.Add("V_ITEM_SO", lITEMSO.GetColumnValue("CODE").ToString()); // 품목코드
                //거래처에서 운송사로 수정(2019-10-05 한민호)
                //NULL 일 경우 예외처리(2019-10-09 한민호)
                dict.Add("V_CUST_CD", lCust.Text == "" || lCust.Text == "[EditValue is null]"  ? "" : lCust.GetColumnValue("SCHA_CD").ToString());  // 운송사코드
                dict.Add("V_CUST_NM", lCust.Text == "" || lCust.Text == "[EditValue is null]" ? "" : lCust.GetColumnValue("SCHA_NAME").ToString());// 운송사명
                //dict.Add("V_CUST_CD", lCust.GetColumnValue("CUST_CD").ToString()); // 거래처코드
                //dict.Add("V_CUST_NM", lCust.GetColumnValue("CUST_NM").ToString()); // 거래처명
                dict.Add("V_DRVR_NM", txtDrvrNM.Text.Trim()); // 운전사명
                dict.Add("V_DRVR_PHON", txtDrvrHP.Text.Trim()); // 운전사 연락처
                //사용안함(2019-10-09 한민호)
                //dict.Add("V_UP_SITE_CD", lUpSite.GetColumnValue("CODE").ToString()); // 상차지코드
                //행선지 추가(2019-12-01 한민호)
                dict.Add("V_DOWN_SITE_CD", lDownSite.GetColumnValue("CODE").ToString()); // 하차지코드

                dict.Add("V_CRT_USER", clsUserInfo.User_id); // 입력자                
                string rtn = DBConn.ExecuteQuerySPR3("SP_TB_WS01_0016_R", dict);

                if (rtn != "-1")
                {
                    MsgBoxUtil.AlertInformation(rtn);
                }
            }
            catch (Exception exception)
            {
                MsgBoxUtil.AlertError(exception.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                //BtnNew.PerformClick();
                BtnSearch.PerformClick();
            }
        }

        #endregion

        #region Excel

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            if (gvwInner.RowCount == 0) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvwInner.ExportToXls(desktop + "\\" + this.Text + ".xls");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                BtnSearch.PerformClick();
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Close

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Edit Value Changed

        private void IProductNM_EditValueChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region FocusedRowChanged

        private void gvwInner_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvwInner.GetFocusedRow() != null)
            {
                GetDataShow(true);
            }
            else
            {
                BtnNew.PerformClick();
            }
        }

        private void GetDataShow(bool isView)
        {
            bool isShow = (isView && gvwInner.DataRowCount > 0) ? true : false;

            if (!isShow)
            {
                txtRfidNo.Text = string.Empty;
                txtRfidNo_SC.Text = string.Empty;
                txtVehlNo.Text = string.Empty;
                txtVehlNo_SC.Text = string.Empty;
                txtDrvrNM.Text = string.Empty;
                txtDrvrHP.Text = string.Empty;

                IProductNM.ItemIndex = 5;
                IProductNM_SC.ItemIndex = 5;
                lITEMSO.ItemIndex = 0;
                //lITEMSO_SC.ItemIndex = 0;
                lCust.ItemIndex = 0;
                lUpSite.ItemIndex = 0;
                lDownSite.ItemIndex = 0;
            }
            else
            {
                DataRow dr = gvwInner.GetDataRow(gvwInner.FocusedRowHandle);
                txtRfidNo.Text = dr["RFID_NO"].ToString();
                txtVehlNo.Text = dr["VEHL_NO"].ToString();
                txtDrvrNM.Text = dr["DRVR_NM"].ToString();
                txtDrvrHP.Text = dr["DRVR_PHON"].ToString();
                IProductNM.EditValue = dr["ITEM_JUNG"].ToString();
                lITEMSO.EditValue = dr["ITEM_SO"].ToString();
                //운송사로 수정(2019-11-04 한민호)
                lCust.EditValue = dr["CARRI_CD"].ToString();
                //lCust.EditValue = dr["CUST_CD"].ToString();

                lUpSite.EditValue = dr["UP_SITE"].ToString();
                lDownSite.EditValue = dr["DOWN_SITE"].ToString();
            }
        }

        #endregion

        #region Grid Click

        private void grdInner_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void txtVehlNo_Click(object sender, EventArgs e)
        {
            VehlPopup vehlPopup = new VehlPopup();
            DialogResult = vehlPopup.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                this.txtVehlNo.Text = vehlPopup.vehl_no;
                //팝업 선택 시 운전자명, 운전자연락처, 운송사 던져주게 수정(2019-10-05 한민호)
                this.txtDrvrNM.Text = vehlPopup.drvrnm;
                this.txtDrvrHP.Text = vehlPopup.drvrhp;
                this.lCust.EditValue = vehlPopup.cust; 
            }
        }

        private void txtVehlNo_SC_Click(object sender, EventArgs e)
        {
            //검색조건은 핼프창 띄우면 안됨(2019-10-06 한민호)
            //VehlPopup vehlPopup = new VehlPopup();
            //DialogResult = vehlPopup.ShowDialog();
            //if (DialogResult == DialogResult.OK)
            //{
            //    this.txtVehlNo_SC.Text = vehlPopup.vehl_no;
            //}
        }
    }
}