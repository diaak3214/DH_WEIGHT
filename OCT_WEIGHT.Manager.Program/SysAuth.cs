using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common;
using OCT_WEIGHT.Manager.Common.info;
using DevExpress.XtraTreeList.Nodes;

namespace OCT_WEIGHT.Manager.Program
{
    public partial class SysAuth : OCT_WEIGHT.Manager.Common.FrmBase
    {
        string temp_crud = string.Empty;

        public SysAuth()
        {
            InitializeComponent();
        }

        #region 닫기

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 조회

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search_R("ALL_MENU", "");
            btnSave.CRUD_type = CRUDType.U;
            txtNAME.Enabled = false;
            if (gvCom_M.GetFocusedRow() != null)
            {
                DataRow dr = gvCom_M.GetDataRow(gvCom_M.FocusedRowHandle);
                txtNAME.Text = dr["CODE"].ToString();
                textEdit1.Text = dr["CODE_NAME"].ToString();

                //트리 그리드 
                GetUserMenuList();

                //권한 메뉴 리스트 
                Search_R("AUTH", gvCom_M.GetFocusedRowCellValue("CODE").ToString());
            }
        }

        private void Search_R(string state, string auth_cd)
        {
            //if (_svc == null) _svc = new ServiceAdapter();

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                if (state == "ALL_MENU")
                {
                    dict.Add("P_TYPE_CD", "WS_002");

                    //20190503 임시수정 Kimsw
                    DataSet ds = DBConn.ExecuteDataSet2("SP_COMBOX_R", dict);
                    //_svc.GetQuerySP("SP_COMBOX_R", dict);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grCom_M.DataSource = ds.Tables[0];
                    }
                }
                else
                {
                    //dict.Add("P_FG", state);
                    dict.Add("P_AUTH_CD", auth_cd);

                    //20190503 임시수정 Kimsw
                    DataSet ds = DBConn.ExecuteDataSet2("SP_SYSAUTH_R", dict);
                    //_svc.GetQuerySP("SP_SYSAUTH_R", dict);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gridControlEx1.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        gridControlEx1.DataSource = null;
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

        #region 권한 포커스 쳉지

        private void gvCom_M_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnSave.CRUD_type = CRUDType.U;
            txtNAME.Enabled = false;

            if (gvCom_M.GetFocusedRow() != null)
            {
                DataRow dr = gvCom_M.GetDataRow(gvCom_M.FocusedRowHandle);
                txtNAME.Text = dr["CODE"].ToString();
                textEdit1.Text = dr["CODE_NAME"].ToString();

                //트리 그리드 
                GetUserMenuList();

                //권한 메뉴 리스트 
                Search_R("AUTH", gvCom_M.GetFocusedRowCellValue("CODE").ToString());
            }
        }

        #endregion

        #region 트리 그리드 바인딩

        private void GetUserMenuList()
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                //dict.Add("P_FG", "ALL_MENU");
                //dict.Add("P_AUTH_CD", "");

                //20190503 임시수정 Kimsw
                DataSet ds = DBConn._ExDataSet("SP_SYSAUTHTREE_R");
                //_svc.GetQuerySP("SP_SYSAUTH_R", dict);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataView dv = dt.DefaultView;

                    treeList2.KeyFieldName = "MENU_ID";
                    treeList2.ParentFieldName = "UPPER_MENU_ID";
                    treeList2.DataSource = dt;

                    for (int i = 0; i < treeList2.Columns.Count; i++)
                    {
                        treeList2.Columns[i].Visible = false;
                    }

                    treeList2.Columns["MENU_NM"].Visible = true;
                    treeList2.Columns["MENU_NM"].OptionsColumn.AllowEdit = false;
                    treeList2.BestFitColumns();
                    TreeIconSettings(treeList1, dv);
                    treeList2.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TreeIconSettings(DevExpress.XtraTreeList.TreeList treeList1, DataView dv)
        {
            foreach (TreeListNode node in treeList2.Nodes)
            {
                if (node.Nodes.Count > 0)
                {
                    node.ImageIndex = 2;
                    TreeIconSettings(node, dv);
                }
                else
                {
                    node.ImageIndex = 4;
                }
            }
        }

