using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysWaitVehl : OCT_WEIGHT.Manager.Common.FrmBase
    {
        private string[] list = new string[] { };

        public SysWaitVehl()
        {
            InitializeComponent();
        }

        #region 조회
        //검색조건 엔터키 시 조회되게 분리 시킴(2019-10-26 한민호) 
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //QueryClick();
        }

        public void QueryClick()
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                //dict.Add("V_RFID_SEQ", txtCUST_NM.Text.Trim());
                //dict.Add("V_BAECHA_NO", txtBAECHA_NO.Text.Trim());
                dict.Add("V_VEHL_NO", txtVEHL_NO.Text.Trim());
                //dict.Add("V_BAECHA_SEQ", txtBAECHA_SEQ.Text.Trim());
                //2019-09-25 한민호
                dict.Add("V_START_DATE", START_DATE.Text.Replace("-", ""));
                dict.Add("V_END_DATE", END_DATE.Text.Replace("-", ""));
                //dict.Add("V_START_DATE", START_DATE.Text.Trim());
                //dict.Add("V_END_DATE", END_DATE.Text.Trim());

                dict.Add("V_ITEM_JUNG", lueITEM_JUNG.EditValue.ToString().Replace("<Null>", ""));         //품목구분
                dict.Add("V_INOUT_GUBUN", lueINOUT_GUBUN.EditValue.ToString().Replace("<Null>", ""));     //입출고구분
                dict.Add("V_CUST_NM", txtCUST_NM.Text.Trim());                      //거래처명
                DataSet ds = DBConn.ExecuteDataSet2("SP_TB_WS02_0001_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdPerfo.DataSource = ds.Tables[0];
                }
                else
                {
                    OCT_WEIGHT.Manager.Common.util.MsgBoxUtil.AlertInformation("조회된 자료가 없습니다.");
                    grdPerfo.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("차량 조회중 ERROR:" + ex.Message.ToString().Trim());
            }
            finally
            {
                gvwPerfo.BestFitColumns();
            }
        }
        #endregion

        #region 신규

        private void BtnNew_Click(object sender, EventArgs e)
        {
            txtCUST_NM.Text = string.Empty;
            //txtBAECHA_NO.Text = string.Empty;
            txtVEHL_NO.Text = string.Empty;
            //txtBAECHA_SEQ.Text = string.Empty;
            START_DATE.DateTime = DateTime.Now;
            END_DATE.DateTime = DateTime.Now;
        }

        #endregion

        #region 엑셀

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            if (gvwPerfo.RowCount == 0) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvwPerfo.ExportToXls(desktop + "\\" + this.Text + ".xls");
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

        #region Form Load

        private void SysWaitVehl_Load(object sender, EventArgs e)
        {
            BtnNew.PerformClick();
            //품목분류를 국내고철 E01,E11  수입고철:E12  ,원재료:E15 추가하기 위해 생성(2019-10-06 한민호)
            //SelectUpper3(lueITEM_JUNG, "CODE", "CODE_NAME", "WS_006");   //계량종류_품목구분
            //SelectUpper(lueITEM_JUNG, "CODE", "CODE_NAME", "WS_006");   //계량종류_품목구분
            //SelectUpper(lueINOUT_GUBUN, "CODE", "CODE_NAME", "001");    //입출고구분
            
        }

        #endregion

        #region ComboBox Data Bound

        private void SelectUpper(DevExpress.XtraEditors.LookUpEdit LookUp, string Value, string Display, string fg)
        {

            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_TYPE_CD", fg);

                //20190503 임시수정 Kimsw
                //검색조건에서 빈값 넣기 위해 따로 만든SP(2019-09-28 한민호
                DataSet ds = DBConn.ExecuteDataSet2("SP_COMBOX_R2", dict);
                //DataSet ds = DBConn.ExecuteDataSet2("SP_COMBOX_R", dict);
                //_svc.GetQuerySP("SP_COMBOX_R", dict);
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

        //품목분류를 국내고철 E01,E11  수입고철:E12  ,원재료:E15 추가하기 위해 생성(2019-10-06 한민호)
        private void SelectUpper3(DevExpress.XtraEditors.LookUpEdit LookUp, string Value, string Display, string fg)
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_TYPE_CD", fg);

                DataSet ds = DBConn.ExecuteDataSet2("SP_COMBOX_R3", dict);
                DataTable dt = ds.Tables[0];

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
        #region Focus Row Changed

        private void gvwPerfo_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            /*
            if (gvwPerfo.RowCount > 0)
            {
                DataRow dr = gvwPerfo.GetDataRow(gvwPerfo.FocusedRowHandle);
                txtRFID_SEQ.DataBindings.Clear();
                txtBAECHA_NO.DataBindings.Clear();
                txtBAECHA_SEQ.DataBindings.Clear();
                txtVEHL_NO.DataBindings.Clear();
                    
                txtRFID_SEQ.DataBindings.Add("EditValue", grdPerfo.DataSource, "RFID_SEQ");
                txtBAECHA_NO.DataBindings.Add("EditValue", grdPerfo.DataSource, "BAECHA_NO");
                txtBAECHA_SEQ.DataBindings.Add("EditValue", grdPerfo.DataSource, "BAECHA_SEQ");
                txtVEHL_NO.DataBindings.Add("EditValue", grdPerfo.DataSource, "VEHL_NO");
                //cbUse.SelectedIndex = cbUse.Properties.Items.IndexOf(dr["WEIGHT_YN"].ToString());
            }
            */
        }

        #endregion

        #region 종료

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Custom Column DisplayText

        private void gvwPerfo_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "REAL_WGHT" | e.Column.FieldName == "PROD_WGHT") //실중량  이론중량
            {
                if (e.DisplayText != "" & e.DisplayText != "0" &
                    CustomColumnDisplayTextChange.CheckingSpecialText(e.DisplayText) == false)
                {
                    e.DisplayText = e.DisplayText;
                    //CustomColumnDisplayTextChange.txtsetting(e.DisplayText.Replace("-", "")); // +"Kg";
                    //e.DisplayText = e.DisplayText + "Kg";
                }
            }
        }

        #endregion

        #region Grid Click

        private void grdPerfo_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string[] list = new string[] { };
                for (int i = 0; i < gvwPerfo.RowCount; i++)
                {
                    list[i] = gvwPerfo.GetRowCellValue(i, "CAR_ENT_SEQ").ToString();
                }

                //CAR_ENT_SEQ 컬럼 중복값 체크
                for (int i = 0; i < list.Length; i++)
                {
                    for (int j = 0; j < list.Length; j++)
                    {
                        if (list[i] == list[j])
                        {
                            OCT_WEIGHT.Manager.Common.util.MsgBoxUtil.AlertInformation("중복된 값이 있습니다. 확인바랍니다.");
                            return;
                        }
                    }
                }

                for (int i = 0; i < gvwPerfo.RowCount; i++)
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("V_RFID_SEQ", gvwPerfo.GetRowCellValue(i, "RFID_SEQ").ToString());     //txtRFID_SEQ.Text.Trim());
                    dict.Add("V_BAECHA_NO", list[i]);  //gvwPerfo.GetRowCellValue(i, "BAECHA_NO").ToString());   //txtBAECHA_NO.Text.Trim());
                    dict.Add("V_VEHL_NO", gvwPerfo.GetRowCellValue(i, "VEHL_NO").ToString());       //txtVEHL_NO.Text.Trim());
                    dict.Add("V_BAECHA_SEQ", gvwPerfo.GetRowCellValue(i, "BAECHA_SEQ").ToString()); //txtBAECHA_SEQ.Text.Trim());
                    dict.Add("V_V_CAR_ENT_SEQ", gvwPerfo.GetRowCellValue(i, "CAR_ENT_SEQ").ToString()); 
                    dict.Add("V_START_DATE", "");//gvwPerfo.GetRowCellValue(i, "").ToString());    //txtBAECHA_SEQ.Text.Trim());
                    dict.Add("V_END_DATE", "");//gvwPerfo.GetRowCellValue(i, "").ToString());    //txtBAECHA_SEQ.Text.Trim());

                    string ret = DBConn.ExecuteQuerySPR3("SP_TB_WS02_0001_C", dict);

                    if (ret != "-1")
                    {
                        OCT_WEIGHT.Manager.Common.util.MsgBoxUtil.AlertInformation(ret);
                    }
                }
            }
            catch (Exception exception)
            {
                OCT_WEIGHT.Manager.Common.util.MsgBoxUtil.AlertError(exception.Message);
            }
        }

        private void MsgBoxUtil(string p)
        {
            throw new NotImplementedException();
        }

        private string[] ArrayDistinct(string[] source)
        {
            string destination = string.Empty;//임시 필드

            // 중복제거
            for (int i = 0; i < source.Length; i++)
            {
                for (int j = 0; j < source.Length; j++)
                {
                    if (source[i] == source[j] && i != j) source[j] = string.Empty;
                }
            }

            // ,를 구분자로 문자열 생성
            for (int i = 0; i < source.Length; i++)
            {
                if (!source[i].Equals(string.Empty)) destination += source[i] + ",";
            }
            // 마지막에 붙은 ,하나 제거
            destination = destination.Substring(0, destination.Length - 1);
            return destination.Split(',');
        }

        //순번 추가(2019-10-26 한민호)
        private void gvwPerfo_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            if (e.Info.IsRowIndicator)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        //검색조건 엔터키 시 조회되게 분리 시킴(2019-10-26 한민호)
        private void txtVEHL_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryClick();
            }
        }

        private void txtCUST_NM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryClick();
            }
        }

        //커서가 항상 1번째 가게 수정(2019-11-21 한민호)
        private void gvwPerfo_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (gvwPerfo.FocusedRowHandle < 1)
                return;
            gvwPerfo.FocusedRowHandle = 0;
        }
        private void gvwPerfo_EndSorting(object sender, EventArgs e)
        {
            if (gvwPerfo.FocusedRowHandle < 1)
                return;
            gvwPerfo.FocusedRowHandle = 0;
        }


        //이건 뭐지?(2019-09-26 한민호)
        //private string[] CheckBAECHASEQ(string GET_BAECHA_NO)
        //{
        //    string[] list = new string[] { };

        //    //if (_svc == null) _svc = new ServiceAdapter();

        //    try
        //    {
        //        Dictionary<string, string> dict = new Dictionary<string, string>();
        //        dict.Add("V_RFID_SEQ", txtCUST_NM.Text.Trim());
        //        dict.Add("V_BAECHA_NO", GET_BAECHA_NO);
        //        dict.Add("V_VEHL_NO", txtVEHL_NO.Text.Trim());
        //        dict.Add("V_BAECHA_SEQ", txtBAECHA_SEQ.Text.Trim());

        //        DataSet dataSet = DBConn.ExecuteDataSet2("SP_CHECKDISTINCT", dict);
        //        DataTable dt = dataSet.Tables[0];

        //        if (dataSet.Tables[0].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < list.Length; i++)
        //            {
        //                list[i] = dt.Select("BAECHA_NO=" + GET_BAECHA_NO, "BAECHA_SEQ=").ToString();
        //            }

        //            DataRow dr = gvwPerfo.GetDataRow(gvwPerfo.FocusedRowHandle);

                    
        //        }

        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("차량 조회중 ERROR:" + ex.Message.ToString().Trim());
        //        return list;
        //    }
        //}
    }
}