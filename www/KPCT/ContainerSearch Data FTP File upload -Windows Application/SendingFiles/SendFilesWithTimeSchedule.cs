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

namespace SendingFiles
{
    public partial class SendFilesWithTimeSchedule : Form
    {
        string ftpServerIP;
        string ftpUserID;
        string ftpPassword;
        string ftpfoldername;
        string FileName;
        public SendFilesWithTimeSchedule()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ftpServerIP = txtServerIP.Text.Trim();
            ftpUserID = txtUsername.Text.Trim();
            ftpPassword = txtPassword.Text.Trim();
            btnFTPSave.Enabled = false;
        }

        private void txtServerIP_TextChanged(object sender, EventArgs e)
        {
            btnFTPSave.Enabled = true;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            btnFTPSave.Enabled = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnFTPSave.Enabled = true;
        }

        private void SendFilesWithTimeSchedule_Load(object sender, EventArgs e)
        {
            ftpServerIP = "www.krishnapatnamport.com";
            ftpUserID = "ctfetch1@krishnapatnamport.com";
            ftpPassword = "kpct123$";
            //ftpUserID = "administrator";
            //ftpPassword = "itkpcl789$";
            ftpfoldername = "ContainerSearch";
            txtServerIP.Text = ftpServerIP;
            txtUsername.Text = ftpUserID.Normalize();
            txtPassword.Text = ftpPassword.Normalize();
            this.Text += ftpServerIP;
            timer2.Start();
        }
        private void Upload(string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + ftpServerIP + "/" + ftpfoldername + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + ftpfoldername + "/" + fileInf.Name));
                      
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
                lblMsg.Text = "File Upload Successfully....";
                lblCopiedTime.Text = DateTime.Now.ToString();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Upload Error");
                lblMsg.Text = ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opFilDlg = new OpenFileDialog();
            if (opFilDlg.ShowDialog() == DialogResult.OK)
            {
                FileName = opFilDlg.FileName;
                //Upload(opFilDlg.FileName);
                //btnStart.Enabled = true;
                //btnStop.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
            Upload(FileName);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            FileName = @"E:\Source\ContainerList.xls";
            //MessageBox.Show("Are you Sure To upload a file " + FileName, "Message", MessageBoxButtons.YesNo);
            DialogResult result = MessageBox.Show("Are you Sure To upload a file:  " + FileName + " ...", "Information", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                lblStartTime.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                Timer MyTimer = new Timer();
                MyTimer.Interval = (2 * 60 * 1000); // 6 hrs : 6 * 60 * 60 * 1000 ;  5mins: 5 * 60 * 1000
                MyTimer.Tick += new EventHandler(timer1_Tick);
                MyTimer.Start();
            }
            else
            {
                timer1.Stop();

            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            lblMsg.Text = "File Copying is stoped...";
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}
