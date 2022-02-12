using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using System.Drawing;
using OCT_WEIGHT.Manager.Common.util;
using System.Globalization;
using OCT_WEIGHT.Manager.Common.Popup;
using log4net;

namespace OCT_WEIGHT.Manager
{
    public partial class frmLogin : Form
    {
        protected static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ServiceAdapter _svc = null;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void PasswordtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char) System.Windows.Forms.Keys.Enter)
            {
                btnLogin_Click_1(btnLogin, new EventArgs());
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;

            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;

            label1.BringToFront();
            label2.BringToFront();
            UserIDtextBox.Focus();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            //if (_svc == null) _svc = new ServiceAdapter();
            //2019-12-21 대한제강
            //String Query = " SELECT 'Admin' AS USER_CD, '관리자' AS USER_NM, '01' AS AUTH_CD, '1' AS USER_PW FROM DUAL";
            //DataSet ds = DBConn._ExecuteDataSet(Query);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("v_USER_CD", UserIDtextBox.Text);
            dict.Add("v_USER_PW", PasswordtextBox.Text);
            DataSet ds = DBConn.ExecuteDataSet2("SP_LOGIN", dict);

            DateTime OldDate = new DateTime();
            if (ds.Tables[0].Rows.Count > 0)
            {
                clsUserInfo.User_id = Convert.ToString(ds.Tables[0].Rows[0]["USER_CD"]);
                clsUserInfo.Emp_nm = Convert.ToString(ds.Tables[0].Rows[0]["USER_NM"]);
                clsUserInfo.Place = Convert.ToString(ds.Tables[0].Rows[0]["PLACE_CD"]); //사업장_계량대
                clsUserInfo.Auth = Convert.ToString(ds.Tables[0].Rows[0]["AUTH_CD"]);

                //6개월뒤 비번 교체 제외(2019-12-21 한민호) 
                //// 6개월뒤 비번 교체 요청
                //// 우선 수정일을 확인후 수정일이 없을시 생성일을 기준으로 잡는다.
                //if (ds.Tables[0].Rows[0]["MOD_DTM"].ToString().Length > 0)                
                //{
                //    // 계정 수정일을 DB에서 yyyyMMdd 형식으로 가져온다
                //    OldDate = DateTime.ParseExact(ds.Tables[0].Rows[0]["MOD_DTM"].ToString().Substring(0, 10).Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture);
                //}
                //else if (ds.Tables[0].Rows[0]["CRT_DTM"].ToString().Length > 0)
                //{
                //    // 계정 생성일을 DB에서 yyyyMMdd 형식으로 가져온다
                //    OldDate = DateTime.ParseExact(ds.Tables[0].Rows[0]["CRT_DTM"].ToString().Substring(0, 10).Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture);
                //}

                ////if문 비교를 위해 int 값으로 변환
                //int old = Convert.ToInt32(OldDate.AddMonths(6).ToString("yyyyMMdd"));
                //int now = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));

                //// 계정 생성일 OR 계정 수정일을 6개월뒤 년월일과 현재 년월일과 비교
                //if ( old <= now)
                //{                    
                //    MsgBoxUtil.AlertInformation(" 비밀번호가 6개월이 지났습니다. " + Environment.NewLine + " 비밀번호를 변경해주세요.");

                //    // 비밀번호 변경 팝업 활성화
                //    using (UserPopup popup = new UserPopup())
                //    {
                //        try
                //        {
                //            DialogResult drResult = popup.ShowDialog(this);
                //            if (drResult == DialogResult.OK)
                //            {
                //                MsgBoxUtil.AlertInformation("비밀번호가 변경되었습니다.");
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            logger.Info("비밀 번호 변경 오류 : " + ex.Message);
                //        }
                //    }
                //}

                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(" 로그인에 실패하였습니다. ");
                UserIDtextBox.Text = string.Empty;
                PasswordtextBox.Text = string.Empty;
                UserIDtextBox.Focus();
                return;
                //DialogResult = DialogResult.Cancel;
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
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
    }
}