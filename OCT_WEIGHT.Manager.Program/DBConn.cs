using System;
using Oracle.DataAccess.Client;
using System.Data;
using System.Collections.Generic;

namespace DK_WEIGHT.Manager
{
    public static class DBConn
    {
        private static string _connstr =
            "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=121.133.99.66)(PORT=1521)))(CONNECT_DATA=(SID=IODB)));User Id=AWDBO;Password=AWDBO";

        private static OracleConnection conn;
        private static OracleCommand cmd;

        private static String ErrMsg = null;
        private static OracleConnection mDBConn = null;
        private static OracleCommand mCmd = null;
        private static OracleTransaction mTrans = null; // 트랜젝션

        #region DB연결

        public static bool _DBConn()
        {
            try
            {
                mDBConn = new OracleConnection(_connstr);

                mDBConn.Open();
            }

            catch (Exception e)
            {
                ErrMsg = e.Message;
                throw e;
            }

            return true;
        }

        #endregion

        #region DB연결체크

        public static bool IsOpenDB()
        {
            return ((mDBConn.State != ConnectionState.Closed) ? true : false);
        }

        #endregion

        #region DB연결끈기

        public static void DBClose()
        {
            if (mDBConn == null)
            {
                return;
            }

            try
            {
                if (mDBConn.State.ToString() == "Open")
                {
                    mDBConn.Close();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                throw e;
            }
        }

        #endregion

        #region 쿼리실행

        public static bool ExecuteQuery(string Query)
        {
            int rtn = -1;

            mCmd = new OracleCommand(Query, mDBConn);
            rtn = mCmd.ExecuteNonQuery();

            return rtn < 0 ? false : true;
        }

        #endregion

        #region 쿼리실행 데이터셋리턴

        public static DataSet ExecuteDataSet(string Query)
        {
            DataSet mDataSet = new DataSet();

            mCmd = new OracleCommand(Query, mDBConn);
            mDataSet.Load(mCmd.ExecuteReader(), LoadOption.OverwriteChanges, "");

            return mDataSet;
        }

        public static DataSet ExecuteDataSet(string sp_name, String parameters)
        {
            DataSet mDataSet = new DataSet();

            mCmd = new OracleCommand();
            mCmd.Connection = mDBConn;
            mCmd.CommandType = CommandType.StoredProcedure;
            mCmd.CommandText = sp_name;

            mCmd.Parameters.Clear();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] items = parameters.TrimEnd(';').Split(';');
            /*
            foreach (string item in items)
            {
                string[] keyValue = items;//item.Split('=');
                dic.Add(keyValue[0], keyValue[1]);
            }
            */

            for (int i = 0; i < items.Length; i++)
            {
                dic.Add(Convert.ToString(i), items[i]);
            }
            

            foreach (KeyValuePair<string, string> pair in dic)
            {
                mCmd.Parameters.Add(pair.Key, OracleDbType.Varchar2, pair.Value, ParameterDirection.Input);
                //mCmd.Parameters.Add(pair.Key, OracleDbType.Varchar2, pair.Value, )
            }


            // 조회 리턴받을 REF커서
            mCmd.Parameters.Add("v_OUT_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            mDataSet.Load(mCmd.ExecuteReader(), LoadOption.OverwriteChanges, "");

            return mDataSet;
        }

        #endregion

        #region 쿼리 실행

        public static bool ExecuteQuery(string sp_name, String parameters)
        {
            int rtn = -1;

            mCmd = new OracleCommand();
            mCmd.Connection = mDBConn;
            mCmd.CommandType = CommandType.StoredProcedure;
            mCmd.CommandText = sp_name;

            mCmd.Parameters.Clear();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] items = parameters.TrimEnd(';').Split(';');
            foreach (string item in items)
            {
                string[] keyValue = item.Split('=');
                dic.Add(keyValue[0], keyValue[1]);
            }

            foreach (KeyValuePair<string, string> pair in dic)
            {
                mCmd.Parameters.Add(pair.Key, OracleDbType.Varchar2, pair.Value, ParameterDirection.Input);
            }

            rtn = mCmd.ExecuteNonQuery();

            return rtn < 0 ? false : true;
        }

        #endregion

        public static String ExecuteQuerySPR(string sp_name, String parameters)
        {
            int rtn = -1;

            mCmd = new OracleCommand();
            mCmd.Connection = mDBConn;
            mCmd.CommandType = CommandType.StoredProcedure;
            mCmd.CommandText = sp_name;

            mCmd.Parameters.Clear();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] items = parameters.TrimEnd(';').Split(';');
            foreach (string item in items)
            {
                string[] keyValue = item.Split('=');
                dic.Add(keyValue[0], keyValue[1]);
            }

            foreach (KeyValuePair<string, string> pair in dic)
            {
                mCmd.Parameters.Add(pair.Key, OracleDbType.Varchar2, pair.Value, ParameterDirection.Input);
            }

            mCmd.Parameters.Add("v_RESULT", OracleDbType.Int32).Direction = ParameterDirection.Output;
            mCmd.Parameters.Add("v_ERRMSG", OracleDbType.Varchar2).Direction = ParameterDirection.Output;

            rtn = mCmd.ExecuteNonQuery();

            return mCmd.Parameters["v_ERSULT"].Value.ToString() + "/" + mCmd.Parameters["v_ERRMSG"].Value.ToString();
        }

        public static void TransBegin()
        {
            if (mDBConn.State == ConnectionState.Open)
                mTrans = mDBConn.BeginTransaction();
        }

        public static void TransCommit()
        {
            if (mTrans != null)
                mTrans.Commit();
        }

        public static void TransRollBack()
        {
            if (mTrans != null)
                mTrans.Rollback();
        }
    }
}