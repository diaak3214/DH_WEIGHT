using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace OCT_WEIGHT.Manager.Common.Popup
{
    public partial class VehlimgPopup : Form
    {
        public string IN_IMG_PATH = "";
        public string OUT_IMG_PATH = "";
        //차량번호, 물류구분, 품명구분 추가(2020-02-18 한민호)
        public string CAR_NO = "";
        public string ITEM_TYPE_NM = "";
        public string ITEM_NM = "";

        string ftp_user = "ftpuser";
        string ftp_pass = "eogks@123";

        public VehlimgPopup()
        {
            InitializeComponent();
            //if(IN_IMG_PATH != "")
            //    PicIN_IMG_PATH.Image = ByteToImage(GetImgByte(IN_IMG_PATH));
            //if (OUT_IMG_PATH != "")
            //    PicOUT_IMG_PATH.Image = ByteToImage(GetImgByte(OUT_IMG_PATH));
        }

        public void getImg()
        {
            if (IN_IMG_PATH != "")
                PicIN_IMG_PATH.Image = ByteToImage(GetImgByte(IN_IMG_PATH));
            if (OUT_IMG_PATH != "")
                PicOUT_IMG_PATH.Image = ByteToImage(GetImgByte(OUT_IMG_PATH));

            //차량번호, 물류구분, 품명구분 추가(2020-02-18 한민호)
            txtCAR_NO.Text = CAR_NO;
            txtDIS_CLASS.Text = ITEM_TYPE_NM;
            txtITEM_NM.Text = ITEM_NM;

        }

        public byte[] GetImgByte(string ftpFilePath)
        {
            WebClient ftpClient = new WebClient();
            ftpClient.Credentials = new NetworkCredential(ftp_user, ftp_pass);

            byte[] imageByte = ftpClient.DownloadData(ftpFilePath);
            return imageByte;
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 기능키
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & ~(Keys.Shift | Keys.Control);

            switch (key)
            {
                case Keys.Escape:
                    btnClose_Click(btnClose, new EventArgs());  //닫기
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
