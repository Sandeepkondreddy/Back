//****************************************************************************
//
//	Simple RFID Console Sample Code For IF2 Plus
//
//	Author: Jim Peternel 2014
//
//	Copyright 2014 Intermec Technologies Corp.
//
//  The purpose of this code is to show how to create a console application to
//  run in the IF2+
//
//
//  To recompile this sample code you should download an install the IDL RFID resource kit from
//  the web site: www.intermec.com, or more specifically at:
//  http://www.intermec.com/support/downloads/search.aspx?categoryid=11&familyid=20&productnodeid=DEVRESOURCEKIT
//
//
//  Sample Code version 1.03
//
//
//****************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Intermec.DataCollection.RFID;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace SimpleConsoleApplication
{
    class Program
    {
        private static bool bUseDebug = false;
       
        static void Main(string[] args)
        {
            myAppsThread oAlpha = new myAppsThread();

            // Create the thread object, passing in the Alpha.Beta method
            // via a ThreadStart delegate. This does not start the thread.
            Thread oThread = new Thread(new ThreadStart(oAlpha.RunApp));

            //set source code version for display
            oAlpha.sVersion = "7.01";

            //turn on debug messages written to the console
            oAlpha.bDebug = bUseDebug;

            System.Console.WriteLine("Intermec Fixed Reader");

            // Start the thread
            oThread.Start();

            //wait till thread starts up
            while (!oThread.IsAlive)
                Thread.Sleep(1000);

            //run until user sends KILL command via serial port
            while (!oAlpha.bKill)
                Thread.Sleep(1000);

            System.Console.WriteLine("Console Application Closing...");

            // Request that oThread be stopped
            oThread.Abort();

            // Wait until oThread finishes. Join also has overloads
            // that take a millisecond interval or a TimeSpan object.
            oThread.Join();

            System.Console.WriteLine("Goodbye");
        }

        public class myAppsThread
        {
            public bool bDebug = false;
            public bool bKill = false;
            public bool AppIsRunning = false;
            public string sVersion = "";

            RFIDReaderClass MyReader = new RFIDReaderClass();
            ftp myftp = null;

            public void RunApp()
            {
                bool bStatus = false;
                bool bUseSerialPort = false;
                //bool bFTPCrashed = false;

                MyReader.bDebug = bDebug;

                bKill = false;

                //*************************************************************
                //test code you can ignore this
                //TCPClientClass jp = new TCPClientClass();
                //jp.ConnectToTCPServerTestMessage("10.10.10.7", 80);
                //jp = null;
                //*************************************************************

                //connection for talking to BRI server via the IDL driver
                //string sURL = "TCP://" + "127.0.0.1" + ":2189";  //localhost connection in IF61/IF2+.
                //string sURL = "TCP://" + "192.168.0.20" + ":2189";   //when I'm running code on my PC
                string sURL = "TCP://" + "172.168.5.220" + ":2189";   //when I'm running code on my PC
                bUseSerialPort = true;
                bStatus = OpenRFIDReader(sURL, bUseSerialPort, false);
                
                if (bStatus)
                {
                    System.Console.WriteLine("reader open and ready");

                    LoadSettings();
              
                    AppIsRunning = true;

                    SetMode();
                    
                    while (AppIsRunning)
                    {
                        //just sleep
                        Thread.Sleep(1000);
                    }
                }

                if (MyReader != null) { MyReader.CloseReader(); MyReader = null; }
                bKill = true;
            }

            private void SetMode()
            {
                if (MyReader.myRdrOpts.sMode.Equals("IMMEDIATE"))
                {
                    MyReader.Read_Immediate();



                }
                else if (MyReader.myRdrOpts.sMode.Equals("PERIODIC"))
                {
                    MyReader.Read_Periodic();
                }
                else if (MyReader.myRdrOpts.sMode.Equals("TAG"))
                {
                    MyReader.Read_Tag();
                }
                else if (MyReader.myRdrOpts.sMode.Equals("GPI"))
                {
                    //for gpi mode the read starts and stops with trigger events
                    //so there is nothing to do but wait for those events.

                    if (!MyReader.myRdrOpts.bStartReadTriggerFound)
                    {
                        System.Console.WriteLine("*** NO START TRIGGER DEFINED...Aborting");
                        AppIsRunning = false;
                    }

                    if (!MyReader.myRdrOpts.bStopReadTriggerFound)
                    {
                        System.Console.WriteLine("*** NO STOP TRIGGER DEFINED.");
                        System.Console.WriteLine("*** Using duration.");
                    }
                }
            }

            private bool PingFTServer()
            {
                string s = null;
                string result;
                bool b = false;
                int x = 0;

                //There is a bug in Mono that will crash the app if you attempt to ftp to 
                //a server that is offline (or network goes down).  You cannot recover from 
                //this failure.  Once the network comes back online the app will crash on the next 
                //ftp attempt.  Its a mono bug.  Ping the ftp server first to make sure it is available.

                //You cannot use the Mono ping class to ping from the IF2 reader.  It will always fail because
                //of Linux permissions.  You must call the ping.exe instead.

                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("ping", MyReader.myFTP.ftpIPADDR + " -w 3");

                procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                // The following commands are needed to redirect the standard output.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Get the ping responses
                try
                {
                    while ((result = proc.StandardOutput.ReadLine()) != null)
                    {
                        s = result.ToUpper().Trim();
                        System.Console.WriteLine("->>>" + s);

                        //ping on PC responds differently from ping command on IF2 (Windows vs. Linux)
                        //->>>REPLY FROM 10.10.10.7....
                        //->>>64 BYTES FROM 10.10.10.7: SEQ=0 TTL=127 TIME=2.754 MS

                        if (s.IndexOf("BYTES FROM " + MyReader.myFTP.ftpIPADDR) >= 0
                            || s.IndexOf("REPLY FROM " + MyReader.myFTP.ftpIPADDR) >= 0)
                        {
                            System.Console.WriteLine("**************** Ping SUCCESS ******************");
                            b = true;
                            break;
                        }
                        x++;
                        if (x > 3)
                            break;
                    }
                }
                catch (Exception ee)
                {
                    System.Console.WriteLine("->>>>>Ping Exception: " + ee.Message);
                }

                proc.Close();
                //proc.WaitForExit();
                proc.Dispose();
                proc = null;

                if (!b)
                {
                    System.Console.WriteLine("************* Ping FAILED and completed ******************");
                    System.Console.WriteLine("************* Ping FAILED and completed ******************");
                    System.Console.WriteLine("************* Ping FAILED and completed ******************");
                    System.Console.WriteLine("************* Ping FAILED and completed ******************");
                    System.Console.WriteLine("************* Ping FAILED and completed ******************");
                }
                else
                    System.Console.WriteLine("->>>>>Ping completed");
              
                return b;
            }

            private string GetMyTimeNow()
            {
                string tMyTimeNow = null;

                int tHR = DateTime.Now.Hour;
                int tMN = DateTime.Now.Minute;
                int tSEC = DateTime.Now.Second;
                int tmSec = DateTime.Now.Millisecond;

                //to YYYYMMDD-HHMM

                tMyTimeNow = tHR.ToString().PadLeft(2, '0') + "_" + tMN.ToString().PadLeft(2, '0') + "_" + tSEC.ToString().PadLeft(2, '0');

                return tMyTimeNow;
            }

            private string GetMyDateNow()
            {
                string tMyDateNow = null;

                int tMO = DateTime.Now.Month;
                int tDAY = DateTime.Now.Day;
                int tYR = DateTime.Now.Year;

                //to YYYYMMDD-HHMM 

                tMyDateNow = tYR.ToString() + "_" + tMO.ToString().PadLeft(2, '0') + "_" + tDAY.ToString().PadLeft(2, '0');

                return tMyDateNow;
            }

            private bool FTPDataFile()
            {
                bool bFailed = false;
                string sfile = null;
                string s = GetMyDateNow() + "_" + GetMyTimeNow();
                sfile = "TagDataLog_" + s + ".txt";

                try
                {
                    System.Console.WriteLine("starting ftp upload....");
                    if (myftp.upload("/" + sfile, "TagDataLog.txt"))
                    {
                        System.Console.WriteLine("ftp upload completed....");
                        File.Delete("TagDataLog.txt");
                        System.Console.WriteLine("File deleted....");
                        MyReader.iCycleCount = 0;
                    }
                    else
                    {
                        bFailed = true;
                        System.Console.WriteLine("File NOT deleted....");
                    }
                }
                catch (Exception ee)
                {
                    bFailed = true;
                    System.Console.WriteLine("ftp exception->" + ee.Message);
                }
                System.Console.WriteLine("ftp test done");
                return bFailed;
            }

            private void SetAttribsAndTriggers(string s)
            {
                bool b = false;

                b = MyReader.SendCMD(s);
                if (!b)
                    System.Console.WriteLine("Error setting attrib/trigger: " + s);
                else
                {
                    if (s.StartsWith("TRIGGER"))
                    {
                        if (s.IndexOf("STARTREAD") >= 0)
                            MyReader.myRdrOpts.bStartReadTriggerFound = true;
                        else if (s.IndexOf("STOPREAD") >= 0)
                            MyReader.myRdrOpts.bStopReadTriggerFound = true;
                        else
                            System.Console.WriteLine("Unknown trigger def: " + s);
                    }
                }
            }

            private void ParseSettingField(string s, string sInput)
            {
                if (s.StartsWith("FTP_IPADDR"))
                {
                    MyReader.myFTP.ftpIPADDR = s.Replace("FTP_IPADDR=", "").Trim();
                }
                else if (s.StartsWith("FTP_PORT"))
                {
                    MyReader.myFTP.ftpPort = s.Replace("FTP_PORT=", "").Trim();
                }
                else if (s.StartsWith("FTP_USER"))
                {
                    int i = s.IndexOf("=") + 1;
                    MyReader.myFTP.ftpUser = sInput.Substring(i);
                    System.Console.WriteLine("MyReader.ftpUser=" + MyReader.myFTP.ftpUser);
                }
                else if (s.StartsWith("FTP_PASS"))
                {
                    int i = s.IndexOf("=") + 1;
                    MyReader.myFTP.ftpPass = sInput.Substring(i);
                    System.Console.WriteLine("MyReader.ftpPass=" + MyReader.myFTP.ftpUser);
                }
                else if (s.StartsWith("FTP_ENABLE"))
                {
                    if (s.Replace("FTP_ENABLE=", "").Trim().Equals("TRUE"))
                    {
                        MyReader.myFTP.ftpEnable = true;
                        System.Console.WriteLine("MyReader.myFTP.ftpEnable = true");
                    }
                    else
                    {
                        MyReader.myFTP.ftpEnable = false;
                        System.Console.WriteLine("MyReader.myFTP.ftpEnable = false");
                    }
                }
                else if (s.StartsWith("FIELD"))
                {
                    MyReader.myRdrOpts.sFieldSchema = s.Replace("FIELD=", "").Trim().Replace(",", " ");
                }
                else if (s.StartsWith("FILTER"))
                {
                    //Nexus cards use the WHTI compliant GDTI-96 standard for its RFID tags.  This data tag is 96 bits long, 
                    //which translates into a 24 character hexadecimal string.  The first 55 bits of all Nexus cards is always 
                    //the same, like so:
                    //0010,1100,0010,1000,0011,0101,0011,0111,1011,0011,1111,1010,0000,001
                    //Translated to hexadecimal it is either 2c283537b3fa02 or 2c283537b3fa03 depending on the value of the 56th bit.

                    MyReader.myRdrOpts.sFilterSchema = s.Replace("FILTER=", "").Trim();
                    if (s.IndexOf("YES") >= 0)
                        MyReader.myRdrOpts.sFilterSchema = "bit(1:32,55)=b0010110000101000001101010011011110110011111110100000001";
                }
                else if (s.StartsWith("REPORT"))
                {
                    MyReader.myRdrOpts.sReportMode = s.Replace("REPORT=", "").Trim();
                }
                else if (s.StartsWith("POLLINTERVAL"))
                {
                    MyReader.myRdrOpts.iPollInterval = Convert.ToInt16(s.Replace("POLLINTERVAL=", "").Trim());
                }
                else if (s.StartsWith("PERIOD"))
                {
                    MyReader.myRdrOpts.iPeriod = Convert.ToInt16(s.Replace("PERIOD=", "").Trim());
                }
                else if (s.StartsWith("DURATION"))
                {
                    MyReader.myRdrOpts.iDuration = Convert.ToInt16(s.Replace("DURATION=", "").Trim());
                }
                else if (s.StartsWith("MODE"))
                {
                    MyReader.myRdrOpts.sMode = s.Replace("MODE=", "").Trim();
                    //GPI
                    //Immediate
                    //periodic
                    //till tag found...TAG
                }
                else if (s.StartsWith("LIGHT_OFF"))
                {
                    MyReader.myRdrOpts.Light_Off = s.Replace("LIGHT_OFF=", "").Trim();
                }
                else if (s.StartsWith("LIGHT_ON_READING"))
                {
                    MyReader.myRdrOpts.Light_On_Reading = s.Replace("LIGHT_ON_READING=", "").Trim();
                }
                else if (s.StartsWith("LIGHT_ON_TAGS_FOUND"))
                {
                    MyReader.myRdrOpts.Light_On_TagsFound = s.Replace("LIGHT_ON_TAGS_FOUND=", "").Trim();
                }
                else if (s.StartsWith("LIGHT_ON_ERROR"))
                {
                    MyReader.myRdrOpts.Light_On_Error = s.Replace("LIGHT_ON_ERROR=", "").Trim();
                }
                else if (s.StartsWith("LIGHT_ON_STANDBY"))
                {
                    MyReader.myRdrOpts.Light_On_Standy = s.Replace("LIGHT_ON_STANDBY=", "").Trim();
                }
                else if (s.StartsWith("GREEN"))
                {
                    MyReader.myRdrOpts.LED_Green = s.Replace("GREEN=", "").Trim();
                }
                else if (s.StartsWith("YELLOW"))
                {
                    MyReader.myRdrOpts.LED_Yellow = s.Replace("YELLOW=", "").Trim();
                }
                else if (s.StartsWith("RED"))
                {
                    //MyReader.myRdrOpts.LED_Red = s.Replace("RED=", "").Trim();
                }
                else if (s.StartsWith("OFF"))
                {
                    MyReader.myRdrOpts.LED_Off = s.Replace("OFF=", "").Trim();
                }
                else if (s.StartsWith("GATE_DELAY"))
                {
                    MyReader.myRdrOpts.Gate_Delay = s.Replace("GATE_DELAY=", "").Trim();
                }
                else if (s.StartsWith("LOG_TAGS"))
                {
                    s = s.Replace("LOG_TAGS=", "").Trim();
                    if (s.Equals("TRUE"))
                        MyReader.myRdrOpts.Log_Tags = true;
                    else
                        MyReader.myRdrOpts.Log_Tags = false;
                }
            }

            private void LoadSettings()
            {
                string s = null;

                MyReader.myRdrOpts.iFieldCount = 0;

                if (File.Exists("settings.txt"))
                {
                    string sInput = null;
                    FileStream fs = new FileStream("settings.txt", FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    while (!sr.EndOfStream)
                    {
                        sInput = sr.ReadLine().Trim();
                        s = sInput.ToUpper();
                        System.Console.WriteLine(sInput);
                        if (!sInput.StartsWith(";"))
                        {
                            if (s.StartsWith("ATTRIB") || s.StartsWith("TRIGGER"))
                            {
                                SetAttribsAndTriggers(s);
                            }
                            else
                            {
                                ParseSettingField(s, sInput);
                            }
                        }
                    }

                    if (MyReader.myRdrOpts.sFieldSchema != null)
                        MyReader.BuildFieldNameList();
                    
                    sr.Close();
                    sr = null;
                    fs.Close();
                    fs = null;
                }
                else
                {
                    System.Console.WriteLine("settings file not found!");
                }
            }

            public bool OpenRFIDReader(string sURL, bool bUseSerialPort, bool bEnableIDLDebug)
            {
                bool bStatus = false;

                //two methods for opening a connection to a reader.
                //One option opens the reader with IDL debug logging.
                //and the second method opens reader with no IDL debug logging.                
                if (bEnableIDLDebug)                    
                    bStatus = MyReader.CreateReaderWithIDLDebugging(sURL);                     
                else
                    bStatus = MyReader.CreateReaderWithNoIDLDebugging(sURL);  //no idl debugging
               
                if (!bStatus)
                {
                    System.Console.WriteLine("Console Application failed to open reader and is aborting");
                    MyReader = null;
                    bStatus = false;
                }
                else
                {
                    if (bUseSerialPort)
                    {
                        System.Console.WriteLine("serial port code disabled");
                        //if you want to send data out the serial port use this:
                        bUseSerialPort = MyReader.OpenSerialPort();
                    }
                }
                return bStatus;
            }

            //private void RunFtpTestCode()
            //{
            //    //test code you can ignore this
            //    Thread.Sleep(2000);
            //    MyReader.StartReadingTags();
            //    Thread.Sleep(2000);
            //    MyReader.StopReadingTags();
            //    if (!bFTPCrashed)
            //    {
            //        if (PingFTServer())
            //        {
            //            System.Console.WriteLine("staring ftp....");
            //            try
            //            {
            //                myftp = null;
            //                myftp = new ftp("ftp://" + MyReader.myFTP.ftpIPADDR, MyReader.myFTP.ftpUser, MyReader.myFTP.ftpPass);
            //                System.Console.WriteLine("ftp class created....");
            //                bFTPCrashed = FTPDataFile();
            //                myftp = null;
            //                System.Console.WriteLine("ftp class disposed");
            //            }
            //            catch (Exception ee)
            //            {
            //                bFTPCrashed = true;
            //                System.Console.WriteLine("# ftp exception: " + ee.Message);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        System.Console.WriteLine("********** ftp crashed saving data locally *******************");
            //    }
            //}

            //private bool TCPClientTest()
            //{
            //    //test code you can ignore this
            //
            //    bool b = false;
            //    System.Console.WriteLine("starting tpc test...");
            //    TCPClientClass jp = new TCPClientClass();
            //    b = jp.ConnectToTCPServerTestMessage("10.10.10.7", 80);
            //    if (b)
            //        System.Console.WriteLine("tcp success");
            //    else
            //        System.Console.WriteLine("***************** tcp failed **********************");
            //    jp = null;
            //    return b;
            //}

        }//END public class myAppsThread
    }//END class Program
}//END namespace SimpleConsoleApplication

