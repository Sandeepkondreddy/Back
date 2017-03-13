/*********************************
Author Name:		Sandeep.K
Project Name:		RFID
Purpose:	        Log Details
Created Date:		21 Nov 2014
Updated Date:       14 Feb 2015
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
                    tw.WriteLine(datet+" ::"+txt.ToString() );
                    tw.Close();
                }

            }

            else if (File.Exists(path))
            {
                using (System.IO.StreamWriter fwrite = new System.IO.StreamWriter(path, true))
                {
                    try
                    {
                        fwrite.WriteLine(datet + " ::" + txt.ToString() );
                    }
                    finally
                    {
                        fwrite.Close();
                    }
                    
                }
            }
        }
        #endregion


        #region write it in TruckMaster Sync Log file
        public static void TruckMasterSyncLog(string txt, string filename)
        {
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string fname = "TruckMasterSyncLog" + datet.Replace("-", "").Replace(":", "") + ".txt";
            string path = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\Log\\" + filename + "";
            string dirpath = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\Log";
            if (!File.Exists(path))
            {
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);

                }
                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(txt.ToString() + " : " + datet);
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                using (System.IO.StreamWriter fwrite = new System.IO.StreamWriter(path, true))
                {
                    try
                    {
                        fwrite.WriteLine(txt.ToString() + " : " + datet);
                    }
                    finally
                    {
                        fwrite.Close();
                    }
                }
            }
        }
        #endregion

        #region write it in UserMaster Sync Log file
        public static void UserMasterSyncLog(string txt, string filename)
        {
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string fname = "UserMasterSyncLog" + datet.Replace("-", "").Replace(":", "") + ".txt";
            string path = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\Log\\" + filename + "";
            string dirpath = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\Log";
            if (!File.Exists(path))
            {
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);

                }
                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(txt.ToString() + " : " + datet);
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                using (System.IO.StreamWriter fwrite = new System.IO.StreamWriter(path, true))
                {
                    try
                    {
                        fwrite.WriteLine(txt.ToString() + " : " + datet);
                    }
                    finally
                    {
                        fwrite.Close();
                    }
                }
            }
        }
        #endregion

        #region write it in Task Sync Log file
        public static void TaskSyncLog(string txt,string filename)
        {
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string fname = "TaskSyncLog" + datet.Replace("-", "").Replace(":", "") + ".txt";
            string path = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\Log\\" + filename + "";
            string dirpath = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\Log";
            if (!File.Exists(path))
            {
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);

                }
                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(txt.ToString() + " : " + datet);
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                using (System.IO.StreamWriter fwrite = new System.IO.StreamWriter(path, true))
                {
                    try
                    {
                        fwrite.WriteLine(txt.ToString() + " : " + datet);
                    }
                    finally
                    {
                        fwrite.Close();
                    }
                }
            }
        }
        #endregion

        #region write it in DataUpload Sync Log file
        public static void DataSyncLog(string txt, string filename)
        {
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string fname = "DataSyncLog" + datet.Replace("-", "").Replace(":", "") + ".txt";
            string path = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\Log\\" + filename + "";
            string dirpath = "" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\Log";
            if (!File.Exists(path))
            {
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);

                }
                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(txt.ToString() + " : " + datet);
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                using (System.IO.StreamWriter fwrite = new System.IO.StreamWriter(path, true))
                {
                    try
                    {
                        fwrite.WriteLine(txt.ToString() + " : " + datet);
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
