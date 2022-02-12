using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.info;

namespace OCT_WEIGHT.Manager.Common.Popup
{
    public partial class MasterCode : Form
    {
        public string mastercode;

        public string mastercodenm;

        private string temp_crud = string.Empty;
          
        public MasterCode()
        {
            InitializeComponent();
        }

        #region 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
        }

        #endregion

        #region Form Load

        private void MasterCode_Load(object sender, EventArgs e)
        {
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            if (mastercode != string.Empty)
            {
                try
                {
                    btnSave.CRUD_type = CRUDType.C;
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("P_FG", "M");
                    dict.Add("v_TYPE_CD", mastercode);
                    dict.Add("v_TYPE_DESC", mastercodenm);

                    DataSet ds = DBConn.ExecuteDataSet2("SP_SYSCOMMON_R", dict);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdMaster.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        grdMaster.DataSource = null;
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
        }

        #endregion

        #region Grid Focus Change

        private void grvMaster_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtCode.Text = gvCom_M.GetFocusedRowCellValue("TYPE_CD").ToString();
        }

        #endregion

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gvCom_M.RowCount > 0)
            {
                temp_crud = btnSave.CRUD_type.ToString();
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();

                    if (txtCode.Text.Length > 0)
                    {
                        dict.Add("P_FG", "U");
                    }
                    else
                    {
                        dict.Add("P_FG", "I");
                    }

                    //dict.Add("P_FG", temp_crud);
                    dict.Add("P_TYPE_CD  ", txtCode.Text.Trim());
                    dict.Add("P_TYPE_NAME", txtName.Text.Trim());
                    dict.Add("P_USE_YN   ", "Y");
                    dict.Add("P_CRT_USER ", clsUserInfo.User_id);
                    dict.Add("P_MOD_USER ", clsUserInfo.User_id);

                    string rtn = DBConn.ExecuteQuerySPR3("SP_SYSCOMMON_POP_S", dict);
                    if (rtn != "-1")
                    {
                        MessageBox.Show(rtn);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        #endregion

        private void gvCom_M_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtCode.Text = gvCom_M.GetFocusedRowCellValue("TYPE_CD").ToString();
            txtName.Text = gvCom_M.GetFocusedRowCellValue("TYPE_DESC").ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("P_FG", "D");
                dict.Add("P_TYPE_CD  ", txtCode.Text.Trim());
                dict.Add("P_TYPE_NAME", txtName.Text.Trim());
                dict.Add("P_USE_YN   ", "N");
                dict.Add("P_CRT_USER ", clsUserInfo.User_id);
                dict.Add("P_MOD_USER ", clsUserInfo.User_id);

                string rtn = DBConn.ExecuteQuerySPR3("SP_SYSCOMMON_POP_S", dict);
                if (rtn != "-1")
                {
                    MessageBox.Show(rtn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}