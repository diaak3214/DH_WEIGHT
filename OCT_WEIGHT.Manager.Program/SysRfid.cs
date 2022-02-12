using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysRfid : OCT_WEIGHT.Manager.Common.FrmBase
    {
        string temp_crud = string.Empty;

        public SysRfid()
        {
            InitializeComponent();
        }

        private void SysRfid_Load(object sender, EventArgs e)
        {
            InitInputLayout();
        }

        #region 조회

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_RFID_NO", txtRFID.Text);

                //20190503 임시수정 Kimsw
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSRFID_R", dict);
                //DataSet ds = _svc.GetQuerySP("SP_SYSRFID_R", dict);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd_rfid.DataSource = ds.Tables[0];
                    GetDataShow(true);
                }
                else
                {
                    grd_rfid.DataSource = null;
                    InitInputLayout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                gvw_rfid.BestFitColumns();
            }
        }

        #endregion

        #region 포커스 췡지

        private void gvw_rfid_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnSave.CRUD_type = CRUDType.U;
        }

        #endregion

        #region 바인딩 설정

        private void GetDataShow(bool isView)
        {
            bool isShow = (isView && gvw_rfid.DataRowCount > 0) ? true : false;

            if (!isShow)
            {
                txtRFID_NO.Text = "";
                txtRFID_SERIAL.Text = "";
                txtVEHL_NO.Text = string.Empty;
                cbFIXED_YN.SelectedIndex = 0;
                cbAUTO_YN.SelectedIndex = 0;
            }
            else
            {
                DataRow dr = gvw_rfid.GetDataRow(gvw_rfid.FocusedRowHandle);
                txtRFID_NO.Text = dr["RFID_NO"].ToString();
                txtRFID_SERIAL.Text = dr["RFID_SERIAL"].ToString();
                txtVEHL_NO.Text = dr["VEHL_NO"].ToString();
                cbFIXED_YN.Text = dr["FIXED_YN"].ToString();
                cbAUTO_YN.Text = dr["AUTO_YN"].ToString();
            }
        }

        #endregion

        #region 입력화면초기화

        private void InitInputLayout()
        {
            txtRFID_NO.Text = "";
            txtRFID_SERIAL.Text = "";
            txtVEHL_NO.Text = string.Empty;
            cbFIXED_YN.SelectedIndex = 0;
            cbAUTO_YN.SelectedIndex = 0;

            btnSave.CRUD_type = CRUDType.C;
        }

        #endregion

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
            InitInputLayout();
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
            if (gvw_rfid.RowCount == 0) return;
            try
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvw_rfid.ExportToXls(desktop + "\\" + this.Text + ".xls");
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

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((btnSave.CRUD_type != CRUDType.C) && (gvw_rfid.DataRowCount.Equals(0))) return;

            if (txtRFID_NO.Text == "")
            {
                MessageBox.Show("카드번호를 입력하세요");
                txtRFID_NO.Focus();
                return;
            }

            if (txtRFID_SERIAL.Text == "")
            {
                MessageBox.Show("시리얼번호를 입력하세요");
                txtRFID_SERIAL.Focus();
                return;
            }

            //if (txtRFID_SERIAL.Text.Length != 24)
            //{
            //    MessageBox.Show("시리얼번호는 24자리입니다. 확인해주세요.");
            //    txtRFID_SERIAL.Focus();
            //    return;
            //}

            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", temp_crud);
                p.Add("P_RFID_NO", txtRFID_NO.Text);
                p.Add("P_RFID_SERIAL", txtRFID_SERIAL.Text);
                p.Add("P_VEHL_NO", txtVEHL_NO.Text);
                p.Add("P_CRT_USER", clsUserInfo.User_id);
                p.Add("P_FIXED_YN", cbFIXED_YN.Text);
                p.Add("P_AUTO_YN", cbAUTO_YN.Text);

                //20190503 임시수정 Kimsw
                string rtn = DBConn.ExecuteQuerySPR3("SP_SYSRFID_TB_WS01_0004_S", p);
                if (rtn != "-1")
                {
                    MsgBoxUtil.AlertError(rtn);
                }

                //_svc.SetQuerySP("SP_SYSRFID_TB_WG01_0004_S", p);
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

        #region 그리드 순번

        private void gvw_rfid_CustomDrawRowIndicator(object sender,
            DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            if (e.Info.IsRowIndicator)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        #endregion

        #region Grid Click

        private void grd_rfid_Click(object sender, EventArgs e)
        {
            if (gvw_rfid.GetFocusedRow() != null)
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