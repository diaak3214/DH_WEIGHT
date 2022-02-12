using System;
using System.Drawing;

namespace DK_WEIGHT.AutoWeight.Report
{
    public partial class SubMatrial_Barcode : DevExpress.XtraReports.UI.XtraReport
    {
        public SubMatrial_Barcode(String WGHT_NO, String VEHL_NO, String CUST_CD, String LOAD_WEIGHT, String LOAD_DATE, String IMG_NAME)
        {
            InitializeComponent();

            xrBarCode1.Text = WGHT_NO;
            xrTableCell6.Text = VEHL_NO;
            xrTableCell7.Text = CUST_CD;
            xrTableCell8.Text = LOAD_WEIGHT + "Kg";
            xrTableCell9.Text = LOAD_DATE;

            if (IMG_NAME != "")
            {
                String Img_filepath = "\\\\10.10.94.197\\lpr_server\\F0001\\" + IMG_NAME.Substring(0, 4) + "\\" + IMG_NAME.Substring(4, 2) + "\\" + IMG_NAME.Substring(6, 2) + "\\Plate\\";
                xrPictureBox1.Image = Image.FromFile(Img_filepath + IMG_NAME + ".bmp");
            }
            //xrPictureBox1.Image = Image.FromFile(IMG_NAME + ".bmp");
        }         
    }
}
