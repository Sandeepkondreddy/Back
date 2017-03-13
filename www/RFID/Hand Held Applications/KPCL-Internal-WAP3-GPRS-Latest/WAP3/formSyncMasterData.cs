/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-KPCL
Purpose:	        Data Sync
Created Date:		23 May 2014 
Updated Date:		08 Dec 2014 
******************************************************/
using System;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace WAP3
{
    public partial class formSyncMasterData : Form
    {
        private bool isProcessRunning = false;
        string localConnection = classServerDetails.SdfConnection;
        string sqlConnection = classServerDetails.WifiSQLConnection;
        int Rid;
        int strength;
        string ReaderId;
        SqlCeConnection dbCon;
        public formSyncMasterData()
        {
            InitializeComponent();
        }

        
        private void btnRequestSync_Click(object sender, EventArgs e)
        {
            //string fname="loggg";
            try
            {
                if (cmbSyncData.Text != "----Select----" && cmbSyncData.Text != "" && cmbSyncData.Text != "System.Data.DataRowView")
                {
                    cmbSyncData.Enabled = false; btnRequestSync.Enabled = false;
                    txtStatus.Text = "";
                    string sql = "";
                    
                     if (cmbSyncData.Text == "User Master")
                    {
                        string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                         //fname = "UserMasterSyncLog" + datet.Replace("-", "").Replace(":", "") + ".txt";
                        //classLog.UserMasterSyncLog("Truck Sync Started...", fname);
                        classLog.writeLog("User Master Sync Started.");
                        sql = " Select UId,Loginid,Password,LoginType,UserName from [dbo].[HHLoginMst] where [UStatus]='Active'";
                        //classLog.TruckMasterSyncLog("User Details fetch Start...", fname);
                    }
                    else if (cmbSyncData.Text == "Loader Master")
                    {
                        sql = " Select LId,LoaderName,LoaderCode from [dbo].[LoaderMst] where [LStatus]='Active'";
                    }
                    else if (cmbSyncData.Text == "Truck Master")
                    {
                        string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                         //fname = "TruckMasterSyncLog" + datet.Replace("-", "").Replace(":", "") + ".txt";
                        //classLog.TruckMasterSyncLog("Truck Sync Started...", fname);
                        classLog.writeLog("Truck Master Sync Started");
                        sql = "select a.Alltid,a.tkid,b.truckno,a.Tagid,c.tagno from [dbo].[Tag_TruckAllocation] a,[dbo].[TruckMaster] b,[dbo].[TagMaster] c where a.tkid=b.tkid and a.[Tagid]=c.[Tagid] and a.RStatus='Active'";
                        //classLog.TruckMasterSyncLog("Truck Details fetch Start...", fname);
                    }

                    string CONNSTRING = sqlConnection;
                    SqlConnection dbCon = new SqlConnection(CONNSTRING);
                    dbCon.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, dbCon);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (cmbSyncData.Text == "CargoGroup Master")
                    {
                        clearlocaldbCargoGroupMaster();
                    }
                    else if (cmbSyncData.Text == "Commodity Master")
                    {
                        clearlocaldbCommodityMaster();
                    }
                    else if (cmbSyncData.Text == "User Master")
                    {
                        //classLog.UserMasterSyncLog("User Details fetch Complete", fname);
                        clearlocaldbUserMaster();
                        //classLog.UserMasterSyncLog(dt.Rows.Count.ToString() + ":User details to Sync", fname);
                        //classLog.UserMasterSyncLog("[Id],[LoginId],[LoginType],[UserName]", fname);
                    }
                    else if (cmbSyncData.Text == "Loader Master")
                    {
                        clearlocaldbLoaderMaster();
                    }
                    else if (cmbSyncData.Text == "Truck Master")
                    {
                        //classLog.TruckMasterSyncLog("Truck Details fetch Complete", fname);
                        clearlocaldbTruckMaster();
                        ////classLog.TruckMasterSyncLog("Truck Details Cleared in Local DB");
                        //classLog.TruckMasterSyncLog(dt.Rows.Count.ToString() + ":Trucks to Sync", fname); 
                       //classLog.TruckMasterSyncLog("[Id],[Tkid],[TruckNo],[Tagid],[TagNo]", fname);
                    }
                    //................................................

                    lblStatus.Text = "Sync In Progress....";
                    string InsertQry = "";
                    try
                    {
                        //progressBar1.Value = 0;
                        
                        if (dt.Rows.Count > 0)
                        {
                            int i=0;
                            txtStatus.Visible = true;
                            txtStatus.Text = dt.Rows.Count.ToString()+"-" + i.ToString();
                            //while (progressBar1.Value < 100)
                            foreach (DataRow dr in dt.Rows)
                            {
                                
                                
                                if (dr != null)
                                {
                                    i++;
                                    txtStatus.Text = "Sync Progress:" +dt.Rows.Count.ToString() + "-" + i.ToString();
                                    lblStatus.Text = "Sync ....";
                                     if (cmbSyncData.Text == "User Master")
                                    {
                                        InsertQry = "INSERT INTO [UserMst] ([Id],[LoginId],[Password],[LoginType],[UserName],[Status]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','Active')";
                                        //classLog.UserMasterSyncLog(dr[0].ToString() + "," + dr[1].ToString() + "," + dr[3].ToString() + "," + dr[4].ToString() + "," + "User Details Saved", fname);
                                    }
                                    else if (cmbSyncData.Text == "Loader Master")
                                    {
                                        InsertQry = "INSERT INTO [LoaderMst] ([Id],[LoaderName],[LoaderCode],[Status]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','Active')";
                                    }
                                    else if (cmbSyncData.Text == "Truck Master")
                                    {
                                        InsertQry = "INSERT INTO [TruckMaster] ([Id],[Tkid],[TruckNo],[Tagid],[TagNo],[TStatus]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','Active')";
                                        //classLog.TruckMasterSyncLog(dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString() + "," + dr[4].ToString() + "," + "Truck Details Saved", fname );
                                    }
                                    string CONN_STRING = localConnection;
                                    SqlCeConnection Con = new SqlCeConnection(CONN_STRING);

                                    Con.Open();
                                    SqlCeCommand cmd = new SqlCeCommand(InsertQry, Con);
                                    cmd.ExecuteNonQuery();
                                    Con.Close();

                                    lblStatus.Text = "Sync In Progress....";
                                    //System.Threading.Thread.Sleep(2000);
                                    //progressBar1.Value += 5;
                                }
                            }

                        }
                        else
                        {
                            throw new Exception("No row for insertion");
                        }
                        dt.Dispose();
                        lblStatus.Text = "Sync Complete.";
                        if (cmbSyncData.Text == "User Master")
                        {
                            //classLog.UserMasterSyncLog(cmbSyncData.Text + "Sync Completed", fname);
                            classLog.writeLog("User Master Sync Completed.");
                        }
                        else if (cmbSyncData.Text == "Loader Master")
                        {
                            
                        }
                        else if (cmbSyncData.Text == "Truck Master")
                        {
                            //classLog.TruckMasterSyncLog(cmbSyncData.Text + "Sync Completed", fname);
                            classLog.writeLog("Truck Master Sync Completed.");
                            TruckMasterSyncLogInfo();
                        }
                        
                        classLog.writeLog(cmbSyncData.Text + "Sync Complete");
                        txtStatus.Visible = false;
                    }

                    catch (Exception ex)
                    {
                        dt.Dispose();
                        throw new Exception("Please attach file in Proper format.");
                    }


                }
                cmbSyncData.Enabled = true; btnRequestSync.Enabled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                //txtStatus.Text = "Wifi Not Available.";
            }


           
        }
        public void clearlocaldbCargoGroupMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM CargoGroupMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
        public void clearlocaldbCommodityMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM CommodityMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }

        public void clearlocaldbUserMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM UserMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
        public void clearlocaldbLoaderMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM LoaderMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }

        public void clearlocaldbTruckMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM TruckMaster";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }

        private void formSyncMasterData_Closing(object sender, CancelEventArgs e)
        {
            classLog.writeLog("SyncMaster Form Exit");
            Application.Exit();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Form config = new formConfig();
            config.Show();
            this.Hide();
        }

        private void cmbSyncData_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtStatus.Text = "";
            lblStatus.Text = "";
        }

        private void formSyncMasterData_Load(object sender, EventArgs e)
        {
            classLog.writeLog("SyncMaster Form Loaded");
            string cmd = "select ReaderId from ReaderConfig  ";
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, localConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ReaderId = ds.Tables[0].Rows[0].ItemArray[0].ToString();
        }

        #region Sync UserLoginLog details to SQL DB
        #region Signal Strenth
        public void signal()
        {
            try
            {
                PsionTeklogix.WLAN.ConnectionState connection = WAP3.WLANHelper.GetCurrentConnection();
                strength = connection.SignalStrength;
            }
            catch (Exception ex)
            {
                //_lbReadInfoReadWrite.Text = "Wifi Not Available.";
                strength = -99;
            }
            //strength = -30;
            //_lbReadInfoReadWrite.Text = connection.SignalStrength.ToString();
        }
        #endregion

        public void SyncUserLoginLogDtl()
        {
            signal();
            if (strength < -20 && strength > -95)
            {

                string CONN_STRING = sqlConnection;
                SqlConnection dbCon = new SqlConnection(CONN_STRING);
                try
                {
                    dbCon.Open();
                    dbCon.Close();

                    syncUserLoginLogfromlocaldbtoSQLdb();
                }
                catch
                {
                    //
                }

            }
            else
            {
                //
            }

        }

        public void syncUserLoginLogfromlocaldbtoSQLdb()
        {
            dbCon = new SqlCeConnection(localConnection);
            dbCon.Open();
            string sql = "select count(*) from UserLoginLog";
            SqlCeCommand cmd = new SqlCeCommand(sql, dbCon);
            int flg;
            flg = int.Parse(cmd.ExecuteScalar().ToString());
            if (flg == 0)
            {
                MessageBox.Show("No data in the localdb to sync.");
                //classLog.DataSyncLog("No data in the localdb to sync.", fname);
            }
            else
            {
                //classLog.DataSyncLog("[TagNo],[TruckNo],[TaskCode],[SubTaskCode],[Location],[ReaderNo],[ReaderIP],[TStatus],[ReadTime],[CreatedBy],[CreatedDate],[ReaderOperation]", fname);
                int i;
                for (i = 0; i < flg; i++)
                {
                    //txtCount.Text = "Sync Progress: " + (flg - i).ToString();
                    string c = "Select min(Id) from UserLoginLog ";
                    SqlCeCommand c1 = new SqlCeCommand(c, dbCon);
                    Rid = int.Parse(c1.ExecuteScalar().ToString());
                    c1.Dispose();
                    string cmd1 = "select Id,Loginid,UserType,LoginTime from UserLoginLog where  Id=" + Rid + "";
                    SqlCeDataAdapter da = new SqlCeDataAdapter(cmd1, dbCon);
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds);
                        string id = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        string loginid = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        string usertype = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                        string logintime = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                        DateTime dt = Convert.ToDateTime(logintime);
                        string cdt = dt.ToString("yyy-MM-dd HH:mm:ss");
                        string ndt = "{ts '" + cdt + "'}";
                        string cmd01 = "select ReaderId from Config ";
                        SqlCeDataAdapter da1 = new SqlCeDataAdapter(cmd01, dbCon);
                        DataSet ds1 = new DataSet(); da1.Fill(ds1);
                        string readerid = ds1.Tables[0].Rows[0].ItemArray[0].ToString();

                        string TableName = "HHDUserLoginLog";
                        signal();
                        if (strength < -10 && strength > -95)
                        {
                            string CONN_STRING = sqlConnection;
                            SqlConnection dbCon1 = new SqlConnection(CONN_STRING);
                            try
                            {
                                dbCon1.Open();
                                string Query = "Insert into [dbo].[" + TableName + "] ([Readerid],[Loginid],[UserType],[LoginTime],[Synctime]) Values('" + readerid + "','" + loginid + "'," + usertype + "," + ndt + "',GETDATE())";
                                SqlCommand cm = new SqlCommand(Query, dbCon1);
                                cm.ExecuteNonQuery();
                                dbCon1.Close();
                                clearLoginlocaldb();
                                da.Dispose();
                                ds.Dispose();
                                // classLog.DataSyncLog(tagno + "," + truckno + "," + taskcode + "," + subtaskcode + "," + Location + "," + readerno + "," + readerip + "," + status + "," + ndt + "," + createdby + "," + ndt + "," + readeroperation, fname);
                            }
                            catch
                            {
                                MessageBox.Show("Database Disconnected.");
                                //classLog.DataSyncLog("Database Disconnected", fname);
                            }
                            // count--;
                        }
                        else
                        {
                            MessageBox.Show("Wifi Disconnected.");
                            //classLog.DataSyncLog("Wifi Disconnected", fname);
                            i = 100000;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Details not found in the Database.");
                    }
                }
                if (i == flg)
                {
                    //_lbReadInfoReadWrite.Text = "Sync Complete";
                    classLog.writeLog("Sync Complete.");
                    // classLog.DataSyncLog("Sync Complete", fname);
                }
                else
                    //_lbReadInfoReadWrite.Text = "DB Connection Failed.";
                    MessageBox.Show("DB Connection Failed.");
            }
            dbCon.Close();
        }

        public void clearLoginlocaldb()
        {
            string Query = "DELETE FROM InternalFleet where Id=" + Rid + "";
            //string Query = "Update InternalFleet set Status='InActive' where Id=" + Rid + "";
            SqlCeCommand cm = new SqlCeCommand(Query, dbCon);
            cm.ExecuteNonQuery();
            cm.Dispose();
            //_btReadReadWrite.Focus();
        }
        #endregion

        #region Log File Move to Serevr
        public void Movefiles()
        {
            string fileName = "test.txt";
            string sourcePath = @"" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\Log";
            string targetPath = @"\\SQLServer\LogDetails";
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                    //using (new Impersonator("myUsername", "myDomainname", "myPassword"))
                    //{
                        // code that executes under the new context.
                        File.Copy(s, destFile, true);
                    //}
                }
            }
            else
            {
                MessageBox.Show("Source path does not exist!");
            }
        }

       
        #endregion
        #region Logdetails updated in SQL DB
        private void button1_Click(object sender, EventArgs e)
        {
            //Movefiles();
            string path = @"" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\Log";
            //DirectoryInfo d = new DirectoryInfo(path);
            //FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            //foreach(FileInfo file in Files )
            //{
            //  string str = str + ", " + file.Name;
            //}
            StringBuilder sb = new StringBuilder();
            foreach (string txtName in Directory.GetFiles(path, "*.txt"))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(txtName.ToString());
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    string filename = txtName.ToString().Substring(29);
                    string readerid = "10";
                    classLogUpload.UserMasterSyncLog(readerid, filename, line);
                    //counter++;
                }

                file.Close();
                classLog.writeLog("Log uploded" + txtName.ToString());
                File.Delete(txtName);
                //using (StreamReader sr = new StreamReader(txtName))
                //{
                //    sb.AppendLine(txtName.ToString());
                //    sb.AppendLine("= = = = = =");
                //    sb.Append(sr.ReadToEnd());
                //    sb.AppendLine();
                //    sb.AppendLine();
                //    string filename = txtName.ToString().Substring(29);
                //    string readerid="10";
                    
                //    MessageBox.Show(sb.ToString());
                //    classLogUpload.UserMasterSyncLog(readerid, filename, sb.ToString());
                //}
            }
        }
        #endregion
        #region TruckMasterSyncLog
        public void TruckMasterSyncLogInfo()
        {
                string CONNSTRING = sqlConnection;
                SqlConnection sqldbCon = new SqlConnection(CONNSTRING);
                sqldbCon.Open();
                string sql = "Insert into [dbo].[TruckMasterSync_Log] ([ReaderId],[SyncTime]) Values ('" + ReaderId + "',GETDATE())";
                SqlCommand cmd = new SqlCommand(sql, sqldbCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                sqldbCon.Close();
        }
        #endregion
    }
}