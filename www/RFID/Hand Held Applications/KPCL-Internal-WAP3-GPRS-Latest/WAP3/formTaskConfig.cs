using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using com.caen.RFIDLibrary;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using UpdateCEMobileLocalTime;
namespace WAP3
{
    public partial class formTaskConfig : Form
    {
        string localConnection = classServerDetails.SdfConnection;
        string sqlConnection = classServerDetails.WifiSQLConnection;
        string sqlGPRSConnection = classServerDetails.GPRSSQLConnection;
        string User = classLogin.User;
        string readerip = classLogin.ReaderIP;
        string AppName = classLogin.AppName;
        string Verssion = classLogin.Version;
        int strength;
        int localdbcount;
        SqlCeConnection localdbCon;
        SqlConnection   sqldbCon;
        public formTaskConfig()
        {
            InitializeComponent();
        }
        public void signal()
        {
            try
            {
                PsionTeklogix.WLAN.ConnectionState connection = WAP3.WLANHelper.GetCurrentConnection();
                //string signal;
                strength =connection.SignalStrength;
                //strength = -30;
                if (strength < -10 && strength > -95)
                {
                    sqlConnection = classServerDetails.WifiSQLConnection;
                }
                else
                {
                    //sqlConnection = classServerDetails.GPRSSQLConnection;
                    MessageBox.Show("Wifi Not Available/Week Signal");
                }
            }
            catch
            {
                MessageBox.Show("Wifi Not Available");
            }
            //_lbReadInfoReadWrite.Text = connection.SignalStrength.ToString();
        }

        public void syncTime()
        {
           
            string cmd = "select GetDATE()";
            sqldbCon.Open();
            DateTime dts = System.DateTime.Now;
            SqlDataAdapter da = new SqlDataAdapter(cmd, sqldbCon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[0]);
            UpdateCEMobileLocalTime.classSyncTime.SetSystemDateTime(dt);
            sqldbCon.Close();
            classLog.writeLog("Message @:Time Sync Completed");
            //classLog.TaskSyncLog("Time Sync Completed", fname);
            string cmds = "select ReaderId from ReaderConfig  ";
            SqlCeDataAdapter das = new SqlCeDataAdapter(cmds, localdbCon);
            DataSet dss = new DataSet();
            das.Fill(dss);
            ReaderId = dss.Tables[0].Rows[0].ItemArray[0].ToString();
            dss.Dispose();
            das.Dispose();
            VerssionAlert();
            TruckMasterSyncValidation();
        }

