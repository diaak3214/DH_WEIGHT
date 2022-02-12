using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using OCT_WEIGHT.Manager.Common.info;

namespace OCT_WEIGHT.Manager.Common.Control
{
    /// <summary>
    /// 공통 KeyValue 검색 공통 컨트롤
    /// </summary>
    [DefaultEvent("KeyValueChanged")]
    public partial class OctoHelper : UserControl
    {
        private HelpType _helpType = HelpType.CUST;
        private DataRow _ReturnRow = null;
        private bool _LoadShow = false;

        public OctoHelper()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(OctoHelperV2_Load);
        }

        void OctoHelperV2_Load(object sender, EventArgs e)
        {
            //if (!DesignMode)
            //    this.KeyWidth = 100;
        }

        #region 속성정의
        /// <summary>
        /// Help Type 를 가져오거나 설정합니다
        /// </summary>
        /// <value> Help Type </value>
        [Bindable(BindableSupport.No)]
        [Browsable(true)]
        [DefaultValue(HelpType.CUST)]
        public HelpType HelpTypes
        {
            get
            {
                return _helpType;
            }
            set
            {
                _helpType = value;

            }
        }

        /// <summary>
        /// Gets or sets the width of the key.
        /// </summary>
        /// <value>The width of the key.</value>
        [Bindable(BindableSupport.No)]
        [Browsable(true)]
        [DefaultValue(100)]
        public int KeyWidth
        {
            set
            {
                edtKey.Width = value;
            }
            get
            {
                return edtKey.Width;
            }
        }

        /// <summary>
        /// Gets or sets the width of the value.
        /// </summary>
        /// <value>The width of the value.</value>
        [Bindable(BindableSupport.No)]
        [Browsable(true)]
        [DefaultValue(200)]
        public int ValueWidth
        {
            set
            {
                edtValue.Width = value;
            }
            get
            {
                return edtValue.Width;
            }
        }

        /// <summary>
        /// Gets the width of the Return Row.
        /// </summary>
        /// <value>The width of the Return Row.</value>
        [Bindable(BindableSupport.No)]
        [Browsable(false)]
        public DataRow ReturnRow
        {
            get
            {
                return _ReturnRow; ;
            }
        }

        /// <summary>
        /// Gets the width of the Load 시 사용되는지.
        /// </summary>
        /// <value>The width of the  Load 시 사용되는지.</value>
        [Bindable(BindableSupport.No)]
        [Browsable(false)]
        public bool LoadShow
        {
            get
            {
                return _LoadShow; ;
            }
        }

        [Bindable(BindableSupport.No)]
        [Browsable(true)]
        [DefaultValue("")]
        public string Key
        {
            get
            {
                return edtKey.Text;
            }
            set
            {
                edtKey.Text = value;
            }
        }

        [Bindable(BindableSupport.No)]
        [Browsable(true)]
        [DefaultValue("")]
        public string Value
        {
            get
            {
                return edtValue.Text;
            }
            set
            {
                edtValue.Text = value;
            }
        }

        [Category("사용자정의"), DisplayName("SetColor")]
        [Bindable(BindableSupport.No)]
        [Browsable(true)]
        [DefaultValue("WindowColor")]

        public Color SetColor
        {
            get
            {
                return edtKey.BackColor;
            }
            set
            {
                edtKey.BackColor = value;
                edtValue.BackColor = value;
            }
        }
        #endregion

        #region 사용자 정의 Events
        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler KeyValueChanged;
        #endregion

        private void edtKey_TextChanged(object sender, EventArgs e)
        {
            if (edtKey.Text == "")
            {
                _ReturnRow = null;
                edtValue.Text = "";
                //이벤트 처리
                if (KeyValueChanged != null)
                    KeyValueChanged(this, new EventArgs());
            }
        }        