        private void TreeIconSettings(DevExpress.XtraTreeList.Nodes.TreeListNode pNode, DataView dv)
        {
            foreach (TreeListNode node in pNode.Nodes)
            {
                if (node.Nodes.Count > 0)
                {
                    node.ImageIndex = 2;
                    TreeIconSettings(node, dv);
                }
                else
                    node.ImageIndex = 4;
            }
        }

        #endregion

        #region 권한 그룹 신규

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnSave.CRUD_type = CRUDType.C;
            txtNAME.Enabled = true;
            txtNAME.Text = "";
            textEdit1.Text = "";
        }

        #endregion

        #region 저장

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((btnSave.CRUD_type != CRUDType.C) && (gvCom_M.DataRowCount.Equals(0))) return;

            temp_crud = btnSave.CRUD_type.ToString();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                #region 체크

                if (txtNAME.Text == "")
                {
                    MessageBox.Show("권한그룹을 입력하세요");
                    txtNAME.Focus();
                    return;
                }

                if (textEdit1.Text == "")
                {
                    MessageBox.Show("권한그룹명을 입력하세요");
                    textEdit1.Focus();
                    return;
                }

                #endregion

                Dictionary<string, string> p = new Dictionary<string, string>();

                p.Add("P_FG", temp_crud);
                p.Add("P_TYPE_CD", "002");
                p.Add("P_CODE", txtNAME.Text);
                p.Add("P_CODE_NAME", textEdit1.Text);
                p.Add("P_CODE_VALUE1", "");
                p.Add("P_CODE_VALUE2", "");
                p.Add("P_CODE_VALUE3", "");
                p.Add("P_REMARK", "");
                p.Add("P_USE_YN", "Y");
                p.Add("P_CRT_USER", clsUserInfo.User_id);

                //20190503 임시수정 Kimsw
                DBConn.ExecuteQuerySPR2("SP_SYSCOMMON_S", p);
                //_svc.SetQuerySP("SP_FRMCOMMON_TB_WG01_0002_S", p);

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

        #endregion

        #region ▶ ◀

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //▶
            TreeListNode node = treeList2.Selection[0];
            Save_state("C", gvCom_M.GetFocusedRowCellValue("CODE").ToString(), node.GetValue("MENU_ID").ToString());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //◀
            Save_state("D", gridViewEx1.GetFocusedRowCellValue("AUTH_CD").ToString(),
                gridViewEx1.GetFocusedRowCellValue("MENU_ID").ToString());
        }

        private void Save_state(string fg, string codes, string menuid)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("P_FG", fg.ToUpper().Trim());
                p.Add("P_AUTH_CD", codes);
                p.Add("P_MENU_ID", menuid);
                p.Add("P_RW_FLAG", menuid);
                //p.Add("P_RW_FLAG", "");
                p.Add("P_C_FLAG", "Y");
                p.Add("P_R_FLAG", "Y");
                p.Add("P_U_FLAG", "Y");
                p.Add("P_D_FLAG", "Y");
                p.Add("P_CRT_USER", clsUserInfo.User_id);

                //20190503 임시수정 Kimsw
                string rtn = DBConn.ExecuteQuerySPR3("SP_SYSAUTH_S", p);
                if (rtn != "-1")
                {
                    MessageBox.Show(rtn);
                }
                //_svc.SetQuerySP("SP_SYSAUTH_TB_WG01_0013_S", p);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //권한 메뉴 리스트 
                Search_R("AUTH", gvCom_M.GetFocusedRowCellValue("CODE").ToString());
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        private void SysAuth_Load(object sender, EventArgs e)
        {

        }
    }
}