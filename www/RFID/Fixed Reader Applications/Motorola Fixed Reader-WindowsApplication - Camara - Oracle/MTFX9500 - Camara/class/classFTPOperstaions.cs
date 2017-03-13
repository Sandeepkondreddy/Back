using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Linq;

namespace CS_RFID3_Host_Sample1
{
	class classFTPOperstaions
	{
        #region "FTP server access with credentials to retrieve the directory list from the server and Download from file from FTP Server""
        public static string GetDirectoryListing(string CamaraFTP_Path, string SaveImage_Path)
        {
            
                FtpWebRequest directoryListRequest = (FtpWebRequest)WebRequest.Create(classFTPConnections._ftpURL.ToString() + CamaraFTP_Path.ToString());
                directoryListRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                directoryListRequest.Credentials = new NetworkCredential(classFTPConnections._UserName, classFTPConnections._Password);
                directoryListRequest.UsePassive = false;
                Thread.Sleep(1000);
            try{
                using (FtpWebResponse directoryListResponse = (FtpWebResponse)directoryListRequest.GetResponse())
                {
                    using (StreamReader directoryListResponseReader = new StreamReader(directoryListResponse.GetResponseStream()))
                    {
                        string responseString = directoryListResponseReader.ReadToEnd();
                        if(responseString=="")
                        {
                            classLog.writeLog("Message: Image Not Captured. Please check the Camara.");
                            return "Image Not Captured";
                        }
                        else
                        {
                            string[] results = responseString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            
                            string filename = results.Last();
                            string oldurl = classFTPConnections._ftpURL.ToString() + CamaraFTP_Path.ToString() + filename.ToString();
                            string newurl = SaveImage_Path.ToString() + System.DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + filename.ToString();
                                                      
                            // Start download from FTP
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(oldurl);

                            request.Method = WebRequestMethods.Ftp.DownloadFile;
                            request.Credentials = new NetworkCredential(classFTPConnections._UserName, classFTPConnections._Password);
                            request.UseBinary = true;
                            request.UsePassive = false;
                            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                            {
                                using (Stream rs = response.GetResponseStream())
                                {
                                    using (FileStream ws = new FileStream(newurl, FileMode.Create))
                                    {
                                        byte[] buffer = new byte[2048];
                                        int bytesRead = rs.Read(buffer, 0, buffer.Length);

                                        while (bytesRead > 0)
                                        {
                                            ws.Write(buffer, 0, bytesRead);
                                            bytesRead = rs.Read(buffer, 0, buffer.Length);
                                        }
                                    }
                                }
                            }
                            // End download from FTP
                            Thread.Sleep(1000);
                            string sts = FileDelete(oldurl);
                            DateTime creation = File.GetCreationTime(@newurl.ToString());
                            DateTime modification = File.GetLastWriteTime(@newurl.ToString());
                            return newurl;
                    }
                    }
                }
            }
            catch (Exception ex)
            {
                classLog.writeLog("Exception: FTP Acccess Issue");
                string newurl = "FTP Access Issue";
                return newurl;
            }
            
        }
        #endregion
        #region "Delete file from FTP"
        public static string FileDelete(string path)
        {
            string status = "";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(classFTPConnections._UserName, classFTPConnections._Password);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Delete status: {0}", response.StatusDescription);
            response.Close();
            return status;
        }
        #endregion
	}
}
