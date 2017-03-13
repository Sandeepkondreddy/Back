/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication-MasterDataSync
Created Date:		14 Dec 2015 
Updated Date:		14 Dec 2015  
******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
namespace RFID_WebService
{
    public class classMasterData
    {
        public static DataTable getMasterDetails(string getMasterDtls)
        {
            DataTable dt = new DataTable("Master");
            try
            {
                string Query = ""; ;
                if (getMasterDtls == "User Master")
                {
                    //sql = " Select UId,Loginid,Password,LoginType,UserName from [dbo].[HHLoginMst] where [UStatus]='Active'";
                    Query = "Select UserId,Loginid,UserPassword,LoginType,UserName from RFID.RM_HHDUSERMASTER where UStatus='Active'";
                }
                classLog.writeLog("Message @: Master Details : " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                classServerConnections.OracledbCon.Close();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Master Details : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }
            return dt;
        }
    }
}
