using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace ThingMagic.URA2.BL
{
    class ServerConnections
    {
        //public static readonly string SqlConn_String_Internal = "Data Source=172.168.3.88;Initial Catalog=RFID;User ID=sandeep;Password=san789;";
        //public static readonly string SqlConn_String_Road = "Data Source=172.168.6.239;Initial Catalog=RFID;User ID=sa;Password=123456;";//rfid@kpcl;";
        public static readonly string SqlConn_String_Internal = "Data Source=172.168.1.44;Initial Catalog=RFID;User ID=sandeep;Password=san789;";
        public static readonly string SqlConn_String_Road = "Data Source=172.168.1.44;Initial Catalog=RFID;User ID=sandeep;Password=san789;";
        private static string Reader_IP= ConfigurationSettings.AppSettings["readerIP"];
        public static string ReaderIP
        {
            get { return (Reader_IP); }
            set { Reader_IP = value; }
        }

        private static string Status_Details;
        public static string Status
        {
            get { return (Status_Details); }
            set { Status_Details = value; }
        }

        public static string Loc1= ConfigurationSettings.AppSettings["ant1-WBNo"];
        public static string Loc2 = ConfigurationSettings.AppSettings["ant2-WBNo"];
        
    }
}
