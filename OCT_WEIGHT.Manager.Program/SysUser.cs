using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysUser : OCT_WEIGHT.Manager.Common.FrmBase
    {
        int focus_cnt = 0;
        string temp_crud = string.Empty;


        public SysUser()
        {
            InitializeComponent();
        }

        #region Onload

        private void SysUser_Load(object sender, EventArgs e)
        {
            SelectUpper(lokPLACE_CD, "CODE", "CODE_NAME", "WS_003");  //사업장_계량대
            SelectUpper(lokAUTH_CD, "CODE", "CODE_NAME", "WS_002");   //권한
            InitInputLayout();
            //txtSearchUser.Focus();
        }

        #endregion

        #region SelectUpper 룩업에디트 데이터 채우기

        private void SelectUpper(DevExpress.XtraEditors.LookUpEdit LookUp, string Value, string Display, string fg)
        {

            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_TYPE_CD", fg);
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

        #region 조회 

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_USER", txtUSER_NMR.Text);

                //20190503 임시수정 Kimsw
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSUSER_R", dict);
                //_svc.GetQuerySP("SP_SYSUSER_R", dict);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd_menu.DataSource = ds.Tables[0];
                    GetDataShow(true);
                }
                else
                {
                    InitInputLayout();
                    grd_menu.DataSource = null;
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

        #region 바인딩 설정

        private void GetDataShow(bool isView)
        {
            bool isShow = (isView && gvw_menu.DataRowCount > 0) ? true : false;

            if (!isShow)
            {
                txtUSER_CD.Text = "";
                txtUSER_CD.Enabled = true;
                txtUSER_NM.Text = "";
                txtUSER_PW.Text = "";
                txtREMARK.Text = "";
            }
            else
            {
                DataRow dr = gvw_menu.GetDataRow(gvw_menu.FocusedRowHandle);
                txtUSER_CD.Text = dr["USER_CD"].ToString();
                txtUSER_NM.Text = dr["USER_NM"].ToString();
                txtUSER_PW.Text = dr["USER_PW"].ToString();
                txtREMARK.Text = dr["REMARK"].ToString();
                lokPLACE_CD.EditValue = dr["PLACE_CD"].ToString();
                lokAUTH_CD.EditValue = dr["AUTH_CD"].ToString();
            }
        }

        #endregion

        #region 입력화면초기화

        private void InitInputLayout()
        {
            txtUSER_CD.Text = "";
            txtUSER_NM.Text = "";
            txtUSER_PW.Text = "";
            txtREMARK.Text = "";
            btnSave.CRUD_type = CRUDType.C;
        }

        #endregion

        #region 신규 

        private void btnNew_Click(object sender, EventArgs e)
        {
            InitInputLayout();
            txtUSER_CD.Text = "";

        }

        #endregion

        #region 저장 

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((btnSave.CRUD_type != CRUDType.C) && (gvw_menu.DataRowCount.Equals(0))) return;

            #region 체크 루틴

            if (txtUSER_CD.Text == "")
            {
                MessageBox.Show("사원코드를 입력하세요");
                txtUSER_CD.Focus();
                return;
            }

            if (txtUSER_NM.Text == "")
            {
                MessageBox.Show("사원명을 입력하세요");
                txtUSER_NM.Focus();
                return;
            }

            if (txtUSER_PW.Text == "")
            {
                MessageBox.Show("비밀번호를 입력하세요");
                txtUSER_PW.Focus();
                return;
            }

            if (lokPLACE_CD.Text == "")
            {
                MessageBox.Show("사업장_계량대을 입력하세요");
                lokPLACE_CD.Focus();
                return;
            }

            if (lokAUTH_CD.Text == "")
            {
                MessageBox.Show("권한그룹을 입력하세요");
                lokAUTH_CD.Focus();
                return;
            }

            #endregion


            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", temp_crud);
                p.Add("P_USER_CD", txtUSER_CD.Text);
                p.Add("P_USER_NM", txtUSER_NM.Text);
                p.Add("P_PLACE_CD", lokPLACE_CD.GetColumnValue("CODE").ToString());
                p.Add("P_AUTH_CD", lokAUTH_CD.GetColumnValue("CODE").ToString());
                p.Add("P_USER_PW", txtUSER_PW.Text);
                p.Add("P_USE_YN", "Y");
                p.Add("P_REMARK", txtREMARK.Text);
                p.Add("P_CRT_USER", clsUserInfo.User_id);

                //20190503 임시수정 Kimsw
                DBConn.ExecuteQuerySPR2("SP_SYSUSER_S", p);
                //_svc.SetQuerySP("SP_SYSUSER_TB_WG01_0008_S", p);

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

        #region 삭제 

        private void octoButton1_Click(object sender, EventArgs e)
        {
            if (gvw_menu.DataRowCount == 0) return;

            DialogResult dlgResult = MsgBoxUtil.ConfirmDelete();
            if (dlgResult != DialogResult.OK) return;

            btnSave.CRUD_type = CRUDType.D;
            btnSave.PerformClick();
        }

        #endregion

        #region 닫기 

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 포커스 쳉지

        private void gvw_menu_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnSave.CRUD_type = CRUDType.U;
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