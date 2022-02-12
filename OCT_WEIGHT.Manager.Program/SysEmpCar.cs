using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysEmpCar : OCT_WEIGHT.Manager.Common.FrmBase
    {
        int focus_cnt = 0;
        string temp_crud = string.Empty;
        string seq_no = string.Empty;


        public SysEmpCar()
        {
            InitializeComponent();
        }

        #region Onload

        private void SysEmpCar_Load(object sender, EventArgs e)
        {
            SelectUpper(lokEMP_GUBUN, "CODE", "CODE_NAME", "WS_008");   //권한
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
                dict.Add("P_EMP_CAR_NO", txtEMP_CAR_NO1.Text);
                dict.Add("P_EMP_NM", txtEMP_NM1.Text);
                dict.Add("P_EMP_COMP_NM", txtEMP_COMP_NM1.Text);
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSEMPCAR_R", dict);

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
                txtEMP_CAR_NO.Text = "";
                txtEMP_CAR_NO.Enabled = true;
                txtEMP_NM.Text = "";
                txtEMP_COMP_NM.Text = "";
                txtEMP_HP.Text = "";
            }
            else
            {
                DataRow dr = gvw_menu.GetDataRow(gvw_menu.FocusedRowHandle);
                seq_no = dr["SEQ_NO"].ToString();
                txtEMP_CAR_NO.Text = dr["EMP_CAR_NO"].ToString();
                txtEMP_NM.Text = dr["EMP_NM"].ToString();
                txtEMP_COMP_NM.Text = dr["EMP_COMP_NM"].ToString();
                txtEMP_HP.Text = dr["EMP_HP"].ToString();
                lokEMP_GUBUN.EditValue = dr["EMP_GUBUN"].ToString();
            }
        }

        #endregion

        #region 입력화면초기화

        private void InitInputLayout()
        {
            txtEMP_CAR_NO.Text = "";
            txtEMP_NM.Text = "";
            txtEMP_COMP_NM.Text = "";
            txtEMP_HP.Text = "";
            btnSave.CRUD_type = CRUDType.C;
        }

        #endregion

        #region 신규 

        private void btnNew_Click(object sender, EventArgs e)
        {
            InitInputLayout();
        }

        #endregion

        #region 저장 

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((btnSave.CRUD_type != CRUDType.C) && (gvw_menu.DataRowCount.Equals(0))) return;

            #region 체크 루틴

            if (txtEMP_CAR_NO.Text == "")
            {
                MessageBox.Show("차량번호를 입력하세요");
                txtEMP_CAR_NO.Focus();
                return;
            }

            if (txtEMP_NM.Text == "")
            {
                MessageBox.Show("사원명을 입력하세요");
                txtEMP_NM.Focus();
                return;
            }

            if (txtEMP_COMP_NM.Text == "")
            {
                MessageBox.Show("회사명를 입력하세요");
                txtEMP_COMP_NM.Focus();
                return;
            }

            if (lokEMP_GUBUN.Text == "")
            {
                MessageBox.Show("사원구분을 선택하세요");
                lokEMP_GUBUN.Focus();
                return;
            }

            #endregion


            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", temp_crud);
                p.Add("P_SEQ_NO", seq_no);
                p.Add("P_EMP_CAR_NO", txtEMP_CAR_NO.Text);
                p.Add("P_EMP_NM", txtEMP_NM.Text);
                p.Add("P_EMP_COMP_NM", txtEMP_COMP_NM.Text);
                p.Add("P_EMP_GUBUN", lokEMP_GUBUN.GetColumnValue("CODE").ToString());
                p.Add("P_EMP_HP", txtEMP_HP.Text);
                DBConn.ExecuteQuerySPR2("SP_SYSEMPCAR_S", p);

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