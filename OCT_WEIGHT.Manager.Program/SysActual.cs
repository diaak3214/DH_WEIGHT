using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using OCT_WEIGHT.Manager.Common.util;
using log4net;
using DevExpress.XtraEditors;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysActual : OCT_WEIGHT.Manager.Common.FrmBase
    {
        protected static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string cnt = string.Empty;

        public SysActual()
        {
            InitializeComponent();
        }

        #region  계량대출력매수  조회

        private void octoButton3_Click(object sender, EventArgs e)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                this.Cursor = Cursors.WaitCursor;
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "GRD");
                //dict.Add("V_START_DATE", START_DATE.Text.Trim());
                //dict.Add("V_END_DATE", END_DATE.Text.Trim());
                dict.Add("V_OP_CODE", "");
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSACTUAL_R2", dict);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd_Op.DataSource = ds.Tables[0];
                }
                else
                {
                    grd_Op.DataSource = null;
                    //reset();
                }
                /*
                dict.Clear();
                dict.Add("P_FG", "GRD");
                dict.Add("V_START_DATE", START_DATE.Text.Trim());
                dict.Add("V_END_DATE", END_DATE.Text.Trim());
                dict.Add("V_OP_CODE", textBox1.Text.Trim());
                DataSet ds2 = DBConn.ExecuteDataSet2("SP_SYSACTUAL_R2", dict);

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    gridControlEx1.DataSource = ds2.Tables[0];

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        //txtsubsource1.Text = r[""].ToString();
                    }
                }
                else
                {
                    gridControlEx1.DataSource = null;
                    //reset();
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //grvPaper.BestFitColumns();
                //gvw_op.BestFitColumns();
                //gridViewEx1.BestFitColumns();
                this.Cursor = Cursors.Default;
            }
        }

        #endregion


        #region 계량대출력매수 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {

                #region grdGongM 저장

                /*
                for (int Cnt = 0; Cnt < gvwGong_M.RowCount; Cnt++)
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Add("P_FG", "PNT");
                    p.Add("P_OP_CODE", "PNT");
                    p.Add("P_OP_SEQ", "1");
                    p.Add("P_STATE_FG", "A"); // 계량품목별 출력 구분자 
                    p.Add("P_OP_NM", "PRINT");
                    p.Add("P_ITEM_DAE", gvwGong_M.GetRowCellValue(Cnt, "ITEM_DAE").ToString());
                    p.Add("P_ITEM_JUNG", gvwGong_M.GetRowCellValue(Cnt, "ITEM_JUNG").ToString());
                    p.Add("P_PNT_ONE", gvwGong_M.GetRowCellValue(Cnt, "PNT_ONE").ToString());
                    p.Add("P_PNT_TWO", gvwGong_M.GetRowCellValue(Cnt, "PNT_TWO").ToString());
                    p.Add("P_PNT_ALL", "0");
                    p.Add("P_PNT_NOW", "0");
                    p.Add("P_CUT_YN", "N");
                    p.Add("P_MO_YN", "N");
                    p.Add("P_LPR_YN", "N");
                    p.Add("P_CRT_USER", clsUserInfo.User_id);

                    //20190503 Kimsw 수정
                    DBConn.ExecuteQuerySPR2("SP_SYSACTUAL_TB_WG01_0005_S", p);
                    //_svc.SetQuerySP("SP_SYSACTUAL_TB_WG01_0005_S", p);
                }
                */

                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
                //btnSearch.PerformClick();
            }

        }

        #endregion

        #region 계량대 상태 조회

        private void search_mu(string op_code, string state)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "MU");
                dict.Add("P_OPCODE", op_code);

                //20190503 Kimsw 수정
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSACTUAL_R", dict);
                //_svc.GetQuerySP("SP_SYSACTUAL_R", dict);

                switch (state)
                {
                    case "A": // 계량대 1번 
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            /*
                            txtRFID_NO1.Text = ds.Tables[0].Rows[0]["RFID_NO"].ToString();
                            textEdit1.Text = ds.Tables[0].Rows[0]["VEHL_NO"].ToString();
                            textEdit2.Text = ds.Tables[0].Rows[0]["ITEM_DAE"].ToString();
                            textEdit3.Text = ds.Tables[0].Rows[0]["ITEM_SO_NM"].ToString();
                            textEdit4.Text = ds.Tables[0].Rows[0]["CUST_CD"].ToString();
                            lblFirst_wght.Text = ds.Tables[0].Rows[0]["NOW_WEIGHT"].ToString();
                            labelControl12.Text = ds.Tables[0].Rows[0]["LOAD_WEIGHT"].ToString();
                            labelControl14.Text = ds.Tables[0].Rows[0]["DOWN_WEIGHT"].ToString();
                            labelControl22.Text = ds.Tables[0].Rows[0]["REAL_WGHT"].ToString();
                            memoEdit1.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                            */
                        }
                        else
                        {
                            /*
                            txtRFID_NO1.Text = "";
                            textEdit1.Text = "";
                            textEdit2.Text = "";
                            textEdit3.Text = "";
                            textEdit4.Text = "";
                            lblFirst_wght.Text = "0";
                            labelControl12.Text = "0";
                            labelControl14.Text = "0";
                            labelControl22.Text = "0";
                            memoEdit1.Text = "";
                            */
                        }

                        break;

                    case "B": // 계량대 2번 
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            /*
                            textEdit13.Text = ds.Tables[0].Rows[0]["RFID_NO"].ToString();
                            textEdit12.Text = ds.Tables[0].Rows[0]["VEHL_NO"].ToString();
                            textEdit11.Text = ds.Tables[0].Rows[0]["ITEM_DAE"].ToString();
                            textEdit10.Text = ds.Tables[0].Rows[0]["ITEM_SO_NM"].ToString();
                            textEdit9.Text = ds.Tables[0].Rows[0]["CUST_CD"].ToString();
                            labelControl43.Text = ds.Tables[0].Rows[0]["NOW_WEIGHT"].ToString();
                            labelControl39.Text = ds.Tables[0].Rows[0]["LOAD_WEIGHT"].ToString();
                            labelControl33.Text = ds.Tables[0].Rows[0]["DOWN_WEIGHT"].ToString();
                            labelControl35.Text = ds.Tables[0].Rows[0]["REAL_WGHT"].ToString();
                            memoEdit2.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                            */
                        }
                        else
                        {
                            /*
                            textEdit13.Text = "";
                            textEdit12.Text = "";
                            textEdit11.Text = "";
                            textEdit10.Text = "";
                            textEdit9.Text = "";
                            labelControl43.Text = "0";
                            labelControl39.Text = "0";
                            labelControl33.Text = "0";
                            labelControl35.Text = "0";
                            memoEdit2.Text = "";
                            */
                        }

                        break;
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

        #region 자동 조회 타이머

        private void timer1_Tick(object sender, EventArgs e)
        {
            //// CCTV1              
            /*
            AMC.MediaURL = "http://210.1.10.60/axis-cgi/mjpg/video.cgi";
            AMC.MediaType = "mjpeg";
            AMC.StretchToFit = true;
            AMC.DisplayMessages = false;
            AMC.EnableReconnect = true;
            AMC.Play();

            //계량대 상태 select 
            //// CCTV2
            AMC2.MediaURL = "http://10.10.62.235/axis-cgi/mjpg/video.cgi";
            AMC2.MediaType = "mjpeg";
            AMC2.StretchToFit = true;
            AMC2.DisplayMessages = false;
            AMC2.EnableReconnect = true;
            AMC2.Play();

            search_mu(la_opcode.Text, "A");
            search_mu(la_opcode2.Text, "B");
            */
        }

        #endregion

        #region Load

        private void SysActual_Load(object sender, EventArgs e)
        {
            //START_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //END_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //dtOpdt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            chkMO.CheckState = CheckState.Unchecked;
            chkLPR.CheckState = CheckState.Unchecked;
            chkCUT.CheckState = CheckState.Unchecked;
            //chkRD.CheckState = CheckState.Unchecked;
            //btnChangePaper.Enabled = false;
            //reset();
            print_check();
            //gridControlEx2.Visible = false;
            button1.Visible = false;
            //RepositoryItemComboBox _riEditor = new RepositoryItemComboBox();
            //_riEditor.Items.AddRange(new string[] { "Item1", "Item2", "Item3" });
            //grdPaper.RepositoryItems.Add(_riEditor);
            //gridView1.Columns[1].ColumnEdit = _riEditor;

            //계량대 조회 
            //timer1.Start();

            //하드웨어 조회 - 계량대 코드, 바인딩 위치 
            //search_hw(la_opcode.Text, "A");
            //search_hw(la_opcode2.Text, "B");
        }

        #endregion

        #region Close

        private void SysActual_FormClosing(object sender, FormClosingEventArgs e)
        {
            //timer1.Stop();
        }

        #endregion

        #region 하드웨어 조회

        private void search_hw(string op_code, string state)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "HW");
                dict.Add("P_OPCODE", op_code);

                //임시수정 kimsw 20190430
                DataSet ds = DBConn.ExecuteDataSet2("SP_SYSACTUAL_R", dict);
                //_svc.GetQuerySP("SP_SYSACTUAL_R", dict);

                switch (state)
                {
                    case "A": // 계량대 1번 
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            /*
                            textEdit7.Text = ds.Tables[0].Rows[0]["PNT_ALL"].ToString();
                            textEdit8.Text = ds.Tables[0].Rows[0]["PNT_NOW"].ToString();
                            radioGroup6.EditValue = ds.Tables[0].Rows[0]["LPR_YN"].ToString();
                            radioGroup5.EditValue = ds.Tables[0].Rows[0]["MO_YN"].ToString();
                            radioGroup4.EditValue = ds.Tables[0].Rows[0]["CUT_YN"].ToString();
                        }
                        else
                        {
                            textEdit7.Text = "0";
                            textEdit8.Text = "0";
                            radioGroup6.EditValue = "Y";
                            radioGroup5.EditValue = "Y";
                            radioGroup4.EditValue = "Y";
                             */
                        }

                        break;

                    case "B": // 계량대 2번 
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            /*
                            textEdit5.Text = ds.Tables[0].Rows[0]["PNT_ALL"].ToString();
                            textEdit6.Text = ds.Tables[0].Rows[0]["PNT_NOW"].ToString();
                            radioGroup3.EditValue = ds.Tables[0].Rows[0]["LPR_YN"].ToString();
                            radioGroup2.EditValue = ds.Tables[0].Rows[0]["MO_YN"].ToString();
                            radioGroup1.EditValue = ds.Tables[0].Rows[0]["CUT_YN"].ToString();
                        }
                        else
                        {
                            textEdit5.Text = "0";
                            textEdit6.Text = "0";
                            radioGroup3.EditValue = "Y";
                            radioGroup2.EditValue = "Y";
                            radioGroup1.EditValue = "Y";
                            */
                        }

                        break;
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

        #region 하드웨어 저장

        private void octoButton2_Click(object sender, EventArgs e)
        {
            //hw_save(la_opcode.Text, "A");
            //하드웨어 조회 - 계량대 코드, 바인딩 위치 
            //search_hw(la_opcode.Text, "A");

        }

        private void octoButton4_Click(object sender, EventArgs e)
        {
            //hw_save(la_opcode2.Text, "B");
            //search_hw(la_opcode2.Text, "B");
        }

        private void hw_save(string op_code, string state)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", "HW");
                p.Add("P_OP_CODE", op_code);
                p.Add("P_OP_SEQ", "1");
                p.Add("P_STATE_FG", "C"); // 계량품목별 하드웨어 구분자 
                p.Add("P_OP_NM", "H/W");
                p.Add("P_ITEM_DAE", "");
                p.Add("P_ITEM_JUNG", "");
                p.Add("P_PNT_ONE", "");
                p.Add("P_PNT_TWO", "");

                if (state == "A")
                {
                    /*
                    p.Add("P_PNT_ALL", textEdit7.Text);
                    p.Add("P_PNT_NOW", textEdit8.Text);
                    p.Add("P_CUT_YN", radioGroup4.EditValue.ToString());
                    p.Add("P_MO_YN", radioGroup5.EditValue.ToString());
                    p.Add("P_LPR_YN", radioGroup6.EditValue.ToString());
                    */
                }
                else if (state == "B")
                {
                    /*
                    p.Add("P_PNT_ALL", textEdit5.Text);
                    p.Add("P_PNT_NOW", textEdit6.Text);
                    p.Add("P_CUT_YN", radioGroup1.EditValue.ToString());
                    p.Add("P_MO_YN", radioGroup2.EditValue.ToString());
                    p.Add("P_LPR_YN", radioGroup3.EditValue.ToString());
                    */
                }

                p.Add("P_CRT_USER", clsUserInfo.User_id);

                //임시수정 kimsw 20190503
                DBConn.ExecuteQuerySPR2("SP_SYSACTUAL_TB_WS01_0005_S", p);
                //_svc.SetQuerySP("SP_SYSACTUAL_TB_WG01_0005_S", p);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MessageBox.Show("저장되었습니다");
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region 종료

        private void octoButton4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 출력매수저장

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                string rtn = string.Empty;
                string code = gvw_op.GetFocusedRowCellValue("CODE").ToString();
                Dictionary<string, string> p = new Dictionary<string, string>();
                DataSet ds = DBConn._ExDataSet("SP_ITEMLIST");
                DataTable dt = ds.Tables[0];
                DataRow[] rows = dt.Select();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    p.Clear();
                    p.Add("P_TYPE", "N4");
                    p.Add("P_OP_CODE", code);
                    p.Add("P_ITEM_JUNG", rows[i]["ITEM_JUNG"].ToString());
                    if (rows[i]["ITEM_JUNG"].ToString() == "1")
                    {
                        p.Add("PNT_ONE", txtsubsource1.Text);
                        p.Add("PNT_TWO", txtsubsource2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "2")
                    {
                        p.Add("PNT_ONE", txtsub1.Text);
                        p.Add("PNT_TWO", txtsub2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsub3.Text);
                        //p.Add("P_PNT_NOW", txtsub4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "3")
                    {
                        p.Add("PNT_ONE", txtsave1.Text);
                        p.Add("PNT_TWO", txtsave2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsave3.Text);
                        //p.Add("P_PNT_NOW", txtsave4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "4")
                    {
                        p.Add("PNT_ONE", txtnomal1.Text);
                        p.Add("PNT_TWO", txtnomal2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtnomal3.Text);
                        //p.Add("P_PNT_NOW", txtnomal4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "5")
                    {
                        p.Add("PNT_ONE", txtair1.Text);
                        p.Add("PNT_TWO", txtair2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtair3.Text);
                        //p.Add("P_PNT_NOW", txtair4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "6")
                    {
                        p.Add("PNT_ONE", txtinnermove1.Text);
                        p.Add("PNT_TWO", txtinnermove2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtinnermove3.Text);
                        //p.Add("P_PNT_NOW", txtinnermove4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "7")
                    {
                        p.Add("PNT_ONE", txtproduct1.Text);
                        p.Add("PNT_TWO", txtproduct2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtproduct3.Text);
                        //p.Add("P_PNT_NOW", txtproduct4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "E")
                    {
                        p.Add("PNT_ONE", txtsource1.Text);
                        p.Add("PNT_TWO", txtsource2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsource3.Text);
                        //p.Add("P_PNT_NOW", txtsource4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "G")
                    {
                        p.Add("PNT_ONE", txtect1.Text);
                        p.Add("PNT_TWO", txtect1.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtect1.Text);
                        //p.Add("P_PNT_NOW", txtect1.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "W")
                    {
                        p.Add("PNT_ONE", txtadd1.Text);
                        p.Add("PNT_TWO", txtadd2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtadd3.Text);
                        //p.Add("P_PNT_NOW", txtadd4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "Z")
                    {
                        p.Add("PNT_ONE", txtbusanmul1.Text);
                        p.Add("PNT_TWO", txtbusanmul2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtbusanmul3.Text);
                        //p.Add("P_PNT_NOW", txtbusanmul4.Text);
                    }

                    p.Add("P_MO_YN", checkval(chkMO)); //chkMO.Checked ? "Y" : "N");
                    p.Add("P_LPR_YN", checkval(chkLPR)); //chkLPR.Checked ? "Y" : "N");
                    //p.Add("P_CUT_YN", checkval(chkCUT)); //chkCUT.Checked ? "Y" : "N");
                    p.Add("P_RD_YN", checkval(chkRD)); //chkRD.Checked ? "Y" : "N");

                    p.Add("P_CRT_USER", clsUserInfo.User_id);
                    rtn = DBConn.ExecuteQuerySPR3("SP_SYSACTUAL_TB_WS01_0005_S", p);

                    if (rtn != "-1")
                    {
                        MsgBoxUtil.AlertError(rtn);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.AlertError(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
                //BtnSearch.PerformClick();

                gvw_op.SetFocusedRowCellValue("MO_YN", checkval(chkMO));
                gvw_op.SetFocusedRowCellValue("LPR_YN", checkval(chkLPR));
                gvw_op.SetFocusedRowCellValue("RD_YN", checkval(chkRD));
                gvw_op.SetFocusedRowCellValue("PNT_ALL", txtsubsource3.Text);
                gvw_op.SetFocusedRowCellValue("PNT_NOW", txtsubsource4.Text);

            }
        }

        string checkval(CheckEdit chk)
        {
            string ret = string.Empty;
            if (chk.Checked)
            {
                ret = "Y";
            }
            else
            {
                ret = "N";
            }

            return ret;
        }

        #endregion

        #region Focus Change

        private void gvw_op_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvw_op.RowCount > 0)
            {
                //if (_svc == null) _svc = new ServiceAdapter();

                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("P_TYPE_CD", "WS_006");
                    DataSet ds = DBConn.ExecuteDataSet2("SP_COMBOX_R", dict);
                    DataTable dt = ds.Tables[0];
                    DataRow[] rows1 = dt.Select();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string _code = gvw_op.GetFocusedRowCellValue("CODE").ToString();
                        textBox1.Text = _code;
                        dict.Clear();
                        dict.Add("P_FG", "MAT");
                        dict.Add("V_DATE", "");
                        //dict.Add("V_END_DATE", END_DATE.Text.Trim());
                        dict.Add("V_OP_CODE", _code);
                        dict.Add("V_ITEM_JUNG", rows1[i]["CODE"].ToString());
                        DataSet ds2 = DBConn.ExecuteDataSet2("SP_PAPERACTUAL_R", dict);
                        DataTable dt2 = ds2.Tables[0];
                        DataRow[] rows2 = dt2.Select();
                        if (dt2.Rows.Count == 0) reset();
                        
                        for (int k = 0; k < dt2.Rows.Count; k++)
                        {
                          
                            if (rows2[k]["ITEM_JUNG"].ToString() == "1")
                            {
                                txtsubsource1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtsubsource2.Text = rows2[k]["PNT_TWO"].ToString();
                                txtsubsource3.Text = rows2[k]["PNT_ALL"].ToString();
                                txtsubsource4.Text = rows2[k]["PNT_NOW"].ToString();          
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                            else if (rows2[k]["ITEM_JUNG"].ToString() == "2")
                            {
                                txtsub1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtsub2.Text = rows2[k]["PNT_TWO"].ToString();
                                //txtsub3.Text = rows2[k]["PNT_NOW"].ToString();
                                //txtsub4.Text = rows2[k]["PNT_ALL"].ToString();
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                            else if (rows2[k]["ITEM_JUNG"].ToString() == "3")
                            {
                                txtsave1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtsave2.Text = rows2[k]["PNT_TWO"].ToString();
                                //txtsave3.Text = rows2[k]["PNT_NOW"].ToString();
                               // txtsave4.Text = rows2[k]["PNT_ALL"].ToString();
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                            else if (rows2[k]["ITEM_JUNG"].ToString() == "4")
                            {
                                txtnomal1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtnomal2.Text = rows2[k]["PNT_TWO"].ToString();
                                //txtnomal3.Text = rows2[k]["PNT_NOW"].ToString();
                                //txtnomal4.Text = rows2[k]["PNT_ALL"].ToString();
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                            else if (rows2[k]["ITEM_JUNG"].ToString() == "5")
                            {
                                txtair1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtair2.Text = rows2[k]["PNT_TWO"].ToString();
                                //txtair3.Text = rows2[k]["PNT_NOW"].ToString();
                                //txtair4.Text = rows2[k]["PNT_ALL"].ToString();
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                            else if (rows2[k]["ITEM_JUNG"].ToString() == "6")
                            {
                                txtinnermove1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtinnermove2.Text = rows2[k]["PNT_TWO"].ToString();
                                //txtinnermove3.Text = rows2[k]["PNT_NOW"].ToString();
                                //txtinnermove4.Text = rows2[k]["PNT_ALL"].ToString();
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                            else if (rows2[k]["ITEM_JUNG"].ToString() == "7")
                            {
                                txtproduct1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtproduct2.Text = rows2[k]["PNT_TWO"].ToString();
                                //txtproduct3.Text = rows2[k]["PNT_NOW"].ToString();
                                //txtproduct4.Text = rows2[k]["PNT_ALL"].ToString();
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                            else if (rows2[k]["ITEM_JUNG"].ToString() == "E")
                            {
                                txtsource1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtsource2.Text = rows2[k]["PNT_TWO"].ToString();
                                //txtsource3.Text = rows2[k]["PNT_NOW"].ToString();
                                //txtsource4.Text = rows2[k]["PNT_ALL"].ToString();
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                            else if (rows2[k]["ITEM_JUNG"].ToString() == "G")
                            {
                                txtect1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtect2.Text = rows2[k]["PNT_TWO"].ToString();
                                //txtect3.Text = rows2[k]["PNT_NOW"].ToString();
                                //txtect4.Text = rows2[k]["PNT_ALL"].ToString();
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                            else if (rows2[k]["ITEM_JUNG"].ToString() == "W")
                            {
                                txtadd1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtadd2.Text = rows2[k]["PNT_TWO"].ToString();
                                //txtadd3.Text = rows2[k]["PNT_NOW"].ToString();
                                //txtadd4.Text = rows2[k]["PNT_ALL"].ToString();
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                            else if (rows2[k]["ITEM_JUNG"].ToString() == "Z")
                            {
                                txtbusanmul1.Text = rows2[k]["PNT_ONE"].ToString();
                                txtbusanmul2.Text = rows2[k]["PNT_TWO"].ToString();
                                //txtbusanmul4.Text = rows2[k]["PNT_NOW"].ToString();
                                //txtbusanmul3.Text = rows2[k]["PNT_ALL"].ToString();
                                checkboxset(ds2, START_DATE.Text.Trim(), END_DATE.Text.Trim(), _code,
                                    rows2[k]["ITEM_JUNG"].ToString());
                            }
                        }

                   

                        gridControlEx2.DataSource = dt2;

                        if (gvw_op.GetFocusedRowCellValue("MO_YN").ToString() == "Y")
                        {
                            chkMO.Checked = true;
                        }
                        else
                        {
                            chkMO.Checked = false;
                        }
                        if (gvw_op.GetFocusedRowCellValue("LPR_YN").ToString() == "Y")
                        {
                            chkLPR.Checked = true;
                        }
                        else
                        {
                            chkLPR.Checked = false;
                        }
                        if (gvw_op.GetFocusedRowCellValue("RD_YN").ToString() == "Y")
                        {
                            chkRD.Checked = true;
                        }
                        else
                        {
                            chkRD.Checked = false;
                        }


                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Pnt Loading Error" + ex.Message);
                    MsgBoxUtil.AlertError(ex.ToString());
                }
                finally
                {
                    //grvPaper.BestFitColumns();
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void checkboxset(DataSet ds2, string p, string p_2, string _code, string p_3)
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "DEV");
                //dict.Add("V_START_DATE", p);
                //dict.Add("V_END_DATE", p_2);
                dict.Add("V_DATE", p);
                dict.Add("V_OP_CODE", _code);
                dict.Add("V_ITEM_JUNG", p_3);
                DataSet ds = DBConn.ExecuteDataSet2("SP_PAPERACTUAL_R", dict);

               // textBox2.Text = p + Environment.NewLine;
                //textBox2.Text = p_2 + Environment.NewLine;
                //textBox2.Text = _code + Environment.NewLine;
                //textBox2.Text = p_3 + Environment.NewLine;
                //textBox2.Text = Convert.ToString(ds.Tables[0].Rows.Count);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        chkMO.Checked = "Y".Equals(r["MO_YN"].ToString(), StringComparison.OrdinalIgnoreCase);
                        chkLPR.Checked = "Y".Equals(r["LPR_YN"].ToString(), StringComparison.OrdinalIgnoreCase);
                        chkCUT.Checked = "Y".Equals(r["CUT_YN"].ToString(), StringComparison.OrdinalIgnoreCase);
                        chkRD.Checked = "Y".Equals(r["RD_YN"].ToString(), StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("장치 사용 여부 에러 " + ex.Message);
                MsgBoxUtil.AlertError(ex.ToString());
            }
        }

        string ret(string _data)
        {
            if (_data.Length > 0)
            {
                return _data;
            }
            else
            {
                return "0";
            }
        }

        void reset()
        {
            foreach (Control c in panelControl3.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = Convert.ToString(0);
                }
            }
        }

        private void grvPaper_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            /*
            if (grvPaper.RowCount > 0)
            {
                settextdata();
            }
            else
            {
                BtnNew.PerformClick();
            }
            */
        }

        private void gridViewEx1_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewEx1.RowCount > 0)
            {
                /*
                DataRow dr = gridViewEx1.GetDataRow(gridViewEx1.FocusedRowHandle);

                //BindingSource bsTemp = (BindingSource)gridViewEx1.DataSource;
                DataTable dt = (DataTable)gridControlEx2.DataSource;
                
                foreach (Control c in panelControl3.Controls)
                {
                    if (c is TextBox)
                    {
                        c.DataBindings.Clear();
                    }
                }

                int row = gridViewEx1.RowCount;

                for (int i = 0; i < row; i++)
                {
                    if (i == 1)
                    {
                        txtsubsource1.Text = dr["PNT_ONE"].ToString();
                        txtsubsource2.Text = dr["PNT_TWO"].ToString();
                        txtsubsource3.Text = dr["PNT_ONE"].ToString();
                        txtsubsource4.Text = dr["PNT_ONE"].ToString();

                    }
                    else if (i == 2)
                    {
                        txtsub1.Text = dr["PNT_ONE"].ToString();
                        txtsub2.Text = dr["PNT_TWO"].ToString();
                        txtsub3.Text = dr["PNT_ONE"].ToString();
                        txtsub4.Text = dr["PNT_ONE"].ToString();
                    }
                    else if (i == 3)
                    {
                        txtsave1.Text = dr["PNT_ONE"].ToString();
                        txtsave2.Text = dr["PNT_TWO"].ToString();
                        txtsave3.Text = dr["PNT_ONE"].ToString();
                        txtsave4.Text = dr["PNT_ONE"].ToString();
                    }
                    else if (i == 4)
                    {
                        txtnomal1.Text = dr["PNT_ONE"].ToString();
                        txtnomal2.Text = dr["PNT_TWO"].ToString();
                        txtnomal3.Text = dr["PNT_ONE"].ToString();
                        txtnomal4.Text = dr["PNT_ONE"].ToString();
                    }
                    else if (i == 5)
                    {
                        txtair1.Text = dr["PNT_ONE"].ToString();
                        txtair2.Text = dr["PNT_TWO"].ToString();
                        txtair3.Text = dr["PNT_ONE"].ToString();
                        txtair4.Text = dr["PNT_ONE"].ToString();
                    }
                    else if (i == 6)
                    {
                        txtinnermove1.Text = dr["PNT_ONE"].ToString();
                        txtinnermove2.Text = dr["PNT_TWO"].ToString();
                        txtinnermove3.Text = dr["PNT_ONE"].ToString();
                        txtinnermove4.Text = dr["PNT_ONE"].ToString();
                    }
                    else if (i == 7)
                    {
                        txtproduct1.Text = dr["PNT_ONE"].ToString();
                        txtproduct2.Text = dr["PNT_TWO"].ToString();
                        txtproduct3.Text = dr["PNT_ONE"].ToString();
                        txtproduct4.Text = dr["PNT_ONE"].ToString();
                    }
                    else if (i == 8)
                    {
                        txtsource1.Text = dr["PNT_ONE"].ToString();
                        txtsource2.Text = dr["PNT_TWO"].ToString();
                        txtsource3.Text = dr["PNT_ONE"].ToString();
                        txtsource4.Text = dr["PNT_ONE"].ToString();
                    }
                    else if (i == 8)
                    {
                        txtect1.Text = dr["PNT_ONE"].ToString();
                        txtect2.Text = dr["PNT_TWO"].ToString();
                        txtect3.Text = dr["PNT_ONE"].ToString();
                        txtect4.Text = dr["PNT_ONE"].ToString();
                    }
                    else if (i == 9)
                    {
                        txtadd1.Text = dr["PNT_ONE"].ToString();
                        txtadd2.Text = dr["PNT_TWO"].ToString();
                        txtadd3.Text = dr["PNT_ONE"].ToString();
                        txtadd4.Text = dr["PNT_ONE"].ToString();
                    }
                    else if (i == 10)
                    {
                        txtbusanmul1.Text = dr["PNT_ONE"].ToString();
                        txtbusanmul1.Text = dr["PNT_TWO"].ToString();
                        txtbusanmul1.Text = dr["PNT_ONE"].ToString();
                        txtbusanmul1.Text = dr["PNT_ONE"].ToString();
                    }

                    foreach (DataRow r in dt.Rows) //ds.Tables[0].Rows)
                    {
                        chkMO.Checked = "Y".Equals(r["MO_YN"].ToString(), StringComparison.OrdinalIgnoreCase);
                        chkLPR.Checked = "Y".Equals(r["LPR_YN"].ToString(), StringComparison.OrdinalIgnoreCase);
                        chkCUT.Checked = "Y".Equals(r["CUT_YN"].ToString(), StringComparison.OrdinalIgnoreCase);
                        chkRD.Checked = "Y".Equals(r["RD_YN"].ToString(), StringComparison.OrdinalIgnoreCase);
                    }
                }
                */
            }
        }

        #endregion

        #region 신규

        private void BtnNew_Click(object sender, EventArgs e)
        {
            grd_Op.DataSource = null;
            //grdPaper.DataSource = null;
            //txtOPNM.Text = string.Empty;
            //dtOpdt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            START_DATE.DateTime = DateTime.Now;
            END_DATE.DateTime = DateTime.Now;
            chkMO.Checked = false;
            chkLPR.Checked = false;
            chkCUT.Checked = false;
            chkRD.Checked = false;
            txtsub1.Text = string.Empty;
            txtsubsource1.Text = string.Empty;
            txtsubsource3.Text = string.Empty;
            txtsubsource4.Text = string.Empty;
            txtsub1.Focus();
        }

        #endregion

        #region Print Check

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //SP_MU_PRINT_CHK_R Print Check
            //print_check();

            this.Cursor = Cursors.WaitCursor;

            try
            {
                string rtn = string.Empty;
                string code = gvw_op.GetFocusedRowCellValue("CODE").ToString();
                Dictionary<string, string> p = new Dictionary<string, string>();
                DataSet ds = DBConn._ExDataSet("SP_ITEMLIST");
                DataTable dt = ds.Tables[0];
                DataRow[] rows = dt.Select();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    p.Clear();
                    p.Add("P_TYPE", "N2");
                    p.Add("P_OP_CODE", code);
                    p.Add("P_ITEM_JUNG", rows[i]["ITEM_JUNG"].ToString());
                    if (rows[i]["ITEM_JUNG"].ToString() == "1")
                    {
                        p.Add("PNT_ONE", txtsubsource1.Text);
                        p.Add("PNT_TWO", txtsubsource2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "2")
                    {
                        p.Add("PNT_ONE", txtsub1.Text);
                        p.Add("PNT_TWO", txtsub2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsub3.Text);
                        //p.Add("P_PNT_NOW", txtsub4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "3")
                    {
                        p.Add("PNT_ONE", txtsave1.Text);
                        p.Add("PNT_TWO", txtsave2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsave3.Text);
                        //p.Add("P_PNT_NOW", txtsave4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "4")
                    {
                        p.Add("PNT_ONE", txtnomal1.Text);
                        p.Add("PNT_TWO", txtnomal2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtnomal3.Text);
                        //p.Add("P_PNT_NOW", txtnomal4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "5")
                    {
                        p.Add("PNT_ONE", txtair1.Text);
                        p.Add("PNT_TWO", txtair2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtair3.Text);
                        //p.Add("P_PNT_NOW", txtair4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "6")
                    {
                        p.Add("PNT_ONE", txtinnermove1.Text);
                        p.Add("PNT_TWO", txtinnermove2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtinnermove3.Text);
                        //p.Add("P_PNT_NOW", txtinnermove4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "7")
                    {
                        p.Add("PNT_ONE", txtproduct1.Text);
                        p.Add("PNT_TWO", txtproduct2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtproduct3.Text);
                        //p.Add("P_PNT_NOW", txtproduct4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "E")
                    {
                        p.Add("PNT_ONE", txtsource1.Text);
                        p.Add("PNT_TWO", txtsource2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsource3.Text);
                        //p.Add("P_PNT_NOW", txtsource4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "G")
                    {
                        p.Add("PNT_ONE", txtect1.Text);
                        p.Add("PNT_TWO", txtect1.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtect1.Text);
                        //p.Add("P_PNT_NOW", txtect1.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "W")
                    {
                        p.Add("PNT_ONE", txtadd1.Text);
                        p.Add("PNT_TWO", txtadd2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtadd3.Text);
                        //p.Add("P_PNT_NOW", txtadd4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "Z")
                    {
                        p.Add("PNT_ONE", txtbusanmul1.Text);
                        p.Add("PNT_TWO", txtbusanmul2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtbusanmul3.Text);
                        //p.Add("P_PNT_NOW", txtbusanmul4.Text);
                    }

                    p.Add("P_MO_YN", checkval(chkMO)); //chkMO.Checked ? "Y" : "N");
                    p.Add("P_LPR_YN", checkval(chkLPR)); //chkLPR.Checked ? "Y" : "N");
                    //p.Add("P_CUT_YN", checkval(chkCUT)); //chkCUT.Checked ? "Y" : "N");
                    p.Add("P_RD_YN", checkval(chkRD)); //chkRD.Checked ? "Y" : "N");

                    p.Add("P_CRT_USER", clsUserInfo.User_id);
                    rtn = DBConn.ExecuteQuerySPR3("SP_SYSACTUAL_TB_WS01_0005_S", p);

                    if (rtn != "-1")
                    {
                        MsgBoxUtil.AlertError(rtn);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.AlertError(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
                //BtnSearch.PerformClick();

                gvw_op.SetFocusedRowCellValue("MO_YN", checkval(chkMO));
                gvw_op.SetFocusedRowCellValue("LPR_YN", checkval(chkLPR));
                gvw_op.SetFocusedRowCellValue("RD_YN", checkval(chkRD));
                gvw_op.SetFocusedRowCellValue("PNT_ALL", txtsubsource3.Text);
                gvw_op.SetFocusedRowCellValue("PNT_NOW", txtsubsource4.Text);

            }
        }

        #endregion

        #region 프린트 용지 체크

        public void print_check()
        {

            /*
            try
            {
                //if (_svc == null) _svc = new ServiceAdapter();

                //DataTable dt_print = DB_Process.PRINT_CHK_R(Weight_Area);
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_WEIGHT_AREA", "계량대구분");
                DataSet ds2 = DBConn.ExecuteDataSet2("SP_MU_PRINT_CHK_R", dict);
                DataTable dt_print = new DataTable();
                dt_print = ds2.Tables[0];
                if (dt_print.Rows.Count > 0)
                {
                    if (dt_print.Rows[0]["STATUS"].ToString() == "USE")
                    {
                        btnChangePaper.Enabled = false;
                        //print_bt.Image = imageList4.Images[0];
                    }
                    else if (dt_print.Rows[0]["STATUS"].ToString() == "REPLACE")
                    {
                        btnChangePaper.Enabled = true;
                        //print_bt.Image = imageList4.Images[1];
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("PRINT 용지 체크 에러 : " + ex.Message.ToString());
            }
            */
            


        }

        #endregion

        private void START_DATE_EditValueChanged(object sender, EventArgs e)
        {
            END_DATE.DateTime = START_DATE.DateTime;
        }

        private void END_DATE_EditValueChanged(object sender, EventArgs e)
        {
            START_DATE.DateTime = END_DATE.DateTime;
        }

        private void octoButton2_Click_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                string rtn = string.Empty;
                string code = gvw_op.GetFocusedRowCellValue("CODE").ToString();
                Dictionary<string, string> p = new Dictionary<string, string>();
                DataSet ds = DBConn._ExDataSet("SP_ITEMLIST");
                DataTable dt = ds.Tables[0];
                DataRow[] rows = dt.Select();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    p.Clear();
                    p.Add("P_TYPE", "N1");
                    p.Add("P_OP_CODE", code);
                    p.Add("P_ITEM_JUNG", rows[i]["ITEM_JUNG"].ToString());
                    if (rows[i]["ITEM_JUNG"].ToString() == "1")
                    {
                        p.Add("PNT_ONE", txtsubsource1.Text);
                        p.Add("PNT_TWO", txtsubsource2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "2")
                    {
                        p.Add("PNT_ONE", txtsub1.Text);
                        p.Add("PNT_TWO", txtsub2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsub3.Text);
                        //p.Add("P_PNT_NOW", txtsub4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "3")
                    {
                        p.Add("PNT_ONE", txtsave1.Text);
                        p.Add("PNT_TWO", txtsave2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsave3.Text);
                        //p.Add("P_PNT_NOW", txtsave4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "4")
                    {
                        p.Add("PNT_ONE", txtnomal1.Text);
                        p.Add("PNT_TWO", txtnomal2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtnomal3.Text);
                        //p.Add("P_PNT_NOW", txtnomal4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "5")
                    {
                        p.Add("PNT_ONE", txtair1.Text);
                        p.Add("PNT_TWO", txtair2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtair3.Text);
                        //p.Add("P_PNT_NOW", txtair4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "6")
                    {
                        p.Add("PNT_ONE", txtinnermove1.Text);
                        p.Add("PNT_TWO", txtinnermove2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtinnermove3.Text);
                        //p.Add("P_PNT_NOW", txtinnermove4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "7")
                    {
                        p.Add("PNT_ONE", txtproduct1.Text);
                        p.Add("PNT_TWO", txtproduct2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtproduct3.Text);
                        //p.Add("P_PNT_NOW", txtproduct4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "E")
                    {
                        p.Add("PNT_ONE", txtsource1.Text);
                        p.Add("PNT_TWO", txtsource2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsource3.Text);
                        //p.Add("P_PNT_NOW", txtsource4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "G")
                    {
                        p.Add("PNT_ONE", txtect1.Text);
                        p.Add("PNT_TWO", txtect1.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtect1.Text);
                        //p.Add("P_PNT_NOW", txtect1.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "W")
                    {
                        p.Add("PNT_ONE", txtadd1.Text);
                        p.Add("PNT_TWO", txtadd2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtadd3.Text);
                        //p.Add("P_PNT_NOW", txtadd4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "Z")
                    {
                        p.Add("PNT_ONE", txtbusanmul1.Text);
                        p.Add("PNT_TWO", txtbusanmul2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtbusanmul3.Text);
                        //p.Add("P_PNT_NOW", txtbusanmul4.Text);
                    }

                    p.Add("P_MO_YN", checkval(chkMO)); //chkMO.Checked ? "Y" : "N");
                    p.Add("P_LPR_YN", checkval(chkLPR)); //chkLPR.Checked ? "Y" : "N");
                    //p.Add("P_CUT_YN", checkval(chkCUT)); //chkCUT.Checked ? "Y" : "N");
                    p.Add("P_RD_YN", checkval(chkRD)); //chkRD.Checked ? "Y" : "N");

                    p.Add("P_CRT_USER", clsUserInfo.User_id);
                    rtn = DBConn.ExecuteQuerySPR3("SP_SYSACTUAL_TB_WS01_0005_S", p);

                    if (rtn != "-1")
                    {
                        MsgBoxUtil.AlertError(rtn);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.AlertError(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
                   gvw_op.SetFocusedRowCellValue("MO_YN", checkval(chkMO));
                gvw_op.SetFocusedRowCellValue("LPR_YN", checkval(chkLPR));
                gvw_op.SetFocusedRowCellValue("RD_YN", checkval(chkRD));
                gvw_op.SetFocusedRowCellValue("PNT_ALL", txtsubsource3.Text);
                gvw_op.SetFocusedRowCellValue("PNT_NOW", txtsubsource4.Text);
             

            }
        }

        private void btnChangePaper_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                string rtn = string.Empty;
                string code = gvw_op.GetFocusedRowCellValue("CODE").ToString();
                Dictionary<string, string> p = new Dictionary<string, string>();
                DataSet ds = DBConn._ExDataSet("SP_ITEMLIST");
                DataTable dt = ds.Tables[0];
                DataRow[] rows = dt.Select();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    p.Clear();
                    p.Add("P_TYPE", "N3");
                    p.Add("P_OP_CODE", code);
                    p.Add("P_ITEM_JUNG", rows[i]["ITEM_JUNG"].ToString());
                    if (rows[i]["ITEM_JUNG"].ToString() == "1")
                    {
                        p.Add("PNT_ONE", txtsubsource1.Text);
                        p.Add("PNT_TWO", txtsubsource2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "2")
                    {
                        p.Add("PNT_ONE", txtsub1.Text);
                        p.Add("PNT_TWO", txtsub2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsub3.Text);
                        //p.Add("P_PNT_NOW", txtsub4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "3")
                    {
                        p.Add("PNT_ONE", txtsave1.Text);
                        p.Add("PNT_TWO", txtsave2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsave3.Text);
                        //p.Add("P_PNT_NOW", txtsave4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "4")
                    {
                        p.Add("PNT_ONE", txtnomal1.Text);
                        p.Add("PNT_TWO", txtnomal2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtnomal3.Text);
                        //p.Add("P_PNT_NOW", txtnomal4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "5")
                    {
                        p.Add("PNT_ONE", txtair1.Text);
                        p.Add("PNT_TWO", txtair2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtair3.Text);
                        //p.Add("P_PNT_NOW", txtair4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "6")
                    {
                        p.Add("PNT_ONE", txtinnermove1.Text);
                        p.Add("PNT_TWO", txtinnermove2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtinnermove3.Text);
                        //p.Add("P_PNT_NOW", txtinnermove4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "7")
                    {
                        p.Add("PNT_ONE", txtproduct1.Text);
                        p.Add("PNT_TWO", txtproduct2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtproduct3.Text);
                        //p.Add("P_PNT_NOW", txtproduct4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "E")
                    {
                        p.Add("PNT_ONE", txtsource1.Text);
                        p.Add("PNT_TWO", txtsource2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtsource3.Text);
                        //p.Add("P_PNT_NOW", txtsource4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "G")
                    {
                        p.Add("PNT_ONE", txtect1.Text);
                        p.Add("PNT_TWO", txtect1.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtect1.Text);
                        //p.Add("P_PNT_NOW", txtect1.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "W")
                    {
                        p.Add("PNT_ONE", txtadd1.Text);
                        p.Add("PNT_TWO", txtadd2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtadd3.Text);
                        //p.Add("P_PNT_NOW", txtadd4.Text);
                    }
                    else if (rows[i]["ITEM_JUNG"].ToString() == "Z")
                    {
                        p.Add("PNT_ONE", txtbusanmul1.Text);
                        p.Add("PNT_TWO", txtbusanmul2.Text);
                        p.Add("P_PNT_ALL", txtsubsource3.Text);
                        p.Add("P_PNT_NOW", txtsubsource4.Text);
                        //p.Add("P_PNT_ALL", txtbusanmul3.Text);
                        //p.Add("P_PNT_NOW", txtbusanmul4.Text);
                    }

                    p.Add("P_MO_YN", checkval(chkMO)); //chkMO.Checked ? "Y" : "N");
                    p.Add("P_LPR_YN", checkval(chkLPR)); //chkLPR.Checked ? "Y" : "N");
                    //p.Add("P_CUT_YN", checkval(chkCUT)); //chkCUT.Checked ? "Y" : "N");
                    p.Add("P_RD_YN", checkval(chkRD)); //chkRD.Checked ? "Y" : "N");

                    p.Add("P_CRT_USER", clsUserInfo.User_id);
                    rtn = DBConn.ExecuteQuerySPR3("SP_SYSACTUAL_TB_WS01_0005_S", p);

                    if (rtn != "-1")
                    {
                        MsgBoxUtil.AlertError(rtn);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBoxUtil.AlertError(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
                gvw_op.SetFocusedRowCellValue("MO_YN", checkval(chkMO));
                gvw_op.SetFocusedRowCellValue("LPR_YN", checkval(chkLPR));
                gvw_op.SetFocusedRowCellValue("RD_YN", checkval(chkRD));
                gvw_op.SetFocusedRowCellValue("PNT_ALL", txtsubsource3.Text);
                txtsubsource4.Text = gvw_op.GetFocusedRowCellValue("PNT_ALL").ToString();
                gvw_op.SetFocusedRowCellValue("PNT_NOW", txtsubsource4.Text);
             

            }
        }
        
  
   
    }
}