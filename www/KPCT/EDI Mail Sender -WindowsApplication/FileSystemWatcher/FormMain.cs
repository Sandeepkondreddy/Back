using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using System.Threading;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Timers;
namespace FileChangeNotifier
{
    
    public partial class frmNotifier : Form
    {
        //private  Timer aTimer;
        private StringBuilder m_Sb;
        private bool m_bDirty;
        private System.IO.FileSystemWatcher m_Watcher;
        private bool m_bIsWatching;
        string sqlConnection = classServerDetails.SQLConnection;
        SqlConnection Sqlcon;
        string Line="Others";
        public frmNotifier()
        {
            InitializeComponent();
            m_Sb = new StringBuilder();
            m_bDirty = false;
            m_bIsWatching = false;
        }

        private void btnWatchFile_Click(object sender, EventArgs e)
        {
            if (m_bIsWatching)
            {
                m_bIsWatching = false;
                m_Watcher.EnableRaisingEvents = false;
                m_Watcher.Dispose();
                btnWatchFile.BackColor = Color.LightSkyBlue;
                btnWatchFile.Text = "Start Watching";
                
            }
            else
            {
                m_bIsWatching = true;
                btnWatchFile.BackColor = Color.Red;
                btnWatchFile.Text = "Stop Watching";
                //lstNotification.Text = "EDI File Watch is Started.";
                m_Watcher = new System.IO.FileSystemWatcher();
                if (rdbDir.Checked)
                {
                    m_Watcher.Filter = "*.edi";
                    m_Watcher.Path = txtFile.Text + "\\";
                }
                else
                {
                    m_Watcher.Filter = txtFile.Text.Substring(txtFile.Text.LastIndexOf('\\') + 1);
                    m_Watcher.Path = txtFile.Text.Substring(0, txtFile.Text.Length - m_Watcher.Filter.Length);
                }

                if (chkSubFolder.Checked)
                {
                    m_Watcher.IncludeSubdirectories = true;
                }

                m_Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                m_Watcher.Changed += new FileSystemEventHandler(OnChanged);
                m_Watcher.Created += new FileSystemEventHandler(OnChanged);
                m_Watcher.Deleted += new FileSystemEventHandler(OnChanged);
                m_Watcher.Renamed += new RenamedEventHandler(OnRenamed);
                m_Watcher.EnableRaisingEvents = true;
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!m_bDirty)
            {
                if (e.ChangeType.ToString() == "Created" || e.ChangeType.ToString() == "Renamed")
                {
                    string strFileName;
                    strFileName = e.FullPath.ToString();
                    //try
                    //{
                        string xsubstr = ""; ;

                        string x = e.Name;
                        for (int i = 0; i < (x.Length - 3); i++)
                        {
                            xsubstr = x.Substring(i, 3);
                            if (xsubstr == "MSK" || xsubstr == "SCL")
                            {
                                i = i + 30;

                            }
                            else if (xsubstr == "MSC")
                            {
                                i = i + 30;

                            }
                            else if (xsubstr == "HLL")
                            {
                                i = i + 30;

                            }

                        }
                        if (xsubstr == "MSC")
                        {
                            Line = xsubstr;
                            insertEDIDetails(xsubstr, e.Name, e.FullPath);
                        }
                        else if (xsubstr == "MSK" || xsubstr == "SCL")
                        {
                            Line = xsubstr;
                            insertEDIDetails(xsubstr, e.Name, e.FullPath);
                        }
                        else if (xsubstr == "HLL")
                        {
                            Line = xsubstr;
                            insertEDIDetails(xsubstr, e.Name, e.FullPath);
                        }
                        else
                        {
                            Line = "Others";
                            insertEDIDetails("Others", e.Name, e.FullPath);

                        }

                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.ToString());
                    //}
                }
                
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!m_bDirty)
            {
                m_Sb.Remove(0, m_Sb.Length);
                m_Sb.Append(e.OldFullPath);
                m_Sb.Append(" ");
                m_Sb.Append(e.ChangeType.ToString());
                m_Sb.Append(" ");
                m_Sb.Append("to ");
                m_Sb.Append(e.Name);
                m_Sb.Append("    ");
                m_Sb.Append(DateTime.Now.ToString());
                m_bDirty = true;
                if (rdbFile.Checked)
                {
                    m_Watcher.Filter = e.Name;
                    m_Watcher.Path = e.FullPath.Substring(0, e.FullPath.Length - m_Watcher.Filter.Length);
                }

                string xsubstr = ""; ;

                string x = e.Name;
                for (int i = 0; i < (x.Length - 3); i++)
                {
                    xsubstr = x.Substring(i, 3);
                    if (xsubstr == "MSK" || xsubstr == "SCL")
                    {
                        i = i + 30;

                    }
                    else if (xsubstr == "MSC")
                    {
                        i = i + 30;

                    }
                    else if (xsubstr == "HLL")
                    {
                        i = i + 30;

                    }

                }
                if (xsubstr == "MSC")
                {
                    Line = xsubstr;
                    insertEDIDetails(xsubstr, e.Name, e.FullPath);
                }
                else if (xsubstr == "MSK" || xsubstr == "SCL")
                {
                    Line = xsubstr;
                    insertEDIDetails(xsubstr, e.Name, e.FullPath);
                }
                else if (xsubstr == "HLL")
                {
                    Line = xsubstr;
                    insertEDIDetails(xsubstr, e.Name, e.FullPath);
                }
                else
                {
                    Line = "Others";
                    insertEDIDetails("Others", e.Name, e.FullPath);

                }
            }            
        }
       
