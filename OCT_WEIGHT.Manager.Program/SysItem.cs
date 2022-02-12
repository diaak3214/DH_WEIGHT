using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysItem : OCT_WEIGHT.Manager.Common.FrmBase
    {
        int focus_cnt = 0;
        string temp_crud = string.Empty;

        public SysItem()
        {
            InitializeComponent();
        }

        #region 조회

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            //try
            //{
            //    Dictionary<string, string> dict = new Dictionary<string, string>();
            //    dict.Add("P_FG", "ALL");
            //    DataSet ds = DBConn.ExecuteDataSet2("SP_SYSITEM_R", dict);
            //    //_svc.GetQuerySP("SP_SYSITEM_R", dict);

            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        grd_menu.DataSource = ds.Tables[0];
            //        gvw_menu.ExpandAllGroups();
            //    }
            //    else
            //    {
            //        grd_menu.DataSource = null;
            //        // InitInputLayout();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{

            //}
        }

        #endregion

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
            InitInputLayout();
            txtITEM_JUNG.Focus();
        }

        #endregion

        #region 입력화면초기화

        private void InitInputLayout()
        {
            txtITEM_JUNG.Enabled = true;
            lWEIGHT_FG.Enabled = false;

            txtITEM_SO.Enabled = true;

            txtITEM_JUNG.Text = "";
            txtITEM_JUNG_NM.Text = "";

            btnSave.CRUD_type = CRUDType.C;

            DataRow dr = gvw_menu.GetDataRow(gvw_menu.FocusedRowHandle);
            if (dr != null)
            {
                txtITEM_JUNG.Text = dr["ITEM_JUNG"].ToString();
                txtITEM_JUNG_NM.Text = dr["ITEM_JUNG_NM"].ToString();
                lWEIGHT_FG.EditValue = dr["WEIGHT_FG"].ToString();
            }

            txtITEM_SO.Text = "";
            txtITEM_SO_NM.Text = "";
            txtREMARK.Text = "";

            lITEM_IF.EditValue = "";
            lITEM_FG.EditValue = "";
            lUse.SelectedIndex = 0;
        }

        #endregion

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if ((btnSave.CRUD_type != CRUDType.C) && (gvw_menu.DataRowCount.Equals(0))) return;

            //temp_crud = btnSave.CRUD_type.ToString();
            //focus_cnt = gvw_menu.FocusedRowHandle;
            //this.Cursor = Cursors.WaitCursor;

            //try
            //{
            //    // 마스터 저장 
            //    Dictionary<string, string> p = new Dictionary<string, string>();
            //    p.Add("P_FG", temp_crud);
            //    p.Add("P_WEIGHT_FG", lWEIGHT_FG.GetColumnValue("CODE").ToString());
            //    p.Add("P_ITEM_JUNG", txtITEM_JUNG.Text);
            //    p.Add("P_ITEM_JUNG_NM", txtITEM_JUNG_NM.Text);

            //    p.Add("P_ITEM_SO", txtITEM_SO.Text);
            //    p.Add("P_ITEM_SO_NM", txtITEM_SO_NM.Text);

            //    p.Add("P_ITEM_IF", lITEM_FG.GetColumnValue("CODE").ToString());
            //    p.Add("P_ITEM_FG", lITEM_IF.GetColumnValue("CODE").ToString());

            //    p.Add("P_USE_YN", lUse.Text);
            //    p.Add("P_REMARK", txtREMARK.Text);
            //    p.Add("P_CRT_USER", clsUserInfo.User_id);

            //    //20190503 임시수정 Kimsw
            //    string rtn = DBConn.ExecuteQuerySPR3("SP_SYSITEM_TB_WS01_0010_S", p);
            //    if (rtn != "-1")
            //    {
            //        MessageBox.Show(rtn);
            //    }
            //    //_svc.SetQuerySP("SP_SYSITEM_TB_WG01_0010_S", p);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    btnSearch.PerformClick();
            //    gvw_menu.MoveBy(focus_cnt);
            //    this.Cursor = Cursors.Default;
            //}
        }

        #endregion

        #region Onload

        private void SysItem_Load(object sender, EventArgs e)
        {
            SelectUpper(lWEIGHT_FG, "CODE", "CODE_NAME", "VC_018");
            SelectUpper(lITEM_IF, "CODE", "CODE_NAME", "WS_003");
            SelectUpper(lITEM_FG, "CODE", "CODE_NAME", "WS_004");
        }

        private void SelectUpper(DevExpress.XtraEditors.LookUpEdit LookUp, string Value, string Display, string fg)
        {

            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_TYPE_CD", fg);

                //20190503 임시수정 Kimsw
                DataSet ds = DBConn.ExecuteDataSet2("SP_COMBOX_R", dict);
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

        #endregion

        #region 닫기

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 그리드 순번

        private void gvw_menu_CustomDrawRowIndicator(object sender,
            DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            //    e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //    if (e.Info.IsRowIndicator)
            //        e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        #endregion

        #region 바인딩

        private void gvw_menu_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnSave.CRUD_type = CRUDType.U;

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

        #region 바인딩 설정

        private void GetDataShow(bool isView)
        {
            bool isShow = (isView && gvw_menu.DataRowCount > 0) ? true : false;

            if (!isShow)
            {

                lWEIGHT_FG.EditValue = "";
                lUse.SelectedIndex = 0;
                lITEM_FG.EditValue = "";
                lITEM_IF.EditValue = "";

                txtITEM_JUNG.Text = "";
                txtITEM_JUNG_NM.Text = "";
                txtITEM_SO.Text = "";
                txtITEM_SO_NM.Text = "";
                txtREMARK.Text = "";

                txtITEM_JUNG.Enabled = true;
                txtITEM_SO.Enabled = true;
                lWEIGHT_FG.EditValue = true;

            }
            else
            {
                DataRow dr = gvw_menu.GetDataRow(gvw_menu.FocusedRowHandle);

                txtITEM_JUNG.Text = dr["ITEM_JUNG"].ToString();
                txtITEM_JUNG_NM.Text = dr["ITEM_JUNG_NM"].ToString();
                txtITEM_SO.Text = dr["ITEM_SO"].ToString();
                txtITEM_SO_NM.Text = dr["ITEM_SO_NM"].ToString();
                txtREMARK.Text = dr["REMARK"].ToString();
                lWEIGHT_FG.EditValue = dr["WEIGHT_FG"].ToString();
                lITEM_IF.EditValue = dr["ITEM_IF"].ToString();
                lITEM_FG.EditValue = dr["ITEM_FG"].ToString();
                lUse.SelectedIndex = lUse.Properties.Items.IndexOf(dr["USE_YN"].ToString());
            }
        }

        #endregion

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

        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {

        }

        #region 삭제

        private void octoButton1_Click_1(object sender, EventArgs e)
        {
            //if ((btnSave.CRUD_type != CRUDType.C) && (gvw_menu.DataRowCount.Equals(0))) return;

            //temp_crud = btnSave.CRUD_type.ToString();
            //focus_cnt = gvw_menu.FocusedRowHandle;
            //this.Cursor = Cursors.WaitCursor;

            //try
            //{
            //    // 마스터 저장 
            //    Dictionary<string, string> p = new Dictionary<string, string>();
            //    p.Add("P_FG", "D");
            //    p.Add("P_WEIGHT_FG", lWEIGHT_FG.GetColumnValue("CODE").ToString());
            //    p.Add("P_ITEM_JUNG", txtITEM_JUNG.Text);
            //    p.Add("P_ITEM_JUNG_NM", txtITEM_JUNG_NM.Text);

            //    p.Add("P_ITEM_SO", txtITEM_SO.Text);
            //    p.Add("P_ITEM_SO_NM", txtITEM_SO_NM.Text);

            //    p.Add("P_ITEM_IF", lITEM_FG.GetColumnValue("CODE").ToString());
            //    p.Add("P_ITEM_FG", lITEM_IF.GetColumnValue("CODE").ToString());

            //    p.Add("P_USE_YN", lUse.Text);
            //    p.Add("P_REMARK", txtREMARK.Text);
            //    p.Add("P_CRT_USER", clsUserInfo.User_id);

            //    //20190503 임시수정 Kimsw
            //    string rtn = DBConn.ExecuteQuerySPR3("SP_SYSITEM_TB_WS01_0010_S", p);
            //    if (rtn != "-1")
            //    {
            //        MessageBox.Show(rtn);
            //    }
            //    //_svc.SetQuerySP("SP_SYSITEM_TB_WG01_0010_S", p);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    btnSearch.PerformClick();
            //    gvw_menu.MoveBy(focus_cnt);
            //    this.Cursor = Cursors.Default;
            //}
        }

        #endregion
    }
}