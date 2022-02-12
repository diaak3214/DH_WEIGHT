using System;
using System.Data;
using log4net.Repository.Hierarchy;
using MySql.Data.MySqlClient;

namespace OCT_DeaMon
{
    public static class MysqlDBConn
    {
        private static string _connstr = "Server=10.1.3.16;Database=pms;Uid=DYWork;Pwd=dyik6488;";
        //옥토시스 외부망
        //"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=121.133.99.66)(PORT=1521)))(CONNECT_DATA=(SID=IODB)));User Id=AWDBO;Password=AWDBO";
        //한국철강 내부망
        //"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.1.1.46)(PORT=1521)))(CONNECT_DATA=(SID=IODB)));User Id=AWDBO;Password=AWDBO";
        //    "server=10.1.3.16;user=DYWork;database=pms;password=dyik6488";
        //"Server=10.1.3.16;Database=pms;Uid=DYWork;password=(dyik6488);";
        //MySqlConnection connection = new MySqlConnection("Server=localhost;Database=test_schema;Uid=root;password=(비밀번호);");

        static MySqlConnection connection = new MySqlConnection("Server=10.1.3.16;Database=pms;Uid=DYWork;Pwd=dyik6488;");
        //서버     //DB이름      // 유저이름  //비밀번호

        private static MySqlConnection conn;    
        private static string ErrMsg = null;
        public static MySqlConnection mDBConn = null;
        private static MySqlCommand mCmd = null;
        private static MySqlTransaction mTrans = null; // 트랜젝션

        #region DB연결

        public static DataSet Dreturn(string sql)
        {
            DataSet mDataSet = new DataSet();
            /*
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            */
            //MySqlCommand command = new MySqlCommand(sql, mDBConn);
            mCmd = new MySqlCommand(sql, mDBConn);
            mDataSet.Load(mCmd.ExecuteReader(), LoadOption.OverwriteChanges, "");

            return mDataSet;
            /*
            try
            {
                mDataSet.Load(command.ExecuteReader(), LoadOption.OverwriteChanges, "");

                return mDataSet;
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                throw e;
            }
            */
        }

        public static DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlCommand selectCommand = new MySqlCommand();
                selectCommand.Connection = mDBConn; // myConnection;
                selectCommand.CommandText = sql;//"SELECT * FROM test";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, mDBConn);
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                throw e;
            }
            return ds;
        }

        public static DataSet _ExecuteDataSet(string Query)
        {
            DataSet mDataSet = new DataSet();

            mCmd = new MySqlCommand(Query, mDBConn);
            mDataSet.Load(mCmd.ExecuteReader(), LoadOption.OverwriteChanges, "");

            return mDataSet;
        }

        public static bool _DBConn()
        {
            try
            {
                mDBConn = new MySqlConnection(_connstr);

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

            mCmd = new MySqlCommand(Query, mDBConn);
            rtn = mCmd.ExecuteNonQuery();

            return rtn < 0 ? false : true;
        }

        #endregion

        #region 쿼리실행 데이터셋리턴

        /*
        public static DataSet _ExecuteDataSet(string Query)
        {
            DataSet mDataSet = new DataSet();
            mCmd = new MySqlCommand(Query, mDBConn);
            mDataSet.Load(mCmd.ExecuteReader(), LoadOption.OverwriteChanges, "");

            return mDataSet;
        }
        */

        public static DataSet _ExDataSet(string sp_name)
        {
            DataSet mDataSet = new DataSet();

            mCmd = new MySqlCommand();
            mCmd.Connection = mDBConn;
            mCmd.CommandType = CommandType.StoredProcedure;
            mCmd.CommandText = sp_name;

            mCmd.Parameters.Add("v_OUT_DATA", MySqlDbType.Text).Direction = ParameterDirection.Output;
            mDataSet.Load(mCmd.ExecuteReader(), LoadOption.OverwriteChanges, "");

            return mDataSet;
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

        #endregion
    }
}