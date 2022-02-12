using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.info;

namespace OCT_WEIGHT.Manager.Common.Control
{
    public partial class FrmHelpDlg : Form
    {
        public DataRow PublicRow = null;
        public string publicKey = null;
        public string publicValue = null;
        public HelpType Selhelp =  HelpType.CUST;
        public FrmHelpDlg()
        {
            InitializeComponent();
        }

        public FrmHelpDlg(DataTable dt, OCT_WEIGHT.Manager.Common.info.HelpType selType, string strKey, string strValue)
        {
            InitializeComponent();
            
            this.Width = 390;
            Selhelp = selType;
            DisplayGrid(dt, selType, "CODE");
            edtSearch.Focus();
        }

        #region 그리드 그리기
        private void DisplayGrid(DataTable dt, OCT_WEIGHT.Manager.Common.info.HelpType selType, string KeyField)
        {
            gvSearch.OptionsView.ColumnAutoWidth = false;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DevExpress.XtraGrid.Columns.GridColumn col = gvSearch.Columns.Add();
                col.Name = dt.Columns[i].ColumnName;
                col.FieldName = dt.Columns[i].ColumnName;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                col.OptionsColumn.AllowEdit = false;
                col.Visible = true;
                if (i > 2)
                {
                    col.Visible = false;
                }
            }

            if (selType == HelpType.USER)
            {
                this.Text = "사원검색";
                gvSearch.Columns[0].Caption = "사원코드";
                gvSearch.Columns[1].Caption = "사원명";
                gvSearch.Columns[0].Width = 90;
                gvSearch.Columns[1].Width = 80;
                //gvSearch.Columns[0].Visible = false;
                //gvSearch.Columns[1].Visible = false;
            }
            else if (selType == HelpType.CUST)
            {
                this.Text = "고객검색";
                gvSearch.Columns[0].Caption = "고객코드";
                gvSearch.Columns[1].Caption = "고객명";
                gvSearch.Columns[0].Width = 90;
                gvSearch.Columns[1].Width = 200;
            }
            else if (selType == HelpType.ITEM)
            {
                this.Text = "품목검색";
                gvSearch.Columns[0].Caption = "";
                gvSearch.Columns[1].Caption = "품목코드";
                gvSearch.Columns[2].Caption = "품목명";
                gvSearch.Columns[0].Width = 0;
                gvSearch.Columns[1].Width = 100;
                gvSearch.Columns[2].Width = 200;
                gvSearch.Columns[0].Visible = false;
            }
            else if (selType == HelpType.ENTER)
            {
                this.Text = "입고예정번호검색";
                gvSearch.Columns[0].Caption = "예정번호";
                gvSearch.Columns[1].Caption = "품목명";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 200;
            }
            else if (selType == HelpType.OUT)
            {
                this.Text = "출고예정번호검색";
                gvSearch.Columns[0].Caption = "예정번호";
                gvSearch.Columns[1].Caption = "품목명";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 200;
            }
            else if (selType == HelpType.WAREHOUSE)
            {
                this.Text = "창고간예정번호검색";
                gvSearch.Columns[0].Caption = "예정번호";
                gvSearch.Columns[1].Caption = "품목명";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 200;
            }
            else if (selType == HelpType.SAPLOC)
            {
                this.Text = "창고검색";
                gvSearch.Columns[0].Caption = "창고코드";
                gvSearch.Columns[1].Caption = "창고명";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 200;
            }
            else if (selType == HelpType.COSTCENTER)
            {
                this.Text = "코스트센터";
                gvSearch.Columns[0].Caption = "센터코드";
                gvSearch.Columns[1].Caption = "센터명";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 200;
            }
            else if (selType == HelpType.MOVEREASON)
            {
                this.Text = "이동사유";
                gvSearch.Columns[0].Caption = "이동코드";
                gvSearch.Columns[1].Caption = "사유명";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 200;
            }
            else if (selType == HelpType.MOVETYPE || selType == HelpType.MOVETYPE_ETC || selType == HelpType.MOVETYPE_ETC_A || selType == HelpType.MOVETYPE_ETC_B || selType == HelpType.MOVETYPE_ETC_C)
            {
                this.Text = "이동유형";
                gvSearch.Columns[0].Caption = "이동유형";
                gvSearch.Columns[1].Caption = "재고지시자";
                gvSearch.Columns[2].Caption = "이동지시자";
                gvSearch.Columns[3].Caption = "입고시시자";
                gvSearch.Columns[4].Caption = "이동지시자";
                gvSearch.Columns[5].Caption = "소비전기";
                gvSearch.Columns[6].Caption = "트랜잭션코드";
                gvSearch.Columns[0].Width = 80;
                gvSearch.Columns[1].Width = 80;
                gvSearch.Columns[2].Width = 150;
                gvSearch.Columns[3].Width = 100;
                gvSearch.Columns[3].Visible = true;
                gvSearch.Columns[4].Width = 100;
                gvSearch.Columns[4].Visible = true;
                gvSearch.Columns[5].Width = 100;
                gvSearch.Columns[5].Visible = true;
                gvSearch.Columns[6].Width = 100;
                gvSearch.Columns[6].Visible = true;
            }
            else if (selType == HelpType.SPSTOCK)
            {
                this.Text = "특별재고 지시자";
                gvSearch.Columns[0].Caption = "지시자코드";
                gvSearch.Columns[1].Caption = "지시자명";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 200;
            }
            else if (selType == HelpType.WMSSTORAGE)
            {
                this.Text = "WMS 창고";
                gvSearch.Columns[0].Caption = "창고코드";
                gvSearch.Columns[1].Caption = "창고명";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 200;
            }

