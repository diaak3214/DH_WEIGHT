using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysOpPerfo : OCT_WEIGHT.Manager.Common.FrmBase
    {
        private DataTable dt_pnt;

        public SysOpPerfo()
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

                dict.Add("V_VEHL_NO", txtVEHL_NO.Text.Trim());
                //2019-09-25 한민호
                dict.Add("V_START_DATE", START_DATE.Text.Replace("-", ""));
                dict.Add("V_END_DATE", END_DATE.Text.Replace("-", ""));
                //dict.Add("V_START_DATE", START_DATE.Text.Trim());
                //dict.Add("V_END_DATE", END_DATE.Text.Trim());
                //dict.Add("V_MATERIAL", lITEM_IF.Text.Trim());
                //dict.Add("V_RFID_NO", txtRFIDSEQ.Text.Trim());

                dict.Add("V_ITEM_JUNG", lueITEM_JUNG.EditValue.ToString().Replace("<Null>", ""));         //품목구분
                dict.Add("V_INOUT_GUBUN", lueINOUT_GUBUN.EditValue.ToString().Replace("<Null>", ""));     //입출고구분
                dict.Add("V_CUST_NM", txtCUST_NM.Text.Trim());                      //거래처명

                DataSet ds = DBConn.ExecuteDataSet2("SP_TB_WS02_0002_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdPerfo.DataSource = ds.Tables[0];
                    grdPrint.DataSource = ds.Tables[0];
                    dt_pnt = ds.Tables[0];
                }
                else
                {
                    MsgBoxUtil.AlertInformation("조회된 자료가 없습니다.");
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
                gvwPrint.BestFitColumns();
            }
        }

        #endregion

        #region 엑셀저장

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            if (gvwPerfo.RowCount == 0) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvwPerfo.ExportToXlsx(desktop + "\\" + this.Text + "_" + DateTime.Now.ToString("YYYY-MM-dd") + ".xlsx",
                    new XlsxExportOptions(TextExportMode.Text, true, false));
                //ExportToXls(desktop + "\\" + this.Text + ".xls");
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

        #region 신규

        private void BtnNew_Click(object sender, EventArgs e)
        {
            START_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            END_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //lITEM_IF.SelectedIndex = 0;
            //lITEM_IF.EditValue = "";
            //txtVehlNo.Text = string.Empty;
            //txtRFIDSEQ.Text = string.Empty;
        }

        #endregion

        #region FormLoad

        private void SysOpPerfo_Load(object sender, EventArgs e)
        {
            BtnNew.PerformClick();

            //ComboBox Data Setting
            //품목분류를 국내고철 E01,E11  수입고철:E12  ,원재료:E15 추가하기 위해 생성(2019-10-06 한민호)
            //SelectUpper3(lueITEM_JUNG, "CODE", "CODE_NAME", "WS_006");   //계량종류_품목구분
            //SelectUpper(lueITEM_JUNG, "CODE", "CODE_NAME", "WS_006");       //계량종류_품목구분
            //SelectUpper(lueINOUT_GUBUN, "CODE", "CODE_NAME", "001");    //입출고구분
            //SelectUpper(lITEM_IF, "CODE", "CODE_NAME", "WS_006");
            gvwPerfo.BestFitColumns();
            //gvwPerfo.Editable = false;
        }

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

        #endregion

        #region Focus Row Changed

        private void gvwPerfo_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {

        }

        #endregion

        #region 종료

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Report

        private void BtnReport_Click(object sender, EventArgs e)
        {
            Report.Report Print_etc = new Report.Report(dt_pnt);
            Print_etc.start_date = DateTime.Now.ToString("yyyy-MM-dd");
            Print_etc.end_date = DateTime.Now.ToString("yyyy-MM-dd");
            Print_etc.GridControl = grdPrint;
            Print_etc.company = "창원";
            Print_etc.load();
            //Print_etc.ShowPreview();
            //Print_etc.Print();
            ReportPrintTool printTool = new ReportPrintTool(Print_etc);
            printTool.ShowPreviewDialog();
        }

        #endregion

        #region Edit Value Changed

        private void START_DATE_EditValueChanged(object sender, EventArgs e)
        {
            //여러날짜로 조회 할 수 있는 방법이 없어서 막음(2019-10-05 한민호)
            //END_DATE.DateTime = START_DATE.DateTime;
        }

        private void END_DATE_EditValueChanged(object sender, EventArgs e)
        {
            //여러날짜로 조회 할 수 있는 방법이 없어서 막음(2019-10-05 한민호)
            //START_DATE.DateTime = END_DATE.DateTime;
        }

        #endregion

        #region Custom Column DisplayText

        private void gvwPerfo_CustomColumnDisplayText_1(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "LOAD_WEIGHT" | e.Column.FieldName == "DOWN_WEIGHT" | //1차중량 2차중량
                e.Column.FieldName == "REAL_WGHT" | e.Column.FieldName == "PROD_WGHT" | //실중량  이론중량
                //감량, 확정중량 추가(2019-10-21 한민호)
                e.Column.FieldName == "REDUCE_WG" | e.Column.FieldName == "CONF_WGHT") //감량  확정중량
            {
                if (e.DisplayText != "" & e.DisplayText != "0" &
                    CustomColumnDisplayTextChange.CheckingSpecialText(e.DisplayText) == false)
                {
                    if (e.DisplayText.IndexOf(',') > 0) return;
                    e.DisplayText = CustomColumnDisplayTextChange.txtsetting(e.DisplayText.Replace("-", ""));
                }
            }
        }

        private void gvwPrint_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "LOAD_WEIGHT" | e.Column.FieldName == "DOWN_WEIGHT" | //1차중량 2차중량
                e.Column.FieldName == "REAL_WGHT" | e.Column.FieldName == "PROD_WGHT"| //실중량  이론중량
                //감량, 확정중량 추가(2019-10-21 한민호)
                e.Column.FieldName == "REDUCE_WG" | e.Column.FieldName == "CONF_WGHT") //감량  확정중량
            {
                if (e.DisplayText != "" & e.DisplayText != "0" &
                    CustomColumnDisplayTextChange.CheckingSpecialText(e.DisplayText) == false)
                {
                    e.DisplayText = CustomColumnDisplayTextChange.txtsetting(e.DisplayText.Replace("-", ""));
                }
            }
        }

        #endregion

        #region Grid Click

        private void grdPerfo_Click(object sender, EventArgs e)
        {
            /*
            if (gvwPerfo.RowCount > 0)
            {
                DataRow dr = gvwPerfo.GetDataRow(gvwPerfo.FocusedRowHandle);
                if (dr.Table.Rows.Count > 0)
                {
                    txtVehlNo.DataBindings.Clear();
                    txtRFIDSEQ.DataBindings.Clear();
                    lITEM_IF.EditValue = "";

                    txtVehlNo.DataBindings.Add("EditValue", grdPerfo.DataSource, "VEHL_NO");
                    lITEM_IF.EditValue = dr["ITEM_SO"].ToString();
                    txtRFIDSEQ.DataBindings.Add("EditValue", grdPerfo.DataSource, "RFID_NO");
                }
            }
            */
        }

        #endregion

        //순번 추가(2019-10-26 한민호)
        private void gvwPerfo_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
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
        
        private void gvwPerfo_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            DevExpress.XtraGrid.GridColumnSummaryItem item = e.Item as DevExpress.XtraGrid.GridColumnSummaryItem;
            DevExpress.XtraGrid.Views.Base.ColumnView gvwPerfo = sender as DevExpress.XtraGrid.Views.Base.ColumnView;

            if (gvwPerfo.RowCount <= 0) return;

            switch (e.SummaryProcess)
            {
                case DevExpress.Data.CustomSummaryProcess.Start:
                    {
                        e.TotalValue = 0D;
                    }
                    break;
                case DevExpress.Data.CustomSummaryProcess.Calculate:
                    {
                        Decimal dec = 0;
                        try
                        {
                            Decimal.TryParse(e.TotalValue.ToString(), out dec);
                            //Decimal.TryParse(e.TotalValue.ToString());
                        }
                        catch
                        {
                        }
                        e.TotalValue += dec.ToString();
                        //e.TotalValue += Decimal.Parse(dec);
                    }
                    break;
                case DevExpress.Data.CustomSummaryProcess.Finalize:
                    {
                        e.TotalValue = Decimal.Parse(e.TotalValue.ToString()).ToString("#,##0");
                    }
                    break;
            }
        }
    }
}