using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Oracle.DataAccess.Client;

namespace CS_RFID3_Host_Sample1
{
	class classSaveTransactionDetails
	{
        public static string SaveTransaction(string Tagno,string Antina,string Location,string ReaderIP,string ImgPath)
        {
            string saveStaues="";
            SqlConnection Sqlcon = new SqlConnection(classServerConnections.SqlConn_String);
            Sqlcon.Open();
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            try
            {
                string strNonQuery = "Insert into  [dbo].[RFIDPhotoLog] ([tagno],[Ant],[Location],[readdt],[ReaderIP],[ImgPath]) Values('" + Tagno + "','" + Antina + "','" + Location + "',GETDATE(),'" + ReaderIP + "','" + ImgPath + "')";
                SqlCommand cmd = new SqlCommand(strNonQuery, Sqlcon);
                cmd.ExecuteNonQuery();
                saveStaues = "Transaction Saved Successfully.";
            }
            catch (Exception ex)
            {
                saveStaues = "Transaction Save Failed.";
            }
            Sqlcon.Close();

            return saveStaues;
        }

        public static string SavePhotoLog(string Tagno, string Antina, string Location, string ReaderIP, string ImgPath)
        {
            string OpStatus = "";
            try
            {
                string Query = "Insert into PROD.ACC_RFIDPhotoLog(TAGNO,AntNo,RLocation,ReaderIP,ImgPath,CREATEDBY)" + "Values('" + Tagno + "','" + Antina + "','" + Location + "','" + ReaderIP + "','" + ImgPath + "','SandeepApp')";
                classLog.writeLog("Message @: PhotoLog-Save: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();
                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: Gate Register-Save : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }
            return OpStatus;
        }

        public static string SaveRouteAccessLog(string RouteAccId, string RouteDtlId, string MapId, string ImgPath)
        {
            string OpStatus = "";
            try
            {
                string Query = "Insert into PROD.ACC_Route_Access_Log(RouteAccId,RouteDtlId,MapId,ImgPath,CREATEDBY)" + "Values(" + RouteAccId + "," + RouteDtlId + "," + MapId + ",'" + ImgPath + "','SandeepApp')";
                classLog.writeLog("Message @: Route Access Log-Save: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();
                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: Gate Register-Save : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }
            return OpStatus;
        }
	}
}
