using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.util;
using OCT_WEIGHT.Manager.Common.info;
using DevExpress.XtraEditors;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class sysVehlSearch : OCT_WEIGHT.Manager.Common.FrmBase
    {
        public ServiceAdapter _svc = null;

        public sysVehlSearch()
        {
            InitializeComponent();
        }

        #region Form_Load

        private void sysVehlSearch_Load(object sender, EventArgs e)
        {
            this.Text = "차량조회";
            txtVEHLCD.Text = string.Empty;
            txtPRODWGHT.Text = string.Empty;
            txVEHLNO.Text = string.Empty;
            txtCustCD.Text = string.Empty;

            txVEHLNO_SC.Text = string.Empty; // 조회 차량번호

            txtREMARK.Text = string.Empty;
            txVEHLNO.Text = string.Empty;
            txtEMPTY_WEIGHT.Text = string.Empty;
            txtCust_Code.Text = string.Empty;
            txtUSE_YN.Text = string.Empty;
            txtDRVR_NM.Text = string.Empty;
            txtDRVR_PHON.Text = string.Empty;


            //SelectUpper(txtVEHL_FLAG, "CODE", "CODE_NAME", "6", "SP_VEHL_FLAG", ""); //
            //SelectUpper(txtVEHL_KIND, "CODE", "CODE_NAME", "6", "SP_VEHL_KIND", ""); //

            txtVEHL_KIND.ItemIndex = 0;
            txtVEHL_FLAG.ItemIndex = 0;

        }

        #endregion

        #region 조회

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            //try
            //{
            //    Dictionary<string, string> dict = new Dictionary<string, string>();
            //    dict.Add("P_VEHL_NO", txVEHLNO_SC.Text.Trim());

            //    //20190503 임시수정 Kimsw
            //    DataSet ds = DBConn.ExecuteDataSet2("SP_VEHLLIST_R", dict);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        grd_carsearch.DataSource = ds.Tables[0];
            //    }
            //    else
            //    {
            //        MsgBoxUtil.AlertInformation("조회된 자료가 없습니다.");
            //        grd_carsearch.DataSource = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("차량 조회중 ERROR:" + ex.Message.ToString().Trim());
            //}
            //finally
            //{
            //    //gvw_carsearch.BestFitColumns();
            //}
        }

        #endregion

        #region 엑셀

        private void octoButton1_Click(object sender, EventArgs e)
        {
            if (gvw_carsearch.RowCount == 0) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvw_carsearch.ExportToXls(desktop + "\\" + this.Text + ".xls");
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

        #region 종료

        private void octoButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 신규

        private void BtnNew_Click(object sender, EventArgs e)
        {
            //txtVEHLCD.Text = string.Empty;
            //txtPRODWGHT.Text = string.Empty;
            //txVEHLNO.Text = string.Empty;
            //txtCustCD.Text = string.Empty;

            //txVEHLNO_SC.Text = string.Empty; // 조회 차량번호

            //txtREMARK.Text = string.Empty;
            //txVEHLNO.Text = string.Empty;
            //txtEMPTY_WEIGHT.Text = string.Empty;
            //txtCust_Code.Text = string.Empty;
            //txtUSE_YN.Text = string.Empty;
            //txtDRVR_NM.Text = string.Empty;
            //txtDRVR_PHON.Text = string.Empty;


            //SelectUpper(txtVEHL_FLAG, "CODE", "CODE_NAME", "6", "SP_VEHL_FLAG", ""); //
            //SelectUpper(txtVEHL_KIND, "CODE", "CODE_NAME", "6", "SP_VEHL_KIND", ""); //

            //txtVEHL_KIND.ItemIndex = -1;
            //txtVEHL_FLAG.ItemIndex = -1;

        }

        #endregion

        #region 저장

        private void BtnSave_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;

            //    Dictionary<string, string> dict = new Dictionary<string, string>();
            //    dict.Add("P_VEHL_CD", txtVEHLCD.Text.Trim()); // 차량코드
            //    dict.Add("P_VEHL_NO", txVEHLNO.Text.Trim()); // 차량번호
            //    dict.Add("P_VEHL_KIND", txtVEHL_KIND.EditValue.ToString()); // 차량종류
                
            //    dict.Add("P_EMPTY_WEIGHT",txtEMPTY_WEIGHT.Text.Trim()); // 공차중량
            //    dict.Add("P_PROD_WGHT", txtPRODWGHT.Text.Trim()); // 이론중량  
            //    dict.Add("P_CARRI_CD", txtCust_Code.Text.Trim()); // 업체코드
            //    dict.Add("P_CARRI_NM", txtCustCD.Text.Trim()); // 업체명 
            //    dict.Add("P_DRVR_NM", txtDRVR_NM.Text.Trim()); // 운전자명 
            //    dict.Add("P_DRVR_PHON", txtDRVR_PHON.Text.Trim()); // 운전자연락처 
            //    dict.Add("P_REMARK", txtREMARK.Text.Trim()); // 비고
            //    dict.Add("P_USE_YN", txtUSE_YN.Text.Trim()); // 사용여부  
            //    dict.Add("P_CRT_USER", clsUserInfo.User_id); // 입력자
            //    dict.Add("P_MOD_USER", clsUserInfo.User_id); // 수정자

            //    dict.Add("P_VEHL_FLAG", txtVEHL_FLAG.EditValue.ToString()); // 차량구분

            //    dict.Add("P_PALLET", txtPALLET.Text.Trim()); // 비고
            //    dict.Add("P_BAG", txtBAG.Text.Trim()); // 사용여부  
                
                  

            //    string rtn = DBConn.ExecuteQuerySPR3("SP_TB_WS01_0012_R", dict);

            //    if (rtn != "-1")
            //    {
            //        MsgBoxUtil.AlertInformation(rtn);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    MsgBoxUtil.AlertError(exception.Message);
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //    BtnNew.PerformClick();
            //    BtnSearch.PerformClick();
            //}
        }

        #endregion

        #region Focus Change

        private void gvw_carsearch_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {

        }

        #endregion

        #region Grid Click

        private void grd_carsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvw_carsearch.RowCount > 0)
                {
                    txtVEHLCD.Text = gvw_carsearch.GetFocusedRowCellValue("VEHL_CD").ToString();
                    txVEHLNO.Text = gvw_carsearch.GetFocusedRowCellValue("VEHL_NO").ToString();
                    txtCustCD.Text = gvw_carsearch.GetFocusedRowCellValue("CARRI_NM").ToString();
                    txtPRODWGHT.Text = gvw_carsearch.GetFocusedRowCellValue("PROD_WGHT").ToString();
                    txtVEHL_KIND.EditValue = gvw_carsearch.GetFocusedRowCellValue("CAR_WK_TP").ToString();   
                    txtVEHL_FLAG.EditValue = gvw_carsearch.GetFocusedRowCellValue("CAR_USAGE_GRP").ToString();
                    txtEMPTY_WEIGHT.Text = gvw_carsearch.GetFocusedRowCellValue("EMPTY_WEIGHT").ToString();
                    txtCust_Code.Text = gvw_carsearch.GetFocusedRowCellValue("CARRI_CD").ToString();
                    txtUSE_YN.Text = gvw_carsearch.GetFocusedRowCellValue("USE_YN").ToString();
                    txtDRVR_NM.Text = gvw_carsearch.GetFocusedRowCellValue("DRVR_NM").ToString();
                    txtDRVR_PHON.Text = gvw_carsearch.GetFocusedRowCellValue("DRVR_PHON").ToString();
                    txtREMARK.Text = gvw_carsearch.GetFocusedRowCellValue("REMARK").ToString();

                }
            }
            catch (Exception exception)
            {

            }
        }

        #endregion


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



        #region Custom Column DisplayText

        private void gvw_carsearch_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "EMPTY_WEIGHT" | e.Column.FieldName == "PROD_WGHT") //공차중량  이론중량
            {
                if (e.DisplayText != "" & e.DisplayText != "0" &
                    CustomColumnDisplayTextChange.CheckingSpecialText(e.DisplayText) == true)
                {
                    e.DisplayText = CustomColumnDisplayTextChange.txtsetting(e.DisplayText.Replace("-", "")); // +"Kg";
                    //e.DisplayText = e.DisplayText + "Kg";
                }
            }
        }

        #endregion
    }
}