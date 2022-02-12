using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DK_WEIGHT.Manager.Common.info;

namespace DK_WEIGHT.Manager.Program.Popup
{
    public partial class PopPassword : Form
    {
        Boolean isMove = false;
        string OldPW = string.Empty;
        string NewPW = string.Empty;

        public PopPassword()
        {
            InitializeComponent();
        }

        private void octoButton3_Click(object sender, EventArgs e)
        {
            try
            {
                //비밀번호 입력란 입력 여부 확인
                if (txtOldPw.Text == "")
                {
                    MessageBox.Show("기존 비밀번호는 필수입니다.");
                    return;
                }

                if (txtNewPw.Text == "")
                {
                    MessageBox.Show("신규 비밀번호는 필수입니다.");
                    return;
                }

                //기존에 비밀번호 불러오기
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("v_USER_PW", txtOldPw.Text);

                string param = String.Format("{0};{1}", clsUserInfo.User_id, txtOldPw.Text);

                DataSet ds = DBConn.ExecuteDataSet("SP_LOGIN", param);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    OldPW = Convert.ToString(ds.Tables[0].Rows[0]["USER_PW"]); //기존 비밀번호

                    if (OldPW == txtNewPw.Text)
                    {
                        MessageBox.Show("신규 비밀번호는 기존의 비밀번호와 동일하게 설정할수 없습니다.");
                        return;
                    }
                }

                //신규 비밀번호 저장
                Dictionary<string, string> dict2 = new Dictionary<string, string>();
                dict2.Add("V_USER_PW", txtNewPw.Text);
                dict2.Add("V_USER_CD", clsUserInfo.User_id);
                string rtn = DBConn.ExecuteQuerySPR3("sp_ChangePassword", dict2);

                if (rtn != "-1")
                {
                    MessageBox.Show("비밀번호 변경시 오류가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
