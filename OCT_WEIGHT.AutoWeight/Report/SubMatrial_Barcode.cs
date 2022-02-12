using System;
using System.Drawing;

namespace OCT_WEIGHT.AutoWeight.Report
{
    public partial class SubMatrial_Barcode : DevExpress.XtraReports.UI.XtraReport
    {
        public SubMatrial_Barcode(String WGHT_NO, String VEHL_NO, String CUST_CD, String LOAD_WEIGHT, String LOAD_DATE, String IMG_NAME)
        {
            InitializeComponent();

            xrBarCode1.Text = WGHT_NO;
            xrTableCell6.Text = VEHL_NO;
            xrTableCell7.Text = CUST_CD;
            xrTableCell8.Text = LOAD_WEIGHT;
            xrTableCell9.Text = LOAD_DATE;

            xrPictureBox1.Image = Image.FromFile("\\\\10.10.94.197\\lpr_server\\F0001\\2016\\08\\23\\Plate\\" + IMG_NAME + ".bmp");
            //xrPictureBox1.Image = Image.FromFile(IMG_NAME + ".bmp");
        }         
    }
}
