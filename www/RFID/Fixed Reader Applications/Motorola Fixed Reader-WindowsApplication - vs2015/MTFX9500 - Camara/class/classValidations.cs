/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-AccessControl
Purpose:	        RFID-Acccess Control-Tag Validation 
Created Date:		02 Feb 2017 
Updated Date:		05 Feb 2017   
******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace CS_RFID3_Host_Sample1
{
	class classValidations
	{
        public static string ROUTEACCID;
        public static string ROUTEDTLID;
        public static string ValidateGateAcces(string tag,string MappingId,string ImagePath)
        {
            string AccessStatus = "InValid";
            DataTable dt = new DataTable("Master");
            try
            {

                string Query = "Select ROUTEACCID,ROUTEDTLID from PROD.VW_ACC_AccessDetails where Status='A' and ACCESSCARDNO='" + tag + "' and MAPID="+MappingId+"";
                classLog.writeLog("Message @: Validate Gate Access : " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                classServerConnections.OracledbCon.Close();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    AccessStatus = "Valid";
                    ROUTEACCID = dt.Rows[0]["ROUTEACCID"].ToString();
                    ROUTEDTLID = dt.Rows[0]["ROUTEDTLID"].ToString();
                    string status = classSaveTransactionDetails.SaveRouteAccessLog(ROUTEACCID, ROUTEDTLID, MappingId, ImagePath);
                }
                else
                {
                    AccessStatus = "InValid";
                    string status = classSaveTransactionDetails.SaveRouteAccessLog("0", "0", MappingId, ImagePath);
                }
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
            return AccessStatus;
        }


        public static string getMappingId(string AntennaNo)
        {
            string MappingId = "0";
            DataTable dt = new DataTable("Master");
            try
            {
                string Query = "";
                Query = "Select MapId from PROD.VW_ACC_RDRMAPPINGDTLS where ReaderIP='" + classGlobalVariables.RaederIP + "' and AntennaNo=" + AntennaNo + "";
                classLog.writeLog("Message @: get Mapping Id : " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                MappingId = classServerConnections.OracleCMD.ExecuteScalar().ToString();
                classServerConnections.OracleCMD.Dispose();
                classServerConnections.OracledbCon.Close();
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Master Details : " + ex.ToString());
                MappingId = "0";
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }
            return MappingId;
        }
	}
}
