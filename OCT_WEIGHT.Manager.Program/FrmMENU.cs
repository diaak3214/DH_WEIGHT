using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class FrmMENU : OCT_WEIGHT.Manager.Common.FrmBase
    {
        int focus_cnt = 0;
        string temp_crud = string.Empty;

        public FrmMENU()
        {
            InitializeComponent();
        }

        #region Onload

        private void FrmMENU_Load(object sender, EventArgs e)
        {
            cbUse.SelectedIndex = 0;
            SelectUpper(lueUPPER, "MENU_ID", "MENU_NM");
        }

        private void SelectUpper(DevExpress.XtraEditors.LookUpEdit LookUp, string Value, string Display)
        {

            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "U");
                dict.Add("V_USE_YN", cbUse.Text.Trim());
                //임시수정 kimsw 20190430
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSMENU_R", dict);
                //.ExecuteDataSet("SP_SYSMENU_R", "U");
                //_svc.GetQuerySP("SP_SYSMENU_R", dict);
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

        #region 입력화면초기화

        private void InitInputLayout()
        {
            txtCD.Text = "";
            txtCD.Enabled = true;
            txtNM.Text = "";
            txtSORT.Text = "";
            txtFORM.Text = "";
            cbDesk.SelectedIndex = 0;
            cbDIV.SelectedIndex = 0;
            cbUse.SelectedIndex = 0;
            lueUPPER.EditValue = "";

            btnSave.CRUD_type = CRUDType.C;
        }

        #endregion

        #region 조회

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "A");
                //MsgBoxUtil.AlertInformation(cbUse.Text.Trim());
                dict.Add("V_USE_YN", cbUse.Text.Trim());
                //임시수정 kimsw 20190430
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSMENU_R", dict);
                //ExecuteDataSet("SP_SYSMENU_R", "A");
                //_svc.GetQuerySP("SP_SYSMENU_R", dict);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd_menu.DataSource = ds.Tables[0];
                }
                else
                {
                    grd_menu.DataSource = null;
                    InitInputLayout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                gvw_menu.BestFitColumns();
            }
        }

        #endregion

        #region 포커스 췡지

        private void gvw_menu_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            btnSave.CRUD_type = CRUDType.U;
            //Focus Row Changed 막기
            /*
            
            */
        }

        #endregion

        #region 바인딩 설정

        private void GetDataShow(bool isView)
        {
            bool isShow = (isView && gvw_menu.DataRowCount > 0) ? true : false;

            if (!isShow)
            {
                txtCD.Text = "";
                txtCD.Enabled = true;
                txtNM.Text = "";
                txtSORT.Text = "";
                txtFORM.Text = "";
                cbDesk.SelectedIndex = 0;
                cbDIV.SelectedIndex = 0;
                cbUse.SelectedIndex = 0;
                lueUPPER.EditValue = "";
            }
            else
            {
                DataRow dr = gvw_menu.GetDataRow(gvw_menu.FocusedRowHandle);
                txtCD.Text = dr["MENU_ID"].ToString();
                txtNM.Text = dr["MENU_NM"].ToString();
                txtSORT.Text = dr["SORT_NO"].ToString();
                txtFORM.Text = dr["FORM_NM"].ToString();
                lueUPPER.EditValue = dr["UPPER_MENU_ID"].ToString();
                cbDesk.SelectedIndex = cbDesk.Properties.Items.IndexOf(dr["MOBILE_CTGRY"].ToString());
                cbDIV.SelectedIndex = cbDIV.Properties.Items.IndexOf(dr["MENU_DIV"].ToString());
                cbUse.SelectedIndex = cbUse.Properties.Items.IndexOf(dr["USE_YN"].ToString());
            }
        }

        #endregion

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
            InitInputLayout();
            txtCD.Focus();
            cbUse.SelectedIndex = 0;
        }

        #endregion

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((btnSave.CRUD_type != CRUDType.C) && (gvw_menu.DataRowCount.Equals(0))) return;

            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", temp_crud);
                p.Add("P_MENU_ID", txtCD.Text);
                p.Add("P_MENU_NM", txtNM.Text);
                p.Add("P_UPPER_MENU_ID", lueUPPER.GetColumnValue("MENU_ID").ToString());
                p.Add("P_MOBILE_CTGRY", cbDesk.Text);
                p.Add("P_MENU_DIV", cbDIV.Text);
                p.Add("P_SORT_NO", txtSORT.Text);
                p.Add("P_FORM_NM", txtFORM.Text);
                p.Add("P_USE_YN", cbUse.Text);
                p.Add("P_CRT_USER", clsUserInfo.User_id);

                //20190503 Kimsw 수정
                string rtn = DBConn.ExecuteQuerySPR3("SP_SYSMENU_S", p);
                if (rtn != "-1")
                {
                    MessageBox.Show(rtn);
                }
                //_svc.SetQuerySP("SP_FRMMENU_TB_WG01_0012_S", p);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSearch.PerformClick();
                this.Cursor = Cursors.Default;
            }


        }

        #endregion

        #region 닫기

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 엑셀

        private void octoButton2_Click(object sender, EventArgs e)
        {
            if (gvw_menu.RowCount == 0) return;
            try
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvw_menu.ExportToXls(desktop + "\\" + this.Text + ".xls");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSearch.PerformClick();
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Grid Click

        private void grd_menu_Click(object sender, EventArgs e)
        {
            if (gvw_menu.GetFocusedRow() != null)
            {
                GetDataShow(true);
            }
            else
            {
                InitInputLayout();
            }
        }

        #endregion

    }
}