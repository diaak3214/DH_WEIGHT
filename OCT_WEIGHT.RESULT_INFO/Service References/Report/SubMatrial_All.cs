using System.Data;

namespace OCT_WEIGHT.RESULT_INFO.Report
{
    public partial class SubMatrial_All : DevExpress.XtraReports.UI.XtraReport
    {
        public SubMatrial_All(DataTable dt)
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

        }         
    }
}
