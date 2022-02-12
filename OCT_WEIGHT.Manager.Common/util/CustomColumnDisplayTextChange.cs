using System;

namespace OCT_WEIGHT.Manager.Common.util
{
    public static class CustomColumnDisplayTextChange
    {
        /// <summary>
        /// 소수점찍기
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string txtsetting(string data)
        {
            string ret = string.Empty;
            if (CheckingSpecialText(data) == false) // 숫자를 뺀 나머지 제외
            {
                if (data == "0")
                {
                    return data;
                }
                int cdata = Convert.ToInt32(data);
                ret = string.Format("{0:#,##0}", cdata);
            }
            else
            {
                ret = data;
            }
            return ret;
        }

        /// <summary>
        /// 특수문자 제외
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool CheckingSpecialText(string txt)
        {
            string str = @"[~,.!@\#$%^&*\()\=+|\\/:;?""<>']KGKgkgkG";
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(str);
            return rex.IsMatch(txt);
        }
    }
}
