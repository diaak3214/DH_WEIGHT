using System.Data;

namespace OCT_WEIGHT.AutoWeight.Report
{
    public partial class SubMatrial_NEW : DevExpress.XtraReports.UI.XtraReport
    {
        public SubMatrial_NEW(DataTable dt)
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            DataTable Rep_dt = dt.Copy();
            Rep_dt.TableName =  "PRINT";
            ds.Tables.Add(Rep_dt);

            xrTableCell29.Text = ds.Tables[0].Rows[0]["WEIGHT_STATE"].ToString();
            xrTableCell24.Text = ds.Tables[0].Rows[0]["ITEM_DAE_NM"].ToString();

            xrTableCell2.DataBindings.Add("Text", ds, ds.Tables[0].Columns["WGHT_NO"].ColumnName);
            xrTableCell4.DataBindings.Add("Text", ds, ds.Tables[0].Columns["RFID_NO"].ColumnName);
            xrTableCell6.DataBindings.Add("Text", ds, ds.Tables[0].Columns["ITEM_JUNG_NM"].ColumnName);
            xrTableCell8.DataBindings.Add("Text", ds, ds.Tables[0].Columns["ITEM_SO_NM"].ColumnName);
            xrTableCell10.DataBindings.Add("Text", ds, ds.Tables[0].Columns["VEHL_NO"].ColumnName);
            xrTableCell12.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CUST_CD"].ColumnName);
            xrTableCell14.DataBindings.Add("Text", ds, ds.Tables[0].Columns["LOAD_DATE"].ColumnName);
            xrTableCell16.DataBindings.Add("Text", ds, ds.Tables[0].Columns["LOAD_WEIGHT"].ColumnName);

            xrTableCell36.DataBindings.Add("Text", ds,   ds.Tables[0].Columns["LOAD_STATE"].ColumnName  );
            xrTableCell18.DataBindings.Add("Text", ds, ds.Tables[0].Columns["DOWN_DATE"].ColumnName);
            xrTableCell20.DataBindings.Add("Text", ds, ds.Tables[0].Columns["DOWN_WEIGHT"].ColumnName);
            xrTableCell38.DataBindings.Add("Text", ds,  ds.Tables[0].Columns["DOWN_STATE"].ColumnName );
            xrTableCell22.DataBindings.Add("Text", ds, ds.Tables[0].Columns["REAL_WGHT"].ColumnName);
            
            if (ds.Tables[0].Rows[0]["ITEM_DAE"].ToString() == "04")
            {
                xrTableCell23.Text = "상차지";
                xrTableCell26.Text = "하자치";
                xrTableCell25.DataBindings.Add("Text", ds, ds.Tables[0].Columns["UP_AREA"].ColumnName);
                xrTableCell27.DataBindings.Add("Text", ds, ds.Tables[0].Columns["DOWN_AREA"].ColumnName);
            }
            else
            {
                if (ds.Tables[0].Rows[0]["ITEM_JUNG"].ToString() == "108")
                {
                    xrTableCell23.Text = "발주번호";
                    xrTableCell26.Text = "행     번";
                    xrTableCell25.DataBindings.Add("Text", ds, ds.Tables[0].Columns["IF_NO"].ColumnName);
                    xrTableCell27.DataBindings.Add("Text", ds, ds.Tables[0].Columns["IF_NO2"].ColumnName);
                }

                if (ds.Tables[0].Rows[0]["ITEM_JUNG"].ToString() == "201")
                {
                    xrTableCell23.Text = "상차지시번호";
                    xrTableCell26.Text = "대기대수";
                    xrTableCell25.DataBindings.Add("Text", ds, ds.Tables[0].Columns["IF_NO"].ColumnName);
                    xrTableCell27.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CNT"].ColumnName);
                }
            }
        }         
    }
}
