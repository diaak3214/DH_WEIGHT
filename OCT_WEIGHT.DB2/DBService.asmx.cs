using System;
using System.Web.Services;
using System.Data;
using System.Text;

namespace OCT_WEIGHT.DB2
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
        private System.Collections.Specialized.NameValueCollection _appSettings = System.Web.Configuration.WebConfigurationManager.AppSettings;
        private String CS_MES = "Data Source=MES_TNS;User Id=m80apuser;Password=m80apuser";
        private String MES_TNS = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.90.151)(PORT = 2005))) (CONNECT_DATA = (SERVICE_NAME = DPMESA1)))";

        public DBService()
        {
              
        }

        #region DB 쿼리조회
        [WebMethod]
        public string GetQuery(string SqlString)
        {
            string rtn = null;
            DBHelper_ORACLE find = null;
            
            try
            {
                find = new DBHelper_ORACLE(CS_MES.Replace("MES_TNS", MES_TNS));
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
                rtn = find.GetError(); 
                //rtn = e1.Message.ToString();  
            }
            finally
            {
                //find.DBClose();
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
                find = new DBHelper_ORACLE(CS_MES.Replace("MES_TNS", MES_TNS));
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
                find = new DBHelper_ORACLE(CS_MES.Replace("MES_TNS", MES_TNS));
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
                find = new DBHelper_ORACLE(CS_MES.Replace("MES_TNS", MES_TNS));
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
                dataSQL = new DBHelper_ORACLE(CS_MES.Replace("MES_TNS", MES_TNS));
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
                dataSQL = new DBHelper_ORACLE(CS_MES.Replace("MES_TNS", MES_TNS));
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
                dataSQL = new DBHelper_ORACLE(CS_MES.Replace("MES_TNS", MES_TNS));
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
