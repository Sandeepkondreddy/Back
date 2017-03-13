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
    public partial class SendingAnyFile : Form
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
        public static string SendFilename = ConfigurationSettings.AppSettings["FileName"];
        public SendingAnyFile()
        {
            InitializeComponent();
        }

        private void SendingAnyFile_Load(object sender, EventArgs e)
        {

            ftpServerIP = "www.krishnapatnamport.com";
            ftpUserID = "ctfetch1@krishnapatnamport.com";
            ftpPassword = "kpct123$";
            ftpfoldername = "ContainerSearch";
            FileName = @"E:\ContainerSearch\" + SendFilename;//ContainerList.xls
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            if (System.IO.File.Exists("E:\\ContainerSearch\\"+SendFilename))//ContainerList.xls
            {
                Upload(FileName);
            }
            btnStart.Enabled = true;
        }
        private void Upload(string filename)
        {
            try
            {
                if (System.IO.File.Exists("E:\\ContainerSearch\\"+SendFilename))//ContainerList.xls
                {
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
                        //Send Confirmation mail after upload data
                        //SendMail("CONTAINER LIST IS UPLOADED SUCCESSFULLY");
                        MessageBox.Show("CONTAINER LIST IS UPLOADED SUCCESSFULLY");

                        lblCopiedTime.Text = DateTime.Now.ToString();
                        // btnStopSending.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        //SendMail("FAILED: CONTAINER LIST NOT UPLOADED DUE TO SOME TECHNICAL PROBLEMS");
                        MessageBox.Show("FAILED: FILE IS NOT UPLOADED DUE TO SOME TECHNICAL PROBLEMS");
                        lblErrorMsg.Text = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                //SendMail("FAILED: CONTAINER LIST NOT UPLOADED DUE TO SOME TECHNICAL PROBLEMS");
                lblErrorMsg.Text = ex.Message;
            }
        }

    }
}