        /// <summary>
        /// Query하기.....(ShowDlg : Dlg 창 띄우기, EnterShow : 데이터 로드시만 true )
        /// </summary>
        private void Searchdata(bool ShowDlg, bool EnterShow)
        {
            //DataRow FindRow = null;
            //_LoadShow = EnterShow;  // 로드(그리드 RowColChange)시 사용인지 검색할 때 사용인지
                       

            //p.AddParam("@TYPE", _helpType.ToString());
            //_qMsg.AddSpService("SP_DIALOGUE_GET", p, DbCommandType.ExecuteQuery); //ALTER PROCEDURE SP_GET_INLIST   
            //_rMsg = _svc.Execute(_qMsg);

            //try
            //{
            //    if (_rMsg.Successed)
            //    {
            //        DataTable dtHelp = _rMsg.Services[0].Result.DataSet.Tables[0];
            //        if (_helpType == HelpType.WORK)
            //        {
            //            dtHelp.PrimaryKey = new DataColumn[] { dtHelp.Columns["CODE"], dtHelp.Columns["NAME"], dtHelp.Columns["LINE_NO"] };
            //            dtHelp.AcceptChanges();

            //            if (edtKey.Text != "")
            //            {
            //                string[] Find = new string[3];
            //                Find[0] = edtKey.Text;
            //                Find[1] = edtValue.Text;
            //                Find[2] = "";
            //                FindRow = dtHelp.Rows.Find(Find);
            //            }
            //        }
            //        else if (_helpType == HelpType.LINE)
            //        {
            //            dtHelp.PrimaryKey = new DataColumn[] { dtHelp.Columns["CODE"], dtHelp.Columns["NAME"], dtHelp.Columns["LINE_NO"], dtHelp.Columns["ITEM_CD"] };
            //            dtHelp.AcceptChanges();

            //            if (edtKey.Text != "")
            //            {
            //                string[] Find = new string[4];
            //                Find[0] = edtKey.Text;
            //                Find[1] = edtValue.Text;
            //                Find[2] = "";
            //                Find[3] = "";
            //                FindRow = dtHelp.Rows.Find(Find);
            //            }
            //        }
            //        else if (_helpType == HelpType.ORDER || _helpType == HelpType.RESERVE_OUT || _helpType == HelpType.RESERVE_IN || _helpType == HelpType.MOVE_OUT || _helpType == HelpType.MOVE_IN
            //            || _helpType == HelpType.SHIP || _helpType == HelpType.IMPORT
            //        )
            //        {
            //            dtHelp.PrimaryKey = new DataColumn[] { dtHelp.Columns["CODE"], dtHelp.Columns["NAME"] };
            //            dtHelp.AcceptChanges();
            //            string[] Find = new string[2];
            //            Find[0] = edtKey.Text;
            //            Find[1] = "1";
            //            FindRow = dtHelp.Rows.Find(Find);
            //        }

            //        else if (_helpType == HelpType.ITEM)
            //        {
            //            dtHelp.PrimaryKey = new DataColumn[] { dtHelp.Columns["CODE"] };
            //            dtHelp.AcceptChanges();
            //            if (edtKey.Text.Length == 8)
            //            {
            //                FindRow = dtHelp.Rows.Find("0000000000" + edtKey.Text);
            //            }
            //            else
            //            {
            //                FindRow = dtHelp.Rows.Find(edtKey.Text);
            //            }
            //        }
            //        else
            //        {
            //            dtHelp.PrimaryKey = new DataColumn[] { dtHelp.Columns["CODE"] };
            //            dtHelp.AcceptChanges();

            //            //if (edtKey.Text != "")
            //            //{
            //            //    string[] Find = new string[2];
            //            //    Find[0] = edtKey.Text;
            //            //    Find[1] = edtValue.Text;
            //            //    FindRow = dtHelp.Rows.Find(Find);
            //            //}
            //            FindRow = dtHelp.Rows.Find(edtKey.Text);
            //        }
            //        if ((ShowDlg == false) && (FindRow != null))
            //            _ReturnRow = FindRow;
            //        else if (EnterShow == false)
            //        {
            //            FrmHelpDlg frm = new FrmHelpDlg(dtHelp, this.HelpTypes, edtKey.Text, edtValue.Text);
            //            frm.ShowDialog();
            //            if (frm.DialogResult == DialogResult.OK)
            //                _ReturnRow = frm.PublicRow;
            //        }

            //        if (_ReturnRow != null)
            //        {
            //            edtKey.Text = _ReturnRow["CODE"].ToString();
            //            edtValue.Text = _ReturnRow["NAME"].ToString();
            //            if (_helpType == HelpType.WORK || _helpType == HelpType.LINE)
            //            {
            //                this.Tag = _ReturnRow["LINE_NO"].ToString();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        throw new OctoCommon.Except.OctoException(_rMsg.ErrorMessage);
            //    }
            //}
            //finally
            //{
                
            //}
        }

        private void edtKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Searchdata(false, false);
                //이벤트 처리
                if (KeyValueChanged != null)
                    KeyValueChanged(this, new EventArgs());
            }
        }

        private void edtKey_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Searchdata(true, false);
            //이벤트 처리
            if (KeyValueChanged != null)
                KeyValueChanged(this, new EventArgs());
        }

        private void OctoHelperV2_SizeChanged(object sender, EventArgs e)
        {
            edtValue.Width = this.Width - edtKey.Width - 1;
        }

        private void edtValue_SizeChanged(object sender, EventArgs e)
        {
            this.Width = edtKey.Width + edtValue.Width + 1;
        }

        public void KeyEnterShow()
        {
            Searchdata(false, true);
            //이벤트 처리
            if (KeyValueChanged != null)
                KeyValueChanged(this, new EventArgs());
        }

        public void Clear()
        {
            this.Key = string.Empty;
            this.Value = string.Empty;
        }
    }
}
