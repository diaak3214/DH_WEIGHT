using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using OCT_WEIGHT.Manager.Common.util;
using OCT_WEIGHT.Manager.Common.Popup;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class FrmCommon : OCT_WEIGHT.Manager.Common.FrmBase
    {
        int focus_cnt = 0;
        string temp_crud = string.Empty;

        public FrmCommon()
        {
            InitializeComponent();
        }

        #region initInputLayout 입력부초기화

        private void initInputLayout()
        {
            txtCode.Text = string.Empty;
            txtCodeName.Text = string.Empty;
            txtRemark.Text = string.Empty;
            txtValue1.Text = string.Empty;
            txtValue2.Text = string.Empty;
            txtValue3.Text = string.Empty;
            cbUse.SelectedIndex = 0;
            txtCode.Enabled = true;
            btnSave.CRUD_type = CRUDType.C;
        }

        #endregion

        #region 종료버튼

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 마스터검색

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "M");
                dict.Add("v_TYPE_CD", "");
                dict.Add("v_TYPE_DESC", txtNAME.Text);

                //20190503 Kimsw 수정
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSCOMMON_R", dict);
                //_svc.GetQuerySP("SP_SYSCOMMON_R", dict);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grCom_M.DataSource = ds.Tables[0];
                    if (gvCom_M.RowCount > 0)
                    {
                        gvCom_M.Focus();
                        string _code = gvCom_M.GetFocusedRowCellValue("TYPE_CD").ToString();
                        if (_code == "")
                        {
                            getCommonD(gvCom_M.GetRowCellValue(1, "TYPE_CD").ToString());
                            //GetFocusedRowCellValue())
                        }

                        getCommonD(gvCom_M.GetFocusedRowCellValue("TYPE_CD").ToString());
                    }
                }
                else
                {
                    initInputLayout();
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

        #region 저장버튼

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((btnSave.CRUD_type != CRUDType.C) && (gvCom_M.DataRowCount.Equals(0))) return;

            focus_cnt = gvCom_M.FocusedRowHandle;
            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                #region 체크

                string useYN = "Y";
                if (btnSave.CRUD_type == CRUDType.D)
                {
                    DialogResult dlgResult = MsgBoxUtil.AlertQuestion("사용유무를 변경하시겠습니까?");
                    if (dlgResult != DialogResult.OK) return;

                    if (gvCom_D.GetFocusedRowCellValue("USE_YN").ToString() == "Y")
                        useYN = "N";
                    else
                        useYN = "Y";
                }

                txtCode.Text = txtCode.Text.Replace(" ", "");

                if (txtCodeName.Text == string.Empty)
                {
                    MsgBoxUtil.AlertTbxEmpty("코드명");
                    txtCodeName.Focus();
                    return;
                }

                #endregion

                if (gvCom_D.RowCount == 0)
                {
                    //MessageBox.Show("마스터 코드는 필수입니다.");
                    //return;
                }

                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("V_P_FG", temp_crud);
                p.Add("P_TYPE_CD", gvCom_M.GetFocusedRowCellValue("TYPE_CD").ToString());
                p.Add("P_CODE", txtCode.Text);
                p.Add("P_CODE_NAME", txtCodeName.Text);
                p.Add("P_CODE_VALUE1", txtValue1.Text);
                p.Add("P_CODE_VALUE2", txtValue2.Text);
                p.Add("P_CODE_VALUE3", txtValue3.Text);
                p.Add("P_REMARK", txtRemark.Text);
                p.Add("P_USE_YN", cbUse.Text);
                p.Add("P_CRT_USER", clsUserInfo.User_id);

                //20190503 Kimsw 수정
                string msg = DBConn.ExecuteQuerySPR3("SP_SYSCOMMON_S", p);
                if (msg != "-1")
                {
                    MessageBox.Show(msg);
                }

                //_svc.SetQuerySP("SP_SYSCOMMON_TB_WG01_0002_S", p);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSearch.PerformClick();
                gvCom_M.MoveBy(focus_cnt);
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region 사용유무 버튼

        private void btnUse_Click(object sender, EventArgs e)
        {
            if (gvCom_D.DataRowCount == 0) return;

            btnSave.CRUD_type = CRUDType.D;
            btnSave.PerformClick();
        }

        #endregion

        #region getCommonD 기초코드 조회

        private void getCommonD(string comMCD)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "D");
                dict.Add("v_TYPE_CD", gvCom_M.GetFocusedRowCellValue("TYPE_CD").ToString());
                dict.Add("v_TYPE_DESC", comMCD);

                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSCOMMON_R", dict);
                //_svc.GetQuerySP("SP_SYSCOMMON_R", dict);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdCom_D.DataSource = ds.Tables[0];
                }
                else
                {
                    grdCom_D.DataSource = null;
                    initInputLayout();
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region bindInputLayout 입력부바인딩

        private void bindInputLayout()
        {
            DataRow dr = gvCom_D.GetDataRow(gvCom_D.FocusedRowHandle);
            txtCode.DataBindings.Clear();
            txtCodeName.DataBindings.Clear();
            txtRemark.DataBindings.Clear();
            txtValue1.DataBindings.Clear();
            txtValue2.DataBindings.Clear();
            txtValue3.DataBindings.Clear();

            txtCode.DataBindings.Add("EditValue", grdCom_D.DataSource, "CODE");
            txtCodeName.DataBindings.Add("EditValue", grdCom_D.DataSource, "CODE_NAME");
            txtRemark.DataBindings.Add("EditValue", grdCom_D.DataSource, "REMARK");
            txtValue1.DataBindings.Add("EditValue", grdCom_D.DataSource, "CODE_VALUE1");
            txtValue2.DataBindings.Add("EditValue", grdCom_D.DataSource, "CODE_VALUE2");
            txtValue3.DataBindings.Add("EditValue", grdCom_D.DataSource, "CODE_VALUE3");

            txtCode.Enabled = false;
            btnSave.CRUD_type = CRUDType.U;
        }

        #endregion

        #region 포커스 쳉~지

        private void gvCom_M_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gvCom_M.DataRowCount == 0)
                {
                    initInputLayout();
                    grdCom_D.DataSource = null;
                }
                else
                {
                    txtNAME.Text = gvCom_M.GetFocusedRowCellValue("TYPE_DESC").ToString();
                    getCommonD(gvCom_M.GetFocusedRowCellValue("TYPE_CD").ToString());

                    if (gvCom_D.DataRowCount > 0)
                        bindInputLayout();
                }
            }
            catch (Exception exception)
            {

            }
        }

        private void gvCom_D_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvCom_D.DataRowCount == 0)
            {
                initInputLayout();
                return;
            }
            else
            {
                bindInputLayout();
                //Code_Value 2,3 항목 추가
                try
                {
                    
                    txtValue2.Text = gvCom_D.GetFocusedRowCellValue("CODE_VALUE2").ToString();
                    txtValue3.Text = gvCom_D.GetFocusedRowCellValue("CODE_VALUE3").ToString();
                }
                catch (Exception exception)
                {
                    MsgBoxUtil.AlertError(exception.Message);
                }
            }
        }

        #endregion

        #region 신규 버튼

        private void btnNew_Click(object sender, EventArgs e)
        {
            initInputLayout();
            txtCode.Focus();
        }

        #endregion

        #region 마스터코드 수정팝업

        private void grCom_M_DoubleClick(object sender, EventArgs e)
        {
            MasterCode master = new MasterCode();
            master.mastercode = gvCom_M.GetFocusedRowCellValue("TYPE_CD").ToString(); //
            master.mastercodenm = gvCom_M.GetFocusedRowCellValue("TYPE_DESC").ToString();
            if (master.ShowDialog() == DialogResult.Yes)
            {

            }
        }

        #endregion

        private void FrmCommon_Load(object sender, EventArgs e)
        {
            initInputLayout();
        }

        private void grdCom_D_Click(object sender, EventArgs e)
        {

        }

        private void octoButton1_Click(object sender, EventArgs e)
        {
            if (gvCom_D.RowCount == 0) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvCom_D.ExportToXls(desktop + "\\" + this.Text + ".xls");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void grCom_M_Click(object sender, EventArgs e)
        {

        }
    }
}