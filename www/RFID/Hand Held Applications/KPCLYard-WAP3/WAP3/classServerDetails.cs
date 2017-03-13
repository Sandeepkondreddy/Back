using System;

using System.Collections.Generic;
using System.Text;

namespace WAP3
{
    class classServerDetails
    {

        private static string globalVar = "";

        public static string ServerIP
        {
            get { return globalVar; }
            set { globalVar = value; }
        }

        private static string WifiServerConnection = "Data Source=172.168.1.44 ;Initial Catalog=RFID;User ID=sandeep;Password=san789;";
        //private static string WifiServerConnection = "Data Source=172.168.3.122 ;Initial Catalog=RFID;User ID=sandeep;Password=san789;";
        //private static string ServerConnection = "Data Source=182.72.244.21,1433;Integrated Security=false;Initial Catalog=RFID;User ID=sa;Password=123456;";

        public static string WifiSQLConnection
        {
            get { return WifiServerConnection; }
            set { WifiServerConnection = value; }
        }

        private static string localdbConnection = "Data Source="+
            (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) +
            "\\localdb.sdf";
        public static string SdfConnection
        {
            get { return localdbConnection; }
            set { localdbConnection = value; }
        }

        //private static string ServerConnection = "Data Source=172.168.1.13 ;Initial Catalog=RFID;User ID=sa;Password=123456;";
        //private static string GPRSServerConnection = "Data Source=182.72.244.30,1433;Integrated Security=false;Initial Catalog=RFID;User ID=sa;Password=123456;";
        private static string GPRSServerConnection = "Data Source=202.83.27.197,1433;Integrated Security=false;Initial Catalog=RFID;User ID=sandeep;Password=san789;";

        public static string GPRSSQLConnection
        {
            get { return GPRSServerConnection; }
            set { GPRSServerConnection = value; }
        }

        //private static string WebLink = @"http://172.168.0.45/rfid-web/";
        private static string WebLink = @"http://google.co.in";
        public static string WebSite
        {
            get { return WebLink; }
            set { WebLink = value; }
        }
    }
}
