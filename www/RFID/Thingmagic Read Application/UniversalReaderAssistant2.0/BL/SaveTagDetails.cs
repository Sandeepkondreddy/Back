using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ThingMagic.URA2.BL
{

    class SaveTagDetails
    {
        static string SqlstrRoad = ServerConnections.SqlConn_String_Road;
        static string SqlstrInternal = ServerConnections.SqlConn_String_Internal;
        static SqlCommand cmd;
        
        #region Check the Tag is either in Internal fleet or By Road to get the truckno
        public static string FunctionValidateTag_Details(string tagno,string antina)
        {
            string message;
            string truckno="";
            SqlConnection SqlRoadCon = new SqlConnection(SqlstrRoad);
            SqlConnection SqlInternalCon = new SqlConnection(SqlstrInternal);
            try
            {
                if (SqlRoadCon.State == ConnectionState.Closed || SqlRoadCon.State == ConnectionState.Broken)
                {
                    SqlRoadCon.Open();
                }
                if (SqlInternalCon.State == ConnectionState.Closed || SqlInternalCon.State == ConnectionState.Broken)
                {
                    SqlInternalCon.Open();
                }
                
                string com1 = "select [TruckNo] from [dbo].[Tag_TruckAllocation] A,  [dbo].[TruckMaster] B,  [dbo].[TagMaster] C where A.Tagid=C.Tagid and A.Tkid=B.Tkid and A.[RStatus]='Active' and C.TagNo='" + tagno + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(com1, SqlInternalCon);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                   truckno = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                    bool validateDuplicateTagdetails = Validate_TagDuplication(tagno, getWBNo(antina),"Internal");
                    if (validateDuplicateTagdetails)
                    {
                        string insertQry = "Insert into [dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values('" + tagno + "','" + antina + "'" + ",'" + getWBNo(antina) + "',GETDATE(),'" + truckno + "','" + ServerConnections.ReaderIP + "')";
                        cmd = new SqlCommand(insertQry, SqlInternalCon); cmd.ExecuteNonQuery();
                        message = tagno + " / " + truckno + " : Tag / Truck Internal Fleet details captured successfully at Location:" + getWBNo(antina).ToString();
                    }
                    else message ="Duplicate " +tagno + " / " + truckno + " : Tag / Truck Internal Fleet details found at Location:" + getWBNo(antina).ToString();
                }
                else
                {
                        if (SqlRoadCon.State == ConnectionState.Closed || SqlRoadCon.State == ConnectionState.Broken)
                        {
                        SqlRoadCon.Open();
                        }
                        string com2 = "SELECT [Truck No]  FROM  [dbo].[Tag_Register] where [Tag No] ='" + tagno + "' and [TransctionStatus] = 'P' order by [Tag ID] desc";
                        SqlDataAdapter da2 = new SqlDataAdapter(com2, SqlRoadCon);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            truckno = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
                        bool validateDuplicateTagdetails = Validate_TagDuplication(tagno, getWBNo(antina), "ByRoad");
                        if (validateDuplicateTagdetails)
                        {
                            string insertQry1 = "Insert into [dbo].[MTFX9500BYROAD] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values('" + tagno + "','" + antina + "'" + ",'" + getWBNo(antina) + "',GETDATE(),'" + truckno + "','" + ServerConnections.ReaderIP + "')";
                            cmd = new SqlCommand(insertQry1, SqlRoadCon); cmd.ExecuteNonQuery();
                            message = tagno + " / " + truckno + " : Tag / Truck ByRoad Fleet details captured successfully at Location:" + getWBNo(antina).ToString();
                        }
                        else message = "Duplicate " + tagno + " / " + truckno + " : Tag / Truck ByRoad Fleet details found at Location:" + getWBNo(antina).ToString();
                    }
                        else
                        {
                            string strNonQuery_Parking = "Insert into  [dbo].[MTFX9500JunkData] ([tagno],[Ant],[wbno],[readdt],[Reader]) Values('" + tagno + "','" + antina + "','" + getWBNo(antina) + "',GETDATE(),'" + ServerConnections.ReaderIP + "')";
                            cmd = new SqlCommand(strNonQuery_Parking, SqlRoadCon);
                            cmd.ExecuteNonQuery();
                            message = tagno + ": Tag is Not Registered to any truck at Location:" + getWBNo(antina).ToString();
                        }
                        
                    }
                
            }
            catch (Exception ex)
            {
                message = "Exception: "+ex.Message.ToString();
            }
            SqlRoadCon.Close();
            SqlInternalCon.Close();
            ServerConnections.Status = message.ToString();
            return message;
        }
        #endregion

        #region Save Tag details
        public static string SaveTag_details(string tagno, string antina)
        {
            string msg="";


            string message = FunctionValidateTag_Details(tagno.Substring(0,5).ToString(), antina);
            return msg;
        }
        #endregion

        #region getWBNo using antena
        public static string getWBNo(string antena)
        {
            string wbno = "";
            if (antena == "1") wbno = ServerConnections.Loc1;
            else if (antena == "2") wbno = ServerConnections.Loc2;
            return wbno;
        }
        #endregion

        #region Internal Tag duplication validation
        public static bool Validate_TagDuplication(string tagno,string wbno, string fleetType)
        {
            SqlConnection SqlRoadCon = new SqlConnection(SqlstrRoad);
            SqlConnection SqlInternalCon = new SqlConnection(SqlstrInternal);
            try
            {
                if (fleetType == "Internal")
                {
                    if (SqlInternalCon.State == ConnectionState.Closed || SqlInternalCon.State == ConnectionState.Broken)
                    {
                        SqlInternalCon.Open();
                    }
                    string sqlQry = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from [dbo].[MTFX9500InternalFleet] where  [wbno] ='" + wbno + "'  order by readdt desc";
                    SqlDataAdapter da = new SqlDataAdapter(sqlQry, SqlInternalCon);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (tagno == ds.Tables[0].Rows[0].ItemArray[0].ToString()) return false;
                        else return true;
                    }
                    else return true;
                }
                if (fleetType == "ByRoad")
                {
                    if (SqlRoadCon.State == ConnectionState.Closed || SqlRoadCon.State == ConnectionState.Broken)
                    {
                        SqlRoadCon.Open();
                    }
                    string sqlQry = "select  TOP 1 [tagno],[Ant],[wbno],[readdt] from  [dbo].[MTFX9500BYROAD] where  [wbno] ='" + wbno + "'  order by readdt desc";
                    SqlDataAdapter da = new SqlDataAdapter(sqlQry, SqlRoadCon);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (tagno == ds.Tables[0].Rows[0].ItemArray[0].ToString()) return false;
                        else return true;
                    }
                    else return true;
                }
                else return false;
            }
            catch(Exception ex)
            {
                return true;
            }
        }
        #endregion
    }
}
