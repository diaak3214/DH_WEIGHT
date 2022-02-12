using System;
using System.Collections.Generic;
using System.Data;
using Oracle.DataAccess.Client;

namespace DK_WEIGHT.AutoWeight
{
    public class DBHelper_ORACLE
    {
        private OracleConnection mDBConn = null;
        private OracleCommand mCmd = null;
        private OracleTransaction mTrans = null;  // 트랜젝션

        private String ErrMsg = null;
        private string _connstr = string.Empty;

        #region DB연결
        public DBHelper_ORACLE(string __connstr)
        {
            this._connstr = __connstr;
        }

        public bool DBConn()
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
        public bool IsOpenDB()
        {
            return ((mDBConn.State != ConnectionState.Closed) ? true : false);
        }
        #endregion

        #region DB연결끈기
        public void DBClose()
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
        public bool ExecuteQuery(string Query)
        {
            int rtn = -1;

            mCmd = new OracleCommand(Query, mDBConn);
            rtn = mCmd.ExecuteNonQuery();
            
            return rtn < 0 ? false : true;
        }
        #endregion

        #region 쿼리실행 데이터셋리턴
        public DataSet ExecuteDataSet(string Query)
        {
            DataSet mDataSet = new DataSet();

            mCmd = new OracleCommand(Query, mDBConn);
            mDataSet.Load(mCmd.ExecuteReader(), LoadOption.OverwriteChanges, "");           

            return mDataSet;
        }

        public DataSet ExecuteDataSet(string sp_name, String parameters)
        {
            DataSet mDataSet = new DataSet();

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
			
            // 조회 리턴받을 REF커서
            mCmd.Parameters.Add("v_OUT_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            mDataSet.Load(mCmd.ExecuteReader(), LoadOption.OverwriteChanges, "");

            return mDataSet;
        }

        public String ExecuteQuerySPR(string sp_name, String parameters)
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
        #endregion

        #region 쿼리 실행
        public bool ExecuteQuery(string sp_name, String parameters)
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

        public void TransBegin()
        {
            if(mDBConn.State == ConnectionState.Open)   
                mTrans = mDBConn.BeginTransaction();  
        }

        public void TransCommit()
        {
            if(mTrans != null) 
                mTrans.Commit();  
        }

        public void TransRollBack()
        {
            if (mTrans != null)
                mTrans.Rollback();  
        }

        public String GetError()
        {
            return ErrMsg;
        }
    }
}
