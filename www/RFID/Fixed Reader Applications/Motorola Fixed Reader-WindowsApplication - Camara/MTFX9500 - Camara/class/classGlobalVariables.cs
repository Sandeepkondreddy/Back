using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CS_RFID3_Host_Sample1
{
	class classGlobalVariables
	{
        private static string AntenaSet1_Loc = ConfigurationSettings.AppSettings["AntenaSet1Loc"];
        private static string AntenaSet2_Loc = ConfigurationSettings.AppSettings["AntenaSet2Loc"];
        private static string Reader_IP = ConfigurationSettings.AppSettings["ReaderIPAddress"];
        private static string Reader_Port = ConfigurationSettings.AppSettings["ReaderPort"];
        private static string AntenaSet1_OldTag;
        private static string AntenaSet2_OldTag;

        public static string AntenaSet1Loc
        {
            get { return AntenaSet1_Loc; }                                          
            set { AntenaSet1_Loc = value; }
        }
        public static string AntenaSet2Loc
        {
            get { return AntenaSet2_Loc; }
            set { AntenaSet2_Loc = value; }
        }
        public static string RaederIP
        {
            get { return Reader_IP; }
            set { Reader_IP = value; }
        }
        public static string ReaderPort
        {
            get { return Reader_Port; }
            set { Reader_Port = value; }
        }
        public static string AntenaSet1OldTag
        {
            get { return AntenaSet1_OldTag; }
            set { AntenaSet1_OldTag = value; }
        }
        public static string AntenaSet2OldTag
        {
            get { return AntenaSet2_OldTag; }
            set { AntenaSet2_OldTag = value; }
        }

        public static int AntenaSet1Loc_Camara_GPIO_PortNo = Convert.ToInt32(ConfigurationSettings.AppSettings["AntenaSet1Loc_Camara_GPIO_PortNo"]);
        public static string AntenaSet1Loc_CamaraFTP_Path = ConfigurationSettings.AppSettings["AntenaSet1Loc_CamaraFTP_Path"];
        public static string AntenaSet1Loc_SaveImage_Path = ConfigurationSettings.AppSettings["AntenaSet1Loc_SaveImage_Path"];
        public static int AntenaSet1Loc_BoomBarrier_GPIO_PortNo = Convert.ToInt32(ConfigurationSettings.AppSettings["AntenaSet1Loc_BoomBarrier_GPIO_PortNo"]);

        public static int AntenaSet2Loc_Camara_GPIO_PortNo = Convert.ToInt32(ConfigurationSettings.AppSettings["AntenaSet2Loc_Camara_GPIO_PortNo"]);
        public static string AntenaSet2Loc_CamaraFTP_Path = ConfigurationSettings.AppSettings["AntenaSet2Loc_CamaraFTP_Path"];
        public static string AntenaSet2Loc_SaveImage_Path = ConfigurationSettings.AppSettings["AntenaSet2Loc_SaveImage_Path"];
        public static int AntenaSet2Loc_BoomBarrier_GPIO_PortNo = Convert.ToInt32(ConfigurationSettings.AppSettings["AntenaSet2Loc_BoomBarrier_GPIO_PortNo"]);
	}
}
