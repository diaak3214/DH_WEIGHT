using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace DK_WEIGHT.AutoWeight.Report
{
    public partial class Allbaro_pnt : DevExpress.XtraReports.UI.XtraReport
    {
        public Allbaro_pnt(DataTable dt)
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            DataTable Rep_dt = dt.Copy();
            Rep_dt.TableName = "PRINT";
            ds.Tables.Add(Rep_dt);

            xrTableCell2.DataBindings.Add("Text", ds, ds.Tables[0].Columns["MANF_NUMS"].ColumnName);
            xrTableCell20.DataBindings.Add("Text", ds, ds.Tables[0].Columns["ITME_TP_NM"].ColumnName);
            xrTableCell21.DataBindings.Add("Text", ds, ds.Tables[0].Columns["WSTE_CODE"].ColumnName);
            xrTableCell23.DataBindings.Add("Text", ds, ds.Tables[0].Columns["GNTP"].ColumnName);
            xrTableCell26.DataBindings.Add("Text", ds, ds.Tables[0].Columns["GIVE_QUNT"].ColumnName);
            xrTableCell29.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRTM_WAYS_NM"].ColumnName);

            xrTableCell30.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRTM_WAYS"].ColumnName);

            xrTableCell10.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CRT_DATE"].ColumnName);

            xrTableCell15.DataBindings.Add("Text", ds, ds.Tables[0].Columns["MANB_TYPE"].ColumnName);
            xrTableCell6.DataBindings.Add("Text", ds, ds.Tables[0].Columns["GIVE_DATE"].ColumnName);
            xrTableCell17.DataBindings.Add("Text", ds, ds.Tables[0].Columns["VEHC_NUMS"].ColumnName);
            xrTableCell18.DataBindings.Add("Text", ds, ds.Tables[0].Columns["GIVE_CHRG_NAME"].ColumnName);


            xrTableCell31.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRAN_NM"].ColumnName);
            xrTableCell33.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRAN_BUSN_REGN"].ColumnName);
            xrTableCell37.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRAN_OFFC_ADDR"].ColumnName);
            xrTableCell35.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRAN_OFFC_TEL"].ColumnName);
            xrTableCell52.DataBindings.Add("Text", ds, ds.Tables[0].Columns["VEHC_NUMS"].ColumnName);

            xrTableCell38.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRTM_NM"].ColumnName);
            xrTableCell40.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRTM_BUSN_REGN"].ColumnName);

            xrTableCell38.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRTM_NM"].ColumnName);
            xrTableCell40.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRTM_BUSN_REGN"].ColumnName);
            xrTableCell42.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRTM_OFFC_ADDR"].ColumnName);
            xrTableCell43.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TRTM_OFFC_TEL"].ColumnName);
        }

    }
}
