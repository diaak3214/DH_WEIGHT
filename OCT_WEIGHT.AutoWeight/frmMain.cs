using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.IO.Ports;
using System.Threading;
using System.Media;
using Automation.BDaq;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors;
using nesslab.reader.api;
using OCT_WEIGHT.AutoWeight.Report;
using ErrorCode = Automation.BDaq.ErrorCode;
using System.Collections.Generic;
//카메라
using AxAXISMEDIACONTROLLib;
using System.Runtime.Serialization.Formatters.Binary;
//SMS전송을 위해 SQL연결(2020-02-20 한민호)
using System.Data.SqlClient;

namespace OCT_WEIGHT.AutoWeight
{
    public partial class frmMain : Form
    {
        protected static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///  리더객체
        /// </summary>
        private Reader reader;
        
        #region DB 접속 정보

        private static String CS_MES = "Data Source=MES_TNS;User Id=AWDBO;Password=AWDBO";

        private static String MES_TNS =
            "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=121.133.99.66)(PORT=1521)))(CONNECT_DATA=(SID=IODB)));User Id=AWDBO;Password=AWDBO";
        //"(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 121.133.99.66)(PORT = 1521)))  (CONNECT_DATA = (SID = IODB)))";


        public static DBHelper_ORACLE mdb_main;

        int ret = 0;

        #endregion

        #region 하드웨어 변수

        private string RFID_CARD_NO = string.Empty;

        private Int32 iFormHandle = 0;

        // RFID 사용 변수
        private string ReceveData1 = "";

        String Prev_rfid = "";
        String HW_COMPORT = "";
        String HW_RFID_IP = "";
        String HW_RFID_PORT = "";
        String HW_LPR_IP = "";
        private String Weight_Knd = string.Empty;

        String HW_LPR_PORT = "";

        //String HW_CAM_IP = "";
        String PROG_TITLE = "";
        String Lpr_index = "0";
        String tmp_down = "0";
        String RFID_CODE = "0";

        //RFID카드 접촉 멘트 나올 때 중량 가지고 있음(2019-09-30 오창휘 수정)
        String tmp_down_rfid = "0";

        private String SENSOR_PORT = "0";

        private string WEIGHT_NO = string.Empty; //계량대번호
        private string weight_no = string.Empty;

        const char STX = (char) 2;
        const char ETX = (char) 3;
        const char CR = (char) 13;

        // 시리얼포트
        // 0 : 인디게이터
        private SerialPort[] serialPort = new SerialPort[2];

        private System.Threading.Thread ThreadClient_RFID;
        private System.Threading.Thread ThreadClient_LPR;
        private Socket SocketClient_LPR;
        private Socket SocketClient_RFID;
        private IPAddress ServerIPAddress;
        private int PORT;
        private IPEndPoint ServerIPEndPoint;

        public static Boolean rfid_ready = false;
        Int32 DeviceHandle;
        Boolean CardRead = false;
        Boolean Billet_fg = false; //빌렛 계량구분
        Boolean fix_fg = false; // 공차 계량 저장 여부 

        delegate void SetTextCallback(string text);

        //public int[] indicator_arr = {1, 2, 3, 4, 5, 6, 7, 8}; //4초
        //인디게이터 확인 2초로 변경(2020-03-25 한민호)
        //public int[] indicator_arr = { 1, 2, 3, 4 }; // 2초
        public int[] indicator_arr = { 1, 2, 3, 4, 5, 6 }; // 3초
        //인디게이터 확인 3초로 변경(2019-10-02 오창휘 수정)
        //인디게이터 확인 5초로 설정(2019-09-30)
        //public int indicator_max = 8; // 4초
        //인디게이터 확인 2초로 변경(2020-03-25 한민호)
        //public int indicator_max = 4; // 2초
        public int indicator_max = 6; // 3초
        public int indicator_cnt = 0;
        public int indicator_stopper = 0;

        public int indicator_read_old = 0; // 이전 Indicator 수신값
        public int indicator_read_cnt = 0; // 이상 Data 읽은 수
        public int indicator_read_max = 3; // 3번 수신까지 이상시 0으로 값 설정

        public int Gap = 90; //1차계량후 2차계량가능한 갭(초) (2021-06-29 정성호)

        Win32API.COPYDATASTRUCT wmcopydataRFID = new Win32API.COPYDATASTRUCT();

        //카메라1,2 추가(2020-01-29 오창휘 수정) START
        string cam_cd1 = "";
        string cam_cd2 = "";
        string cam_nm1 = "";
        string cam_nm2 = "";
        string cam_ip1 = "";
        string cam_ip2 = "";
        string file_path1;
        string file_path2;
        string ftp_host = "ftp://192.168.110.43/";
        string ftp_user = "ftpuser";
        string ftp_pass = "eogks@123";
        //카메라1,2 추가(2020-01-29 오창휘 수정) END

        //계량표 출력 datatable 전역변수 추가(2020-02-10 오창휘 수정))
        DataTable dt_pnt_qty;
        #endregion

        #region 계량처리 변수

        String car_no;
        String InOut_Gubun;
        String item_jung;
        String item_so;
        String item_so_nm;
        String cust_nm;
        String rfid_no;
        String wght_no;
        //계량번호 표시는 MAIN계량번호 변경(2019-11-30 한민호)
        String main_wght_no;

        String plnt_no;
        String site_nm;
        String item_type;

        String TimeGap; //1차계량후 2차계량 갭 검사하기 위해 추가(2021-06-29 정성호)
        String wght_no_print;
        String pmax_wght;
        String pmin_wght;
        String up_site_nm;
        String down_site_nm;
        String inspect;
        String bj_fg;//분진차량 구분 추가(2019-10-02 오창휘 수정)
        String img_vehl_no = "";//LPR차량번호 추가(2019-10-11 오창휘 수정)
        String img_vehl_file;//LPR 파일 위치 추가(2019-10-11 오창휘 수정)

        String rfid_seq;

        String load_area_cd = "";

        String down_area_cd = "";

        int rdt_chk_count = 0;//방사능 검출 횟수 항목 추가(2019-11-01 오창휘 수정)
        //기사명, 기사연락처 추가(2020-04-16 한민호)
        String drv_nm = "";
        String drv_tel_no = "";
        //수동접수여부 추가(Y:수동접수, N:자동접수, E:출입관리)(2020-05-06 한민호)
        String manual_yn = "";     

        // LPR 사용 유무
        String LPR_USE = "N";

        private string AutoNum = ""; //LPR에서 수신 받은 차량번호

        Boolean weight_fg = false; //1차, 2차 계량 저장 여부
        Boolean wegiht_fg_03 = false; // 사내이송
        Boolean RADIATION_CHK = false; // 방사능 센서 걸림
        int RADIATION_CNT = 0; // 방사능 측정 횟수 확인 추가 (2019-10-20 오창휘 수정)
        String Weight_Area; // 계량대 코드 
        private string szReceived = string.Empty; // LPR 리시브 변수
        private string[] list = new string[] { }; // LPR 리시브 변수 배열

        Int32 Iny_cnt = 0;
        String Gubun = "N";

        String SEIN_MIN = "";
        String SEIN_MAX = "";

        //빌릿출하 오차율 추가 (2021-10-05 정성호)
        String TMP_SEIN_MIN = "-7"; //빌릿출하 오차율 MIN
        String TMP_SEIN_MAX = "7"; //빌릿출하 오차율 MAX

        private frmMessage showform = null;
        private RedWindow RedWindowform = null;

        //private Timer timer1 = new Timer();
        private int time = 0;

        //차량진입 체크 Flag 추가(2019-09-30 오창휘 수정)
        private string AW_CHK = "0"; //1 > 차량IN으로 진입, 2 > 차량OUT으로 진입, 3 > 계량진행
        private Boolean VEHl_CHK = false; //계량대에 차량이 있는지 없는지 확인

        //크로스체크 Flag 추가(2019-10-07 오창휘 수정)
        private string CROSS_CHK = "0"; //크로스 체크 시 확인 변수 0 = 평시, 1 = pass, 2 = stop

        //중량초과 시 추가(2019-10-08 오창휘 수정)
        private Boolean OVER_WGT = false; //계량대에 차량이 있는지 없는지 확인

        //중량 안정화 중 색상 변화(2019-10-15 오창휘 수정)
        private Boolean wght_save_chk = false; //안정화 중 색상

        #endregion

        #region 윈도우 함수

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString( // GetIniValue 를 위해
            String section,
            String key,
            String def,
            StringBuilder retVal,
            int size,
            String filePath);


        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString( // SetIniValue를 위해
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
                uint newVolumeAllChannels = (((uint) newVolume & 0x0000ffff) | ((uint) newVolume << 16));
                waveOutSetVolume(IntPtr.Zero, newVolumeAllChannels);
            }
            catch (Exception)
            {
            }
        }

        public String GetIniValue(String Section, String Key, String iniPath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
            return temp.ToString();
        }

        // INI 값 설정
        public void SetIniValue(String Section, String Key, String Value, String iniPath)
        {
            WritePrivateProfileString(Section, Key, Value, iniPath);
        }

        #endregion

        #region Sound Voice

