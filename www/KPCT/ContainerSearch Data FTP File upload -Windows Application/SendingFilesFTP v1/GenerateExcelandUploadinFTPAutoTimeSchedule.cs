using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Oracle.DataAccess.Client;
using System.Net.Mail;
using System.Threading;
using System.Configuration;

namespace SendingFiles
{
    public partial class GenerateExcelandUploadinFTPAutoTimeSchedule : Form
    {
        string ftpServerIP;
        string ftpUserID;
        string ftpPassword;
        string ftpfoldername;
        string FileName;

        OracleConnection con;
        string connectionString = null;
        string sql = null;
        string data = null;
        int i = 0;
        int j = 0;
        public static readonly string EPMSPwd = ConfigurationSettings.AppSettings["EPMSPWD"];

        public GenerateExcelandUploadinFTPAutoTimeSchedule()
        {
            InitializeComponent();
        }

        private void GenerateExcelandUploadinFTPAutoTimeSchedule_Load(object sender, EventArgs e)
        {
            ftpServerIP = "www.krishnapatnamport.com";
            ftpUserID = "ctfetch1@krishnapatnamport.com";
            ftpPassword = "kpct123$";
            ftpfoldername = "ContainerSearch";
            txtServerIP.Text = ftpServerIP;
            txtUsername.Text = ftpUserID.Normalize();
            txtPassword.Text = ftpPassword.Normalize();
            //this.Text += "  " + ftpServerIP;
            PresentTimer.Start();
            FileName = @"E:\ContainerSearch\ContainerList.xls";//ContainerList.xls
        }

        private void PresentTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
        }

        private void UploadTimer_Tick(object sender, EventArgs e)
        {
            UploadTimer.Enabled = false;
            UploadTimer.Stop();
            lblErrorMsg.Text = "";
            ExporttoExcel();

            if (System.IO.File.Exists("E:\\ContainerSearch\\ContainerList.xls"))
            {
                Thread.Sleep(1 * 60 * 1000);

                Upload(FileName);
                lblErrorMsg.Text = "";
            }
            UploadTimer.Enabled = true;
            UploadTimer.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //lblStartTime.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
            btnStart.Enabled = false;
            btnStopSending.Enabled = true;
           ExporttoExcel();
           if (System.IO.File.Exists("E:\\ContainerSearch\\ContainerList.xls"))//ContainerList.xls
            {
              
                Upload(FileName);

            }
           UploadTimer.Interval = (4 * 60 * 60 * 1000);//4 hours//(6 * 60 * 60 * 1000);//6 hours
            UploadTimer.Enabled = true;
            UploadTimer.Start();
        }

        private void ExporttoExcel()
        {
            try
            {
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;

                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                connectionString = @"Data Source=NAVISDB;User ID=navis;password=navis;Connection Lifetime=60; pooling=true; Min Pool Size=1; Max Pool Size=10; Incr Pool Size=1; Decr Pool Size=1;";

                con = new OracleConnection(connectionString);
                if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                {
                    con.Open();
                }
                string Query = "SELECT * FROM ContainerSearch";
                OracleDataAdapter dscmd = new OracleDataAdapter(Query, con);
                //DataSet ds = new DataSet();
                //dscmd.Fill(ds);

                DataTable dt = new DataTable();
                dscmd.Fill(dt);

                //int Cnt = Convert.ToInt32(ds.Tables[0].Rows.Count) + 1, k = 0;
                //for (i = 0; i <= Cnt - 1; i++)
                //{
                //    if (i == 0)
                //    {
                //        for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                //        {
                //            xlWorkSheet.Cells[1, j + 1] = ds.Tables[0].Columns[j].ColumnName;
                //        }
                //    }
                //    else
                //    {
                //        for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                //        {
                //            string data = ds.Tables[0].Rows[k].ItemArray[j].ToString();
                //            xlWorkSheet.Cells[i + 1, j + 1] = data;
                //        }
                //        k++;
                //    }
                //}
                int Cnt = Convert.ToInt32(dt.Rows.Count) + 1, k = 0;
                for (i = 0; i <= Cnt - 1; i++)
                {
                    if (i == 0)
                    {
                        for (j = 0; j <= dt.Columns.Count - 1; j++)
                        {
                            xlWorkSheet.Cells[1, j + 1] = dt.Columns[j].ColumnName;
                        }
                    }
                    else
                    {
                        for (j = 0; j <= dt.Columns.Count - 1; j++)
                        {
                            string data = dt.Rows[k].ItemArray[j].ToString();
                            xlWorkSheet.Cells[i + 1, j + 1] = data;
                        }
                        k++;
                    }
                }
                string filepathtime = "E:\\ContainerData\\ContainerList-" + DateTime.Now.ToString("dd-MM-yy HHmm") + ".xls";
                xlWorkBook.SaveAs("E:\\ContainerSearch\\ContainerList.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.SaveAs(filepathtime, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);

                xlApp.Quit();

                releaseObject(xlWorkSheet);

                releaseObject(xlWorkBook);

                releaseObject(xlApp);
                lblReportStatus.Text = "Excel file created.. " + DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message + " : Generate Excel file is Getting Error..";
                SendMail("FAILED: CONTAINER FILE(EXCEL) IS NOT GENERATED DUE TO SOME TECHNICAL PROBLEMS");
            }
        }

        private void releaseObject(object obj)
        {

            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
                lblErrorMsg.Text = "Exception Occured while releasing object " + ex.ToString();

            }
            finally
            {
                GC.Collect();
            }
        }

