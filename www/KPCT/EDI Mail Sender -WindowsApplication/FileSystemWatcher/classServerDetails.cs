using System;
using System.Collections.Generic;
using System.Text;

namespace FileChangeNotifier
{
    class classServerDetails
    {
        private static string ServerConnection = "Data Source=172.168.3.88 ;Initial Catalog=RFID;User ID=sandeep;Password=san789;";

        public static string SQLConnection
        {
            get { return ServerConnection; }
            set { ServerConnection = value; }
        }
    }
}
