using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Common.Popup
{
    public partial class VehlPopup : OCT_WEIGHT.Manager.Common.FrmBase
    {
        //public ServiceAdapter _svc = null;

        public string vehl_no = string.Empty;
        //팝업 선택 시 운전자명, 운전자연락처, 운송사 던져주게 수정(2019-10-05 한민호)
        public string drvrnm = string.Empty;
        public string drvrhp = string.Empty;
        public string cust = string.Empty;

        public VehlPopup()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_VEHL_NO", txVEHLNO.Text.Trim());

                //20190503 임시수정 Kimsw
                DataSet ds = DBConn.ExecuteDataSet2("SP_VEHLLIST_R", dict);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd_carsearch.DataSource = ds.Tables[0];
                }
                else
                {
                    MsgBoxUtil.AlertInformation("조회된 자료가 없습니다.");
                    grd_carsearch.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("차량 조회중 ERROR:" + ex.Message.ToString().Trim());
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            txVEHLNO.Text = string.Empty;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void octoButton1_Click(object sender, EventArgs e)
        {
            DataRow dr = gvw_carsearch.GetDataRow(gvw_carsearch.FocusedRowHandle);
            vehl_no = dr["VEHL_NO"].ToString();
            //팝업 선택 시 운전자명, 운전자연락처, 운송사 던져주게 수정(2019-10-05 한민호)
            drvrnm = dr["DRVR_NM"].ToString();
            drvrhp = dr["DRVR_PHON"].ToString();
            cust = dr["CARRI_CD"].ToString();
            DialogResult = DialogResult.OK;
        }

        private void VehlPopup_Load(object sender, EventArgs e)
        {
            this.Text = "차량 조회 팝업";
            BtnSearch.PerformClick();
        }

        
        //더블클릭 시 선택 됨(2019-10-05 한민호) 
        private void gvw_carsearch_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gvw_carsearch.GetDataRow(gvw_carsearch.FocusedRowHandle);
            vehl_no = dr["VEHL_NO"].ToString();
            //팝업 선택 시 운전자명, 운전자연락처, 운송사 던져주게 수정(2019-10-05 한민호)
            drvrnm = dr["DRVR_NM"].ToString();
            drvrhp = dr["DRVR_PHON"].ToString();
            cust = dr["CARRI_CD"].ToString();
            DialogResult = DialogResult.OK;

        }
    }
}
