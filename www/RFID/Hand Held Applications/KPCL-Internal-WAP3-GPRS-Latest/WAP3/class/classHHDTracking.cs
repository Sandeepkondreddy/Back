/*********************************
Author Name:		Sandeep.K
Project Name:		RFID
Purpose:	        Login Screen
Created Date:		05 Dec 2014
Updated Date:       05 Dec 2014
******************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
namespace WAP3
{
    class classHHDTracking
    {
        #region Inserting the AdminUserHHDIssue details into HHDTrack table
        public static void InsertAdminUserHHDIssueDtl(string user, string usertype)
        {
            string localConnection = classServerDetails.SdfConnection;
            SqlCeConnection dbCon = new SqlCeConnection(localConnection);
            dbCon.Open();
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string ndt = "{ts '" + datet + "'}";
            SqlCeCommand cmd = new SqlCeCommand("select Max(Id) from HHDTrack where AdminUserIssuedBy is NOT NULL and KiteUserReceivedBy is NULL and EndUserReceivedBy is NULL and EndUserReturnedBy is NULL and KiteUserRetrievedBy is NULL and AdminUserReceivedBy is NULL ", dbCon);
           // int Id;
            //Id = int.Parse(cmd.ExecuteScalar().ToString());
            string qry = "";
            string idex=cmd.ExecuteScalar().ToString();
            if (idex == "")
                qry = "Insert into HHDTrack(AdminUserIssuedBy,AdminUserIssuedTime)" + "Values('" + user + "'," + ndt + ")";
            else
                qry = "Update HHDTrack set AdminUserIssuedBy='" + user + "',AdminUserIssuedTime=" + ndt + " where Id="+ idex +"";
            SqlCeCommand cmd1 = new SqlCeCommand(qry, dbCon);
            cmd1.ExecuteNonQuery();
            dbCon.Close();
            dbCon.Dispose();
            cmd.Dispose();
            cmd1.Dispose();
        }
        #endregion

        #region Update KiteUserHHDReceive details into HHDTrack table
        public static void UpdateKiteUserHHDReceiveDtl(string user, string usertype)
        {
            string localConnection = classServerDetails.SdfConnection;
            SqlCeConnection dbCon = new SqlCeConnection(localConnection);
            dbCon.Open();
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string ndt = "{ts '" + datet + "'}";
            SqlCeCommand cmd = new SqlCeCommand("select Max(Id) from HHDTrack where AdminUserIssuedBy is NOT NULL and KiteUserReceivedBy is NULL and EndUserReceivedBy is NULL and EndUserReturnedBy is NULL and KiteUserRetrievedBy is NULL and AdminUserReceivedBy is NULL ", dbCon);
            // int Id;
            //Id = int.Parse(cmd.ExecuteScalar().ToString());
            string qry = "";
            string idex = cmd.ExecuteScalar().ToString();
            if (idex != "")
            {
                qry = "Update HHDTrack set KiteUserReceivedBy='" + user + "',KiteUserReceivedTime=" + ndt + " where Id=" + idex + "";
                SqlCeCommand cmd1 = new SqlCeCommand(qry, dbCon);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
            }
            dbCon.Close();
            dbCon.Dispose();
            cmd.Dispose();
            
        }
        #endregion

        #region Update EndUserHHDReceive details into HHDTrack table
        public static void UpdateEndUserHHDReceiveDtl(string user, string usertype)
        {
            string localConnection = classServerDetails.SdfConnection;
            SqlCeConnection dbCon = new SqlCeConnection(localConnection);
            dbCon.Open();
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string ndt = "{ts '" + datet + "'}";
            SqlCeCommand cmd = new SqlCeCommand("select Max(Id) from HHDTrack where AdminUserIssuedBy is NOT NULL and KiteUserReceivedBy is NOT NULL and EndUserReceivedBy is NULL and EndUserReturnedBy is NULL and KiteUserRetrievedBy is NULL and AdminUserReceivedBy is NULL ", dbCon);
            string qry = "";
            string idex = cmd.ExecuteScalar().ToString();
            if (idex == "")
            {
                SqlCeCommand cm = new SqlCeCommand("select Max(Id) from HHDTrack where AdminUserIssuedBy is NOT NULL and KiteUserReceivedBy is NULL and EndUserReceivedBy is NULL and EndUserReturnedBy is NULL and KiteUserRetrievedBy is NULL and AdminUserReceivedBy is NULL ", dbCon);
                idex = cm.ExecuteScalar().ToString();
            }
            if (idex != "")
            {
                qry = "Update HHDTrack set EndUserReceivedBy='" + user + "',EndUserReceivedTime=" + ndt + " where Id=" + idex + "";
                SqlCeCommand cmd1 = new SqlCeCommand(qry, dbCon);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
            }
            dbCon.Close();
            dbCon.Dispose();
            cmd.Dispose();

        }
        #endregion

        #region Update EndUserHHDReturn details into HHDTrack table
        public static void UpdateEndUserHHDReturnedDtl(string user, string usertype)
        {
            string localConnection = classServerDetails.SdfConnection;
            SqlCeConnection dbCon = new SqlCeConnection(localConnection);
            dbCon.Open();
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string ndt = "{ts '" + datet + "'}";
            SqlCeCommand cmd = new SqlCeCommand("select Max(Id) from HHDTrack where AdminUserIssuedBy is NOT NULL and KiteUserReceivedBy is NOT NULL and EndUserReceivedBy is NOT NULL and KiteUserRetrievedBy is NULL and AdminUserReceivedBy is NULL ", dbCon);
            // int Id;
            //Id = int.Parse(cmd.ExecuteScalar().ToString());
            string qry = "";
            string idex = cmd.ExecuteScalar().ToString();
            if (idex == "")
            {
                SqlCeCommand cm = new SqlCeCommand("select Max(Id) from HHDTrack where AdminUserIssuedBy is NOT NULL and KiteUserReceivedBy is NULL and EndUserReturnedBy is NULL and KiteUserRetrievedBy is NULL and AdminUserReceivedBy is NULL ", dbCon);
                idex = cm.ExecuteScalar().ToString();
            }
            if (idex != "")
            {
                qry = "Update HHDTrack set EndUserReturnedBy='" + user + "',EndUserReturnedTime=" + ndt + " where Id=" + idex + "";
                SqlCeCommand cmd1 = new SqlCeCommand(qry, dbCon);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
            }
            dbCon.Close();
            dbCon.Dispose();
            cmd.Dispose();

        }
        #endregion

        #region Update KiteUserRetrieved details into HHDTrack table
        public static void UpdateKiteUserRetrievedDtl(string user, string usertype)
        {
            string localConnection = classServerDetails.SdfConnection;
            SqlCeConnection dbCon = new SqlCeConnection(localConnection);
            dbCon.Open();
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string ndt = "{ts '" + datet + "'}";
            SqlCeCommand cmd = new SqlCeCommand("select Max(Id) from HHDTrack where AdminUserIssuedBy is NOT NULL and KiteUserReceivedBy is NOT NULL and EndUserReceivedBy is NOT NULL and EndUserReturnedBy is NOT NULL and KiteUserRetrievedBy is NULL and AdminUserReceivedBy is NULL ", dbCon);
            // int Id;
            //Id = int.Parse(cmd.ExecuteScalar().ToString());
            string qry = "";
            string idex = cmd.ExecuteScalar().ToString();
            if (idex == "")
            {
                SqlCeCommand cm = new SqlCeCommand("select Max(Id) from HHDTrack where AdminUserIssuedBy is NOT NULL and KiteUserReceivedBy is NULL and EndUserReceivedBy is NOT NULL and EndUserReturnedBy is NOT NULL and KiteUserRetrievedBy is NULL and AdminUserReceivedBy is NULL ", dbCon);
                idex = cm.ExecuteScalar().ToString();
            }
            if (idex != "")
            {
                qry = "Update HHDTrack set KiteUserRetrievedBy='" + user + "',KiteUserRetrievedTime=" + ndt + " where Id=" + idex + "";
                SqlCeCommand cmd1 = new SqlCeCommand(qry, dbCon);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
            }
            dbCon.Close();
            dbCon.Dispose();
            cmd.Dispose();

        }
        #endregion

        #region Update AdminUserReceived details into HHDTrack table
        public static void UpdateAdminUserReceivedDtl(string user, string usertype)
        {
            string localConnection = classServerDetails.SdfConnection;
            SqlCeConnection dbCon = new SqlCeConnection(localConnection);
            dbCon.Open();
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string ndt = "{ts '" + datet + "'}";
            SqlCeCommand cmd = new SqlCeCommand("select Max(Id) from HHDTrack where AdminUserIssuedBy is NOT NULL and KiteUserReceivedBy is NOT NULL and EndUserReceivedBy is NOT NULL and EndUserReturnedBy is NOT NULL and KiteUserRetrievedBy is NOT NULL and AdminUserReceivedBy is NULL ", dbCon);
            // int Id;
            //Id = int.Parse(cmd.ExecuteScalar().ToString());
            string qry = "";
            string idex = cmd.ExecuteScalar().ToString();
            if (idex == "")
            {
                SqlCeCommand cm = new SqlCeCommand("select Max(Id) from HHDTrack where AdminUserIssuedBy is NOT NULL and KiteUserReceivedBy is NULL and EndUserReceivedBy is NOT NULL and EndUserReturnedBy is NOT NULL and KiteUserRetrievedBy is NULL and AdminUserReceivedBy is NULL ", dbCon);
                idex = cm.ExecuteScalar().ToString();
            }
            if (idex != "")
            {
                qry = "Update HHDTrack set AdminUserReceivedBy='" + user + "',AdminUserReceivedTime=" + ndt + " where Id=" + idex + "";
                SqlCeCommand cmd1 = new SqlCeCommand(qry, dbCon);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
            }
            dbCon.Close();
            dbCon.Dispose();
            cmd.Dispose();

        }
        #endregion

        
       
        
    }
}
