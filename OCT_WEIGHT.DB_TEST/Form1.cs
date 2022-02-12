using System;
using System.Data;
using System.Windows.Forms;

namespace OCT_WEIGHT.DB_TEST
{
    public partial class Form1 : Form
    {
        DBHelper_ORACLE mdb;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 접속
            String CS_MES = "Data Source=MES_TNS;User Id=m80apuser;Password=m80apuser";
            String MES_TNS = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.151)(PORT = 2005))) (CONNECT_DATA = (SERVICE_NAME = DPMESA1)))";
            
            mdb = new DBHelper_ORACLE(CS_MES.Replace("MES_TNS", MES_TNS));

            mdb.DBConn(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 데이터 가져오기

            DataSet ds = mdb.ExecuteDataSet("select * from tb_wg01_0008");

            dataGridView1.DataSource = ds.Tables[0]; 
  

        }
    }
}
