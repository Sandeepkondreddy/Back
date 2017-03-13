/*********************************
Author Name:		Sandeep.K
Project Name:		RFID-DMS
Purpose:	        Log Details
Created Date:		09 Nov 2015 
Updated Date:       23 Nov 2015
******************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.IO;

namespace WAP3
{
    class classLog
    {
        #region write it in Log file



        public static void writeLog(string txt)
        {
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string path = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\log.txt";
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
