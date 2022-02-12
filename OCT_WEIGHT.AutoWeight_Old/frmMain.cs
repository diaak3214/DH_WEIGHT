using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using System.IO.Ports;

//Sound 들리는 프로그램
using System.Media;

using System.Xml;

//출력물 이용
using DK_WEIGHT.AutoWeight.Report;
using System.Diagnostics;
using DevExpress.XtraReports.UI;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using Automation.BDaq;

namespace DK_WEIGHT.AutoWeight
{
    public partial class frmMain : Form
    {
        private readonly string EXE_FILE_NAME = "DK_WEIGHT.AutoWeight";

        protected static readonly ILog logger =
                 LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // 식당서버 DB 접속정보
        public static DBHelper_SQL mdb = new DBHelper_SQL("Database=dkmeal;Data Source=10.10.90.201,4117;User Id=door5;Password=door%^&*");

        // 개발개 접속 정보
        //private static String CS_MES = "Data Source=MES_TNS;User Id=m80apuser;Password=m80apuser";
        //private static String MES_TNS = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.151)(PORT = 2005))) (CONNECT_DATA = (SERVICE_NAME = DPMESA1)))";
        
        // 테스트계 
        //private static String CS_MES = "Data Source=MES_TNS;User Id=m80apuser;Password=m80apuser_tst";
        //private static String MES_TNS = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.153)(PORT = 2025))) (CONNECT_DATA = (SID = TSTMES1)))";

