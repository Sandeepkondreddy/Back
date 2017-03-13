/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication- Log Details
Created Date:		07 Sep 2015 
Updated Date:		09 Dec 2015  
******************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RFID_WebService
{
    class classLog
    {
        #region write it in Log file

        public static void writeLog(string txt)
        {
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string path = "C:\\Test\\log.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(datet + " ::" + txt.ToString());
                    tw.Close();
                }

            }

            else if (File.Exists(path))
            {
                using (System.IO.StreamWriter fwrite = new System.IO.StreamWriter(path, true))
                {
                    try
                    {
                        fwrite.WriteLine(datet + " ::" + txt.ToString());
                    }
                    finally
                    {
                        fwrite.Close();
                    }

                }
            }
        }
        #endregion

    }
}