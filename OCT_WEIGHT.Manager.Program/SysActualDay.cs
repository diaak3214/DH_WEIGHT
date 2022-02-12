using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.util;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysActualDay : OCT_WEIGHT.Manager.Common.FrmBase
    {
        private DataTable dt_pnt;

        public SysActualDay()
        {
            InitializeComponent();
        }

        #region Load

        private void SysActualDay_Load(object sender, EventArgs e)
        {
            this.Text = "일일계량실적조회";
            START_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            END_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        #endregion

        #region  Data 조회

        private void octoButton3_Click(object sender, EventArgs e)
        {
            ////if (_svc == null) _svc = new ServiceAdapter();

            //try
            //{
            //    //계량종류 공통코드값 가져오기
            //    Dictionary<string, string> dict = new Dictionary<string, string>();
            //    dict.Add("P_TYPE_CD", "WS_006");

            //    DataSet ds = DBConn.ExecuteDataSet2("SP_COMBOX_R", dict);
            //    DataTable dt = ds.Tables[0];

            //    List<string> strDetailIDList = new List<string>();

            //    foreach (DataRow row in ds.Tables[0].Rows)
            //    {
            //        strDetailIDList.Add(row["CODE"].ToString());
            //    }

            //    string[] strDetailID = new string[] { };
            //    strDetailID = strDetailIDList.ToArray();
            //    for (int i = 0; i < strDetailID.Length; i++)
            //    {
            //        dict.Clear();
            //        dict.Add("P_START_DATE", START_DATE.Text);
            //        dict.Add("P_END_DATE", END_DATE.Text);
            //        dict.Add("P_CODE", strDetailID[i]);

            //        DataSet ds2 = DBConn.ExecuteDataSet2("SP_SysActualDay_R", dict);
            //        dt_pnt = ds2.Tables[0];
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            if (strDetailID[i] == "1") // 부재료
            //            {
            //                grdPartfee.DataSource = ds2.Tables[0];
            //            }
            //            else if (strDetailID[i] == "2") // 보조재료
            //            {
            //                grdSubItem.DataSource = ds2.Tables[0];
            //            }
            //            else if (strDetailID[i] == "3") // 저장품
            //            {
            //                grdSmaterial.DataSource = ds2.Tables[0];
            //            }
            //            else if (strDetailID[i] == "4") // 일반자재
            //            {
            //                grdNmaterial.DataSource = ds2.Tables[0];
            //            }
            //            else if (strDetailID[i] == "5") // 산소
            //            {
            //                grdOxygen.DataSource = ds2.Tables[0];
            //            }
            //            else if (strDetailID[i] == "6") // 사내이송
            //            {
            //                grdInnerMove.DataSource = ds2.Tables[0];
            //            }
            //            else if (strDetailID[i] == "7") // 제품
            //            {
            //                grdProduct.DataSource = ds2.Tables[0];
            //            }
            //            else if (strDetailID[i] == "E") // 원재료
            //            {
            //                grdRsource.DataSource = ds2.Tables[0];
            //            }
            //            else if (strDetailID[i] == "G") // 기타
            //            {
            //                grdOther.DataSource = ds2.Tables[0];
            //            }
            //            else if (strDetailID[i] == "W") // 영업외수익
            //            {
            //                grdProfit.DataSource = ds2.Tables[0];
            //            }
            //            else if (strDetailID[i] == "Z") // 부산물
            //            {
            //                grdByProduct.DataSource = ds2.Tables[0];
            //            }
            //        }
            //        else
            //        {
            //            MsgBoxUtil.AlertInformation("조회된 자료가 없습니다.");
            //            grdSubItem.DataSource = null;
            //            grdPartfee.DataSource = null;

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBoxUtil.AlertInformation(ex.Message);
            //}
            //finally
            //{
            //    //gvwSubItem.BestFitColumns();
            //    //gvwPartfee.BestFitColumns();
            //    //gvwByProduct.BestFitColumns();
            //    //gvwInnerMove.BestFitColumns();
            //    //gvwNmaterial.BestFitColumns();
            //    //gvwOther.BestFitColumns();
            //    //gvwOxygen.BestFitColumns();
            //    //gvwProduct.BestFitColumns();
            //    //gvwProfit.BestFitColumns();
            //    //gvwRsource.BestFitColumns();
            //    //gvwSmaterial.BestFitColumns();
            //    //gvwSubItem.BestFitColumns();
            //}
        }

        #endregion

        #region  종료

        private void octoButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Excel

        private void octoButton1_Click(object sender, EventArgs e)
        {
            if (gvwSubItem.RowCount == 0) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                gvwSubItem.ExportToXls(desktop + "\\" + this.Text + ".xls");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSearch1.PerformClick();
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Report

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Report.Report Print_etc = new Report.Report(dt_pnt);
            Print_etc.start_date = START_DATE.Text;
            Print_etc.end_date = END_DATE.Text;
            Print_etc.GridControl = grdPartfee;
            Print_etc.company = "창원";
            Print_etc.load();
            //Print_etc.ShowPreview();
            //Print_etc.Print();
            ReportPrintTool printTool = new ReportPrintTool(Print_etc);
            printTool.ShowPreviewDialog();
        }

        #endregion

        #region Edit Value Changed 날짜 동기= 해제상태

        //private void START_DATE_EditValueChanged(object sender, EventArgs e)
        //{
        //    END_DATE.DateTime = START_DATE.DateTime;
        //}

        //private void END_DATE_EditValueChanged(object sender, EventArgs e)
        //{
        //    START_DATE.DateTime = END_DATE.DateTime;
        //}

        #endregion

        #region New

        private void btnNew_Click(object sender, EventArgs e)
        {
            START_DATE.DateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            END_DATE.DateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        }

        #endregion

        private void gvwOxygen_CustomColumnDisplayText(object sender,
            DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column.FieldName == "LOAD_WEIGHT" | e.Column.FieldName == "DOWN_WEIGHT" | //1차중량 2차중량
            //    e.Column.FieldName == "REAL_WGHT") //실중량  이론중량
            //{
            //    if (e.DisplayText != "" & e.DisplayText != "0" & e.DisplayText.ToUpper() != "KG")
            //    {
            //        e.DisplayText = CustomColumnDisplayTextChange.txtsetting(e.DisplayText);
            //    }
            //}
        }
    }
}