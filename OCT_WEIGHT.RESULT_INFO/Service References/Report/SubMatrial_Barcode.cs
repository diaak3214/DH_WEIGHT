using System.Data;

namespace OCT_WEIGHT.RESULT_INFO.Report
{
    public partial class SubMatrial_Barcode : DevExpress.XtraReports.UI.XtraReport
    {
        public SubMatrial_Barcode(DataTable dt)
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            DataTable Rep_dt = dt.Copy();
            Rep_dt.TableName =  "PRINT";
            ds.Tables.Add(Rep_dt);

            xrBarCode1.Text = ds.Tables[0].Rows[0]["WGHT_NO"].ToString();
            xrTableCell6.DataBindings.Add("Text", ds, ds.Tables[0].Columns["VEHL_NO"].ColumnName);
            xrTableCell7.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CUST_CD"].ColumnName);
            xrTableCell8.DataBindings.Add("Text", ds, ds.Tables[0].Columns["LOAD_WEIGHT"].ColumnName);
            xrTableCell9.DataBindings.Add("Text", ds, ds.Tables[0].Columns["LOAD_DATE"].ColumnName);
        }         
    }
}