        private void Upload(string filename)
        {
            try
            {
                if (System.IO.File.Exists("E:\\ContainerSearch\\ContainerList.xls"))//ContainerList.xls
                {
                    btnStopSending.Enabled = false;
                    FileInfo fileInf = new FileInfo(filename);
                    string uri = "ftp://" + ftpServerIP + "/" + ftpfoldername + "/" + fileInf.Name;
                    FtpWebRequest reqFTP;

                    // Create FtpWebRequest object from the Uri provided
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + ftpfoldername + "/" + fileInf.Name));// 

                    // Provide the WebPermission Credintials
                    reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                    // By default KeepAlive is true, where the control connection is not closed
                    // after a command is executed.
                    reqFTP.KeepAlive = false;

                    // Specify the command to be executed.
                    reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

                    // Specify the data transfer type.
                    reqFTP.UseBinary = true;

                    // Notify the server about the size of the uploaded file
                    reqFTP.ContentLength = fileInf.Length;

                    // The buffer size is set to 2kb
                    int buffLength = 2048;
                    byte[] buff = new byte[buffLength];
                    int contentLen;

                    // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                    FileStream fs = fileInf.OpenRead();

                    try
                    {
                        // Stream to which the file to be upload is written
                        Stream strm = reqFTP.GetRequestStream();

                        // Read from the file stream 2kb at a time
                        contentLen = fs.Read(buff, 0, buffLength);

                        // Till Stream content ends
                        while (contentLen != 0)
                        {
                            // Write Content from the file stream to the FTP Upload Stream
                            strm.Write(buff, 0, contentLen);
                            contentLen = fs.Read(buff, 0, buffLength);
                        }

                        // Close the file stream and the Request Stream
                         strm.Close();
                        fs.Close();
                        lblSendingStatus.Text = "File Upload Successfully...";
                        try
                        {
                            System.IO.File.Delete("E:\\ContainerSearch\\ContainerList.xls");
                        }
                        catch (System.IO.IOException e)
                        {
                            lblErrorMsg.Text = e.Message;
                        }
                        //Send Confirmation mail after upload data
                        SendMail("CONTAINER LIST IS UPLOADED SUCCESSFULLY");

                        lblCopiedTime.Text = DateTime.Now.ToString();
                        btnStopSending.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "Upload Error");
                        SendMail("FAILED: CONTAINER LIST NOT UPLOADED DUE TO SOME TECHNICAL PROBLEMS");
                        lblErrorMsg.Text = ex.Message;
                        btnStopSending.Enabled = true;
                        UploadTimer.Enabled = true;
                        UploadTimer.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                SendMail("FAILED: CONTAINER LIST NOT UPLOADED DUE TO SOME TECHNICAL PROBLEMS");
                lblErrorMsg.Text = ex.Message;
                btnStopSending.Enabled = true;
                UploadTimer.Enabled = true;
                UploadTimer.Start();
            }
        }

        private void SendMail(string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();

                mail.From = new MailAddress("epms_support@krishnapatnamport.com");
                mail.To.Add(new MailAddress("epms_support@krishnapatnamport.com"));
                mail.Subject = message;
                mail.Body = "FYKI";

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.office365.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("epms_support@krishnapatnamport.com", EPMSPwd);//ITS@789$

                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Send(mail);
                lblMailStatus.Text = "Mail Send";
                //MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                lblMailStatus.Text = ex.Message + "Mail Sending Error";
                //MessageBox.Show(ex.ToString());
            }
        }

        private void btnStopSending_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStopSending.Enabled = false;
            UploadTimer.Enabled = false;
            UploadTimer.Stop();

        }

    }
}
