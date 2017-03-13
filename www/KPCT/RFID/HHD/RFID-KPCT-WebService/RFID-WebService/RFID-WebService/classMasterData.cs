/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Comminication-MasterDataSync
Created Date:		01 July 2016 
Updated Date:		01 July 2016  
******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
//using System.Data.OracleClient;
using System.Data.SqlClient;
namespace RFID_WebService
{
    public class classMasterData
    {
        //----Start---Oracle Section--------
        #region HHD Master-Fetch Master Details  -- Oracle
        public static DataTable getMasterDetailsOracle(string getMasterDtls)
        {
            DataTable dt = new DataTable("Master");
            try
            {
                string Query = ""; 
                if (getMasterDtls == "User Master")
                {
                    Query = "Select UserId,Loginid,UserPassword,LoginType,UserName from KPCT.KM_HHDUSERMASTER where UStatus='Active'";
                }
                else if (getMasterDtls == "Reason Master")
                {
                    Query = "Select ID,REASONNAME,DESCRIPTION from KPCT.KM_REASONMST where ISACTIVE=1";
                }
                else if (getMasterDtls == "Loader Master")
                {
                    Query = "Select LOADERID,LOADERNAME,LOADERCODE from KPCT.KM_LOADERMST where LSTATUS='Active'";
                }
                else if (getMasterDtls == "Location Master-CT-Yard")
                {
                    Query = "Select ID,NAME from KPCT.KM_CTYWHLOCMST where ISACTIVE=1 and LOCTYPE='YARD'";
                }
                else if (getMasterDtls == "Location Master-CT-Warehouse")
                {
                    Query = "Select ID,NAME from KPCT.KM_CTYWHLOCMST where ISACTIVE=1 and LOCTYPE='WH'";
                }
                else if (getMasterDtls == "Location Master-CT-Parking")
                {
                    Query = "Select ID,NAME from KPCT.KM_WHLOCMST where ISACTIVE=5";
                }
                else if (getMasterDtls == "Cargo Condition Master")
                {
                    Query = "Select ID,NAME,DESCRIPTION from KPCT.KM_CARGOCONDITIONMST where ISACTIVE=1";
                }
                else if (getMasterDtls == "OPHandled By Master")
                {
                    Query = "Select ID,NAME,DESCRIPTION from KPCT.KM_OP_HANDLING_CMPNYMST where ISACTIVE=1";
                }
                else if (getMasterDtls == "Weather Condition Master")
                {
                    Query = "Select ID,NAME,DESCRIPTION from KPCT.KM_WEATHERCONMST where ISACTIVE=1";
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
        #endregion
        //----END---Oracle Section--------


        //----Start---SQL Section--------
        #region HHD Master-Fetch Master Details  -- SQL
        public static DataTable getMasterDetailsSQL(string getMasterDtls)
        {
            DataTable dt = new DataTable("Master");
            try
            {
                string Query = ""; ;
                if (getMasterDtls == "User Master")
                {
                    Query = "Select UId,Loginid,Password,LoginType,UserName from [dbo].[HHLoginMst] where [UStatus]='Active'";
                }
                else if (getMasterDtls == "Loader Master")
                {
                    Query = "Select LId,LoaderName,LoaderCode from [dbo].[LoaderMst] where [LStatus]='Active'";
                }
                classLog.writeLog("Message @: Master Details : " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                SqlDataAdapter da = new SqlDataAdapter(classServerConnections.SQLCMD);
                classServerConnections.SQLdbCon.Close();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Master Details : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.SQLCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.SQLCMD.Connection.Close();
                }
            }
            return dt;
        }
        #endregion
        //----End---SQL Section--------
    }
}
