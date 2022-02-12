using System;
using System.Collections.Generic;
using System.Data;

namespace OCT_WEIGHT.RESULT_INFO
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

            //WebReference.DBServiceSoapClient main = new WebReference.DBServiceSoapClient();

            //if (_url == string.Empty)
            //{
            //    throw new Exception("Service URL이 지정되어 있지 않습니다.");
            //}

            //main.Endpoint.Address = new System.ServiceModel.EndpointAddress(_url);

            //return main.GetQueryDataSet(Query);
            return frmMain.mdb_main.ExecuteDataSet(Query); 
        }

        public DataSet GetQuerySP(String sp_name, Dictionary<string, string> parameters)
        {

            //WebReference.DBServiceSoapClient main = new WebReference.DBServiceSoapClient();

            //if (_url == string.Empty)
            //{
            //    throw new Exception("Service URL이 지정되어 있지 않습니다.");
            //}

            //String tmp = "";

            //foreach (KeyValuePair<string, string> pair in parameters)
            //{
            //    tmp = tmp + pair.Key + "=" + pair.Value + ";";
            //}

            //main.Endpoint.Address = new System.ServiceModel.EndpointAddress(_url);

            //return main.GetQueryDataSetSP(sp_name, tmp);

            String tmp = "";

            foreach (KeyValuePair<string, string> pair in parameters)
            {
                tmp = tmp + pair.Key + "=" + pair.Value + ";";
            }

            return frmMain.mdb_main.ExecuteDataSet(sp_name, tmp);

        }

        public bool SetQuery(String Query)
        {

            //WebReference.DBServiceSoapClient main = new WebReference.DBServiceSoapClient();

            //if (_url == string.Empty)
            //{
            //    throw new Exception("Service URL이 지정되어 있지 않습니다.");
            //}

            //main.Endpoint.Address = new System.ServiceModel.EndpointAddress(_url);

            //return main.SetQuery(Query);
            return frmMain.mdb_main.ExecuteQuery(Query);
        }

        public bool SetQuerySP(String sp_name, Dictionary<string, string> parameters)
        {

            //WebReference.DBServiceSoapClient main = new WebReference.DBServiceSoapClient();

            //if (_url == string.Empty)
            //{
            //    throw new Exception("Service URL이 지정되어 있지 않습니다.");
            //}

            //String tmp = "";

            //foreach (KeyValuePair<string, string> pair in parameters)
            //{
            //    tmp = tmp + pair.Key + "=" + pair.Value + ";";
            //}

            //main.Endpoint.Address = new System.ServiceModel.EndpointAddress(_url);

            //return main.SetQuerySP(sp_name, tmp);

            String tmp = "";

            foreach (KeyValuePair<string, string> pair in parameters)
            {
                tmp = tmp + pair.Key + "=" + pair.Value + ";";
            }
            return frmMain.mdb_main.ExecuteQuery(sp_name, tmp);
        }

        public RetType SetQuerySPR(String sp_name, Dictionary<string, string> parameters)
        {
            //RetType ret;

            //String tmp1 = "";
            //WebReference.DBServiceSoapClient main = new WebReference.DBServiceSoapClient();

            //if (_url == string.Empty)
            //{
            //    throw new Exception("Service URL이 지정되어 있지 않습니다.");
            //}

            //String tmp = "";

            //foreach (KeyValuePair<string, string> pair in parameters)
            //{
            //    tmp = tmp + pair.Key + "=" + pair.Value + ";";
            //}

            //main.Endpoint.Address = new System.ServiceModel.EndpointAddress(_url);

            //tmp1 = main.SetQuerySPR(sp_name, tmp);

            //ret = new RetType(tmp1.Split('/')[0], tmp1.Split('/')[1]);

            //return ret;

            RetType ret;

            String tmp1 = "";
            //WebReference.DBServiceSoapClient main = new WebReference.DBServiceSoapClient();

            //if (_url == string.Empty)
            //{
            //    throw new Exception("Service URL이 지정되어 있지 않습니다.");
            //}

            String tmp = "";

            foreach (KeyValuePair<string, string> pair in parameters)
            {
                tmp = tmp + pair.Key + "=" + pair.Value + ";";
            }

            //main.Endpoint.Address = new System.ServiceModel.EndpointAddress(_url);

            tmp1 = frmMain.mdb_main.ExecuteQuerySPR(sp_name, tmp);

            ret = new RetType(tmp1.Split('/')[0], tmp1.Split('/')[1]);

            return ret;
        }        
    }
}
