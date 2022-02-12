using System.ComponentModel;
using DevExpress.XtraBars;

namespace OCT_WEIGHT.OctoCommon
{
    public partial class OctoBarButtonItem : BarButtonItem
    {
        private string _FormName;
        private string _Permision;
        private string _MenuID;
        public OctoBarButtonItem()
        {
            InitializeComponent();
        }

        public OctoBarButtonItem(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        public string Permision
        {
            get { return _Permision; }
            set { _Permision = value; }
        }
        public string FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }
        public string MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
 
    }
}
