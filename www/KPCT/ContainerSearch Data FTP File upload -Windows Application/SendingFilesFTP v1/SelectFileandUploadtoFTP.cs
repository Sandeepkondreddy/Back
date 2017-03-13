using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Security;

namespace SendingFiles
{
    public partial class SelectFileandUploadtoFTP : Form
    {
        string ftpServerIP;
        string ftpUserID;
        string ftpPassword;
        string ftpfoldername;
        public SelectFileandUploadtoFTP()
        {
            InitializeComponent();
        }

        private void SendingToFTP_Load(object sender, EventArgs e)
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
        }
        private void Upload(string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + ftpServerIP + "/" + ftpfoldername + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + ftpfoldername + "/" + fileInf.Name));
            
            //FileInfo fileInf = new FileInfo(filename);
            //string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            //FtpWebRequest reqFTP;

            //// Create FtpWebRequest object from the Uri provided
            //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileInf.Name));

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
                lblMessage.Text = "File Upload Successfully....";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upload Error");
            }
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opFilDlg = new OpenFileDialog();
            if (opFilDlg.ShowDialog() == DialogResult.OK)
            {
                Upload(opFilDlg.FileName);
            }

        }

        private void txtServerIP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
