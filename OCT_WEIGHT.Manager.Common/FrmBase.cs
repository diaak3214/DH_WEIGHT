using System.Data;

namespace OCT_WEIGHT.Manager.Common
{
    public partial class FrmBase : DevExpress.XtraEditors.XtraForm
    {
        #region 내부변수
        // APP 서비스처리 Class      
        protected string[] _szAuth;
        protected string c_flag;
        protected string r_flag;
        protected string u_flag;
        protected string d_flag;
        protected string p_flag;
        protected string x_flag;
        protected DataTable LanguageDictionary = null;

        //public ServiceAdapter _svc = null; 
        #endregion

        public FrmBase()
        {
            InitializeComponent();

            ////if (_svc == null) _svc = new ServiceAdapter();
        }      

        public override string ToString()
        {
            return "모듈명  :   " + base.ToString().Replace("Text", "폼명 ");
        }

        public string[] szAuth
        {
            set
            {
                _szAuth = value;
            }
            get
            {
                return _szAuth;
            }
        }

        protected DataTable getTable(string Query)
        {
            DataTable dt = new DataTable();
             
            return dt;
        }

    }
}
