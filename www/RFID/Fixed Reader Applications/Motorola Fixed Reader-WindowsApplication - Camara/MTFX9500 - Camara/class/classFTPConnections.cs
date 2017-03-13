using System;
using System.Collections.Generic;
using System.Text;

namespace CS_RFID3_Host_Sample1
{
	class classFTPConnections
	{
        public static string _ftpURL = "ftp://172.168.8.60";                  //Host URL or address of the FTP server
        public static string _UserName = "Axis";                              //User Name of the FTP server
        public static string _Password = "axis";                              //Password of the FTP server
        public static string _ftpDirectory = "//Entry Lane - 01/";            //The directory in FTP server where the files are present
        public static string _LocalDirectory = "D:\\ContainerData\\";         //Local directory where the files will be downloaded
	}
}