        SoundPlayer player01 = new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._01); // 1차 계량이 완료 되었습니다
        SoundPlayer player02 = new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._02); // 2차 계량이 완료 되었습니다
        SoundPlayer player05 = new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._05); // 계량이 완료되었습니다

        SoundPlayer player08 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._08); // 입출차 제한 차량입니다 경비실에 확인 바랍니다

        SoundPlayer player13 = new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._13); // RFID카드를 대주세요.   

        SoundPlayer player20 = new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._20); // 띵동소리 
        SoundPlayer player31 = new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._31); // RFID 카드 정보가 없습니다.

        SoundPlayer player34 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._34); // 공차 계량이 완료 되었습니다. 천천히 출발해 주세요 

        SoundPlayer player35 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._35); // 계량을 진행할수 없습니다. 출하실에 연락 바랍니다. 

        //공차계량은 1일1회 만 가능합니다. 천천히 출발하세요 메시지 띄울 것(2019-11-18 한민호)
        SoundPlayer player36 = new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._36); // 공차계량은 1일1회 만 가능합니다. 천천히 출발하세요

        SoundPlayer player65 = new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._65); // 계량표 뽑고 출발 

        SoundPlayer player66 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._66); // 고철검수가 완료되지 않아 계량을진행할수 없습니다

        SoundPlayer player67 = new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._67);

        SoundPlayer player68 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._68); // 미검수차량입니다. 현장 검수자에게 확인바랍니다.

        SoundPlayer player71 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._71); // 분진 품목은 2번 계량대에서 계량 바랍니다.
        SoundPlayer player72 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._72); // 분진 품목은 6번 계량대에서 계량 바랍니다.
        SoundPlayer player73 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._73); // 배차정보와 인식된 차량번호가 다릅니다. 
        SoundPlayer player74 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._74); // 방사능 검출 차량입니다. 다시 계량대로 진입하여 계량해주시기 바랍니다.
        SoundPlayer player75 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._75); // 배차정보가 없습니다. 차량 이동 후 해당 업체에 문의바랍나다.
        SoundPlayer player76 =
            new SoundPlayer(OCT_WEIGHT.AutoWeight.Properties.Resources._76); // 방사능 검출 차량입니다. 차량 이동 후 대기바랍니다.

        #endregion


        public frmMain()
        {
            InitializeComponent();
        }

        #region 설정가져오기

        private void GetConfig()
        {
            //계량대 코드 --------------------------------------------------------------- 
            // N : 인천 계량대 
            //---------------------------------------------------------------------------

            Weight_Area = GetIniValue("CONFIG", "Weight", Application.StartupPath + @"\SETTING.ini");

            HW_COMPORT = GetIniValue("CONFIG", "Indg", Application.StartupPath + @"\SETTING.ini");

            HW_RFID_IP = GetIniValue("CONFIG", "RFID_IP", Application.StartupPath + @"\SETTING.ini");

            HW_RFID_PORT = GetIniValue("CONFIG", "RFID_PORT", Application.StartupPath + @"\SETTING.ini");

            Weight_Knd = GetIniValue("CONFIG", "Weight", Application.StartupPath + @"\SETTING.ini");

            HW_LPR_IP = GetIniValue("CONFIG", "LPR_IP", Application.StartupPath + @"\SETTING.ini");

            HW_LPR_PORT = GetIniValue("CONFIG", "LPR_PORT", Application.StartupPath + @"\SETTING.ini");

            PROG_TITLE = GetIniValue("CONFIG", "TITLE", Application.StartupPath + @"\SETTING.ini");

            lblPhone.Text = "[" + GetIniValue("CONFIG", "PHONE", Application.StartupPath + @"\SETTING.ini") + "]";

            SEIN_MIN = GetIniValue("CONFIG", "SEIN_MIN", Application.StartupPath + @"\SETTING.ini");
            SEIN_MAX = GetIniValue("CONFIG", "SEIN_MAX", Application.StartupPath + @"\SETTING.ini");

            SENSOR_PORT = GetIniValue("SENSOR", "SENSOR_PORT", Application.StartupPath + @"\SETTING.ini");

            WEIGHT_NO = GetIniValue("CONFIG", "WEIGHT_NO", Application.StartupPath + @"\SETTING.ini");

            //계량대 명 표시 하기
            switch (WEIGHT_NO)
            {
                case "11001":
                    labelControl1.Text = "신평 입문";
                    break;
                case "11002":                   
                    labelControl1.Text = "신평 출문";
                    break;
                case "12001":                   
                    labelControl1.Text = "녹산정문 입문";
                    break;
                case "12002":                   
                    labelControl1.Text = "녹산정문 출문";
                    break;
                case "12101":                   
                    labelControl1.Text = "녹산후문 입문";
                    break;
                case "12102":
                    labelControl1.Text = "녹산후문 출문";
                    break;
                case "13001":                   
                    labelControl1.Text = "평택 입문";
                    break;
                case "13002":
                    labelControl1.Text = "평택 출문";
                    break;
            }
            //DB_Process.weight_name(Weight_Area);

            //풀로 채월시 숨겨놓은 작업표시줄이 열수 가 없는 문제 발생(2020-06-02 한민호)
            //평택은 화면을 풀로 채워서 이동 할 수 없도록 함(2020-05-29 한민호)
            if (WEIGHT_NO.Substring(0, 2) == "13" || WEIGHT_NO.Substring(0, 2) == "12")
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Location = new Point(0, 0);
                //this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                btnPrint.Visible = false;
            }

            dtSetCam = DB_Process.Camera_Info(WEIGHT_NO);

            cam_cd1 = dtSetCam.Rows[0]["CAMERA_CD1"].ToString();
            cam_cd2 = dtSetCam.Rows[0]["CAMERA_CD2"].ToString();
            cam_nm1 = dtSetCam.Rows[0]["CAMERA_NM1"].ToString();
            cam_nm2 = dtSetCam.Rows[0]["CAMERA_NM2"].ToString();
            cam_ip1 = dtSetCam.Rows[0]["IP_ADDR1"].ToString();
            cam_ip2 = dtSetCam.Rows[0]["IP_ADDR2"].ToString();

            //fnSetCam();
        }

        //카메라 컨트롤 추가(2020-01-29 오창휘 수정) START
        List<DevExpress.XtraTab.XtraTabPage> listTabPages = new List<DevExpress.XtraTab.XtraTabPage>();
        DataTable dtSetCam = null;
        void fnSetCam()
        {
            DataTable dt = dtSetCam;
            if (dt == null)
                return;

            AxAXISMEDIACONTROLLib.AxAxisMediaControl Ax1 = new AxAXISMEDIACONTROLLib.AxAxisMediaControl();
            //LPR 카메라 사용 안함(2020-04-26 한민호)
            //AxAXISMEDIACONTROLLib.AxAxisMediaControl Ax2 = new AxAXISMEDIACONTROLLib.AxAxisMediaControl();

            cam_cd1 = dt.Rows[0]["CAMERA_CD1"].ToString();
            cam_cd2 = dt.Rows[0]["CAMERA_CD2"].ToString();
            cam_nm1 = dt.Rows[0]["CAMERA_NM1"].ToString();
            cam_nm2 = dt.Rows[0]["CAMERA_NM2"].ToString();
            cam_ip1 = dt.Rows[0]["IP_ADDR1"].ToString();
            cam_ip2 = dt.Rows[0]["IP_ADDR2"].ToString();

            Ax1.Name = cam_cd1;
            //LPR 카메라 사용 안함(2020-04-26 한민호)
            //Ax2.Name = cam_cd2;

            Ax1.Tag = "STOP";
            //LPR 카메라 사용 안함(2020-04-26 한민호)
            //Ax2.Tag = "STOP";

            Ax1.TabIndex = 0;
            //LPR 카메라 사용 안함(2020-04-26 한민호)
            //Ax2.TabIndex = 1;
            PnlAxis.Controls.Add(Ax1);
            Size_Loc_Set(Ax1, 1);
            //LPR 카메라 사용 안함(2020-04-26 한민호)
            //PnlAxis.Controls.Add(Ax2);
            //Size_Loc_Set(Ax2, 1);

            // Set properties, deciding what url completion to use by MediaType.
            try
            {
                Ax1.MediaUsername = "root";
                Ax1.MediaPassword = "zaq1@wsx";
                Ax1.PTZControlURL = "http://" + cam_ip1 + "/axis-cgi/com/ptz.cgi";
                Ax1.ShowStatusBar = false;
                Ax1.EnableContextMenu = true;
                Ax1.StretchToFit = true;
                Ax1.MediaType = "h264";
                Ax1.MediaURL = CompleteURL(cam_ip1, "h264");
                Ax1.AudioReceiveURL = null;
                Ax1.AudioTransmitURL = "http://" + cam_ip1 + "/axis-cgi/audio/transmit.cgi";
                Ax1.AudioReceiveStop();
                Ax1.StopTransmitMedia();
                Ax1.H264VideoRenderer = 0x1000;
                Ax1.H264VideoDecodingMode = 3;
                Ax1.Play();
                Ax1.Tag = "PLAY";
            }
            catch
            {
                Ax1.Tag = "STOP";
            }
            //LPR 카메라 사용 안함(2020-04-26 한민호)
            //try
            //{
            //    Ax2.MediaUsername = "root";
            //    Ax2.MediaPassword = "zaq1@wsx";
            //    Ax2.PTZControlURL = "http://" + cam_ip2 + "/axis-cgi/com/ptz.cgi";
            //    Ax2.ShowStatusBar = false;
            //    Ax2.EnableContextMenu = true;
            //    Ax2.StretchToFit = true;
            //    Ax2.MediaType = "h264";
            //    Ax2.MediaURL = CompleteURL(cam_ip2, "h264");
            //    Ax2.AudioReceiveURL = null;
            //    Ax2.AudioTransmitURL = "http://" + cam_ip2 + "/axis-cgi/audio/transmit.cgi";
            //    Ax2.AudioReceiveStop();
            //    Ax2.StopTransmitMedia();
            //    Ax2.H264VideoRenderer = 0x1000;
            //    Ax2.H264VideoDecodingMode = 3;
            //    Ax2.Play();
            //    Ax2.Tag = "PLAY";
            //}
            //catch
            //{
            //    Ax2.Tag = "STOP";
            //}

        }
        private void Size_Loc_Set(AxAxisMediaControl Ax, int Cnt) //카메라 탭인덱스 별로 정렬
        {
            Ax.Width = (PnlAxis.Width / 2) - 10;
            Ax.Height = (PnlAxis.Height) - 10;

            switch (Ax.TabIndex)
            {
                case 0:
                    Ax.Top = 5;
                    Ax.Left = 3;
                    Application.DoEvents();
                    break;
                case 1:
                    Ax.Top = 5;
                    Ax.Left = (PnlAxis.Width / 2) - 3;
                    Application.DoEvents();
                    break;
            }
        }
        private string CompleteURL(string theMediaURL, string theMediaType)
        {
            string anURL = theMediaURL;
            if (!anURL.EndsWith("/")) anURL += "/";

            if (theMediaType == "mjpeg")
            {
                anURL += "axis-cgi/mjpg/video.cgi";
            }
            else if (theMediaType == "mpeg4")
            {
                anURL += "mpeg4/media.amp";
            }
            else if (theMediaType == "h264")
            {
                anURL += "axis-media/media.amp?videocodec=h264";
            }
            else if (theMediaType == "mpeg2-unicast")
            {
                anURL += "axis-cgi/mpeg2/video.cgi";
            }
            else if (theMediaType == "mpeg2-multicast")
            {
                anURL += "axis-cgi/mpeg2/video.cgi";
            }

            anURL = CompleteProtocol(anURL, theMediaType);
            return anURL;
        }
        private string CompleteProtocol(string theMediaURL, string theMediaType)
        {
            if (theMediaURL.IndexOf("://") >= 0) return theMediaURL;

            string anURL = theMediaURL;

            if (theMediaType == "mjpeg")
            {
                anURL = "http://" + anURL;
            }
            else if (theMediaType == "mpeg4" || theMediaType == "h264")
            {
                anURL = "axrtsp://" + anURL;
            }
            else if (theMediaType == "mpeg2-unicast")
            {
                anURL = "http://" + anURL;
            }
            else if (theMediaType == "mpeg2-multicast")
            {
                anURL = "axsdp" + anURL;
            }

            return anURL;
        }
        //카메라 컨트롤 추가(2020-01-29 오창휘 수정) END

        #endregion

        #region 프로그램 로드

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.Size = new Size(1024, 768);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //this.Size = new Size(1107, 795);
                //this.MaximizeBox = false;
                //화면작게, 창닫기 안보이게 수정(2020-05-28 한민호)
                //this.MinimizeBox = false;
                this.ControlBox = false;
                //this.ControlBox = false;

                //this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                SetSoundVolume(100);

                mdb_main = new DBHelper_ORACLE(
                    //CS_MES.Replace("MES_TNS", MES_TNS));
                    "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.110.43)(PORT=1524)))(CONNECT_DATA=(SID=WGDBO)));User Id=WGDBO;Password=WGDBO123");//대한제강 운영계(WGDBO로 변경)
                    //"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.110.43)(PORT=1521)))(CONNECT_DATA=(SID=AWDBO)));User Id=AWDBO;Password=AWDBO");//대한제강 운영계
                    //"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.110.107)(PORT=1521)))(CONNECT_DATA=(SID=STAZDEV)));User Id=GATE;Password=GATE");//대한제강 개발계
                    //"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.1.1.46)(PORT=1521)))(CONNECT_DATA=(SID=IODB)));User Id=AWDBO;Password=AWDBO");//가동계
                    //"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.1.1.48)(PORT=1521)))(CONNECT_DATA=(SID=IODB)));User Id=AWDBO;Password=AWDBO");//개발계

                //포커스이동 타이머 추가(2020-06-22 오창휘 수정)
                timer_focus.Enabled = true;

                GetConfig();

                // 프린트용지 잔량 체크
                // print_check();

                //COM.ECOMSOFT.DOTPRINT.PrintTool printTool = new COM.ECOMSOFT.DOTPRINT.PrintTool();
                //var serialDevice = System.Windows.Devices.SerialCommunication.SerialDevice; 
                //printTool.GetAttachedProperty()
                //=====================================================
                // 테스트시 주석으로 막는 부분 시작
                //======================================================

                //하드웨어 연결 부분 김상우 20190911
                //RFID 카드 리더기 연결

                //수동계량 막음(2019-10-09 한민호)_시작
                //막음(2019-12-20 한민호)
                //RFID_Connect(HW_RFID_IP);

                //시리얼 생성 : 인디게이터, RFID, 정위치 센서
                SerialCreate();

                //LPR Main PC PMC 접속
                //막음(2019-12-20 한민호)
                //server_connect_LPR();

                //Main 타이머
                timer_main.Enabled = true;

                //센서 타이머
                tmrsensor.Enabled = true;

                //인디게이터 타이머
                timer_indicator.Enabled = true;

                //수동계량 막음(2019-10-09 한민호)_끝

                

                //// LPR 사용여부 확인
                //DataTable dt_lpr = null;
                //ServiceAdapter _svc = new ServiceAdapter();
                //dt_lpr = DB_Process.LPR_INFO(Weight_Area);
                //if (dt_lpr != null && dt_lpr.Rows.Count > 0)
                //{
                //    if (dt_lpr.Rows[0]["USE_YN"].ToString() == "Y")
                //    {
                //        Lpr_index = dt_lpr.Rows[0]["CODE_VALUE2"].ToString();
                //        LPR_USE = "Y";
                //    }
                //    else
                //    {
                //        Lpr_index = dt_lpr.Rows[0]["CODE_VALUE2"].ToString();
                //        LPR_USE = "N";
                //    }
                //}

                LPR_USE = "Y";

                //server_connect_RFID();
                //======================================================
                // 테스트시 주석으로 막는 부분 종료
                //======================================================


                txtManualRfid.Focus();
            }
            catch (Exception ex)
            {
                logger.Error("무인 계량 LOAD중 ERROR: " + ex.Message.ToString().Trim());
            }
        }

        private void RFID_Connect(string data)
        {
            try
            {
                //ipaddress = this.txtIpAddress.Text.Trim();

                // 리더객체 TCP 타입으로 생성한다.
                reader = new Reader(ConnectType.Tcp);
                // 사용될 리더의 모델타입을 지정한다.
                reader.ModelType = ModelType.NL_RF2200;
                // 이벤트 핸들러를 등록한다.
                //reader.ReaderEvent += new ReaderEventHandler(OnReaderEvent);
                //textBox3.Text = HW_RFID_IP;
                // IP, 관리포트(5578)로 Socket 연결을 시작한다.
                //textBox3.Text = HW_RFID_IP;
                reader.ConnectSocket(HW_RFID_IP, 5578); // PORT 번호 고정

                if (reader.IsConnecting)
                {
                    ThreadClient_RFID = new Thread(new ThreadStart(onNewReaderEvent));
                    ThreadClient_RFID.IsBackground = true;
                    ThreadClient_RFID.Start();
                    logger.Info("RF-ID 카드 접속!");
                    reader.ReadTagId(TagType.ISO18000_6C_GEN2, ReadType.MULTI); //  card Read 가능 상태 명령어 <== 중요!!!!
                }

            }
            catch (Exception ex)
            {
                logger.Error(" RF-ID 카드 접속 오류 " + ex.Message);
            }
        }

        void onNewReaderEvent()
        {
            reader.ReaderEvent += new ReaderEventHandler(OnReaderEvent);
        }

        private void OnReaderEvent(object sender, ReaderEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ReaderEventHandler(OnReaderEvent), new object[] {sender, e});
                return;
            }

            // sender : Reader 객체ㄴ
            // e.Kind : 발생된 이벤트 종류
            // e.Message : 이벤트 발생에 대한 설명
            // e.Payload : 리더로부터 수신된 바이트 배열
            // e.CloseType : 닫기 유형
            string payload;
            //김상우 차장 수정(2019-10-07 오창휘 수정)
            //String tmp = "";
            int mode = 0;

            switch (e.Kind)
            {
                // 연결이 완료되면 발생합니다.
                case ReaderEventKind.Connected:
                    logger.Info("RFID 리더기 접속 " + e.Message);
                    //textBox3.Text += "Disconnect" + Environment.NewLine;

                    // 리더 정보 조회
                    //GetConfiguration();

                    //this.splitContainer1.Panel2.Enabled = true;
                    break;
                // 연결이 정상적으로 해제되면 발생합니다.
                case ReaderEventKind.Disconnected:
                    logger.Info("RFID 리더기 해제 " + e.Message);
                    // 끊길경우 재접속 추가(2019-10-16 오창휘 수정)
                    try
                    {
                        //reader.Dispose();
                        //// 리더객체 TCP 타입으로 생성한다.
                        //reader = new Reader(ConnectType.Tcp);
                        //// 사용될 리더의 모델타입을 지정한다.
                        //reader.ModelType = ModelType.NL_RF2200;

                        // 0.5초 뒤에 다시 커넥트 추가(2019-12-06 오창휘 수정)
                        Thread.Sleep(500);
                        //리더기 재연결 시 상태 체크 후 재연결 추가(2019-12-09 오창휘 수정)
                        logger.Info("RFID 리더기 재연결 체크 [ 동작여부 reader.IsHandling : " + (reader.IsHandling ? "Y" : "N") + " / 연결시도중여부 reader.IsConnecting : " + (reader.IsConnecting ? "Y" : "N") + " / 인벤토리작업중여부 reader.IsInventorying : " + (reader.IsInventorying ? "Y" : "N") + " ]");
                        if(!reader.IsHandling && !reader.IsConnecting && !reader.IsInventorying )
                        {
                            reader.ConnectSocket(HW_RFID_IP, 5578); // PORT 번호 고정
                        }
                        //reader.ConnectSocket(HW_RFID_IP, 5578); // PORT 번호 고정

                        //if (reader.IsConnecting)
                        //{
                        //    reader.ReadTagId(TagType.ISO18000_6C_GEN2, ReadType.MULTI); //  card Read 가능 상태 명령어 <== 중요!!!!
                        //}
                    }
                    catch (Exception ex)
                    {
                        logger.Error(" RFID 리더기 재연결 시 에러 발생 - " + ex.Message);
                    }
                    break;
                /*
                textBox3.Text += e.Message + Environment.NewLine;
                textBox3.Text += "Connect" + Environment.NewLine;

                //this.splitContainer1.Panel2.Enabled = false;
                //창닫기를 통해서 발생되었으면 창을 닫아 준다.
                if (e.CloseType == CloseType.FormClose) Close();
                break;
                */
                // 일정시간 동안 응답이 없는 경우 발생합니다.
                case ReaderEventKind.timeout:
                    logger.Info(e.Message);
                    break;
                //break;
                // 리더의 버전정보 수신시 발생합니다.
                case ReaderEventKind.Version:
                    payload = Encoding.ASCII.GetString(e.Payload);
                    //textBox3.Text += payload;
                    string[] items = payload.Split(' ');

                    switch (items[0])
                    {
                        case "v0": // Reader Version
                            logger.Info("Reader Version : " + items[1]);
                            break;
                        case "v2": // Root Version
                            logger.Info("Root Version : " + items[1]);
                            break;
                        case "v5": // H/W Version
                            logger.Info("H/W Version : " + items[1]);
                            break;
                    }

                    break;
                // 리더의 안테나 정보 수신시 발생합니다.
                case ReaderEventKind.AntennaState:
                    logger.Info(e.Message);
                    break;
                // 리더의 안테나 파워값 수신시 발생합니다.
                case ReaderEventKind.Power:
                    payload = Encoding.ASCII.GetString(e.Payload);

                    switch (payload.Substring(0, 1))
                    {
                        case "e": // Antenna State
                            int number = Convert.ToInt32(payload.Substring(1).Replace(",", ""));
                            /*// Antenna Port는 안테나 번호순서대로 1, 2, 4, 8의 값이 할당됨
                            this.chkAnt1.Checked = (number & 0x0001) > 0;
                            this.chkAnt2.Checked = (number & 0x0002) > 0;
                            this.chkAnt3.Checked = (number & 0x0004) > 0;
                            this.chkAnt4.Checked = (number & 0x0008) > 0;
                            */
                            break;
                        case "p": //Power Value
                            logger.Info("payload : " + payload.Substring(1));
                            break;
                    }

                    break;
                // Inventory시 Tag ID 수신시 발생합니다.
                case ReaderEventKind.TagId:
                    // 리더로부터 수신된 데이터는 바이트 배열에 들어 있으며 문자열로 Decode하여 사용합니다.
                    // 한개 이상의 수신 데이터가 들어 있을 수 있습니다.
                    // 하기와 같이 "\r\n>" 기준으로 분리하여 사용합니다.

                    payload = Encoding.ASCII.GetString(e.Payload);
                    string[] tagIds = payload.Split(new string[] {"\r\n>"}, StringSplitOptions.RemoveEmptyEntries);

                    if (lblRfid_no.Text.Length > 0)
                    {
                        return;
                    }

                    foreach (string tagid in tagIds)
                    {
                        //중량안정화 됐을 때만 RFID 태그 적용(2019-10-02 오창휘 수정)
                        if (CardRead && AW_CHK != "3")
                        {
                            //카드 리드 로그 추가(2019-10-16 오창휘 수정)
                            logger.Info("중량 안전화 후 tag : " + tagid);
                            //logger.Info("tagIds : " + tagIds[0]);
                            //this.lvwInventory.Items.Add(tagid);

                            //센서걸림 확인 로직 추가(2019-11-11 오창휘 수정)
                            if (InSensor.Tag == "01" || outSensor.Tag == "01")
                            {
                                logger.Info("정위치 센서 걸림");
                                la_ment.Text = "계량을 진행할 수 없습니다. 계량대 정위치 센서를 확인바랍니다.";
                                return;
                            }

                            // 앞의 2자 (1T)는 응답코드이므로 제거합니다.
                            string epc = tagid.Substring(2);
                            RFID_CARD_NO = epc;
                            lblRfid_no.Text = RFID_CARD_NO;

                            int count = 0;

                            //배차정보 체크
                            logger.Info("epc.Substring(24, 4) = " + epc.Substring(24, 4));
                            // 수동도 확인 가능하게 옮김(2019-09-30 오창휘 수정)
                            //DataTable dt_rfid = DB_Process.FINDVEHLNO2(epc.Substring(24, 4));

                            //if (dt_rfid != null)
                            //{
                            //    int msg = Convert.ToInt32(dt_rfid.Rows[0]["CNT"].ToString());

                            //    if (msg == 0)
                            //    {
                            //        player67.Play();
                            //        la_ment.Text = "배차 정보가 없습니다. ";
                            //        RedWindowform.labelControl1.Text = "배차 정보가 없습니다. ";
                            //        logger.Error(this.ToString() + " 배차정보 없음 P_RFID_CARD : [" + rfid_no + "]");
                            //        RedWindowform = new RedWindow();
                            //        RedWindowform.TopMost = true;
                            //        if (RedWindowform.ShowDialog() == DialogResult.OK)
                            //        {
                            //            RedWindowform.Close();
                            //        }
                            //        la_ment.Text = "배차 정보가 없습니다. ";
                            //        return;
                            //    }
                            //}

                            //LPR 차량번호 촬영
                            //server_connect_LPR();

                            //LPR_load로 옮김(2019-10-15 오창휘 수정)
                            //server_send_LPR("LPR", "전광판", "Test", WEIGHT_NO);

                            //바로 계량
                            if (epc.Length > 0)
                            {
                                logger.Info(epc);
                                txtManualRfid.Text = epc.Substring(24, 4);
                                logger.Info(txtManualRfid.Text);
                                txtManualWeight.Text = lblWeight_value.Text;
                                logger.Info(txtManualWeight.Text);
                            }

                            if (txtManualRfid.Text.Length == 4)
                            {

                                rfid_no = txtManualRfid.Text;

                                logger.Info(txtManualRfid.Text);
                                btnRFID_Click(null, null);
                            }
                            //HEX 형태를 문자열로 표시해 준다.
                            /*
                            string txt = string.Empty;
                            if (reader.TagType == TagType.ISO18000_6C_GEN2)
                            {
                                // PC 값 "HHHH" 을 제거하고 처리한다 - PC(HHHH)
                                int p = 4;
                                if (tagid.Length > p)
                                {
                                    string hex = epc.Substring(p, epc.Length - p);
                                    txt = reader.MakeTextFromHex(hex);
                                }
                            }
                            */

                            //lvi.SubItems.Add(txt);

                            //this.lvwInventory.EndUpdate();
                            //this.lvwInventory.Refresh();
                            //break;
                        }
                        else
                        {
                            //카드 리드 로그 추가(2019-10-16 오창휘 수정)
                            logger.Info("중량 안전화 전 tag : " + tagid);
                        }
                    }

                    break;
                // Memory Bank 값 Read시 발생
                case ReaderEventKind.GetTagMemory:
                    payload = Encoding.ASCII.GetString(e.Payload);

                    if (payload.Substring(1, 1) == "T" || payload.Substring(1, 1) == "B")
                    {
                        logger.Info("payload.substring(2) : " + payload.Substring(2));
                    }

                    break;
                // Memory Bank 값 Write, Lock, Kill 등 작업에 대한 응답
                case ReaderEventKind.TagResponseCode:
                    // C : Error등 응답코드
                    payload = Encoding.ASCII.GetString(e.Payload);

                    if (payload.Substring(1, 1) == "C")
                    {
                        string code = payload.Substring(2);
                        //textBox3.Text += 0 + "/" + code + "-" + Reader.Responses(code);
                        logger.Info("응답코드 : " + 0 + "/" + code + "-" + Reader.Responses(code));
                        /*
                        if (this.tabControl1.SelectedTab.Name == "tpReadWrite")
                            this.lstResponse.Items.Insert(0, code + "-" + Reader.Responses(code));
                        else
                            this.lstResponseLockKill.Items.Insert(0, code + "-" + Reader.Responses(code));
                        */
                    }

                    break;
            }
        }

        private void GetConfiguration()
        {
            if (reader != null)
            {
                // 리더 쓰레드가 동작중이며, 인벤토링중이 아니라면
                if (reader.IsHandling && !reader.IsInventorying)
                {
                    this.Cursor = Cursors.WaitCursor;

                    //// Version - reader
                    reader.GetVersion(0);
                    Thread.Sleep(300);
                    //// Version - H/W
                    reader.GetVersion(5);
                    Thread.Sleep(300);
                    //// Version - Root
                    reader.GetVersion(2);
                    Thread.Sleep(300);

                    // Power
                    reader.GetPower();
                    Thread.Sleep(300);
                    //Antenna state
                    reader.GetAntennaState();
                    Thread.Sleep(300);

                    reader.ReadTagId(TagType.ISO18000_6C_GEN2, ReadType.ONE);

                    this.Cursor = Cursors.Arrow;
                }
            }
        }


        #endregion

        #region Main_Load (RFID 처리 메인)

        #region LPR 인식

        private Boolean LPR_Load()
        {
            DataTable dt_rf = null;
            ServiceAdapter _svc = new ServiceAdapter();
            Boolean ret = false;
            //계량값으로 계량 시작 
            tmp_down = "0";
            tmp_down = lblWeight_value.Text;

            if (tmp_down == "-9999")
            {
                return false;
            }

            if (rfid_no.Length >= 4)
            {
                //수동계량 시 주석처리(2019-10-17 오창휘 수정) START
                server_connect_LPR();

                Thread.Sleep(500);
                //수동테스트 주석(오창휘 수정)
                server_send_LPR("LPR", "", "", WEIGHT_NO);
                //슬립 추가 (2019-10-14 오창휘 수정)
                //Thread.Sleep(1000);
                Thread.Sleep(1500);// 슬립 시간 늘림(2019-10-31 오창휘 수정)
                //수동계량 시 주석처리(2019-10-17 오창휘 수정) END


                logger.Info("{" + Lpr_index + "|" + car_no + "}");

                ret = true;
            }

            return ret;
        }

        #endregion

        #region 사내이송 및 테스트 계량 처리

        private string getSecToMin()
        {
            int min;
            int sec;
            string rtValue = string.Empty;

            min = Gap/60;
            sec = Gap%60;

            if(min > 0)
            {
                rtValue += string.Format("{0}분", min);
            }

            if(sec > 0)
            {
                rtValue += string.Format("{0}초", sec);
            }

            return rtValue;
        }

        private Boolean Main_03_Load()
        {
            DataTable dt_Chk = null;
            DataTable dt_result = null;
            DataTable dt_result_Gap = null;
            DataTable dt_result_Display = null;
            ServiceAdapter _svc = new ServiceAdapter();
            Boolean ret = false;

            DataTable dt_rfid = null;

            //자가고철은 4자리임(2020-02-17 한민호)
            if (rfid_no.Length >= 3)
            //if (rfid_no.Length >= 4)
            {
                //사내이송 및 테스트 계량 여부 체크
                //공장구분 추가(2020-02-17 한민호)
                dt_Chk = DB_Process.RFID_TRANSFER_CHK(rfid_no, WEIGHT_NO.Substring(0, 4)); //SP_MU_TRANSFER_CHK
                //dt_Chk = DB_Process.RFID_TRANSFER_CHK(rfid_no);
                if (dt_Chk != null && dt_Chk.Rows.Count > 0)
                {
                    car_no = dt_Chk.Rows[0]["VEHL_NO"].ToString();
                    InOut_Gubun = dt_Chk.Rows[0]["INOUT_GUBUN"].ToString();
                    item_jung = dt_Chk.Rows[0]["ITEM_JUNG"].ToString();
                    item_so = dt_Chk.Rows[0]["ITEM_SO"].ToString();
                    item_so_nm = dt_Chk.Rows[0]["ITEM_SO_NM"].ToString();

                    ret = true;

                    if (InOut_Gubun == "2" && item_jung == "6" && item_so == "69999") //테스트 계량 로직
                    {
                        //테스트 계량
                        dt_result = DB_Process.insert_test(Weight_Area, rfid_no, Convert.ToInt32(lblWeight_value.Text.Replace(",", "")));//Convert.ToInt32(tmp_down));
                        if (dt_result.Rows.Count > 0)
                        {
                            if (dt_result.Rows[0]["RESULT"].ToString() == "1") //테스트차량 계량 완료
                            {
                                dt_result_Display = DB_Process.RESULT_DISPLAY(dt_result.Rows[0]["RFID_SEQ"].ToString());
                                if (dt_result_Display.Rows.Count > 0)
                                {
                                    // 결과 값 DISPALY
                                    lblRfid_no.Text = rfid_no;
                                    lblItem_Nm.Text = item_so_nm;
                                    lblVEHL_NO.Text = car_no;
                                    lblFirst_wght.Text = dt_result_Display.Rows[0]["LOAD_WEIGHT"].ToString();
                                    lblSecond_wght.Text = dt_result_Display.Rows[0]["DOWN_WEIGHT"].ToString();
                                    lblReal_wgt.Text = dt_result_Display.Rows[0]["REAL_WGHT"].ToString();
                                    //계량번호 표시는 MAIN계량번호 변경(2019-11-30 한민호)
                                    lblWght_No.Text = dt_result_Display.Rows[0]["MAIN_WGHT_NO"].ToString();
                                    //lblWght_No.Text = dt_result_Display.Rows[0]["WGHT_NO"].ToString();

                                    player05.Play();
                                    la_ment.Text = "계량이 완료 되었습니다. ";
                                    //계량진행 상태값 추가 (2019-11-01 오창휘 수정)
                                    AW_CHK = "3";
                                }
                            }
                            else
                            {
                                logger.Info(" 테스트차량 계량 Error>< " + dt_result.Rows[0]["ERRMSG"].ToString() + " >  ");
                                //계량진행 상태값 추가 (2019-11-01 오창휘 수정)
                                AW_CHK = "3";
                            }
                        }
                    }
                    else // 사내이송
                    {
                        logger.Info(rfid_no + "/" + lblWeight_value.Text.Replace(",", "") + "/" + Weight_Area);

                        //자가고철 스틸컷 이미지 경로 추가(2020-02-24 한민호)
                        StillCut();

                        //1차계량후 GAP 체크하는 로직 추가(2021-06-29 정성호)
                        bool Stop = false;

                        dt_result_Gap = DB_Process.GAP_Check(rfid_no, Weight_Area);

                        if(dt_result_Gap.Rows.Count > 0)
                        {
                            if (dt_result_Gap.Rows[0]["RESULT"].ToString() == "2") //2차계량일때
                            {
                                TimeGap = dt_result_Gap.Rows[0]["TIMEGAP"].ToString();

                                if (int.Parse(TimeGap) < Gap)
                                {
                                    player20.Play();
                                    la_ment.Text = string.Format("1차 계량후 {0} 이후에 2차 계량을 진행 할 수 있습니다. {1}초 남았습니다.", getSecToMin(), Gap - int.Parse(TimeGap));

                                    AW_CHK = "3";

                                    Stop = true;
                                }
                            }
                        }

                        if (!Stop)
                        {
                            //공장구분 추가(2020-02-17 한민호)
                            //계량대번호로 변경(2020-02-20 한민호)
                            //자가고철 스틸컷 이미지 경로 추가(2020-02-24 한민호)
                            dt_result = DB_Process.Gongcha_insert(rfid_no,
                                                                  Convert.ToInt32(lblWeight_value.Text.Replace(",", "")),
                                                                  Weight_Area, file_path1, file_path2);
                            //dt_result = DB_Process.Gongcha_insert(rfid_no, Convert.ToInt32(lblWeight_value.Text.Replace(",", "")), Weight_Area);
                            //dt_result = DB_Process.Gongcha_insert(rfid_no, Convert.ToInt32(lblWeight_value.Text.Replace(",", "")), WEIGHT_NO.Substring(0, 4)); //Convert.ToInt32(tmp_down), Weight_Area);
                            //dt_result = DB_Process.Gongcha_insert(rfid_no, Convert.ToInt32(lblWeight_value.Text.Replace(",", "")), Weight_Area); //Convert.ToInt32(tmp_down), Weight_Area);
                            if (dt_result.Rows.Count > 0)
                            {
                                if (dt_result.Rows[0]["RESULT"].ToString() == "0") //쿼리 에러
                                {
                                    logger.Info(" 자가고철 계량 Error>< " + dt_result.Rows[0]["ERRMSG"].ToString() + " >  ");
                                    //계량진행 상태값 추가 (2019-11-01 오창휘 수정)
                                    AW_CHK = "3";
                                }
                                else if (dt_result.Rows[0]["RESULT"].ToString() == "1") //공차계량
                                {
                                    // 결과 값 DISPALY
                                    lblRfid_no.Text = rfid_no;
                                    lblItem_Nm.Text = item_so_nm;
                                    lblVEHL_NO.Text = car_no;
                                    lblFirst_wght.Text = lblWeight_value.Text;
                                    lblWght_No.Text = dt_result.Rows[0]["MAIN_WGHT_NO"].ToString();

                                    player01.Play();
                                    la_ment.Text = "1차 계량이 완료 되었습니다. ";

                                    //수동계량 시 주석처리(2019-10-17 오창휘 수정) START
                                    //server_connect_LPR();
                                    //Thread.Sleep(500);
                                    //server_send_LPR("PID", "행선지", down_site_nm, WEIGHT_NO);
                                    //수동계량 시 주석처리(2019-10-17 오창휘 수정) END

                                    //계량진행 상태값 추가 (2019-11-01 오창휘 수정)
                                    AW_CHK = "3";

                                    //계량 후에는 코드버튼 클리어(2020-02-17 한민호)
                                    lblInput.Text = "";
                                    txtManualRfid.Text = "";
                                }
                                    //공차계량은 1일1회 만 가능합니다. 천천히 출발하세요 메시지 띄울 것(2019-11-18 한민호)
                                else if (dt_result.Rows[0]["RESULT"].ToString() == "3") //공차계량
                                {
                                    player36.Play();
                                    la_ment.Text = "공차계량은 1일1회 만 가능합니다. 천천히 출발하세요 ";

                                    AW_CHK = "3";
                                }
                                else //자동 2차 계량 
                                {
                                    dt_result_Display =
                                        DB_Process.RESULT_DISPLAY(dt_result.Rows[0]["RFID_SEQ"].ToString());
                                    if (dt_result_Display.Rows.Count > 0)
                                    {
                                        // 결과 값 DISPALY
                                        lblRfid_no.Text = rfid_no;
                                        lblItem_Nm.Text = item_so_nm;
                                        lblVEHL_NO.Text = car_no;
                                        lblFirst_wght.Text = dt_result_Display.Rows[0]["LOAD_WEIGHT"].ToString();
                                        lblSecond_wght.Text = dt_result_Display.Rows[0]["DOWN_WEIGHT"].ToString();
                                        lblReal_wgt.Text = dt_result_Display.Rows[0]["REAL_WGHT"].ToString();
                                        //계량번호 표시는 MAIN계량번호 변경(2019-11-30 한민호)
                                        lblWght_No.Text = dt_result_Display.Rows[0]["MAIN_WGHT_NO"].ToString();
                                        //lblWght_No.Text = dt_result_Display.Rows[0]["WGHT_NO"].ToString();

                                        player02.Play();
                                        la_ment.Text = "2차 계량이 완료 되었습니다. ";
                                        //계량진행 상태값 추가 (2019-11-01 오창휘 수정)
                                        AW_CHK = "3";

                                        //계량 후에는 코드버튼 클리어(2020-02-17 한민호)
                                        lblInput.Text = "";
                                        txtManualRfid.Text = "";

                                        //by_Matrial("N", InOut_Gubun, item_jung,
                                        //            item_so, ds_r.Tables[0].Rows[0]["RFID_SEQ"].ToString(), ds_r.Tables[0].Rows[0]["WGHT_NO"].ToString());
                                        //server_connect_LPR();

                                        //대한제강 자가고철도 계량표 출력(2020-02-17 한민호)
                                        rfid_seq = dt_result.Rows[0]["RFID_SEQ"].ToString();
                                            //재발행을 위해 변수에 넣음(2020-02-20 한민호)
                                        dt_pnt_qty = DB_Process.pnt_qty("N", InOut_Gubun, item_jung, item_so,
                                                                        dt_result.Rows[0]["RFID_SEQ"].ToString());

                                        if (dt_pnt_qty.Rows[0]["ITEM_TYPE_NM"].ToString() == "원자재")
                                        {
                                            SubMatrial_원자재 Print_etc = new SubMatrial_원자재(dt_pnt_qty);

                                            Print_etc.Print();
                                            Print_etc.Dispose();
                                        }
                                        else
                                        {
                                            SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);

                                            Print_etc.Print();
                                            Print_etc.Dispose();
                                        }
                                        //미리보기 화면
                                        //Print_etc.ShowPreview();
                                        ////인쇄
                                        //Print_etc.Print();
                                        //Print_etc.Dispose();

                                        //DB_Process.PRINT_PAGE_CNT_SET(Weight_Area, 1);  //출력매수1장

                                        ////계량대 번호 기준 바꿈(2019-10-31 오창휘 수정)
                                        //if (WEIGHT_NO == "0100" || WEIGHT_NO == "0104")//1번 5번 계량대만 출력
                                        ////if (weight_no == "ALF" && weight_no == "BLF")
                                        //{
                                        //     //슬래그(G0001)량만 출력(2019-11-01 한민호)
                                        //    if (item_so == "G0001")
                                        //    {
                                        //        //by_Matrial(Gubun, InOut_Gubun, item_jung, item_so, rfid_seq, wght_no);
                                        //        //전역 변수로 변경 > 재발행 기능을 위해(2020-02-10 오창휘 수정)
                                        //        dt_pnt_qty = DB_Process.pnt_qty("N", InOut_Gubun, item_jung, item_so, dt_result.Rows[0]["RFID_SEQ"].ToString());
                                        //        //RFID_SEQ 가져오게 수정(2019-11-08 한민호)
                                        //        //DataTable dt_pnt_qty = DB_Process.pnt_qty("N", InOut_Gubun, item_jung, item_so, dt_result.Rows[0]["RFID_SEQ"].ToString());
                                        //        //DataTable dt_pnt_qty = DB_Process.pnt_qty("N", InOut_Gubun, item_jung, item_so, rfid_seq);
                                        //        if (dt_pnt_qty != null)
                                        //        {
                                        //            //출력매수 무조건 2장(2019-11-15 한민호)
                                        //            int pnt_page_cnt = 2;
                                        //            for (int Cnt = 0; Cnt < pnt_page_cnt; Cnt++)
                                        //            {
                                        //                SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);
                                        //                //미리보기 화면
                                        //                //미리보기 감추기(2019-11-06 한민호)
                                        //                //Print_etc.ShowPreview();
                                        //                //인쇄
                                        //                Print_etc.Print();
                                        //            }
                                        //            // 프린터 사용 매수 업데이트
                                        //            DB_Process.PRINT_PAGE_CNT_SET(Weight_Area, pnt_page_cnt);
                                        //            //SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);
                                        //            ////미리보기 화면
                                        //            ////미리보기 감추기(2019-11-06 한민호)
                                        //            ////Print_etc.ShowPreview();
                                        //            ////인쇄
                                        //            //Print_etc.Print();
                                        //        }
                                        //        else
                                        //        {
                                        //            logger.Info("Print Data get Error!");
                                        //        }
                                        //    }
                                        //}
                                    }

                                    //print_check();
                                    //by_Matrial(Gubun, InOut_Gubun, item_jung, item_so, rfid_seq, wght_no);
                                    //by_Matrial("Y", "02", "201", "2016", "161108N00008", "161108NW0004");
                                    //print_check();
                                }
                            }
                        }

                        wegiht_fg_03 = true;
                    }
                }
            }

            return ret;
        }

        #endregion


        private void Main_Load()
        {
            try
            {
                Int32 real = 0;

                DataTable dt_rfid = null;
                DataTable dt_result_Display = null;

                Double Sein = 0;
                Double Sein_Point = 0;
                tmp_down = "0";

                #region 계량값으로 계량 시작

                //계량값으로 계량 시작                 
                tmp_down = lblWeight_value.Text;

                if (tmp_down == "-9999")
                {
                    return;
                }

                #endregion

                #region 1. 당일 계량 금지 인지 확인

                //2019-12-28 대한제강
                dt_rfid = DB_Process.Start_rfid2(rfid_no);

                //빌렛출하시 별도 세인율 검사 로직추가 (2021-09-27 정성호)
                /*SP_MU_WEIGHT_ALL_NEW_R2 

                추가해야할 컬럼

                plnt_no : 공장코드
                site_nm : 납품위치

                기존컬럼

                item_type :  기존 컬럼 ITEM_JUNG_NM 사용*/

                ////-------------------------------------------------------
                //// 1. 당일 계량 금지 인지 확인 
                //if (DB_Process.CAR_LIMIT_CHK_ALL() == "Y")
                //{
                //    //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                //    AW_CHK = "3";
                //    //player30.Play();
                //    //la_ment.Text = "오늘은 계량을 진행하지 않습니다. 천천히 출발해 주세요";
                //    //logger.Info(this.ToString() + " 입차제한-일자");
                //    //RedWindowform = new RedWindow();
                //    //RedWindowform.ShowDialog();
                //    la_ment.Text = "오늘은 계량을 진행하지 않습니다. 천천히 출발해 주세요";
                //    logger.Error("오늘은 계량을 진행하지 않습니다. 천천히 출발해 주세요");
                //    RedWindowform = new RedWindow();
                //    RedWindowform.TopMost = true;
                //    RedWindowform.labelControl1.Text = "오늘은 계량을 진행하지 않습니다. 천천히 출발해 주세요";
                //    if (RedWindowform.ShowDialog() == DialogResult.OK)
                //    {
                //        RedWindowform.Close();
                //    }
                //    la_ment.Text = "오늘은 계량을 진행하지 않습니다. 천천히 출발해 주세요";
                //    return;
                //}
                //else
                //{
                //    dt_rfid = DB_Process.Start_rfid(rfid_no);
                //}
                ////======================================================

                #endregion

                //-------------------------------------------------------
                // 2. RFID 카드 번호 확인. 계량 시작  
                if (dt_rfid != null && dt_rfid.Rows.Count > 0)
                {

                    plnt_no = dt_rfid.Rows[0]["plnt_no"].ToString();
                    site_nm = dt_rfid.Rows[0]["site_nm"].ToString();
                    item_type = dt_rfid.Rows[0]["ITEM_JUNG_NM"].ToString();
                    TimeGap = dt_rfid.Rows[0]["TIMEGAP"].ToString();//1차계량후 갭 검사(2021-06-29 정성호)
                    car_no = dt_rfid.Rows[0]["VEHL_NO"].ToString();
                    item_so = dt_rfid.Rows[0]["ITEM_SO"].ToString();
                    item_so_nm = dt_rfid.Rows[0]["ITEM_SO_NM"].ToString();
                    item_jung = dt_rfid.Rows[0]["ITEM_JUNG"].ToString();
                    cust_nm = dt_rfid.Rows[0]["CUST_NM"].ToString();
                    pmax_wght = dt_rfid.Rows[0]["PMAX_WGHT"].ToString();
                    pmin_wght = dt_rfid.Rows[0]["PMIN_WGHT"].ToString();
                    up_site_nm = dt_rfid.Rows[0]["UP_SITE_NM"].ToString();
                    down_site_nm = dt_rfid.Rows[0]["DOWN_SITE_NM"].ToString();
                    rfid_no = dt_rfid.Rows[0]["RFID_NO"].ToString();
                    wght_no = dt_rfid.Rows[0]["WGHT_NO"].ToString();
                    //계량번호 표시는 MAIN계량번호 변경(2019-11-30 한민호)
                    main_wght_no = dt_rfid.Rows[0]["MAIN_WGHT_NO"].ToString();

                    wght_no_print = dt_rfid.Rows[0]["WGHT_NO"].ToString();
                    rfid_seq = dt_rfid.Rows[0]["RFID_SEQ"].ToString();
                    InOut_Gubun = dt_rfid.Rows[0]["INOUT_GUBUN"].ToString();
                    inspect = dt_rfid.Rows[0]["INSPECT"].ToString();
                    //InOut_Gubun = "3";
                    lblRfid_no.Text = rfid_no == null ? "" : rfid_no;
                    lblItem_Nm.Text = dt_rfid.Rows[0]["ITEM_SO_NM"] == null
                        ? ""
                        : dt_rfid.Rows[0]["ITEM_SO_NM"].ToString();

                    bj_fg = dt_rfid.Rows[0]["BJ_FG"].ToString();
                    //크로스체크 수정 (2019-10-11 오창휘 수정)
                    //img_vehl_no = dt_rfid.Rows[0]["IMG_VEHL_NO"].ToString();
                    //방사능 횟수 가져옴(2019-11-01 오창휘 수정)
                    rdt_chk_count = Convert.ToInt32(dt_rfid.Rows[0]["RADIATION_CHK_COUNT"].ToString() == "" ? "0" : dt_rfid.Rows[0]["RADIATION_CHK_COUNT"].ToString());
                    //기사명, 기사연락처 추가(2020-04-16 한민호)
                    drv_nm = dt_rfid.Rows[0]["DRV_NM"].ToString();
                    drv_tel_no = dt_rfid.Rows[0]["DRV_TEL_NO"].ToString();
                    //수동접수여부 추가(Y:수동접수, N:자동접수, E:출입관리)(2020-05-06 한민호)
                    manual_yn = dt_rfid.Rows[0]["MANUAL_YN"].ToString();

                    //계량번호 표시는 MAIN계량번호 변경(2019-11-30 한민호)
                    lblWght_No.Text = main_wght_no == null ? "" : main_wght_no;
                    //lblWght_No.Text = wght_no == null ? "" : wght_no;
                    lblVEHL_NO.Text = car_no == null ? "" : car_no;

                    //세인율 출하오차범위 테이블 에서 가져옴(2020-02-18 한민호)
                    /* YK   dwbae   20210217    허용오차범위 MIN/MAX값 분리
                    SEIN_MIN = "-" + dt_rfid.Rows[0]["SEIN"].ToString();
                    SEIN_MAX = "+" + dt_rfid.Rows[0]["SEIN"].ToString();
                     * 
                     */
                    SEIN_MIN = dt_rfid.Rows[0]["SEIN_MIN"].ToString();
                    SEIN_MAX = dt_rfid.Rows[0]["SEIN_MAX"].ToString();


                    ////세인률 소스에 하드 코딩(2019-11-14 오창휘 수정)
                    //SEIN_MIN = "-4";
                    ////세인율은 MAX 초과 된 값 체크 추가_1%(2019-11-21 한민호)
                    //SEIN_MAX = "+1";
                    ////SEIN_MAX = "+4";

                    string calc_wght = "";

                    logger.Info("2");

                    //2019-10-02 오창휘 추가
                    #region 분진차 확인
                    //막음(2019-12-30 대한제강)
                    if (bj_fg == "Y")
                    {
                        //정문일 때
                        //2019-11-01 수정 롤백(2019-11-06 오창휘 수정)
                        //1번 계량대에서도 분진차 확인 가능하게 수정(2019-11-01 오창휘 수정)
                        //if (WEIGHT_NO == "0102" ||
                        //    WEIGHT_NO == "0103"
                        //    )
                        if(WEIGHT_NO == "0100" || 
                            WEIGHT_NO == "0102" || 
                            WEIGHT_NO == "0103" 
                            )
                        {
                            //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                            AW_CHK = "3";
                            player71.Play();

                            //2019-11-01 수정 롤백(2019-11-06 오창휘 수정)
                            //1번 계량대에서도 분진차 확인 가능하게 수정(2019-11-01 오창휘 수정)
                            //la_ment.Text = "분진 품목은 1,2번 계량대에서 계량바랍니다.";
                            la_ment.Text = "분진 품목은 2번 계량대에서 계량바랍니다.";
                            logger.Info(this.ToString() + "<" + car_no + " >분진 품목 차량 ");

                            RedWindowform = new RedWindow();
                            RedWindowform.TopMost = true;

                            //2019-11-01 수정 롤백(2019-11-06 오창휘 수정)
                            //1번 계량대에서도 분진차 확인 가능하게 수정(2019-11-01 오창휘 수정)
                            //RedWindowform.labelControl1.Text = "분진 품목은 1,2번 계량대에서 계량바랍니다.";
                            RedWindowform.labelControl1.Text = "분진 품목은 2번 계량대에서 계량바랍니다.";

                            if (RedWindowform.ShowDialog() == DialogResult.OK)
                            {
                                RedWindowform.Close();
                            }

                            //2019-11-01 수정 롤백(2019-11-06 오창휘 수정)
                            //1번 계량대에서도 분진차 확인 가능하게 수정(2019-11-01 오창휘 수정)
                            //la_ment.Text = "분진 품목은 1,2번 계량대에서 계량바랍니다.";
                            la_ment.Text = "분진 품목은 2번 계량대에서 계량바랍니다.";
                            return;
                        }
                        //후문일 때
                        //2019-11-01 수정 롤백(2019-11-06 오창휘 수정)
                        //5번 계량대에서도 분진차 확인 가능하게 수정(2019-11-01 오창휘 수정)
                        //else if (WEIGHT_NO == "0106" ||
                        //    WEIGHT_NO == "0107"
                        //    )
                        else if (WEIGHT_NO == "0104" ||
                            WEIGHT_NO == "0106" ||
                            WEIGHT_NO == "0107"
                            )
                        {
                            //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                            AW_CHK = "3";
                            player72.Play();
                            
                            //2019-11-01 수정 롤백(2019-11-06 오창휘 수정)
                            //5번 계량대에서도 분진차 확인 가능하게 수정(2019-11-01 오창휘 수정)
                            //la_ment.Text = "분진 품목은 5,6번 계량대에서 계량 바랍니다.";
                            la_ment.Text = "분진 품목은 6번 계량대에서 계량 바랍니다.";
                            logger.Info(this.ToString() + "<" + car_no + " >분진 품목 차량 ");

                            RedWindowform = new RedWindow();
                            RedWindowform.TopMost = true;

                            //2019-11-01 수정 롤백(2019-11-06 오창휘 수정)
                            //5번 계량대에서도 분진차 확인 가능하게 수정(2019-11-01 오창휘 수정)
                            //RedWindowform.labelControl1.Text = "분진 품목은 5,6번 계량대에서 계량 바랍니다.";
                            RedWindowform.labelControl1.Text = "분진 품목은 6번 계량대에서 계량 바랍니다.";

                            if (RedWindowform.ShowDialog() == DialogResult.OK)
                            {
                                RedWindowform.Close();
                            }

                            //2019-11-01 수정 롤백(2019-11-06 오창휘 수정)
                            //5번 계량대에서도 분진차 확인 가능하게 수정(2019-11-01 오창휘 수정)
                            //la_ment.Text = "분진 품목은 5,6번 계량대에서 계량 바랍니다.";
                            la_ment.Text = "분진 품목은 6번 계량대에서 계량 바랍니다.";
                            return;
                        }
                    }

                    #endregion

                    #region 방사능 확인

                    if (RADIATION_CHK == true)
                    {
                        //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                        AW_CHK = "3";
                        DataTable dt_result = null;
                        dt_result = DB_Process.insert_RADIATION_CHK(rfid_seq);

                        //방사능 검출 횟수가 2회 이상일 때 안내음성 및 안내멘트 다르게 출력(2019-11-01 오창휘 수정)
                        if (rdt_chk_count > 0)//처음 배차 정보를 가져올 때는 1이기 때문에 0보다 클 때로 확인
                        {
                            player76.Play();
                            la_ment.Text = "방사능 검출 차량입니다. 차량 이동 후 대기바랍니다.";
                            logger.Info(this.ToString() + "<" + car_no + " >방사능 검출 제한 차량 " + (rdt_chk_count + 1).ToString() + "회");
                            RedWindowform = new RedWindow();
                            RedWindowform.TopMost = true;
                            RedWindowform.labelControl1.Text = "방사능 검출 차량입니다. 차량 이동 후 대기바랍니다.";
                            if (RedWindowform.ShowDialog() == DialogResult.OK)
                            {
                                RedWindowform.Close();
                            }
                            la_ment.Text = "방사능 검출 차량입니다. 차량 이동 후 대기바랍니다.";
                        }
                        else
                        {
                            player74.Play();
                            la_ment.Text = "방사능 검출 차량 입니다. 다시 계량대로 진입하여 계량해주시기 바랍니다.";
                            logger.Info(this.ToString() + "<" + car_no + " >방사능 검출 제한 차량 " + (rdt_chk_count + 1).ToString() + "회");
                            RedWindowform = new RedWindow();
                            RedWindowform.TopMost = true;
                            RedWindowform.labelControl1.Text = "방사능 검출 차량 입니다. 다시 계량대로 진입하여 계량해주시기 바랍니다.";
                            if (RedWindowform.ShowDialog() == DialogResult.OK)
                            {
                                RedWindowform.Close();
                            }
                            la_ment.Text = "방사능 검출 차량 입니다. 다시 계량대로 진입하여 계량해주시기 바랍니다.";
                        }                    
                        //player74.Play();
                        //la_ment.Text = "방사능 검출 차량 입니다. 다시 계량대로 진입하여 계량해주시기 바랍니다.";
                        //logger.Info(this.ToString() + "<" + car_no + " >방사능 검출 제한 차량 ");
                        //RedWindowform = new RedWindow();
                        //RedWindowform.TopMost = true;
                        //RedWindowform.labelControl1.Text = "방사능 검출 차량 입니다. 다시 계량대로 진입하여 계량해주시기 바랍니다.";
                        //if (RedWindowform.ShowDialog() == DialogResult.OK)
                        //{
                        //    RedWindowform.Close();
                        //}
                        //la_ment.Text = "방사능 검출 차량 입니다. 다시 계량대로 진입하여 계량해주시기 바랍니다.";


                        //방사능 검출 시 담당자에게 SMS문자발송(2020-04-16 한민호)
                        string ConnStr = "server=192.168.10.7;database=TMS_SMS;uid=tms;pwd=tms50";
                        SqlConnection sCon = new SqlConnection(ConnStr);
                        //첫번째만 전송되었기 때문에 위치 변경(2020-04-21 한민호)
                        //sCon.Open();
                        //수신자 연락처(방사능 담당)
                        String Query = string.Empty;
                        Query = " SELECT CODE_VALUE1 AS CODE ";
                        Query += " FROM TB_WS01_0002 ";
                        Query += " WHERE TYPE_CD = 'WS_007' ";
                        Query += " AND CODE_VALUE2 = '" + WEIGHT_NO.Substring(0, 4) + "' ";
                        Query += " AND USE_YN = 'Y' ";
                        Query += " AND DEL_YN = 'N' ";
                        Query += " And code_Name like '%방사능%'"; //방사능 문자 출하오차담당자에게 날라갈 우려가있어서 수정(2021-06-02 정성호)
                        ServiceAdapter _svc = new ServiceAdapter();
                        DataSet ds1 = _svc.GetQuery(Query);
                        DataTable dt1 = ds1.Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            for (int Cnt = 0; Cnt < dt1.Rows.Count; Cnt++)
                            {
                                string Addressee = dt1.Rows[Cnt]["CODE"].ToString();
                                //첫번째만 전송되었기 때문에 위치 변경(2020-04-21 한민호)
                                sCon.Open();
                                string strSQL = string.Empty;
                                strSQL = "insert into EM_TRAN( "
                                        + "   TRAN_PHONE "
                                        + " , TRAN_CALLBACK"
                                        + " , TRAN_STATUS "
                                        + " , TRAN_DATE "
                                        + " , TRAN_MSG ) "
                                        + " Values( "
                                        + "   '" + Addressee + "'"
                                        + " , '" + drv_tel_no + "'"
                                        + " , '1' "
                                        + " , '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                                        + " , '방사능검출차량 차량번호:" + car_no + "  운전자:" + drv_nm + "  회사명:" + cust_nm + "') ";
                                SqlCommand cmd = new SqlCommand(strSQL, sCon);
                                int rtn = -1;
                                rtn = cmd.ExecuteNonQuery();
                                if (rtn < 0)
                                {
                                    MessageBox.Show("SMS전송오류");
                                    sCon.Close();
                                    return;
                                }
                                sCon.Close();
                            }
                        }

                        return;
                    }

                    #endregion

                    #region 입차 제한 차량 확인
                    //막음(2019-12-30 대한제강)
                    ////입차 제한 차량 확인 
                    //if (DB_Process.check_car_limit("01", car_no) == "Y")
                    //{
                    //    //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                    //    AW_CHK = "3";
                    //    player08.Play();

                    //    la_ment.Text = "입출차 제한 차량 입니다. ";
                    //    logger.Info(this.ToString() + "<" + car_no + " >입출차 제한 차량 ");
                    //    RedWindowform = new RedWindow();
                    //    RedWindowform.TopMost = true;
                    //    RedWindowform.labelControl1.Text = "입출차 제한 차량 입니다. ";
                    //    if (RedWindowform.ShowDialog() == DialogResult.OK)
                    //    {
                    //        RedWindowform.Close();
                    //    }
                    //    la_ment.Text = "입출차 제한 차량 입니다. ";
                    //    return;
                    //}

                    #endregion

                    logger.Info("3");

                    #region 전량퇴송 차량 제한 차량 확인

                    //막음(2019-12-30 대한제강)
                    ////전량퇴송 차량 제한 차량 확인
                    //if (DB_Process.check_car_limit("03", car_no) == "Y")
                    //{
                    //    //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                    //    AW_CHK = "3";
                    //    player08.Play();

                    //    la_ment.Text = "금일 퇴송차량입니다. 입고 할수 없습니다. ";
                    //    logger.Info(this.ToString() + "<" + car_no + " >금일 퇴송차량입니다. 입고 할수 없습니다. ");
                    //    RedWindowform = new RedWindow();
                    //    RedWindowform.TopMost = true;
                    //    RedWindowform.labelControl1.Text = "금일 퇴송차량입니다. 입고 할수 없습니다. ";
                    //    if (RedWindowform.ShowDialog() == DialogResult.OK)
                    //    {
                    //        RedWindowform.Close();
                    //    }
                    //    la_ment.Text = "금일 퇴송차량입니다. 입고 할수 없습니다. ";
                    //    return;
                    //}

                    #endregion

                    #region LPR 사용시 차량번호 체크

                    //LPR 사용시 차량번호 체크
                    //if (LPR_USE == "Y")
                    //{
                    //    if (lblVEHL_NO.Text.Trim() != AutoNum.Trim())
                    //    {
                    //        frmChkCAR frm = new frmChkCAR();
                    //        frm.RFID_SEQ = rfid_seq;
                    //        frm.CAR_NO1 = AutoNum;
                    //        frm.CAR_NO2 = lblVEHL_NO.Text;
                    //        if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    //        {
                    //            RFID_CODE = frm.RFID_CODE;
                    //            AutoNum = frm.CAR_NO1;
                    //            frm.Close();

                    //            end_timer.Enabled = true;

                    //            return;
                    //        }
                    //        else
                    //        {
                    //            RFID_CODE = frm.RFID_CODE;
                    //            AutoNum = frm.CAR_NO1;
                    //            frm.Close();
                    //        }

                    //        lblVEHL_NO.BackColor = Color.Red;
                    //    }
                    //}

                    #endregion
                    logger.Info("6");

                    //막음(2019-12-30 대한제강)
                    ////크로스체크 로직 추가(2019-10-07 오창휘 수정)
                    ////Thread.Sleep(3000);
                    ////부산물(Z) 크로스 체크 로직 타지 않게 수정(2019-11-15 오창휘 수정)
                    ////부산물이 아닌 SCALE(Z4001) 만 뺄 것(2019-11-15 한민호)
                    //if (LPR_USE == "Y" && item_so.Substring(0, 3) != "E12" && item_so.Substring(0, 1) != "1" && item_so != "G0005" && item_so != "40054" && item_so != "Z4001")
                    ////if (LPR_USE == "Y" && item_so.Substring(0, 3) != "E12" && item_so.Substring(0, 1) != "1" && item_so != "G0005" && item_so != "40054" && item_jung != "Z" )
                    ////레미콘(40054) 크로스 체크 로직 타지 않게 수정(2019-11-14 오창휘 수정)
                    ////if (LPR_USE == "Y" && item_so.Substring(0, 3) != "E12" && item_so.Substring(0, 1) != "1" && item_so != "G0005" && item_so != "40054")
                    ////폐내화물(G0005) 크로스 체크 로직 타지 않게 수정(2019-11-08 오창휘 수정)
                    ////if (LPR_USE == "Y" && item_so.Substring(0, 3) != "E12" && item_so.Substring(0, 1) != "1" && item_so != "G0005")
                    ////부재료(1) 크로스 체크 로직 타지 않게 수정(2019-11-06 오창휘 수정)
                    ////if (LPR_USE == "Y" && item_so.Substring(0, 3) != "E12" && item_so.Substring(0, 1) != "1")
                    ////수입고철(E12)의 경우 크로스 체크 로직 타지 않게 수정(2019-11-01 오창휘 수정)
                    ////if (LPR_USE == "Y" && item_so.Substring(0, 3) != "E12")
                    ////if (LPR_USE == "Y")
                    //{
                    //    if (car_no.Trim() != img_vehl_no.Trim() && img_vehl_no.Trim() != "미인식" && img_vehl_no.Trim() != "")
                    //    {
                    //        //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                    //        AW_CHK = "3";
                    //        player73.Play();
                    //        la_ment.Text = "차량 번호가 LPR 인식된 번호와 다릅니다." + Environment.NewLine +
                    //                        "[ 인식번호 : " + img_vehl_no + "]";

                    //        logger.Info(this.ToString() + "[ 인식번호 : " + img_vehl_no + "] 크로스체크 불일치 ");

                    //        RedWindowform = new RedWindow();
                    //        RedWindowform.TopMost = true;
                    //        RedWindowform.labelControl1.Text = "차량 번호가 LPR 인식된 번호와 다릅니다." + Environment.NewLine +
                    //                        "[ 인식번호 : " + img_vehl_no + "]";

                    //        if (RedWindowform.ShowDialog() == DialogResult.OK)
                    //        {
                    //            RedWindowform.Close();
                    //        }

                    //        la_ment.Text = "차량 번호가 LPR 인식된 번호와 다릅니다." + Environment.NewLine +
                    //                        "[ 인식번호 : " + img_vehl_no + "]";
                    //        return;
                    //    }
                    //}
                    

                    //========================================================================================================================================
                    // 1차 계량값만 있으면 얘는 2차 계량임. 
                    if ((dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString() != "0") &&
                        (dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString() != ""))
                    {
                        #region 2차 계량

                        lblFirst_wght.Text = dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString(); // txtsetting(); //1차 중량값  
                        lblSecond_wght.Text = lblWeight_value.Text; // txtsetting();

                        Gubun = "Y"; // 2차 계량 

                        if(int.Parse(TimeGap) < Gap) 
                        {
                            player20.Play();
                            AW_CHK = "3";
                            la_ment.Text = string.Format("1차 계량후 {0} 이후에 2차 계량을 진행 할 수 있습니다. {1}초 남았습니다.", getSecToMin(), Gap - int.Parse(TimeGap));
                            return;
                        }

                        //---------------------------------------------------------------
                        //품목 대 분류에 따라 1차 계량과 2차 계량의 차이를 체크 하도록 함
                        //---------------------------------------------------------------
                        int ch_1 = Convert.ToInt32(dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString());
                        int ch_2 = Convert.ToInt32(lblWeight_value.Text.Replace(",",""));
                        int ch_3 = 0;

                        // 계량방식  조회
                        if (InOut_Gubun == "1") //원자재,수입원자재,이송입고,부자재/저장품,수입부/저장품,반품
                        {
                            ch_3 = ch_1 - ch_2;
                        }

                        if (InOut_Gubun == "3")
                        {
                            ch_3 = ch_2 - ch_1;
                        }

                        real = ch_3;

                        //폐기물/저장품 및 수동접수는 입출고구분 체크에서 제외(2020-05-06 한민호)
                        if (item_jung == "폐기물/부산물" || manual_yn == "Y")
                        {
                            lblReal_wgt.Text = Math.Abs(ch_3).ToString();
                        }
                        else
                        {
                            //실중량 표현
                            lblReal_wgt.Text = ch_3.ToString(); // txtsetting(ch_3.ToString());
                            // 1. 입고 : 1차 > 2차 정상--------------------------------------- 
                            if ((InOut_Gubun == "1") && (ch_3 < 0)) //2차 값이 더 클 경우 
                            {
                                //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                                AW_CHK = "3";
                                player35.Play();
                                la_ment.Text = " 2차 중량이 더 클경우 - 중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " +
                                               ch_2.ToString() + " > 중량이상 ";
                                logger.Info("2차 중량이 더 클경우 계량번호[ " + wght_no + " ]" + " >< 1차 : " + ch_1.ToString() +
                                            " , 2차 " + ch_2.ToString() + " >");


                                end_timer.Enabled = true;
                                RedWindowform = new RedWindow();
                                RedWindowform.TopMost = true;
                                RedWindowform.labelControl1.Text = " 2차 중량이 더 클경우 - 중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " +
                                               ch_2.ToString() + " > 중량이상 ";

                                if (RedWindowform.ShowDialog() == DialogResult.OK)
                                {
                                    RedWindowform.Close();
                                }
                                la_ment.Text = " 2차 중량이 더 클경우 - 중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " +
                                               ch_2.ToString() + " > 중량이상 ";
                                return;
                            }
                            else if ((InOut_Gubun == "3") && (ch_1 >= ch_2)) //1차 값이 더 큰경우  
                            {
                                //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                                AW_CHK = "3";
                                // 2. 그외 : 1차 < 2차 
                                player35.Play();
                                la_ment.Text = " 1차 중량이 더 클경우 - 중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " +
                                               ch_2.ToString() + " > 중량이상";
                                logger.Info("1차 중량이 더 클경우 계량번호[ " + wght_no + " ]" + " 중량이상 >< 1차 : " + ch_1.ToString() +
                                            " , 2차 " + ch_2.ToString() + " >");


                                end_timer.Enabled = true;
                                RedWindowform = new RedWindow();
                                RedWindowform.TopMost = true;
                                RedWindowform.labelControl1.Text = " 1차 중량이 더 클경우 - 중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " +
                                               ch_2.ToString() + " > 중량이상";
                                if (RedWindowform.ShowDialog() == DialogResult.OK)
                                {
                                    RedWindowform.Close();
                                }
                                la_ment.Text = " 1차 중량이 더 클경우 - 중량이상 >< 1차 : " + ch_1.ToString() + " , 2차 " +
                                               ch_2.ToString() + " > 중량이상";
                                return;
                            }
                            //--------------------------------------------------------------- 
                        }
                        logger.Info("7");
                        Double TOTPROD_WGT = 0.0;
                        Double TOTPMIN_WGT = 0.0;
                        Double TOTPMAX_WGT = 0.0;
                        Double TOTINST_WGT = 0.0;
                        String LOAD_ORD_NO = "";

                        #region 국내고철

                        // 국내고철
                        // 국내고철 구분 추가(2019-10-14 오창휘 수정)
                        // 대한제강(2020-01-29 한민호)
                        if (item_jung == "원자재" || item_jung == "수입원자재" || item_jung == "이송입고" || item_jung == "부자재/저장품" || item_jung == "수입품/저장품")
                        //if (InOut_Gubun == "1" && item_jung == "E" && (item_so.Substring(0, 3) == "E11" || item_so.Substring(0, 3) == "E01"))
                        //if (InOut_Gubun == "1" && item_jung == "E"))
                        {
                            // 대한제강_임시막음(2020-01-29 한민호)
                            //수입원자재 검수 안함(2020-02-17 한민호)
                            if (item_jung == "원자재" || item_jung == "부자재/저장품")
                            //if (item_jung == "원자재" || item_jung == "수입원자재" || item_jung == "부자재/저장품")
                                if (inspect != "Y")
                                {
                                    //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                                    AW_CHK = "3";
                                    player68.Play();
                                    //멘트 수정(2019-10-15 오창휘 수정)
                                    la_ment.Text = "미검수 완료차량!! 현장 검수원에게 확인바람";
                                    //la_ment.Text = "[2차계량]고철등급판정이 완료되지 않았습니다.";
                                    end_timer.Enabled = true;
                                    RedWindowform = new RedWindow();
                                    RedWindowform.TopMost = true;
                                    RedWindowform.labelControl1.Text = "미검수 완료차량!! 현장 검수원에게 확인바람";
                                    if (RedWindowform.ShowDialog() == DialogResult.OK)
                                    {
                                        RedWindowform.Close();
                                    }
                                    //멘트 수정(2019-10-15 오창휘 수정)
                                    la_ment.Text = "미검수 완료차량!! 현장 검수원에게 확인바람";
                                    //la_ment.Text = "[2차계량]고철등급판정이 완료되지 않았습니다.";
                                    return;
                                }

                            Application.DoEvents();


                            // 업체명 및 배차번호 가져오기
                            insert_text2(lblMSG, "■업체명: " + cust_nm);
                            insert_text2(lblMSG, "■배차NO: " + rfid_seq);
                        }

                        #endregion

                        #region 제품

                        // 제품 계산 
                        // 대한제강(2020-01-29 한민호)
                        if (item_jung == "철근판매" || item_jung == "가공철근" || item_jung == "빌렛출하" || item_jung == "이송출하")
                        //if (InOut_Gubun == "3" && item_jung == "7")
                        {
                            dt_result_Display = DB_Process.RESULT_DISPLAY(rfid_seq);
                            if (dt_result_Display.Rows.Count > 0)
                            {
                                for (int i = 0; i <= dt_result_Display.Rows.Count - 1; i++)
                                {
                                    // 대한제강 검수는 가공철근 제외(2020-01-30 한민호)
                                    //빌렛은 상차완료실적 없음(2020-03-19 한민호) 
                                    if (item_jung == "철근판매" || (item_jung == "이송출하" && item_so_nm.Substring(0,2) == "철근"))
                                    //if (item_jung == "철근판매" || item_jung == "빌렛출하" || item_jung == "이송출하")
                                    {
                                        if (dt_result_Display.Rows[i]["INSPECT_YN"].ToString() != "Y")
                                        {
                                            //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                                            AW_CHK = "3";
                                            logger.Info("제품 검수가 완료되지 않았습니다.  <" +
                                                        dt_result_Display.Rows[i]["WGHT_NO"].ToString() + "> ");

                                            player35.Play();
                                            la_ment.Text = "제품 검수가 완료되지 않았습니다. <" +
                                                           dt_result_Display.Rows[i]["LOAD_ORD_NO"].ToString() + "> ";

                                            end_timer.Enabled = true;
                                            RedWindowform = new RedWindow();
                                            RedWindowform.TopMost = true;
                                            RedWindowform.labelControl1.Text = "제품 검수가 완료되지 않았습니다. <" +
                                                           dt_result_Display.Rows[i]["LOAD_ORD_NO"].ToString() + "> ";
                                            if (RedWindowform.ShowDialog() == DialogResult.OK)
                                            {
                                                RedWindowform.Close();
                                            }
                                            la_ment.Text = "제품 검수가 완료되지 않았습니다. <" +
                                                           dt_result_Display.Rows[i]["LOAD_ORD_NO"].ToString() + "> ";
                                            return;
                                        }
                                    }

                                    LOAD_ORD_NO = dt_result_Display.Rows[i]["LOAD_ORD_NO"].ToString();

                                    // 상차지시번호 없어서 고철과 동일하게 바꿈(2019-10-31 오창휘 수정)
                                    // 업체명 및 배차번호 가져오기
                                    insert_text2(lblMSG, "■업체명: " + cust_nm);
                                    insert_text2(lblMSG, "■배차NO: " + rfid_seq);
                                    //insert_text2(lblMSG, "■상차지시번호: " + LOAD_ORD_NO);

                                    TOTPROD_WGT = TOTPROD_WGT + Convert.ToInt32(dt_result_Display.Rows[i]["PROD_WGHT"]); //지시중량
                                    //DB에 있는 MIN MAX 값 사용하지 않음(2019-11-14 오창휘 수정)
                                    TOTPMIN_WGT = TOTPMIN_WGT + double.Parse(SEIN_MIN);
                                    TOTPMAX_WGT = TOTPMAX_WGT + double.Parse(SEIN_MAX);
                                    //TOTPMIN_WGT = TOTPMIN_WGT + Convert.ToInt32(dt_result_Display.Rows[i]["PMIN_WGHT"]);
                                    //TOTPMAX_WGT = TOTPMAX_WGT + Convert.ToInt32(dt_result_Display.Rows[i]["PMAX_WGHT"]);
                                }

                                //반품이 있기 때문에 입출고구분(1:입고계량, 3:출고계량)으로 처리(2020-05-14 한민호)
                                if (InOut_Gubun == "1")
                                {
                                    ch_3 = ch_1 - ch_2;
                                }

                                if (InOut_Gubun == "3")
                                {
                                    ch_3 = ch_2 - ch_1;
                                }
                                //ch_3 = ch_2 - ch_1;

                                lblSecond_wght.Text = ch_2.ToString();
                                lblProd_wgt.Text = TOTPROD_WGT.ToString();

                                Sein = (((ch_3 - TOTPROD_WGT) / TOTPROD_WGT) * 100.0);
                                Sein_Point = (((ch_3 - TOTPROD_WGT) / TOTPROD_WGT) * 100.0) * 0.0;

                                calc_wght = Convert.ToString(ch_3 - TOTPROD_WGT);

                                pmax_wght = TOTPMAX_WGT.ToString();
                                pmin_wght = TOTPMIN_WGT.ToString();

                                ////세인률검사 로직 추가(2021-10-01 정성호 수정)
                                //if(plnt_no == "1200" && item_type == "30" && site_nm == "평택 압연 공장")
                                //{
                                //    TMP_SEIN_MIN = "-7";
                                //    TMP_SEIN_MAX = "7";
                                //}
                                //else
                                //{
                                //    TMP_SEIN_MIN = SEIN_MIN;
                                //    TMP_SEIN_MAX = SEIN_MAX;
                                //}

                                //if (Sein > double.Parse(SEIN_MAX) || Sein < double.Parse(SEIN_MIN))
                                //{
                                //    /*
                                //    logger.Info("계량을 승인 하시겠습니까?  세인량 범위초과 <" + Sein.ToString() + "> ");

                                //    player35.Play();

                                //    if (DialogResult.Cancel ==
                                //        MessageOpen(
                                //            "중량차이(" + Convert.ToString(Math.Abs(ch_3 - TOTPROD_WGT)) +
                                //            " 가 큽니다.계량을 승인하시겠습니까?", "세인량 범위초과"))
                                //    {
                                //        end_timer.Enabled = true;
                                //        return;
                                //    }
                                //    */
                                //}

                                //세인률 로직 추가(2019-11-14 오창휘 수정)
                                logger.Info("세인률 <" + Sein.ToString() + "> ");
                                logger.Info("세인률 MIN <" + double.Parse(SEIN_MIN).ToString() + "> ");
                                logger.Info("세인률 MAX <" + double.Parse(SEIN_MAX).ToString() + "> ");

                                //세인율은 MAX 초과 된 값 체크 추가_1%(2019-11-21 한민호)
                                // 대한제강(2020-01-29 한민호)
                                //오차범위 대상 은 이송출하이고 품명구분이 철근만 할 것(2020-03-19 한민호)
                                //오차범위 가공철근 제외(2020-03-25 한민호)
                                if ((item_jung == "철근판매" || (item_jung == "이송출하" && item_so_nm.Substring(0, 2) == "철근"))
                                    && (Sein > double.Parse(SEIN_MAX) || Sein < double.Parse(SEIN_MIN)))
                                //if ((item_jung == "철근판매" || item_jung == "가공철근" || (item_jung == "이송출하" && item_so_nm.Substring(1, 4) == "철근")) && (Sein > double.Parse(SEIN_MAX) || Sein < double.Parse(SEIN_MIN)))
                                //if ((item_jung == "철근판매" || item_jung == "가공철근" || item_jung == "빌렛출하" || item_jung == "이송출하") && (Sein > double.Parse(SEIN_MAX) || Sein < double.Parse(SEIN_MIN)))
                                //if ((item_so == "70001" || item_so == "70002" || item_so == "70003") && (Sein > double.Parse(SEIN_MAX) || Sein < double.Parse(SEIN_MIN))) //품목이 철근이나 단조일 때, 세인률은 MIN만 체크
                                //if ((item_so == "70001" || item_so == "70002" || item_so == "70003") && (Sein < double.Parse(SEIN_MIN)) ) //품목이 철근이나 단조일 때, 세인률은 MIN만 체크
                                {

                                    AW_CHK = "3";
                                    //대한제강(2020-02-18 한민호)
                                    logger.Info("출하오차범위 초과 <" + Sein.ToString() + "> ");
                                    //logger.Info("계량을 승인 하시겠습니까?  세인량 범위초과 <" + Sein.ToString() + "> ");

                                    player35.Play();

                                    //대한제강(2020-02-18 한민호)
                                    la_ment.Text = "출하오차범위 초과(" + Convert.ToString(ch_3 - TOTPROD_WGT) + ", " + Math.Round(Sein, 3).ToString() + "% )";

                                    end_timer.Enabled = true;
                                    RedWindowform = new RedWindow();
                                    RedWindowform.TopMost = true;
                                    RedWindowform.labelControl1.Text = "출하오차범위 초과(" + Convert.ToString(ch_3 - TOTPROD_WGT) + ", " + Math.Round(Sein, 3).ToString() + "% )";
                                    if (RedWindowform.ShowDialog() == DialogResult.OK)
                                    {
                                        RedWindowform.Close();
                                    }
                                    la_ment.Text = "출하오차범위 초과(" + Convert.ToString(ch_3 - TOTPROD_WGT) + ", " + Math.Round(Sein, 3).ToString() + "% )";


                                    //출차오차범위 초과 시 담당자에게 SMS문자발송(2020-04-16 한민호)
                                    string ConnStr = "server=192.168.10.7;database=TMS_SMS;uid=tms;pwd=tms50";
                                    SqlConnection sCon = new SqlConnection(ConnStr);
                                    //첫번째만 전송되었기 때문에 위치 변경(2020-04-21 한민호)
                                    //sCon.Open();
                                    //수신자 연락처(방사능 담당)
                                    String Query = string.Empty;

                                    Query = "SELECT CODE_VALUE1 AS CODE ";
                                    Query += " FROM TB_WS01_0002 ";
                                    Query += " WHERE TYPE_CD = 'WS_007' ";
                                    //Query += " AND CODE_VALUE2 = '9999' "; //(2021-05-18 정성호) 원본
                                    Query += " AND CODE_VALUE2 in ('9999','" + WEIGHT_NO.Substring(0,4) + "') "; //(2021-05-18 정성호) SMS문자 발송 조건 추가 해당공장 인원과 9999(전체)만 발송되도록
                                    Query += " AND USE_YN = 'Y' ";
                                    Query += " AND DEL_YN = 'N' ";
                                    Query += " AND CODE_NAME = '출하오차범위 담당자'"; //(2021-06-02 정성호) 방사능 담당자에게도 문자가 날라가는것 때문에 수정

                                    ServiceAdapter _svc = new ServiceAdapter();
                                    DataSet ds1 = _svc.GetQuery(Query);
                                    DataTable dt1 = ds1.Tables[0];
                                    if (dt1.Rows.Count > 0)
                                    {
                                        string plnt_nm = "";
                                        if (WEIGHT_NO.Substring(0, 2) == "11")
                                            plnt_nm = "[신평공장]";
                                        else if (WEIGHT_NO.Substring(0, 2) == "12")
                                            plnt_nm = "[녹산공장]";
                                        else if (WEIGHT_NO.Substring(0, 2) == "13")
                                            plnt_nm = "[평택공장]";


                                        for (int Cnt = 0; Cnt < dt1.Rows.Count; Cnt++)
                                        {
                                            string Addressee = dt1.Rows[Cnt]["CODE"].ToString();
                                            //첫번째만 전송되었기 때문에 위치 변경(2020-04-21 한민호)
                                            sCon.Open();
                                            string strSQL = string.Empty;
                                            strSQL = "insert into EM_TRAN( "
                                                    + "   TRAN_PHONE "
                                                    + " , TRAN_CALLBACK"
                                                    + " , TRAN_STATUS "
                                                    + " , TRAN_DATE "
                                                    + " , TRAN_MSG ) "
                                                    + " Values( "
                                                    + "   '" + Addressee + "'"
                                                    + " , '" + drv_tel_no + "'"
                                                    + " , '1' "
                                                    + " , '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                                                    + " , '출하오차범위 초과(" + Convert.ToString(ch_3 - TOTPROD_WGT) + ", " + Math.Round(Sein, 3).ToString() + "% )  차량번호:" + car_no + "  운전자:" + drv_nm + "  " + plnt_nm + " ') ";
                                            SqlCommand cmd = new SqlCommand(strSQL, sCon);
                                            int rtn = -1;
                                            rtn = cmd.ExecuteNonQuery();
                                            if (rtn < 0)
                                            {
                                                MessageBox.Show("SMS전송오류");
                                                sCon.Close();
                                                return;
                                            }
                                            sCon.Close();
                                        }
                                    }

                                    return;

                                    //if (DialogResult.Cancel ==
                                    //    MessageOpen(
                                    //        "중량차이(" + Convert.ToString(ch_3 - TOTPROD_WGT) + ", " + Math.Round(Sein, 3).ToString() + "% )" + Environment.NewLine +
                                    //        "계량을 승인하시겠습니까?", "세인량 범위초과"))
                                    //{
                                    //    //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                                    //    AW_CHK = "3";
                                    //    end_timer.Enabled = true;
                                    //    return;
                                    //}
                                }
                                else if (plnt_no == "1200" && item_type == "30" && site_nm == "평택 압연 공장" && item_so_nm.Substring(0, 2) == "빌릿"
                                         && (Sein > double.Parse(TMP_SEIN_MAX) || Sein < double.Parse(TMP_SEIN_MIN))) //녹산->평택 빌렛출하 오차체크 로직추가 (2021-10-05 정성호)
                                //else if (plnt_no == "1100" && item_type == "30" && site_nm == "녹산 공장" && item_so_nm.Substring(0, 2) == "빌릿"
                                //         && (Sein > double.Parse(TMP_SEIN_MAX) || Sein < double.Parse(TMP_SEIN_MIN))) 
                                {
                                    //대한제강(2020-02-18 한민호)
                                    logger.Info("빌릿출하오차범위 초과 <" + Sein.ToString() + "> ");
                                    //logger.Info("계량을 승인 하시겠습니까?  세인량 범위초과 <" + Sein.ToString() + "> ");
                                    
                                    AW_CHK = "3";

                                    player35.Play();

                                    logger.Info("빌릿출하오차 음성");

                                    //대한제강(2020-02-18 한민호)
                                    la_ment.Text = "출하오차범위 초과(" + Convert.ToString(ch_3 - TOTPROD_WGT) + ", " + Math.Round(Sein, 3).ToString() + "% )";
                                    logger.Info("빌릿출하오차 la_ment 값지정");

                                    end_timer.Enabled = true;
                                    RedWindowform = new RedWindow();
                                    RedWindowform.TopMost = true;
                                    RedWindowform.labelControl1.Text = "출하오차범위 초과(" + Convert.ToString(ch_3 - TOTPROD_WGT) + ", " + Math.Round(Sein, 3).ToString() + "% )";

                                    logger.Info("빌릿출하오차 메시지 창 실행");
                                    if (RedWindowform.ShowDialog() == DialogResult.OK)
                                    {
                                        RedWindowform.Close();
                                    }
                                    logger.Info("빌릿출하오차 메시지 창 종료");

                                    la_ment.Text = "출하오차범위 초과(" + Convert.ToString(ch_3 - TOTPROD_WGT) + ", " + Math.Round(Sein, 3).ToString() + "% )";
                                    logger.Info("빌릿출하오차 la_ment 값지정");

                                    //출차오차범위 초과 시 담당자에게 SMS문자발송(2020-04-16 한민호)
                                    string ConnStr = "server=192.168.10.7;database=TMS_SMS;uid=tms;pwd=tms50";
                                    SqlConnection sCon = new SqlConnection(ConnStr);
                                    //첫번째만 전송되었기 때문에 위치 변경(2020-04-21 한민호)
                                    //sCon.Open();
                                    //수신자 연락처(방사능 담당)
                                    String Query = string.Empty;

                                    Query = "SELECT CODE_VALUE1 AS CODE ";
                                    Query += " FROM TB_WS01_0002 ";
                                    Query += " WHERE TYPE_CD = 'WS_007' ";
                                    //Query += " AND CODE_VALUE2 = '9999' "; //(2021-05-18 정성호) 원본
                                    Query += " AND CODE_VALUE2 in ('9999','" + WEIGHT_NO.Substring(0,4) + "') "; //(2021-05-18 정성호) SMS문자 발송 조건 추가 해당공장 인원과 9999(전체)만 발송되도록
                                    Query += " AND USE_YN = 'Y' ";
                                    Query += " AND DEL_YN = 'N' ";
                                    Query += " AND CODE_NAME = '출하오차범위 담당자'"; //(2021-06-02 정성호) 방사능 담당자에게도 문자가 날라가는것 때문에 수정

                                    ServiceAdapter _svc = new ServiceAdapter();
                                    DataSet ds1 = _svc.GetQuery(Query);
                                    DataTable dt1 = ds1.Tables[0];
                                    if (dt1.Rows.Count > 0)
                                    {
                                        string plnt_nm = "";
                                        if (WEIGHT_NO.Substring(0, 2) == "11")
                                            plnt_nm = "[신평공장]";
                                        else if (WEIGHT_NO.Substring(0, 2) == "12")
                                            plnt_nm = "[녹산공장]";
                                        else if (WEIGHT_NO.Substring(0, 2) == "13")
                                            plnt_nm = "[평택공장]";


                                        for (int Cnt = 0; Cnt < dt1.Rows.Count; Cnt++)
                                        {
                                            string Addressee = dt1.Rows[Cnt]["CODE"].ToString();
                                            //첫번째만 전송되었기 때문에 위치 변경(2020-04-21 한민호)
                                            sCon.Open();
                                            string strSQL = string.Empty;
                                            strSQL = "insert into EM_TRAN( "
                                                    + "   TRAN_PHONE "
                                                    + " , TRAN_CALLBACK"
                                                    + " , TRAN_STATUS "
                                                    + " , TRAN_DATE "
                                                    + " , TRAN_MSG ) "
                                                    + " Values( "
                                                    + "   '" + Addressee + "'"
                                                    + " , '" + drv_tel_no + "'"
                                                    + " , '1' "
                                                    + " , '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                                                    + " , '출하오차범위 초과(" + Convert.ToString(ch_3 - TOTPROD_WGT) + ", " + Math.Round(Sein, 3).ToString() + "% )  차량번호:" + car_no + "  운전자:" + drv_nm + "  " + plnt_nm + " ') ";
                                            SqlCommand cmd = new SqlCommand(strSQL, sCon);
                                            int rtn = -1;
                                            rtn = cmd.ExecuteNonQuery();

                                            logger.Info("빌릿출하오차 문자발송" + Addressee + "," + drv_tel_no);
                                            if (rtn < 0)
                                            {
                                                MessageBox.Show("SMS전송오류");
                                                logger.Info("빌릿출하오차 문자오류" + Addressee + "," + drv_tel_no);
                                                sCon.Close();
                                                return;
                                            }
                                            sCon.Close();
                                        }
                                    }

                                    return;
                                }

                            }
                        }

                        #region 철근 단척

                        // 철근 단척
                        if (InOut_Gubun == "3" && item_jung == "Z")
                        {
                            dt_result_Display = DB_Process.RESULT_DISPLAY(rfid_seq);
                            if (dt_result_Display.Rows.Count > 0)
                            {
                                for (int i = 0; i <= dt_result_Display.Rows.Count - 1; i++)
                                {
                                    if (dt_result_Display.Rows[i]["INSPECT_YN"].ToString() != "Y")
                                    {
                                        //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                                        AW_CHK = "3";
                                        logger.Info("검수가 완료되지 않았습니다.  <" +
                                                    dt_result_Display.Rows[i]["WGHT_NO"].ToString() + "> ");

                                        player35.Play();
                                        la_ment.Text = "검수가 완료되지 않았습니다. <" +
                                                       dt_result_Display.Rows[i]["LOAD_ORD_NO"].ToString() + "> ";

                                        end_timer.Enabled = true;
                                        RedWindowform = new RedWindow();
                                        RedWindowform.TopMost = true;
                                        RedWindowform.labelControl1.Text = "검수가 완료되지 않았습니다. <" +
                                                       dt_result_Display.Rows[i]["LOAD_ORD_NO"].ToString() + "> ";
                                        if (RedWindowform.ShowDialog() == DialogResult.OK)
                                        {
                                            RedWindowform.Close();
                                        }
                                        la_ment.Text = "검수가 완료되지 않았습니다. <" +
                                                       dt_result_Display.Rows[i]["LOAD_ORD_NO"].ToString() + "> ";
                                        return;
                                    }

                                    LOAD_ORD_NO = dt_result_Display.Rows[i]["LOAD_ORD_NO"].ToString();

                                    // 상차지시번호 없어서 고철과 동일하게 바꿈(2019-10-31 오창휘 수정)
                                    // 업체명 및 배차번호 가져오기
                                    insert_text2(lblMSG, "■업체명: " + cust_nm);
                                    insert_text2(lblMSG, "■배차NO: " + rfid_seq);
                                    //insert_text2(lblMSG, "■상차지시번호: " + LOAD_ORD_NO);

                                    //세인율 체크 로직 주석 품(2019-10-07 오창휘 수정) START
                                    TOTPROD_WGT = TOTPROD_WGT + Convert.ToInt32(dt_result_Display.Rows[i]["PROD_WGHT"]);
                                    //DB에 있는 MIN MAX 값 사용하지 않음(2019-11-14 오창휘 수정)
                                    TOTPMIN_WGT = TOTPMIN_WGT + double.Parse(SEIN_MIN);
                                    TOTPMAX_WGT = TOTPMAX_WGT + double.Parse(SEIN_MAX);
                                    //TOTPMIN_WGT = TOTPMIN_WGT + Convert.ToInt32(dt_result_Display.Rows[i]["PMIN_WGHT"]);
                                    //TOTPMAX_WGT = TOTPMAX_WGT + Convert.ToInt32(dt_result_Display.Rows[i]["PMAX_WGHT"]);
                                    //세인율 체크 로직 주석 품(2019-10-07 오창휘 수정) END

                                    TOTINST_WGT = TOTINST_WGT + Convert.ToInt32(dt_result_Display.Rows[i]["INST_WGT"]);
                                }

                                ch_3 = ch_2 - ch_1;
                                lblSecond_wght.Text = ch_2.ToString();
                                //세인율 체크 로직 주석 품(2019-10-07 오창휘 수정) START
                                lblProd_wgt.Text = TOTPROD_WGT.ToString();
                                //lblProd_wgt.Text = txtsetting(TOTPROD_WGT.ToString());

                                Sein = (((ch_3 - TOTPROD_WGT) / TOTPROD_WGT) * 100.0);
                                Sein_Point = (((ch_3 - TOTPROD_WGT) / TOTPROD_WGT) * 100.0) * 0.0;

                                calc_wght = Convert.ToString(ch_3 - TOTPROD_WGT);

                                pmax_wght = TOTPMAX_WGT.ToString();
                                pmin_wght = TOTPMIN_WGT.ToString();
                                //세인율 체크 로직 주석 품(2019-10-07 오창휘 수정) END

                                //자리수1자임 수정(2019-11-05 한민호)
                                if (item_jung == "Z" && (dt_result_Display.Rows[0]["RMW_SALES_TP"].ToString().Substring(0, 1)) == "A")
                                    //if (item_jung == "Z" &&
                                    //    (dt_result_Display.Rows[0]["RMW_SALES_TP"].ToString().Substring(1, 1)) == "A")
                                {
                                    if (ch_3 < TOTINST_WGT)
                                    {
                                        //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                                        AW_CHK = "3";

                                        logger.Info("지시미달 중량입니다. 추가 상차하세요(출차하세요)");

                                        player35.Play();
                                        la_ment.Text = "지시미달 중량입니다. 추가 상차하세요(출차하세요) ";

                                        end_timer.Enabled = true;
                                        RedWindowform = new RedWindow();
                                        RedWindowform.TopMost = true;
                                        RedWindowform.labelControl1.Text = "지시미달 중량입니다. 추가 상차하세요(출차하세요) ";
                                        if (RedWindowform.ShowDialog() == DialogResult.OK)
                                        {
                                            RedWindowform.Close();
                                        }
                                        la_ment.Text = "지시미달 중량입니다. 추가 상차하세요(출차하세요) ";
                                        return;
                                    }
                                }

                                //if (Sein > double.Parse(SEIN_MAX) || Sein < double.Parse(SEIN_MIN))
                                //{
                                //    logger.Info("계량을 승인 하시겠습니까?  세인량 범위초과 <" + Sein.ToString() + "> ");

                                //    player35.Play();

                                //    if (DialogResult.Cancel ==
                                //        MessageOpen(
                                //            "중량차이(" + Convert.ToString(Math.Abs(ch_3 - TOTPROD_WGT)) +
                                //            " 가 큽니다.계량을 승인하시겠습니까?", "세인량 범위초과"))
                                //    {
                                //        end_timer.Enabled = true;
                                //        return;
                                //    }
                                //}
                            }
                        }

                        #endregion

                        #endregion

                        #endregion 2차 계량
                    }
                    else
                    {
                        #region 1차 계량

                        // 국고
                        // 국내고철 구분 추가(2019-10-14 오창휘 수정)
                        if (InOut_Gubun == "1" && item_jung == "E" && (item_so.Substring(0, 3) == "E11" || item_so.Substring(0, 3) == "E01"))
                        //if (InOut_Gubun == "1" && item_jung == "E")
                        {

                            if (lblWeight_value.Text != "")
                            {
                                if (Convert.ToInt32(lblWeight_value.Text.Replace(",", "")) > 44010)
                                {
                                    player35.Play();
                                    logger.Info(" 중량값이 44톤을 넘었습니다. 계량할수 없습니다.  <" + lblWeight_value.Text + "> ");
                                    la_ment.Text = "중량값이 44톤을 넘었습니다. 계량할수 없습니다. ";

                                    end_timer.Enabled = true;
                                    RedWindowform = new RedWindow();
                                    RedWindowform.TopMost = true;
                                    RedWindowform.labelControl1.Text = "중량값이 44톤을 넘었습니다. 계량할수 없습니다. ";
                                    if (RedWindowform.ShowDialog() == DialogResult.OK)
                                    {
                                        RedWindowform.Close();
                                    }
                                    la_ment.Text = "중량값이 44톤을 넘었습니다. 계량할수 없습니다. ";
                                    //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                                    AW_CHK = "3";
                                    // 중량초과 계량 가능하게 수정(2019-10-08 오창휘 수정)
                                    OVER_WGT = true;
                                    return;
                                }
                                else
                                {
                                    OVER_WGT = false;
                                }
                            }

                            //2019-10-15 오창휘 추가
                            #region 배차순번 체크
                            if (DB_Process.enter_car_search(rfid_seq, rfid_no) == "Y")
                            {
                                //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                                AW_CHK = "3";
                                la_ment.Text = "계량 할 배차의 입차예정시각 보다 빠른 배차가 존재합니다. 계량불가";
                                logger.Info(this.ToString() + "<" + rfid_seq + " > 계량 할 배차의 입차예정시각 보다 빠른 배차가 존재합니다. 계량불가 ");
                                RedWindowform = new RedWindow();
                                RedWindowform.TopMost = true;
                                RedWindowform.labelControl1.Text = "계량 할 배차의 입차예정시각 보다 빠른 배차가 존재합니다. 계량불가";
                                if (RedWindowform.ShowDialog() == DialogResult.OK)
                                {
                                    RedWindowform.Close();
                                }
                                la_ment.Text = "계량 할 배차의 입차예정시각 보다 빠른 배차가 존재합니다. 계량불가";
                                return;
                            }

                            #endregion

                            insert_text2(lblMSG, "■업체명: " + cust_nm);
                            insert_text2(lblMSG, "■배차NO: " + rfid_seq);
                        }

                        //1차 계량 
                        Gubun = "N";

                        #endregion
                    }

                    //알림 화면 초기화 
                    la_ment.Text = "";

                    //스틸컷 이미지 경로 추가(2020-02-10 오창휘 수정)
                    StillCut();

                    //스틸컷 이미지 경로 추가(2020-02-10 오창휘 수정)
                    //Gubun = N - 1차, Y - 2차
                    weight_fg = DB_Process.weight_fg(item_so, Gubun, Convert.ToInt32(tmp_down.Replace(",", "")), rfid_seq, wght_no, Weight_Area, file_path1, file_path2);

                    //weight_fg = DB_Process.weight_fg(item_so, Gubun, Convert.ToInt32(tmp_down.Replace(",", "")), rfid_seq, wght_no, Weight_Area);
                    logger.Info(" weight_fg = " + weight_fg);
                    //==============================================================================================================================

                    logger.Info(" 저장 구분 : " + weight_fg);

                    //==============================================================================================================================
                    //weight_fg :true 계량저장이 정상적으로 되었음. -> 계량표 출력 
                    if (weight_fg == true)
                    {
                        //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                        AW_CHK = "3";

                        //계량표 출력 
                        //if (InOut_Gubun == "3" && item_jung == "7")

                        // 친환경 철강 슬러그일 경우만 계량표 출력(2019-10-15 오창휘 수정)
                        //if (item_so == "G0001" && (WEIGHT_NO == "0100" || WEIGHT_NO == "0104")) // 라이브
                        ////if (item_so == "G0001" ) // 테스트
                        //{
                        //    logger.Info(" 계량표 출력 시작1");
                        //    if (dt_result_Display != null && dt_result_Display.Rows.Count > 0)
                        //    {
                        //        logger.Info(" 계량표 출력 시작2");
                        //        for (int i = 0; i <= dt_result_Display.Rows.Count - 1; i++)
                        //        {
                        //            logger.Info(" 계량표 출력 시작3");
                        //            by_Matrial(Gubun, InOut_Gubun, item_jung, item_so, dt_result_Display.Rows[i]["RFID_SEQ"].ToString(), dt_result_Display.Rows[i]["WGHT_NO"].ToString());
                        //            print_check();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    //by_Matrial(Gubun, InOut_Gubun, item_jung, item_so, rfid_seq, wght_no_print);
                        //    //print_check();
                        //}

                        if (Gubun == "N") //1차
                        {
                            lblFirst_wght.Text = tmp_down.ToString();

                            dt_result_Display = DB_Process.RESULT_DISPLAY(rfid_seq); 
                            if (dt_result_Display.Rows.Count > 0)
                            {
                                // 결과 값 DISPALY
                                lblRfid_no.Text = rfid_no;
                                lblItem_Nm.Text = item_so_nm;
                                lblVEHL_NO.Text = car_no;
                                lblFirst_wght.Text = dt_result_Display.Rows[0]["LOAD_WEIGHT"].ToString();
                                //계량번호 표시는 MAIN계량번호 변경(2019-11-30 한민호)
                                lblWght_No.Text = dt_result_Display.Rows[0]["MAIN_WGHT_NO"].ToString();
                                //lblWght_No.Text = dt_result_Display.Rows[0]["WGHT_NO"].ToString();

                                player01.Play();
                                la_ment.Text = "1차 계량이 완료 되었습니다. " + (OVER_WGT ? "해당 차량은 중량이 44톤이 넘었습니다." : "");
                                OVER_WGT = false;
                                logger.Info(" 1차 계량 >< " + rfid_seq + " > 1차 계량 완료 ");
                                //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                                AW_CHK = "3";
                                //인디게이터 타이머(2019-10-01 김상우 차장 추가)
                                pid_timer.Start();
                                pid_timer.Enabled = true;

                                //고철차는 1차계량 후 계량표 출력(2020-04-15 한민호)
                                if (item_jung == "원자재" || item_jung == "수입원자재")
                                {
                                    dt_pnt_qty = DB_Process.pnt_qty("N", InOut_Gubun, item_jung, item_so, rfid_seq);

                                    if(dt_pnt_qty.Rows[0]["ITEM_TYPE_NM"].ToString() == "원자재")
                                    {
                                        SubMatrial_원자재 Print_etc = new SubMatrial_원자재(dt_pnt_qty);

                                        Print_etc.Print();
                                        Print_etc.Dispose();
                                    }
                                    else
                                    {
                                        SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);

                                        Print_etc.Print();
                                        Print_etc.Dispose();
                                    }

                                    //미리보기 화면
                                    //Print_etc.ShowPreview();
                                    //인쇄
                                }

                                //실적 img_vehl_no, img_file_nm update(2019-10-04 김상우 차장 수정)
                                //if (list.Length > 0)
                                //{
                                //    updatepath(list);
                                //}
                            }

                            //실적에에 LPR 정보 업데이트
                            if (list.Length > 0)
                            {
                                updatepath(list, rfid_seq);
                            }
                            //국내고철의 경우 하차지 안내 문자 전송(2019-10-16 오창휘 수정)
                            if (InOut_Gubun == "1" && item_jung == "E" && (item_so.Substring(0, 3) == "E11" || item_so.Substring(0, 3) == "E01"))
                            {
                                string rtn = DB_Process.sendSMS(rfid_seq);
                            }

                            return;
                        }
                        else
                        {
                            //제품
                            // 대한제강(2020-01-29 한민호)
                            if (item_jung == "철근판매" || item_jung == "가공철근" || item_jung == "빌렛출하" || item_jung == "이송출하")
                            //if (InOut_Gubun == "3" && item_jung == "7")
                            {
                                int ch_1 = Convert.ToInt32(dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString());
                                int ch_2 = Convert.ToInt32(tmp_down);
                                lblSecond_wght.Text = tmp_down;
                                lblReal_wgt.Text = System.Math.Abs(ch_2 - ch_1).ToString();

                                //계량표 출력 멘트 변경(2019-10-15 오창휘 수정)
                                //대한제강(2020-02-18 한민호)
                                //if (item_so == "G0001" && (WEIGHT_NO == "0100" || WEIGHT_NO == "0104"))// 친환경 철강 슬러그일 경우만 계량표 출력(2019-10-15 오창휘 수정)
                                //{
                                //    player65.Play();//계량이 완료되었습니다. 계량표를 확인 후 출발해주세요.
                                //}
                                //else
                                //{
                                //    player02.Play();//2차계량이 완료되었습니다. 천천히 출발해주세요.
                                //}
                                //평택은 부자재/저장품, 폐기물/부산물만 2차계량 전표 출력 되게 요청(2020-05-11 한민호)
                                //평택공장 제한없이 전부 출력되도록 재수정(2022-01-12 정성호)
                                //if (WEIGHT_NO.Substring(0, 2) == "13")
                                //    player02.Play();//2차계량이 완료되었습니다. 천천히 출발해주세요.
                                //else
                                    player65.Play();//계량이 완료되었습니다. 계량표를 확인 후 출발해주세요.
                                //player65.Play();//계량이 완료되었습니다. 계량표를 확인 후 출발해주세요.

                                logger.Info("세인률 MIN <" + double.Parse(SEIN_MIN).ToString() + "> ");
                                logger.Info("세인률 MAX <" + double.Parse(SEIN_MAX).ToString() + "> ");

                                // 계량 완료 후 멘트 수정(2019-11-14 오창휘 수정)
                                la_ment.Text = "2차 계량이 완료 되었습니다. " + Environment.NewLine +
                                               "( " + calc_wght + " kg / " + Math.Round(Sein, 3).ToString() + " % )";
                                //la_ment.Text = "2차 계량이 완료 되었습니다. " + Math.Round(Sein, 3).ToString() + " ( MAX : " +
                                //               pmax_wght + " / MIN : " + pmin_wght + " )";
                                logger.Info(" 2차 계량 완료후  >< " + rfid_seq + " > 계량표 정상 출력 ");

                                //2차계량은 행선지 안내 하지 않음(2019-10-31 오창휘 수정)
                                //인디게이터 타이머(2019-10-01 김상우 차장 추가)
                                //pid_timer.Start();
                                //pid_timer.Enabled = true;
                            }
                            // 국고

                            //else if (InOut_Gubun == "1" && item_jung == "E")
                            //{
                            //    String auto_value = " SELECT A.WGHT_NO, SUM(B.PROD_WGHT) PROD_WGHT FROM TB_WS02_0002 A "
                            //                        + " LEFT OUTER JOIN TB_WS02_0002 B ON A.RFID_NO = B.RFID_NO AND A.IF_NO = B.IF_NO AND A.CRT_DTM = B.CRT_DTM "
                            //                        + "  WHERE A.RFID_SEQ = '" + rfid_seq +
                            //                        "' AND A.DEL_YN = 'N' GROUP BY A.WGHT_NO ";

                            //    ServiceAdapter _svc = new ServiceAdapter();
                            //    DataSet auto_v = _svc.GetQuery(auto_value);

                            //    int ch_1 = Convert.ToInt32(dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString());
                            //    int ch_2 = Convert.ToInt32(tmp_down);
                            //    lblSecond_wght.Text = tmp_down;
                            //    lblReal_wgt.Text = System.Math.Abs(ch_2 - ch_1).ToString();


                            //    // 검수 정보 표시
                            //    String kuk_value =
                            //        "   SELECT * "
                            //        //"B.SCRP_GRD_1, B.WGHT_REDUCE_QTY_1, B.RETN_FLAG, B.PLC_NO, A.CODE_NAME "
                            //        + " FROM TB_WS03_0023 B "
                            //        + " INNER JOIN (SELECT * FROM TB_WS01_0002 WHERE TYPE_CD ='005') A ON "
                            //        + "   B.DOWN_SITE_CD = A.CODE "
                            //        + " WHERE B.WGHT_NO = '" + wght_no + "' ";

                            //    DataSet ds_chk1 = _svc.GetQuery(kuk_value);

                            //    if (ds_chk1.Tables[0].Rows.Count > 0)
                            //    {
                            //        insert_text2(lblMSG, "■등급: " + ds_chk1.Tables[0].Rows[0]["SCRP_GRD_1"].ToString());
                            //        insert_text2(lblMSG,
                            //            "■감량: " + ds_chk1.Tables[0].Rows[0]["WGHT_REDUCE_QTY_1"].ToString());
                            //        insert_text2(lblMSG,
                            //            "■입고량:" + string.Format("{0:###,###}",
                            //                Convert.ToInt32(lblReal_wgt.Text.ToString()) -
                            //                Convert.ToInt32(ds_chk1.Tables[0].Rows[0]["WGHT_REDUCE_QTY_1"]
                            //                    .ToString())) + "Kg");

                            //        if (ds_chk1.Tables[0].Rows[0]["RETN_FLAG"].ToString() == "N")
                            //        {
                            //            insert_text2(lblMSG, "■퇴송여부 : 정상입고");
                            //            insert_text2(lblMSG,
                            //                "■하화장소 : " + ds_chk1.Tables[0].Rows[0]["CODE_NAME"].ToString());
                            //        }
                            //        else if (ds_chk1.Tables[0].Rows[0]["RETN_FLAG"].ToString() == "Y")
                            //        {
                            //            insert_text2(lblMSG, "■퇴송여부 : 부분반출");
                            //            insert_text2(lblMSG,
                            //                "■하화장소 : " + ds_chk1.Tables[0].Rows[0]["CODE_NAME"].ToString());
                            //        }
                            //        else if (ds_chk1.Tables[0].Rows[0]["RETN_FLAG"].ToString() == "R")
                            //        {
                            //            insert_text2(lblMSG, "■퇴송여부 : 부분반송");
                            //            insert_text2(lblMSG,
                            //                "■하화장소 : " + ds_chk1.Tables[0].Rows[0]["CODE_NAME"].ToString());
                            //        }
                            //        else
                            //        {
                            //            insert_text2(lblMSG, "■퇴송여부 : 전량반출");
                            //        }
                            //    }
                            //    player65.Play();
                            //    la_ment.Text = "2차 계량이 완료 되었습니다. ";
                            //    logger.Info(" 2차 계량 완료후  >< " + rfid_seq + " > 계량표 정상 출력 ");
                                
                            //}
                            else
                            {
                                //2019-12-30 대한제강
                                //ORD_WGT 가 NULL 일 경우 오류 발생하여 수정(2020-05-06 한
                                String auto_value = " SELECT TO_CHAR(A.MEA_DATE,'YYYYMMDD') || LPAD(A.MEA_SEQ,4,'0') AS WGHT_NO, NVL(A.ORD_WGT,0) AS PROD_WGHT "
                                //String auto_value = " SELECT TO_CHAR(A.MEA_DATE,'YYYYMMDD') || LPAD(A.MEA_SEQ,4,'0') AS WGHT_NO, A.ORD_WGT AS PROD_WGHT "
                                                  + "   FROM TB_WS02_0002 A "
                                                  + "  WHERE A.USE_YN= 'Y' "
                                                  + "    AND A.SEQ_NO = '" + rfid_seq + "' ";
                                //String auto_value = " SELECT A.WGHT_NO, SUM(B.PROD_WGHT) PROD_WGHT FROM TB_WS02_0002 A "
                                //                    + " LEFT OUTER JOIN TB_WS02_0002 B ON A.RFID_NO = B.RFID_NO AND A.IF_NO = B.IF_NO AND A.CRT_DTM = B.CRT_DTM "
                                //                    + "  WHERE A.RFID_SEQ = '" + rfid_seq +
                                //                    "' AND A.DEL_YN = 'N' GROUP BY A.WGHT_NO ";

                                ServiceAdapter _svc = new ServiceAdapter();
                                DataSet auto_v = _svc.GetQuery(auto_value);

                                int ch_1 = Convert.ToInt32(dt_rfid.Rows[0]["LOAD_WEIGHT"].ToString());
                                int ch_2 = Convert.ToInt32(tmp_down);
                                lblSecond_wght.Text = tmp_down;
                                lblReal_wgt.Text = System.Math.Abs(ch_2 - ch_1).ToString();
                                lblProd_wgt.Text = auto_v.Tables[0].Rows[0]["PROD_WGHT"].ToString();

                                //계량표 출력 멘트 변경(2019-10-15 오창휘 수정)
                                //대한제강(2020-02-18 한민호)
                                //if (item_so == "G0001" && (WEIGHT_NO == "0100" || WEIGHT_NO == "0104"))// 친환경 철강 슬러그일 경우만 계량표 출력(2019-10-15 오창휘 수정)
                                //{
                                //    player65.Play();//계량이 완료되었습니다. 계량표를 확인 후 출발해주세요.
                                //}
                                //else
                                //{
                                //    player02.Play();//2차계량이 완료되었습니다. 천천히 출발해주세요.
                                //}
                                //평택은 부자재/저장품, 폐기물/부산물만 2차계량 전표 출력 되게 요청(2020-05-11 한민호)
                                if (WEIGHT_NO.Substring(0, 2) == "13")
                                {
                                    //평택공장 제한없이 전부 출력되도록 재수정(2022-01-12 정성호)
                                    //if (item_jung == "부자재/저장품" || item_jung == "폐기물/부산물")
                                        player65.Play();//계량이 완료되었습니다. 계량표를 확인 후 출발해주세요.
                                    //else
                                    //    player02.Play();//2차계량이 완료되었습니다. 천천히 출발해주세요.
                                }
                                else
                                    player65.Play();//계량이 완료되었습니다. 계량표를 확인 후 출발해주세요.
                                //player65.Play();

                                la_ment.Text = "2차 계량이 완료 되었습니다. ";
                                logger.Info(" 2차 계량 완료후  >< " + rfid_seq + " > 계량표 정상 출력 ");
                            }
                            //부자재저장품 여러번 계량 인 경우 검수정보를 받아야만 계량 할 건을 찾기 때문에 2차계량 한 마지막 값을 찾아야 함(2020-04-01 한민호)
                            if (item_jung == "부자재/저장품")
                            {
                                String Query = " SELECT SEQ_NO " 
                                             + "   FROM ( "
                                             + "         SELECT "
                                             + "             SEQ_NO, RANK() OVER(ORDER BY OUT_WGT_DT DESC) AS WGT_RANK "
                                             + "         FROM ( "
                                             + "               SELECT SEQ_NO, OUT_WGT_DT FROM TB_WS02_0002 WHERE PK_FST_NO = (SELECT PK_FST_NO FROM TB_WS02_0002 WHERE SEQ_NO = '" + rfid_seq + "') AND USE_YN = 'Y' AND OUT_WGT_DT IS NOT NULL "
                                             + "              ) "
                                             + "          ) "
                                              + "         WHERE WGT_RANK = 1 ";
                                ServiceAdapter _svc = new ServiceAdapter();
                                DataSet ds = _svc.GetQuery(Query);
                                rfid_seq = ds.Tables[0].Rows[0]["SEQ_NO"].ToString();
                                dt_pnt_qty = DB_Process.pnt_qty("N", InOut_Gubun, item_jung, item_so, rfid_seq);
                            } 
                            else
                                dt_pnt_qty = DB_Process.pnt_qty("N", InOut_Gubun, item_jung, item_so, rfid_seq);

                            if (dt_pnt_qty.Rows[0]["ITEM_TYPE_NM"].ToString() == "원자재")
                            {
                                SubMatrial_원자재 Print_etc = new SubMatrial_원자재(dt_pnt_qty);

                                //평택은 부자재/저장품, 폐기물/부산물만 2차계량 전표 출력 되게 요청(2020-05-11 한민호)
                                if (WEIGHT_NO.Substring(0, 2) == "13")
                                {
                                    //평택공장 제한없이 전부 출력되도록 재수정(2022-01-12 정성호)
                                    //if (item_jung == "부자재/저장품" || item_jung == "폐기물/부산물")
                                    //{
                                        //미리보기 화면
                                        //Print_etc.ShowPreview();
                                        //인쇄
                                        Print_etc.Print();
                                        //빌릿, 부자재저장품은 2장 출력
                                        // 평택 폐기물/부산물 2장 -> 1장 출력 2020-07-13
                                        //if (item_jung == "폐기물/부산물")
                                        //    Print_etc.Print();
                                        Print_etc.Dispose();
                                    //}
                                }
                                else
                                {
                                    //미리보기 화면
                                    //Print_etc.ShowPreview();
                                    //인쇄
                                    Print_etc.Print();
                                    //빌릿, 부자재저장품은 2장 출력
                                    if (item_jung == "빌렛출하" || (item_jung == "이송출하" && item_so_nm.Substring(0, 2) == "빌릿") || item_jung == "폐기물/부산물")
                                        Print_etc.Print();
                                    Print_etc.Dispose();
                                } 

                            }
                            else
                            {
                                SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);

                                //평택은 부자재/저장품, 폐기물/부산물만 2차계량 전표 출력 되게 요청(2020-05-11 한민호)
                                if (WEIGHT_NO.Substring(0, 2) == "13")
                                {

                                    //평택공장 제한없이 전부 출력되도록 재수정(2022-01-12 정성호)
                                    //if (item_jung == "부자재/저장품" || item_jung == "폐기물/부산물") 
                                    //{
                                        //미리보기 화면
                                        //Print_etc.ShowPreview();
                                        //인쇄
                                        Print_etc.Print();
                                        //빌릿, 부자재저장품은 2장 출력
                                        // 평택 폐기물/부산물 2장 -> 1장 출력 2020-07-13
                                        //if (item_jung == "폐기물/부산물")
                                        //    Print_etc.Print();
                                        Print_etc.Dispose();
                                    //}
                                }
                                else
                                {
                                    //미리보기 화면
                                    //Print_etc.ShowPreview();
                                    //인쇄
                                    Print_etc.Print();
                                    //빌릿, 부자재저장품은 2장 출력
                                    if (item_jung == "빌렛출하" || (item_jung == "이송출하" && item_so_nm.Substring(0, 2) == "빌릿") || item_jung == "폐기물/부산물")
                                        Print_etc.Print();
                                    Print_etc.Dispose();
                                } 
                            }
                                       
                            ////미리보기 화면
                            ////Print_etc.ShowPreview();
                            ////인쇄
                            //Print_etc.Print();
                            ////빌릿, 부자재저장품은 2장 출력
                            //if (item_jung == "빌렛출하" || (item_jung == "이송출하" && item_so_nm.Substring(0, 2) == "빌릿") || item_jung == "폐기물/부산물")
                            //    Print_etc.Print();
                            //Print_etc.Dispose();

                            //실적에에 LPR 정보 업데이트
                            //if (list.Length > 0)
                            //{
                            //    updatepath(list, rfid_seq);
                            //}

                            //국내고철의 경우 계량 값 안내 문자 전송(2019-10-17 오창휘 수정)
                            if (InOut_Gubun == "1" && item_jung == "E" && (item_so.Substring(0, 3) == "E11" || item_so.Substring(0, 3) == "E01"))
                            {
                                string rtn = DB_Process.sendSMS2(rfid_seq);
                            }

                            return;
                        }
                    } //-------------------------                   
                } //---------------------------------- 카드 번호가 널이 아닌거 종료                 
            }
            catch (Exception ex)
            {
                // 로그를 남기고
                if (logger.IsErrorEnabled)
                {
                    logger.Error(this.ToString() + " Mainload 에러----" + ex);
                }
            }
        }

        // LPR 차량번호 이미지 실적테이블에 업데이트 추가(2019-10-04 김상우 수정)
        void updatepath(string[] list, string key_seq)
        {
            try
            {
                if (list.Length > 0)
                {
                    string[] weightkind = new string[]
                    {
                        "10.1.3.18", "10.1.3.19", "10.1.3.20", "10.1.3.21",
                        "10.1.6.18", "10.1.6.19", "10.1.6.20", "10.1.6.21"
                    };

                    string img_car_no = list[4];

                    /*
                    if (car_no == "미인식")
                    {
                        MessageBox.Show(car_no);
                        return;
                    }
                    */

                    logger.Info("list[4] IMG_CAR_NO : " + img_car_no);
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Clear();

                    string weight_knd = string.Empty;
                    switch (WEIGHT_NO)
                    {
                        case "0100":
                            weight_knd = weightkind[0];
                            break;
                        case "0101":
                            weight_knd = weightkind[1];
                            break;
                        case "0102":
                            weight_knd = weightkind[2];
                            break;
                        case "0103":
                            weight_knd = weightkind[3];
                            break;
                        case "0104":
                            weight_knd = weightkind[4];
                            break;
                        case "0105":
                            weight_knd = weightkind[5];
                            break;
                        case "0106":
                            weight_knd = weightkind[6];
                            break;
                        case "0107":
                            weight_knd = weightkind[7];
                            break;
                    }
                    string path = @"http://"
                                  + weight_knd
                                  + ":6486/"  //PORT 번호 고정
                                  + WEIGHT_NO
                                  + "/"
                                  + DateTime.Now.ToString("yyyyMMdd")
                                  + "/"
                                  + list[6];
                    string rtn = DB_Process.updatevehlno(key_seq, car_no, img_car_no, path, "SP_TB_WS02_0002_U");
                    logger.Info(rtn);
                    if (Convert.ToInt32(rtn) < 0)
                    {
                        MessageBox.Show("Data Update 오류");
                    }
                }
                else
                {
                    logger.Info(Convert.ToString(list.Length));
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }
        #endregion

        #region 텍스트 추가

        private void insert_text2(LabelControl insert_target, string insert_value)
        {
            insert_target.Invoke((MethodInvoker) delegate
            {
                if (insert_value.Trim() != "")
                {
                    insert_target.Text += insert_value + "\r\n";
                }

            });
        }

        #endregion

        #region 메세지폼

        private DialogResult MessageOpen(String message, String title)
        {
            showform = new frmMessage();
            showform.Location = new Point(this.Location.X + 100, this.Location.Y + 560);
            showform.Text = title;
            showform.Message = message;
            return showform.ShowDialog();
        }

        #endregion

        #region 프린트 용지 체크

        public void print_check()
        {
            try
            {
                ServiceAdapter _svc = new ServiceAdapter();
                DataTable dt_print = DB_Process.PRINT_CHK_R(Weight_Area);

                if (dt_print.Rows.Count > 0)
                {
                    if (dt_print.Rows[0]["STATUS"].ToString() == "USE")
                    {
                        print_bt.Image = imageList4.Images[0];
                    }
                    else if (dt_print.Rows[0]["STATUS"].ToString() == "REPLACE")
                    {
                        print_bt.Image = imageList4.Images[1];
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("PRINT 용지 체크 에러 : " + ex.Message.ToString());
            }
        }

        #endregion

        #region 계량표 출력

        //Gubun : N 1차 , Y 2차  // weight_kind : item_jung 
        private void by_Matrial(string gubun, string InOut_Gubun, string item_jung, string item_so, string rfid_seq,
            string wght_no)
        {
            try
            {
                ServiceAdapter _svc = new ServiceAdapter();

                // 출력 매수 확인
                DataTable dt_pnt_qty = new DataTable();
                DataTable dt_pnt = new DataTable();
                DataTable dt_pnt_all = new DataTable();
                int pnt_page_cnt = 0;

                // 소분류 출력매수 처리 추가
                dt_pnt_qty = DB_Process.pnt_qty(gubun, InOut_Gubun, item_jung, item_so, rfid_seq);

                if (dt_pnt_qty != null && dt_pnt_qty.Rows.Count > 0)
                {
                    pnt_page_cnt = Convert.ToInt32(dt_pnt_qty.Rows[0]["QTY"].ToString());
                    if (pnt_page_cnt > 0)
                    {
                        //출력할 데이터를 가져옴 
                        dt_pnt = DB_Process.pnt(gubun, rfid_seq, wght_no, item_so);

                        if (dt_pnt != null && dt_pnt.Rows.Count > 0)
                        {
                            if (InOut_Gubun == "2")
                            {
                                for (int Cnt = 0; Cnt < pnt_page_cnt; Cnt++)
                                {
                                    SubMatrial_NEW Print_etc = new SubMatrial_NEW(dt_pnt);
                                    Print_etc.Print();
                                }
                            }
                            else
                            {
                                // 사내이송, 제품외 나머지 것들
                                switch (item_so)
                                {
                                    //case "1051": //1051 : 입고 - 국내 고철 일 경우 별도 , 출력물 양식이 다름

                                    //    if (gubun == "Y")
                                    //    {
                                    //        // 정상이 아닐경우 출력매수 2장
                                    //        if (dt_pnt.Rows[0]["RETN_FLAG"].ToString() != "")
                                    //        {
                                    //            pnt_page_cnt = 2;
                                    //        }
                                    //    }

                                    //    for (int Cnt = 0; Cnt < pnt_page_cnt; Cnt++)
                                    //    {
                                    //        SubMatrial_KukS Print_kuk = new SubMatrial_KukS(dt_pnt);
                                    //        //Print_kuk.ShowPreview();
                                    //        Print_kuk.Print();
                                    //    }

                                    //    break;
                                    //case "1081":
                                    //    // 부재료 신규 양식적용                                    
                                    //    for (int Cnt = 0; Cnt < pnt_page_cnt; Cnt++)
                                    //    {
                                    //        SubMatrial_NEW Print_etc = new SubMatrial_NEW(dt_pnt);
                                    //        //Print_etc.ShowPreview();
                                    //        Print_etc.Print();
                                    //    }

                                    //    break;
                                    default:
                                        for (int Cnt = 0; Cnt < pnt_page_cnt; Cnt++)
                                        {
                                            logger.Info(" 계량표 출력 중" + Cnt.ToString() );
                                            SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt);
                                            //Print_etc.ShowPreview();
                                            Print_etc.Print();
                                        }

                                        break;
                                }
                            }

                            pnt_page_cnt = 1;
                            // 프린터 사용 매수 업데이트
                            DB_Process.PRINT_PAGE_CNT_SET(Weight_Area, pnt_page_cnt);
                        }
                        else
                        {
                            logger.Info(gubun + " 차 출력정보 없음><배차: " + rfid_seq + "><계량:" + wght_no + "><품목:" + item_so +
                                        " >");
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

        #region 화면초기화 함수

        private void txt_clear()
        {
            txtManualRfid.Text = string.Empty;

            lblRfid_no.Text = string.Empty; // 카드
            lblItem_Nm.Text = string.Empty; // 계량품목
            //labelControl4.Text = string.Empty; // 차량
            lblWght_No.Text = string.Empty; //계량
            lblVEHL_NO.Text = string.Empty;
            lblVEHL_NO.BackColor = Color.White;

            lblFirst_wght.Text = "0";
            lblSecond_wght.Text = "0";
            lblReal_wgt.Text = "0";
            lblProd_wgt.Text = "0";

            lblMSG.Text = "";
            AutoNum = "";
            tmp_down = "0";

            // 모든 변수 초기화  
            Gubun = "N";
            rfid_no = string.Empty;
            car_no = string.Empty;
            InOut_Gubun = string.Empty;
            item_jung = string.Empty;
            item_so = string.Empty;
            wght_no = string.Empty;
            rfid_seq = string.Empty;
            weight_fg = false; //1차, 2차 계량 저장 여부
            //LPR차량번호 초기화 추가(2019-10-12 오창휘 수정)
            img_vehl_no = string.Empty;//LPR차량번호 추가(2019-10-11 오창휘 수정)
            img_vehl_file = string.Empty;//LPR 파일경로 추가(2019-10-13 오창휘 수정)

            list = new string[] { }; // LPR 리시브 변수 배열

            //카메라 스틸컷 경로 초기화(2020-02-04 오창휘 수정))
            file_path1 = "";
            file_path2 = "";

            //la_ment.Text = "RFID 카드를 대주세요 ";
            wegiht_fg_03 = false;
            load_area_cd = "";
            down_area_cd = "";
            //lblInput.Text = "";
            timer_main.Enabled = true;

            //계량진행 상태값 추가
            AW_CHK = "0";

            if (showform != null)
            {
                showform.Close();
            }

            if (RedWindowform != null)
            {
                RedWindowform.Close();
            }

            if (LPR_USE == "Y")
            {
                if (SocketClient_LPR != null)
                {
                    if (Lpr_index != "0" && SocketClient_LPR.Connected)
                    {
                        //server_send_LPR("{" + Lpr_index + "|0}");
                        server_close_LPR();
                    }
                }
            }

            //클리어 시 카드 태그 멘트 뺌(2019-10-16 오창휘 수정)
            //player13.Play();
        }

        #endregion

        #region 시리얼코드 생성

        private void SerialCreate()
        {
            // 인디게이터
            serialPort[0] = new SerialPort();
            serialPort[0].PortName = HW_COMPORT;
            //평택은 2400 임(2020-05-07 한민호)
            if (WEIGHT_NO.Substring(0, 4) == "1300")
                serialPort[0].BaudRate = 2400;
            else
                serialPort[0].BaudRate = 9600;
            //serialPort[0].BaudRate = 9600;
            serialPort[0].Parity = Parity.Even;
            serialPort[0].DataBits = 7;
            serialPort[0].StopBits = StopBits.One;
            //serialPort[0] = new SerialPort();
            //serialPort[0].PortName = HW_COMPORT;
            //serialPort[0].BaudRate = 2400;
            //serialPort[0].Parity = Parity.Even;
            //serialPort[0].DataBits = 7;
            //serialPort[0].StopBits = StopBits.One;

            serialPort[0].Open();
        }

        #endregion


        #region RFID 처리함수

        #region RFID 연결

        private void server_connect_RFID()
        {
            HW_RFID_IP = "10.1.5.203";
            ServerIPAddress = IPAddress.Parse(HW_RFID_IP);

            logger.Info(" HW_RFID_IP : " + HW_RFID_IP);
            PORT = 5578; // "";
            //Convert.ToInt32(HW_RFID_PORT);

            if (SocketClient_RFID != null)
            {
                SocketClient_RFID.Close();
            }

            ServerIPEndPoint = new IPEndPoint(ServerIPAddress, PORT);

            SocketClient_RFID = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                SocketClient_RFID.Connect(ServerIPEndPoint);
                logger.Info(" RFID 카드 연결중 ");
                if (SocketClient_RFID.Connected)
                {
                    logger.Info(" RFID 카드 연결 성공");
                    ThreadClient_RFID =
                        new System.Threading.Thread(new System.Threading.ThreadStart(RecieveFromServer_RFID));

                    ThreadClient_RFID.IsBackground = true;
                    ThreadClient_RFID.Start();
                    //권오택 차장 확인 삭제, RFID_READY 함수 안에 server_connect_RFID 함수 또 있음(2019-12-06 오창휘 수정)
                    //RFID_READY();
                    logger.Info(ThreadClient_RFID.ThreadState.ToString());
                    rfid1.Image = imageList2.Images[0];
                    logger.Info(" 2");
                }
            }
            catch (Exception ex)
            {
                la_ment.Text = ex.Message;
                rfid1.Image = imageList2.Images[1];
                logger.Error("소켓 에러 : " + ex.Message.ToString());
                server_close_RFID();
            }
        }

        #endregion

        #region RFID 연결끊기

        private void server_close_RFID()
        {
            if (SocketClient_RFID != null)
            {
                SocketClient_RFID.Close();
            }

            if (ThreadClient_RFID != null)
            {
                if (ThreadClient_RFID.IsAlive)
                {
                    ThreadClient_RFID.Abort();
                }
            }
        }

        #endregion

        #region RFID 스타트 리드

        private void RFID_READY()
        {
            if (SocketClient_RFID.Connected == true)
            {
                SocketClient_RFID.Send(HexToByte(">f"));

                logger.Info(" RFID 전송 : 66");

                //SocketClient_RFID.Send(HexToByte("7E01010004A3011000B27F"));

                //logger.Info(" RFID 전송 : 7E01010004A3011000B27F");

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
                server_close_RFID();
                Thread.Sleep(500);
                server_connect_RFID();
            }
        }

        #endregion

        #region RFID 메세지 수신

        private void RecieveFromServer_RFID()
        {
            Thread.Sleep(2000);
            RFID_READY();

            while (true)
            {
                try
                {
                    logger.Info("시작!!!");
                    if (!SocketClient_RFID.Connected)
                    {
                        logger.Error("RecieveFromServer_RFID(!SocketClient_RFID.Connected) : ");
                        server_close_RFID();
                        server_connect_RFID();
                        return;
                    }

                    Byte[] ReceiveByte = new Byte[30];
                    logger.Info(ReceiveByte.Length);
                    int nValue = SocketClient_RFID.Receive(ReceiveByte, ReceiveByte.Length, 0);
                    logger.Info(" nValue = " + nValue);

                    if (nValue >= 0)
                    {
                        String tmp = "";

                        foreach (byte val in ReceiveByte)
                        {
                            tmp = tmp + string.Format("{0:x2}", val);
                        }

                        lblRfid_no.Text = tmp;
                        if (tmp.Length > 28)
                        {
                            //logger.Error("RFID 카드값(최초리딩) : " + tmp);
                            //logger.Error("RFID 카드값(+18) : " + tmp.Substring(18));

                            if (tmp.Substring(18, 20) != "00000000000000000000")
                            {
                                logger.Error("RFID 카드값(최초리딩 != 00000000000000000000) : " + tmp);
                                //logger.Error("RFID 카드값(+18 != 00000000000000000000) : " + tmp.Substring(18));

                                //160908 hoseong.hwang 
                                //wmcopyData = new Win32API.COPYDATASTRUCT();
                                wmcopydataRFID.dwData = (IntPtr) (Win32API.WM_USER_RFID_RECV);
                                wmcopydataRFID.cbData = (uint) tmp.Length * sizeof(char);
                                wmcopydataRFID.lpData = tmp;
                                //Int32 iHaldle = GetHandle;

                                if (iFormHandle > 0)
                                {
                                    Win32API.SendMessage((IntPtr) iFormHandle, Win32API.WM_COPYDATA, IntPtr.Zero,
                                        ref wmcopydataRFID);
                                }
                            }

                            //insert_text(textEdit1, tmp);
                        }

                        //ReceiveData = System.Text.Encoding.Unicode.GetString(ReceiveByte);
                        //Console.WriteLine(ReceiveData);
                        //MessageDisplay(ReceiveData);
                        MessageBox.Show(tmp);
                    }

                    Thread.Sleep(500);

                    //if (bClose == true)
                    //{
                    //    break;
                    //}
                }
                catch (Exception ex)
                {
                    logger.Error("RFID 수신에러 : " + ex.Message.ToString());
                }
            }

            //ThreadClient.Abort();
        }

        #endregion

        protected override void WndProc(ref Message wMessage)
        {
            switch (wMessage.Msg)
            {
                case Win32API.WM_COPYDATA:
                    Win32API.COPYDATASTRUCT lParam1 =
                        (Win32API.COPYDATASTRUCT) Marshal.PtrToStructure(wMessage.LParam,
                            typeof(Win32API.COPYDATASTRUCT));
                    Win32API.COPYDATASTRUCT lParam2 = new Win32API.COPYDATASTRUCT();

                    logger.Error("WndProc WM_COPYDATA Start!!");

                    lParam2 = (Win32API.COPYDATASTRUCT) wMessage.GetLParam(lParam2.GetType());
                    if (Win32API.WM_USER_RFID_RECV == (int) lParam2.dwData)
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

        private void insert_text(string insert_value)
        {
            //insert_target.Invoke((MethodInvoker)delegate
            //{
            ReceveData1 = insert_value.ToUpper();

            ReceveData1 = ReceveData1.Substring(16);
            //logger.Error("insert_text : insert_value : " + insert_value);
            //logger.Error("insert_text : ReceveData1 : " + ReceveData1);

            //if (ReceveData1[0] == 'E')
            //{
            //    //logger.Error("RFID 카드값 자른거 E : " + ReceveData1);

            //    if (ReceveData1.Length >= 17)
            //    {
            //        if (ReceveData1.Substring(0, 2) == "E2")
            //        {
            //            ReceveData1 = ReceveData1.Substring(0, 24);
            //        }
            //        else
            //        {
            //            ReceveData1 = ReceveData1.Substring(0, 16);
            //        }

            //        if (CardRead == true)
            //        {
            //            //if (outSensor.Tag.ToString() == "00")
            //            //{
            //            if (Prev_rfid != ReceveData1)
            //            {
            //                //logger.Error("RFID 카드값 정상인식 : " + ReceveData1);

            //                textBox1.Text = ReceveData1;
            //                Prev_rfid = ReceveData1;
            //                player20.PlaySync();

            //                CardRead = false;
            //                rfid_ready = true;

            //                btnRFID_Click(btnRFID, new EventArgs());
            //            }
            //            else
            //            {
            //                CardRead = false;
            //                ReceveData1 = "";
            //                textBox1.Text = "";
            //                logger.Error("RFID 중복 : " + ReceveData1);
            //            }
            //            //}
            //            //else
            //            //{
            //            //    ReceveData1 = "";
            //            //}
            //        }
            //        else
            //        {
            //            ReceveData1 = "";
            //        }
            //    }
            //}
            //else if (ReceveData1[0] == 'D')
            //{
            //logger.Error("RFID 카드값 자른거 D : " + ReceveData1);

            if (ReceveData1.Length >= 24)
            {
                ReceveData1 = ReceveData1.Substring(0, 23);
                if (outSensor.Tag.ToString() == "01" && InSensor.Tag.ToString() == "01")
                {
                    if (Prev_rfid != ReceveData1)
                    {
                        //logger.Error("RFID 카드값 정상인식 : " + ReceveData1);

                        txtManualRfid.Text = ReceveData1;
                        Prev_rfid = ReceveData1;
                        player20.PlaySync();

                        CardRead = false;
                        rfid_ready = true;

                        btnRFID_Click(btnRFID, new EventArgs());
                    }
                    else
                    {
                        ReceveData1 = "";
                        txtManualRfid.Text = "";
                        logger.Error("RFID 중복 : " + ReceveData1);
                    }
                }
                else
                {
                    ReceveData1 = "";
                    logger.Info(" 정위치 센서 감지로 계량할수 없습니다 ");
                    SystemSounds.Exclamation.Play();
                    la_ment.Text = " 정위치 센서 감지로 계량할수 없습니다";
                }
            }
            //}
            else
            {
                ReceveData1 = "";
                logger.Error("insert_text : (이상한 RFID 수신) insert_value : " + insert_value);
            }
        }

        public byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");
            byte[] comBuffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                try
                {
                    comBuffer[i / 2] = (byte) Convert.ToByte(msg.Substring(i, 2), 16);
                }
                catch (ArgumentOutOfRangeException argumentoutofrange)
                {
                    MessageBox.Show("잘못된 형식의 HEX값이 입력되었습니다." + argumentoutofrange);
                }
            }

            return comBuffer;
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

        #region 인디게이터 타이머(인디게이터 무게 올라 오는 부분 처리)

        private void timer_indicator_Tick(object sender, EventArgs e)
        {
            timer_indicator.Enabled = false;

            try
            {
                if (serialPort[0].IsOpen)
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

                    //logger.Info("timer_indicator_Tick : serialPort[0] : " + sRead);

                    //숫자부분 짜르기
                    int iFr = 0;
                    int iTo = 0;

                    sRead = sRead.ToUpper();    // 전체 대문자로 변경
                    iFr = sRead.IndexOf("+");
                    if (iFr >= 0)
                    {
                        iTo = sRead.IndexOf("KG", iFr);
                    }

                    if (iFr < 0 || iTo < 0)
                    {
                        iFr = sRead.IndexOf("-");
                        iTo = sRead.IndexOf("KG", iFr);
                    }

                    if (iFr < 0 || iTo - iFr <= 0)
                    {
                        logger.Info("ERR!! timer_indicator_Tick :sRead.Substring : iFr : " + iFr.ToString() + " iTo : " + iTo.ToString() + " sRead : " + sRead);
                    }

                    string sNumber = sRead.Substring(iFr, iTo - iFr).Trim().Replace(" ", "");
                    sNumber = sNumber.Replace(".", "");
                    sNumber = sNumber.Trim();

                    // + 만 나올경우 한번더 검색
                    if (sNumber == "+")
                    {
                        iFr = sRead.IndexOf("+", iFr);
                        if (iFr >= 0)
                        {
                            iTo = sRead.IndexOf("KG", iFr);
                        }

                        if (iFr < 0 || iTo < 0)
                        {
                            iFr = sRead.IndexOf("-");
                            iTo = sRead.IndexOf("KG", iFr);
                        }

                        if (iFr < 0 || iTo - iFr <= 0)
                        {
                            logger.Info("ERR!! timer_indicator_Tick :sRead.Substring : iFr : " + iFr.ToString() + " iTo : " + iTo.ToString() + " sRead : " + sRead);
                        }
                    }

                    sNumber = sRead.Substring(iFr, iTo - iFr).Trim().Replace(" ", "");
                    sNumber = sNumber.Replace(".", "");
                    sNumber = sNumber.Trim();


                    //  이상값 수신 체크                    
                    // 중간에 튀는 중량 체크
                    String tmp = sNumber.Replace("+", "0");
                    int iData = Convert.ToInt32(tmp.Trim());
                    if (Math.Abs(indicator_read_old - iData) > 1000 && "+" == sNumber.Trim())
                    {
                        indicator_read_cnt++;
                        if (indicator_read_cnt == indicator_read_max) indicator_read_cnt = 0;
                        else
                        {
                            //logger.Info("sNumber = + " + indicator_read_old.ToString() + "/ return");
                            sNumber = "+" + indicator_read_old.ToString();
                        }
                    }
                    else
                    {
                        //logger.Info("CNT : " + indicator_read_cnt.ToString() + "/ return");
                        indicator_read_cnt = 0;
                    }
                    if (indicator_read_cnt == 0)
                    {
                        if ("+" == sNumber.Trim()) sNumber = "+0";

                        //logger.Info("OLD : " + indicator_read_old.ToString() + "/ iData : " + iData.ToString()); 
                        indicator_read_old = iData;
                    }
                    // 화면 text에 중량 넣기
                    txtManualWeight.Text = sNumber;

                    if (sNumber.Length > 1 && sNumber.Length <= 10)    // 부호(1) + 숫자(5) ~ 숫자(9), 일반적으로는 숫자(6) 자리임
                    //if (sNumber.Length <= 10)    // 부호(1) + 숫자(5) ~ 숫자(9), 일반적으로는 숫자(6) 자리임
                    {
                        int Num;
                        bool isNum = int.TryParse(sNumber, out Num);
                        if (isNum)
                        {
                            if (weight_fg == false)
                            {
                                lblWeight_value.Text = Convert.ToInt32(sNumber).ToString();
                                lblWeight_value.ForeColor = Color.Yellow;
                            }
                        }
                        else
                        {
                            lblWeight_value.Text = "-1";
                            lblWeight_value.ForeColor = Color.Red;

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
                        if (weight_fg == false)
                        {
                            lblWeight_value.Text = "0";
                            tmp_down = "0";
                        }
                        //lblWeight_value.ForeColor = Color.Red;

                        //logger.Info("ERR!! timer_indicator_Tick : sRead.length != 7 : " + sRead);
                        //logger.Info("ERR!! timer_indicator_Tick : sNumber.length != 7 : " + sNumber);                        
                    }
                }
                timer_indicator.Enabled = true;

                #region 주석 막음
                /*
                if (serialPort[0].IsOpen)
                {
                    //logger.Info(" Indicaors 연결!!!! ");
                    //Idicator로 "CLEAR" 명령을 내보낸다.  
                    /*
                    byte[] outputCLEAR = Encoding.UTF8.GetBytes("CLEAR" + (char)13 + (char)10);
                        serialPort[0].Write(outputCLEAR, 0, outputCLEAR.Length);

                        //안정적인 Data 송신을 위해 0.1초간 Delay Time을 갖는다.
                        Thread.Sleep (100);

                        //Idicator로 "READ" 명령을 내보낸다.
                        byte[] outputREAD = Encoding.UTF8.GetBytes("READ" + (char)13 + (char)10);
                        serialPort[0].Write(outputREAD, 0, outputREAD.Length);

                    //Idicator로 데이터 읽기
                    string sRead = serialPort[0].ReadExisting();
                    logger.Info("1. sRead =" + sRead);
                    serialPort[0].DiscardInBuffer();

                    //숫자부분 짜르기
                    int iFr = 0;
                    int iTo = 0;

                    sRead = sRead.ToUpper(); // 전체 대문자로 변경
                    iFr = sRead.IndexOf("+");
                    logger.Info("2. sRead =" + sRead);
                    if (iFr >= 0)
                    {
                        iTo = sRead.IndexOf("KG", iFr);
                    }

                    if (iFr < 0 || iTo < 0)
                    {
                        iFr = sRead.IndexOf("-");
                        iTo = sRead.IndexOf("KG", iFr);
                    }

                    if (iFr < 0 || iTo - iFr <= 0)
                    {
                        logger.Info("ERR!! timer_indicator_Tick :sRead.Substring : iFr : " + iFr.ToString() +
                                    " iTo : " + iTo.ToString() + " sRead : " + sRead);

                    }

                    string sNumber = sRead.Substring(iFr, iTo - iFr).Trim().Replace(" ", "");
                    sNumber = sNumber.Replace(".", "");
                    sNumber = sNumber.Trim();

                    logger.Info("3. sNumber =" + sNumber);

                    // + 만 나올경우 한번더 검색
                    if (sNumber == "+")
                    {
                        iFr = sRead.IndexOf("+", iFr);
                        if (iFr >= 0)
                        {
                            iTo = sRead.IndexOf("KG", iFr);
                        }

                        if (iFr < 0 || iTo < 0)
                        {
                            iFr = sRead.IndexOf("-");
                            iTo = sRead.IndexOf("KG", iFr);
                        }

                        if (iFr < 0 || iTo - iFr <= 0)
                        {
                            logger.Info("ERR!! timer_indicator_Tick :sRead.Substring : iFr : " + iFr.ToString() +
                                        " iTo : " + iTo.ToString() + " sRead : " + sRead);
                        }
                    }

                    sNumber = sRead.Substring(iFr, iTo - iFr).Trim().Replace(" ", "");
                    sNumber = sNumber.Replace(".", "");
                    sNumber = sNumber.Trim();

                    
                    //  이상값 수신 체크                    
                    // 중간에 튀는 중량 체크
                    String tmp = sNumber.Replace("+", "0");
                    int iData = Convert.ToInt32(tmp.Trim());

                    if (Math.Abs(indicator_read_old - iData) > 1000 && "+" == sNumber.Trim())
                    {
                        indicator_read_cnt++;
                        if (indicator_read_cnt == indicator_read_max) indicator_read_cnt = 0;
                        else
                        {
                            //logger.Info("sNumber = + " + indicator_read_old.ToString() + "/ return");
                            sNumber = "+" + indicator_read_old.ToString();
                        }
                    }
                    else
                    {
                        //logger.Info("CNT : " + indicator_read_cnt.ToString() + "/ return");
                        indicator_read_cnt = 0;
                    }

                    if (indicator_read_cnt == 0)
                    {
                        if ("+" == sNumber.Trim()) sNumber = "+0";

                        //logger.Info("OLD : " + indicator_read_old.ToString() + "/ iData : " + iData.ToString()); 
                        indicator_read_old = iData;
                    }

                    // 화면 text에 중량 넣기
                    textBox2.Text = sNumber;

                    if (sNumber.Length > 1 && sNumber.Length <= 10) // 부호(1) + 숫자(5) ~ 숫자(9), 일반적으로는 숫자(6) 자리임
                        //if (sNumber.Length <= 10)    // 부호(1) + 숫자(5) ~ 숫자(9), 일반적으로는 숫자(6) 자리임
                    {
                        int Num;
                        bool isNum = int.TryParse(sNumber, out Num);
                        if (isNum)
                        {
                            if (weight_fg == false)
                            {
                                lblWeight_value.Text = Convert.ToInt32(sNumber).ToString();
                                //textBox2.Text = txtsetting(Convert.ToInt32(sNumber).ToString());
                                //textBox4.Text = txtsetting(Convert.ToInt32(sNumber).ToString());
                                //textBox2.ForeColor = Color.Red;
                                //logger.Info(lblWeight_value.Text + "kg");
                                lblWeight_value.ForeColor = Color.Yellow;
                            }
                        }
                        else
                        {
                            lblWeight_value.Text = "-1";
                            lblWeight_value.ForeColor = Color.Red;

                            logger.Info("ERR!! timer_indicator_Tick : sRead -1 : " + sRead);
                            logger.Info("ERR!! timer_indicator_Tick : sNumber -1 : " + sNumber);
                        }

                        if (Num > 0)
                        {
                            logger.Info("timer_indicator_Tick : sRead > 0 : " + sRead);
                            logger.Info("timer_indicator_Tick : sNumber > 0 : " + sNumber);
                            logger.Info("timer_indicator_Tick : 숫자 > 0 : " + Num.ToString());
                        }
                    }
                    else
                    {
                        if (weight_fg == false)
                        {
                            lblWeight_value.Text = "0";
                            tmp_down = "0";
                        }
                        //lblWeight_value.ForeColor = Color.Red;

                        //logger.Info("ERR!! timer_indicator_Tick : sRead.length != 7 : " + sRead);
                        //logger.Info("ERR!! timer_indicator_Tick : sNumber.length != 7 : " + sNumber);                        
                    }
                }
                */
                #endregion
            }
            catch (Exception ex)
            {
                serialPort[0].DiscardInBuffer();
                CardRead = false;
                lblWeight_value.Text = "-9999";
                lblWeight_value.ForeColor = Color.Red;
                logger.Error("ERR!! timer_indicator_Tick : 인디게이터 에러 : " + ex.Message.ToString());

                Iny_cnt = Iny_cnt + 1;
                //인디게이트 재연결 시도
                SerialPortOpen(serialPort[0]);

                //5초 동안 연결 확인 삭제(2019-12-06 오창휘 수정)
                //if (Iny_cnt > 10) // 5초간 연결 확인후 연결이 안될시 
                //{
                //    Iny_cnt = 0;                    
                //    //인디게이터 타이머 종료 재연결 시도 X
                //    timer_indicator.Enabled = false;
                //    return;
                //}
                timer_indicator.Enabled = true;
            }
            
        }

        #endregion
        
        #region 정위치센서

        static bool BioFailed(Automation.BDaq.ErrorCode err)
        {
            return err < ErrorCode.Success && err >= ErrorCode.ErrorHandleNotValid;
        }

        #endregion


        #region 센서 확인 타이머(센서값 확인)

        private void tmrsensor_Tick(object sender, EventArgs e)
        {
            tmrsensor.Enabled = false;
            DataTable dt_result = null;
            //// read Di port state
            byte portData = 0;
            bool wght_knd = false;
            ErrorCode errorCode = ErrorCode.Success;
            InstantDiCtrl instantDiCtrl;
            instantDiCtrl = new InstantDiCtrl();
            try
            {
                //logger.Info("SENSOR_PORT : " + Convert.ToInt32(SENSOR_PORT));
                instantDiCtrl.SelectedDevice = new DeviceInformation(Convert.ToInt32(SENSOR_PORT));
                //byte[] buffer = new byte[64];
                // Step 3: Read DI ports' status and show.
                errorCode = instantDiCtrl.Read(0, out portData);
                if (BioFailed(errorCode))
                {
                    throw new Exception();
                } 

                if (portData.ToString("X2") != "00")
                {
                    //logger.Info(" portData : " + portData.ToString("X2"));
                    //logger.Info(portData);
                }

                //if (Weight_Knd.Substring(0, 1) == "A")
                //{
                //    wght_knd = true;
                //}
                //2번,6번 계량대만 방사능 차량 체크(2019-10-15 오창휘 수정)
                //신평입문(11001), 녹산후문입문(12101)만 방사능 체크(2020-04-03 한민호)

                //방사능 임시 체크(2020-04-09 한민호)
                //logger.Info("계량대번호 : " + WEIGHT_NO);

                switch (WEIGHT_NO)
                {
                    case "11001":
                        wght_knd = true;
                        break;
                    case "12101":
                        wght_knd = true;
                        break;
                    default:
                        wght_knd = false;
                        break;
                }

                //방사능 임시 체크(2020-04-09 한민호)
                //logger.Info("방사능계량대 여부 : " + wght_knd);
                //logger.Info("X2의 값 : " + portData.ToString("X2"));

                //logger.Info("1  wght_knd =" + wght_knd);
                /*
                if (portData.ToString() == "128" & wght_knd == true) //정문 계량대 방사능 검출
                {
                    logger.Info("128  wght_knd =" + wght_knd);
                    RADIATION_CHK = true;
                    la_ment.Text = "방사능 검출 차량 입니다. ";
                    player08.Play(); //입출차 제한 차량
                    logger.Info("방사능 검출 차량 입니다. ");
                    //logger.Info(this.ToString() + "<" + car_no + " >방사능 검출 제한 차량 ");
                    RedWindowform = new RedWindow();66
                    RedWindowform.ShowDialog();
                }
                */

                //timer1.Enabled = true;
                //timer1.Interval = 1000;
                //timer1.Tick += new EventHandler(timer1_tick);

                switch (portData.ToString("X2"))
                {
                    case "01":
                        outSensor.Image = imageList1.Images[1];
                        outSensor.Tag = "00";
                        InSensor.Image = imageList1.Images[0];
                        InSensor.Tag = "01";
                        if (Convert.ToInt32(lblWeight_value.Text.Trim()) <= 200)
                        {
                            lblRfid_no.Text = string.Empty;
                            lblItem_Nm.Text = string.Empty;
                            lblVEHL_NO.Text = string.Empty;
                            lblWght_No.Text = string.Empty;
                        }
                        reset();
                        ReceveData1 = "";
                        CardRead = false;
                        Prev_rfid = "";
                        //방사능 검출 확인 추가(2019-10-15 오창휘 수정)
                        RADIATION_CHK = false;//방사능 검출될 경우 true
                        //방사능 검출 횟수 추가(2019-10-20 오창휘 수정)
                        RADIATION_CNT = 0;
                        //차량진입상태 체크 추가(2019-09-30 오창휘 수정)
                        //if (outSensor.Tag == "00" && InSensor.Tag == "01" && !VEHl_CHK)
                        //{
                        //    AW_CHK = "1";//OUT 진입
                        //    VEHl_CHK = true;
                        //}

                        //계량 상태값 초기화 뺌(2019-11-14 오창휘 수정)
                        //계량진행 상태값 추가
                        //AW_CHK = "0";

                        break;

                    case "02":
                        outSensor.Image = imageList1.Images[0];
                        outSensor.Tag = "01";
                        InSensor.Image = imageList1.Images[1];
                        InSensor.Tag = "00";
                        if (Convert.ToInt32(lblWeight_value.Text.Trim()) <= 200)
                        {
                            lblRfid_no.Text = string.Empty;
                            lblItem_Nm.Text = string.Empty;
                            lblVEHL_NO.Text = string.Empty;
                            lblWght_No.Text = string.Empty;
                        }
                        reset();
                        ReceveData1 = "";
                        CardRead = false;
                        Prev_rfid = "";
                        //방사능 검출 확인 추가(2019-10-15 오창휘 수정)
                        RADIATION_CHK = false;//방사능 검출될 경우 true
                        //차량진입상태 체크 추가(2019-09-30 오창휘 수정)
                        //if (outSensor.Tag == "01" && InSensor.Tag == "00" && !VEHl_CHK)
                        //{
                        //    AW_CHK = "2";//OUT 진입
                        //    VEHl_CHK = true;
                        //}
                        break;
     

                    //후문 계량대 방사능 검출
                    case "80":
                        if (wght_knd)
                        {
                            //방사능 검출 횟수 추가(2019-10-20 오창휘 수정)
                            RADIATION_CNT++;
                            logger.Info("80  wght_knd =" + wght_knd + "측정 횟수 = " + RADIATION_CNT.ToString() );

                            if (RADIATION_CNT >= 11) // 5초 초과하여 방사능 측정이 올 때로 변경(2019-11-06 오창휘 수정)
                            //if (RADIATION_CNT >= 8) // 4초 이상 방사능 측정이 올 때(2019-10-20 오창휘 수정)
                            {
                                RADIATION_CHK = true;//방사능 검출될 경우 true
                            }
                            //player08.Play(); //입출차 제한 차량
                            //la_ment.Text = "방사능 검출 차량 입니다. ";
                            ////logger.Info(this.ToString() + "<" + car_no + " >방사능 검출 제한 차량 ");
                            //logger.Info("방사능 검출 차량 입니다. ");
                            //RedWindowform = new RedWindow();
                            //RedWindowform.TopMost = true;
                            //if (RedWindowform.ShowDialog() == DialogResult.OK)
                            //{
                            //    RedWindowform.Close();
                            //}
                            //la_ment.Text = "방사능 검출 차량 입니다. ";
                        }
                        break;

                    default:
                        outSensor.Image = imageList1.Images[1];
                        outSensor.Tag = "00";
                        InSensor.Image = imageList1.Images[1];
                        InSensor.Tag = "00";
                        //차량 진입 상태 체크(2019-09-30 오창휘 수정)
                        if (Convert.ToInt32(lblWeight_value.Text.Replace(",", "")) <= 200)//평소상태
                        {
                            //방사능 검출 확인 추가(2019-10-15 오창휘 수정)
                            RADIATION_CHK = false;//방사능 검출될 경우 true
                            AW_CHK = "0";
                            VEHl_CHK = false;
                        }
                        else
                            VEHl_CHK = true;
                        
                        break;
                }

                instantDiCtrl.Dispose();

                String tmp = txtManualWeight.Text.Replace("+", "");
                tmp = tmp.Replace(" ", "");
                if (CardRead == false && lblItem_Nm.Text.Trim() != "" && lblRfid_no.Text.Trim() != "")
                {
                    //logger.Info(" CARDREAD : " + CardRead.ToString() + "/" + labelControl2.Text + "/" + lblRfid_no.Text);                     

                    if (Convert.ToInt32(tmp.ToString()) < 1000)
                    {
                        clear_timer.Enabled = true;
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

        void reset()
        {
            txtManualRfid.Text = string.Empty;

            lblRfid_no.Text = string.Empty; // 카드
            lblItem_Nm.Text = string.Empty; // 계량품목
            //labelControl4.Text = string.Empty; // 차량
            lblWght_No.Text = string.Empty; //계량
            lblVEHL_NO.Text = string.Empty;
            lblVEHL_NO.BackColor = Color.White;

            lblFirst_wght.Text = "0";
            lblSecond_wght.Text = "0";
            lblReal_wgt.Text = "0";
            lblProd_wgt.Text = "0";

            lblMSG.Text = "";
            AutoNum = "";
            tmp_down = "0";

            // 모든 변수 초기화  
            Gubun = "N";
            rfid_no = string.Empty;
            car_no = string.Empty;
            InOut_Gubun = string.Empty;
            item_jung = string.Empty;
            item_so = string.Empty;
            wght_no = string.Empty;
            rfid_seq = string.Empty;
            weight_fg = false; //1차, 2차 계량 저장 여부
            //LPR차량번호 초기화 추가(2019-10-12 오창휘 수정)
            img_vehl_no = string.Empty;//LPR차량번호 추가(2019-10-11 오창휘 수정)
            img_vehl_file = string.Empty;//LPR 파일경로 추가(2019-10-13 오창휘 수정)

            list = new string[] { }; // LPR 리시브 변수 배열

            //la_ment.Text = "RFID 카드를 대주세요 ";
            wegiht_fg_03 = false;
            load_area_cd = "";
            down_area_cd = "";
            //lblInput.Text = "";
            timer_main.Enabled = true;

            if (showform != null)
            {
                showform.Close();
            }

            if (LPR_USE == "Y")
            {
                if (SocketClient_LPR != null)
                {
                    if (SocketClient_LPR.Connected)
                    //if (Lpr_index != "0" && SocketClient_LPR.Connected)
                    {
                        //server_connect_LPR();
                        //server_send_LPR("{" + Lpr_index + "|0}");
                        server_close_LPR();
                    }
                }
            }
        }

        private void timer1_tick(object sender, EventArgs e)
        {
            time++;
            //textBox3.Text = Convert.ToString(time);
            if (time >= 2)
            {
                //timer1.Enabled = false;
            }
        }

        #endregion

        #region 프로그램 종료 확인

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("프로그램을 종료하시겠습니까?", "종료 확인 ", MessageBoxButtons.YesNo) ==
                System.Windows.Forms.DialogResult.No)
            {
                // 종료 취소
                e.Cancel = true;
            }
            else
            {
                Thread.Sleep(500);

                Application.ExitThread();
                Application.Exit();
            }
        }

        #endregion

        #region 메인타이머

        private void timer_main_Tick(object sender, EventArgs e)
        {
            timer_main.Enabled = false;
            try
            {
                String tmp = "";
                int max_weight = 0;
                if (CheckingSpecialText(txtManualWeight.Text))
                {
                    tmp = txtManualWeight.Text.Replace("+", "");
                }
                //logger.Info("메인타이머 INFO:tmp 값 - " + tmp.ToString().Trim());
                if (tmp.ToString() != "")
                {
                    // 계량 중량 안정화
                    // 최소/최대 중량차이가 30KG보다 작고, 최소중량이 200KG보다 클경우
                    indicator_arr[indicator_cnt] = Convert.ToInt32(tmp.ToString());
                    //안정화 차이 부분, 200 -> 30으로 변경(2019-09-30 오창휘 수정)
                    //logger.Info("메인타이머 INFO : 인디게이터 안정화 - CNT[" + indicator_cnt.ToString() + "] - MAX[" + indicator_arr.Max().ToString() + "] - MIN ["+indicator_arr.Min().ToString()+"]");
                    if (indicator_arr.Max() - indicator_arr.Min() < 30) //&& indicator_arr.Min() > 200)
                    {
                        //중량 안정화가 되었을 때 색상 Yellow(2019-10-08 오창휘 수정)
                        lblWeight_value_true.ForeColor = Color.Yellow;
                        //중량 안정화 색상변경(2019-10-15 오창휘 수정)
                        la_ment.ForeColor = Color.Red;
                        la_ment.BackColor = Color.White;
                        //중량 안정화 안내문 추가(2019-10-15 오창휘 수정)
                        //이전으로(2020-03-25 한민호)
                        //멘트 전에 카드를 대는 사람이 있어서 포커스 가게 함(2020-03-24 한민호)
                        //if (AW_CHK != "3")
                        //{
                        //    la_ment.Text = "RFID 카드를 대주세요 ";
                        //    txtManualRfid.Focus();
                        //    txtManualRfid.SelectAll();
                        //}
                        //else
                        //{
                        //    la_ment.Text = la_ment.Text;
                        //}
                        la_ment.Text = (AW_CHK != "3" ? "RFID 카드를 (최대한 가까이)대주세요 " : la_ment.Text);

                        if (CardRead == false)
                        {
                            if (lblWeight_value.Text != "-9999" && AW_CHK != "3")//AW_CHK 1 일 때 IN으로 진입(2019-09-30 오창휘 수정)
                            {
                                // 중량 안정화시 RFID 카드를 대주세요 멘트
                                //if (textBox1.Text == "")
                                //필요 없음 뺌(2019-09-30 오창휘 수정)
                                //if ((Math.Abs(indicator_stopper - indicator_arr[indicator_cnt])) <= 60 && indicator_arr[indicator_cnt] < 300 && indicator_stopper < 300)
                                if (CheckingSpecialText(lblWeight_value.Text))
                                {
                                    max_weight = Convert.ToInt32(lblWeight_value.Text.Replace(",", ""));
                                }

                                //textBox3.Text += max_weight + Environment.NewLine;
                                if (indicator_arr.Max() >= max_weight & indicator_arr.Max() > 200)
                                {
                                    if (Convert.ToInt32(lblWeight_value.Text.Replace(",", "")) > 200)
                                    {
                                        //textBox3.Text = "ret = " + Convert.ToString(ret);
                                        la_ment.Text = "RFID 카드를 (최대한 가까이)대주세요 ";
                                        //멘트 전에 카드를 대는 사람이 있어서 포커스 가게 함(2020-03-24 한민호)
                                        //txtManualRfid.Focus();
                                        //txtManualRfid.SelectAll();

                                        //textBox3.Text = "ret = " + ret;
                                        //계량대 딜레이 1초 늘림 (2019-09-25 오창휘 수정)
                                        //if (ret == 6)
                                        //if (ret == 8)
                                        //{
                                        //timer1.Start();
                                        player13.Play();
                                        //}
                                        //ret++;
                                        CardRead = true;
                                        //return;
                                    }
                                }
                                else CardRead = false;
                            }
                            else CardRead = false;
                        }
                        //else
                        //{
                        //    indicator_stopper = 0;
                        //    indicator_cnt = 0;
                        //    indicator_arr[indicator_cnt] = 0;
                        //}
                    }
                    else
                    {
                        CardRead = false;
                        //중량 안정화가 되지 않았을 때 색상 Red(2019-10-08 오창휘 수정)
                        lblWeight_value_true.ForeColor = Color.Red;
                        //중량 안정화 색상변경(2019-10-15 오창휘 수정)
                        if (wght_save_chk)
                        {
                            la_ment.ForeColor = Color.Red;
                            la_ment.BackColor = Color.White;
                            wght_save_chk = false;
                        }
                        else
                        {
                            la_ment.ForeColor = Color.White;
                            la_ment.BackColor = Color.Red;
                            wght_save_chk = true;
                        }
                        //중량 안정화 안내문 추가(2019-10-15 오창휘 수정)
                        //중량 안정화 안내문 추가(2019-10-15 오창휘 수정)
                        la_ment.Text = (AW_CHK != "3" ? "중량 안정화 중입니다.... " + Environment.NewLine + "잠시만 기다려주세요." : la_ment.Text);
                    }

                    // 계량 완료 후 다음차량 진입 시간이 짧아 중량 안정화 사용 안함.
                    // 계량완료된 상태이고 현재/이전 중량 이 300KG보다 작고 이전 중량차이가 60KG 보다 작은경우, 차단기 동작 가능 상태로 설정
                    //계량 end 조건으로 추가                    
                    //if ((Math.Abs(indicator_stopper - indicator_arr[indicator_cnt])) <= 60 && indicator_arr[indicator_cnt] < 300 && indicator_stopper < 300)
                    //{
                    //    //계량 end 조건으로 추가
                    //    CardRead = false;
                    //}

                    if (CardRead)
                    {
                        indicator_stopper = 0;
                        indicator_cnt = 0;
                        indicator_arr[indicator_cnt] = 0;
                    }
                    else
                    {
                        indicator_stopper = indicator_arr[indicator_cnt];
                        indicator_cnt++;
                    }
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
            }

            try
            {
                if (serialPort[0].IsOpen)
                {
                    rfid1.Image = imageList2.Images[0];
                }
                else
                {
                    SerialPortOpen(serialPort[0]);
                    rfid1.Image = imageList2.Images[1];
                }
            }
            catch (Exception ex)
            {
                logger.Error("ERR!! timer_main_Tick : RFID 연결 에러 : " + ex.Message.ToString());
                SerialPortOpen(serialPort[0]);
                rfid1.Image = imageList2.Images[1];
                timer_main.Enabled = true;
            }

            if (Convert.ToInt32(lblWeight_value.Text.Replace(",", "")) == 0)
            {
                ret = 0;
            }

            timer_main.Enabled = true;
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
        }

        #endregion

        #region LPR 처리함수

        #region LPR 연결

        private void server_connect_LPR()
        {
            //LPR_IP=10.1.2.153
            //LPR_PORT=6486
            try
            {
                ServerIPAddress = IPAddress.Parse(HW_LPR_IP);
                PORT = Convert.ToInt32(HW_LPR_PORT);

                if (SocketClient_LPR != null)
                {
                    SocketClient_LPR.Close();
                }

                ServerIPEndPoint = new IPEndPoint(ServerIPAddress, PORT);
                SocketClient_LPR = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


                SocketClient_LPR.Connect(ServerIPEndPoint);
                if (SocketClient_LPR.Connected)
                {
                    logger.Info("LPR 접속 ");
                    //lblMSG.Text = "LPR 접속";
                    ThreadClient_LPR =
                        new System.Threading.Thread(new System.Threading.ThreadStart(RecieveFromServer_LPR));
                    ThreadClient_LPR.IsBackground = true;
                    ThreadClient_LPR.Start();
                }
            }
            catch (Exception ex)
            {
                logger.Error("소켓 에러 : " + ex.Message.ToString());
                server_close_LPR();
            }
        }

        #endregion

        #region LPR,LED 연결끊기

        private void server_close_LPR()
        {
            if (SocketClient_LPR != null)
            {
                SocketClient_LPR.Close();
            }

            if (ThreadClient_LPR != null)
            {
                if (ThreadClient_LPR.IsAlive)
                {
                    ThreadClient_LPR.Abort();
                }
            }

            SocketClient_LPR = null;
            ThreadClient_LPR = null;
        }

        #endregion

        #region LPR,LED 메세지 수신

        private void RecieveFromServer_LPR()
        {
            //DataTable dt = null;
            while (true)
            {
                try
                {
                    if (!SocketClient_LPR.Connected)
                    {
                        logger.Error("RecieveFromServer_LPR(!SocketClient_LPR.Connected) : ");
                        return;
                    }

                    //Byte 200 고정
                    Byte[] ReceiveByte = new Byte[200];
                    String ReceiveData = "";

                    int nValue = SocketClient_LPR.Receive(ReceiveByte, ReceiveByte.Length, 0);

                    // Encoding Default 고정
                    ReceiveData = Encoding.Default.GetString(ReceiveByte, 0, ReceiveByte.Length);
                    logger.Info("ReceiveData : " + ReceiveData);
                    list = ReceiveData.Split(' ');

                    if (list[0] == "[IMG]")
                    {
                        //계량대번호 체크(2019-10-07 오창휘 수정)
                        logger.Info("list[2] : " + list[2] + " / WEIGHT_NO : " + WEIGHT_NO);
                        if (list[2] == WEIGHT_NO)
                        {
                            // 차량 번호 크로스 체크
                            try
                            {
                                if (rfid_no.Trim().Length > 0)
                                {
                            
                                    //LPR 차량번호
                                    img_vehl_no = list[4];

                                    logger.Info("list[4] IMG_CAR_NO : " + img_vehl_no);

                                    string weight_knd = "";
                                    string[] weightkind = new string[]
                                    {
                                        "10.1.3.18", "10.1.3.19", "10.1.3.20", "10.1.3.21",
                                        "10.1.6.18", "10.1.6.19", "10.1.6.20", "10.1.6.21"
                                    };
                                    switch (WEIGHT_NO)
                                    {
                                        case "0100":
                                            weight_knd = weightkind[0];
                                            break;
                                        case "0101":
                                            weight_knd = weightkind[1];
                                            break;
                                        case "0102":
                                            weight_knd = weightkind[2];
                                            break;
                                        case "0103":
                                            weight_knd = weightkind[3];
                                            break;
                                        case "0104":
                                            weight_knd = weightkind[4];
                                            break;
                                        case "0105":
                                            weight_knd = weightkind[5];
                                            break;
                                        case "0106":
                                            weight_knd = weightkind[6];
                                            break;
                                        case "0107":
                                            weight_knd = weightkind[7];
                                            break;
                                    }
                                    img_vehl_file = @"http://"
                                                  + weight_knd
                                                  + ":6486/"  //PORT 번호 고정
                                                  + WEIGHT_NO
                                                  + "/"
                                                  + DateTime.Now.ToString("yyyyMMdd")
                                                  + "/"
                                                  + list[6];

                                    logger.Info("list[4] IMG_FILE_PATH : " + img_vehl_file);
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                            }
                        }
                    }

                    //2019-10-14 오창휘 수정
                    Thread.Sleep(500);

                    // LPR 값 수신후 종료
                    if (ReceiveData != "")
                    {
                        if (SocketClient_LPR != null)
                        {
                            SocketClient_LPR.Close();
                        }
                        if (ThreadClient_LPR != null)
                        {
                            if (ThreadClient_LPR.IsAlive)
                            {
                                ThreadClient_LPR.Abort();
                            }
                        }
                        SocketClient_LPR = null;
                        ThreadClient_LPR = null;
                        break;
                    }
                    //if (nValue <= 0)
                    //{
                    //    throw new SocketException();
                    //}
                }
                catch (Exception ex)
                {
                    logger.Error("LPR 수신에러 : " + ex.ToString());
                }
            }
        }

        //private string EUCKR_TO_UTF8(string strEUCKR)
        //{
        //    return Encoding.UTF8.GetString(
        //        Encoding.Convert(
        //            Encoding.GetEncoding("euc-kr"),
        //            Encoding.UTF8,
        //            Encoding.GetEncoding("euc-kr").GetBytes(strEUCKR)));
        //}

        //public bool isContainHangul(string s)
        //{
        //    char[] charArr = s.ToCharArray();

        //    foreach (char c in charArr)
        //    {
        //        if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        #endregion

        #region LPR에 값 전송

        private void server_send_LPR(string gubun, string send_msg1, string send_msg2, string WEIGHTNO)
        {
            string s_send_msg = "";
            try
            {
                if (SocketClient_LPR.Connected)
                {
                    Byte[] SendBuffer;
                    string strBuffer;
                    if (gubun == "LPR")
                    {
                        s_send_msg = "[CMD] LPR " + WEIGHTNO + " Grab"; // 촬영 명렁어
                            //"[CMD] PID 0104 Txt PC 23 PT 40 Msg 전광판;시험중";
                            //"[CMD] LPR 0104 Grab";
                            //"0000:[CMD] PID 0101 Txt PC 17 PT 120 Msg  " + send_msg1 + ";" + send_msg2;
                        logger.Info("s_send_msg = " + s_send_msg);
                        //"[CMD] LPR 0104 Grab";
                    }
                    else
                    {
                        //                                              색상  표출시간     나올메시지1(상단) 나올메시지1(하단)
                        s_send_msg = "[CMD] PID " + WEIGHTNO + " Txt PC 23 PT 8 Msg  " + send_msg1 + ";" +
                                     send_msg2; //전광판 행선지 출력 명렁어
                        //"//PID 0104 Txt PC 17 PT 120 Msg  " + send_msg1 + ";" + send_msg2;
                        logger.Info("s_send_msg = " + s_send_msg);
                    }

                    strBuffer = String.Format("{0}", s_send_msg);

                    //EUC-KR 인코딩 <== 변경하지 말것
                    //20190909 김상우
                    SendBuffer = System.Text.Encoding.GetEncoding(51949).GetBytes(strBuffer);
                        //Encoding.UTF8.GetBytes(strBuffer);
                    int ret = SocketClient_LPR.Send(SendBuffer, 0, SendBuffer.Length, 0);
                    if (ret <= 0)
                    {
                        throw new SocketException();
                    }
                }
                else
                {
                    logger.Info("lpr 접속 오류");
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException = " + se.Message.ToString());
                logger.Error("SocketException = " + se.Message);
                SocketClient_LPR.Close();
                SocketClient_LPR.Dispose();
            }
        }

        #endregion


        #endregion

        #region 수동계량

        private void btnRFID_Click(object sender, EventArgs e)
        {
            if (txtManualWeight.Text != "")
            {   
                string Str = txtManualWeight.Text;
                logger.Info(Str);
                int Num;
                bool isNum = int.TryParse(Str, out Num);
                if (isNum)
                {
                    lblWeight_value.Text = Convert.ToInt32(txtManualWeight.Text).ToString();
                    logger.Info(lblWeight_value.Text);
                    tmp_down = Convert.ToInt32(txtManualWeight.Text).ToString();
                    logger.Info(tmp_down);
                }
            }

            if (lblWeight_value.Text != "0")
            {
                if (lblWeight_value.ForeColor == Color.Yellow)
                {
                    // 테스트용 라이브 반영 시 주석(2019-10-15 오창휘 수정)

                    rfid_no = txtManualRfid.Text;

                    //MessageBox.Show("4 : " + rfid_no);
                    //계량 시작
                    //수동계량에서 배차정보 체크 옮김(2019-09-30 오창휘 수정)
                    DataTable dt_rfid = DB_Process.FINDVEHLNO2(rfid_no); //SP_MU_WEIGHT_RECEPT_CHK

                    if (dt_rfid != null)
                    {
                        int msg = Convert.ToInt32(dt_rfid.Rows[0]["CNT"].ToString());

                        if (msg == 0)
                        {
                            //계량진행 상태값 추가 (2019-09-30 오창휘 수정)
                            AW_CHK = "3";
                            //음성 수정(2019-10-31 오창휘 수정)
                            //player67.Play();
                            player75.Play();
                            //안내 멘트 수정(2019-10-31 오창휘 수정)
                            //la_ment.Text = "배차 정보가 없습니다. ";
                            la_ment.Text = "배차정보가 없습니다. 차량 이동 후 해당 업체에 문의바랍나다.";
                            logger.Error(this.ToString() + " 배차정보 없음 P_RFID_CARD : [" + rfid_no + "]");
                            RedWindowform = new RedWindow();
                            RedWindowform.TopMost = true;
                            //안내 멘트 수정(2019-10-31 오창휘 수정)
                            //RedWindowform.labelControl1.Text = "배차 정보가 없습니다. ";
                            RedWindowform.labelControl1.Text = "배차정보가 없습니다. 차량 이동 후 해당 업체에 문의바랍나다.";
                            if (RedWindowform.ShowDialog() == DialogResult.OK)
                            {
                                RedWindowform.Close();
                            }
                            //안내 멘트 수정(2019-10-31 오창휘 수정)
                            //la_ment.Text = "배차 정보가 없습니다. ";
                            la_ment.Text = "배차정보가 없습니다. 차량 이동 후 해당 업체에 문의바랍나다.";

                            //txtManualRfid.Text = "";
                            txtManualRfid.Focus();
                            txtManualRfid.SelectAll();
                            return;
                        }
                    }

                    logger.Info("begin Main_Load()");
                    logger.Info(LPR_USE);
                    // 사용여부 체크와 관계 없이 LPR 스틸컷은 무조건 찍어서 저장 함(2019-10-15 오창휘 수정)
                    // LPR 사용 여부 체크
                    //if (LPR_USE == "Y")
                    //{
                    //LPR_Load();

                    //막음(2019-12-30 대한제강)
                    ////테스트 시 주석처리(2019-10-15 오창휘 수정)
                    //if (LPR_Load() == false)
                    //{
                    //    return;
                    //}

                    //}

                    if (Main_03_Load() == false) //차량관제서 배차를 주지 않는 경우 (사내이송,테스트계량)
                    {
                        //사내이송,TEST 계량이 아닌애덜 계량
                        Main_Load();
                    }

                    logger.Info("end Main_Load()");
                    
                    //계속 계량 시키기 위해 추가(2020-02-17 한민호)
                    lblInput.Text = "";
                    txtManualRfid.Focus();
                    txtManualRfid.SelectAll();
                }
            }
        }

        #endregion

        #region 종료타이머

        private void end_timer_Tick(object sender, EventArgs e)
        {
            end_timer.Enabled = false;
            CardRead = false;
            Prev_rfid = "";

            if (RedWindowform != null)
            {
                RedWindowform.Close();
            }
        }

        #endregion

        #region 다시계량

        private void btnRetry_Click(object sender, EventArgs e)
        {
            txt_clear();
            ReceveData1 = "";
            CardRead = false;

            Prev_rfid = "";
            txtManualRfid.Focus();
        }

        #endregion

        #region 재발행

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            // 재발행

            //by_Matrial("Y", "02", "201", "2016", "161108N00008", "161108NW0004");

            //리셋 되더라도 해당 함체에서 마지막 2차계량 번호로 찾아서 출력(2020-03-17 한민호)
            if (rfid_seq == "" || rfid_seq == null)
            {
                //2차계량일시로 변경(2020-03-28 한민호)  
                String Query = " SELECT SEQ_NO FROM TB_WS02_0002 WHERE OUT_STATE = '" + Weight_Area + "' AND OUT_WGT_DT = (SELECT MAX(OUT_WGT_DT) FROM TB_WS02_0002 WHERE OUT_STATE = '" + Weight_Area + "') ";
                //String Query = " SELECT MAX(SEQ_NO) AS SEQ_NO FROM TB_WS02_0002 WHERE OUT_STATE = '" + Weight_Area + "' ";
                ServiceAdapter _svc = new ServiceAdapter();
                DataSet ds = _svc.GetQuery(Query);
                //2차계량 된 건이 없을 경우 함체 프로그램이 종료되는 문제 때문에 수정(2020-04-20 한민호)
                if (ds.Tables[0].Rows.Count > 0)
                    rfid_seq = ds.Tables[0].Rows[0]["SEQ_NO"].ToString();
            }
            if (rfid_seq != "")
            {
                //대한제강(2020-02-20 한민호)
                dt_pnt_qty = DB_Process.pnt_qty("N", InOut_Gubun, item_jung, item_so, rfid_seq);

                if (dt_pnt_qty.Rows[0]["ITEM_TYPE_NM"].ToString() == "원자재")
                {
                    SubMatrial_원자재 Print_etc = new SubMatrial_원자재(dt_pnt_qty);

                    Print_etc.Print();
                    Print_etc.Dispose();
                }
                else
                {
                    SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);

                    Print_etc.Print();
                    Print_etc.Dispose();
                }

                //미리보기 화면
                //Print_etc.ShowPreview();
                //인쇄
                //Print_etc.Print();
                //Print_etc.Dispose();

                //DB_Process.PRINT_PAGE_CNT_SET(Weight_Area, 1);  //출력매수1장


                //by_Matrial(Gubun, InOut_Gubun, item_jung, item_so, rfid_seq, wght_no);
                //print_check();
                txtManualRfid.Focus();
            }
        }

        #endregion

        #region 화면 CLEAR 타이머

        private void clear_timer_Tick(object sender, EventArgs e)
        {
            rfid_no = "";
            txt_clear();
            end_timer.Enabled = true;
            clear_timer.Enabled = false;
        }

        #endregion

        private string txtsetting(string data)
        {
            string ret = string.Empty;
            if (CheckingSpecialText(data) == false) // 숫자를 뺀 나머지 제외
            {
                if (data == "0")
                {
                    return data;
                }

                int cdata = Convert.ToInt32(data);
                ret = string.Format("{0:#,##0}", cdata);
            }
            else
            {
                ret = data;
            }

            return ret;
        }

        public bool CheckingSpecialText(string txt)
        {
            string str = @"[~!@\#$%^&*\()\=+|\\/:;?""<>']";
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(str);
            return rex.IsMatch(txt);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            btnRFID.PerformClick();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            StillCut(); //스틸컷 함수(2020-02-05 한민호)

            //server_send_LPR("LPR", "전광판", "Test", WEIGHT_NO);
            //Thread.Sleep(1000);
            //if (list.Length > 0)
            //{
            //    updatepath(list, rfid_seq);
            //}
        }

        //스틸컷 함수(2020-02-05 한민호)
        private void StillCut()
        {
            //카메라 스틸컷 FTP업로드 추가(2020-01-30 오창휘 수정) START
            //Control.ControlCollection listCon = PnlAxis.Controls;
            //AxAXISMEDIACONTROLLib.AxAxisMediaControl ax1 = listCon[0] as AxAXISMEDIACONTROLLib.AxAxisMediaControl;
            //LPR 카메라 사용 안함(2020-04-26 한민호)
            //AxAXISMEDIACONTROLLib.AxAxisMediaControl ax2 = listCon[1] as AxAXISMEDIACONTROLLib.AxAxisMediaControl;

            FtpWebRequest req;
            DateTime today = DateTime.Now;

            string dir = "신평1/" + today.ToString("yyyyMMdd") + "/";
            string filePath = "C:\\" + today.ToString("yyyyMMddHHmmss") + cam_cd1 + ".jpg";
            string fileName = Path.GetFileName("C:\\" + today.ToString("yyyyMMddHHmmss") + cam_cd1 + ".jpg");

            //사진 저장 폴더명 수정(2020-02-11 오창휘 수정))
            string direc1 = WEIGHT_NO.Substring(0, 4) + "/";
            //string direc1 = "";
            string direc2 = today.ToString("yyyyMMdd") + "/";

            //사진 저장 폴더명 수정(2020-02-11 오창휘 수정))
            //switch (WEIGHT_NO)
            //{
            //    case "11001":
            //        direc1 = "신평1/";
            //        break;
            //    case "11002":
            //        direc1 = "신평2/";
            //        break;
            //    case "12001":
            //        direc1 = "녹산1/";
            //        break;
            //    case "12002":
            //        direc1 = "녹산2/";
            //        break;
            //    case "12101":
            //        direc1 = "녹산가공1/";
            //        break;
            //    case "12102":
            //        direc1 = "녹산가공2/";
            //        break;
            //    case "13001":
            //        direc1 = "평택1/";
            //        break;
            //    case "13002":
            //        direc1 = "평택2/";
            //        break;
            //}

            file_path1 = ftp_host + direc1 + direc2;
            file_path2 = ftp_host + direc1 + direc2;

            //MessageBox.Show("STEP1");

            try
            {
                FtpWebRequest ftpWebRequest_check1 = (FtpWebRequest)WebRequest.Create(ftp_host + direc1);
                ftpWebRequest_check1.Credentials = new NetworkCredential(ftp_user, ftp_pass);
                var request_chk1 = ftpWebRequest_check1;

                //MessageBox.Show("STEP2");

                request_chk1.Method = WebRequestMethods.Ftp.ListDirectory;
                try
                {
                    using (var result = (FtpWebResponse)request_chk1.GetResponse())
                    {
                        result.Close();  //정상 종료
                    }
                }
                catch
                {
                    FtpWebRequest ftpWebRequest1 = (FtpWebRequest)WebRequest.Create(ftp_host + direc1);
                    ftpWebRequest1.Credentials = new NetworkCredential(ftp_user, ftp_pass);
                    var request1 = ftpWebRequest1;
                    request1.Method = WebRequestMethods.Ftp.MakeDirectory;
                    using (var resp = (FtpWebResponse)request1.GetResponse())
                    {
                        resp.Close();
                    }
                }

                //MessageBox.Show("STEP3");

                FtpWebRequest ftpWebRequest_check2 = (FtpWebRequest)WebRequest.Create(ftp_host + direc1 + direc2);
                ftpWebRequest_check2.Credentials = new NetworkCredential(ftp_user, ftp_pass);
                var request_chk2 = ftpWebRequest_check2;
                request_chk2.Method = WebRequestMethods.Ftp.ListDirectory;
                try
                {
                    using (var result = (FtpWebResponse)request_chk2.GetResponse())
                    {
                        result.Close();  //정상 종료
                    }
                }
                catch
                {
                    FtpWebRequest ftpWebRequest2 = (FtpWebRequest)WebRequest.Create(ftp_host + direc1 + direc2);
                    ftpWebRequest2.Credentials = new NetworkCredential(ftp_user, ftp_pass);
                    var request2 = ftpWebRequest2;
                    request2.Method = WebRequestMethods.Ftp.MakeDirectory;
                    using (var resp = (FtpWebResponse)request2.GetResponse())
                    {
                        resp.Close();
                    }
                }

                //MessageBox.Show("STEP4");

            }
            catch
            {
            }

            if (cam_ip1 != "")
            //if (ax1.Tag.ToString() != "STOP")
            {
                //MessageBox.Show("STEP5");

                file_path1 = file_path1 + today.ToString("yyyyMMddHHmmss") + cam_cd1 + ".jpg";
                try
                {
                    //MessageBox.Show("STEP5-1");
                    //이미지 스틸컷 방식 변경(20200611 오창휘 수정)
                    byte[] data = null;
                    using (WebClient wc = new WebClient())
                    {
                        wc.Credentials = new NetworkCredential("root", "zaq1@wsx");
                        data = wc.DownloadData("http://" + cam_ip1 + "/jpg/image.jpg");
                    }
                    /*
                    //영상검수 소스와 동일하게 수정(2020-04-26 한민호)
                    object fileObject = null;
                    //object fileObject;
                    //MessageBox.Show("STEP5-1-1");
                    int filelen = 0;
                    //MessageBox.Show("STEP5-1-2");
                    ax1.GetCurrentImage(0, out fileObject, out filelen);
                    //MessageBox.Show("STEP5-2");
                    byte[] data = new byte[filelen];
                    //Add JPEG header to new byte array
                    data[0] = Convert.ToByte(255);
                    data[1] = Convert.ToByte(216);
                    data[2] = Convert.ToByte(255);
                    data[3] = Convert.ToByte(224);
                    data[4] = Convert.ToByte(0);
                    data[5] = Convert.ToByte(16);
                    data[6] = Convert.ToByte(74);
                    data[7] = Convert.ToByte(70);
                    data[8] = Convert.ToByte(73);
                    data[9] = Convert.ToByte(70);
                    //Copy actual image into new byte array
                    Buffer.BlockCopy(fileObject as Array, 10, data, 10, filelen - 10);
                    //data = ObjectToByte(fileObject);
                    */

                    //MessageBox.Show("STEP5-3");

                    FtpWebRequest ftpWebRequest = WebRequest.Create(file_path1) as FtpWebRequest;
                    ftpWebRequest.Credentials = new NetworkCredential(ftp_user, ftp_pass);
                    ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

                    //MessageBox.Show("STEP5-4");

                    ftpWebRequest.ContentLength = data.Length;
                    using (Stream reqStream = ftpWebRequest.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);
                    }

                    //MessageBox.Show("STEP5-5");

                    using (FtpWebResponse resp = (FtpWebResponse)ftpWebRequest.GetResponse())
                    {
                        resp.Close();
                    }

                }
                catch (Exception e)
                {
                    //logger.Info("스틸컷 에러 : " + e.Message.ToString());
                    logger.Info("스틸컷 에러 : " + e.StackTrace);
                }

                //MessageBox.Show("STEP6");

                //카메라 스틸컷 FTP업로드 추가(2020-01-30 오창휘 수정) END
            }

            //LPR 카메라 없음_막음(2020-04-22 한민호)
            //if (ax2.Tag.ToString() != "STOP")
            //{
            //    file_path2 = file_path2 + today.ToString("yyyyMMddHHmmss") + ax2.Name + ".jpg";

            //    try
            //    {
            //        object fileObject;
            //        int filelen = 0;
            //        ax2.GetCurrentImage(0, out fileObject, out filelen);

            //        byte[] data = new byte[filelen];
            //        //Add JPEG header to new byte array
            //        data[0] = Convert.ToByte(255);
            //        data[1] = Convert.ToByte(216);
            //        data[2] = Convert.ToByte(255);
            //        data[3] = Convert.ToByte(224);
            //        data[4] = Convert.ToByte(0);
            //        data[5] = Convert.ToByte(16);
            //        data[6] = Convert.ToByte(74);
            //        data[7] = Convert.ToByte(70);
            //        data[8] = Convert.ToByte(73);
            //        data[9] = Convert.ToByte(70);
            //        //Copy actual image into new byte array
            //        Buffer.BlockCopy(fileObject as Array, 10, data, 10, filelen - 10);
            //        //data = ObjectToByte(fileObject);

            //        FtpWebRequest ftpWebRequest = WebRequest.Create(file_path2) as FtpWebRequest;
            //        ftpWebRequest.Credentials = new NetworkCredential(ftp_user, ftp_pass);
            //        ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

            //        ftpWebRequest.ContentLength = data.Length;
            //        using (Stream reqStream = ftpWebRequest.GetRequestStream())
            //        {
            //            reqStream.Write(data, 0, data.Length);
            //        }

            //        using (FtpWebResponse resp = (FtpWebResponse)ftpWebRequest.GetResponse())
            //        {
            //            resp.Close();
            //        }

            //    }
            //    catch
            //    {
            //    }
            //}
        }

        private void lblWeight_value_TextChanged(object sender, EventArgs e)
        {
            lblWeight_value_true.Text = txtsetting(lblWeight_value.Text);

            //인디게이터 값 바뀔때 마다 계량중량 업데이트(대한제강 2020-01-22)
            String Query = " UPDATE TB_WS02_0003 SET IND_WEIGHT = '" + lblWeight_value.Text + "' WHERE WEIGHT_NO = '" + WEIGHT_NO + "'";
            ServiceAdapter _svc = new ServiceAdapter();
            _svc.GetQuery(Query);
        }

        private void lblFirst_wght_TextChanged(object sender, EventArgs e)
        {
            lblFirst_wght_true.Text = txtsetting(lblFirst_wght.Text);
        }

        private void lblSecond_wght_TextChanged(object sender, EventArgs e)
        {
            lblSecond_wght_true.Text = txtsetting(lblSecond_wght.Text);
        }

        private void lblReal_wgt_TextChanged(object sender, EventArgs e)
        {
            lblReal_wgt_true.Text = txtsetting(lblReal_wgt.Text);
        }

        private void lblProd_wgt_true_TextChanged(object sender, EventArgs e)
        {
            //대한제강 막기(2020-01-20 한민호)
            //lblProd_wgt_true.Text = txtsetting(lblProd_wgt.Text);
        }

        //대한제강 추가(2020-01-20 한민호)
        private void lblProd_wgt_TextChanged(object sender, EventArgs e)
        {
            lblProd_wgt_true.Text = txtsetting(lblProd_wgt.Text);
        }
        private void CrossCheck()
        {
            //행선지 표시 Kimsw 20190917
            //DataTable dt = DB_Process.getDownSite("SP_TB_WS02_0001_R2",
            //    DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), item_so);

            //foreach (DataRow row in dt.Rows)
            //{
            //    downsite = row["DOWN_SITE_NM"].ToString();
            //    //break;
            //}

            //server_send_LPR("PID", "행선지", downsite, WEIGHT_NO);down_site_nm
            //down_site_nm 잘 가져와서 해당 부분 변경(2019-10-01 오창휘 수정)

            try
            {
                logger.Info(" START - 하차지 : " + down_site_nm + " ");
                server_send_LPR("PID", "행선지", down_site_nm, WEIGHT_NO);
                logger.Info(" END   - 하차지 : " + down_site_nm + " ");
            }
            catch (Exception e )
            {
                logger.Error(e.ToString());
                return;
            }
        }
        //30초마다 행선지표시 추가(김상우 차장 2019-10-01)
        private int pid_time = 0;
        private void pid_timer_Tick(object sender, EventArgs e)
        {
            pid_timer.Enabled = false;
            pid_time++;
            //lblMSG.Text = Convert.ToString(pid_time);
            if (pid_time >= 11)
            {
                //lblMSG.Text = string.Empty;                       
                pid_time = 0;
                pid_timer.Stop();         
                pid_timer.Enabled = false;
            }
            else
            {
                //수동계량 시 주석처리(2019-10-17 오창휘 수정) START
                //server_connect_LPR();
                //Thread.Sleep(500);
                //CrossCheck();
                //pid_timer.Enabled = true;
                //수동계량 시 주석처리(2019-10-17 오창휘 수정) END
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            la_ment.ForeColor = System.Drawing.Color.Red;
        }

        private void print_bt_Click(object sender, EventArgs e)
        {
            txtManualRfid.Focus();
        }

        //RFID리딩 시 계량 처리
        private void txtManualRfid_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtManualRfid.Text == "")
            {
                return;      
            }
            try
            {
                //카드체크 재수정(2020-03-24 한민호)
                if (e.KeyCode == Keys.Enter)
                {
                    //중량안정화 됐을 때만 RFID 태그 적용(2020-03-25 한민호)
                    if (CardRead && AW_CHK != "3")
                        //if (CardRead)
                    {

                        string ManualRfid = txtManualRfid.Text; //RFID 풀번호
                        string RfidNo = "";

                        //8자리 이상이면 무조건 뒷자리 5자리 짜름(2020-03-24 한민호)
                        if (ManualRfid.Length >= 8)
                        {
                            RfidNo = ManualRfid.Substring(ManualRfid.Length - 5, 5);

                            //MessageBox.Show(RfidNo);
                            //고정RF카드 키패트입력변환 (2021-07-23 정성호)
                            DataTable dataTable = new DataTable();

                            dataTable = DB_Process.Get_RFID_INFO("WS_010");

                            if (dataTable.Rows.Count > 0) //고정 RF정보가 있으면 연결된 키패드코드를 가져와서 변경해줌
                            {
                                var Query = (from rw in dataTable.AsEnumerable()
                                             where rw.Field<string>("CODE_VALUE1") == RfidNo
                                             select new
                                                        {
                                                            Keypad = rw.Field<string>("CODE_VALUE2")
                                                        }).FirstOrDefault();

                                if (Query != null)
                                {
                                    RfidNo = Query.Keypad;
                                    txtManualRfid.Text = RfidNo;
                                    btnRFID_Click(btnRFID, new EventArgs());
                                    txtManualRfid.Text = "";
                                }
                                else
                                {
                                    txtManualRfid.Text = RfidNo;
                                    btnRFID_Click(btnRFID, new EventArgs()); //수동계량
                                    txtManualRfid.Text = "";
                                }
                            }
                            else
                            {
                                txtManualRfid.Text = RfidNo;
                                btnRFID_Click(btnRFID, new EventArgs()); //수동계량
                                txtManualRfid.Text = "";
                            }

                        }
                    }
                    else
                    {
                        txtManualRfid.Text = "";
                    }

                    //if (ManualRfid.Length == 8)
                    //{
                    //    RfidNo = ManualRfid.Substring(3, 5);
                    //    txtManualRfid.Text = RfidNo;
                    //    btnRFID_Click(btnRFID, new EventArgs());    //수동계량
                    //    txtManualRfid.Text = "";
                    //}
                }
                ////카드체크 추가(2020-03-24 한민호)
                //if (CardRead)
                //{
                //    if (e.KeyCode == Keys.Enter)
                //    {
                //        string ManualRfid = txtManualRfid.Text; //RFID 풀번호
                //        string RfidNo = "";
                //        if (ManualRfid.Length == 8)
                //        {
                //            RfidNo = ManualRfid.Substring(3, 5);
                //            txtManualRfid.Text = RfidNo;
                //            btnRFID_Click(btnRFID, new EventArgs());    //수동계량
                //            txtManualRfid.Text = "";
                //        }
                //    }
                //}
                //else
                //{
                //    txtManualRfid.Text = "";
                //}
                //if (e.KeyCode == Keys.Enter)
                //{
                //    string ManualRfid = txtManualRfid.Text; //RFID 풀번호
                //    string RfidNo = "";
                //    if (ManualRfid.Length == 8)
                //    {
                //        RfidNo = ManualRfid.Substring(3, 5);
                //        txtManualRfid.Text = RfidNo;
                //        btnRFID_Click(btnRFID, new EventArgs());    //수동계량
                //        txtManualRfid.Text = "";
                //    }
                //}
            }
            catch
            {
                return;
            }
        }


        #region 버튼

        #region 숫자버튼

        private void btn01_Click(object sender, EventArgs e)
        {
            // 1
            if (lblInput.Text.Length < 4)
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;
                lblInput.Text += "1";
                txtManualRfid.Text += "1";
                //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
                //txtManualRfid.Focus();
            }
            //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
            txtManualRfid.Focus();

        }

        private void btn02_Click(object sender, EventArgs e)
        {
            // 2
            if (lblInput.Text.Length < 4)
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;
                lblInput.Text += "2";
                txtManualRfid.Text += "2";
                //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
                //txtManualRfid.Focus();
            }
            //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
            txtManualRfid.Focus();
        }

        private void btn03_Click(object sender, EventArgs e)
        {
            // 3
            if (lblInput.Text.Length < 4)
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;
                lblInput.Text += "3";
                txtManualRfid.Text += "3";
                //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
                //txtManualRfid.Focus();
            }
            //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
            txtManualRfid.Focus();
        }

        private void btn04_Click(object sender, EventArgs e)
        {
            // 4
            if (lblInput.Text.Length < 4)
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;
                lblInput.Text += "4";
                txtManualRfid.Text += "4";
                //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
                //txtManualRfid.Focus();
            }
            //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
            txtManualRfid.Focus();
        }

        private void btn05_Click(object sender, EventArgs e)
        {
            // 5
            if (lblInput.Text.Length < 4)
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;
                lblInput.Text += "5";
                txtManualRfid.Text += "5";
                //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
                //txtManualRfid.Focus();
            }
            //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
            txtManualRfid.Focus();
        }

        private void btn06_Click(object sender, EventArgs e)
        {
            // 6
            if (lblInput.Text.Length < 4)
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;
                lblInput.Text += "6";
                txtManualRfid.Text += "6";
                //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
                //txtManualRfid.Focus();
            }
            //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
            txtManualRfid.Focus();
        }

        private void btn07_Click(object sender, EventArgs e)
        {
            // 7
            if (lblInput.Text.Length < 4)
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;
                lblInput.Text += "7";
                txtManualRfid.Text += "7";
                //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
                //txtManualRfid.Focus();
            }
            //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
            txtManualRfid.Focus();
        }

        private void btn08_Click(object sender, EventArgs e)
        {
            // 8
            if (lblInput.Text.Length < 4)
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;
                lblInput.Text += "8";
                txtManualRfid.Text += "8";
                //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
                //txtManualRfid.Focus();
            }
            //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
            txtManualRfid.Focus();
        }

        private void btn09_Click(object sender, EventArgs e)
        {
            // 9
            if (lblInput.Text.Length < 4)
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;
                lblInput.Text += "9";
                txtManualRfid.Text += "9";
                //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
                //txtManualRfid.Focus();
            }
            //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
            txtManualRfid.Focus();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            //0 
            if (lblInput.Text.Length < 4)
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;
                lblInput.Text += "0";
                txtManualRfid.Text += "0";
                //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
                //txtManualRfid.Focus();
            }
            //4자리 이상이라도 무조건 포커스가 가야 함(2020-05-26 한민호)
            txtManualRfid.Focus();
        }

        private void btn01_DoubleClick(object sender, EventArgs e)
        {
            //if (lblRfid_no.Text.Length < 5)
            //{
            //    lblRfid_no.Text += "1";
            //}
        }

        private void btn02_DoubleClick(object sender, EventArgs e)
        {
            //if (lblRfid_no.Text.Length < 5)
            //{
            //    lblRfid_no.Text += "2";
            //}
        }

        private void btn03_DoubleClick(object sender, EventArgs e)
        {
            //if (lblRfid_no.Text.Length < 5)
            //{
            //    lblRfid_no.Text += "3";
            //}
        }

        private void btn04_DoubleClick(object sender, EventArgs e)
        {
            //if (lblRfid_no.Text.Length < 5)
            //{
            //    lblRfid_no.Text += "4";
            //}
        }

        private void btn05_DoubleClick(object sender, EventArgs e)
        {
            //if (lblRfid_no.Text.Length < 5)
            //{
            //    lblRfid_no.Text += "5";
            //}
        }

        private void btn06_DoubleClick(object sender, EventArgs e)
        {
            //if (lblRfid_no.Text.Length < 5)
            //{
            //    lblRfid_no.Text += "6";
            //}
        }

        private void btn07_DoubleClick(object sender, EventArgs e)
        {
            //if (lblRfid_no.Text.Length < 5)
            //{
            //    lblRfid_no.Text += "7";
            //}
        }

        private void btn08_DoubleClick(object sender, EventArgs e)
        {
            //if (lblRfid_no.Text.Length < 5)
            //{
            //    lblRfid_no.Text += "8";
            //}
        }

        private void btn09_DoubleClick(object sender, EventArgs e)
        {
            //if (lblRfid_no.Text.Length < 5)
            //{
            //    lblRfid_no.Text += "9";
            //}
        }

        private void btn0_DoubleClick(object sender, EventArgs e)
        {
            //if (lblRfid_no.Text.Length < 5)
            //{
            //    lblRfid_no.Text += "0";
            //}
        }

        #endregion

        #region 계량버튼 클릭

        private void btnEnter_Click(object sender, EventArgs e)
        {
            //카드체크 추가(2020-03-24 한민호)
            //중량안정화 됐을 때만 RFID 태그 적용(2020-03-25 한민호)
            if (CardRead && AW_CHK != "3")
            //if (CardRead)
            {
                //계량버튼 클릭 시 코드버튼값 던져주기(2020-03-26)
                txtManualRfid.Text = lblInput.Text;
                btnRFID_Click(btnRFID, new EventArgs());
            }
            else
            {
                logger.Info("중량 안전화 후 tag : ");
                la_ment.Text = "중량 안전화 후 계량하세요";
                return;
            }
            //ServiceAdapter _svc = new ServiceAdapter();
            ////int ch_1;
            ////int ch_2;
            //tmp_down = "0";
            //tmp_down = lblWeight_value.Text;
            //if (tmp_down == "-9999")
            //{
            //    return;
            //}
            //// 입력
            //if (rfid_no == "")
            //{
            //    if (wegiht_fg_03 == false || lblRfid_no.Text.Length >= 4)
            //    {
            //        // RFID 카드번호 수동입력
            //        rfid_no = lblRfid_no.Text;
            //        //계량 시작
            //        logger.Info("begin Main_Load()");

            //        // LPR 사용 여부 체크
            //        if (LPR_USE == "Y")
            //        {
            //            if (LPR_Load())
            //            {

            //            }
            //        }
            //        else
            //        {
            //            Main_Load();
            //        }

            //        logger.Info("end Main_Load()");
            //    }

            //    return;
            //}
        }

        #endregion

        #region 지움버튼 클릭

        private void btnDel_Click(object sender, EventArgs e)
        {
            // 지움
            try
            {
                if (lblInput.Text != txtManualRfid.Text)
                    txtManualRfid.Text = lblInput.Text;

                //lblInput.Text 이 0 일 경우 오류로 빠져서 수정(2020-05-26 한민호)
                if (lblInput.Text.Length > 0)
                {
                    lblInput.Text = lblInput.Text.Substring(0, lblInput.Text.Length - 1);
                    txtManualRfid.Text = txtManualRfid.Text.Substring(0, txtManualRfid.Text.Length - 1);
                }
                txtManualRfid.Focus();
            }
            catch
            {
            }
        }

        #endregion
                
        #endregion

        //모든 사항에 대해서 포커스가 가게 수정(2020-05-26 한민호)
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtManualRfid.Focus();
        }
        
        private void rfid1_Click(object sender, EventArgs e)
        {
            txtManualRfid.Focus();
        }

        private void InSensor_Click(object sender, EventArgs e)
        {
            txtManualRfid.Focus();
        }

        private void outSensor_Click(object sender, EventArgs e)
        {
            txtManualRfid.Focus();

        }

        private void txtManualWeight_Click(object sender, EventArgs e)
        {
            txtManualRfid.Focus();
        }

        private void timer_focus_Tick(object sender, EventArgs e)
        {
            timer_focus.Enabled = false;
            if (!checkBox1.Checked) txtManualRfid.Focus();
            timer_focus.Enabled = true;
        }

        private void txtManualRfid_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt_pnt_qty = new DataTable();
            dt_pnt_qty = DB_Process.pnt_qty("", "", "", "", txtseq.Text);

            if (dt_pnt_qty.Rows[0]["ITEM_TYPE_NM"].ToString() == "원자재")
            {
                SubMatrial_원자재 Print_etc = new SubMatrial_원자재(dt_pnt_qty);

                Print_etc.Print();
                Print_etc.Dispose();
            }
            else
            {
                SubMatrial_All Print_etc = new SubMatrial_All(dt_pnt_qty);

                Print_etc.Print();
                Print_etc.Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtManualRfid.Text = "1234";
            CardRead = true;
            txtManualRfid_KeyUp(new object(), new KeyEventArgs(Keys.Enter));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtManualWeight.Text = "+0030000";
        }

        private void txtManualWeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                Gap = int.Parse(textBox2.Text);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            rfid_no = txtRfid.Text;
            lblWeight_value.Text = txtWeight.Text;
            Main_Load();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dt_result_Gap = new DataTable();

            dt_result_Gap = DB_Process.GAP_Check("00305", "ALF");

            if (dt_result_Gap.Rows.Count > 0)
            {
                if (dt_result_Gap.Rows[0]["RESULT"].ToString() == "2") //2차계량일때
                {
                    TimeGap = dt_result_Gap.Rows[0]["TIMEGAP"].ToString();

                    //if (int.Parse(TimeGap) < Gap)
                    //{
                    //    player20.Play();
                        la_ment.Text = string.Format("1차 계량후 {0} 이후에 2차 계량을 진행 할 수 있습니다. {1}초 남았습니다.", getSecToMin(), Gap - int.Parse(TimeGap));

                    //    AW_CHK = "3";
                    //}
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}