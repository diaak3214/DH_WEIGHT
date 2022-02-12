using System;
using System.Windows.Forms;
using System.Threading;
using log4net.Config;

namespace OCT_WEIGHT.AutoWeight
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {

            bool CreateNew;
            Mutex dup = new Mutex(true, "MAIN_WEIGHT", out CreateNew);
            if (CreateNew)
            {
                //로그 설정파일 읽기
                XmlConfigurator.Configure(new System.IO.FileInfo("log4net.xml"));

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
                dup.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("이미 프로그램이 실행중입니다");
            }
        }
    }
}
