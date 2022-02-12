using System;
using System.Data;
using System.Data.SqlClient;

namespace DK_WEIGHT.AutoWeight
{
    public class DBHelper_SQL
    {
        private SqlConnection mDBConn = null;
        private SqlCommand mCmd = null;
        private SqlTransaction mTrans = null;  // 트랜젝션

        private String ErrMsg = null;
        private string _connstr = string.Empty;

        #region DB연결
        public DBHelper_SQL(string __connstr)
        {
            this._connstr = __connstr;
        }

        public bool DBConn()
        {
            try
            {
                mDBConn = new SqlConnection(_connstr);
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
            
            mCmd = new SqlCommand(Query, mDBConn);
            rtn = mCmd.ExecuteNonQuery();
            
            return rtn < 0 ? false : true;
        }
        #endregion

        #region 쿼리실행 데이터셋리턴
        public DataSet ExecuteDataSet(string Query)
        {
            DataSet mDataSet = new DataSet();
            
            mCmd = new SqlCommand(Query, mDBConn);
            mDataSet.Load(mCmd.ExecuteReader(), LoadOption.OverwriteChanges, "");           

            return mDataSet;
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
    }
}
