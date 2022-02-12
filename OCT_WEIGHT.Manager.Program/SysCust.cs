using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysCust : OCT_WEIGHT.Manager.Common.FrmBase
    {

        string temp_crud = string.Empty;

        public SysCust()
        {
            InitializeComponent();
        }

        private void SysCust_Load(object sender, EventArgs e)
        {
            btnSave.CRUD_type = CRUDType.C;
        }

        #region 입력화면초기화

        private void InitInputLayout()
        {
            txtCUST_CD.Text = "";
            txtCUST_NM.Text = "";
            txtCUST_CEO.Text = "";
            txtCUST_SAUP.Text = "";
            txtREMARK.Text = "";

            btnSave.CRUD_type = CRUDType.C;
        }

        #endregion

        #region 조회

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            //try
            //{
            //    Dictionary<string, string> dict = new Dictionary<string, string>();
            //    //dict.Add("P_FG", "A");
            //    dict.Add("P_CUST_NAME", txtNAME.Text);

            //    //Kimsw 20190502 수정
            //    DataSet ds = DBConn.ExecuteDataSet2("SP_SYSCUST_R", dict);
            //    //_svc.GetQuerySP("SP_SYSCUST_R", dict);

            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        grd_cust.DataSource = ds.Tables[0];
            //        GetDataShow(true);
            //    }
            //    else
            //    {
            //        grd_cust.DataSource = null;
            //        InitInputLayout();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    //gvw_cust.BestFitColumns();
            //}
        }

        #endregion

        #region 포커스 췡지

        private void gvw_cust_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnSave.CRUD_type = CRUDType.U;
        }

        #endregion

        #region 바인딩 설정

        private void GetDataShow(bool isView)
        {
            bool isShow = (isView && gvw_cust.DataRowCount > 0) ? true : false;

            if (!isShow)
            {
                txtCUST_CD.Text = "";
                txtCUST_NM.Text = "";
                txtCUST_CEO.Text = "";
                txtCUST_SAUP.Text = "";
                txtREMARK.Text = "";
            }
            else
            {
                DataRow dr = gvw_cust.GetDataRow(gvw_cust.FocusedRowHandle);
                txtCUST_CD.Text = dr["CUST_CD"].ToString();
                txtCUST_NM.Text = dr["CUST_NM"].ToString();
                txtCUST_CEO.Text = dr["CUST_CEO"].ToString();
                txtCUST_SAUP.Text = dr["CUST_SAUP"].ToString();
                txtREMARK.Text = dr["REMARK"].ToString();
            }
        }

        #endregion

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
            InitInputLayout();
            txtCUST_CD.Focus();
        }

        #endregion

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((btnSave.CRUD_type != CRUDType.C) && (gvw_cust.DataRowCount.Equals(0))) return;

            if (txtCUST_CD.Text == "")
            {
                MessageBox.Show("거래처코드를 입력하세요");
                txtCUST_CD.Focus();
                return;
            }

            if (txtCUST_NM.Text == "")
            {
                MessageBox.Show("거래처명을 입력하세요");
                txtCUST_NM.Focus();
                return;
            }

            if (txtCUST_CEO.Text == "")
            {
                MessageBox.Show("대표자를 입력하세요");
                txtCUST_CEO.Focus();
                return;
            }

            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", temp_crud.Trim());
                p.Add("P_CUST_CD", txtCUST_CD.Text);
                p.Add("P_CUST_NM", txtCUST_NM.Text);
                p.Add("P_CUST_CEO", txtCUST_CEO.Text);
                p.Add("P_CUST_SAUP", txtCUST_SAUP.Text);
                p.Add("P_REMARK", txtREMARK.Text);
                p.Add("P_CRT_USER", clsUserInfo.User_id);

                //Kimsw 20190502 수정
                //_svc.SetQuerySP("SP_SYSCUST_TB_WG01_0003_S", p);
                string rtn = DBConn.ExecuteQuerySPR3("SP_SYSCUST_TB_WS01_0003_S", p);
                if (rtn != "-1")
                {
                    MessageBox.Show(rtn);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.Message);
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
            if (gvw_cust.RowCount == 0) return;
            try
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvw_cust.ExportToXls(desktop + "\\" + this.Text + ".xls");
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (gvw_cust.DataRowCount == 0) return;

            DialogResult dlgResult = MsgBoxUtil.ConfirmDelete();
            if (dlgResult != DialogResult.OK) return;

            btnSave.CRUD_type = btnDel.CRUD_type;
            btnSave.PerformClick();
        }

        #endregion

        #region 그리드 순번

        private void gvw_cust_CustomDrawRowIndicator(object sender,
            DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            if (e.Info.IsRowIndicator)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        #endregion

        #region Grid Click

        private void grd_cust_Click(object sender, EventArgs e)
        {
            if (gvw_cust.GetFocusedRow() != null)
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