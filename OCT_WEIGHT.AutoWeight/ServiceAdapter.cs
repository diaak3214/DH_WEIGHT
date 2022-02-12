using System;
using System.Collections.Generic;
using System.Data;

namespace OCT_WEIGHT.AutoWeight
{
    public struct RetType
    {
        String v_Result;
        String V_ERRMSG;

        public RetType(string result, string errmsg)
        {
            this.v_Result = result;
            this.V_ERRMSG = errmsg;
        }
    }

    public class ServiceAdapter
    {
        private static string _url = string.Empty;

        /// <summary>
        /// Mobis Web Service Server URL
        /// - Client Main Application or Test에서 Url 을 지정해주어야 합니다.
        /// </summary>
        public static string Url
        {
            get { return ServiceAdapter._url; }
            set { ServiceAdapter._url = value; }
        }

        public DataSet GetQuery(String Query)
        {
            frmMain.mdb_main.DBConn();

            DataSet ds = frmMain.mdb_main.ExecuteDataSet(Query); 
            frmMain.mdb_main.DBClose();

            return ds;
        }

        public DataSet GetQuerySP(String sp_name, Dictionary<string, string> parameters)
        {
            String tmp = "";

            foreach (KeyValuePair<string, string> pair in parameters)
            {
                tmp = tmp + pair.Key + "=" + pair.Value + ";";
            }

            frmMain.mdb_main.DBConn();
            DataSet ds = frmMain.mdb_main.ExecuteDataSet(sp_name, tmp);
            frmMain.mdb_main.DBClose();

            return ds;
        }

        public bool SetQuery(String Query)
        {
            frmMain.mdb_main.DBConn();
            Boolean ret = frmMain.mdb_main.ExecuteQuery(Query);
            frmMain.mdb_main.DBClose();

            return ret; 
        }

        public string SetQuerySP(String sp_name, Dictionary<string, string> parameters)
        {
            //2019-10-04 김상우 차장 수정
            //String tmp = "";

            //foreach (KeyValuePair<string, string> pair in parameters)
            //{
            //    tmp = tmp + pair.Key + "=" + pair.Value + ";";
            //}
            //frmMain.mdb_main.DBConn();
            //Boolean ret = frmMain.mdb_main.ExecuteQuery(sp_name, tmp);
            //frmMain.mdb_main.DBClose();

            //return ret;
            String tmp = "";

            foreach (KeyValuePair<string, string> pair in parameters)
            {
                tmp = tmp + pair.Key + "=" + pair.Value + ";";
            }
            frmMain.mdb_main.DBConn();
            string ret = frmMain.mdb_main.ExecuteQuerySPR(sp_name, parameters);
            frmMain.mdb_main.DBClose();

            return ret;
        }

        public RetType SetQuerySPR(String sp_name, Dictionary<string, string> parameters)
        {
            RetType ret;

            String tmp1 = "";         

            String tmp = "";

            foreach (KeyValuePair<string, string> pair in parameters)
            {
                tmp = tmp + pair.Key + "=" + pair.Value + ";";
            }
            frmMain.mdb_main.DBConn();
            //2019-10-04 김상우 수정
            tmp1 = frmMain.mdb_main.ExecuteQuerySPR(sp_name, parameters);
            //tmp1 = frmMain.mdb_main.ExecuteQuerySPR(sp_name, tmp);

            ret = new RetType(tmp1.Split('/')[0], tmp1.Split('/')[1]);
            frmMain.mdb_main.DBClose();

            return ret;
        }        
    }
}
