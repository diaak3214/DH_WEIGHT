using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.info;
using DevExpress.XtraEditors;

namespace OCT_WEIGHT.Manager.Common.Popup
{
    public partial class SeinPopup : Form
    {

        private string temp_crud = string.Empty;

        public SeinPopup()
        {
            InitializeComponent();
        }

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Form Load

        private void SeinPopup_Load(object sender, EventArgs e)
        {
        }

        #endregion

        private void txtSEIN_MIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            TypingOnlyNumber(sender, e, true, true);

        }

        private void txtSEIN_MAX_KeyPress(object sender, KeyPressEventArgs e)
        {
            TypingOnlyNumber(sender, e, true, true);
        }

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {

            string sSEIN_MAX = txtSEIN_MAX.Text;
            string sSEIN_MIN = txtSEIN_MIN.Text;

            if (sSEIN_MAX.IndexOf('.') != -1)
            {
                string[] a = sSEIN_MAX.Split('.');
                txtSEIN_MAX.Text = a[0] + "." + a[1].Substring(0, 1);
            }

            if (sSEIN_MIN.IndexOf('.') != -1)
            {
                string[] a = sSEIN_MIN.Split('.');
                txtSEIN_MIN.Text = a[0] + "." + a[1].Substring(0, 1);
            }


            if (txtSEIN_MIN.Text == "")
            {
                MessageBox.Show("최소 출하오차범위를 입력하세요");
                txtSEIN_MIN.Focus();
                return;
            }

            if (txtSEIN_MAX.Text == "")
            {
                MessageBox.Show("최대 출하오차범위를 입력하세요");
                txtSEIN_MIN.Focus();
                return;
            }


            if (RangeValid(txtSEIN_MIN) == false)
            {
                MessageBox.Show("최소오차 입력값이 입력 범위를 초과하였습니다(입력 가능값 : -100 ~ 100)");
                txtSEIN_MIN.Focus();
                return;
            }

            if (RangeValid(txtSEIN_MAX) == false)
            {
                MessageBox.Show("최대오차 입력값이 입력 범위를 초과하였습니다(입력 가능값 : -100 ~ 100)");
                txtSEIN_MAX.Focus();
                return;
            }

            if (txtCRT_USER.Text == "")
            {
                MessageBox.Show("등록자를 입력하세요");
                txtCRT_USER.Focus();
                return;
            }

            if (txtREASON.Text == "")
            {
                MessageBox.Show("사유를 입력하세요");
                txtREASON.Focus();
                return;
            }

            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", "C"); // C:등록
                p.Add("P_PLNT_NO", clsUserInfo.Place);
                p.Add("P_SEIN_MIN", txtSEIN_MIN.Text);
                p.Add("P_SEIN_MAX", txtSEIN_MAX.Text);
                p.Add("P_REASON", txtREASON.Text);
                p.Add("P_REG_ID", txtCRT_USER.Text);

                DBConn.ExecuteQuerySPR2("SP_MU_WEIGHT_SEIN_NEW_S", p);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.Close();
            }
        }

        #endregion

        #region 기능키
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & ~(Keys.Shift | Keys.Control);

            switch (key)
            {
                case Keys.F6:
                    btnSave_Click(btnSave, new EventArgs());    //저장
                    return true;
                case Keys.Escape:
                    btnClose_Click(btnClose, new EventArgs());  //닫기
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static bool RangeValid(object obj)
        {
            double number = 0;
            bool canConvert = double.TryParse((obj as TextEdit).Text, out number);
            bool rtn = true; 

            try
            {
                if (canConvert == false || double.Parse((obj as TextEdit).Text) > 100 || double.Parse((obj as TextEdit).Text) < -100)
                    {
                        rtn = false;
                    }
                
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());

            }

            return rtn;

        }

        public static void TypingOnlyNumber(object sender, KeyPressEventArgs e, bool includePoint, bool includeMinus)
        {
            bool isValidInput = false;

            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    if (includePoint == true) { if (e.KeyChar == '.') isValidInput = true; }
                    if (includeMinus == true) { if (e.KeyChar == '-') isValidInput = true; }

                    if (isValidInput == false) e.Handled = true;
                }

                if (includePoint == true)
                {
                    if (e.KeyChar == '.' && (string.IsNullOrEmpty((sender as TextEdit).Text.Trim()) || (sender as TextEdit).Text.IndexOf('.') > -1)) e.Handled = true;

                }
                if (includeMinus == true)
                {
                    //if (e.KeyChar == '-' && (!string.IsNullOrEmpty((sender as TextEdit).Text.Trim()) || (sender as TextEdit).Text.IndexOf('-') > -1)) e.Handled = true;
                    if (e.KeyChar == '-' && (sender as TextEdit).Text.IndexOf('-') > -1) e.Handled = true;
                }
            }
            catch (Exception error)
            {

            }
        }  
    }
}