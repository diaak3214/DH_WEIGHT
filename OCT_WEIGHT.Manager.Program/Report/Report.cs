using System.Data;
using DevExpress.XtraGrid;

namespace OCT_WEIGHT.Manager.Program.Report
{
    public partial class Report : DevExpress.XtraReports.UI.XtraReport
    {
        private GridControl control;
        public string start_date = string.Empty;
        public string end_date = string.Empty;
        public string company = string.Empty;

        public GridControl GridControl
        {
            get { return control; }
            set
            {
                control = value;
                printableComponentContainer1.PrintableComponent = control;
            }
        }

        public void load()
        {
            xrCompany.Text = xrCompany.Text + company + "]";
            xrDate.Text = xrDate.Text + start_date + " ~ " + end_date + "]";
        }

        public Report(DataTable dt)
        {
            InitializeComponent();
        }
    }
}