        // 가동계 
        private static String CS_MES = "Data Source=MES_TNS;User Id=m80apuser;Password=m80_wght";
        private static String MES_TNS = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.17)(PORT = 2006)) (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.19)(PORT = 2006))) (CONNECT_DATA = (SERVICE_NAME = mepp) (SERVER = DEDICATED)))";

        public static DBHelper_ORACLE mdb_main;

        #region Sound Voice
        
        SoundPlayer player01 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._01); // 1차 계량이 완료 되었습니다
        SoundPlayer player02 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._02); // 2차 계량이 완료 되었습니다
        SoundPlayer player03 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._03); // 2차 계량 정보가 없습니다        
        SoundPlayer player05 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._05); // 계량이 완료되었습니다
        SoundPlayer player08 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._08); // 입출차 제한 차량입니다 경비실에 확인 바랍니다
        SoundPlayer player11 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._11); // 프로그램 에러 발생. 관리자에게 문의바랍니다        
        SoundPlayer player13 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._13); // RFID카드를 대주세요.                
        SoundPlayer player20 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._20); // 띵동소리 

        //신규 추가 16-06-16
        SoundPlayer player30 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._30); // 오늘은 계량을 진행하지 않습니다. 천천히 출발하세요 
        SoundPlayer player31 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._31);  // RFID 카드 정보가 없습니다.

        SoundPlayer player32 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._32);  // 잠시만 기다려 주세요 
        SoundPlayer player33 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._33);  // 천천히 출발해 주세요 
        SoundPlayer player34 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._34);  // 공차 계량이 완료 되었습니다. 천천히 출발해 주세요 
        SoundPlayer player35 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._35);  // 계량을 진행할수 없습니다. 출하실에 연락 바랍니다. 

        SoundPlayer player60 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._60);  // 부두계량대, 안전모 쓰세요  
        SoundPlayer player61 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._61);  // 부두계량대, 깜박이를 켜주세요  

        SoundPlayer player62 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._62);  // 상차지시가 없습니다.
        SoundPlayer player63 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._63);  // 배차지시 오류 

        SoundPlayer player64 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._64);  // 계량표 뽑고 출발 
        SoundPlayer player65 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._65);  // 계량표 뽑고 출발 

        SoundPlayer player66 = new SoundPlayer(DK_WEIGHT.AutoWeight.Properties.Resources._66);  // 고철검수가 완료되지 않아 계량을진행할수 없습니다
        #endregion

        #region 하드웨어 변수        

        private Int32 iFormHandle = 0;

        private System.Threading.Thread ThreadClient;
        private Socket SocketClient;
        private IPAddress ServerIPAddress;
        private int PORT;
        private IPEndPoint ServerIPEndPoint;

        private System.Threading.Thread ThreadClient1;
        private Socket SocketClient1;

        // 국고용 천광판
        private Socket SocketLED;

        private Thread ServerThread = null;
        private Socket ServerSocket = null;
        private bool bSocketEnd = false;
        private Socket ClientSocket = null;
        private byte[] buff = new byte[4096];
        private string strReceiveMsg = "";

        // RFID 사용 변수
        private string ReceveData1 = "";

        private string AutoNum = "";

        byte STX1 = (byte)0x02;
        byte ETX1 = (byte)0x03;

        const char STX = (char)2;
        const char ETX = (char)3;
        const char CR = (char)13;

        // 시리얼포트
        private SerialPort[] serialPort = new SerialPort[2];

        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate1;
        #endregion

        String car_no;
        String item_dae;
        String item_jung;
        String item_so;        
        String rfid_no;
        String wght_no;
        String if_no;
        String pmax_wght;
        String pmin_wght;

        String rfid_seq;
        //String MTRL_NO = "";

        String PAN1 = "";
        String PAN2 = "";
        String BON1 = "";
        String BON2 = "";
        String BEM1 = "";
        String BEM2 = "";

        String AREA; // 지역 구분 인천 : C, 당진 : D, 포항 : P, 신평 : S
        String Weight_Area; // 계량대 코드 
        Boolean weight_fg = false ; //1차, 2차 계량 저장 여부
        Boolean fix_fg = false; // 공차 계량 저장 여부 
        Boolean play60_chk = false;
        Boolean bClose = false;

        //Int32 err_cnt = 0;
        Int32 Iny_cnt = 0;
        String Gubun = "N";

        String RFID_CODE = "0";

        //int pnt_per = 100; // 프린트 잔량 확인 

        string limit_yn;    // 입출차 제한 여부
         
        public ServiceAdapter _svc = null;

        Int32 DeviceHandle;

        String Prev_rfid = "";
        String HW_COMPORT = "";
        String HW_RFID = "";
        String HW_LPR = "";
        String HW_RFID_PORT = "";
        //Boolean BARRICATE_ON = false;

        //public int tmp_indicator = 0;
        //public int indicator_status = 0;
        public static Boolean rfid_ready = false;

        Boolean Sencer_Action = false;
        Boolean CardRead = false;
        Boolean bStopper_Action = false;        // 차단기 동작 가능여부 상태
        Boolean bStopper_Status = false;        // 차단기 상태 : true(닫음 = 진입), false(열림 = 통과)

        Boolean Billet_fg = false; //빌렛 계량구분 

        delegate void SetTextCallback(string text);

        //public int[] indicator_arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }; //5초
        public int[] indicator_arr = { 1, 2, 3, 4, 5, 6 }; // 3초
        public int indicator_max = 6; // 3초
        public int indicator_cnt = 0;
        public int indicator_stopper = 0;

        Win32API.COPYDATASTRUCT wmcopydataRFID = new Win32API.COPYDATASTRUCT();

        public frmMain()
        {
            InitializeComponent();
        }

        #region getTable
        protected DataTable getTable(string spName, Dictionary<string, string> p)
        {
            if (_svc == null) _svc = new ServiceAdapter();

            DataSet ds = _svc.GetQuerySP(spName, p);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0]; 
            }

            return dt;
        }
        #endregion

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(    // GetIniValue 를 위해
            String section,
            String key,
            String def,
            StringBuilder retVal,
            int size,
            String filePath);


        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(  // SetIniValue를 위해
            String section,
            String key,
            String val,
            String filePath);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        public static void SetSoundVolume(int volume)
        {
            try
            {
                int newVolume = ((ushort.MaxValue / 10) * volume);
                uint newVolumeAllChannels = (((uint)newVolume & 0x0000ffff) | ((uint)newVolume << 16));
                waveOutSetVolume(IntPtr.Zero, newVolumeAllChannels);
            }
            catch (Exception) { }
        }

        private void SetServiceURL()
        {
            string clientFolder = Application.StartupPath + @"\";
            XmlDocument clientDoc = new XmlDocument();
            clientDoc.Load(clientFolder+"UpdateFiles.xml"); 

            XmlElement clientDocRoot = clientDoc.DocumentElement;
            XmlNode nodeUpdateUrl = null;
            nodeUpdateUrl = clientDocRoot.SelectSingleNode("descendant::ServiceUrl");
            //ServiceAdapter.Url = nodeUpdateUrl.Attributes["Url"].InnerText;

            //계량대 코드 --------------------------------------------------------------- 
            // B01 : 부산 계량대 
            //---------------------------------------------------------------------------
            XmlNode nodeArea = null;
            nodeArea = clientDocRoot.SelectSingleNode("descendant::Area");
            AREA = nodeArea.Attributes["Url"].InnerText;
            //---------------------------------------------------------------------------
            
            //계량 장소 --------------------------------------------------------------- 
            // B : 신평 고정값 
            //---------------------------------------------------------------------------
            XmlNode nodeWeight = null;
            nodeWeight = clientDocRoot.SelectSingleNode("descendant::Weight");
            Weight_Area = nodeWeight.Attributes["Url"].InnerText;
            //---------------------------------------------------------------------------

            //인디게이터 코드------------------------------------------------------------  
            // 부산  - COM1 
            //---------------------------------------------------------------------------
            XmlNode nodeCom = null;
            nodeCom = clientDocRoot.SelectSingleNode("descendant::Comport");
            HW_COMPORT = nodeCom.Attributes["value"].InnerText;
            //---------------------------------------------------------------------------

            //RFID 아이피---------------------------------------------------------------- 
            // B01 일 경우 - 10.10.50.197
            //--------------------------------------------------------------------------
            XmlNode nodeRfid = null;
            nodeRfid = clientDocRoot.SelectSingleNode("descendant::Rfid");
            HW_RFID = nodeRfid.Attributes["value"].InnerText;
            //---------------------------------------------------------------------------

            //RFID 아이피---------------------------------------------------------------- 
            // B01 일 경우 - 10.10.50.197
            //--------------------------------------------------------------------------
            XmlNode nodeRfid_port = null;
            nodeRfid_port = clientDocRoot.SelectSingleNode("descendant::Rfid_PORT");
            HW_RFID_PORT = nodeRfid_port.Attributes["value"].InnerText;
            //--------------------------------------------------------------------------                    

            //LPR 아이피---------------------------------------------------------------- 
            // B01 일 경우 - 10.10.50.197
            //--------------------------------------------------------------------------
            XmlNode nodeLPR = null;
            nodeLPR = clientDocRoot.SelectSingleNode("descendant::LPR");
            HW_LPR = nodeRfid.Attributes["value"].InnerText;
            //---------------------------------------------------------------------------

            // 세인율 계산 값 가져오기
            XmlNode nodePAN1 = null;
            nodePAN1 = clientDocRoot.SelectSingleNode("descendant::PAN1");
            PAN1 = nodePAN1.Attributes["value"].InnerText;

            XmlNode nodePAN2 = null;
            nodePAN2 = clientDocRoot.SelectSingleNode("descendant::PAN2");
            PAN2 = nodePAN2.Attributes["value"].InnerText;

            XmlNode nodeBON1 = null;
            nodeBON1 = clientDocRoot.SelectSingleNode("descendant::BON1");
            BON1 = nodeBON1.Attributes["value"].InnerText;

            XmlNode nodeBON2 = null;
            nodeBON2 = clientDocRoot.SelectSingleNode("descendant::BON2");
            BON2 = nodeBON2.Attributes["value"].InnerText;

            XmlNode nodeBEM1 = null;
            nodeBEM1 = clientDocRoot.SelectSingleNode("descendant::BEM1");
            BEM1 = nodeBEM1.Attributes["value"].InnerText;

            XmlNode nodeBEM2 = null;
            nodeBEM2 = clientDocRoot.SelectSingleNode("descendant::BEM2");
            BEM2 = nodeBEM2.Attributes["value"].InnerText;

            //계량대 명 표시 하기
            labelControl21.Text = DB_Process.weight_name(Weight_Area);

            spinEdit1.Value = Convert.ToDecimal(PAN1);
            spinEdit2.Value = Convert.ToDecimal(PAN2);
            //spinEdit3.Value = Convert.ToDecimal(BON1);
            //spinEdit4.Value = Convert.ToDecimal(BON2);
            spinEdit5.Value = Convert.ToDecimal(BEM1);
            spinEdit6.Value = Convert.ToDecimal(BEM2); ;
        }

        private void SenserCreate()
        {
            try
            {
                AdvantechAPI.OpenDevice(0, ref DeviceHandle);

                tmrsensor.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region Onload
        private void frmMain_Load(object sender, EventArgs e)
        {
            //계량전 
            try
            {
                Check_Update(); 

                SetSoundVolume(100);

                mdb_main = new DBHelper_ORACLE(CS_MES.Replace("MES_TNS", MES_TNS));
                //mdb_main.DBConn();

                SetServiceURL();

                //모든 변수 초기화 
                txt_clear();

                lblautoNum.Tag = "";
                AutoNum = "";
                lblautoNum.Text = "";

                // RFID 접속
                server_connect();

                if (!SocketClient.Connected)
                {
                    MessageBox.Show("RFID 접속 안됨");
                }

                //인디 게이터랑 RFID 카드 되도록 설정ss                
                SerialCreate(Weight_Area);

                if (Weight_Area == "P04" || Weight_Area == "P05" || Weight_Area == "P03")
                {
                    SenserCreate();
                }

                // LPR 접속
                if (Weight_Area == "P05") // 3문 공차
                {
                    server_connect_LPR();

                    logger.Info("LPR 스타트");
                }

                if (Weight_Area == "P08") // 5문국고
                {
                    paLPR.Visible = true;
                    label4.Visible = true;

                    ServerThread = new Thread(new ThreadStart(Thread_Run));
                    ServerThread.IsBackground = true;
                    ServerThread.Start();

                    paLPR.BackColor = Color.Green;

                    logger.Info("LPR 스타트");

                    string processName = Process.GetCurrentProcess().ProcessName;
                    Process.Start(Environment.CurrentDirectory + "\\LED_CONTROL.exe", processName);
                }

                timer_main.Enabled = true;
                tmrsensor.Enabled = true;
                timer_indicator.Enabled = true;

                //SendMessage를 위한 Handle값 저장
                iFormHandle = this.Handle.ToInt32();

                // 식당서버 접속
                mdb.DBConn();

                BarricateSensor_off.BackColor = System.Drawing.Color.Gray;
                BarricateSensor_on.BackColor = System.Drawing.Color.Lime;
            }
            catch (Exception ex)
            {
                //err_cnt = err_cnt + 1;
                logger.Error("무인 계량 LOAD중 ERROR: " + ex.Message.ToString().Trim());
            }
        }
        #endregion

        #region Main_Load (RFID 처리 메인)
        private void Main_Load()
        {
            try
            {
                String tmp_down = "";
                Int32 real = 0;

                DataTable dt_rfid = null;
                //-------------------------------------------------------
                // 1. 당일 계량 금지 인지 확인 
                limit_yn = DB_Process.CAR_LIMIT_CHK_ALL();
                if (limit_yn == "Y")
                {
                    player30.Play();
                    la_ment.Text = "오늘은 계량을 진행하지 않습니다. 천천히 출발해 주세요";
                    logger.Info(this.ToString() + " 입차제한-일자");
                    end_timer.Enabled = true;
                    return;
                }
                else
                {
                    dt_rfid = DB_Process.Start_rfid(rfid_no);
                    player32.Play(); //잠시만 기다리세요   
                }
                //======================================================

                //-------------------------------------------------------
                // 2. RFID 카드 번호 확인. 계량 시작  
                if (dt_rfid != null && dt_rfid.Rows.Count > 0)
                {
                    car_no = dt_rfid.Rows[0]["VEHL_NO"].ToString();
                    item_so = dt_rfid.Rows[0]["ITEM_SO"].ToString();
                    item_jung = dt_rfid.Rows[0]["ITEM_JUNG"].ToString();
                    if_no = dt_rfid.Rows[0]["IF_NO2"].ToString();
                    pmax_wght = dt_rfid.Rows[0]["PMAX_WGHT"].ToString();
                    pmin_wght = dt_rfid.Rows[0]["PMIN_WGHT"].ToString();

                    //입차 제한 차량 확인 
                    limit_yn = DB_Process.check_car_limit("01", car_no);

                    if (limit_yn == "Y")
                    {
                        //전광판- 입출차제한
                        player08.Play();

                        la_ment.Text = "입출차 제한 차량 입니다. ";
                        logger.Info(this.ToString() + "<" + car_no + " >입출차 제한 차량 ");
                        end_timer.Enabled = true;
                        return;
                    }

                    //전량퇴송 차량 제한 차량 확인 
                    limit_yn = DB_Process.check_car_limit("03", car_no);

                    if (limit_yn == "Y")
                    {
                        //전광판- 입출차제한
                        player08.Play();

                        la_ment.Text = "금일 퇴송차량입니다. 입고 할수 없습니다. ";
                        logger.Info(this.ToString() + "<" + car_no + " >금일 퇴송차량입니다. 입고 할수 없습니다. ");
                        end_timer.Enabled = true;
                        return;
                    }

                    rfid_no = dt_rfid.Rows[0]["RFID_NO"].ToString();
                    wght_no = dt_rfid.Rows[0]["WGHT_NO"].ToString();
                    rfid_seq = dt_rfid.Rows[0]["RFID_SEQ"].ToString();
                    item_dae = dt_rfid.Rows[0]["ITEM_DAE"].ToString();

                    if (if_no == "B")
                    {
                        lb_max.Text = pmax_wght.ToString().Trim();
                        lb_min.Text = pmin_wght.ToString().Trim();
                    }

                    lblRfid_no.Text = rfid_no;
                    labelControl2.Text = dt_rfid.Rows[0]["ITEM_SO_NM"].ToString();
                    labelControl4.Text = car_no;
                    labelControl17.Text = wght_no;
                    labelControl18.Text = dt_rfid.Rows[0]["ITEM_JUNG_NM"].ToString();

                    //========================================================================================================================================
                    // 1차 계량값만 있으면 얘는 2차 계량임. 
                    if ((dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString() != "0") && (dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString() != ""))
                    {
                        //품목은 수입 형강에 , 2차 계량까지 다 끝나면 얘는 검량임 
                        if (dt_rfid.Rows[0]["WEIGHT_STATE"].ToString() == "2")
                        {
                            Gubun = "K";
                        }
                        else //---------------------- 3문에 수입 형강 검량 아닌애들 if 시작 
                        {
                            lblFirst_wght.Text = dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString(); //1차 중량값  
                            lblSecond_wght.Text = lblWeight_value.Text;

                            Gubun = "Y"; // 2차 계량 

                            //---------------------------------------------------------------
                            //품목 대 분류에 따라 1차 계량과 2차 계량의 차이를 체크 하도록 함
                            //---------------------------------------------------------------
                            int ch_1 = Convert.ToInt32(dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString());
                            int ch_2 = Convert.ToInt32(lblWeight_value.Text);
                            int ch_3 = 0;

                            if (item_dae == "01")
                            {
                                ch_3 = ch_1 - ch_2;
                            }

                            if (item_dae != "01")
                            {
                                ch_3 = ch_2 - ch_1;
                            }
                            
                            real = ch_3;
                            if (real < 101)
                            {
                                if (item_jung == "105")
                                {
                                    if (DialogResult.Cancel == MessageOpen("계량번호[ " + wght_no + " ]" + labelControl18.Text + "중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " + ch_2.ToString() + " >", "중량이상"))
                                    {
                                        player35.Play();
                                        la_ment.Text = "계량을 진행할수 없습니다. 출하실에 연락바랍니다 (출하실전화번호) ";

                                        end_timer.Enabled = true;

                                        return;
                                    }
                                }
                                else if (item_jung == "108")
                                {
                                    ch_3 = Math.Abs(ch_3);

                                    if (DialogResult.Cancel == MessageOpen("계량번호[ " + wght_no + " ]" + labelControl18.Text + "중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " + ch_2.ToString() + " >", "중량이상"))
                                    {
                                        player35.Play();
                                        la_ment.Text = "계량을 진행할수 없습니다. 출하실에 연락바랍니다 (출하실전화번호) ";

                                        end_timer.Enabled = true;

                                        return;
                                    }
                                }
                                else
                                {
                                    if (DialogResult.Cancel == MessageOpen("계량번호[ " + wght_no + " ]" + labelControl18.Text + "중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " + ch_2.ToString() + " >", "중량이상"))
                                    {
                                        player35.Play();
                                        la_ment.Text = "계량을 진행할수 없습니다. 출하실에 연락바랍니다 (출하실전화번호) ";

                                        logger.Info("계량번호[ " + wght_no + " ]" + labelControl18.Text + "중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " + ch_2.ToString() + " 100키로 미만>");
                                        end_timer.Enabled = true;
                                        return;
                                    }
                                }
                            }
                            lblReal_wgt.Text = ch_3.ToString();

                            // 1. 입고 : 1차 > 2차 정상--------------------------------------- 
                            if ((item_dae == "01") && (ch_3 < 0)) //2차 값이 더 클 경우 
                            {
                                //전광판- 중량이상오류
                                //dp_show("중량이상오류", lblWeight_value.Text);

                                if (item_jung == "105")
                                {
                                    if (DialogResult.Cancel == MessageOpen("계량번호[ " + wght_no + " ]" + labelControl18.Text + "중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " + ch_2.ToString() + " >", "중량이상"))
                                    {
                                        end_timer.Enabled = true;

                                        return;
                                    }
                                }
                                else if (item_jung == "108")
                                {
                                    if (DialogResult.Cancel == MessageOpen("계량번호[ " + wght_no + " ]" + labelControl18.Text + "중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " + ch_2.ToString() + " >", "중량이상"))
                                    {
                                        ch_3 = Math.Abs(ch_3);

                                        end_timer.Enabled = true;

                                        return;
                                    }
                                }
                                else
                                {
                                    player35.Play();
                                    la_ment.Text = "계량을 진행할수 없습니다. 출하실에 연락바랍니다 < 1차 : " + ch_1.ToString() + " , 2차 " + ch_2.ToString() + " >";

                                    logger.Info("계량번호[ " + wght_no + " ]" + labelControl18.Text + " >< 1차 : " + ch_1.ToString() + " , 2차 " + ch_2.ToString() + " >");
                                    end_timer.Enabled = true;
                                    return;
                                }
                            }
                            else if ((item_dae != "01") && (ch_1 >= ch_2)) //1차 값이 더 큰경우  
                            {
                                // 2. 그외 : 1차 < 2차 

                                //전광판- 중량이상오류
                                //dp_show("중량이상오류", lblWeight_value.Text);

                                player35.Play();
                                la_ment.Text = "계량을 진행할수 없습니다. 출하실에 연락바랍니다 중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " + ch_2.ToString() + " >";
                                logger.Info("계량번호[ " + wght_no + " ]" + labelControl18.Text + " 중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " + ch_2.ToString() + " >");
                                end_timer.Enabled = true;
                                return;
                            }
                            //--------------------------------------------------------------- 

                            Double TOTPROD_WGT = 0.0;
                            Double TOTPMIN_WGT = 0.0;
                            Double TOTPMAX_WGT = 0.0;

                            Double Sein = 0;
                            Double Sein_Point = 0;

                            String LOAD_ORD_NO = "";

                            // 국내고철
                            if (item_jung == "105")
                            {
                                String chk_query = " SELECT NVL(INSPECT,'N') INSPECT "
                                                + " FROM TB_WG02_0002 AR "
                                                + " WHERE AR.WGHT_NO = '" + dt_rfid.Rows[0]["WGHT_NO"].ToString() + "' "
                                                + "   AND AR.DEL_YN = 'N'"
                                                ;
                                ServiceAdapter _svc = new ServiceAdapter();
                                DataSet ds_chk = _svc.GetQuery(chk_query);

                                if (ds_chk.Tables[0].Rows[0]["INSPECT"].ToString() != "Y")
                                {
                                    player66.Play(); 
                                    MessageOpen("[2차계량]고철등급판정이 완료되지 않았습니다.", "고철등급판정");
                                    end_timer.Enabled = true;
                                    return;
                                }
                            }

                            // 제품 계산 
                            if (item_jung == "201")
                            {
                                String chk_query = " SELECT AR.RFID_NO AS RFID_NO, SO.PROD_WGHT AS PROD_WGHT, SO.IF_NO AS LOAD_ORD_NO, AR.WGHT_NO AS WGHT_NO, "
                                                 + "   NVL(SO.PMIN_WGHT, 0) AS PMIN_WGHT, NVL(SO.PMAX_WGHT, 0) AS PMAX_WGHT, SO.CUST_CD AS CUST_CD, NVL(AR.INSPECT,'N') INSPECT_YN "
                                                 + "   FROM TB_WG02_0001 SO "
                                                 + "   LEFT OUTER JOIN TB_WG02_0002 AR ON (SO.RFID_SEQ = AR.RFID_SEQ)  "
                                                 + " WHERE AR.RFID_NO = '" + rfid_no + "' AND SO.DEL_YN = 'N' AND AR.DEL_YN = 'N' AND SO.WEIGHT_YN = 'W'";
                                ServiceAdapter _svc = new ServiceAdapter();
                                DataSet ds_chk = _svc.GetQuery(chk_query);

                                if (ds_chk.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i <= ds_chk.Tables[0].Rows.Count - 1; i++)
                                    {
                                        if (ds_chk.Tables[0].Rows[i]["INSPECT_YN"].ToString() != "Y")
                                        {
                                            logger.Info("제품 검수가 완료되지 않았습니다. 3 <" + ds_chk.Tables[0].Rows[i]["WGHT_NO"].ToString() + "> ");
                                            //player35.Play();
                                            //la_ment.Text = "제품 검수가 완료되지 않았습니다. <" + ds_chk.Tables[0].Rows[i]["LOAD_ORD_NO"].ToString() + "> ";
                                            //timer_main.Enabled = true;
                                            //return;
                                        }

                                        LOAD_ORD_NO = ds_chk.Tables[0].Rows[i]["LOAD_ORD_NO"].ToString();

                                        TOTPROD_WGT = TOTPROD_WGT + Convert.ToInt32(ds_chk.Tables[0].Rows[i]["PROD_WGHT"]);
                                        TOTPMIN_WGT = TOTPMIN_WGT + Convert.ToInt32(ds_chk.Tables[0].Rows[i]["PMIN_WGHT"]);
                                        TOTPMAX_WGT = TOTPMAX_WGT + Convert.ToInt32(ds_chk.Tables[0].Rows[i]["PMAX_WGHT"]);
                                    }

                                    if (if_no == "B")
                                    {
                                        lb_max.Text = TOTPMAX_WGT.ToString().Trim();
                                        lb_min.Text = TOTPMIN_WGT.ToString().Trim();
                                    }

                                    ch_3 = ch_2 - ch_1;
                                    lblSecond_wght.Text = ch_2.ToString(); 
                                    lblProd_wgt.Text = TOTPROD_WGT.ToString(); 

                                    Sein = (((ch_3 - TOTPROD_WGT) / TOTPROD_WGT) * 100.0);
                                    //2014.03.26 소수점 2째자리 까지 표현하기 위해 추가 (Sein_Point)
                                    Sein_Point = (((ch_3 - TOTPROD_WGT) / TOTPROD_WGT) * 100.0) * 0.0;

                                    if (LOAD_ORD_NO.Substring(1, 1) == "P")
                                    {
                                        if (Sein > 7 || Sein < -7)
                                        {
                                            logger.Info("계량을 승인 하시겠습니까?  세인량 범위초과 <" + Sein.ToString() + "> ");
                                            player35.Play();

                                            if (DialogResult.Cancel == MessageOpen("중량차이(" + Convert.ToString(Math.Abs(ch_3 - TOTPROD_WGT)) + " 가 큽니다.계량을 승인하시겠습니까?", "세인량 범위초과"))
                                            {
                                                end_timer.Enabled = true;

                                                return;
                                            }
                                        }
                                    }

                                    if (LOAD_ORD_NO.Substring(1, 1) == "B")
                                    {
                                        if (TOTPMIN_WGT > ch_3)
                                        {
                                            logger.Info("계량을 승인 하시겠습니까?  세인량 범위초과 <" + ch_3.ToString() + "> ");
                                            player35.Play();

                                            if (DialogResult.Cancel == MessageOpen("중량차이(" + Convert.ToString(Math.Abs(ch_3 - TOTPROD_WGT)) + " 가 큽니다.계량을 승인하시겠습니까?", "세인량 범위초과"))
                                            {
                                                end_timer.Enabled = true;

                                                return;
                                            }
                                        }

                                        if (TOTPMAX_WGT < ch_3)
                                        {
                                            logger.Info("계량을 승인 하시겠습니까?  세인량 범위초과 <" + ch_3.ToString() + "> ");
                                            player35.Play();

                                            if (DialogResult.Cancel == MessageOpen("중량차이(" + Convert.ToString(Math.Abs(ch_3 - TOTPROD_WGT)) + " 가 큽니다.계량을 승인하시겠습니까?", "세인량 범위초과"))
                                            {
                                                end_timer.Enabled = true;

                                                return;
                                            }
                                        }
                                    }

                                    if (LOAD_ORD_NO.Substring(1, 1) == "S")
                                    {
                                        if (Sein > 7 || Sein < -7)
                                        {
                                            logger.Info("계량을 승인 하시겠습니까?  세인량 범위초과 <" + Sein.ToString() + "> ");
                                            player35.Play();

                                            if (DialogResult.Cancel == MessageOpen("중량차이(" + Convert.ToString(Math.Abs(ch_3 - TOTPROD_WGT)) + " 가 큽니다.계량을 승인하시겠습니까?", "세인량 범위초과"))
                                            {
                                                end_timer.Enabled = true;

                                                return;
                                            }
                                        }
                                    }   
                                }
                            }

                            // H수입형강
                            if (item_jung == "104")
                            {
                                String chk_query = " SELECT AR.RFID_NO AS RFID_NO, SO.PROD_WGHT AS PROD_WGT, SO.IF_NO AS LOAD_ORD_NO, AR.WGHT_NO AS WGHT_NO, "
                                                 + "   NVL(SO.PMIN_WGHT, 0) AS PMIN_WGHT, NVL(SO.PMAX_WGHT, 0) AS PMAX_WGHT, SO.CUST_CD AS CUST_CD, NVL(AR.INSPECT,'N') INSPECT_YN "
                                                 + "   FROM TB_WG02_0001 SO "
                                                 + "   LEFT OUTER JOIN TB_WG02_0002 AR ON (SO.RFID_SEQ = AR.RFID_SEQ)  "
                                                 + " WHERE AR.WGHT_NO = '" + dt_rfid.Rows[0]["WGHT_NO"].ToString() + "' AND AR.DEL_YN = 'N' AND SO.DEL_YN = 'N' ";
                                ServiceAdapter _svc = new ServiceAdapter();
                                DataSet ds_chk = _svc.GetQuery(chk_query);

                                if (ds_chk.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i <= ds_chk.Tables[0].Rows.Count - 1; i++)
                                    {
                                        LOAD_ORD_NO = ds_chk.Tables[0].Rows[0]["LOAD_ORD_NO"].ToString();

                                        TOTPROD_WGT = Convert.ToInt32(ds_chk.Tables[0].Rows[i]["PROD_WGT"]);
                                    }
                                    lblProd_wgt.Text = TOTPROD_WGT.ToString();
                                    ch_3 = ch_1 - ch_2;
                                    Sein = (((ch_3 - TOTPROD_WGT) / TOTPROD_WGT) * 100.0);
                                    //2014.03.26 소수점 2째자리 까지 표현하기 위해 추가 (Sein_Point)
                                    Sein_Point = (((ch_3 - TOTPROD_WGT) / TOTPROD_WGT) * 100.0) * 0.0;
                                    
                                    if (Sein > 7 || Sein < -7)
                                    {
                                        logger.Info("계량을 승인 하시겠습니까?  세인량 범위초과 <" + Sein.ToString() + "> ");
                                        player35.Play();

                                        if (DialogResult.Cancel == MessageOpen("중량차이(" + Convert.ToString(Math.Abs(ch_3 - TOTPROD_WGT)) + " 가 큽니다.계량을 승인하시겠습니까?", "세인량 범위초과"))
                                        {
                                            end_timer.Enabled = true;

                                            return;
                                        }
                                    }
                                }
                            }

                            // 형강수출
                            if (item_jung == "202")
                            {
                                String chk_query = " SELECT AR.RFID_NO AS RFID_NO, SO.PROD_WGHT AS PROD_WGT, SO.IF_NO AS LOAD_ORD_NO, AR.WGHT_NO AS WGHT_NO, "
                                                 + "   NVL(SO.PMIN_WGHT, 0) AS PMIN_WGHT, NVL(SO.PMAX_WGHT, 0) AS PMAX_WGHT, SO.CUST_CD AS CUST_CD, NVL(AR.INSPECT,'N') INSPECT_YN "
                                                 + "   FROM TB_WG02_0001 SO "
                                                 + "   LEFT OUTER JOIN TB_WG02_0002 AR ON (SO.RFID_SEQ = AR.RFID_SEQ)  "
                                                 + " WHERE AR.RFID_NO = '" + rfid_no + "' AND AR.DEL_YN = 'N' AND SO.DEL_YN = 'N' AND SO.WEIGHT_YN = 'W'";
                                ServiceAdapter _svc = new ServiceAdapter();
                                DataSet ds_chk = _svc.GetQuery(chk_query);

                                if (ds_chk.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i <= ds_chk.Tables[0].Rows.Count - 1; i++)
                                    {
                                        if (ds_chk.Tables[0].Rows[i]["INSPECT_YN"].ToString() != "Y")
                                        {
                                            logger.Info("제품 검수가 완료되지 않았습니다. 1 <" + ds_chk.Tables[0].Rows[i]["WGHT_NO"].ToString() + "> ");
                                            //player35.Play();
                                            //la_ment.Text = "제품 검수가 완료되지 않았습니다. <" + ds_chk.Tables[0].Rows[i]["LOAD_ORD_NO"].ToString() + "> ";
                                            //timer_main.Enabled = true;
                                            //return;
                                        }

                                        LOAD_ORD_NO = ds_chk.Tables[0].Rows[0]["LOAD_ORD_NO"].ToString();
                                        TOTPROD_WGT = TOTPROD_WGT + Convert.ToInt32(ds_chk.Tables[0].Rows[i]["PROD_WGT"]);
                                    }
                                    lblProd_wgt.Text = TOTPROD_WGT.ToString();

                                    ch_3 = ch_2 - ch_1;
                                    Sein = (((ch_3 - TOTPROD_WGT) / TOTPROD_WGT) * 100);
                                    //2014.03.26 소수점 2째자리 까지 표현하기 위해 추가 (Sein_Point)
                                    Sein_Point = (((ch_3 - TOTPROD_WGT) / TOTPROD_WGT) * 100) * 0.0;

                                    if (Sein > 7 || Sein < -7)
                                    {
                                        logger.Info("계량을 승인 하시겠습니까?  세인량 범위초과 <" + Sein.ToString() + "> ");
                                        player35.Play();

                                        if (DialogResult.Cancel == MessageOpen("중량차이(" + Convert.ToString(Math.Abs(ch_3 - TOTPROD_WGT)) + " 가 큽니다.계량을 승인하시겠습니까?", "세인량 범위초과"))
                                        {
                                            end_timer.Enabled = true;

                                            return;
                                        }
                                    }
                                }
                            } 
                        }//---------------------- 3문에 수입 형강 검량 아닌애들 if 종료 
                    }
                    else
                    {
                        // 국고
                        if (item_jung == "105")
                        {
                            String kuk_value = " SELECT NVL(A.CONFIRM_YN,'N') CONFIRM_YN  FROM TB_WG03_0001 A "
                                              + "WHERE A.RFID_SEQ_LINK = '" + rfid_seq + "' ";
                            ServiceAdapter _svc = new ServiceAdapter();
                            DataSet kuk_v = _svc.GetQuery(kuk_value);

                            if(kuk_v.Tables[0].Rows.Count > 0)
                            {
                                if (kuk_v.Tables[0].Rows[0]["CONFIRM_YN"].ToString() == "N")
                                {
                                    player35.Play();
                                    logger.Info(" 확정되지 않는 스크랩계량을 할수 없습니다");

                                    la_ment.Text = "확정되지 않는 스그랩계량을 할수 없습니다 ";

                                    end_timer.Enabled = true;

                                    return;
                                }
                            }

                            if (lblWeight_value.Text != "")
                            {
                                if (Convert.ToInt32(lblWeight_value.Text) > 44000)
                                {
                                    player35.Play();
                                    logger.Info(" 중량값이 44톤을 넘었습니다. 계량할수 없습니다.  <" + lblWeight_value.Text + "> ");

                                    la_ment.Text = "중량값이 44톤을 넘었습니다. 계량할수 없습니다. ";

                                    end_timer.Enabled = true;

                                    return;
                                }
                            }

                            // LPR 차량번호 인식
                            if (Weight_Area == "P05" || Weight_Area == "P08")
                            {
                                if (labelControl4.Text.Trim() != AutoNum.Trim())
                                {
                                    frmChkCAR frm = new frmChkCAR();
                                    frm.RFID_SEQ = rfid_seq; 
                                    frm.CAR_NO1 = AutoNum;
                                    frm.CAR_NO2 = labelControl4.Text;
                                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                                    {
                                        RFID_CODE = frm.RFID_CODE; 
                                        AutoNum = frm.CAR_NO1;
                                        frm.Close();

                                        end_timer.Enabled = true;

                                        return;
                                    }
                                    else
                                    {
                                        RFID_CODE = frm.RFID_CODE; 
                                        AutoNum = frm.CAR_NO1;
                                        frm.Close();
                                    }
                                }
                            }
                        }
                        
                        //1차 계량 
                        Gubun = "N";
                    }
                    //알림 화면 초기화 
                    la_ment.Text = "";

                    //계량값으로 계량 시작 
                    tmp_down = Convert.ToInt32(lblWeight_value.Text).ToString();

                    //수입빌렛일 경우 입고지만 이적 처럼 사용 해야함 
                    if (item_so == "1014")
                    {
                        if (Gubun == "N")
                        {
                            //1차 계량일때 
                            Billet_fg = DB_Process.Billet_01_set(rfid_seq, rfid_no, AREA, Convert.ToInt32(tmp_down));

                        }
                        else
                        {
                            //2차 계량일때 
                            Billet_fg = DB_Process.Billet_02_set(wght_no, AREA, Convert.ToInt32(tmp_down));
                        }

                        if (Billet_fg == true)
                        {
                            weight_fg = true;
                        }
                    }
                    else
                    {
                        weight_fg = DB_Process.weight_fg(item_so, AREA, Gubun, Convert.ToInt32(tmp_down), rfid_seq, wght_no, Weight_Area);
                    }
                    //==============================================================================================================================

                    logger.Info(" 저장 구분 : " + weight_fg);

                    //==============================================================================================================================
                    //weight_fg :true 계량저장이 정상적으로 되었음. -> 계량표 출력 
                    if (weight_fg == true)
                    {

                        //계량표 출력
                        if (Gubun == "K")
                        {
                            player05.Play();
                            la_ment.Text = "검수 계량이 완료 되었습니다. ";
                            logger.Info(" 검수 계량 >< " + rfid_seq + " > 검수  계량 완료 ");

                            GateControl("01");
                        }
                        else//-------------------------검수 계량 아닌거 if 시작 
                        {
                            // 1차계량 국내고철 
                            // 식당DB에 계량번호 및 차량번호, 
                            if (Gubun == "N")
                            {
                                if (item_jung == "105")
                                {
                                    ServiceAdapter _svc = new ServiceAdapter();

                                    if (AutoNum != "")
                                    {
                                        String VHEL_IMAGE_SAVE = " INSERT INTO TB_WG04_0001 "
                                                       + "            (WGHT_NO, WEIGHT_FG, RFID_NO, VEHL_NO, LOAD_IMAGE, RFID_CODE, CRT_DTM) "
                                                       + " SELECT "
                                                       + "   WGHT_NO, '1', RFID_NO, "
                                                       + "   '" + labelControl4.Text.Trim() + "', '" + lblautoNum.Tag.ToString() + "', '" + RFID_CODE + "', SYSDATE  "
                                                       + " FROM TB_WG02_0002 "
                                                       + " WHERE RFID_SEQ = '" + rfid_seq + "' AND DEL_YN = 'N' ";
                                        _svc.SetQuery(VHEL_IMAGE_SAVE);

                                        lblautoNum.Tag = "";
                                        AutoNum = "";
                                        //lblautoNum.Text = "";                                        
                                    }

                                    String food_value = " SELECT A.WGHT_NO, A.VEHL_NO, A.RFID_SEQ, B.CD_V_MEANING   FROM TB_WG02_0002 A "
                                                      + " LEFT OUTER JOIN (SELECT  CD_V AS CD_V, CD_V_MEANING AS CD_V_MEANING "
                                                      + "                  FROM  M00APUSER.VI_M00_CODE_ACCESS "
                                                      + "                  WHERE CATEGORY_GROUP_NM = 'SZ0000' "
                                                      + "                   AND CD_TP = 'VENDER_CD'  ) B ON "
                                                      + "       A.CUST_CD = B.CD_V "
                                                      + "WHERE A.RFID_SEQ = '" + rfid_seq + "' AND A.DEL_YN = 'N' ";                                    
                                    DataSet food_v = _svc.GetQuery(food_value);

                                    // 식당서버에 값 인서트
                                    FOOD_SAVE(food_v.Tables[0].Rows[0]["WGHT_NO"].ToString(), food_v.Tables[0].Rows[0]["VEHL_NO"].ToString(), food_v.Tables[0].Rows[0]["CD_V_MEANING"].ToString());
                                }
                            }
                            else
                            {
                                // LPR 업데이트
                                if (item_jung == "105")
                                {
                                    if (AutoNum != "")
                                    {
                                        String VHEL_IMAGE_SAVE = " UPDATE TB_WG04_0001 SET"
                                                       + "            DOWN_IMAGE = '" + lblautoNum.Tag.ToString() + "' "
                                                       + "         WHERE WGHT_NO = '" + wght_no + "' ";
                                        ServiceAdapter _svc = new ServiceAdapter();
                                        _svc.SetQuery(VHEL_IMAGE_SAVE);

                                        lblautoNum.Tag = "";
                                        AutoNum = "";
                                        //lblautoNum.Text = "";
                                    }
                                }
                            }

                            by_Matrial(Gubun, item_dae, item_jung, item_so, rfid_seq);

                            GateControl("01");

                            if (Gubun == "N")
                            {
                                lblFirst_wght.Text = tmp_down.ToString();

                                //이적 차량 이거나 신평이체로 들어온 애는 2차 계량만 하고 끝임. 
                                if (item_dae == "04" || item_so == "3043")
                                {
                                    // 이적일때 디스플레이 값 불러오기
                                    String auto_value = " SELECT WGHT_NO, LOAD_WEIGHT, PROD_WGHT, DOWN_WEIGHT FROM TB_WG02_0002 WHERE RFID_SEQ = '" + rfid_seq + "' AND DEL_YN = 'N' ";
                                    ServiceAdapter _svc = new ServiceAdapter();
                                    DataSet auto_v = _svc.GetQuery(auto_value);

                                    if (auto_v.Tables[0].Rows.Count > 0)
                                    {
                                        if(Convert.ToInt32(auto_v.Tables[0].Rows[0]["DOWN_WEIGHT"].ToString()) == 0)
                                        {
                                            int ch_1 = Convert.ToInt32(auto_v.Tables[0].Rows[0]["LOAD_WEIGHT"].ToString());
                                            lblFirst_wght.Text = ch_1.ToString();

                                            player34.Play();
                                            la_ment.Text = "공차 계량이 완료 되었습니다. ";
                                            logger.Info(" 이적차량 공차계량 >< " + rfid_seq + " >  ");
                                        }
                                        else
                                        {
                                            int ch_1 = Convert.ToInt32(auto_v.Tables[0].Rows[0]["LOAD_WEIGHT"].ToString());
                                            int ch_2 = Convert.ToInt32(tmp_down);
                                            lblFirst_wght.Text = ch_1.ToString();
                                            lblSecond_wght.Text = tmp_down;
                                            lblReal_wgt.Text = System.Math.Abs(ch_2 - ch_1).ToString();
                                            lblProd_wgt.Text = auto_v.Tables[0].Rows[0]["PROD_WGHT"].ToString();
                                            labelControl17.Text = auto_v.Tables[0].Rows[0]["WGHT_NO"].ToString();

                                            //전광판-2차계량완료
                                            player65.Play();
                                            la_ment.Text = "2차 계량이 완료 되었습니다. ";
                                            logger.Info(" 2차 계량 >< " + rfid_seq + " > 2차 계량 완료 ");
                                            //dp_show("출발하세요", lblWeight_value.Text);
                                        }
                                    }

                                    GateControl("01");
                                }
                                else if (item_dae == "01" && item_so == "1014")
                                {
                                    String auto_value = " SELECT WGHT_NO, LOAD_WEIGHT,  DOWN_WEIGHT, REAL_WGHT  FROM TB_WG02_0002 WHERE RFID_SEQ = '" + rfid_seq + "' AND DEL_YN = 'N' ";
                                    ServiceAdapter _svc = new ServiceAdapter();
                                    DataSet auto_v = _svc.GetQuery(auto_value);

                                    if (auto_v.Tables[0].Rows.Count > 0)
                                    {
                                        lblFirst_wght.Text = auto_v.Tables[0].Rows[0]["LOAD_WEIGHT"].ToString();
                                        lblSecond_wght.Text = auto_v.Tables[0].Rows[0]["DOWN_WEIGHT"].ToString();
                                        lblReal_wgt.Text = auto_v.Tables[0].Rows[0]["REAL_WGHT"].ToString();
                                        labelControl17.Text = auto_v.Tables[0].Rows[0]["WGHT_NO"].ToString();
                                    }
                                    if (lblSecond_wght.Text != "0")
                                    {
                                        by_Matrial("Y", item_dae, item_jung, item_so, rfid_seq);
                                    }
                                    player05.Play();
                                    la_ment.Text = "계량이 완료 되었습니다. ";
                                    logger.Info(" 수입 빌렛 계량 >< " + rfid_seq + " > 계량 완료 ");

                                    //dp_show("출발하세요", lblWeight_value.Text);

                                    GateControl("01");
                                }
                                else
                                {
                                    // 형강 수출 계량 하는데 검수 확인 
                                    if (item_jung == "202")
                                    {

                                        String chk_value = " SELECT LOAD_ORD_NO , NVL(INSPECT_YN,'N') INSPECT FROM TB_WG03_0004  "
                                                         + " WHERE VEHL_NO = '" + car_no + "'"
                                                         + "  AND RFID_NO = '" + rfid_no + "'"
                                                         + "  AND DATA_USE = 'Y' "
                                                         + "  AND NVL(WEIGHT_YN ,'N') = 'N' ";
                                        ServiceAdapter _svc = new ServiceAdapter();
                                        DataSet chk_v = _svc.GetQuery(chk_value);
                                        for (int i = 0; i <= chk_v.Tables[0].Rows.Count - 1; i++)
                                        {
                                            if (chk_v.Tables[0].Rows[i]["INSPECT"].ToString() != "Y")
                                            {
                                                logger.Info("제품 검수가 완료되지 않았습니다. 2 <" + chk_v.Tables[0].Rows[i]["LOAD_ORD_NO"].ToString() + "> ");
                                                //player35.Play();
                                                //la_ment.Text = "제품 검수가 완료되지 않았습니다. <" + chk_v.Tables[0].Rows[i]["LOAD_ORD_NO"].ToString() + "> ";
                                                //timer_main.Enabled = true;
                                                //return;
                                            }
                                        }

                                        String wght_gung = " SELECT COUNT(*) AS CNT  FROM TB_WG01_0006 WHERE USE_YN = 'Y'"
                                                          + " AND VEHL_NO = '" + car_no + "'"
                                                          + " AND RFID_NO = '" + rfid_no + "'";

                                        ServiceAdapter _svc2 = new ServiceAdapter();
                                        DataSet chk_v2 = _svc2.GetQuery(wght_gung);

                                        if (chk_v.Tables[0].Rows.Count > 0)
                                        {
                                            if (chk_v.Tables[0].Rows[0]["cnt"].ToString() != "0")
                                            {
                                                player65.Play();
                                                la_ment.Text = "2차 계량이 완료 되었습니다. ";
                                                logger.Info(" 형강 2차 계량 >< 차량 : " + car_no + "- 카드 : " + rfid_no + " > 공차 있어서 한방에 계량 종료  ");
                                            }
                                        }
                                        else
                                        {
                                            player01.Play();
                                            la_ment.Text = "1차 계량이 완료 되었습니다. ";
                                            logger.Info(" 1차 계량 >< " + rfid_seq + " > 1차 계량 완료 ");
                                        }
                                    }
                                    else
                                    {
                                        //전광판-1차계량완료
                                        player01.Play();
                                        la_ment.Text = "1차 계량이 완료 되었습니다. ";
                                        logger.Info(" 1차 계량 >< " + rfid_seq + " > 1차 계량 완료 ");
                                        
                                        //dp_show("출발하세요", lblWeight_value.Text);
                                    }
                                }

                                GateControl("01");

                                return;
                            }
                            else
                            {
                                if (item_dae == "01" && item_so == "1014")
                                {
                                    String auto_value = " SELECT WGHT_NO, LOAD_WEIGHT,  DOWN_WEIGHT, REAL_WGHT  FROM TB_WG02_0002 WHERE RFID_SEQ = '" + rfid_seq + "' AND DEL_YN = 'N' ";
                                    ServiceAdapter _svc = new ServiceAdapter();
                                    DataSet auto_v = _svc.GetQuery(auto_value);

                                    if (auto_v.Tables[0].Rows.Count > 0)
                                    {
                                        lblFirst_wght.Text = auto_v.Tables[0].Rows[0]["LOAD_WEIGHT"].ToString();
                                        lblSecond_wght.Text = auto_v.Tables[0].Rows[0]["DOWN_WEIGHT"].ToString();
                                        lblReal_wgt.Text = auto_v.Tables[0].Rows[0]["REAL_WGHT"].ToString();
                                        labelControl17.Text = auto_v.Tables[0].Rows[0]["WGHT_NO"].ToString();
                                    }

                                    player05.Play();
                                    la_ment.Text = "계량이 완료 되었습니다. ";
                                    logger.Info(" 수입 빌렛 계량 >< " + rfid_seq + " > 계량 완료 ");

                                    //dp_show("출발하세요", lblWeight_value.Text);
                                }
                                else
                                {

                                    String auto_value = " SELECT A.WGHT_NO, SUM(B.PROD_WGHT) PROD_WGHT FROM TB_WG02_0002 A "
                                            + " LEFT OUTER JOIN TB_WG02_0002 B ON A.RFID_NO = B.RFID_NO AND A.IF_NO = B.IF_NO AND A.CRT_DTM = B.CRT_DTM "
                                            + "  WHERE A.RFID_SEQ = '" + rfid_seq + "' AND A.DEL_YN = 'N' GROUP BY A.WGHT_NO ";
                                    ServiceAdapter _svc = new ServiceAdapter();
                                    DataSet auto_v = _svc.GetQuery(auto_value);

                                    int ch_1 = Convert.ToInt32(dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString());
                                    int ch_2 = Convert.ToInt32(tmp_down);
                                    lblSecond_wght.Text = tmp_down;
                                    lblReal_wgt.Text = System.Math.Abs(ch_2 - ch_1).ToString();
                                    lblProd_wgt.Text = auto_v.Tables[0].Rows[0]["PROD_WGHT"].ToString();
                                    player65.Play(); 
                                    //전광판-2차계량완료

                                    la_ment.Text = "2차 계량이 완료 되었습니다. ";
                                    logger.Info(" 2차 계량 완료후  >< " + rfid_seq + " > 계량표 정상 출력 "); 
                                    //dp_show("출발하세요", tmp_down);
                                }


                                // 포항 이체
                                if (item_jung == "301")
                                {
                                    // 이적 배차 전송
                                    //String callUrl = "http://10.10.47.37/dk_mes_if/recv_rfid_info.aspx";

                                    //String postData = "V_AREA=P&V_RFID_NO2=" + dt.Rows[0]["RFID_NO"].ToString() + "&" + "V_VEHL_NO=" + textBox4.Text + "&" + "V_DRVR_NM=" + textBox5.Text + "&" + "V_DRVR_PHON=" + textBox6.Text + "&" + "V_DOWN_WGHT=" + textBox7.Text;

                                    //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(callUrl);

                                    //// 인코딩 UTF-8
                                    //byte[] sendData = UTF8Encoding.UTF8.GetBytes(postData);

                                    //httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                                    //httpWebRequest.Method = "POST";
                                    //httpWebRequest.ContentLength = sendData.Length;
                                    //Stream requestStream = httpWebRequest.GetRequestStream();
                                    //requestStream.Write(sendData, 0, sendData.Length);
                                    //requestStream.Close();

                                    //HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                    //StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                                    //string ret = streamReader.ReadToEnd();
                                    //streamReader.Close();
                                    //httpWebResponse.Close();

                                }

                                // 인천이체
                                if (item_jung == "303")
                                {
                                    //String callUrl = "http://10.10.47.37/dk_mes_if/recv_rfid_info.aspx";

                                    //String postData = "V_AREA=I&V_RFID_NO2=" + textBox3.Text + "&" + "V_VEHL_NO=" + textBox4.Text + "&" + "V_DRVR_NM=" + textBox5.Text + "&" + "V_DRVR_PHON=" + textBox6.Text + "&" + "V_DOWN_WGHT=" + textBox7.Text;

                                    //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(callUrl);

                                    ////// 인코딩 UTF-8
                                    ////byte[] sendData = UTF8Encoding.UTF8.GetBytes(postData);

                                    ////httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                                    ////httpWebRequest.Method = "POST";
                                    ////httpWebRequest.ContentLength = sendData.Length;
                                    ////Stream requestStream = httpWebRequest.GetRequestStream();
                                    ////requestStream.Write(sendData, 0, sendData.Length);
                                    ////requestStream.Close();

                                    ////HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                    ////StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                                    ////string ret = streamReader.ReadToEnd();
                                    ////streamReader.Close();
                                    ////httpWebResponse.Close();
                                }

                                GateControl("01");

                                return;
                            }
                        }//-------------------------검수 계량 아닌거 if 종료 
                    }
                    //==============================================================================================================================
                }//---------------------------------- 카드 번호가 널이 아인거 종료 
                else
                {
                    //==============================================================================================================================
                    // 배차된 RFID 카드 정보가 없을때 공차 계량인지 확인한다. 

                    if (rfid_no.Length > 4)
                    {
                        string query_rfid = " SELECT RFID_NO FROM TB_WG01_0004 WHERE RFID_SERIAL = '" + rfid_no + "' ";
                        ServiceAdapter _svc = new ServiceAdapter();
                        DataSet ds_r = _svc.GetQuery(query_rfid);

                        if (ds_r.Tables[0].Rows.Count > 0)
                        {
                            rfid_no = ds_r.Tables[0].Rows[0]["RFID_NO"].ToString();
                        }
                    }

                    string fix_car = DB_Process.Fix_car(rfid_no);

                    if (fix_car == "Y")
                    {
                        lblFirst_wght.Text = lblWeight_value.Text;
                        fix_fg = DB_Process.Gong_insert(rfid_no, Convert.ToInt32(lblWeight_value.Text), "1", Weight_Area);

                        player34.Play();
                        la_ment.Text = "공차계량이 완료되었습니다. 천천히 출발하세요. ";
                        logger.Info(" 공차계량 >< " + rfid_no + " > 공차 계량 정상 처리 ");

                        GateControl("01");
                    }
                    else
                    {

                        //전광판-카드정보없음
                        //dp_show("카드정보없음", lblWeight_value.Text);  
                        player31.Play();
                        la_ment.Text = "< " + rfid_no + " > RFID 카드 정보가 없습니다.";
                        logger.Info("< " + rfid_no + " > RFID 카드 정보 없음.");
                        end_timer.Enabled = true; 
                        return;
                    }
                    //===============================================================================================================================
                }
            }
            catch (Exception ex)
            {
                // 로그를 남기고
                if (logger.IsErrorEnabled)
                {
                    logger.Error(this.ToString() + " Mainload 에러----" + ex);
                }

                end_timer.Enabled = true;
            }
        }
        #endregion
         
        #region 계량표 출력
        //Gubun : N 1차 , Y 2차  // weight_kind : item_jung 
        private void by_Matrial(string gubun, string item_dae, string item_jung, string item_so, string rfid_seq)
        {
            try
            {
                // 출력 매수 확인
                DataTable dt_pnt_qty = new DataTable();
                DataTable dt_pnt = new DataTable();
                DataTable dt_pnt_all = new DataTable();
                int pnt_page_cnt = 0;


                // 소분류 출력매수 처리 추가
                // 
                dt_pnt_qty = DB_Process.pnt_qty(gubun, item_dae, item_jung, item_so, rfid_seq);

                if (dt_pnt_qty != null && dt_pnt_qty.Rows.Count > 0)
                {
                    pnt_page_cnt = Convert.ToInt32(dt_pnt_qty.Rows[0]["QTY"].ToString());
                    if (pnt_page_cnt > 0)
                    {
                        //출력할 데이터를 가져옴 
                        dt_pnt = DB_Process.pnt(gubun, rfid_seq, wght_no, item_so);

                        if (dt_pnt != null && dt_pnt.Rows.Count > 0)
                        {
                            switch (item_so)
                            {
                                case "1051": //1051 : 입고 - 국내 고철 일 경우 별도 , 출력물 양식이 다름 
                                    if (gubun == "N")
                                    {
                                        if (Weight_Area == "P08")
                                        {
                                            logger.Info("국고 확인증 출력 명령");

                                            String Query_print = " SELECT AR.LOAD_IMAGE, TO_CHAR(B.NUM,'0999') NUM "
                                                        + " FROM TB_WG04_0001 AR "
                                                        + " LEFT OUTER JOIN (SELECT ROWNUM AS NUM, WGHT_NO FROM TB_WG04_0001 "
                                                        + " WHERE SUBSTR(WGHT_NO,1,6) = TO_CHAR(SYSDATE,'YYMMDD')) B ON AR.WGHT_NO = B.WGHT_NO  "
                                                        + " WHERE AR.WGHT_NO = '" + dt_pnt.Rows[0]["WGHT_NO"].ToString().Replace("-", "") + "' ";
                                            ServiceAdapter _svc = new ServiceAdapter();
                                            DataSet ds_chk = _svc.GetQuery(Query_print);

                                            PirntFoodPaper(dt_pnt.Rows[0]["WGHT_NO"].ToString().Replace("-", ""), ds_chk.Tables[0].Rows[0]["NUM"].ToString(), dt_pnt.Rows[0]["VEHL_NO"].ToString(),
                                                                            dt_pnt.Rows[0]["VENDER_NAME"].ToString() + "/" + dt_pnt.Rows[0]["REAL_VENDER_NAME"].ToString(),
                                                                            dt_pnt.Rows[0]["LOAD_WEIGHT"].ToString(), dt_pnt.Rows[0]["LOAD_DATE"].ToString(), ds_chk.Tables[0].Rows[0]["LOAD_IMAGE"].ToString());

                                           
                                        }
                                        else
                                        {
                                            String Query_print = " SELECT AR.LOAD_IMAGE, TO_CHAR(B.NUM,'0999') NUM "
                                                        + " FROM TB_WG04_0001 AR "
                                                        + " LEFT OUTER JOIN (SELECT ROWNUM AS NUM, WGHT_NO FROM TB_WG04_0001 "
                                                        + " WHERE SUBSTR(WGHT_NO,1,6) = TO_CHAR(SYSDATE,'YYMMDD')) B ON AR.WGHT_NO = B.WGHT_NO  "
                                                        + " WHERE AR.WGHT_NO = '" + dt_pnt.Rows[0]["WGHT_NO"].ToString().Replace("-", "") + "' ";
                                            ServiceAdapter _svc = new ServiceAdapter();
                                            DataSet ds_chk = _svc.GetQuery(Query_print);


                                            if (ds_chk.Tables[0].Rows.Count > 0)
                                            {
                                                SubMatrial_Barcode Print_kuk = new SubMatrial_Barcode(dt_pnt.Rows[0]["WGHT_NO"].ToString().Replace("-", ""), dt_pnt.Rows[0]["VEHL_NO"].ToString(),
                                                                                dt_pnt.Rows[0]["VENDER_NAME"].ToString() + "/" + dt_pnt.Rows[0]["REAL_VENDER_NAME"].ToString(), dt_pnt.Rows[0]["LOAD_WEIGHT"].ToString(), dt_pnt.Rows[0]["LOAD_DATE"].ToString(), ds_chk.Tables[0].Rows[0]["LOAD_IMAGE"].ToString());
                                                //Print_kuk.ShowPreview();
                                                Print_kuk.Print();
                                            }
                                            else
                                            {
                                                SubMatrial_Barcode Print_kuk = new SubMatrial_Barcode(dt_pnt.Rows[0]["WGHT_NO"].ToString().Replace("-", ""), dt_pnt.Rows[0]["VEHL_NO"].ToString(),
                                                                                dt_pnt.Rows[0]["VENDER_NAME"].ToString() + "/" + dt_pnt.Rows[0]["REAL_VENDER_NAME"].ToString(), dt_pnt.Rows[0]["LOAD_WEIGHT"].ToString(), dt_pnt.Rows[0]["LOAD_DATE"].ToString(), "");
                                                //Print_kuk.ShowPreview();
                                                Print_kuk.Print();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 국구계량대만 시리얼 포트로 전송
                                        if (Weight_Area == "P08")
                                        {
                                            PrintCAR(dt_pnt);
                                        }
                                        else
                                        {
                                            for (int Cnt = 0; Cnt < pnt_page_cnt; Cnt++)
                                            {
                                                SubMatrial_KukS Print_kuk = new SubMatrial_KukS(dt_pnt);
                                                //Print_kuk.ShowPreview();
                                                Print_kuk.Print();
                                            }
                                        }
                                    }
                                    break;
                                case "2041"://2041 : 출고 - 올바로 연동 폐기물 일 경우 별도 , 2차 계량일 경우 인계서 나와야 함 

                                    for (int Cnt = 0; Cnt < pnt_page_cnt; Cnt++)
                                    {
                                        SubMatrial_All Print = new SubMatrial_All(dt_pnt);
                                        //Print.ShowPreview();
                                        Print.Print();
                                    }
                                    //if (gubun == "Y")
                                    //{
                                    //    //Allbaro_pnt Print_allbaro = new Allbaro_pnt();
                                    //    //Print_allbaro.ShowPreview(); 

                                    //    ManagementScope scope = new ManagementScope(@"\root\cimv2");
                                    //    scope.Connect();

                                    //    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Printer");
                                    //    string pnt_name = string.Empty;
                                    //    string printerName = string.Empty;

                                    //    foreach (ManagementObject printer in searcher.Get())
                                    //    {
                                    //        pnt_name = printer["Name"].ToString().ToUpper();
                                    //        printerName = "HP LASERJET M5035 PCL 6";
                                    //        bool chk;
                                    //        chk = pnt_name.Contains(printerName);
                                    //        if (chk == true) //프린터이름검색
                                    //        {
                                    //            dt_pnt_all = DB_Process.pnt_all(rfid_seq);
                                    //            Allbaro_pnt Print_allbaro = new Allbaro_pnt(dt_pnt_all);
                                    //            Print_allbaro.PrinterName = printer["Name"].ToString();
                                    //            Print_allbaro.Print();
                                    //            logger.Info(pnt_name + " 폐기물 인계서 출력 완료");
                                    //        }
                                    //        else
                                    //        {
                                    //            logger.Info(pnt_name + "<" + printerName + ">" + " 폐기물 인계서 출력 프린트 없음 ");
                                    //        }
                                    //    }
                                    //}
                                    break;
                                case "2023" : 
                                    // HBEAM 상차지시별로 출력
                                    // 2016-09-20 조용호
                                    // 상차지시서 수 별로 출력매수 정하기
                                    if (gubun == "Y")
                                    {
                                        String Query_print = " SELECT A.WGHT_NO, COUNT(*) CNT FROM TB_WG02_0002 A "
                                                           + " INNER JOIN TB_WG02_0002 B ON A.RFID_NO = B.RFID_NO AND A.IF_NO = B.IF_NO AND A.CRT_DTM = B.CRT_DTM "
                                                           + " WHERE A.RFID_SEQ = '" + dt_pnt.Rows[0]["RFID_SEQ"].ToString() + "' AND A.DEL_YN = 'N' GROUP BY A.WGHT_NO ";
                                       
                                        ServiceAdapter _svc = new ServiceAdapter();
                                        DataSet ds_chk = _svc.GetQuery(Query_print);

                                        if (ds_chk.Tables[0].Rows.Count > 0)
                                        {
                                            if (Convert.ToInt32(ds_chk.Tables[0].Rows[0]["CNT"]) > 0)
                                            {
                                                pnt_page_cnt = Convert.ToInt32(ds_chk.Tables[0].Rows[0]["CNT"]);
                                            }
                                        }

                                        for (int Cnt = 0; Cnt < pnt_page_cnt; Cnt++)
                                        {
                                            SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt);
                                            //Print_etc.ShowPreview();
                                            Print_etc.Print();
                                        }
                                    }
                                    else
                                    {
                                        for (int Cnt = 0; Cnt < pnt_page_cnt; Cnt++)
                                        {
                                            SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt);
                                            //Print_etc.ShowPreview();
                                            Print_etc.Print();
                                        }
                                    }
                                    break;
                                default:
                                    for (int Cnt = 0; Cnt < pnt_page_cnt; Cnt++)
                                    {
                                        SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt);
                                        //Print_etc.ShowPreview();
                                        Print_etc.Print();
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            logger.Info(gubun + " 차 출력정보 없음><배차: " + rfid_seq + "><계량:" + wght_no + "><품목:" + item_so + " >");
                            return;
                        }
                    }
                    else
                    {
                        logger.Info(gubun + " 차 해당 품목출력매수없음 ><품목: " + item_so + " >");
                        return;
                    }
                }
                else
                {
                    logger.Info(gubun + " 차 해당 품목출력없음 ><품목: " + item_so + " >");
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Error("출력물 에러 : " + ex.Message.ToString());    
            }
        }
        #endregion

        #region H/W        

        // 조용호 추가 인디게이터,RFID,위치세선 코드        
        #region 시리얼 객체 생성
        private void SerialCreate(String CODE)
        {
            try
            {
                // 인디게이터 시리얼포트
                serialPort[0] = new SerialPort();

                //060912 hoseong.hwang event TO timer
                //serialPort[0].DataReceived += new SerialDataReceivedEventHandler(Serial_DataRecv1);
                serialPort[0].PortName = HW_COMPORT;

                if (CODE == "P02")
                {
                    serialPort[0].BaudRate = 9600;
                    serialPort[0].Parity = Parity.None;
                    serialPort[0].DataBits = 7;
                    serialPort[0].StopBits = StopBits.One;
                }
                if (CODE == "P03")
                {
                    serialPort[0].BaudRate = 9600;
                    serialPort[0].Parity = Parity.None;
                    serialPort[0].DataBits = 8;
                    serialPort[0].StopBits = StopBits.One;
                }
                if (CODE == "P04")
                {
                    serialPort[0].BaudRate = 9600;
                    serialPort[0].Parity = Parity.Even;
                    serialPort[0].DataBits = 7;
                    serialPort[0].StopBits = StopBits.One;
                }
                if (CODE == "P05")
                {
                    serialPort[0].BaudRate = 9600;
                    serialPort[0].Parity = Parity.None;
                    serialPort[0].DataBits = 7;
                    serialPort[0].StopBits = StopBits.One;
                }
                if (CODE == "P06")
                {
                    serialPort[0].BaudRate = 9600;
                    serialPort[0].Parity = Parity.None;
                    serialPort[0].DataBits = 8;
                    serialPort[0].StopBits = StopBits.One;
                }
                if (CODE == "P07")
                {
                    serialPort[0].BaudRate = 9600;
                    serialPort[0].Parity = Parity.None;
                    serialPort[0].DataBits = 8;
                    serialPort[0].StopBits = StopBits.One;
                }

                if (CODE == "P08")
                {
                    serialPort[0].BaudRate = 9600;
                    serialPort[0].Parity = Parity.None;
                    serialPort[0].DataBits = 8;
                    serialPort[0].StopBits = StopBits.One;
                }
                if (CODE == "P09")
                {
                    serialPort[0].BaudRate = 2400;
                    serialPort[0].Parity = Parity.Even;
                    serialPort[0].DataBits = 7;
                    serialPort[0].StopBits = StopBits.One;
                }
                if (CODE == "P10")
                {
                    serialPort[0].BaudRate = 9600;
                    serialPort[0].Parity = Parity.None;
                    serialPort[0].DataBits = 7;
                    serialPort[0].StopBits = StopBits.One;
                }

                serialPort[0].Open();

                //Idicator로 수신버퍼 데이터 삭제                
                serialPort[0].DiscardInBuffer();
                serialPort[0].ReadExisting();
                serialPort[0].DiscardInBuffer();

                paIndg.BackColor = Color.Green; 

            }
            catch (Exception ex)
            {
                logger.Error("인디게이터 접속 에러: " + ex.Message.ToString());
   
                lblWeight_value.ForeColor = Color.Red;
                paIndg.BackColor = Color.Red;
            }
        }
        #endregion


        #region SerialPort Open(Indicaors공통)
        private void SerialPortOpen(SerialPort sp)
        {
            try
            {
                logger.Info(" Indicaors 재연결시도 ");
                sp.Close();
                Thread.Sleep(500);
                sp.Open();
            }
            catch
            {

            }
        }
        #endregion

        #region 센서 확인
        private void tmrsensor_Tick(object sender, EventArgs e)
        {   
            tmrsensor.Enabled = false;

            if (Weight_Area == "P03" || Weight_Area == "P04" || Weight_Area == "P05")
            {
                short portData = 0;

                try
                {
                    AdvantechAPI.Digital_ReadByteFromPort(DeviceHandle, 0, out portData);

                    switch (portData)
                    {
                        case 0:
                            outSensor.Image = imageList1.Images[1];
                            outSensor.Tag = "01";
                            InSensor.Image = imageList1.Images[1];
                            InSensor.Tag = "01";
                            break;

                        case 1:
                            outSensor.Image = imageList1.Images[1];
                            outSensor.Tag = "01";
                            InSensor.Image = imageList1.Images[0];
                            InSensor.Tag = "00";

                            logger.Info("1-outSensor(01), InSensor(00)");
                            //정지센스 사용안함. 중량안정화 사용
                            //RFID_READY();
                            break;

                        case 2:
                            outSensor.Image = imageList1.Images[0];
                            outSensor.Tag = "00";
                            InSensor.Image = imageList1.Images[1];
                            InSensor.Tag = "01";
                            logger.Info("1-outSensor(00), InSensor(01)");
                            break;

                        case 4:
                            outSensor.Image = imageList1.Images[0];
                            outSensor.Tag = "00";
                            InSensor.Image = imageList1.Images[0];
                            InSensor.Tag = "00";

                            logger.Info("1-outSensor(00), InSensor(00)");
                            break;

                        default:
                            outSensor.Image = imageList1.Images[1];
                            outSensor.Tag = "01";
                            InSensor.Image = imageList1.Images[1];
                            InSensor.Tag = "01";
                            break;
                    }
                   
                    if (Convert.ToInt32(lblWeight_value.Text.ToString()) < 500 && portData == 0)
                    {   
                        rfid_no = "";
                        txt_clear();

                        //lblautoNum.Tag = "";
                        //AutoNum = "";
                        //lblautoNum.Text = "";
                    }

                    if (Sencer_Action == false)
                    {
                        // 정지센스 인식 및 차단기 내림상태
                        if (outSensor.Tag.ToString() == "00" && bStopper_Status == true)
                        {
                            if (rfid_ready == false)
                            {
                                //dp_show("카드를대세요", lblWeight_value.Text);                                    

                                textEdit1.Text = "";
                                //RFID 카드를 대주세요 메세지 
                                player13.Play();
                                la_ment.Text = "RFID 카드를 대주세요 ";

                                rfid_ready = true;
                                //indicator_status = 0;
                            }
                        }                        
                    }

                    if (InSensor.Tag.ToString() == "00")
                    {
                        if (BarricateSensor_off.BackColor != System.Drawing.Color.Lime)
                        {
                            if (play60_chk == false)
                            {                            
                                GateControl("02");
                                play60_chk = true;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    logger.Error("센서 에러 : " + ex.Message.ToString());     
                    tmrsensor.Enabled = true;
                }

                tmrsensor.Enabled = true;   
            }           
            else
            {
                //// read Di port state
                byte portData = 0;

                ErrorCode errorCode = ErrorCode.Success;

                InstantDiCtrl instantDiCtrl = new InstantDiCtrl();

                try
                {
                    instantDiCtrl.SelectedDevice = new DeviceInformation(0);


                    //byte[] buffer = new byte[64];

                    // Step 3: Read DI ports' status and show.
                    errorCode = instantDiCtrl.Read(0, out portData);
                    if (BioFailed(errorCode))
                    {
                        throw new Exception();
                    }
                    //textBox2.Text = portData.ToString();

                    switch (portData.ToString("X2"))
                    {
                        case "00":
                            outSensor.Image = imageList1.Images[1];
                            outSensor.Tag = "01";
                            InSensor.Image = imageList1.Images[1];
                            InSensor.Tag = "01";
                            break;

                        case "01":
                            outSensor.Image = imageList1.Images[1];
                            outSensor.Tag = "01";
                            InSensor.Image = imageList1.Images[0];
                            InSensor.Tag = "00";

                            //정지센스 사용안함. 중량안정화 사용
                            //RFID_READY();
                            logger.Info("2-outSensor(01), InSensor(00)");
                            break;

                        case "02":
                            outSensor.Image = imageList1.Images[0];
                            outSensor.Tag = "00";
                            InSensor.Image = imageList1.Images[1];
                            InSensor.Tag = "01";

                            logger.Info("2-outSensor(00), InSensor(01)");
                            break;

                        case "03":
                            outSensor.Image = imageList1.Images[0];
                            outSensor.Tag = "00";
                            InSensor.Image = imageList1.Images[0];
                            InSensor.Tag = "00";

                            logger.Info("2-outSensor(00), InSensor(00)");
                            break;

                        default:
                            outSensor.Image = imageList1.Images[1];
                            outSensor.Tag = "01";
                            InSensor.Image = imageList1.Images[1];
                            InSensor.Tag = "01";
                            break;
                    }

                    instantDiCtrl.Dispose();
                    
                    if (Convert.ToInt32(lblWeight_value.Text.ToString()) < 500 && portData.ToString("X2") == "00")
                    {   
                        rfid_no = "";
                        txt_clear();

                        //lblautoNum.Tag = "";
                        //AutoNum = "";
                        //lblautoNum.Text = "";
                    }

                    if (Sencer_Action == false)
                    {
                        if (outSensor.Tag.ToString() == "00" && bStopper_Status == true)
                        {
                            if (rfid_ready == false)
                            {
                                //dp_show("카드를대세요", lblWeight_value.Text);                                    

                                textEdit1.Text = "";
                                //RFID 카드를 대주세요 메세지 
                                player13.Play();
                                la_ment.Text = "RFID 카드를 대주세요 ";

                                rfid_ready = true;
                                //indicator_status = 0;
                            }
                        }
                    }

                    if (InSensor.Tag.ToString() == "00")
                    {
                        if (play60_chk == false)
                        {
                            GateControl("02");

                            play60_chk = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("정위치 센서에러 : " + ex.Message.ToString());
                    tmrsensor.Enabled = true;
                }
                tmrsensor.Enabled = true;   
            }        
        }

        #endregion

        #endregion       

        #region 메인 타이머
        private void timer_main_Tick(object sender, EventArgs e)
        {
            timer_main.Enabled = false;
            
            try
            {
                if (lblWeight_value.Text.ToString() != "")
                {
                    // 계량 중량 안정화
                    // 최소/최대 중량차이가 50KG보다 작고, 최소중량이 900KG보다 클경우
                    indicator_arr[indicator_cnt] = Convert.ToInt32(lblWeight_value.Text.ToString());
                    if (indicator_arr.Max() - indicator_arr.Min() < 60)
                    {
                        if (indicator_arr.Min() > 800)
                        {
                            if (CardRead == false)
                            {
                                CardRead = true;
                                indicator_cnt = 0;
                            }
                        }
                        else CardRead = false;
                    }
                    else CardRead = false;

                    // 계량 완료 후 다음차량 진입 시간이 짧아 중량 안정화 사용 안함.
                    // 계량완료된 상태이고 현재/이전 중량 이 300KG보다 작고 이전 중량차이가 60KG 보다 작은경우, 차단기 동작 가능 상태로 설정
                    //계량 end 조건으로 추가
                    if ((indicator_stopper - indicator_arr[indicator_cnt]) < 60 && indicator_arr[indicator_cnt] < 300 && indicator_stopper < 300)
                    {
                        bStopper_Action = true;
                        CardRead = false;

                        //end_timer.Enabled = false;
                        //계량 end 조건으로 추가
                        Sencer_Action = false;
                        CardRead = false;
                        Prev_rfid = "";
                    }

                    indicator_stopper = indicator_arr[indicator_cnt];
                    indicator_cnt++;
                    if (indicator_cnt >= indicator_max) indicator_cnt = 0;                    
                }
                else
                {
                    indicator_stopper = 0;
                    indicator_cnt = 0;
                    indicator_arr[indicator_cnt] = 0;                    
                }     
            }
            catch (Exception ex)
            {
                logger.Error("메인타이머 ERROR:" + ex.Message.ToString().Trim());
                timer_main.Enabled = true;

                //err_cnt = err_cnt + 1;

                //if (err_cnt > 10)
                //{
                //    string processName = Process.GetCurrentProcess().ProcessName;
                //    Process.Start(Environment.CurrentDirectory + "\\Restart.exe", processName);
                //}
            }
            
            timer_main.Enabled = true;
        }
        #endregion

        #region 수기 등록 보이냐 안보이냐
        private void labelControl13_Click(object sender, EventArgs e)
        {

            //if (textEdit6.Visible == true)
            //{
            //    textEdit6.Visible = false;
            //    textEdit7.Visible = false;
            //    textEdit1.Visible = false;
            //    textEdit2.Visible = false;
            //    //btn_RFID_search.Visible = false;

            //}
            //else
            //{
            //    textEdit6.Visible = true;
            //    textEdit7.Visible = true;
            //    textEdit1.Visible = true;
            //    textEdit2.Visible = true;
            //    //btn_RFID_search.Visible = true;
            //}
        }
        #endregion

        #region RFID 조회 ( 임시 )
        private void btn_RFID_search_Click(object sender, EventArgs e)
        {                     
            
        }
        #endregion       

        #region 초기화
        private void txt_clear()
        {
            //용지 잔량 확인 
            pnt_size_state();

            textEdit1.Text = string.Empty;
            lblFirst_wght.Text = string.Empty;
            lblSecond_wght.Text = string.Empty;
            lblReal_wgt.Text = string.Empty;
            lblProd_wgt.Text = string.Empty;
            lblWeight_value.Text = "0";
            la_ment.Text = string.Empty;
            lblRfid_no.Text = string.Empty; // 카드
            labelControl2.Text = string.Empty; // 계량품목
            labelControl4.Text = string.Empty; // 차량
            labelControl17.Text = string.Empty; //계량
            labelControl18.Text = string.Empty; // 대표 자재 그룹 
            // 모든 변수 초기화  
            Gubun = "N";
            rfid_no = string.Empty;
            car_no = string.Empty;
            item_dae =  string.Empty;
            item_jung = string.Empty;
            item_so = string.Empty;
            wght_no = string.Empty;
            rfid_seq = string.Empty;
            limit_yn = string.Empty;    
            weight_fg = false; //1차, 2차 계량 저장 여부
            //fix_fg = false; // 공차 계량 저장 여부    

            //lblautoNum.Text = "";
            //indicator_status = 0;
            lb_max.Text = "";
            lb_min.Text = "";

            Prev_rfid = "";
            play60_chk = false;
            rfid_ready = false;
            CardRead = false;

            timer_main.Enabled = true; 
        }
        #endregion

        #region 프린트 잔량 확인
        private void pnt_size_state()
        {
        //    pnt_per = Convert.ToInt32( DB_Process.pnt_per(Weight_Area, "1"));
        //    if (pnt_per < 20)
        //    {
        //        labelControl22.Visible = true;
        //        pictureBox1.Visible = true;
        //    }
        //    else
        //    {
        //        labelControl22.Visible = false;
        //        pictureBox1.Visible = false;
        //    }
        }
        #endregion

        #region 음성 안내 버튼 
        private void button1_Click(object sender, EventArgs e)
        {
            //기다려 주세요 
            player32.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //RFID 카드를 대주세요 
            textEdit1.Text = "";
            //RFID 카드를 대주세요 메세지 
            player13.Play();
            la_ment.Text = "RFID 카드를 대주세요 ";

            rfid_ready = true;
            //indicator_status = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //출발해 주세요 
            player33.Play();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //상차지시가 없습니다
            player62.Play();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 배차지시 오류
            player63.Play(); 
        }
        #endregion 

        #region 다시 계량 
        private void btnReWeight_Click(object sender, EventArgs e)
        {
            end_timer.Enabled = false;
            txt_clear();

            Sencer_Action = false;
            CardRead = false;

            RFID_READY();
 
            Prev_rfid = "";
        }
        #endregion 

        #region RFID 처리함수
        public byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");
            byte[] comBuffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                try
                {
                    comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
                }
                catch (ArgumentOutOfRangeException argumentoutofrange)
                {
                    MessageBox.Show("잘못된 형식의 HEX값이 입력되었습니다." + argumentoutofrange);
                }
            }
            return comBuffer;
        }

        private void server_connect()
        {
            ServerIPAddress = IPAddress.Parse(HW_RFID);
            PORT = Convert.ToInt32(HW_RFID_PORT);

            if (SocketClient != null)
            {
                SocketClient.Close();
            }

            ServerIPEndPoint = new IPEndPoint(ServerIPAddress, PORT);
            SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                SocketClient.Connect(ServerIPEndPoint);

                if (SocketClient.Connected)
                {
                    ThreadClient = new System.Threading.Thread(new System.Threading.ThreadStart(RecieveFromServer));
                    ThreadClient.Start();

                    paRfid.BackColor = Color.Green; 
                }
            }
            catch (Exception ex)
            {
                logger.Error("소켓 에러 : " + ex.Message.ToString());
                server_close();
            }
        }

        private void insert_text(string insert_value)
        {            
            //insert_target.Invoke((MethodInvoker)delegate
            //{
                ReceveData1 = insert_value.ToUpper();

                ReceveData1 = ReceveData1.Substring(16);
                //logger.Error("insert_text : insert_value : " + insert_value);
                //logger.Error("insert_text : ReceveData1 : " + ReceveData1);

                if (Sencer_Action == false)
                {
                    if (ReceveData1[0] == 'E')
                    {
                        //logger.Error("RFID 카드값 자른거 E : " + ReceveData1);

                        if (ReceveData1.Length >= 17)
                        {
                            if (ReceveData1.Substring(0, 2) == "E2")
                            {
                                ReceveData1 = ReceveData1.Substring(0, 24);
                            }
                            else
                            {
                                ReceveData1 = ReceveData1.Substring(0, 16);
                            }

                            if (CardRead == true)
                            {
                                //if (outSensor.Tag.ToString() == "00")
                                //{
                                    if (Prev_rfid != ReceveData1)
                                    {
                                        //logger.Error("RFID 카드값 정상인식 : " + ReceveData1);

                                        textEdit1.Text = ReceveData1;
                                        Prev_rfid = ReceveData1;
                                        player20.PlaySync();

                                        CardRead = false;
                                        rfid_ready = true;

                                        btnRFID_Click(btnRFID, new EventArgs());                                        
                                    }
                                    else
                                    {
                                        CardRead = false;
                                        ReceveData1 = "";
                                        textEdit1.Text = "";
                                        logger.Error("RFID 중복 : " + ReceveData1);
                                    }
                                //}
                                //else
                                //{
                                //    ReceveData1 = "";
                                //}
                            }
                            else
                            {
                                ReceveData1 = "";
                            }
                        }
                    }
                    else if (ReceveData1[0] == 'D')
                    {
                        //logger.Error("RFID 카드값 자른거 D : " + ReceveData1);

                        if (ReceveData1.Length >= 24)
                        {
                            ReceveData1 = ReceveData1.Substring(0, 23);

                            if (Prev_rfid != ReceveData1)
                            {
                                //logger.Error("RFID 카드값 정상인식 : " + ReceveData1);

                                textEdit1.Text = ReceveData1;
                                Prev_rfid = ReceveData1;
                                player20.PlaySync();

                                CardRead = false;
                                rfid_ready = true;

                                btnRFID_Click(btnRFID, new EventArgs());
                            }
                            else
                            {
                                ReceveData1 = "";
                                textEdit1.Text = "";
                                logger.Error("RFID 중복 : " + ReceveData1);
                            }
                        }
                    }
                    else
                    {
                        ReceveData1 = "";
                        logger.Error("insert_text : (이상한 RFID 수신) insert_value : " + insert_value);
                    }
                }
                else
                {
                    ReceveData1 = "";
                    logger.Error("insert_text : (계량준비 상태가 아님) insert_value : " + insert_value);
                }
            //});
        }

        #region RFID 스타트 리드
        private void RFID_READY()
        {
            if (SocketClient.Connected == true)
            {
                SocketClient.Send(HexToByte("7E01000003011000117F"));
                logger.Info(" RFID 전송 : 7E01000003011000117F");                

//                SocketClient.Send(HexToByte("7E01010004A3011000B27F"));
//                logger.Info(" RFID 전송 : 7E01010004A3011000B27F");
/*
                if (Weight_Area == "P05" || Weight_Area == "P04" || Weight_Area == "P03" || Weight_Area == "P06")
                {
                    SocketClient.Send(HexToByte("7E01000003011000117F"));
                    logger.Info(" RFID 전송 : 7E01000003011000117F");
                }
                else
                {
                    SocketClient.Send(HexToByte("7E01010004A3011000B27F"));
                    //                             7E01010004A3002000837F
                    logger.Info(" RFID 전송 : 7E01010004A3011000B27F");
                }
 */
            }
            else
            {
                server_close();
                Thread.Sleep(500);
                server_connect();
            }
        }
        #endregion

        //메세지 수신
        private void RecieveFromServer()
        {
            Thread.Sleep(2000);
            RFID_READY();

            while (true)
            {
                try
                {
                    if (!SocketClient.Connected)
                    {
                        logger.Error("RecieveFromServer(!SocketClient.Connected) : ");
                        server_close();
                        server_connect();
                        //RFID_READY();
                        return;
                    }

                    Byte[] ReceiveByte = new Byte[50];

                    int nValue = SocketClient.Receive(ReceiveByte, ReceiveByte.Length, 0);
                    
                    if (nValue > 0)
                    {
                        String tmp = "";

                        foreach (byte val in ReceiveByte)
                        {
                            tmp = tmp + String.Format("{0:x2}", val);
                        }

                        if (tmp.Length > 24 && Sencer_Action == false)
                        {
                            //logger.Error("RFID 카드값(최초리딩) : " + tmp);
                            //logger.Error("RFID 카드값(+18) : " + tmp.Substring(18));

                            if (tmp.Substring(18, 20) != "00000000000000000000")
                            {
                                logger.Error("RFID 카드값(최초리딩 != 00000000000000000000) : " + tmp);
                                //logger.Error("RFID 카드값(+18 != 00000000000000000000) : " + tmp.Substring(18));

                                //160908 hoseong.hwang 
                                //wmcopyData = new Win32API.COPYDATASTRUCT();
                                wmcopydataRFID.dwData = (IntPtr)(Win32API.WM_USER_RFID_RECV);
                                wmcopydataRFID.cbData = (uint)tmp.Length * sizeof(char);
                                wmcopydataRFID.lpData = tmp;
                                //Int32 iHaldle = GetHandle;

                                if (iFormHandle > 0)
                                {
                                    Win32API.SendMessage((IntPtr)iFormHandle, Win32API.WM_COPYDATA, IntPtr.Zero, ref wmcopydataRFID);
                                }
                            }

                            //insert_text(textEdit1, tmp);
                        }

                        //ReceiveData = System.Text.Encoding.Unicode.GetString(ReceiveByte);
                        //Console.WriteLine(ReceiveData);
                        //MessageDisplay(ReceiveData);
                        //MessageBox.Show(ReceiveData);
                    }

                    Thread.Sleep(500);
  
                    if (bClose == true)
                    {
                        break;
                    }
                }
                catch(Exception ex)
                {
                    logger.Error("RFID 수신에러 : " + ex.Message.ToString());  
                }
            }
            //ThreadClient.Abort();
        }
/*
        private delegate int GetHandleDelegate();
        public int GetHandle
        {
            get
            {
                if (this.InvokeRequired)
                {
                    return (int)this.Invoke((GetHandleDelegate)delegate
                    {
                        return this.Handle.ToInt32();
                    });
                }
                return this.Handle.ToInt32();
            }
        } 
*/
        protected override void WndProc(ref Message wMessage)
        {
            switch (wMessage.Msg)
            {
                case Win32API.WM_COPYDATA:
                    Win32API.COPYDATASTRUCT lParam1 = (Win32API.COPYDATASTRUCT)Marshal.PtrToStructure(wMessage.LParam, typeof(Win32API.COPYDATASTRUCT));
                    Win32API.COPYDATASTRUCT lParam2 = new Win32API.COPYDATASTRUCT();

                    logger.Error("WndProc WM_COPYDATA Start!!");

                    lParam2 = (Win32API.COPYDATASTRUCT)wMessage.GetLParam(lParam2.GetType());
                    if (Win32API.WM_USER_RFID_RECV == (int)lParam2.dwData)
                    {
                        logger.Error("WndProc WM_COPYDATA Win32API.WM_USER_RFID_RECV ==");
                        insert_text(lParam2.lpData);
                    }

                    logger.Error("WndProc WM_COPYDATA End!!");                    

                    break;
                default:
                    break;
            }
            base.WndProc(ref wMessage);
        }

        //연결끊기
        private void server_close()
        {
            if (SocketClient != null)
            {
                SocketClient.Close();
            }
            if (ThreadClient != null)
            {
                if (ThreadClient.IsAlive)
                {
                    ThreadClient.Abort();
                }
            }
            //SocketClient.Disconnect(true);
        }
        #endregion 

        #region 3문 LPR 처리
        private void server_connect_LPR()
        {
            ServerIPAddress = IPAddress.Parse("10.10.94.197");
            PORT = 31003;

            if (SocketClient1 != null)
            {
                SocketClient1.Close();
            }

            ServerIPEndPoint = new IPEndPoint(ServerIPAddress, PORT);
            SocketClient1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                SocketClient1.Connect(ServerIPEndPoint);

                if (SocketClient1.Connected)
                {
                    ThreadClient1 = new System.Threading.Thread(new System.Threading.ThreadStart(RecieveFromServer1));
                    ThreadClient1.Start();
                }
            }
            catch (Exception ex)
            {
                logger.Error("소켓 에러 : " + ex.Message.ToString());
                server_close1();
            }
        }

        //메세지 수신
        private void RecieveFromServer1()
        {
            while (true)
            {
                try
                {
                    if (!SocketClient1.Connected)
                    {
                        server_close1();
                        server_connect_LPR();
                    }

                    Byte[] ReceiveByte = new Byte[50];

                    int nValue = SocketClient1.Receive(ReceiveByte, ReceiveByte.Length, 0);

                    if (nValue > 0)
                    {
                        String ReceiveData = System.Text.Encoding.Unicode.GetString(ReceiveByte);

                        logger.Error("3문 LPR 수신 : " + ReceiveData);

                        if (ReceiveData.Contains("F0001"))
                        {
                            if (ReceiveData.Length - 15 - 46 > 0)
                            {
                                SetText1(strReceiveMsg);

                                //lblautoNum.Text = ReceiveData.Substring(ReceiveData.IndexOf("F0001") + 5, ReceiveData.Length - 15 - 46).Trim();
                                //AutoNum = ReceiveData.Substring(ReceiveData.IndexOf("F0001") + 5, ReceiveData.Length - 15 - 46).Trim();

                                //lblautoNum.Tag = ReceiveData.Substring(ReceiveData.IndexOf(DateTime.Now.ToString("yyyyMMdd")), 24).Replace(ETX, ' ').Trim();
                            }
                        }

                        //Console.WriteLine(ReceiveData);
                        //MessageDisplay(ReceiveData);
                        //MessageBox.Show(ReceiveData);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("LPR 수신에러 : " + ex.Message.ToString());
                }
            }
            //ThreadClient.Abort();
        }

        //연결끊기
        private void server_close1()
        {
            if (SocketClient1 != null)
            {
                SocketClient1.Close();
            }
            if (ThreadClient1 != null)
            {
                if (ThreadClient1.IsAlive)
                {
                    ThreadClient1.Abort();
                }
            }
            //SocketClient.Disconnect(true);
        }
        #endregion

        #region LPR처리함수
        private void Receive()
        {
            ClientSocket.BeginReceive(buff, 0, 4096, SocketFlags.None, new AsyncCallback(ReceiveCallBack), buff);
        }

        private void Disconnet()
        {
            ClientSocket.Close();
            ClientSocket = null;
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                byte[] Ibuff = (byte[])ar.AsyncState;
                int recv = ClientSocket.EndReceive(ar);

                if (recv == 0)
                {
                    Disconnet();
                    return;
                }
                else
                {
                    strReceiveMsg = Encoding.Default.GetString(Ibuff, 0, recv);

                    logger.Info("LPR 정보 수신 : " + strReceiveMsg);

                    if (strReceiveMsg.Contains("F0001"))
                    {
                        if (strReceiveMsg.Length - 15 - 46 > 0)
                        {
                            SetText1(strReceiveMsg);
                        }
                    }

                    if (bClose == false)
                    {
                        Receive();
                    }
                }
            }
            catch (Exception e)
            {
                Receive();
                logger.Error("LPR 리시브 에러 : " + e.Message.ToString());     
            }
        }

        private void SetText1(string text)
        {
            if (this.lblautoNum.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText1);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                lblautoNum.Text = text.Substring(text.IndexOf("F0001") + 5, text.Length - 15 - 46).Trim();
                AutoNum = text.Substring(text.IndexOf("F0001") + 5, text.Length - 15 - 46).Trim();

                lblautoNum.Tag = text.Substring(text.IndexOf(DateTime.Now.ToString("yyyyMMdd")), 24).Replace(ETX, ' ').Trim();
            }
        }

        public void Thread_Run()
        {

            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, 4100));
            ServerSocket.Listen(3);

            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = host.AddressList[0];            

            while (bSocketEnd == false)
            {
                try
                {
                    Socket socClient = (Socket)ServerSocket.Accept();

                    ClientSocket = socClient;
                    Receive();
                }
                catch { }
            }
        }

        #endregion

        #region 종료처리
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort[0] != null)
            {
                serialPort[0].DiscardInBuffer();
                serialPort[0].DiscardOutBuffer();

                if (serialPort[0].IsOpen)
                {
                    serialPort[0].Close();
                }
            }

            if (serialPort[1] != null)
            {
                serialPort[1].DiscardInBuffer();
                serialPort[1].DiscardOutBuffer();

                if (serialPort[1].IsOpen)
                {
                    serialPort[1].Close();
                }
            }

            server_close();

            if (ClientSocket != null)
            {
                ClientSocket.Close();
            }
            if (ThreadClient != null)
            {
                if (ThreadClient.IsAlive)
                {
                    ThreadClient.Abort();
                }
            }

            //if (mdb.IsOpenDB())
            //{
            //    mdb.DBClose();
            //}
        }
        #endregion

        #region 전광판 처리
        private void dp_show(String led1, String led2)
        {
            WritePrivateProfileString("DATA", "DISPLAY1", led1, "C:\\LED.INI");
            WritePrivateProfileString("DATA", "DISPLAY2", led2, "C:\\LED.INI");
        }
        #endregion

        #region 수동계량
        private void btnRFID_Click(object sender, EventArgs e)
        {
            ////동적 판넬 초기화
            //this.panelControl1.Controls.Clear();

            //lblWeight_value.Text = textEdit6.Text;

            logger.Info("lblWeight_value.Text : " + lblWeight_value.Text);
            logger.Info("textEdit6.Text : " + textEdit6.Text);

            if (textEdit6.Text != "")
            {
                string Str = textEdit6.Text;
                int Num;
                bool isNum = int.TryParse(Str, out Num);
                if (isNum)
                {
                    lblWeight_value.Text = Convert.ToInt32(textEdit6.Text).ToString();
                }
            }

            if (lblWeight_value.Text != "0")
            {
                if (lblWeight_value.ForeColor == Color.Yellow)
                {
                    rfid_no = textEdit1.Text;
                    //계량 시작 잼
                    Sencer_Action = true;

                    logger.Info("begin Main_Load()");
                    Main_Load();
                    logger.Info("end Main_Load()");                    
                }
            }

            //txt_clear();
        }
        #endregion

        #region 메세지폼
        private DialogResult MessageOpen(String message, String title)
        {
            frmMessage frm = new frmMessage();
            frm.Location = new Point(this.Location.X+100, this.Location.Y + 560);
            frm.Text = title;
            frm.Message = message;
            return frm.ShowDialog();
        }
        #endregion

        #region 세인율 반영
        private void btnSein_Click(object sender, EventArgs e)
        {
            PAN1 = spinEdit1.Value.ToString();
            PAN2 = spinEdit2.Value.ToString();
            //BON1 = spinEdit3.Value.ToString();
            //BON2 = spinEdit4.Value.ToString();
            BEM1 = spinEdit5.Value.ToString();
            BEM2 = spinEdit6.Value.ToString();

            string clientFolder = Application.StartupPath + @"\";
            XmlDocument clientDoc = new XmlDocument();
            clientDoc.Load(clientFolder + "UpdateFiles.xml");

            XmlElement clientDocRoot = clientDoc.DocumentElement;         

            // 세인율 계산 값 가져오기
            XmlNode nodePAN1 = null;
            nodePAN1 = clientDocRoot.SelectSingleNode("descendant::PAN1");
            nodePAN1.Attributes["value"].InnerText = PAN1;

            XmlNode nodePAN2 = null;
            nodePAN2 = clientDocRoot.SelectSingleNode("descendant::PAN2");
            nodePAN2.Attributes["value"].InnerText = PAN2;

            //XmlNode nodeBON1 = null;
            //nodeBON1 = clientDocRoot.SelectSingleNode("descendant::BON1");
            //nodeBON1.Attributes["value"].InnerText = BON1;

            //XmlNode nodeBON2 = null;
            //nodeBON2 = clientDocRoot.SelectSingleNode("descendant::BON2");
            //nodeBON2.Attributes["value"].InnerText = BON2;

            XmlNode nodeBEM1 = null;
            nodeBEM1 = clientDocRoot.SelectSingleNode("descendant::BEM1");
            nodeBEM1.Attributes["value"].InnerText = BEM1;

            XmlNode nodeBEM2 = null;
            nodeBEM2 = clientDocRoot.SelectSingleNode("descendant::BEM2");
            nodeBEM2.Attributes["value"].InnerText = BEM2;

            clientDoc.Save(clientFolder + "UpdateFiles.xml"); 
        }
        #endregion

        #region 식당DB 정보등록
        private void FOOD_SAVE(String WGHT_NO, String VEHL_NO, String CUST_NM)
        {
            String fdate = DateTime.Now.ToString("yyyyMMdd");

            String Query = " SELECT ISNULL(max(ilno),0) + 1 AS ilno FROM ONE_FOOD_TB WHERE fdate = '" + fdate + "' AND rest_cd = 'R4' ";
            String ilno;
            DataSet ds = mdb.ExecuteDataSet(Query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ilno = ds.Tables[0].Rows[0]["ilno"].ToString();

                Query = " Insert Into ONE_FOOD_TB "
                    + " (fdate,         rest_cd,     ilno,        snno,        empno,"
                    + "  rfdate,    rrest_cd,       rilno,         time_cd,        cust_cd, "
                    + "  plan_qnty,  use_qnty,       id_barcode,         jumin_no,         jumin_nm,"
                    + "  carnum,        door_cust_nm,        sdate,          edate)  "
                    + " Values ('" + fdate + "', "
                    + "         'R4', " + ilno + ",1,'DOOR3', '" + fdate + "', 'R4' ," + ilno + " , 'T00' ,'' ,1,NULL, "
                    + "         '" + WGHT_NO + "','','', "                          // 바코드정보
                    + "         '" + VEHL_NO + "', "                                // 차량번호
                    + "         '" + CUST_NM + "', "                                // 업체명
                    + "          getdate() , "                                      // 날짜
                    + "         '00000000') ";
                mdb.ExecuteQuery(Query);
            }
        }
        #endregion

        #region 차단기 제어
        static bool BioFailed(ErrorCode err)
        {
            return err < ErrorCode.Success && err >= ErrorCode.ErrorHandleNotValid;
        }

        private void GateControl(String data)
        {
            try
            {
                int startPort = 0;
                int portCount = 1;

                ErrorCode errorCode = ErrorCode.Success;

                if (data == "01")
                {
                    if (BarricateSensor_on.BackColor != System.Drawing.Color.White)
                    {
                        if (BarricateSensor_on.BackColor == System.Drawing.Color.Lime)
                        {
                            return;
                        }
                    }

                    BarricateSensor_off.BackColor = System.Drawing.Color.Gray;
                    BarricateSensor_on.BackColor = System.Drawing.Color.Lime;

                    bStopper_Status = false;
                    logger.Info("차단기 열기 명령-1");

                    if (Weight_Area == "P08")
                    {
                        SendLED(data);
                        //WritePrivateProfileString("DATA", "DISPLAY1", data, "C:\\LED.INI");
                    }
                }
                else
                {
                    if (BarricateSensor_off.BackColor != System.Drawing.Color.White)
                    {
                        if (BarricateSensor_off.BackColor == System.Drawing.Color.Lime)
                        {
                            return;
                        }
                    }

                    BarricateSensor_off.BackColor = System.Drawing.Color.Lime;
                    BarricateSensor_on.BackColor = System.Drawing.Color.Gray;

                    bStopper_Status = true;
                    logger.Info("차단기 닫기 명령-1");

                    if (Weight_Area == "P08")
                    {
                        SendLED(data);
                        //WritePrivateProfileString("DATA", "DISPLAY1", data, "C:\\LED.INI");
                    }
                }
                logger.Info("bStopper_Action.ToString() : " + bStopper_Action.ToString());

                // 구형 IO 카드들
                // 사용하는 계량대
                if (Weight_Area == "P03" || Weight_Area == "P04" || Weight_Area == "P05")
                {
                    if (data == "01")   //차단기 열기 명령
                    {
                        AdvantechAPI.Digital_WriteByteToPort(DeviceHandle, 0, 1);
                        bStopper_Action = false;         // 열린상태는 프로그램상으로 동작가능 상태 후 닫힘설정
                        logger.Info("차단기 열기 명령-2");
                    }
                    else if (data == "02" && bStopper_Action == true)       //차단기 닫기 명령 :  Stopper 동작가능 상태에서만 닫힘명령 가능
                    {
                        AdvantechAPI.Digital_WriteByteToPort(DeviceHandle, 0, 2);
                        bStopper_Action = true;         // 닫힌상태는 항상 프로그램상 동작가능 설정
                        logger.Info("차단기 닫기 명령-2");
                    }
                    Thread.Sleep(1000);
                    AdvantechAPI.Digital_WriteByteToPort(DeviceHandle, 0, 0);

                    paSencer.BackColor = Color.Green; 
                }
                else
                {
                    InstantDoCtrl instantDoCtrl = new InstantDoCtrl();

                    try
                    {
                        instantDoCtrl.SelectedDevice = new DeviceInformation(0);
                        byte[] bufferForWriting = new byte[64];

                        if (data == "01")
                        {
                            for (int i = 0; i < portCount; ++i)
                            {
                                bufferForWriting[i] = byte.Parse(data.Contains("0x") ? data.Remove(0, 2) : data, System.Globalization.NumberStyles.HexNumber);
                            }
                            errorCode = instantDoCtrl.Write(startPort, portCount, bufferForWriting);
                            if (BioFailed(errorCode))
                            {
                                throw new Exception();
                            }

                            Thread.Sleep(1000);
                            data = "00";

                            for (int i = 0; i < portCount; ++i)
                            {
                                bufferForWriting[i] = byte.Parse(data.Contains("0x") ? data.Remove(0, 2) : data, System.Globalization.NumberStyles.HexNumber);
                            }
                            errorCode = instantDoCtrl.Write(startPort, portCount, bufferForWriting);
                            if (BioFailed(errorCode))
                            {
                                throw new Exception();
                            }

                            logger.Info("차단기 열기 명령-3");
                            bStopper_Action = false;         // 열린상태는 프로그램상으로 동작가능 상태 후 닫힘설정
                        }
                        else if (data == "02" && bStopper_Action == true)       // Stopper 동작가능 상태에서만 닫힘명령 가능
                        {
                            data = "03";
                            for (int i = 0; i < portCount; ++i)
                            {
                                bufferForWriting[i] = byte.Parse(data.Contains("0x") ? data.Remove(0, 2) : data, System.Globalization.NumberStyles.HexNumber);
                            }
                            errorCode = instantDoCtrl.Write(startPort, portCount, bufferForWriting);
                            if (BioFailed(errorCode))
                            {
                                throw new Exception();
                            }

                            Thread.Sleep(1000);
                            data = "02";

                            for (int i = 0; i < portCount; ++i)
                            {
                                bufferForWriting[i] = byte.Parse(data.Contains("0x") ? data.Remove(0, 2) : data, System.Globalization.NumberStyles.HexNumber);
                            }
                            errorCode = instantDoCtrl.Write(startPort, portCount, bufferForWriting);
                            if (BioFailed(errorCode))
                            {
                                throw new Exception();
                            }

                            bStopper_Action = true;         // 닫힌상태는 항상 프로그램상 동작가능 설정
                            logger.Info("차단기 닫기 명령-3");                            
                        }
                        instantDoCtrl.Dispose();

                        paSencer.BackColor = Color.Green; 
                    }
                    catch (Exception e)
                    {
                        paSencer.BackColor = Color.Red; 

                        // Something is wrong
                        string errStr = BioFailed(errorCode) ? " Some error occurred. And the last error code is " + errorCode.ToString()
                                                                   : e.Message;
                        logger.Error("차단기 에러 : " + errStr);
                    }
                    finally
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("차단기 제어 에러 : " + ex.Message.ToString());    
            }
        }
        #endregion

        #region 차량번호로 RFID 조회
        private void btnCardSearch_Click(object sender, EventArgs e)
        {
            if (textEdit4.Text != "")
            {
                DataTable tmp = DB_Process.SearchRFID(textEdit4.Text);

                if (tmp.Rows.Count > 0)
                {
                    textEdit1.Text = tmp.Rows[0]["RFID_NO"].ToString();  
                }
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

                serialPort[1].Write("^XA^LL1470^LH10,70^PRB^FS");
                serialPort[1].Write("^BY2,3.0^FS");
                serialPort[1].Write("^SEE:UHANGUL.DAT^FS");
                serialPort[1].Write("^CW1,E:KFONT3.FNT^FS");

                serialPort[1].Write("^FO200,20^A1N,50,60^FD계량표 (" + dt.Rows[0]["WEIGHT_STATE"].ToString() + ")^FS");

                serialPort[1].Write("^FO70,200^A1N,50,40^FD품목   : " + dt.Rows[0]["ITEM_DAE_NM"].ToString() + "^FS");

                serialPort[1].Write("^FO70,300^A1N,50,40^FD계량번호 : " + dt.Rows[0]["WGHT_NO"].ToString() + "^FS");
                serialPort[1].Write("^FO70,360^A1N,50,40^FDRFID : " + dt.Rows[0]["RFID_NO"].ToString() + "^FS");
                serialPort[1].Write("^FO70,410^A1N,50,40^FD중분류   : " + dt.Rows[0]["ITEM_JUNG_NM"].ToString() + "^FS");
                serialPort[1].Write("^FO70,460^A1N,50,40^FD소분류   : " + dt.Rows[0]["ITEM_SO_NM"].ToString() + "^FS");
                serialPort[1].Write("^FO70,510^A1N,50,40^FD업체명   : " + dt.Rows[0]["VENDER_NAME"].ToString() + "/" + dt.Rows[0]["REAL_VENDER_NAME"].ToString() + "^FS");

                serialPort[1].Write("^FO70,560^A1N,50,40^FD1차일자 : " + dt.Rows[0]["LOAD_DATE"].ToString() + "^FS");
                serialPort[1].Write("^FO70,610^A1N,50,40^FD1차중량 : " + dt.Rows[0]["LOAD_WEIGHT"].ToString() + " KG [" + dt.Rows[0]["LOAD_STATE"].ToString() + "] ^FS");
                serialPort[1].Write("^FO70,660^A1N,50,40^FD2차일자 : " + dt.Rows[0]["DOWN_DATE"].ToString() + "^FS");
                serialPort[1].Write("^FO70,710^A1N,50,40^FD2차중량 : " + dt.Rows[0]["DOWN_WEIGHT"].ToString() + " KG [" + dt.Rows[0]["DOWN_STATE"].ToString() + "] ^FS");

                serialPort[1].Write("^FO70,760^A1N,50,40^FD실중량 : " + dt.Rows[0]["REAL_WGHT"].ToString() + "^FS");
                serialPort[1].Write("^FO70,810^A1N,50,40^FD감량 : " + "^FS");
                serialPort[1].Write("^FO120,860^A1N,60,80^FD동국제강 포항 제강소^FS");

                serialPort[1].Write("^PQ1^FS");
                serialPort[1].Write("^XZ");
                serialPort[1].Write("   ");

                serialPort[1].Close();
            }
            catch (Exception ex)
            {
                logger.Error("국내고철 확인출 출력 에러 : " + ex.Message.ToString());    
            }
        }
        #endregion

        #region 국고계량대 확인증 출력
        private void PirntFoodPaper(String WGHT_NO, String CNT,  String VEHL_NO, String CUST_CD, String LOAD_WEIGHT, String LOAD_DATE, String IMG_NAME)
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

                serialPort[1].Write("^XA^LL1470^LH10,70^PRB^FS");
                serialPort[1].Write("^BY2,3.0^FS");
                serialPort[1].Write("^SEE:UHANGUL.DAT^FS");
                serialPort[1].Write("^CW1,E:KFONT3.FNT^FS");
                serialPort[1].Write("^FO220,70^A1N,80,100^FD입 고 확 인 증^FS");

                //'계량번호 뒷자리만
                serialPort[1].Write("^FO650,330^A0N,180,180^FD" + CNT + "^FS");

                //'차량번호
                serialPort[1].Write("^FO80,650^A1N,50,50^FD차량번호 : " + VEHL_NO + "^FS");

                //'업체명
                serialPort[1].Write("^FO80,750^A1N,50,50^FD업 체 명 : " + CUST_CD + "^FS");
                //'상차계량
                serialPort[1].Write("^FO80,850^A1N,50,50^FD상차계량 : " + LOAD_WEIGHT + "Kg^FS");
                //'계량시간
                serialPort[1].Write("^FO80,950^A1N,50,50^FD계량시간 : " + LOAD_DATE + "^FS");

                serialPort[1].Write("^FO150,1100^A1N,60,80^FD동국제강 포항 제강소^FS");

                //'바코드 코드128
                serialPort[1].Write("^FO220,1250^BY4^BCN,100,Y,N,Y^FD" + WGHT_NO + "^FS");

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

                logger.Info(" 이미지 출력 : " + Img_filepath); 

                Bitmap image1 = new Bitmap(Img_filepath, true);
                String str = ConvertImageToCode(image1);

                String t = ((image1.Size.Width / 8 + ((image1.Size.Width % 8 == 0) ? 0 : 1)) * image1.Size.Height).ToString();
                String w = (image1.Size.Width / 8 + ((image1.Size.Width % 8 == 0) ? 0 : 1)).ToString();

                serialPort[1].Write("^FO150,300");
                serialPort[1].Write(string.Format("~DGR:imgName.GRF,{0},{1},{2}", t, w, str));
                serialPort[1].Write("^XGR:imgName.GRF,2,2^FS");

                serialPort[1].Write("^PQ1^FS");
                serialPort[1].Write("^XZ");
                serialPort[1].Write("   ");
  
                serialPort[1].Close();
            }
            catch (Exception ex)
            {
                logger.Error("국내고철 확인출 출력 에러 : " + ex.Message.ToString());    
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

        #region 통과버튼
        private void button7_Click(object sender, EventArgs e)
        {
            bStopper_Action = true;     // 차단기 동작 가능케 무조건 true 설정
            GateControl("01");
        }
        #endregion

        #region 진입버튼
        private void button6_Click(object sender, EventArgs e)
        {
            bStopper_Action = true;      // 차단기 동작 가능케 무조건 true 설정
            GateControl("02");
        }
        #endregion

        #region 재발행버튼
        private void button9_Click(object sender, EventArgs e)
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            Process.Start(Environment.CurrentDirectory + "\\DK_WEIGHT.RESULT_INFO.exe", processName);
        }
        #endregion

        private void end_timer_Tick(object sender, EventArgs e)
        {
            end_timer.Enabled = false;
            Sencer_Action = false;
            CardRead = false;
            Prev_rfid = "";

            logger.Info("end end_timer_Tick() : 카드정보가 없는경우 수행");
        }

        #region 프로그램 종료확인
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("프로그램을 종료하시겠습니까?", "종료 확인 ", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                // 종료 취소
                e.Cancel = true;
            }
            else
            {
                bSocketEnd = true;
                bClose = true;

                Thread.Sleep(500); 

                Application.ExitThread();
                Application.Exit(); 
            }
        }
        #endregion

        #region 프로그램 업데이트
        private void Check_Update()
        {
            bool Closes = false;
            int thisID = System.Diagnostics.Process.GetCurrentProcess().Id;
            System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName(EXE_FILE_NAME);

            if (p.Length > 1)
            {
                MessageBox.Show("[" + EXE_FILE_NAME + "] 프로그램이 이미 실행중입니다.\n 현재 프로그램을 종료합니다.", "프로그램 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                for (int i = 0; i < p.Length; i++)
                {

                    if (p[i].Id == thisID)
                    {
                        p[i].Kill(); ; //똑같은 넘이 있다면!
                        // 프로세스 강제 종료
                        Closes = true;
                    }
                }

            }
            if (!Closes)
            {
                string clientFolder = Application.StartupPath + @"\";
                XmlDocument clientDoc = new XmlDocument();
                clientDoc.Load(clientFolder + UpdateManager.UPDATE_LIST_FILE_NAME);
                UpdateManager.Update(clientDoc);
            }
        }
        #endregion

        private void timer_indicator_Tick(object sender, EventArgs e)
        {
            timer_indicator.Enabled = false;

            try
            {
                if(serialPort[0].IsOpen)
                {
                    /*
                        //Idicator로 "CLEAR" 명령을 내보낸다.  
                        byte[] outputCLEAR = Encoding.UTF8.GetBytes("CLEAR" + (char)13 + (char)10);
                        serialPort[0].Write(outputCLEAR, 0, outputCLEAR.Length);

                        //안정적인 Data 송신을 위해 0.1초간 Delay Time을 갖는다.
                        Thread.Sleep (100);

                        //Idicator로 "READ" 명령을 내보낸다.
                        byte[] outputREAD = Encoding.UTF8.GetBytes("READ" + (char)13 + (char)10);
                        serialPort[0].Write(outputREAD, 0, outputREAD.Length);
                    */
                    //Idicator로 데이터 읽기
                    string sRead = serialPort[0].ReadExisting();
                    serialPort[0].DiscardInBuffer();

                    // logger.Info("timer_indicator_Tick : serialPort[0] : " + sRead);

                    //숫자부분 짜르기
                    int iFr = 0;
                    int iTo = 0;

                    sRead = sRead.ToUpper();    // 전체 대문자로 변경
                    iFr = sRead.IndexOf("+");
                    if (iFr >= 0)
                    {
                        iTo = sRead.IndexOf("KG", iFr);
                    }
                    
                    if(iFr < 0 || iTo < 0)
                    {
                        iFr = sRead.IndexOf("-");
                        iTo = sRead.IndexOf("KG", iFr);
                    }

                    if (iFr < 0 || iTo - iFr <= 0)
                    {
                        logger.Info("ERR!! timer_indicator_Tick :sRead.Substring : iFr : " + iFr.ToString() + " iTo : " + iTo.ToString() + " sRead : " + sRead);
                    }

                    string sNumber = sRead.Substring(iFr, iTo - iFr).Trim().Replace(" ", "");

                    textEdit6.Text = sNumber;

                    if (sNumber.Length >= 6 && sNumber.Length <= 10)    // 부호(1) + 숫자(5) ~ 숫자(9), 일반적으로는 숫자(6) 자리임
                    {
                        int Num;
                        bool isNum = int.TryParse(sNumber, out Num);
                        if (isNum)
                        {
                            lblWeight_value.Text = Convert.ToInt32(sNumber).ToString();
                            lblWeight_value.ForeColor = Color.Yellow;
                        }
                        else
                        {
                            lblWeight_value.Text = "-1";
                            lblWeight_value.ForeColor = Color.Red;
                            paIndg.BackColor = Color.Red;

                            logger.Info("ERR!! timer_indicator_Tick : sRead -1 : " + sRead);
                            logger.Info("ERR!! timer_indicator_Tick : sNumber -1 : " + sNumber);
                        }

                        //if (Num > 0)
                        //{                            
                        //    logger.Info("timer_indicator_Tick : sRead > 0 : " + sRead);
                        //    logger.Info("timer_indicator_Tick : sNumber > 0 : " + sNumber);
                        //    logger.Info("timer_indicator_Tick : 숫자 > 0 : " + Num.ToString());
                        //}
                    }
                    else
                    {
                        lblWeight_value.Text = "-6";
                        lblWeight_value.ForeColor = Color.Red;
                        paIndg.BackColor = Color.Red;

                        //logger.Info("ERR!! timer_indicator_Tick : sRead.length != 7 : " + sRead);
                        //logger.Info("ERR!! timer_indicator_Tick : sNumber.length != 7 : " + sNumber);                        
                    }                
                }
            }
            catch (Exception ex)
            {
                paIndg.BackColor = Color.Red;

                serialPort[0].DiscardInBuffer();

                lblWeight_value.Text = "-9999";
                lblWeight_value.ForeColor = Color.Red;
                logger.Error("ERR!! timer_indicator_Tick : 인디게이터 에러 : " + ex.Message.ToString());

                Iny_cnt = Iny_cnt + 1;

                if (Iny_cnt > 10)
                {
                    Iny_cnt = 0;
                    SerialPortOpen(serialPort[0]);
                }
            }

            timer_indicator.Enabled = true;
        }

        public string str2hex(string strData)
        {
            string resultHex = string.Empty;
            byte[] arr_byteStr = Encoding.Default.GetBytes(strData);

            foreach (byte byteStr in arr_byteStr)
                resultHex += string.Format("{0:X2}", byteStr);

            return resultHex;
        }

        private void SendLED(String Data)
        {
            try
            {
                IPEndPoint ServerIPEndPoint;

                ServerIPEndPoint = new IPEndPoint(IPAddress.Parse("10.10.94.190"), 4300);
                //ServerIPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4300);
                SocketLED = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //SocketClient.Blocking = false;
                SocketLED.Connect(ServerIPEndPoint);

                if (SocketClient.Connected)
                {
                    long i = 0;

                    Byte[] buf = new Byte[1063];

                    buf[0] = STX1;
                    buf[1] = 4;                        //Length >> 8
                    buf[2] = 0x27;                        //Length 8bit
                    buf[3] = 0x30;                     //Command -> 방데이타전송
                    buf[4] = 0;                        //방번호  ( 0~ 9)
                    buf[5] = 0;                        //1줄문장
                    buf[6] = 16;
                    buf[7] = 01;
                    buf[8] = 15;
                    buf[9] = 0;

                    for (i = 10; i <= 36; i++)
                    {
                        buf[i] = 0; //사용안함
                    }

                    Byte[] SendBuffer;

                    if (Data == " 01")
                    {
                        SendBuffer = HexToByte(str2hex("진입"));

                        for (i = 0; i <= SendBuffer.Length - 1; i++)
                        {
                            buf[i + 37] = SendBuffer[i];
                            buf[i + 293] = 64;
                        }
                        buf[1061] = 202;
                        buf[1062] = ETX1;
                    }
                    else
                    {
                        SendBuffer = HexToByte(str2hex("대기"));

                        for (i = 0; i <= SendBuffer.Length - 1; i++)
                        {
                            buf[i + 37] = SendBuffer[i];
                            buf[i + 293] = 0;
                        }
                        buf[1061] = 175;
                        buf[1062] = ETX1;
                    }                    

                    for (int j = 0; j <= buf.Length - 1; j++)
                    {
                        byte[] send_data = new byte[1];
                        send_data[0] = buf[j];
                        int ret = SocketClient.Send(send_data, 0, 1, 0);
                        Thread.Sleep(1);
                    }
                }               
                
                SocketLED.Close();
            }
            catch (Exception ex)
            {
                logger.Error("전광판 전송 에러 : " + ex.Message.ToString());
            }
        }
    }    
}
