using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deamon_VehlNo
{
    class Program
    {
        public static void Main(string[] args)
        {
            //파라미터 값이 없을경우 종료
            if (args.Length > 0)
            {
                DBConn._DBConn();

                try
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Add("V_VEHL_NO", args[0].Trim());
                    p.Add("V_IMG_VEHL_NO", args[0].Trim());
                    //D:\PMC\Img\0101\20190920\0101_20190920093340457_경남82사1652.jpg
                    p.Add("V_IMG_FILE_NM",
                        @"D:\PMC\Img\" + args[2] + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + args[1] +
                        DateTime.Now.ToString("yyyymmddhhnnsszzz") + "_" + args[0] + ".jpg");

                    string rtn = DBConn.ExecuteQuerySPR3("SP_TB_WS02_0002_U", p);
                    if (rtn != "-1")
                    {
                        Console.WriteLine(rtn);
                    }
                    else
                    {
                        Console.WriteLine("데이터 입력이 완료되었습니다.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
             Console.WriteLine("종료");   
            }
        }
    }
}