            else if (selType == HelpType.ORDER)
            {
                this.Text = "구매오더";
                gvSearch.Columns[0].Caption = "예정번호";
                gvSearch.Columns[1].Caption = "항번";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 200;
            }

            else if (selType == HelpType.WORK)
            {
                this.Text = "작업지시";
                gvSearch.Columns[0].Caption = "작업지시일";
                gvSearch.Columns[1].Caption = "생산순번";
                gvSearch.Columns[2].Caption = "라인번호";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 80;
                gvSearch.Columns[2].Width = 80;
            }

            else if (selType == HelpType.RESERVE_IN)
            {
                this.Text = "예약정보";
                gvSearch.Columns[0].Caption = "예약번호";
                gvSearch.Columns[1].Caption = "항번";            
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 80;

            }

            else if (selType == HelpType.MOVE_IN)
            {
                this.Text = "공장간재고";
                gvSearch.Columns[0].Caption = "자재전표번호";
                gvSearch.Columns[1].Caption = "항번";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 80;

            }

            else if (selType == HelpType.IMPORT)
            {
                this.Text = "외자입고";
                gvSearch.Columns[0].Caption = "등록번호";
                gvSearch.Columns[1].Caption = "항번";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 80;

            }

            else if (selType == HelpType.LINE)
            {
                this.Text = "라인피딩";
                gvSearch.Columns[0].Caption = "작업지시일";
                gvSearch.Columns[1].Caption = "생산순번";
                gvSearch.Columns[2].Caption = "라인번호";
                gvSearch.Columns[3].Caption = "품목코드";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 80;
                gvSearch.Columns[2].Width = 80;
                gvSearch.Columns[3].Width = 80;
            }

            else if (selType == HelpType.RESERVE_OUT)
            {
                this.Text = "예약정보";
                gvSearch.Columns[0].Caption = "예약번호";
                gvSearch.Columns[1].Caption = "항번";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 80;

            }

            else if (selType == HelpType.MOVE_OUT)
            {
                this.Text = "공장간재고";
                gvSearch.Columns[0].Caption = "자재전표번호";
                gvSearch.Columns[1].Caption = "항번";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 80;
            }

            else if (selType == HelpType.SHIP)
            {
                this.Text = "출하정보";
                gvSearch.Columns[0].Caption = "납품번호";
                gvSearch.Columns[1].Caption = "항번";
                gvSearch.Columns[0].Width = 120;
                gvSearch.Columns[1].Width = 80;
            }

            grdSearch.DataSource = dt;
        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            DataView dv = (DataView)gvSearch.DataSource;

            string expr = string.Format(" LIKE '*{0}*'", edtSearch.Text);
            if (expr.Length == 0) return;
            sb.AppendFormat("[{0}]{1}", "CODE", expr);
            sb.Append(" OR ");
            sb.AppendFormat("[{0}]{1}", "NAME", expr);
            if ((gvSearch.Columns.Count > 2) && (gvSearch.Columns[2].Name == "CUST_NM"))
            {
                sb.Append(" OR ");
                sb.AppendFormat("[{0}]{1}", gvSearch.Columns[2].Name, expr);
            }

            string strFilter = sb.ToString();
            if (strFilter == dv.RowFilter) return;

