using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraReports.UI;
using OCT_WEIGHT.RESULT_INFO.Report;
using System.IO.Ports;
using System.Threading;

namespace OCT_WEIGHT.RESULT_INFO
{
    public partial class frmMain : Form
    {
         
        // 개발개 접속 정보
        //private static String CS_MES = "Data Source=MES_TNS;User Id=m80apuser;Password=m80apuser";
        //private static String MES_TNS = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.151)(PORT = 2005))) (CONNECT_DATA = (SERVICE_NAME = DPMESA1)))";

        // 테스트계 
        //private static String CS_MES = "Data Source=MES_TNS;User Id=m80apuser;Password=m80apuser_tst";
        //private static String MES_TNS = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.153)(PORT = 2025))) (CONNECT_DATA = (SID = TSTMES1)))";

        private static String CS_MES = "Data Source=MES_TNS;User Id=m80apuser;Password=m80_wght";
        private static String MES_TNS = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.17)(PORT = 2006)) (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.19)(PORT = 2006))) (CONNECT_DATA = (SERVICE_NAME = mepp) (SERVER = DEDICATED)))";

        public ServiceAdapter _svc = null;
        public static DBHelper_ORACLE mdb_main;

        private SerialPort[] serialPort = new SerialPort[2];

        String Weight_Area;

        public frmMain()
        {
            InitializeComponent();
        }

        private void SetServiceURL()
        {
            string clientFolder = Application.StartupPath + @"\";
            XmlDocument clientDoc = new XmlDocument();
            clientDoc.Load(clientFolder + "UpdateFiles.xml");

            XmlElement clientDocRoot = clientDoc.DocumentElement;
            XmlNode nodeUpdateUrl = null;
            nodeUpdateUrl = clientDocRoot.SelectSingleNode("descendant::ServiceUrl");
            ServiceAdapter.Url = nodeUpdateUrl.Attributes["Url"].InnerText;

            XmlNode nodeWeight = null;
            nodeWeight = clientDocRoot.SelectSingleNode("descendant::Weight");
            Weight_Area = nodeWeight.Attributes["Url"].InnerText;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                dict.Add("P_LOAD_DATE", RFID_DATE.Text);
                dict.Add("P_WGHT_NO", textEdit1.Text);
                dict.Add("P_VEHL_NO", txtRFID_NO.Text);
                dict.Add("P_IF_NO", textEdit2.Text);


                DataSet ds = _svc.GetQuerySP("SP_TB_WS02_0002_R", dict);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd_rfid.DataSource = ds.Tables[0]; 
                }
                else
                {
                    grd_rfid.DataSource = null; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            if (gvw_rfid.DataRowCount == 0) return;

            DataRow dr = gvw_rfid.GetDataRow(gvw_rfid.FocusedRowHandle);

            

            try
            {
                if (dr["ITEM_DAE"].ToString() != "04")
                {
                    if (dr["ITEM_DAE"].ToString() == "02")
                    {
                        if (dr["IF_NM"].ToString() == "LOAD_ORD_NO")
                        {
                            MessageBox.Show(" 이적/출고(제품외) 차량만 삭제 됩니다. ");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show(" 이적/출고(제품외) 차량만 삭제 됩니다. ");
                        return;
                    }
                }

                DialogResult dlgResult = MessageBox.Show(dr["WGHT_NO"].ToString() + " 계량 정보를 삭제하시겠습니까?", "삭제확인", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dlgResult != DialogResult.OK) return;

                if (dr["DEL_REMARK"].ToString() == "")
                {
                    MessageBox.Show(" 삭제 사유를 입력 하세요");
                    return;

                }
                if (dr["DEL_USER"].ToString() == "")
                {
                    MessageBox.Show("삭제자 에 이름을 입력하세요 ");
                    return;

                }

                String Query2 = "INSERT INTO TB_WG02_0003 (  WGHT_NO , WEIGHT_STATE ,TRANS_CD ,EAI_IF_ID ,WORKS_CD ,OP_KIND ,FACTR_OP_CD "
                              + "                           ,LOAD_DATE ,LOAD_HOUR ,DOWN_DATE ,DOWN_HOUR ,LOAD_WEIGHT ,DOWN_WEIGHT ,REAL_WGHT "
                              + "                           ,ITEM_DAE ,ITEM_JUNG ,ITEM_SO ,VEHL_NO ,RFID_NO ,CARRI_CD ,CARRI_NM ,DRVR_NM ,DRVR_PHON "
                              + "                           ,PROD_WGHT ,INSPECT ,RETURN_FG ,SEND_YN ,CUST_CD ,CRT_DTM ,CRT_USER ,MOD_DTM ,MOD_USER "
                              + "                           ,DEL_YN ,DEL_USER ,DEL_DATE ,DEL_REMARK, RFID_SEQ ,IF_NO ,IF_NM , SEQ )"
                              + "                     SELECT WGHT_NO , WEIGHT_STATE ,TRANS_CD ,EAI_IF_ID ,WORKS_CD ,OP_KIND ,FACTR_OP_CD "
                              + "                           ,LOAD_DATE ,LOAD_HOUR ,DOWN_DATE ,DOWN_HOUR ,LOAD_WEIGHT ,DOWN_WEIGHT ,REAL_WGHT "
                              + "                           ,ITEM_DAE ,ITEM_JUNG ,ITEM_SO ,VEHL_NO ,RFID_NO ,CARRI_CD ,CARRI_NM ,DRVR_NM ,DRVR_PHON "
                              + "                           ,PROD_WGHT ,INSPECT ,RETURN_FG ,SEND_YN ,CUST_CD ,CRT_DTM ,CRT_USER ,MOD_DTM ,MOD_USER "
                              + "                           ,'Y' ,'" + dr["DEL_USER"].ToString() + "' ,SYSDATE , '" + dr["DEL_REMARK"].ToString() + "' , RFID_SEQ ,IF_NO ,IF_NM , M80APUSER.SQ_WG040002_01.NEXTVAL  "
                              + "                       FROM TB_WG02_0002 "
                              + "                      WHERE WGHT_NO = '" + dr["WGHT_NO"].ToString().Replace("-", "") + "' ";
                _svc.SetQuery(Query2);

                String Query = " UPDATE TB_WG02_0002 SET "
                            + "   DEL_YN = 'Y',"
                            + "   DEL_DATE = SYSDATE, "
                            + "   DEL_USER = NULL "
                            + " WHERE WGHT_NO = '" + dr["WGHT_NO"].ToString().Replace("-", "") + "' ";
                _svc.SetQuery(Query);

                Query = " UPDATE TB_WG02_0001 SET "
                           + "   WEIGHT_YN = 'Y', "
                           + "   DEL_YN = 'Y',"
                           + "   DEL_DATE = SYSDATE, "
                           + "   DEL_USER = NULL "
                           + " WHERE RFID_SEQ = '" + dr["RFID_SEQ"].ToString() + "' ";
                _svc.SetQuery(Query);
                 
                MessageBox.Show("삭제되었습니다");
                btnSearch_Click(btnSearch, new EventArgs());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

               
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //바탕화면 경로 + 화면명 
                grd_rfid.ExportToXls(desktop + "\\" + this.Text + ".xls");

                System.Diagnostics.Process.Start(desktop + "\\" + this.Text + ".xls");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSearch.PerformClick();
                this.Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            mdb_main = new DBHelper_ORACLE(CS_MES.Replace("MES_TNS", MES_TNS));
            mdb_main.DBConn();
            
            SetServiceURL();

            tabControl1.SelectedIndex = 0;

            RFID_DATE.DateTime = DateTime.Now;
            
            //SelectUpper(lWEIGHT_FG, "CODE", "CODE_NAME", "001");
        }

        private void SelectUpper(DevExpress.XtraEditors.LookUpEdit LookUp, string Value, string Display, string fg)
        {

            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_TYPE_CD", fg);
                DataSet ds = _svc.GetQuerySP("SP_COMBOX_R", dict);
                DataTable dt = ds.Tables[0];

                //룩업 바인딩은 Datatable 로 해야 바인딤 됨. 
                LookUp.Properties.DataSource = dt;

                LookUp.Properties.DisplayMember = Display;
                LookUp.Properties.ValueMember = Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        #region 계량표 출력 
        private void button1_Click(object sender, EventArgs e)
        {
            if (gvw_rfid.RowCount == 0)
            {
                MessageBox.Show("출력할 내용이 없습니다");
                return;
            }

            ServiceAdapter _svc = new ServiceAdapter();  
            
            try
            {
                DataRow dr = gvw_rfid.GetDataRow(gvw_rfid.FocusedRowHandle); 
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", "PNT");
                p.Add("P_GUBUN", "Y");
                p.Add("P_ITEM_JUNG", "");
                p.Add("P_RFID_SEQ", dr["RFID_SEQ"].ToString());
                p.Add("P_WGHT_NO", dr["WGHT_NO"].ToString());
                p.Add("P_ITEM_SO", dr["ITEM_SO"].ToString());
                DataSet ds = _svc.GetQuerySP("SP_MU_PRINT", p);

                if (Weight_Area == "P08")
                {
                    if (dr["ITEM_SO"].ToString() == "1051")
                    {
                        PrintCAR(ds.Tables[0]);
                        MessageBox.Show("계량표 출력 완료");  
                    }
                }
                else
                {
                    if (dr["ITEM_SO"].ToString() == "1051")
                    {
                        SubMatrial_KukS Print2 = new SubMatrial_KukS(ds.Tables[0]);
                        Print2.ShowPreview();
                        //Print2.Print();
                    }
                    else
                    {
                        SubMatrial_All Print2 = new SubMatrial_All(ds.Tables[0]);
                        Print2.ShowPreview();
                        //Print2.Print();
                    }
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
            }
              
        }  
        #endregion

        private void gvw_rfid_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            if (e.Info.IsRowIndicator)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        #region  확인증 
        private void button2_Click(object sender, EventArgs e)
        {
             if (gvw_rfid.RowCount == 0)
            {
                MessageBox.Show("출력할 내용이 없습니다");
                return;
            }

            ServiceAdapter _svc = new ServiceAdapter();

            try
            {

                DataRow dr = gvw_rfid.GetDataRow(gvw_rfid.FocusedRowHandle);
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", "PNT");
                p.Add("P_GUBUN", "Y");
                p.Add("P_ITEM_JUNG", "");
                p.Add("P_RFID_SEQ", dr["RFID_SEQ"].ToString());
                p.Add("P_WGHT_NO", dr["WGHT_NO"].ToString());
                p.Add("P_ITEM_SO", dr["ITEM_SO"].ToString());
                DataSet ds = _svc.GetQuerySP("SP_MU_PRINT", p);

                if (Weight_Area == "P08")
                {
                    if (dr["ITEM_SO"].ToString() == "1051")
                    {

                        String Query_print = " SELECT AR.LOAD_IMAGE, TO_CHAR(B.NUM,'0999') NUM "
                                                        + " FROM TB_WG04_0001 AR "
                                                        + " LEFT OUTER JOIN (SELECT ROWNUM AS NUM, WGHT_NO FROM TB_WG04_0001 "
                                                        + " WHERE SUBSTR(WGHT_NO,1,6) = TO_CHAR(SYSDATE,'YYMMDD')) B ON AR.WGHT_NO = B.WGHT_NO  "
                                                        + " WHERE AR.WGHT_NO = '" + ds.Tables[0].Rows[0]["WGHT_NO"].ToString().Replace("-","") +"' ";
                        DataSet ds_chk = _svc.GetQuery(Query_print);

                        PirntFoodPaper(ds.Tables[0].Rows[0]["WGHT_NO"].ToString(), ds_chk.Tables[0].Rows[0]["NUM"].ToString(), ds.Tables[0].Rows[0]["VEHL_NO"].ToString(),
                                      ds.Tables[0].Rows[0]["VENDER_NAME"].ToString() + "/" + ds.Tables[0].Rows[0]["REAL_VENDER_NAME"].ToString(), ds.Tables[0].Rows[0]["LOAD_WEIGHT"].ToString(), ds.Tables[0].Rows[0]["VEHL_NO"].ToString(), ds_chk.Tables[0].Rows[0]["LOAD_IMAGE"].ToString());

                        MessageBox.Show("확인증 출력 완료");  
                    }
                }
                else
                {
                    if (dr["ITEM_SO"].ToString() == "1051")
                    {
                        SubMatrial_Barcode Print_kuk = new SubMatrial_Barcode(ds.Tables[0]);
                        Print_kuk.ShowPreview();
                        //Print_kuk.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
            }
        }
        #endregion

        #region 국고계량대 계량표 출력
        private void PrintCAR(DataTable dt)
        {
            try
            {
                serialPort[1] = new SerialPort();
                serialPort[1].PortName = "COM3";

                serialPort[1].BaudRate = 9600;
                serialPort[1].Parity = Parity.None;
                serialPort[1].DataBits = 8;
                serialPort[1].StopBits = StopBits.One;
                serialPort[1].Encoding = Encoding.Default;
                serialPort[1].Open();

                String print_text = "";

                print_text = print_text + "^XA^LL1470^LH10,70^PRB^FS";
                print_text = print_text + "^BY2,3.0^FS";
                print_text = print_text + "^SEE:UHANGUL.DAT^FS";
                print_text = print_text + "^CW1,E:KFONT3.FNT^FS";

                print_text = print_text + "^FO200,20^A1N,50,60^FD계량표 (" + dt.Rows[0]["WEIGHT_STATE"].ToString() + ")^FS";

                print_text = print_text + "^FO70,200^A1N,50,40^FD품목   : " + dt.Rows[0]["ITEM_DAE_NM"].ToString() + "^FS";

                print_text = print_text + "^FO70,300^A1N,50,40^FD계량번호 : " + dt.Rows[0]["WGHT_NO"].ToString() + "^FS";
                print_text = print_text + "^FO70,360^A1N,50,40^FDRFID : " + dt.Rows[0]["RFID_NO"].ToString() + "^FS";
                print_text = print_text + "^FO70,410^A1N,50,40^FD중분류   : " + dt.Rows[0]["ITEM_JUNG_NM"].ToString() + "^FS";
                print_text = print_text + "^FO70,460^A1N,50,40^FD소분류   : " + dt.Rows[0]["ITEM_SO_NM"].ToString() + "^FS";
                print_text = print_text + "^FO70,510^A1N,50,40^FD업체명   : " + dt.Rows[0]["VENDER_NAME"].ToString() + "/" + dt.Rows[0]["REAL_VENDER_NAME"].ToString() + "^FS";

                print_text = print_text + "^FO70,560^A1N,50,40^FD1차일자 : " + dt.Rows[0]["LOAD_DATE"].ToString() + "^FS";
                print_text = print_text + "^FO70,610^A1N,50,40^FD1차중량 : " + dt.Rows[0]["LOAD_WEIGHT"].ToString() + " KG [" + dt.Rows[0]["LOAD_STATE"].ToString() + "] ^FS";
                print_text = print_text + "^FO70,660^A1N,50,40^FD2차일자 : " + dt.Rows[0]["DOWN_DATE"].ToString() + "^FS";
                print_text = print_text + "^FO70,710^A1N,50,40^FD2차중량 : " + dt.Rows[0]["DOWN_WEIGHT"].ToString() + " KG [" + dt.Rows[0]["DOWN_STATE"].ToString() + "] ^FS";

                print_text = print_text + "^FO70,760^A1N,50,40^FD실중량 : " + dt.Rows[0]["REAL_WGHT"].ToString() + "^FS";
                print_text = print_text + "^FO70,810^A1N,50,40^FD감량 : " + "^FS";
                print_text = print_text + "^FO120,860^A1N,60,80^FD동국제강 포항 제강소^FS";

                print_text = print_text + "^PQ1^FS";
                print_text = print_text + "^XZ ";

                serialPort[1].Write(print_text);
                Thread.Sleep(500);
                serialPort[1].Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("국내고철 확인출 출력 에러 : " + ex.Message.ToString());
            }
        }
        #endregion

        #region 국고계량대 확인증 출력
        private void PirntFoodPaper(String WGHT_NO, String CNT, String VEHL_NO, String CUST_CD, String LOAD_WEIGHT, String LOAD_DATE, String IMG_NAME)
        {
            try
            {
                serialPort[1] = new SerialPort();
                serialPort[1].PortName = "COM3";

                serialPort[1].BaudRate = 9600;
                serialPort[1].Parity = Parity.None;
                serialPort[1].DataBits = 8;
                serialPort[1].StopBits = StopBits.One;
                serialPort[1].Encoding = Encoding.Default;
                serialPort[1].Open();

                String print_text = "";

                print_text = print_text + "^XA^LL1470^LH10,70^PRB^FS";
                print_text = print_text + "^BY2,3.0^FS";
                print_text = print_text + "^SEE:UHANGUL.DAT^FS";
                print_text = print_text + "^CW1,E:KFONT3.FNT^FS";
                print_text = print_text + "^FO220,70^A1N,80,100^FD입 고 확 인 증^FS";

                //'계량번호 뒷자리만
                print_text = print_text + "^FO650,330^A0N,180,180^FD" + CNT + "^FS";

                //'차량번호
                print_text = print_text + "^FO80,650^A1N,50,50^FD차량번호 : " + VEHL_NO + "^FS";

                //'업체명
                print_text = print_text + "^FO80,750^A1N,50,50^FD업 체 명 : " + CUST_CD + "^FS";
                //'상차계량
                print_text = print_text + "^FO80,850^A1N,50,50^FD상차계량 : " + LOAD_WEIGHT + "Kg^FS";
                //'계량시간
                print_text = print_text + "^FO80,950^A1N,50,50^FD계량시간 : " + LOAD_DATE + "^FS";

                print_text = print_text + "^FO150,1100^A1N,60,80^FD동국제강 포항 제강소^FS";

                //'바코드 코드128
                print_text = print_text + "^FO220,1250^BY4^BCN,100,Y,N,Y^FD" + WGHT_NO + "^FS";

                //'차량이미지 BarCode_Data(6)
                //'BarCode_Data(6) = "20081028-214415-000016"
                //If Len(BarCode_Data(6)) > 8 Then
                //    Local_image = Mid(BarCode_Data(6), 1, 4) & "\" & Mid(BarCode_Data(6), 5, 2) & "\" & Mid(BarCode_Data(6), 7, 2) & "\Plate\"
                //    'MsgBox Barcode_Image_Path & Local_image & BarCode_Data(6) & ".bmp"
                //    If dir(Barcode_Image_Path & Local_image & BarCode_Data(6) & ".bmp") <> "" Then

                //        'ret = zebraOCX1.ZPL_WriteComm_Image(150, 300, "20081028-214415-000016.bmp", "img01", 2, 2)
                //        ret = frmMainWeight.zebraOCX1.ZPL_WriteComm_Image(150, 150, Barcode_Image_Path & Local_image & BarCode_Data(6) & ".bmp", "img01", 2, 2)
                //    End If
                //End If

                String Img_filepath = "\\\\10.10.94.197\\lpr_server\\F0001\\" + IMG_NAME.Substring(0, 4) + "\\" + IMG_NAME.Substring(4, 2) + "\\" + IMG_NAME.Substring(6, 2) + "\\Plate\\";
                Img_filepath = Img_filepath + IMG_NAME + ".bmp";

                //logger.Info(" 이미지 출력 : " + Img_filepath);

                Bitmap image1 = new Bitmap(Img_filepath, true);
                String str = ConvertImageToCode(image1);

                String t = ((image1.Size.Width / 8 + ((image1.Size.Width % 8 == 0) ? 0 : 1)) * image1.Size.Height).ToString();
                String w = (image1.Size.Width / 8 + ((image1.Size.Width % 8 == 0) ? 0 : 1)).ToString();
                print_text = print_text + "^FO150,300";
                print_text = print_text + string.Format("~DGR:imgName.GRF,{0},{1},{2}", t, w, str);
                print_text = print_text + "^XGR:imgName.GRF,2,2^FS";

                print_text = print_text + "^PQ1^FS";
                print_text = print_text + "^XZ";
                print_text = print_text + "   ";

                serialPort[1].Write(print_text);
                Thread.Sleep(500);

                serialPort[1].Write("^XA^IDR:imgName*.GRF^FS^XZ");
                serialPort[1].Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("국내고철 확인출 출력 에러 : " + ex.Message.ToString());
            }
        }

        public static string ConvertImageToCode(Bitmap img)
        {
            var sb = new StringBuilder();
            long clr = 0, n = 0;
            int b = 0;
            for (int i = 0; i < img.Size.Height; i++)
            {
                for (int j = 0; j < img.Size.Width; j++)
                {
                    b = b * 2;
                    clr = img.GetPixel(j, i).ToArgb();
                    string s = clr.ToString("X");

                    if (s.Substring(s.Length - 6, 6).CompareTo("BBBBBB") < 0)
                    {
                        b++;
                    }
                    n++;
                    if (j == (img.Size.Width - 1))
                    {
                        if (n < 8)
                        {
                            b = b * (2 ^ (8 - (int)n));

                            sb.Append(b.ToString("X").PadLeft(2, '0'));
                            b = 0;
                            n = 0;
                        }
                    }
                    if (n >= 8)
                    {
                        sb.Append(b.ToString("X").PadLeft(2, '0'));
                        b = 0;
                        n = 0;
                    }
                }
                sb.Append(System.Environment.NewLine);
            }
            return sb.ToString();
        }
        #endregion

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            mdb_main.DBClose();  
        }
    }
}
