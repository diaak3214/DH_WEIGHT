using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysInnerCar : OCT_WEIGHT.Manager.Common.FrmBase
    {
        public SysInnerCar()
        {
            InitializeComponent();
        }

        #region 조회

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            //try
            //{
            //    Dictionary<string, string> dict = new Dictionary<string, string>();
            //    dict.Add("V_START_DATE", START_DATE.Text.Trim());
            //    dict.Add("V_END_DATE", END_DATE.Text.Trim());
            //    dict.Add("V_VEHL_NO", txtVEHLNO.Text.Trim());
            //    //검색조건에 납품서번호 추가(2019-12-03 한민호)
            //    dict.Add("V_DLV_NO", txtDLV_NO.Text.Trim());
            //    DataSet ds = DBConn.ExecuteDataSet2("SP_TB_WS03_0021_R", dict);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        grdPerfo.DataSource = ds.Tables[0];
            //    }
            //    else
            //    {
            //        MsgBoxUtil.AlertInformation("조회된 자료가 없습니다.");
            //        grdPerfo.DataSource = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("차량 조회중 ERROR:" + ex.Message.ToString().Trim());
            //}
            //finally
            //{
            //    gvwPerfo.BestFitColumns();
            //}
        }

        #endregion

        #region 초기화

        private void BtnNew_Click(object sender, EventArgs e)
        {
            //오늘날짜(2019-12-02 한민호)
            START_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //START_DATE.Text = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            END_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtVEHLNO.Text = string.Empty;
            //검색조건에 납품서번호 추가(2019-12-03 한민호)
            txtDLV_NO.Text = string.Empty;
        }

        #endregion

        #region 엑셀

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            if (gvwPerfo.RowCount == 0) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvwPerfo.ExportToXls(desktop + "\\" + this.Text + ".xls");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                BtnSearch.PerformClick();
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region 로드

        private void SysInnerCar_Load(object sender, EventArgs e)
        {
            BtnNew.PerformClick();
        }

        #endregion

        #region 종료

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 엔터키 처리
        private void txtVEHLNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                BtnSearch.PerformClick();
            }
        }

        //검색조건에 납품서번호 추가(2019-12-03 한민호)
        private void txtDLV_NO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BtnSearch.PerformClick();
            }

        }
        #endregion
    }
}