        private void tmrEditNotify_Tick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(4000);
            if (m_bDirty)
            {
                
                lstNotification.BeginUpdate();
                lstNotification.Items.Add(m_Sb.ToString());
                lstNotification.EndUpdate();
                
                m_bDirty = false;
            }
            //SendPendingMails();
            Checkpendingmails();
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            if (rdbDir.Checked)
            {
                DialogResult resDialog = dlgOpenDir.ShowDialog();
                if (resDialog.ToString() == "OK")
                {
                    txtFile.Text = dlgOpenDir.SelectedPath;
                }
            }
            else
            {
                DialogResult resDialog = dlgOpenFile.ShowDialog();
                if (resDialog.ToString() == "OK")
                {
                    txtFile.Text = dlgOpenFile.FileName;
                }
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            DialogResult resDialog = dlgSaveFile.ShowDialog();
            if (resDialog.ToString() == "OK")
            {
                FileInfo fi = new FileInfo(dlgSaveFile.FileName);
                StreamWriter sw = fi.CreateText();
                foreach (string sItem in lstNotification.Items)
                {
                    sw.WriteLine(sItem);
                }
                sw.Close();
            }
        }

        private void rdbFile_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbFile.Checked == true)
            {
                chkSubFolder.Enabled = false;
                chkSubFolder.Checked = false;
            }
        }

        private void rdbDir_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDir.Checked == true)
            {
                chkSubFolder.Enabled = true;
            }
        }

        public void insertEDIDetails(string line,string filename,string filepath)
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string Query = "Insert into [dbo].[EDIData] ([Line],[FileName],[FilePath]) Values('" + line + "','" + filename + "','" + filepath + "')";
            SqlCommand cmd = new SqlCommand(Query, Sqlcon);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //string MailSendingStatus= Sendmail(line,filename, filepath);
            Sqlcon.Close();
            SendPendingMails(); PreviousExecutiondate = System.DateTime.Now;
        }

        public void SendPendingMails()
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string Qr = "select count(*) from [dbo].[EDIData] where Line='" + Line + "' and [MailStatus] is null";
            SqlCommand cmd = new SqlCommand(Qr, Sqlcon);
            int flg = int.Parse(cmd.ExecuteScalar().ToString());
            if (flg > 0)
            {
                for (int i = 0; i < flg; i++)
                {
                    string cmd0 = "select TOP 1 [Line],[FileName],[FilePath] from [dbo].[EDIData] where Line='" + Line + "' and [MailStatus] is null order by Id asc";
                    SqlDataAdapter da = new SqlDataAdapter(cmd0, Sqlcon);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string MailSendingStatus = Sendmail(ds.Tables[0].Rows[0].ItemArray[0].ToString(), ds.Tables[0].Rows[0].ItemArray[1].ToString(), ds.Tables[0].Rows[0].ItemArray[2].ToString());
                        if (MailSendingStatus == "Success")
                        {
                            string Qry = "Update [dbo].[EDIData] set [MailStatus]='Suceess' where FileName='" + ds.Tables[0].Rows[0].ItemArray[1].ToString() + "'";
                            SqlCommand cmd1 = new SqlCommand(Qry, Sqlcon);

                            cmd1.ExecuteNonQuery();
                        }
                    }
                }
            }
            Sqlcon.Close(); 
        }

        public string Sendmail(string line, string filename, string filepath)
        {
            string MailSendingStatus = "Failed";

            try{
            SmtpClient smtpClient = new SmtpClient();
            //NetworkCredential customCredentials = new NetworkCredential("sandeep.k@krishnapatnamport.com", "SAN@kpcl");
            NetworkCredential customCredentials = new NetworkCredential("it.ct@krishnapatnamport.com", "kpct@999");
            MailMessage message = new MailMessage();
            //smtpClient.Host = "mail.krishnapatnamport.com";
            smtpClient.Host = "smtp.office365.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = customCredentials;
            smtpClient.EnableSsl = true;
            //message.From = new MailAddress("sandeep.k@krishnapatnamport.com");
            message.From = new MailAddress("it.ct@krishnapatnamport.com");

            if (line == "MSC")
            {
                message.Subject = "MSC_CODECO";
                message.IsBodyHtml = true;
                //string CC = ConfigurationManager.AppSettings["cc"];
                //string TO = ConfigurationManager.AppSettings["to"];
                //string BCC = ConfigurationManager.AppSettings["bcc"];
                //message.To.Add("edi_chennai@mscindia.com");
                message.To.Add("IN363-mscchnlog@msc.com");
                message.CC.Add("mscchnlog@mscindia.com");
                message.CC.Add("ragava@mscindia.com");
                message.CC.Add("sandeep.k@krishnapatnamport.com");
                message.CC.Add("ss.ct@krishnapatnamport.com");
                message.CC.Add("it.ct@krishnapatnamport.com");
                message.CC.Add("epms_support@krishnapatnamport.com");
                message.CC.Add("rajkumar.ss@krishnapatnamport.com");

                message.Body = "Dear Sir, <br/><br/>        PFA - MSC EDI File for Containers Transactions file " + filename + " generated on " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " "
                    + "<br/> <br/>       *** This is an automatically generated email, please do not reply ***<br/> <br/> <br/>Thanks and Regards<br><b>IT Support</b><br>Krishnapatnam Port Co. Ltd.</font><br>Website:www.krishnapatnamport.com</font><br/>";
            }
            else if (line == "MSK" || line == "SCL")
            {
                //insertEDIDetails(xsubstr, e.Name, e.FullPath);
                if (line == "MSK") message.Subject = "MSK_EDI";
                if (line == "SCL") message.Subject = "SCL_EDI";
                message.IsBodyHtml = true;
                message.To.Add("ebis@edi.maersk.com");
                message.To.Add("michael.dhinakaran@maersk.com");
                message.To.Add("docs.ct@krishnapatnamport.com");
                message.CC.Add("sandeep.k@krishnapatnamport.com");
                message.CC.Add("epms_support@krishnapatnamport.com");
                message.CC.Add("it.ct@krishnapatnamport.com");
                message.CC.Add("rajkumar.ss@krishnapatnamport.com");
                if (line == "MSK")
                    message.Body = "Dear Sir, <br/><br/>        PFA - MSK EDI File for Containers Transactions file " + filename + " generated on " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " "
                     + "<br/> <br/>       *** This is an automatically generated email, please do not reply ***<br/> <br/> <br/>Thanks and Regards<br><b>IT Support</b><br>Krishnapatnam Port Co. Ltd.</font><br>Website:www.krishnapatnamport.com</font><br/>";
                if (line == "SCL")
                    message.Body = "Dear Sir, <br/><br/>        PFA - SCL EDI File for Containers Transactions file " + filename + " generated on " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " "
                     + "<br/> <br/>       *** This is an automatically generated email, please do not reply ***<br/> <br/> <br/>Thanks and Regards<br><b>IT Support</b><br>Krishnapatnam Port Co. Ltd.</font><br>Website:www.krishnapatnamport.com</font><br/>";
            }
            else if (line == "HLL")
            {
                //insertEDIDetails(xsubstr, e.Name, e.FullPath);
                message.Subject = "HLL_EDI";

                message.IsBodyHtml = true;
                //message.To.Add("ediham@edihlcl.com");
                message.To.Add("Daniel.Nixon@hlag.com");
                message.To.Add("EDIHAM@EDI.HLCL.COM");
                message.CC.Add("Jayant.Abhyankar@hlag.com");
                message.CC.Add("sandeep.k@krishnapatnamport.com");
                message.CC.Add("epms_support@krishnapatnamport.com");
                message.CC.Add("it.ct@krishnapatnamport.com");
                message.CC.Add("rajkumar.ss@krishnapatnamport.com");
                    message.Body = "Dear Sir, <br/><br/>        PFA - HLL EDI File for Containers Transactions file " + filename + " generated on " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " "
                     + "<br/> <br/>       *** This is an automatically generated email, please do not reply ***<br/> <br/> <br/>Thanks and Regards<br><b>IT Support</b><br>Krishnapatnam Port Co. Ltd.</font><br>Website:www.krishnapatnamport.com</font><br/>";
            }
            else
            {
                //insertEDIDetails("Others", e.Name, e.FullPath);
                message.Subject = "EDI File Created";
                message.IsBodyHtml = true;
                //message.To.Add("docs.ct@krishnapatnamport.com");
                message.To.Add("sandeep.k@krishnapatnamport.com");
                message.CC.Add("epms_support@krishnapatnamport.com");
                message.Body = "Dear Sir <br/><br/>  <br/>" + filename + " in " + filepath + " at " + DateTime.Now.ToLongTimeString() + " auto generated by Sandeep"
                  + "<br/> <br/>Thanks and Regards<br><b>Sandeep-Navis Support</b><br><b>Krishnapatnam Port Container Terminal</font><br>Post Box no 1, Mutthukur Mandal,<br>Dist. SPSR Nellore - 524 344, A.P.<br>website:www.krishnapatnamport.com</font><br/>";

            }
            using (Attachment oAttachedFile = new Attachment(filepath))
                       {
                           message.Attachments.Add(oAttachedFile);
                           smtpClient.Send(message);
                           if (NState != "Xstate")
                                { m_Sb.Remove(0, m_Sb.Length); }
                           else { m_Sb.Append("    #"); }
                           m_Sb.Append(filepath);
                           m_Sb.Append(" ");
                           m_Sb.Append("-Email Sent.");
                           MailSendingStatus = "Success"; 
                           
                       }
                   }
                   catch (Exception ex) 
                   {
                       m_Sb.Remove(0, m_Sb.Length);
                       m_Sb.Append(filepath);
                       m_Sb.Append(" ");
                       m_Sb.Append("    ");
                       m_Sb.Append("-Email Failed.");
                       m_Sb.Append("    ");
                       m_Sb.Append(ex.Message.ToString());
                         string exmsg="Could not find file '"+filepath+"'.";
                       if (ex.Message.ToString() == exmsg)
                       {
                           string Qry = "Update [dbo].[EDIData] set [MailStatus]='FileNotFound' where FileName='" + filename + "'";
                           SqlCommand cmd1 = new SqlCommand(Qry, Sqlcon);
                           cmd1.ExecuteNonQuery();
                       }
                       MailSendingStatus = "Failed";
                   }

            m_Sb.Append("    ");
            m_Sb.Append(DateTime.Now.ToString());
            m_bDirty = true;
            //lstNotification.Items.Add(m_Sb.ToString());
            return MailSendingStatus;
        }
        
        private void frmNotifier_Load(object sender, EventArgs e)
        {
            Sqlcon = new SqlConnection(sqlConnection);
            PreviousExecutiondate = System.DateTime.Now;
        }

        string NState = "Hi";
        DateTime PreviousExecutiondate = System.DateTime.Now;
        public void Checkpendingmails()
        {
            NState = "Hi";
            DateTime firstdate = PreviousExecutiondate;
            DateTime secondDate = System.DateTime.Now;
            TimeSpan diff = secondDate - firstdate;
            double hours = diff.TotalHours;
            if (diff.Hours > 0 && diff.Minutes > 2)
            //if(diff.Hours==0&&diff.Minutes>2)
            {
                NState = "Xstate";
                SendPendingMails();
                PreviousExecutiondate = System.DateTime.Now;
            }
        }
    }
}