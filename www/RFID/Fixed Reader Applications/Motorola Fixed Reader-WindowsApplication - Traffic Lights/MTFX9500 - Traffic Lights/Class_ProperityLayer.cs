using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace CS_RFID3_Host_Sample1
{
    public static class Class_ProperityLayer
    {
        public static readonly string OfflineLocalDBType = ConfigurationSettings.AppSettings["LocalDB"];
        public static readonly string OfflineShareDBType = ConfigurationSettings.AppSettings["ShareDB"];
       
        public static readonly string CON_STRING = @"Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=venkat;Data Source=C:\DB\RailOffline.mdb;";
        //public static readonly string SqlConn_String_Internal = "Data Source=172.168.3.88;Initial Catalog=RFID;User ID=sandeep;Password=san789;";       
        //public static readonly string SqlConn_String = "Data Source=172.168.6.239;Initial Catalog=RFID;User ID=sa;Password=123456;";//rfid@kpcl;";
        public static readonly string SqlConn_String_Internal = "Data Source=172.168.3.122;Initial Catalog=RFID;User ID=sandeep;Password=san789;";
        public static readonly string SqlConn_String = "Data Source=172.168.3.122;Initial Catalog=RFID;User ID=sandeep;Password=san789;";
        public static readonly string OLEDBLocalCON_STRING = @"Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=venkat;Data Source=" + OfflineLocalDBType + "RailOffline.mdb;";
        public static readonly string OLEDBShareDBCON_STRING = @"Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=venkat;Data Source=" + OfflineShareDBType + "RailOffline.mdb;";

        private static Control objHomePageControl = null;
        private static int intWorkableAreaTop = 0;
        private static int intWorkableAreaLeft = 0;
        private static int intWorkableAreaHeight = 0;
        private static int intWorkableAreaWidth = 0;


        private static string strConnectionString;
        private static bool blnLoginStatus = false;
        private static string strUserName;
        private static string strUserLocation;
        private static string strOrganizationCode;
        private static string strOrganizationName;
        private static string strSourceLocation;
        private static string strCurrentShipmentNo;

        private static string parkingFleetNo;
        private static string PWBNO;
        private static string PTAGNO;

        private static string CTFleetNo;
        private static string CTWBNO;
        private static string CTTAGNO;

        private static string YardFleetNo;
        private static string YWBNO;
        private static string YTAGNO;

        private static string WHFleetNo;
        private static string WHWBNO;
        private static string WHTAGNO;

        private static string DupTagNo;

        private static int AntNo;

        private static string Ant1and2;
        private static string Ant3and4;

        private static string TaskCode;
        private static string SubTaskCode;
        private static string AllocFlag;

        private static string OperType;
        private static string Location;
        private static string ReaderID;
        private static string ReaderIP;

        public static string TaskCode1
        {
            get { return TaskCode; }
            set { TaskCode = value; }
        }
        public static string SubTaskCode1
        {
            get { return SubTaskCode; }
            set { SubTaskCode = value; }
        }

        public static string AllocFlag1
        {
            get { return AllocFlag; }
            set { AllocFlag = value; }
        }
        public static string OperType1
        {
            get { return OperType; }
            set { OperType = value; }
        }
        public static string ReaderID1
        {
            get { return ReaderID; }
            set { ReaderID = value; }
        }
        public static string ReaderIP1
        {
            get { return ReaderIP; }
            set { ReaderIP = value; }
        }
        public static string Location1
        {
            get { return Location; }
            set { Location = value; }
        }
        public static string Ant3and41
        {
            get { return Ant3and4; }
            set { Ant3and4 = value; }
        }

        public static string Ant1and21
        {
            get { return Ant1and2; }
            set { Ant1and2 = value; }
        }
      
        public static string DupTagNo1
        {
            get { return Class_ProperityLayer.DupTagNo; }
            set { Class_ProperityLayer.DupTagNo = value; }
        }

        public static int AntNo1
        {
            get { return (AntNo); }
            set { AntNo = value; }
        }
        public static string Version = "v 1.0";//04.08.2015 reduce duplicate ittrations//"V1.4";//06.04.2014 reduce duplicate ittrations in tag reading //"V1.3";//03-09-2014

        public static string ParkingFleetNo1
        {
            get { return (parkingFleetNo); }
            set { parkingFleetNo = value; }
        }
        public static string PWBNO1
        {
            get { return (PWBNO); }
            set { PWBNO = value; }
        }
        public static string PTAGNO1
        {
            get { return (PTAGNO); }
            set { PTAGNO = value; }
        }

        public static string CTFleetNo1
        {
            get { return (CTFleetNo); }
            set { CTFleetNo = value; }
        }
        public static string CTWBNO1
        {
            get { return (CTWBNO); }
            set { CTWBNO = value; }
        }
        public static string CTTAGNO1
        {
            get { return (CTTAGNO); }
            set { CTTAGNO = value; }
        }

        public static string YFleetNo1
        {
            get { return (YardFleetNo); }
            set { YardFleetNo = value; }
        }
        public static string YWBNO1
        {
            get { return (YWBNO); }
            set { YWBNO = value; }
        }
        public static string YTAGNO1
        {
            get { return (YTAGNO); }
            set { YTAGNO = value; }
        }

        public static string WHFleetNo1
        {
            get { return (WHFleetNo); }
            set { WHFleetNo = value; }
        }
        public static string WHWBNO1
        {
            get { return (WHWBNO); }
            set { WHWBNO = value; }
        }
        public static string WHTAGNO1
        {
            get { return (WHTAGNO); }
            set { WHTAGNO = value; }
        }

        public static Control PropertyHomePageControl
        {
            get { return (objHomePageControl); }
            set { objHomePageControl = value; }
        }

        public static int PropertyWorkableAreaTop
        {
            get { return (intWorkableAreaTop); }
            set { intWorkableAreaTop = value; }
        }
        public static int PropertyWorkableAreaLeft
        {
            get { return (intWorkableAreaLeft); }
            set { intWorkableAreaLeft = value; }
        }
        public static int PropertyWorkableAreaHeight
        {
            get { return (intWorkableAreaHeight); }
            set { intWorkableAreaHeight = value; }
        }
        public static int PropertyWorkableAreaWidth 
        {
            get { return (intWorkableAreaWidth); }
            set { intWorkableAreaWidth = value; } 
        }


        public static string PropertyConnectionString 
        {
            get { return (strConnectionString); }
            set { strConnectionString = value; }
        }
        public static bool PropertyLoginStatus
        {
            get { return (blnLoginStatus); }
            set { blnLoginStatus = value; }
        }

        public static string PropertyUserName
        {
            get { return (strUserName); }
            set { strUserName = value; }
        }
        public static string PropertyUserLocation
        {
            get { return (strUserLocation); }
            set { strUserLocation = value; }
        }
        public static string PropertyOrganizationCode
        {
            get { return (strOrganizationCode); }
            set { strOrganizationCode = value; }
        }
        public static string PropertyOrganizationName
        {
            get { return (strOrganizationName); }
            set { strOrganizationName = value; }
        }
        public static string PropertySourceLocation
        {
            get { return (strSourceLocation); }
            set { strSourceLocation = value; }
        }
        public static string PropertyCurrentShipmentNo
        {
            get { return (strCurrentShipmentNo); }
            set { strCurrentShipmentNo = value; }
        }


        public static void LoadSubFormsByObject(Form objForm, int frmTop, int frmLeft, int frmHeight, int frmWidth)
        {
            objForm.Top = frmTop;
            objForm.Left = frmLeft;
            objForm.Height = frmHeight;
            objForm.Width = frmWidth;
            
            objForm.TopLevel = false;
            objForm.Parent = Class_ProperityLayer.PropertyHomePageControl;

            objForm.TopMost = true;
            objForm.BringToFront();
            //RoundedRectangleForm(objForm, 10);
            //fm.MdiParent = this;
            objForm.Show();
        }

       

      
    }
}
