using System;
using System.Web.Services;
using System.Data;
using System.Text;

namespace OCT_WEIGHT.DB
{
    /// <summary>
    /// DBService의 요약 설명입니다.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
    // [System.Web.Script.Services.ScriptService]
    public class DBService : System.Web.Services.WebService
    {
        // DB 서버
        private string ConStr_MES = string.Empty;
        private string MES_TNS = string.Empty;

        private System.Collections.Specialized.NameValueCollection _appSettings = System.Web.Configuration.WebConfigurationManager.AppSettings;

        public DBService()
        {
            MES_TNS = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=121.133.99.66)(PORT=1521)))(CONNECT_DATA=(SID=IODB)));User Id=AWDBO;Password=AWDBO";
                //"Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.17)(PORT = 2010))) (CONNECT_DATA = (SID = MEBP1)));User Id=m80apuser;Password=m80_wght";   // MES서버
            ConStr_MES = MES_TNS;
            //ConStr_MES = ConStr_MES.Replace("MES_TNS", MES_TNS);   // MES서버     
        }

        #region DB 쿼리조회
        [WebMethod]
        public string GetQuery(string SqlString)
        {
            string rtn = null;
            DBHelper_ORACLE find = null;
            try
            {
                find = new DBHelper_ORACLE(ConStr_MES);
                if (find.DBConn())
                {
                    rtn = CompressionData.CompressDataSet(find.ExecuteDataSet(SqlString));
                }
                else
                {
                    rtn = "";
                }
            }
            catch (Exception e1)
            {
                return e1.Message.ToString();
            }
            finally
            {
                find.DBClose();
            }
            return rtn;
        }
        [WebMethod]
        public DataSet GetQueryDataSet(string SqlString)
        {
            DataSet rtn = null;
            DBHelper_ORACLE find = null;
            try
            {
                find = new DBHelper_ORACLE(ConStr_MES);
                find.DBConn();
                rtn = find.ExecuteDataSet(SqlString);
            }
            catch (Exception e1)
            {
                throw e1;

            }
            finally
            {
                find.DBClose();
            }
            return rtn;
        }

        [WebMethod]
        public DataSet GetQueryDataSetSP(string sp_name, String parameters)
        {
            DataSet rtn = null;
            DBHelper_ORACLE find = null;
            try
            {
                find = new DBHelper_ORACLE(ConStr_MES);
                find.DBConn();

                rtn = find.ExecuteDataSet(sp_name, parameters);
            }
            catch (Exception e1)
            {
                throw e1;

            }
            finally
            {
                find.DBClose();
            }
            return rtn;
        }
        #endregion

        [WebMethod]
        public String GetQueryJSON(string SqlString)
        {
            DataSet rtn = null;
            DBHelper_ORACLE find = null;

            int j = 1;
            try
            {
                find = new DBHelper_ORACLE(ConStr_MES);
                find.DBConn();
                rtn = find.ExecuteDataSet(SqlString);

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                StringBuilder JsonString = new StringBuilder();

                JsonString.AppendLine("{");
                JsonString.AppendLine("\"column\" :");
                JsonString.AppendLine("[");
                foreach (DataColumn col in rtn.Tables[0].Columns)
                {
                    if (j == rtn.Tables[0].Columns.Count)
                    {
                        JsonString.AppendLine(col.ColumnName);
                    }
                    else
                    {
                        JsonString.AppendLine(col.ColumnName + ",");
                    }
                    j = j + 1;
                }
                JsonString.AppendLine("],");                
                
                JsonString.AppendLine("\"rows\" :");
                JsonString.AppendLine("[");
                
                foreach (DataRow dr in rtn.Tables[0].Rows)
                {
                    JsonString.AppendLine("[");
                    j = 1;
                    foreach (DataColumn col in rtn.Tables[0].Columns)
                    {
                        if (j == rtn.Tables[0].Columns.Count)
                        {
                            JsonString.AppendLine("\"" + dr[col] + "\"");
                        }
                        else
                        {
                            JsonString.AppendLine("\"" + dr[col] + "\",");
                        }
                        j = j + 1;
                    }
                    JsonString.AppendLine("]");
                }
                JsonString.AppendLine("]");
                JsonString.AppendLine("}");
                
                return JsonString.ToString();
            }
            catch (Exception e1)
            {
                throw e1;
            }
            finally
            {
                find.DBClose();
            }
        }

        #region DB 쿼리실행
        [WebMethod]
        public bool SetQuery(string SetQuery)//
        {
            bool rtn = false;
            DBHelper_ORACLE dataSQL = null;
            try
            {
                dataSQL = new DBHelper_ORACLE(ConStr_MES);
                dataSQL.DBConn(); 
                rtn = dataSQL.ExecuteQuery(SetQuery);
            }
            catch (Exception e2)
            {
                throw e2;
            }

            finally
            {
                dataSQL.DBClose();
            }
            return rtn;
        }

        [WebMethod]
        public bool SetQuerySP(string sp_name, String parameters)//
        {
            bool rtn = false;
            DBHelper_ORACLE dataSQL = null;
            try
            {
                dataSQL = new DBHelper_ORACLE(ConStr_MES);
                dataSQL.DBConn();
                rtn = dataSQL.ExecuteQuery(sp_name, parameters);
            }
            catch (Exception e2)
            {
                throw e2;
            }

            finally
            {
                dataSQL.DBClose();
            }
            return rtn;
        }

        [WebMethod]
        public String SetQuerySPR(string sp_name, String parameters)//
        {
            String rtn;
            DBHelper_ORACLE dataSQL = null;
            try
            {
                dataSQL = new DBHelper_ORACLE(ConStr_MES);
                dataSQL.DBConn();
                rtn = dataSQL.ExecuteQuerySPR(sp_name, parameters);
            }
            catch (Exception e2)
            {
                throw e2;
            }

            finally
            {
                dataSQL.DBClose();
            }
            return rtn;
        }        
        #endregion

    }
}