        public void VerssionAlert()
        {

            string cmd = "select [Verssion] from [dbo].[VerssionInfo] where [ApplicationName]='" + AppName + "' and [AStatus]='A'";
            sqldbCon.Open();
            DateTime dts = System.DateTime.Now;
            SqlDataAdapter da = new SqlDataAdapter(cmd, sqldbCon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string SQLAppVer = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            sqldbCon.Close();
            if (SQLAppVer != Verssion)
            {
                MessageBox.Show("Please Install Latest Application.");
                classLog.writeLog("Warning @:Application Upgrade Pending");
            }
            else
            {
                classLog.writeLog("Message @:Application is Uptodate");
            }
            //classLog.TaskSyncLog("Time Sync Completed", fname);
        }

        //string fname = "l";
        private void btnRequestConfig_Click(object sender, EventArgs e)
        {
            if (localdbcount > 0)
            {
                MessageBox.Show("Data Sync Pending in LocalDB.");
                classLog.writeLog("Message @:Data Sync Pending in LocalDB.So Application not able to Sync New Task.");
            }
            else{
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            //fname = "TaskSyncLog" + datet.Replace("-", "").Replace(":", "") + ".txt";
            //classLog.TaskSyncLog("Task Sync Started...", fname);
            btnSave.Enabled = false;
            btnRequestConfig.Enabled = false;
            menuItemBack.Enabled = false;
            menuItemExit.Enabled = false;
            signal();
            if (strength < -10 && strength > -95)
            {
                
                try
                {
                    
                    sqldbCon.Open();
                    sqldbCon.Close();
                    syncTime();
                    if (TruckmasterValdation == "")
                        getReaderDetails();
                    else
                    {
                        MessageBox.Show("Truck Master Sync Pending.");
                    }
                    
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Wifi Not Available.");
                    classLog.writeLog("Error @:Configuration faild lack of Wifi/DB Connectivity");
                    //classLog.TaskSyncLog("Sync Faild lack of Wifi/DB Connectivity", fname);
                }
            }

            btnRequestConfig.Enabled = true;
            menuItemBack.Enabled = true;
            menuItemExit.Enabled = true;
        }
        }
        #region TaskDetails..............

        int TaskConfigId;
        string TaskConfID;
    
        public void clearTaskDatainlocaldb()
        {
            //string connectionString = localConnection;
            //SqlCeConnection cn = new SqlCeConnection(connectionString);
            //cn.Open();
            string Query = "DELETE FROM TaskConfig ";
            SqlCeCommand cm = new SqlCeCommand(Query, localdbCon);
            cm.ExecuteNonQuery();
            cm.Dispose();
            //cn.Close();

        }
        public void saveTaskCofigdetailsinsdfdb()//saveing the configuration details in sdf db..
        {
            //Save Record in Handle sqlce database (sdf)
           // string connectionString = "Data Source=" +
           //(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) +
           //"\\localdb.sdf;Persist Security info=False";


            using (localdbCon)
            {

                try
                {
                //connection.Open();
                string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                string ndt = "{ts '" + datet + "'}";
                SqlCeCommand cmd = new SqlCeCommand("Insert into TaskConfig ([TaskCode],[SubTaskCode],[RefCode],[SubRefCode],[Commodity],[Source],[Destination],[Location],[Status]) Values ('" + txtTaskNo.Text + "','" + txtSubTaskNo.Text + "','" + txtRefCode.Text + "','" + txtSubRefCode.Text + "','" + txtCommodity.Text + "','" + txtSource.Text + "','" + txtDestination.Text + "','" + txtSource.Text + "','Active')", localdbCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                //connection.Close();
                //saveCofigdetailsinsdfdb();


                MessageBox.Show("Configuration Success...");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //MessageBox.Show("Internaldb details notfound.");
                    MessageBox.Show("Configuration Faild...");
                }
            }
        }
        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
            Form config = new formTaskDetails();
            config.Show();
            this.Hide();
            
        }
        #region Application Exit...............
        private void menuItem2_Click(object sender, EventArgs e)
        {
            sqldbCon.Close();
            localdbCon.Close();
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }
        #endregion

        #region Fleet Details or Trucks Allocation Details Configuration
        string AllocationCode = ""; 
        public void FleetDetails()
        {

            DataTable dt = new DataTable();

            if (AllocationCode == "")
            {
                MessageBox.Show("Fleet Allotment pending.");
            }
            else
            {
                //MessageBox.Show(AllocationCode);
                string sql1 = "select a.[Tkid],a.[SubTaskCode] from [dbo].[Fleet_Allocation_Dtl_Tbl] a where a.[AllocationCode]=" + AllocationCode + "  and a.[FAStatus]='Alloted'";
                //string CONNSTRING1 = sqlConnection;
                //SqlConnection dbCon1 = new SqlConnection(CONNSTRING1);
                //dbCon1.Open();
                sqldbCon.Open();
                SqlDataAdapter daa1 = new SqlDataAdapter(sql1, sqldbCon);
                DataTable dt1 = new DataTable();
                daa1.Fill(dt1);
                daa1.Dispose(); sqldbCon.Close();
                //dbCon1.Close();
                if (dt1.Rows.Count == 0)
                {
                    MessageBox.Show("Fleet Details Not found.");
                }
                else
                {
                    try
                    {
                        foreach (DataRow drr in dt1.Rows)
                        {

                            string UpdateQry = "UPDATE [TruckMaster] set [TaskCode]='" + txtTaskNo.Text + "' ,[SubTaskCode]='" + drr[1].ToString() + "' where [Tkid]='" + drr[0].ToString() + "'";
                            //string CONN_STRING1 = localConnection;
                            //SqlCeConnection Con1 = new SqlCeConnection(CONN_STRING1);

                            //Con1.Open();
                            SqlCeCommand cmd111 = new SqlCeCommand(UpdateQry, localdbCon);
                            cmd111.ExecuteNonQuery();
                            //Con1.Close();
                        }
                        MessageBox.Show("Fleet-Task Sync Complete.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }


            }
            
          //clearlocaldbTruckMasterTaskDetails();
          
                  
        }
        #endregion
        #region  Get Reader-task Details Configuration
        string ReaderId;
        string OperationType;
        string LocationId;
        string SubTaskCode;
        public void getReaderDetails()
        {
          //string cmd = "select ReaderId from ReaderConfig  ";
          //  SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, localdbCon);
          //  DataSet ds = new DataSet();
          //  da.Fill(ds);
          //  ReaderId = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            if (ReaderId == "")
            {
                MessageBox.Show("Reader details Not found.");
                classLog.writeLog("Error @:ReaderId Missing from Reader Configuration");
                //classLog.TaskSyncLog("ReaderId Missing from Reader Configuration", fname);
            }
            else 
            {
                txtTaskStatus.Visible = true;
                string cmd1 = "select OperationType,LocationId,AllocationCode,SubTaskCode from [dbo].[Reader_Allocation_Dtl_Tbl]  where [ReaderId]=" + ReaderId + " and [RAStatus]='Allocated' ";
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1, sqldbCon);
                DataTable dt0 = new DataTable();
                da1.Fill(dt0);
                int f = 0;
                if (dt0.Rows.Count > 0)
                {
                    foreach (DataRow dr0 in dt0.Rows)
                    {
                        OperationType = dr0[0].ToString();
                        
                        LocationId = dr0[1].ToString();
                        AllocationCode = dr0[2].ToString();
                        SubTaskCode = dr0[3].ToString();
                        //MessageBox.Show(SubTaskCode);
                        if (f == 0)
                        {
                            clearTaskDatainlocaldb();
                            f++;
                            clearTaskdtlinTruckmst();
                        }
                        //...............................................
                        string cmd11 = "select a.TaskCode,a.OperationType,a.RefCode,c.SubTaskCode,c.SubRefCode,c.Commodity,c.Source,c.Destination,a.TStatus from [dbo].[Task_Tbl] a,[dbo].[Fleet_Allocation_Tbl] b,[dbo].[SubTask_Tbl] c where b.[AllocationCode]=" + AllocationCode + " and a.[TaskCode]=b.[TaskCode]  and b.[TaskCode]=c.[TaskCode] and  a.[TStatus] in('M','S','A','R')";
                    
                    SqlDataAdapter da11 = new SqlDataAdapter(cmd11, sqldbCon);
                    DataTable dt11 = new DataTable();
                    da11.Fill(dt11);
                    if (dt11.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dt11.Rows)
                        {


                            if (dr != null)
                            {
                                //classLog.TaskSyncLog("TaskDetails", fname);
                                if (dr[8].ToString() == "M")//to update the Task Status as Synced if its Status is Mapped.
                                {

                                    string co = "Update [dbo].[Task_Tbl] set TStatus='S',ModifiedBy='" + User + "',ModifiedDate=GETDATE() where TaskCode=" + dr[0].ToString() + " and TStatus='M'";
                                    
                                    sqldbCon.Open();
                                    SqlCommand c = new SqlCommand(co, sqldbCon);
                                    c.ExecuteNonQuery();
                                    sqldbCon.Close();
                                    c.Dispose();
                                }
                                //classLog.TaskSyncLog("[TaskCode],[RefCode],[SubTaskCode],[SubRefCode],[Commodity],[Source],[Destination],[LocationId],[ReaderOperation],[ReaderId]", fname);
                                string InsertQuery = "Insert into TaskConfig ([TaskCode],[RefCode],[SubTaskCode],[SubRefCode],[Commodity],[Source],[Destination],[LocationId],[ReaderOperation],[ReaderId],[Status]) Values ('" + dr[0].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','" + dr[5].ToString() + "','" + dr[6].ToString() + "','" + dr[7].ToString() + "','" + LocationId + "','" + OperationType + "','" + ReaderId + "','Active')";
                                string CON_STRING = localConnection;
                                SqlCeCommand cm = new SqlCeCommand(InsertQuery, localdbCon);
                                cm.ExecuteNonQuery();
                                cm.Dispose();
                                txtTaskNo.Text = dr[0].ToString();
                                txtOperationType.Text = dr[1].ToString();
                                txtRefCode.Text=dr[2].ToString();
                                txtSubTaskNo.Text = dr[3].ToString();
                                txtSubRefCode.Text = dr[4].ToString();
                                txtCommodity.Text = dr[5].ToString();
                                txtSource.Text = dr[6].ToString();
                                txtDestination.Text = dr[7].ToString();
                                txtTaskStatus.Text = "Task deatils Saved.";
                                //classLog.TaskSyncLog(dr[0].ToString() + "," + dr[2].ToString() + "," + dr[3].ToString() + "," + dr[4].ToString() + "," + dr[5].ToString() + "," + dr[6].ToString() + "," + dr[7].ToString() + "," + LocationId + "," + OperationType + ","+  ReaderId +""  + ":TaskDetails Saved", fname);
                                classLog.writeLog("Message @:"+txtTaskNo.Text + "Task details saved and fleat details updation in progress");
                                TaskSyncLogInfo();
                            }
                        }
                        //..............................................................................................................

                        if (AllocationCode == "")
                        {
                            MessageBox.Show("Fleet Allotment pending.");

                        }
                        else
                        {
                            string sql1 = "select a.[Tkid],a.[SubTaskCode],b.[TruckNo] from [dbo].[Fleet_Allocation_Dtl_Tbl] a,[dbo].[TruckMaster] b where a.[AllocationCode]=" + AllocationCode + "and a.[Tkid]=b.[Tkid]  and a.[FAStatus]='Alloted'";
                            sqldbCon.Open();
                            SqlDataAdapter daa1 = new SqlDataAdapter(sql1, sqldbCon);
                            DataTable dt1 = new DataTable();
                            daa1.Fill(dt1);
                            sqldbCon.Close();
                          
                            if (dt1.Rows.Count == 0)
                            {
                                
                                syncstatus = "N";
                               
                                txtTaskStatus.Text = "Fleet Details Not found.";
                                //classLog.TaskSyncLog("Task Sync Faild due to Lack of Fleet details", fname);
                                classLog.writeLog("Error @:"+txtTaskNo.Text + "Task Sync Faild due to Lack of Fleet details");
                            }
                            else
                            {
                                try
                                {
                                    int i = 1;
                                    //classLog.TaskSyncLog(dt1.Rows.Count.ToString() +":Trucks Allocated", fname);
                                    //classLog.TaskSyncLog("[TaskCode],[SubTaskCode],[Tkid],[TruckNo]", fname);
                                    foreach (DataRow drr in dt1.Rows)
                                    {

                                        txtTaskStatus.Text = "Task Truck Update:" + dt1.Rows.Count.ToString() + "-" + i.ToString();
                                        string UpdateQry = "UPDATE [TruckMaster] set [TaskCode]='" + txtTaskNo.Text + "' ,[SubTaskCode]='" + drr[1].ToString() + "' where [Tkid]='" + drr[0].ToString() + "'";
                                        //classLog.TaskSyncLog(txtTaskNo.Text + "," + drr[1].ToString() + "," + drr[0].ToString() + "," + drr[2].ToString() + ":Fleet details updated", fname);
                                        SqlCeCommand cmd111 = new SqlCeCommand(UpdateQry, localdbCon);
                                        cmd111.ExecuteNonQuery();
                                        cmd111.Dispose();
                                        i++;
                                    }
                                    syncstatus = "Y";
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                    classLog.writeLog("Erro @:"+ex.ToString());
                                }
                            }
                           
                            
                        }
                        //...................................................................
                        
                    }
                    else
                    {
                        syncstatus = "X";
                        MessageBox.Show("Task details not found.");
                    }
                    if (syncstatus == "N")
                    {
                        txtTaskNo.Text = "";
                        txtOperationType.Text = "";
                        txtRefCode.Text = "";
                        txtSubTaskNo.Text = "";
                        txtSubRefCode.Text = "";
                        txtCommodity.Text = "";
                        txtSource.Text = "";
                        txtDestination.Text = "";
                        MessageBox.Show("Task Sync Failed.");
                        classLog.writeLog("Error @:Task Sync Failed");
                        //classLog.TaskSyncLog("Task Sync Failed", fname);
                    }
                    else if(syncstatus == "Y")
                    {
                        MessageBox.Show("Task Sync Completed.");
                        classLog.writeLog("Messsage @:"+txtTaskNo.Text+ "Task Sync Completed");
                        //classLog.TaskSyncLog("Task Sync Completed", fname);
                        syncstatus = "ZZ";
                    }
                
                
                        //...............................................
                    }
                }
                else
                {
                    MessageBox.Show("Reader has no Task");
                    classLog.writeLog("Error @:Task Sync Failed due to Lack of Task");
                    //classLog.TaskSyncLog("Task Sync Failed due to Lack of Task", fname);
                }
                //da.Dispose();
                da1.Dispose();
                dt0.Dispose();
                //ds.Dispose();
            }
            txtTaskStatus.Visible = false;
            sqldbCon.Close();
        }
        string syncstatus="";
        public void TaskSyncLogInfo()
        {
            if (SubTaskCode == txtSubTaskNo.Text|| SubTaskCode =="0")
            {
                sqldbCon.Open();
                string sql1 = "Insert into [dbo].[TaskSync_Log] ([TaskCode],[SubTaskCode],[Operation],[ReaderId],[SyncTime]) Values ('" + txtTaskNo.Text + "','" + txtSubTaskNo.Text + "','" + OperationType + "','" + ReaderId + "',GETDATE())";
                SqlCommand cmd = new SqlCommand(sql1, sqldbCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                sqldbCon.Close();
                classLog.writeLog("Message @:"+txtTaskNo.Text + ":Task Sync Info Saved");
                //classLog.TaskSyncLog(txtTaskNo.Text + "Task Sync Info Saved", fname);
            }
           
        }
   
        #endregion
        private void menuItem1_Click(object sender, EventArgs e)
        {
            Form config = new formConfig();
            config.Show();
            this.Hide();
        }

        public void clearTaskdtlinTruckmst()
        {
            string UpdateQry = "UPDATE [TruckMaster] set [TaskCode]='' ,[SubTaskCode]='' ";
            
            SqlCeCommand cmd111 = new SqlCeCommand(UpdateQry, localdbCon);
            cmd111.ExecuteNonQuery();
            cmd111.Dispose();
            classLog.writeLog("Message @:Fleat Task details Reset Complete.");
        }

        public void renamedatafile()
        {
            try
            {
                string path = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\data.txt";
                if (!File.Exists(path))
                {


                }

                else if (File.Exists(path))
                {
                    string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                    string name = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) +" "+"\\data" + datet.Replace("-", "").Replace(":", "") + ".txt";
                  
                    File.Move(path, name);
                }
            }
            catch (Exception ex1)
            {

            }
        }

        private void formTaskConfig_Load(object sender, EventArgs e)
        {
            
            localdbCon = new SqlCeConnection(localConnection);
            localdbCon.Open();
            sqldbCon = new SqlConnection(sqlConnection);
            
            localdbrecordcount();
        }
        public void localdbrecordcount()//To get the initial record count from localdb and displaying in the application...
        {
            string sql = "select count(*) from InternalFleet  where status='Active'";
            SqlCeCommand cmd = new SqlCeCommand(sql, localdbCon);
            int flg;
            flg = int.Parse(cmd.ExecuteScalar().ToString());
            localdbcount = flg;
            if (flg == 0)
            {
                lblLocalDBstatus.Text = "No data in the localdb.";
            }
            else
            {
              int  count = flg;
                lblLocalDBstatus.Text = "Total Records in the LocalDB :" + count.ToString();
            }
            cmd.Dispose();
        }

        private void formTaskConfig_Closing(object sender, CancelEventArgs e)
        {
            localdbCon.Close();

        }
        #region Clearing the Internal Fleet Inactive data
        private void btnClearInternalData_Click(object sender, EventArgs e)
        {
            classLog.writeLog("Message @:Internal Data Clear Start.");
            string Query = "DELETE FROM InternalFleet where Status='InActive'";
            SqlCeCommand cm = new SqlCeCommand(Query, localdbCon);
            cm.ExecuteNonQuery();
            cm.Dispose();
            MessageBox.Show("Internal Data Cleared.");
            classLog.writeLog("Message @:Internal Data Clear Complete.");
        }
        #endregion

        #region TruckMaster Sync Validation
        string TruckmasterValdation; 
        public void TruckMasterSyncValidation()
        {
            string LatestTruckMasterUpdatedDate = "SELECT " +
            "DISTINCT CASE  " +
            "WHEN (select MAX([CreatedDate]) as MaxCreatedDate from [dbo].[Tag_TruckAllocation])  >=  (select MAX([ModifiedDate]) as MaxModifiedDate from [dbo].[Tag_TruckAllocation])  THEN (select MAX([CreatedDate]) as MaxCreatedDate from [dbo].[Tag_TruckAllocation])" +
            "  ELSE                              (select MAX([ModifiedDate]) as MaxModifiedDate from [dbo].[Tag_TruckAllocation] )" +
            " END AS LatestTruckMasterUpdatedDate" +
            " from [dbo].[Tag_TruckAllocation]";
            string ReaderLastUpdatedDate = "SELECT MAX([SyncTime]) as ReaderLastSyncTime  FROM [rfid].[dbo].[TruckMasterSync_Log] where [ReaderId]=" + ReaderId + "";
            
           // classLog.writeLog("Alert @:TruckMaster Qry:" + LatestTruckMasterUpdatedDate.ToString());
           // classLog.writeLog("Alert @:ReaderUpdated Qry:" + ReaderLastUpdatedDate.ToString());
            
            sqldbCon.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(LatestTruckMasterUpdatedDate, sqldbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DateTime LatestTruckMasterUpdatedDate1 = Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[0].ToString());
            
                SqlDataAdapter da1 = new SqlDataAdapter(ReaderLastUpdatedDate, sqldbCon);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                DateTime ReaderLastUpdatedDate1 = Convert.ToDateTime(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
            
            sqldbCon.Close();

            if (LatestTruckMasterUpdatedDate1 > ReaderLastUpdatedDate1)
            {
                //MessageBox.Show("Please Sync TruckMaster.");
                classLog.writeLog("Warning @:Truck Master is not upto date in the Reader. Truck Master Sync required.");
                TruckmasterValdation = "Required";
            }
            else { classLog.writeLog("Message @:Truck Master is upto date in the Reader."); TruckmasterValdation = ""; }
            }
            catch
            {
                classLog.writeLog("Error @:Problem in Truck Master Sysnc Validation.");
            }
        }
        #endregion
    }

}