            try
            {
                dv.RowFilter = strFilter;
            }
            catch
            {
                //
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!gvSearch.GetFocusedRowCellValue("CODE").Equals(null))
            {
                publicKey = gvSearch.GetFocusedRowCellValue("CODE").ToString();
                publicValue = gvSearch.GetFocusedRowCellValue("NAME").ToString();
                if (Selhelp == HelpType.ENTER || Selhelp == HelpType.MOVEREASON)
                {
                    object[] Find = new object[2];
                    Find[0] = gvSearch.GetFocusedRowCellValue("CODE");
                    Find[1] = gvSearch.GetFocusedRowCellValue("CODE2");
                    PublicRow = ((DataView)gvSearch.DataSource).Table.Rows.Find(Find);
                }
                else if (Selhelp == HelpType.OUT)
                {
                    object[] Find = new object[4];
                    Find[0] = gvSearch.GetFocusedRowCellValue("ORDER_NO");
                    Find[1] = gvSearch.GetFocusedRowCellValue("CODE");
                    Find[2] = gvSearch.GetFocusedRowCellValue("CODE2");
                    Find[3] = gvSearch.GetFocusedRowCellValue("CODE3");
                    PublicRow = ((DataView)gvSearch.DataSource).Table.Rows.Find(Find);
                }
                else if ( Selhelp == HelpType.MOVEREASON)
                {
                    object[] Find = new object[2];
                    Find[0] = gvSearch.GetFocusedRowCellValue("CODE");
                    Find[1] = gvSearch.GetFocusedRowCellValue("CODE2");
                    PublicRow = ((DataView)gvSearch.DataSource).Table.Rows.Find(Find);
                }
                else if (Selhelp == HelpType.WAREHOUSE)
                {
                    object[] Find = new object[3];                   
                    Find[0] = gvSearch.GetFocusedRowCellValue("CODE");
                    Find[1] = gvSearch.GetFocusedRowCellValue("CODE2");
                    Find[2] = gvSearch.GetFocusedRowCellValue("CODE3");
                    PublicRow = ((DataView)gvSearch.DataSource).Table.Rows.Find(Find);
                }
                else if (Selhelp == HelpType.COSTCENTER)
                {
                    object[] Find = new object[4];
                    Find[0] = gvSearch.GetFocusedRowCellValue("CODE");
                    Find[1] = gvSearch.GetFocusedRowCellValue("CODE2");
                    Find[2] = gvSearch.GetFocusedRowCellValue("CODE3");
                    Find[3] = gvSearch.GetFocusedRowCellValue("NAME");
                    PublicRow = ((DataView)gvSearch.DataSource).Table.Rows.Find(Find);
                }
                else if (Selhelp == HelpType.MOVETYPE || Selhelp == HelpType.MOVETYPE_ETC || Selhelp == HelpType.MOVETYPE_ETC_A || Selhelp == HelpType.MOVETYPE_ETC_B || Selhelp == HelpType.MOVETYPE_ETC_C)
                {
                    object[] Find = new object[5];
                    Find[0] = gvSearch.GetFocusedRowCellValue("CODE");
                    Find[1] = gvSearch.GetFocusedRowCellValue("CODE2");
                    Find[2] = gvSearch.GetFocusedRowCellValue("CODE3");
                    Find[3] = gvSearch.GetFocusedRowCellValue("CODE4");
                    Find[4] = gvSearch.GetFocusedRowCellValue("CODE5");
                    PublicRow = ((DataView)gvSearch.DataSource).Table.Rows.Find(Find);
                }
                else if (Selhelp == HelpType.WORK)
                {
                   
                        string[] Find = new string[3];
                        Find[0] = gvSearch.GetFocusedRowCellValue("CODE").ToString();
                        Find[1] = gvSearch.GetFocusedRowCellValue("NAME").ToString();
                        Find[2] = gvSearch.GetFocusedRowCellValue("LINE_NO").ToString();
                        PublicRow = ((DataView)gvSearch.DataSource).Table.Rows.Find(Find);
                   
                }
                else if (Selhelp == HelpType.LINE)
                {
                   
                        string[] Find = new string[4];
                        Find[0] = gvSearch.GetFocusedRowCellValue("CODE").ToString();
                        Find[1] = gvSearch.GetFocusedRowCellValue("NAME").ToString();
                        Find[2] = gvSearch.GetFocusedRowCellValue("LINE_NO").ToString();
                        Find[3] = gvSearch.GetFocusedRowCellValue("ITEM_CD").ToString();
                        PublicRow = ((DataView)gvSearch.DataSource).Table.Rows.Find(Find);
                    
                }
                else if (Selhelp == HelpType.ORDER || Selhelp == HelpType.RESERVE_OUT || Selhelp == HelpType.RESERVE_IN || Selhelp == HelpType.MOVE_OUT || Selhelp == HelpType.MOVE_IN
                        || Selhelp == HelpType.SHIP || Selhelp == HelpType.IMPORT
                    )
                {

                    string[] Find = new string[2];
                    Find[0] = gvSearch.GetFocusedRowCellValue("CODE").ToString();
                    Find[1] = gvSearch.GetFocusedRowCellValue("NAME").ToString();
                    
                    PublicRow = ((DataView)gvSearch.DataSource).Table.Rows.Find(Find);

                }
                else
                {
           
                    PublicRow = ((DataView)gvSearch.DataSource).Table.Rows.Find(gvSearch.GetFocusedRowCellValue("CODE").ToString());
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("선택된 건이 없습니다.");
        }

        private void gvSearch_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void edtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) btnSearch.PerformClick();

            if (gvSearch.DataRowCount == 1)
            {
                btnOK.Focus(); 
            }
        }
    }
}