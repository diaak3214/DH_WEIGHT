using System.ComponentModel;
using DevExpress.XtraBars;

namespace OCT_WEIGHT.OctoCommon
{
    public partial class OctoBarSubItem : BarSubItem
    {
        private string _MenuID;
        public OctoBarSubItem()
        {
            InitializeComponent();
        }

        public OctoBarSubItem(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        public string MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
    }
}
