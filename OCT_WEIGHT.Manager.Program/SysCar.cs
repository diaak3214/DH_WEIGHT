using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysCar : OCT_WEIGHT.Manager.Common.FrmBase
    {

        string temp_crud = string.Empty;
        int focus_cnt = 0;
        private int mode = 0;

        public SysCar()
        {
            InitializeComponent();
        }

        #region Onload

        private void SysCar_Load(object sender, EventArgs e)
        {
            InitInputLayout();
        }

        private void SelectUpper(DevExpress.XtraEditors.LookUpEdit LookUp, string Value, string Display, string fg)
        {

            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_CUST_NAME", fg);

                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSCUST_R", dict);
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

        private void SelectUpper2(DevExpress.XtraEditors.LookUpEdit LookUp, string Value, string Display, string fg)
        {

            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "D");
                dict.Add("v_TYPE_CD", "WS_001");
                dict.Add("v_TYPE_DESC", "");

                DataSet ds = DBConn.ExecuteDataSet2("SP_FRMCOMMON_R", dict);
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

        private void InitInputLayout()
        {
            mode = 1;
            txtVEHL_NO.Focus();
            radioGroup1.SelectedIndex = 0;
            START_DATE.DateTime = DateTime.Now.AddDays(-6);
            END_DATE.DateTime = DateTime.Now;
            txtDISCRIPTION.Text = "";
            txtVEHL_NO.Text = "";
            txtVEHL_NO.Enabled = true;
            btnSave.CRUD_type = CRUDType.C;
            //SelectUpper(CUST_CD, "CUST_CD", "CUST_NM", "");
            //SelectUpper2(_OP_CODE, "CODE", "CODE_NAME", "");
            //CUST_CD.ItemIndex = -1;
            CUST_CD.EditValue = "";
            _OP_CODE.EditValue = "";
        }

        #region 종료

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 조회

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            //try
            //{
            //    Dictionary<string, string> dict = new Dictionary<string, string>();
            //    dict.Add("P_GUBUN", radioGroup1.EditValue.ToString());
            //    string cust_code = CUST_CD.GetColumnValue("CUST_CD").ToString();
            //    string op_code = _OP_CODE.GetColumnValue("CODE").ToString();
            //    textEdit1.Text = op_code;
            //    if (cust_code == "")
            //    {
            //        dict.Add("P_CUST_CD", "");
            //    }
            //    else
            //    {
            //        dict.Add("P_CUST_CD", cust_code);
            //    }

            //    if (op_code == "")
            //    {
            //        dict.Add("P_OP_CODE", "");
            //    }
            //    else
            //    {
            //        dict.Add("P_OP_CODE", op_code);
            //    }

            //    dict.Add("P_START_DATE", START_DATE.Text.Replace("-",""));
            //    dict.Add("P_END_DATE", END_DATE.Text.Replace("-", ""));
            //    dict.Add("P_VEHL_NO", txtVEHL_NO.Text);

            //    //임시수정 kimsw 20190430
            //    DataSet ds = DBConn.ExecuteDataSet2("SP_SYSCAR_R", dict);
            //    //_svc.GetQuerySP("SP_SYSCAR_R", dict);

            //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        grd_car.DataSource = ds.Tables[0];
            //        GetDataShow(true);
            //    }
            //    else
            //    {
            //        grd_car.DataSource = null;
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

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((btnSave.CRUD_type != CRUDType.C) && (gvw_car.DataRowCount.Equals(0))) return;

            if (radioGroup1.SelectedIndex == 1)
            {
                if (txtVEHL_NO.Text == "")
                {
                    MessageBox.Show("차량번호를 입력해주세요");
                    txtVEHL_NO.Focus();
                    return;
                }
            }
            else if (radioGroup1.SelectedIndex == 2)
            {
                if (CUST_CD.GetColumnValue("CUST_CD").ToString() == "")
                {
                    MessageBox.Show("거래처를 선택해주세요");
                    CUST_CD.Focus();
                    return;
                }
            }

            if (txtDISCRIPTION.Text == "")
            {
                MessageBox.Show("사유는 필수입니다");
                txtDISCRIPTION.Focus();
                return;
            }

            temp_crud = btnSave.CRUD_type.ToString();
            focus_cnt = gvw_car.FocusedRowHandle;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();

                p.Add("P_FG", temp_crud);

                if (temp_crud != "C")
                    p.Add("P_SEQ", gvw_car.GetFocusedRowCellValue("SEQ").ToString());
                else
                    p.Add("P_SEQ", "");
                p.Add("P_VEHL_NO", gvw_car.GetFocusedRowCellValue("VEHL_NO").ToString());
                p.Add("P_LIMIT_FLAG", gvw_car.GetFocusedRowCellValue("LIMIT_FLAG").ToString()); //차량제한
                /*
                if (radioGroup1.SelectedIndex == 1)
                {
                    p.Add("P_LIMIT_FLAG", "CAR"); //차량제한
                }
                else if (radioGroup1.SelectedIndex == 2)
                {
                    p.Add("P_LIMIT_FLAG", "COMP"); //업체제한
                }
                */

                p.Add("P_CUST_CD", gvw_car.GetFocusedRowCellValue("CUST_CD").ToString());
                p.Add("P_OP_CODE", gvw_car.GetFocusedRowCellValue("OP_CODE").ToString());
                p.Add("P_START_DATE", gvw_car.GetFocusedRowCellValue("START_DATE").ToString());
                p.Add("P_END_DATE", gvw_car.GetFocusedRowCellValue("END_DATE").ToString());
                p.Add("P_DISCRIPTION", gvw_car.GetFocusedRowCellValue("DISCRIPTION").ToString());
                p.Add("P_CRT_USER", clsUserInfo.User_id);

                //20190503 임시수정 Kimsw
                string rtn = DBConn.ExecuteQuerySPR3("SP_SYSCAR_TB_WS01_0009_S", p);
                //_svc.SetQuerySP("SP_SYSCAR_TB_WG01_0009_S", p);

                if (rtn != "-1")
                {
                    MessageBox.Show(rtn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (temp_crud == "C")
                {
                    if (radioGroup1.SelectedIndex == 0)
                    {
                        txtVEHL_NO.Text = txtVEHL_NO.Text;
                    }

                    //START_DATE.Text = START_DATE.Text;
                    //END_DATE.Text = END_DATE.Text;
                    radioGroup1.SelectedIndex = radioGroup1.SelectedIndex + 1;
                }

                btnSearch.PerformClick();
                gvw_car.MoveBy(focus_cnt);
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region 바인딩설정

        private void GetDataShow(bool isView)
        {
            /*
            bool isShow = (isView && gvw_car.DataRowCount > 0) ? true : false;

            if (!isShow)
            {
                txtVEHL_NO.Focus();
                radioGroup1.SelectedIndex = 0;
                START_DATE.DateTime = DateTime.Now.AddDays(-6);
                END_DATE.DateTime = DateTime.Now;
                txtDISCRIPTION.Text = "";
                txtVEHL_NO.Text = "";
                txtVEHL_NO.Enabled = true;

                btnSave.CRUD_type = CRUDType.C;
            }
            else
            {
                DataRow dr = gvw_car.GetDataRow(gvw_car.FocusedRowHandle);

                txtVEHL_NO.Text = dr["VEHL_NO"].ToString();
                START_DATE.Text = dr["START_DATE"].ToString();
                END_DATE.Text = dr["END_DATE"].ToString();
                txtDISCRIPTION.Text = dr["DISCRIPTION"].ToString();
                textEdit1.Text = dr["CUST_CD"].ToString() + " / " + dr["OP_CODE"].ToString();
                CUST_CD.EditValue = dr["CUST_CD"].ToString();
                _OP_CODE.EditValue = dr["OP_CODE"].ToString();
            }
             */ 
        }

        #endregion

        #region 포커스 체인지

        private void gvw_car_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnSave.CRUD_type = CRUDType.U;
        }

        #endregion

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
            InitInputLayout();
        }

        #endregion

        #region 엑셀

        private void octoButton2_Click(object sender, EventArgs e)
        {
            if (gvw_car.DataRowCount == 0) return;
            try
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명
                gvw_car.ExportToXls(desktop + "\\" + this.Text + ".xls");
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

        #region 그리드순번

        private void gvw_car_CustomDrawRowIndicator(object sender,
            DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            if (e.Info.IsRowIndicator)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        #endregion

        #region 삭제

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (gvw_car.DataRowCount == 0) return;

            DialogResult dlgResult = MsgBoxUtil.ConfirmDelete();
            if (dlgResult != DialogResult.OK) return;

            btnSave.CRUD_type = CRUDType.D;
            btnSave.PerformClick();
        }

        #endregion

        #region 라디오버튼 이벤트

        private void radioGroup1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0) //전체
            {
                txtVEHL_NO.Enabled = true;
                CUST_CD.Enabled = true;
            }
            else if (radioGroup1.SelectedIndex == 1) //차량제한
            {
                txtVEHL_NO.Enabled = true;
                CUST_CD.Enabled = false;
            }
            else if (radioGroup1.SelectedIndex == 2) //날짜제한
            {
                txtVEHL_NO.Enabled = false;
                CUST_CD.Enabled = true;
            }
        }

        #endregion

        #region Grid Click

        private void grd_car_Click(object sender, EventArgs e)
        {
            if (gvw_car.GetFocusedRow() != null)
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