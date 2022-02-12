using System;
using System.Data;
using System.Windows.Forms;
using log4net;
using OCT_WEIGHT.Manager.Common.util;
using OCT_WEIGHT.Manager.Common.info;
using System.Xml;
using OCT_WEIGHT.Manager.Common;
using System.IO;
using System.Reflection;
using DevExpress.XtraBars;
using OCT_WEIGHT.OctoCommon;
using DevExpress.XtraTabbedMdi;
using OCT_WEIGHT.Manager.Common.Popup;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace OCT_WEIGHT.Manager
{
    public partial class frmMain : Form
    {
        public ServiceAdapter _svc = null;

        protected static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private String DeployUrl;

        public string userid, UserName = "";
           
        public frmMain()
        {
            InitializeComponent();  
        }

        #region [Method] 열린화면 모두닫기

        public void mFormClose()
        {
            try
            {
                foreach (Form frmForm in this.MdiChildren)
                {
                    frmForm.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("열린화면 모두 닫기중 ERROR:" + ex.Message.ToString().Trim());
            }
        }

        #endregion

        private Form IsFormLoaded(string _MenuId)
        {
            Form _ret = null;
            foreach (Form frmForm in this.MdiChildren)
            {
                if (frmForm.Tag.Equals(_MenuId))
                {
                    frmForm.Activate();
                    _ret = frmForm;
                }
            }

            return _ret;
        }

        private void SetServiceURL()
        {
            string clientFolder = Path.GetDirectoryName(
                                      Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\";
            // 클라이언트에서 업데이트 파일의 목록을 정의한 XML 파일을 로드한다
            XmlDocument clientDoc = new XmlDocument();
            clientDoc.Load(clientFolder + UpdateManager.UPDATE_LIST_FILE_NAME);

            //ServiceAdapter.Url = UpdateManager.GetServiceUrl(clientDoc);
            DeployUrl = UpdateManager.GetUpdateUrl(clientDoc);
        }

        /// <summary>
        /// Loads the assembly.
        /// </summary>
        /// <param name="menuItemTagString">The menu item tag string.</param>
        /// <returns>로드된 어셈블리</returns>
        private Assembly LoadAssembly(string menuItemTagString)
        {
            Assembly asm;

#if DEBUG
            string LoadFileName =
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) +
                @"\" + menuItemTagString;
            LoadFileName = LoadFileName.Replace(@"file:\", "");
            asm = Assembly.LoadFrom(LoadFileName.Substring(0, LoadFileName.LastIndexOf(".")) + ".dll");
#else
            if (menuItemTagString.ToUpper().StartsWith("OctoCommon"))
			{
                asm = Assembly.LoadFrom("OctoCommon.dll");
			}
			else
			{
				asm = Assembly.LoadFrom(OctoCommon.Info.ConfigInfo.DeployServerUrl
					+ menuItemTagString.Substring(0, menuItemTagString.LastIndexOf(".")) + ".dll");
			}
#endif
            return asm;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Icon = OCT_WEIGHT.Manager.Properties.Resources.mark_8hL_icon;

            //if (_svc == null) _svc = new ServiceAdapter();

            // DB 서비스 URL 지정          
            SetServiceURL();

            //20190503 Kimsw 수정
            if (DBConn._DBConn() == false) //ORACLE DB CONNECT
            {
                MessageBox.Show(" DB 접속 오류 ");
            }

            //업데이트 추가(2019-12-03 한민호)
            //2019-12-21 대한제강
            Check_Update(); 

            Login();
            //timer1.Enabled = true;
        }

        //업데이트 추가(2019-12-03 한민호)
        private void Check_Update()
        {
            string clientFolder = Application.StartupPath + @"\";
            XmlDocument clientDoc = new XmlDocument();
            clientDoc.Load(clientFolder + UpdateManager.UPDATE_LIST_FILE_NAME);
            OCT_WEIGHT.Manager.Common.UpdateManager.Update(clientDoc);
        }

        private void Login()
        {
            using (frmLogin _login = new frmLogin())
            {
                try
                {
                    DialogResult drResult = _login.ShowDialog(this);
                    if (drResult == DialogResult.OK)
                    {
                        barStaticUserID.Caption = string.Format("접속아이디 : {0}", clsUserInfo.User_id);
                        barStaticUserName.Caption = string.Format("사용자명 : {0}", clsUserInfo.Emp_nm);
                        
                        /*
                        MessageBox.Show(Client_IP);
                        if (Client_IP == "192.1.1.48")
                        {
                            barStaticItem1.Caption = "한국철강 개발서버";
                        }
                        else if (Client_IP == "192.1.1.46")
                        {
                            barStaticItem1.Caption = "한국철강 운영서버";
                        }
                        */
                        
                        userid = clsUserInfo.User_id;
                        UserName = clsUserInfo.Emp_nm;
                    }
                    else
                    {
                        MessageBox.Show("접속 오류");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("로그인 창 생성중 ERROR:" + ex.Message.ToString().Trim());
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            try
            {
                CreateMainMenu();
            }
            catch (Exception ex)
            {
                logger.Error("메뉴 생성중 ERROR:" + ex.Message.ToString().Trim());
            }
        }

        ///
        /// 클라이언트 IP 주소 얻어오기...
        ///
        public static string Client_IP
        {
            get
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                string ClientIP = string.Empty;
                for (int i = 0; i < host.AddressList.Length; i++)
                {
                    if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        ClientIP = host.AddressList[i].ToString();
                    }
                }
                return ClientIP;
            }
        }

        #region 메뉴클릭 관련 Overload Method

        //실제 메뉴 클릭시 호출
        private void MainMenu_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            OctoBarButtonItem mItem = (OctoBarButtonItem)e.Item;
            try
            {
                Form _form = IsFormLoaded(mItem.MenuID);
                if (_form != null)
                {
                    foreach (XtraMdiTabPage page in xtraTabbedMdiManager1.Pages)
                    {
                        if (page.MdiChild == _form)
                        {
                            xtraTabbedMdiManager1.SelectedPage = page;
                            this.Text = "대한제강 관리시스템 [" + page.Text + "]";
                            break;
                        }

                    }

                    this.Cursor = Cursors.Default;
                    return;
                }

                string[] strTags = mItem.FormName.Split(new char[1] { ',' });
                object[] _args = new object[strTags.Length];
                if (strTags.Length > 1 && strTags[1] != string.Empty)
                {
                    _args = new object[strTags.Length - 1];
                    for (int i = 1; i < strTags.Length; i++)
                    {
                        _args[i - 1] = (object)strTags[i];
                    }
                }

                Assembly asm = LoadAssembly(strTags[0]);


                FrmBase fm;
                if (strTags.Length > 1 && strTags[1] == string.Empty)
                {
                    fm = (FrmBase)asm.CreateInstance(strTags[0]);
                }
                else
                {
                    fm = (FrmBase)asm.CreateInstance(strTags[0], true, BindingFlags.CreateInstance, null, _args,
                        Application.CurrentCulture, null);
                }

                //20190430 kimsw 수정
                if (fm != null)
                {
                    //FrmBase fm2 = new FrmBase();
                    //fm2.
                    fm.MdiParent = this;
                    fm.Tag = mItem.MenuID;
                    fm.Text = mItem.Caption;

                    //((FrmBase)fm).szAuth = mItem.Permision.Split(new char[] { ',' });
                    fm.Show();
                    fm.Focus();
                }
                else
                {
                    //MessageBox.Show("자식폼 생성중 오류.");
                    FrmBase fm2 = new FrmBase();
                    //fm2.
                    fm2.MdiParent = this;
                    fm2.Tag = mItem.MenuID;
                    fm2.Text = mItem.Caption;

                    //((FrmBase)fm).szAuth = mItem.Permision.Split(new char[] { ',' });
                    fm.Show();
                    fm.Focus();
                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                logger.Error("폼 생성 중 오류가 발생하였습니다" + ex.Message.ToString().Trim());
            }

            this.Cursor = Cursors.Default;
        }

        #endregion

        private void CreateMainMenu()
        {
            //2019-12-21 대한제강
            //String Query = " SELECT '003' AS MENU_ID, '계량관리' AS MENU_NM, '003' AS UPPER_MENU_ID, 'F' AS MENU_DIV, '' AS FORM_NM, '' AS RW_FLAG FROM DUAL"
            //             + " UNION"
            //             + " SELECT '031' AS MENU_ID, '차량관리' AS MENU_NM, '003' AS UPPER_MENU_ID, 'I' AS MENU_DIV, 'OCT_WEIGHT.Manager.Program.FrmVehl' AS FORM_NM, '' AS RW_FLAG FROM DUAL"
            //             + " UNION"
            //             + " SELECT '032' AS MENU_ID, '계량관리' AS MENU_NM, '003' AS UPPER_MENU_ID, 'I' AS MENU_DIV, 'OCT_WEIGHT.Manager.Program.FrmWeight' AS FORM_NM, '' AS RW_FLAG FROM DUAL"
            //             ;
            //DataSet ds = DBConn._ExecuteDataSet(Query);
            String Query = " SELECT * FROM TB_WS01_0014 A WHERE MOBILE_CTGRY='DESK' AND USE_YN = 'N' ORDER BY UPPER_MENU_ID ASC,SORT_NO ASC";

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("P_USER_CD", clsUserInfo.User_id);
            DataSet ds = DBConn.ExecuteDataSet2("SP_AUTH_R", dict); 
            try
            {
                OctoBarSubItem _sub = null;
                OctoBarButtonItem _menu = null;
                foreach (DataRow drv in ds.Tables[0].Rows)
                {
                    if (Convert.ToString(drv["UPPER_MENU_ID"]) == "000") // TREE 의  ROOT의 MENU  ID
                        continue;

                    if (Convert.ToString(drv["MENU_DIV"]) == "F")
                    {
                        _sub = new OctoBarSubItem();
                        _sub.Manager = bar2.Manager;
                        _sub.Caption = drv["MENU_NM"].ToString();
                        _sub.MenuID = drv["MENU_ID"].ToString();
                        bar2.ItemLinks.Add(_sub);
                    }
                    else if (Convert.ToString(drv["MENU_DIV"]) == "I")
                    {
                        BarItem _parent = Find_ParentMenu(bar2, drv["UPPER_MENU_ID"].ToString());

                        _menu = new OctoBarButtonItem();
                        _menu.Manager = bar2.Manager;
                        _menu.Caption = drv["MENU_NM"].ToString();
                        _menu.MenuID = drv["MENU_ID"].ToString();
                        _menu.Permision = drv["RW_FLAG"].ToString();
                        _menu.FormName = Convert.ToString(drv["FORM_NM"]) + ",";
                        _menu.ItemClick += new ItemClickEventHandler(MainMenu_Click);
                        if (_parent != null)
                        {
                            ((OctoBarSubItem)_parent).AddItem(_menu);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("메인 메뉴 트리를 생성하는 중 ERROR:" + ex.Message.ToString().Trim());
            }
        }

        private void MenuOnItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        private BarItem Find_ParentMenu(OctoBarSubItem container, string menu_id)
        {
            BarItem _ret = null;
            foreach (BarItemLink _item in container.ItemLinks)
            {
                if (_item.Item is OctoBarSubItem == false) continue;
                OctoBarSubItem _subItem = _item.Item as OctoBarSubItem;
                if (_subItem.MenuID == menu_id)
                {
                    return _subItem;
                }

                if (_subItem.Links.Count > 0)
                {
                    _ret = Find_ParentMenu(_subItem, menu_id);
                    if (_ret != null)
                        return _ret;
                }
            }

            return _ret;
        }

        private BarItem Find_ParentMenu(Bar container, string menu_id)
        {
            BarItem _ret = null;
            foreach (BarItemLink _item in container.ItemLinks)
            {
                if (_item.Item is OctoBarSubItem == false) continue;
                OctoBarSubItem _subItem = _item.Item as OctoBarSubItem;
                if (_subItem.MenuID == menu_id)
                {
                    return _subItem;
                }

                if (_subItem.Links.Count > 0)
                {
                    _ret = Find_ParentMenu(_subItem, menu_id);
                    if (_ret != null)
                        return _ret;
                }
            }

            return _ret;
        }

        private void btnOpenList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 열린창 보기
            try
            {
                foreach (Form frmForm in this.MdiChildren)
                {
                    frmForm.Show();
                }
            }
            catch (Exception ex)
            {
                logger.Error("열린화면 모두 닫기중 ERROR:" + ex.Message.ToString().Trim());
            }
        }

        private void btnAllClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 모듣창 닫기
            mFormClose();
        }

        private void btnChangePassword_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 비밀번호변경
            using (UserPopup popup = new UserPopup())
            {
                try
                {
                    DialogResult drResult = popup.ShowDialog(this);
                    if (drResult == DialogResult.OK)
                    {
                        MsgBoxUtil.AlertInformation("비밀번호가 변경되었습니다.");
                    }
                }
                catch (Exception ex)
                {

                }
            }

        }

        private void btnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 로그아웃
            if (MessageBox.Show("저장하지 않은 모든 화면이 종료됩니다. " + Environment.NewLine + "진행하시겠습니까?", "확인", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                //모든 화면 닫기
                mFormClose();
                //기존에 메뉴를 Clear해준다.
                bar2.ClearLinks();
                //로그인 팝업 화면 띄우기전에 하단 StatusBar 사용자 정보 삭제
                barStaticUserID.Caption = "";
                barStaticUserName.Caption = "";
                using (frmLogin _login = new frmLogin())
                {
                    try
                    {
                        DialogResult drResult = _login.ShowDialog(this);
                        if (drResult == DialogResult.OK)
                        {
                            barStaticUserID.Caption = string.Format("접속아이디 : {0}", clsUserInfo.User_id);
                            barStaticUserName.Caption = string.Format("사용자명 : {0}", clsUserInfo.Emp_nm);
                            userid = clsUserInfo.User_id;
                            UserName = clsUserInfo.Emp_nm;
                        }
                        else
                        {
                            MessageBox.Show("접속 오류");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("로그인 창 생성중 ERROR:" + ex.Message.ToString().Trim());
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

                try
                {
                    //메뉴생성
                    CreateMainMenu();
                }
                catch (Exception ex)
                {
                    logger.Error("메뉴 생성중 ERROR:" + ex.Message.ToString().Trim());
                }
            }
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 프로그램 종료
            DialogResult dlgresult = MsgBoxUtil.AlertQuestion("프로그램을 종료하시겠습니까?");
            if (dlgresult == DialogResult.OK)
            {
                Application.DoEvents();
                //this.Close();
                Application.Exit();
            }

            this.Focus();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DBConn.IsOpenDB())
            {
                DBConn.DBClose();
                MsgBoxUtil.AlertInformation("DB를 재접속합니다.");
                DBConn._DBConn();
            }
            else
            {
                MsgBoxUtil.AlertInformation("DB를 재접속합니다.");
                DBConn._DBConn();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            barStaticItem2.Caption = string.Format("현재 시간 : {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 종료시 db 연결 종료
            DBConn.DBClose();
            timer1.Stop();
        }
    }
}