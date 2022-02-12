using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.util;
using OCT_WEIGHT.Manager.Common;
using DevExpress.XtraEditors;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysScha : OCT_WEIGHT.Manager.Common.FrmBase
    {
        DataTable dt = new DataTable();
        DataRow row = null;

        public SysScha()
        {
            InitializeComponent();
        }

        #region New

        private void BtnNew_Click(object sender, EventArgs e)
        {
            txtSCHANM.Text = string.Empty;
            txtCEO.Text = string.Empty;
            txtAddr.Text = string.Empty;
            START_DATE.DateTime = DateTime.Now;

            SelectUpper(leSchaFlag, "CODE", "CODE_NAME", "WS_007", "SP_COMBOX_R", "CODE");
            leSchaFlag.ItemIndex = 0;
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
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("V_SCHA_NAME", txtSCHANM.Text.Trim());
                //dict.Add("V_SCHA_DPJ", txtCEO.Text.Trim());
                //dict.Add("V_SCHA_ADDR", txtAddr.Text.Trim());
                //dict.Add("V_SCHA_DATE", START_DATE.Text.Trim());
                //dict.Add("V_CARRY_CD", leSchaFlag.GetColumnValue("CODE").ToString());
                //올바른 SP명으로 수정(2019-10-05 한민호)
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSSCHA_R", dict);
                //DataSet ds = DBConn.ExecuteDataSet2("SP_TBWS03_0004_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdScha.DataSource = ds.Tables[0];
                }
                else
                {
                    MsgBoxUtil.AlertInformation("조회된 자료가 없습니다.");
                    grdScha.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("차량 조회중 ERROR:" + ex.Message.ToString().Trim());
            }
            finally
            {
                gvwScha.BestFitColumns();
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
            if (gvwScha.RowCount == 0) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvwScha.ExportToXls(desktop + "\\" + this.Text + ".xls");
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

        #region Save

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                this.Cursor = Cursors.WaitCursor;
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string _cust = leSchaFlag.GetColumnValue("CODE").ToString();
                dict.Add("V_CUST", _cust); // 거래처코드
                dict.Add("V_SCHA_NAME", txtSCHANM.Text.Trim()); // 운송사명
                dict.Add("V_SCHA_ADDR", txtAddr.Text.Trim()); // 운송사주소
                dict.Add("V_SCHA_DPJ", txtCEO.Text.Trim()); // 대표자
                dict.Add("V_SCHA_DATE", START_DATE.Text.Trim()); // 등록일자
                dict.Add("V_TRP_CMP_TP", ""); // ???
                dict.Add("V_REJECT_DATE", ""); // 출입정지일자 

                string rtn = DBConn.ExecuteQuerySPR3("SP_TBWS03_0004_S", dict);

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
                BtnNew.PerformClick();
                BtnSearch.PerformClick();
            }
        }

        #endregion

        #region Load

        private void SysScha_Load(object sender, EventArgs e)
        {
            this.Text = "운송사정보 조회";
            BtnNew.PerformClick();
        }

        #endregion

        #region Grid Click

        private void grdScha_Click(object sender, EventArgs e)
        {
            if (gvwScha.RowCount > 0)
            {
                DataRow dr = gvwScha.GetDataRow(gvwScha.FocusedRowHandle);
                txtSCHANM.Text = string.Empty;
                txtCEO.Text = string.Empty;
                txtAddr.Text = string.Empty;

                txtSCHANM.DataBindings.Add("EditValue", gvwScha.DataSource, "CUST");
                txtCEO.DataBindings.Add("EditValue", gvwScha.DataSource, "SCHA_DPJ");
                txtAddr.DataBindings.Add("EditValue", gvwScha.DataSource, "SCHA_ADDR");

                leSchaFlag.EditValue = dr["CUST"].ToString();
            }
        }

        #endregion
    }
}