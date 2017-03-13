using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace WindowsApp
{
    class classLog
    {
        #region write it in Log file
        public static void writeLog(string txt)
        {
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string path = "D:\\log.txt";
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
