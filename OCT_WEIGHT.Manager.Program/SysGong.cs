using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysGong : OCT_WEIGHT.Manager.Common.FrmBase
    {
        string temp_crud = string.Empty;
        int focus_cnt = 0;

        public SysGong()
        {
            InitializeComponent();
        }

        #region Onload

        private void SysGong_Load(object sender, EventArgs e)
        {
            //사용안함(2019-12-09 한민호)
            //RFID_DATE.DateTime = DateTime.Now;
            InitInputLayout();
        }

        #endregion

        private void InitInputLayout()
        {

            txtVEHL_NO.Text = "";
            txtRFID_NO.Text = "";
            RFID_DATETO.Text = DateTime.Now.ToString("yyyy-MM-dd");

            //사용안함(2019-12-09 한민호)
            //txtRFID_NO1.Focus();
            //txtVEHL_NO1.Text = "";
            //txtRFID_NO1.Text = "";
            //txtEMPTY_WEIGTH1.Text = "";
            //RFID_DATE.DateTime = DateTime.Now;
            //RFID_DATE.Text = DateTime.Now.ToString("yyyy-MM-01");
            //txtRFID_NO1.Enabled = true;
            //txtVEHL_NO1.Enabled = true;
            //txtRFID_NO2.Enabled = true;
            //txtVEHL_NO2.Enabled = true;

            btnSave.CRUD_type = CRUDType.C;

        }

        #region 조회

        private void octoButton1_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            //try
            //{
            //    Dictionary<string, string> dict = new Dictionary<string, string>();
            //    dict.Add("P_FG", "A");
            //    dict.Add("P_VEHL_NO", txtVEHL_NO.Text);
            //    dict.Add("P_RFID_NO", txtRFID_NO.Text);
            //    dict.Add("P_RFID_DATE", "");
            //    //공차이력 히스토리 날짜 검색되게 수정(2019-12-01 한민호)
            //    dict.Add("P_RFID_TO_DATE", "");

            //    //20190503 임시수정 Kimsw
            //    DataSet ds = DBConn.ExecuteDataSet2("SP_SYSGONG_R", dict);
            //    //_svc.GetQuerySP("SP_SYSGONG_R", dict);

            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        grdGong_M.DataSource = ds.Tables[0];
            //        getGongD(gvwGong_M.GetFocusedRowCellValue("VEHL_NO").ToString(),
            //            gvwGong_M.GetFocusedRowCellValue("RFID_NO").ToString());

            //        GetDataShow(true);
            //    }
            //    else
            //    {
            //        grdGong_M.DataSource = null;
            //        InitInputLayout();
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

        #region grdGong_M 포커스 체인지

        private void gvwGong_M_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnSave.CRUD_type = CRUDType.U;

            if (gvwGong_M.GetFocusedRow() != null)
            {
                getGongD(gvwGong_M.GetFocusedRowCellValue("VEHL_NO").ToString(),
                    gvwGong_M.GetFocusedRowCellValue("RFID_NO").ToString());
                GetDataShow(true);
            }
            else
            {
                InitInputLayout();
            }
        }

        #endregion

        #region 바인딩설정

        private void GetDataShow(bool isView)
        {
            bool isShow = (isView && gvwGong_M.DataRowCount > 0) ? true : false;

            if (!isShow)
            {
                //사용안함(2019-12-09 한민호)
                //RFID_DATE.DateTime = DateTime.Now;
                //txtVEHL_NO1.Text = "";
                //txtRFID_NO1.Text = "";
                //txtEMPTY_WEIGTH1.Text = "";

                btnSave.CRUD_type = CRUDType.C;
            }
            else
            {
                //txtRFID_NO1.Enabled = false;
                //txtVEHL_NO1.Enabled = false;

                /*
                DataRow dr = gvwGong_M.GetDataRow(gvwGong_M.FocusedRowHandle);
                RFID_DATE.Text = dr["RFID_DATE"].ToString();
                txtVEHL_NO1.Text = dr["VEHL_NO"].ToString();
                txtRFID_NO1.Text = dr["RFID_NO"].ToString();
                txtEMPTY_WEIGTH1.Text = dr["EMPTY_WEIGHT"].ToString();
                */
                //txtRFID_NO2.Enabled = false;
                //txtVEHL_NO2.Enabled = false;

                DataRow dr1 = gvwGong_D.GetDataRow(gvwGong_D.FocusedRowHandle);
                //왼쪽 그리드 클릭 시 왜 오늘날짜를 박지?_막음(2019-12-01 한민호)
                //RFID_DATEFR.Text = dr1["RFID_DATE"].ToString();
                //사용안함(2019-12-09 한민호)
                //txtVEHL_NO2.Text = dr1["VEHL_NO"].ToString();
                //txtRFID_NO2.Text = dr1["RFID_NO"].ToString();
                //numEMPTY_WEIGTH2.EditValue = Convert.ToDecimal( dr1["EMPTY_WEIGHT"]);

            }
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

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            //사용안함(2019-12-09 한민호)
            //if ((btnSave.CRUD_type != CRUDType.C) && (gvwGong_M.DataRowCount.Equals(0))) return;
            
            //if (txtRFID_NO1.Text == "")
            //{
            //    MessageBox.Show("카드번호를 입력해주세요");
            //    RFID_DATEFR.Focus();
            //    return;
            //}

            //if (txtVEHL_NO1.Text == "")
            //{
            //    MessageBox.Show("차량번호를 입력해주세요");
            //    txtVEHL_NO1.Focus();
            //    return;
            //}

            //if (txtEMPTY_WEIGTH1.Text == "")
            //{
            //    MessageBox.Show("공차 계량값을 입력해주세요");
            //    txtEMPTY_WEIGTH1.Focus();
            //    return;
            //}

            //#region RFID 카드 체크

            //Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("P_FG", "R");
            //dict.Add("P_VEHL_NO", "");
            //dict.Add("P_RFID_NO", txtRFID_NO1.Text);
            //dict.Add("P_RFID_DATE", "");

            ////20190503 임시수정 Kimsw
            //DataSet ds = DBConn.ExecuteDataSet2("SP_SYSGONG_R", dict);
            ////_svc.GetQuerySP("SP_SYSGONG_R", dict);

            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    MessageBox.Show("등록되지 않은 카드번호 입니다. 확인해주세요.");
            //    txtRFID_NO1.Focus();
            //    return;
            //}

            //#endregion

            //temp_crud = btnSave.CRUD_type.ToString();
            //focus_cnt = gvwGong_M.FocusedRowHandle;
            //this.Cursor = Cursors.WaitCursor;

            //try
            //{
            //    #region 삭제여부 

            //    if (btnSave.CRUD_type == CRUDType.D)
            //    {
            //        DialogResult dr = MsgBoxUtil.AlertQuestion("삭제여부를 변경하시겠습니까?");
            //        if (dr == DialogResult.OK)
            //        {
            //            Dictionary<string, string> p = new Dictionary<string, string>();
            //            p.Add("P_FG", temp_crud);
            //            p.Add("P_RFID_SEQ", gvwGong_D.GetFocusedRowCellValue("RFID_SEQ").ToString());
            //            p.Add("P_RFID_SERIAL", gvwGong_D.GetFocusedRowCellValue("RFID_SERIAL").ToString());
            //            p.Add("P_RFID_NO", gvwGong_D.GetFocusedRowCellValue("RFID_NO").ToString());
            //            p.Add("P_SEQ", gvwGong_D.GetFocusedRowCellValue("SEQ").ToString());
            //            p.Add("P_CRT_USER", clsUserInfo.User_id);

            //            //20190503 임시수정 Kimsw
            //            DBConn.ExecuteQuerySPR2("SP_SYSGONG_TB_WS01_0006_S", p);
            //            //_svc.SetQuerySP("SP_SYSGONG_TB_WG01_0006_S", p);
            //        }
            //        else
            //            return;
            //    }

            //    #endregion

            //    #region grdGongM 저장

            //    //Dictionary<string, string> p = new Dictionary<string, string>();
            //    //p.Add("P_FG", temp_crud);

            //    //if (temp_crud != "C")
            //    //{
            //    //    p.Add("P_RFID_SEQ", gvwGong_M.GetFocusedRowCellValue("RFID_SEQ").ToString());
            //    //    p.Add("P_RFID_SERIAL", gvwGong_M.GetFocusedRowCellValue("RFID_SERIAL").ToString());
            //    //}
            //    //else
            //    //{
            //    //    p.Add("P_RFID_SEQ", "");
            //    //    p.Add("P_RFID_SERIAL", "");
            //    //}

            //    //p.Add("P_RFID_DATE", RFID_DATE.Text);
            //    //p.Add("P_RFID_NO", txtRFID_NO1.Text);
            //    //p.Add("P_VEHL_NO", txtVEHL_NO1.Text);
            //    //p.Add("P_EMPTY_WEIGHT", txtEMPTY_WEIGTH1.Text);
            //    //p.Add("P_SEQ", "");
            //    //p.Add("P_DEL_YN", "");
            //    //p.Add("P_CRT_USER", clsUserInfo.User_id);

            //    //_svc.SetQuerySP("SP_SYSGONG_TB_WG01_0006_S", p);

            //    #endregion

            //    #region grdGongD 저장

            //    //Dictionary<string, string> p1 = new Dictionary<string, string>();


            //    //p1.Add("P_FG", "U1");
            //    //p1.Add("P_RFID_SEQ", gvwGong_D.GetFocusedRowCellValue("RFID_SEQ").ToString());
            //    //p1.Add("P_RFID_SERIAL", gvwGong_D.GetFocusedRowCellValue("RFID_SERIAL").ToString());
            //    //p1.Add("P_RFID_DATE", RFID_DATE2.Text);
            //    //p1.Add("P_RFID_NO", txtRFID_NO2.Text);
            //    //p1.Add("P_VEHL_NO", txtVEHL_NO2.Text);
            //    //p1.Add("P_EMPTY_WEIGHT", txtEMPTY_WEIGTH2.Text);
            //    //p1.Add("P_SEQ", gvwGong_D.GetFocusedRowCellValue("SEQ").ToString());
            //    //p1.Add("P_DEL_YN", DEL_YN);
            //    //p1.Add("P_CRT_USER", clsUserInfo.User_id);

            //    //_svc.SetQuerySP("SP_SYSGONG_TB_WG01_0006_S", p1);

            //    #endregion


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    btnSearch.PerformClick();
            //    gvwGong_M.MoveBy(focus_cnt);
            //    this.Cursor = Cursors.Default;
            //}

        }

        #endregion

        #region 엑셀

        private void octoButton2_Click(object sender, EventArgs e)
        {
            if (gvwGong_M.DataRowCount == 0) return;

            try
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                gvwGong_M.ExportToXls(desktop + "\\" + this.Text + ".xls");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSave.PerformClick();
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region 히스토리 조회

        private void getGongD(string gongMVEHL, string gongMRFID)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (RFID_DATEFR.DateTime > RFID_DATETO.DateTime)
                {
                    MessageBox.Show("배차시작일자는 배차종료일자 보다 클 수 없습니다.");
                    RFID_DATEFR.Focus();
                    return;
                }

                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "D");
                dict.Add("P_VEHL_NO", gongMVEHL);
                dict.Add("P_RFID_NO", gongMRFID);
                // 동일변수오류(2019-09-27 오창휘 수정)
                dict.Add("P_RFID_DATE", RFID_DATEFR.DateTime.ToString("yyyy-MM-dd"));
                //공차이력 히스토리 날짜 검색되게 수정(2019-12-01 한민호)
                dict.Add("P_RFID_TO_DATE", RFID_DATETO.DateTime.ToString("yyyy-MM-dd"));
                //dict.Add("P_RFID_DATE", RFID_DATETO.DateTime.ToString("yyyy-MM-dd"));
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSGONG_R", dict);
                //_svc.GetQuerySP("SP_SYSGONG_R", dict);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdGong_D.DataSource = ds.Tables[0];
                }
                else
                {
                    grdGong_D.DataSource = null;
                    InitInputLayout();
                }
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

        #endregion

        #region grdGong_D 포커스 체인지

        private void gvwGong_D_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnSave.CRUD_type = CRUDType.U;

            if (gvwGong_D.GetFocusedRow() != null)
            {
                //GetDataShow(true);
            }
            else
            {
                InitInputLayout();
            }

        }

        #endregion

        #region 사용유무

        private void btnUse_Click(object sender, EventArgs e)
        {
            if (gvwGong_D.DataRowCount == 0) return;

            btnSave.CRUD_type = CRUDType.D;
            btnSave.PerformClick();
        }

        #endregion

        //검색 추가(2019-12-01 한민호)
        private void btnSubSearch_Click(object sender, EventArgs e)
        {
            btnSave.CRUD_type = CRUDType.U;

            if (gvwGong_M.GetFocusedRow() != null)
            {
                getGongD(gvwGong_M.GetFocusedRowCellValue("VEHL_NO").ToString(),
                    gvwGong_M.GetFocusedRowCellValue("RFID_NO").ToString());
                GetDataShow(true);
            }
            else
            {
                InitInputLayout();
            }
        }

    }
}