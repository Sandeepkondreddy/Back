using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace WAP3
{
    class classLogUpload
    {
        
        #region write it in UserMaster Sync Log file
        public static void UserMasterSyncLog(string readerid,string filename, string data)
        {
            string sqlConnection = classServerDetails.WifiSQLConnection;
            string InsertQry = "INSERT INTO [dbo].[HHDLog] ([Readerid],[FileName],[LogDetails],[Synctime]) Values (" + readerid + ",'" + filename + "','" + data + "',GETDATE())";
            string CONN_STRING = sqlConnection;
            SqlConnection Con = new SqlConnection(CONN_STRING);
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand(InsertQry, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @:classLogUpload::" + ex.ToString());
            }
        }
        #endregion
    }
}
