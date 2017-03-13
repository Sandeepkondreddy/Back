using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Intermec.DataCollection.RFID;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;



namespace SimpleConsoleApplication
{
    class RFIDReaderClass
    {
        private bool bReadTillTagFound = false;
        private int iCurrentTagCount = 0;
        private bool bFTPCrashed = false;
        private string[] sBackUpData = new string[1];
        private int iBackupCount = 0;

        private bool bUseSerialPort = false;
        public bool bPollForTags = false;
        public bool bDebug = true;

        public Int64 iTagCount = 0;
        public string[] TagIDList = new string[500];
        public Int64[] TagCountList = new Int64[500];
        public Int64 iCycleCount = 0;
        public static SqlConnection Sqlcon = new SqlConnection("Data Source=172.168.3.122;Initial Catalog=RFID;User ID=sa;Password=123456;connection timeout=30");
        public string WBNO = ConfigurationSettings.AppSettings["WBIN"];
        public string newtag = string.Empty;
        public string newtag1 = string.Empty;
        public string TruckNo = string.Empty;
        public int errorflag = 0;
        public int TruckFlag = 0;
        public int AntID;
        public string ReaderIP = string.Empty;
        SqlCommand SqlInstcmd;
        public bool outParamIsValid = false;
        public string ReadTime = string.Empty;
        public string DBRoadTime = string.Empty;
        public string DBFleetTime = string.Empty;
        //public ArrayList MyTagCollection = new ArrayList();

        private BRIReader brdr = null;

        SerialPortClass mySerialPort = null;

        // worker thread
        Thread m_WorkerThread_SerialPort;
        Thread m_WorkerThread_Immediate;
        Thread m_WorkerThread_Periodic;
        Thread m_WorkerThread_Tag;
        Thread m_WorkerThread_GPI;

        //my Test
        string myEPC=null;

        public class ReadOptions
        {
            public string sFieldSchema = null;
            public string sFilterSchema = null;

            public bool Log_Tags = false;
            public int iPeriod = 0;
            public int iDuration = 0;
            public string[] sFieldNameList = new string[10];
            public int iFieldCount = 0;
            public string sReportMode = null;
            public bool bStartReadTriggerFound = false;
            public bool bStopReadTriggerFound = false;
            public int iPollInterval = 5000;
            public string sMode = null;
            //GPI
            //Immediate
            //Periodic
            //Tag

            //these should be set to ON/OFF
            public string Light_On_Reading = null;
            public string Light_On_TagsFound = null;
            public string Light_On_Error = null;
            public string Light_On_Standy = null;
            public string Light_Off = null;
            public string Gate_Delay = null;//not used

            //values for GPO
            public string LED_Off = "15";
            public string LED_Green = "15";
            public string LED_Yellow = "15";
            public string LED_Red = "15";
        }
        public ReadOptions myRdrOpts = new ReadOptions();
        
        private class TagFields
        {
            public string sANT = null;
            public string sRSSI = null;
            public string sTime = null;
            public string sCount = null;

            public string sRNSI = null;
            public string sSNR = null;
            public string sTAGID = null;
            public string sTagType = null;
            public string sField1 = null;
            public string sField2 = null;
        }

        public class MyFTP
        {
            public string ftpIPADDR = "10.10.10.7";
            public string ftpPort = "";
            public string ftpUser = null;
            public string ftpPass = null;
            public bool ftpEnable = false;
        }
        public MyFTP myFTP = new MyFTP();

        public bool OpenSerialPort()
        {
            //**************************************************************
            //this code requires FW 2.0.1200.1-14:46 or newer
            //**************************************************************
            
            //this code is not used by the Store and Forward demo application

            mySerialPort = new SerialPortClass();
            bUseSerialPort = mySerialPort.ConnectToSerialPort();

            if (bUseSerialPort)
            {
                // create worker thread instance
                m_WorkerThread_SerialPort = new Thread(new ThreadStart(this.WorkerThreadFunction_SerialPort));
                m_WorkerThread_SerialPort.Name = "Worker Thread - serial port";   // looks nice in Output window
                m_WorkerThread_SerialPort.IsBackground = true;
                m_WorkerThread_SerialPort.Start();
            }

            return bUseSerialPort;
        }

        public bool Read_Immediate()
        {
            m_WorkerThread_Immediate = new Thread(new ThreadStart(this.WorkerThreadFunction_Immediate));
            m_WorkerThread_Immediate.Name = "Worker Thread Immediate Read";   // looks nice in Output window
            m_WorkerThread_Immediate.IsBackground = true;
            m_WorkerThread_Immediate.Start();
            return true;
        }

        public bool Read_Periodic()
        {
            m_WorkerThread_Periodic = new Thread(new ThreadStart(this.WorkerThreadFunction_Periodic));
            m_WorkerThread_Periodic.Name = "Worker Thread Periodic Read";   // looks nice in Output window
            m_WorkerThread_Periodic.IsBackground = true;
            m_WorkerThread_Periodic.Start();
            return true;
        }

        public bool Read_Tag()
        {
            System.Console.WriteLine("starting read till tag");
            m_WorkerThread_Tag = new Thread(new ThreadStart(this.WorkerThreadFunction_Tag));
            m_WorkerThread_Tag.Name = "Worker Thread Periodic Read till tag found";   // looks nice in Output window
            m_WorkerThread_Tag.IsBackground = true;
            m_WorkerThread_Tag.Start();
            return true;
        }

        public bool Read_GPI()
        {
            System.Console.WriteLine("starting read gpi mode using duration for stop");
            m_WorkerThread_GPI = new Thread(new ThreadStart(this.WorkerThreadFunction_GPI));
            m_WorkerThread_GPI.Name = "Worker Thread gpi using duration for stop";   // looks nice in Output window
            m_WorkerThread_GPI.IsBackground = true;
            m_WorkerThread_GPI.Start();
            return true;
        }

        public bool SendCMD(string s)
        {
            string rsp = null;

            //called by SetAttribsAndTriggers() in Program.cs

            System.Console.WriteLine(s);
            rsp = brdr.Execute(s);
            System.Console.WriteLine(rsp);
            if (s.IndexOf("ERR") >= 0)
            {
                System.Console.WriteLine("****  BRI ERROR *****");
                return false;
            }
            else
            {
            }
            return true;
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

        private bool FTPTagDataFile()
        {
            //There is a bug in Mono that will crash the app if you attempt to ftp to 
            //a server that is offline (or network goes down).  You cannot recover from 
            //this failure.  Once the network comes back online the app will crash on the next 
            //ftp attempt.  Its a mono bug.  Ping the ftp server first to make sure it is available.

            bool bFailed = false;
            string sfile = null;
            string s = GetMyDateNow() + "_" + GetMyTimeNow();
            sfile = "TagDataLog_" + s + ".txt";
            bFTPCrashed = true;

            try
            {
                ftp myftp = new ftp("ftp://" + myFTP.ftpIPADDR, myFTP.ftpUser, myFTP.ftpPass);
                System.Console.WriteLine("ftp class created....");

                System.Console.WriteLine("starting ftp upload....");
                if (myftp.upload("/" + sfile, "TagDataLog.txt"))
                {
                    System.Console.WriteLine("ftp upload completed....");
                    File.Delete("TagDataLog.txt");
                    System.Console.WriteLine("File deleted....");
                    iCycleCount = 0;
                    iCurrentTagCount = 0;
                    bFTPCrashed = false;
                    myftp = null;
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

        public void CloseReader()
        {
            //delete macros and triggers
            //dispose of IDL reader class
            string sMsg = null;

            if (brdr != null)
            {
                //delete any triggers you created.
                if (bDebug) { System.Console.WriteLine("Deleting triggers..."); }
                sMsg = brdr.Execute("TRIGGER RESET");

                //close reader connection and dispose of object
                brdr.Dispose();
            }
        }

        
        private void ReadData()
        {
            string s = null;
            try
            {
                System.Console.WriteLine("trying to read from serial port....");
                s = mySerialPort.ReadData();
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("exception reading: " + ee.Message);
            }
        }

        private void WorkerThreadFunction_Tag()
        {
            //no exit or break provided in this function because this should run
            //until the user manually stops the app via the web interface of the IF2

            System.Console.WriteLine("Starting Read Till Tag loop....");
            bReadTillTagFound = false;
            while (true)
            {
                if (!bReadTillTagFound)
                {
                    System.Console.WriteLine("Read Till Tag-> Start reading...");
                    bReadTillTagFound = true;
                    StartReadingTags();
                }
                Thread.Sleep(1000);
            }

            //You should not ever get here
            System.Console.WriteLine("**** ERROR Exiting Periodic read loop");
        }

        private void WorkerThreadFunction_Periodic()
        {
            //no exit or break provided in this function because this should run
            //until the user manually stops the app via the web interface of the IF2

            System.Console.WriteLine("Starting Periodic read loop....");

            while (true)
            {
                System.Console.WriteLine("Read Periodic-> Start reading...");
                Thread.Sleep(myRdrOpts.iPeriod);
                StartReadingTags();
                Thread.Sleep(myRdrOpts.iDuration);
                StopReadingTags();
                System.Console.WriteLine("Read Periodic-> Stop reading...");
            }

            //You should not ever get here
            System.Console.WriteLine("**** ERROR Exiting Periodic read loop");
        }

        private void WorkerThreadFunction_GPI()
        {
            //no exit or break provided in this function because this should run
            //until the user manually stops the app via the web interface of the IF2

            System.Console.WriteLine("Starting GPI read loop....");

            System.Console.WriteLine("Read GPI-> Start reading...");
            StartReadingTags();
            Thread.Sleep(myRdrOpts.iDuration);
            StopReadingTags();
            System.Console.WriteLine("Read GPI-> Stop reading...");

            //you should not ever get here
            System.Console.WriteLine("Exiting GPI read loop");
        }

        private void WorkerThreadFunction_Immediate()
        {
            //no exit or break provided in this function because this should run
            //until the user manually stops the app via the web interface of the IF2

            System.Console.WriteLine("Starting Immediate read loop....");

            while (true)
            {
                System.Console.WriteLine("Read Immediate-> Start reading...");
                StartReadingTags();

                               Thread.Sleep(myRdrOpts.iDuration);
                StopReadingTags();
                System.Console.WriteLine("Read Immediate-> Stop reading...");
                Thread.Sleep(myRdrOpts.iPeriod);
            }


            //you should not ever get here
            System.Console.WriteLine("**** ERROR Exiting Immediate read loop");
        }

        private void WorkerThreadFunction_SerialPort()
        {
            //Read input on serial port
            //allows you to start and stop reading via the serial port.

            //**************************************************************
            //this code requires FW 2.0.1200.1-14:46 or newer
            //**************************************************************

            //this code is not used by the Store and Forward demo application

            string s = null;

            while (true)
            {
                s = null;

                try
                {
                    System.Console.WriteLine("trying to read from serial port....");
                    s = mySerialPort.ReadData();
                }
                catch (Exception ee)
                {
                    System.Console.WriteLine("exception reading: " + ee.Message);
                }

                System.Console.WriteLine("read data: " + s);

                if (s != null)
                {
                    if (s.Length > 0)
                    {
                        System.Console.WriteLine("data: " + s);

                        s = s.ToUpper();
                        if (s.StartsWith("STOP"))
                        {
                            StopReadingTags();
                        }
                        else if (s.StartsWith("START EVENT"))
                        {
                            ReadTagsReportEvent();
                        }
                        else if (s.StartsWith("START EVENTALL"))
                        {
                            ReadTagsReportEventAll();
                        }
                        else if (s.StartsWith("START NO"))
                        {
                            ReadTagsReportNo();
                        }
                        else if (s.StartsWith("POLL"))
                        {
                            PollForTagsThread();
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }

        public bool CreateReaderWithIDLDebugging(string sURL)
        {
            bool bStatus = false;
            string sMsg = null;
            ReaderIP = sURL;
            if (bDebug) { System.Console.WriteLine("1.4 creating rfid connections..."); }

            //optional: enable IDL debug logging *******************************
            BRIReader.LoggerOptionsAdv LogOp = new BRIReader.LoggerOptionsAdv();
            LogOp.LogFilePath = ".\\IDLClassDebugLog.txt";
            LogOp.ShowNonPrintableChars = true;
            //******************************************************************

            try
            {
                //option two -> set size of reader buffer, event buffer, and enable IDL logging.
                //Reader Buffer is used for storing tags when you issue a READ, or READ REPORT=NO
                //Event Buffer is used for storing tags when you issue a READ REPORT=EVENT and all other events.
                //BRIReader(this, sConnection, Reader Buffer, Event Buffer, LogOp);
                //Please note that for console applications the first parameter is null.  For win32 and win mobile
                //applications this value is "this".
                this.brdr = new BRIReader(null, sURL, 1000, 1000, LogOp);
                if (bDebug) { System.Console.WriteLine("Connection created"); }
                sMsg = brdr.Execute("VER");
                if (bDebug) { System.Console.WriteLine("VER->" + sMsg); }
                if (sMsg.IndexOf("OK>") >= 0)
                {
                    //sMsg = brdr.Execute("TRIGGER RESET");
                    //if (bDebug) { System.Console.WriteLine("TRIGGER RESET->" + sMsg); }
                    ////turn off gpio output lines.
                    //sMsg = brdr.Execute("WRITEGPIO=12");
                    //if (bDebug) { System.Console.WriteLine("WRITEGPIO=12->" + sMsg); }
                    //bStatus = AddEventHandlers();
                }
            }
            catch (BasicReaderException ex)
            {
                if (bDebug) { System.Console.WriteLine("Failed creating reader connection BasicReaderException:\r\n" + ex.ToString()); }
                bStatus = false;
            }

            if (bStatus == false) { return bStatus; }

            if (bDebug) { System.Console.WriteLine("Reader Ready...starting configuration..."); }

            if (bDebug) { System.Console.WriteLine("Adding Event handlers..."); }
            AddEventHandlers();

            if (bDebug) { System.Console.WriteLine("Starting MyReadThread..."); }

            return bStatus;
        }

        public bool CreateReaderWithNoIDLDebugging(string sURL)
        {
            string sMsg = null;
            bool bStatus = false;
            ReaderIP = sURL;
            if (bDebug) { System.Console.WriteLine("1.4 creating rfid connections..."); }

            try
            {
                //Create reader connection no IDL debugging is enabled.
                brdr = new BRIReader(null, sURL, 22000, 2000);

                if (bDebug) { System.Console.WriteLine("Connection created"); }
                sMsg = brdr.Execute("VER");
                if (bDebug) { System.Console.WriteLine("VER->" + sMsg); }
                if (sMsg.IndexOf("OK>") >= 0)
                {
                    //sMsg = brdr.Execute("TRIGGER RESET");
                    //if (bDebug) { System.Console.WriteLine("TRIGGER RESET->" + sMsg); }
                    ////turn off gpio output lines.
                    //sMsg = brdr.Execute("WRITEGPIO=12");
                    //if (bDebug) { System.Console.WriteLine("WRITEGPIO=12->" + sMsg); }
                    bStatus = AddEventHandlers();

                   // Thread.Sleep(5000);

                   // sMsg = brdr.Execute("WRITEGPIO=15");
                   // if (bDebug) { System.Console.WriteLine("WRITEGPIO=15->" + sMsg); }
                   //// bStatus = AddEventHandlers();




                }
            }
            catch (BasicReaderException eBRI)
            {
                if (bDebug) { System.Console.WriteLine("Failed creating reader connection BasicReaderException:\r\n" + eBRI.ToString()); }
                bStatus = false;
            }
  
            return bStatus;
        }

        public void ReadTagsReportDirect()
        {
            //Simple read
            //this code is not used by the Store and Forward demo

            bool bStatus = false;

            iTagCount = 0;

            //Here are various read options
            //Pick one and comment out the other two

            //1. Simple read of epc id's
            //bStatus = brdr.Read();

            //2. Read only tags who's epc id starts with hex 0102
            //bStatus = brdr.Read("HEX(1:4,2)=H0102");

            //3. Return the antenna that read the tag and the number of times each tag was read
            bStatus = brdr.Read(null, "ANT COUNT");

            //get the tag ids
            LoadTags();
        }

        public void ReadTagsReportNo()
        {
            //Continuous read using polling to retrieve tag list
            //this code is not used by the Store and Forward demo

            bool bStatus = false;

            iTagCount = 0;

            //Here are various read options
            //Pick one and comment out the other two

            //1. Read epc id's
            bStatus = brdr.StartReadingTags(null, "ANT", BRIReader.TagReportOptions.POLL);

            //2. Use a filter to read only tags who's epc id starts with hex 0102
            //bStatus = brdr.StartReadingTags("HEX(1:4,2)=H0102", null, BRIReader.TagReportOptions.POLL);

            //3. Return the antenna that read the tag and the number of times each tag was read
            //bStatus = brdr.StartReadingTags(null, "ANT COUNT", BRIReader.TagReportOptions.POLL);

            //determine how much time you want before polling for tag list.
            //you will need to create thread to poll for tags using brdr.PollTags()
            //Use private void LoadTags() to get the tags after sending the Poll.
        }

        public void PollForTagsThread()
        {
            //sleep to allow events to happen
            //user specifies polling interval when they send the command.

            //Thread.Sleep(iPollInterval);
            brdr.PollTags();
            LoadTags();
        }

        private void LoadTags()
        {
            //load epc ids into a list
            //used with REPORT=N0... -> private void timer1_Tick(object sender, EventArgs e)
            //used with REPORT=DIRECT...default read method -> private int ReadTagsReportDirect(){}

            int RspCount = 0;
            string sTagID = null;
            long iTagIndex = 0;

            bDebug = true;

            foreach (Tag tt in brdr.Tags)
            {
                RspCount++;
                sTagID = tt.ToString();
                if (tt.TagFields.ItemCount > 0)
                {
                    foreach (TagField tf in tt.TagFields.FieldArray)
                    {
                        //get field data
                        sTagID += " " + tf.ToString();
                    }
                }

                //see if this is a new tag and update tag list.
                iTagIndex = CheckTagList(sTagID, sTagID, sTagID);

                //add tag to collection list which is used to send data out the serial port.
                //MyTagCollection.Add("TAGID=" + TagIDList[iTagIndex] + ",ANT=" + TagANTList[iTagIndex] + ",COUNT=" + TagCountList[iTagIndex].ToString());

                if (bUseSerialPort)
                    mySerialPort.SendData(sTagID);

                if (bDebug) { System.Console.WriteLine("->" + sTagID); }
                sTagID = null;
            }
        }

        private string[] ParseResponseMessage(string sMsg)
        {
            //parse the response from the reader.
            //Some responses have multiple lines, such as the reponse
            //to the VERSION command (VER).  Lines separated by \r\n.

            string delimStr = null;
            string[] tList = null;
            char[] delimiter = null;
            
            delimStr = " ";
            delimiter = delimStr.ToCharArray();
            tList = sMsg.Replace("\r\n", " ").Split(delimiter);
            return tList;
        }

        public void ReadTagsReportEvent()
        {
            //Continuous Read which returns tags as events
            //Will return the tag only one time.

            //this code is not used by the Store and Forward demo application

            bool bStatus = false;

            iTagCount = 0;

            //Here are various read options
            //Pick one and comment out the other two

            //1. Read epc id's
            bStatus = brdr.StartReadingTags(null, "ANT", BRIReader.TagReportOptions.EVENT);

            //2. Use a filter to read only tags who's epc id starts with hex 0102
            //bStatus = brdr.StartReadingTags("HEX(1:4,2)=H0102", null, BRIReader.TagReportOptions.EVENT);

            //3. Return the antenna that read the tag and the number of times each tag was read
            //bStatus = brdr.StartReadingTags(null, "ANT COUNT", BRIReader.TagReportOptions.EVENT);
        }

        public void ReadTagsReportEventAll()
        {
            //Continuous Read which returns tags as events.
            //Will return the tag each time its read.
            //Warning you can overwhelm the application using this method with a large 
            //number of tags in the file.

            //this code is not used by the Store and Forward demo

            bool bStatus = false;

            iTagCount = 0;

            //Here are various read options
            //Pick one and comment out the other two

            //1. Read epc id's
            bStatus = brdr.StartReadingTags(null, "ANT", BRIReader.TagReportOptions.EVENTALL);

            //2. Use a filter to read only tags who's epc id starts with hex 0102
            //bStatus = brdr.StartReadingTags("HEX(1:4,2)=H0102", null, BRIReader.TagReportOptions.EVENT);

            //3. Return the antenna that read the tag and the number of times each tag was read
            //bStatus = brdr.StartReadingTags(null, "ANT COUNT", BRIReader.TagReportOptions.EVENT);
        }
        
        private bool AddEventHandlers()
        {
            //*********************************************************************
            // Add the event handlers
            //*********************************************************************

            //I'm only using the tag event handler and GPIO event handlers at the moment.  
            //The other handlers don't do anthing other than print a debug message to the console.

            try
            {
                this.brdr.EventHandlerTag += new Tag_EventHandlerAdv(brdr_EventHandlerTag);
                this.brdr.EventHandlerGPIO += new GPIO_EventHandlerAdv(brdr_EventHandlerGPIO);
                this.brdr.EventHandlerRadio += new Radio_EventHandlerAdv(brdr_EventHandlerRadio);
                this.brdr.EventHandlerThermal += new Thermal_EventHandler(brdr_EventHandlerThermal);
                this.brdr.EventHandlerAntennaFault += new AntennaFault_EventHandler(brdr_EventHandlerAntennaFault);
            }
            catch
            {
                if (bDebug) { System.Console.WriteLine("AddEventHandlers(): Error trying to create event handlers"); }
                return false;
            }
            return true;
        }

        void brdr_EventHandlerAntennaFault(object sender, AntennaFault_EventArgs EvtArgs)
        {
            System.Console.WriteLine("Antenna Fault Detected! Ant ID = " +  EvtArgs.AntennaID);
        }

        void brdr_EventHandlerThermal(object sender, Thermal_EventArgs EvtArgs)
        {
            System.Console.WriteLine("brdr_EventHandlerThermal Detected"); 
        }

        void brdr_EventHandlerRadio(object sender, EVTADV_Radio_EventArgs EvtArgs)
        {
            System.Console.WriteLine("brdr_EventHandlerRadio Detected!");
        }

        void brdr_EventHandlerGPIO(object sender, EVTADV_GPIO_EventArgs EvtArgs)
        {
            //This will only fire if you have defined GPIO TRIGGERS

            System.Console.WriteLine("brdr_EventHandlerGPIO: " + EvtArgs.TriggerNameString);
            
            if (EvtArgs.TriggerNameString.Equals("STARTREAD"))
            {
                if (!myRdrOpts.bStopReadTriggerFound)
                    Read_GPI();
                else
                    StartReadingTags();

            }
            else if (EvtArgs.TriggerNameString.IndexOf("STOPREAD") >= 0)
            {
                StopReadingTags();
            }
            else
            {
                System.Console.WriteLine("brdr_EventHandlerGPIO: Unknown Trigger Name: " + EvtArgs.TriggerNameString);
            }
        }

        public void BuildFieldNameList()
        {
            string[] sList = null;
            string delimStr = " ";
            char[] delimiter = null;

            delimiter = delimStr.ToCharArray();

            if (myRdrOpts.sFieldSchema != null)
            {
                sList = myRdrOpts.sFieldSchema.Split(delimiter);
                myRdrOpts.iFieldCount = 0;

                for (int x = 0; x < sList.Length; x++)
                {
                    if (sList[x] != null)
                        if (sList[x] != "")
                            myRdrOpts.sFieldNameList[myRdrOpts.iFieldCount++] = sList[x];
                }
            }
        }

        private void SetGPIO(string s)
        {
            //15=off
            //14=green
            //13=yellow
            //11=red

            string sLEDColor = null;

            if (s.Equals("OFF"))
                sLEDColor = myRdrOpts.LED_Off;
            if (s.Equals("GREEN"))
                sLEDColor = myRdrOpts.LED_Green;
            if (s.Equals("YELLOW"))
                sLEDColor = myRdrOpts.LED_Yellow;
            if (s.Equals("RED"))
                sLEDColor = myRdrOpts.LED_Red;

            if (sLEDColor == null)
                return;

            string rsp = null;
            rsp = brdr.Execute("WRITEGPIO=" + myRdrOpts.LED_Off);//turn off any GPO that is on
            rsp = brdr.Execute("WRITEGPIO=" + sLEDColor.Trim()); //set GPO
            if (rsp.IndexOf("ERR") >= 0)
            {
                System.Console.WriteLine("->Error settingt GPO: " + s + "\r\n" + rsp);
            }
        }

        public void StartReadingTags()
        {
            string sMsg = null;
            //if (myRdrOpts.Light_On_Reading != null)
            //    SetGPIO(myRdrOpts.Light_On_Reading);

            iCurrentTagCount = 0;
            if (myRdrOpts.sReportMode.Equals("EVENT"))
            {
                brdr.StartReadingTags(myRdrOpts.sFilterSchema, myRdrOpts.sFieldSchema, BRIReader.TagReportOptions.EVENT);
               
                if (myEPC == null)
                {
                    myEPC = null;
                    if (bDebug) { System.Console.WriteLine("TRIGGER RESET->" + sMsg); }
                    //sMsg = brdr.Execute("WRITEGPIO=15");
                    //if (bDebug) { System.Console.WriteLine("WRITEGPIO=15->" + sMsg); }
                    //myEPC = null;
                    Thread.Sleep(500);

                  
                }
                //bvn add 9-6-2015
                else
                {
                    //bvn **********4-5-2015***************
                    //sMsg = brdr.Execute("TRIGGER RESET");
                    myEPC = null;
                    if (bDebug) { System.Console.WriteLine("TRIGGER RESET->" + sMsg); }
                    //turn off gpio output lines.
                    //sMsg = brdr.Execute("WRITEGPIO=14");
                    //if (bDebug) { System.Console.WriteLine("WRITEGPIO=14->" + sMsg); }

                   // myEPC = null;
                    Thread.Sleep(500);
                    

                }
            
            }
            else if (myRdrOpts.sReportMode.Equals("EVENTALL"))
            {
                brdr.StartReadingTags(myRdrOpts.sFilterSchema, myRdrOpts.sFieldSchema, BRIReader.TagReportOptions.EVENTALL);
            }
        }

        public void StopReadingTags()
        {
            brdr.StopReadingTags();

            //if (myRdrOpts.Light_On_TagsFound != null)
            //    SetGPIO(myRdrOpts.Light_On_TagsFound);
            //else if (myRdrOpts.Light_On_Standy != null)
            //    SetGPIO(myRdrOpts.Light_On_Standy);

            if (myRdrOpts.sMode.Equals("TAG"))
            {
                Thread.Sleep(myRdrOpts.iPeriod);
                //if (myRdrOpts.Light_Off != null)
                //    SetGPIO(myRdrOpts.Light_Off);
                return;
            }

            if (myFTP.ftpEnable)
            {
                if (iCurrentTagCount > 0)
                {
                    if (!bFTPCrashed)
                    {
                        if (PingFTServer())
                            bFTPCrashed = FTPTagDataFile();
                    }
                }
            }
            else
                iCurrentTagCount = 0;
        }

        private bool PingFTServer()
        {
            string s = null;
            string result;
            bool b = false;
            int x = 0;

            //Mono does not have permission to ping using the .Net Ping command.
            //So you have to manually issue the ping.

            System.Diagnostics.ProcessStartInfo procStartInfo =
                new System.Diagnostics.ProcessStartInfo("ping", myFTP.ftpIPADDR + " -w 3");

            procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            // The following commands are needed to redirect the standard output.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            // Now we create a process, assign its ProcessStartInfo and start it
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();

            // Get the output into a string
            try
            {
                while ((result = proc.StandardOutput.ReadLine()) != null)
                {
                    s = result.ToUpper().Trim();
                    System.Console.WriteLine("->>>" + s);

                    //response on IF2
                    //->>>PING 10.10.10.7 (10.10.10.7): 56 DATA BYTES
                    //->>>64 BYTES FROM 10.10.10.7: SEQ=0 TTL=127 TIME=2.754 MS

                    //PC response "Reply from 10.10.10.7: bytes=32 time=1ms TTL=128"

                    if (s.IndexOf("BYTES FROM " + myFTP.ftpIPADDR) >= 0 || 
                        s.IndexOf("REPLY FROM " + myFTP.ftpIPADDR) >= 0)
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

            System.Console.WriteLine("->>>>>Ping completed");

            return b;
        }

        private Int64 CheckTagList(string sEPC, string Invaltag, string sfielddata)
        {
            //this code is used to store each tag id in a list.  It looks the new id up in the list
            //if its already in the list it just updates the tag's count, if its not in the list it gets
            //added to the list.  You don't have to store the ID in a list.

            bool bTagFound = false;
            int x = 0;
            Int64 iTagIndex = 0;

            int tMO = DateTime.Now.Month;
            int tDAY = DateTime.Now.Day;
            int tYR = DateTime.Now.Year;
            int tHR = DateTime.Now.Hour;
            int tMN = DateTime.Now.Minute;
            int tSEC = DateTime.Now.Second;
            ReadTime = DateTime.Now.ToString();
            string sDateTimeStamp = tDAY + "." + tMO + "." + tYR + "," + tHR + ":" + tMN + ":" + tSEC;
            bool sqlstatus = true;
            try
            {
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                sqlstatus = true;
            }
            catch(Exception ex)
            {
                sqlstatus = false;
            }
            try
            {
                if (sqlstatus == true)
                {
                    if (File.Exists("OfflineTagDataLog.txt"))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "prc_OfflineInternalFleet";
                        cmd.Connection = Sqlcon;
                        int res=cmd.ExecuteNonQuery();
                        if (res > 0)
                        {
                            File.Delete("OfflineTagDataLog.txt");
                        }
                    }                    
                    string DBDate = "Select GETDATE()";
                    SqlCommand dbcmd = new SqlCommand(DBDate, Sqlcon);
                    SqlDataReader dr = dbcmd.ExecuteReader();
                    if (dr.Read())
                    {
                        ReadTime = dr[0].ToString();
                    }
                    dr.Close();

                    if (bDebug) { System.Console.WriteLine("CheckTagList()"); }


                    //write tag data to log file
                    iCycleCount++;

                    if (ValidTruck(sEPC))
                    {
                        if (myRdrOpts.Log_Tags)
                        {
                            if (sfielddata != null)
                                LogTags(iCycleCount + "," + sEPC + sfielddata + "," + sDateTimeStamp, sEPC);
                            else
                                LogTags(iCycleCount + "," + sEPC + "," + sDateTimeStamp, sEPC);
                        }
                    }
                    else
                    {
                        if (errorflag == 1)
                        {
                            SaveJunkData(Invaltag, WBNO);
                            //Thread.Sleep(5000);
                            LogTags(iCycleCount + "," + sEPC + "," + sDateTimeStamp, sEPC);
                        }
                    }

                    if (iTagCount > 0)
                    {
                        for (x = 1; x <= iTagCount; x++)
                        {
                            if (TagIDList[x].Equals(sEPC))
                            {
                                bTagFound = true;
                                iTagIndex = x;
                                break;
                            }
                        }
                    }

                    if (bTagFound == false)
                    {
                        TagIDList[++iTagCount] = sEPC;
                        TagCountList[iTagCount] = 1;
                        iTagIndex = iTagCount;
                    }
                    else
                    {
                        TagCountList[x]++;
                    }
                }
                else
                {
                    if (sfielddata != null)
                        LogTags(iCycleCount + "," + sEPC + sfielddata + "," + sDateTimeStamp, sEPC);
                    else
                        LogTags(iCycleCount + "," + sEPC + "," + sDateTimeStamp, sEPC);
                }

            }
            catch (Exception ex)
            {
               
            }

            return iTagIndex;
        }
        private void LogTags(string sTagData, string Tagnum)
        {
            //*************************************************
            //*** Write tag data to log file
            //*************************************************

            System.Console.WriteLine("Start LogTags()");

            FileStream tFileStream = null;
            StreamWriter tStreamWriter = null;

            //NOT A VALID FILE PATH FOR IF2+ READERS
            //string tFileName = "/home/developer/edgeware/userapp0/./tagdata.log.txt";
            //string tFileName = "/home/developer/./TagDataLog.txt";  //this will work on the next FW build.
            string tFileName = "TagDataLog.txt";

            try
            {
                tFileStream = new FileStream(tFileName, FileMode.Append);
                System.Console.WriteLine("->FileStream Open");
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("Exception: " + ee.Message);
                return;
            }

            try
            {
                tStreamWriter = new StreamWriter(tFileStream);
                System.Console.WriteLine("->StreamWriter Open");
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("Exception : " + ee.Message);
                return;
            }

            if (outParamIsValid)
            {
                InsertTagDetails(Tagnum);
            }
            try
            {
                tStreamWriter.Write(sTagData + "\r\n");

                if (iBackupCount > 0)
                {
                    int j = 0;
                    for (j = 0; j < iBackupCount; j++)
                    {
                        if (sBackUpData[j] != null)
                            if (sBackUpData[j] != "")
                                tStreamWriter.Write(sBackUpData[j] + "\r\n");
                    }
                    iBackupCount = 0;
                    sBackUpData = null;
                    sBackUpData = new string[1];
                }
                tStreamWriter.Flush();
                tStreamWriter.Close();
                tFileStream.Close();
                System.Console.WriteLine("-> Tag Data logged");
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("***************************************************");
                System.Console.WriteLine("Exception writing data to log: " + ee.Message);
                System.Console.WriteLine("***************************************************");
                Array.Resize<string>(ref sBackUpData, sBackUpData.Length + 1);
                sBackUpData[iBackupCount++] = sTagData;
            }
        }

        private void OfflineLogTags(string sTagData, string Tagnum)
        {
            //*************************************************
            //*** Write tag data to log file
            //*************************************************

            System.Console.WriteLine("Start LogTags()");

            FileStream tFileStream = null;
            StreamWriter tStreamWriter = null;

            //NOT A VALID FILE PATH FOR IF2+ READERS
            //string tFileName = "/home/developer/edgeware/userapp0/./tagdata.log.txt";
            //string tFileName = "/home/developer/./TagDataLog.txt";  //this will work on the next FW build.
            string tFileName = "OfflineTagDataLog.txt";

            try
            {
                tFileStream = new FileStream(tFileName, FileMode.Append);
                System.Console.WriteLine("->FileStream Open");
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("Exception: " + ee.Message);
                return;
            }

            try
            {
                tStreamWriter = new StreamWriter(tFileStream);
                System.Console.WriteLine("->StreamWriter Open");
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("Exception : " + ee.Message);
                return;
            }
            try
            {
                tStreamWriter.Write(sTagData + "\r\n");

                if (iBackupCount > 0)
                {
                    int j = 0;
                    for (j = 0; j < iBackupCount; j++)
                    {
                        if (sBackUpData[j] != null)
                            if (sBackUpData[j] != "")
                                tStreamWriter.Write(sBackUpData[j] + "\r\n");
                    }
                    iBackupCount = 0;
                    sBackUpData = null;
                    sBackUpData = new string[1];
                }
                tStreamWriter.Flush();
                tStreamWriter.Close();
                tFileStream.Close();
                System.Console.WriteLine("-> Tag Data logged");
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("***************************************************");
                System.Console.WriteLine("Exception writing data to log: " + ee.Message);
                System.Console.WriteLine("***************************************************");
                Array.Resize<string>(ref sBackUpData, sBackUpData.Length + 1);
                sBackUpData[iBackupCount++] = sTagData;
            }
        }

        private void InsertTagDetails(string tagNum)
        {
            string TagNo = tagNum;
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            if (TruckFlag == 1)
            {
                //string strNonQuery_PortEntry = "Insert into [IFMS150311].[dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values (" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strPortEntryIPaddress + "')";
                string strNonQuery_PortEntry = "Insert into [rfid].[dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values (" + "'" + TagNo + "'" + "," + "'" + AntID + "'" + "," + "'" + WBNO + "'" + ",GETDATE(),'" + TruckNo + "','" + ReaderIP + "')";
                SqlInstcmd = new SqlCommand(strNonQuery_PortEntry, Sqlcon);
            }
            else if (TruckFlag == 2)
            {
                string strNonQuery_PortEntry = "Insert into [rfid].[dbo].[MTFX9500BYROAD] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values (" + "'" + TagNo + "'" + "," + "'" + AntID + "'" + "," + "'" + WBNO + "'" + ",GETDATE(),'" + TruckNo + "','" + ReaderIP + "')";
                SqlInstcmd = new SqlCommand(strNonQuery_PortEntry, Sqlcon);
            }
            int execquery = SqlInstcmd.ExecuteNonQuery();
            if (execquery >= 1)
            {
                System.Console.WriteLine(TagNo + " has read and PortEntry-In Time captured successfully");
                Thread.Sleep(1000);
                Sqlcon.Close();
            }
        }

        private bool ValidTruck(string sTagData)
        {
            try
            {
                outParamIsValid = false;
                string TagNo = sTagData;
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                // string com1 = "SELECT [TruckNo]  FROM [rfid].[dbo].[TruckMaster]a, [rfid].[dbo].[TagMaster] b where a.TagId = b.TagId and b.TagNo ='" + Class_ProperityLayer.PTAGNO1 + "'";
                string com1 = "select [TruckNo] from rfid.dbo.[Tag_TruckAllocation] A, [rfid].[dbo].[TruckMaster] B, [rfid].[dbo].[TagMaster] C where A.Tagid=C.Tagid and A.Tkid=B.Tkid and A.[RStatus]='Active' and C.TagNo='" + TagNo + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(com1, Sqlcon);
                DataSet ds1 = new DataSet();
                string command = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from [rfid].[dbo].[MTFX9500InternalFleet] where  [wbno] ='" + WBNO + "'  order by readdt desc";
                SqlDataAdapter da = new SqlDataAdapter(command, Sqlcon);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DBFleetTime = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                    newtag = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    newtag = "0";
                }

                string command1 = "select  TOP 1 [tagno],[Ant],[wbno],[readdt] from [rfid].[dbo].[MTFX9500BYROAD] where  [wbno] ='" + WBNO + "'  order by readdt desc";
                SqlDataAdapter dar = new SqlDataAdapter(command1, Sqlcon);
                DataSet dsr = new DataSet();
                dar.Fill(dsr);

                if (dsr.Tables[0].Rows.Count > 0)
                {
                    DBRoadTime = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                    newtag1 = dsr.Tables[0].Rows[0].ItemArray[0].ToString();
                    //dstr = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                }
                else
                {
                    newtag1 = "0";
                }
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    TruckNo = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                    if (newtag == TagNo)// && PTruckNo == Class_ProperityLayer.ParkingFleetNo1
                    {
                        TimeSpan ts = Convert.ToDateTime(ReadTime) - Convert.ToDateTime(DBFleetTime);
                        int difftimeMin = ts.Minutes;
                        int difftimeSec = ts.Seconds;
                        string TimeDiff = difftimeMin.ToString() + ":" + difftimeSec.ToString();
                        if (difftimeMin > 5)
                        {
                            TruckFlag = 1;
                            outParamIsValid = true;
                            return true;
                        }
                        else
                        {
                            System.Console.WriteLine(TagNo + " & " + TruckNo + " has read Same Tag with Same Truck Found..");
                            Thread.Sleep(1000);
                            outParamIsValid = false;
                            errorflag = 0;
                            return false;
                        }

                    }
                    else
                    {
                        TruckFlag = 1;
                        outParamIsValid = true;
                        return true;
                    }
                }
                else
                {
                    if (newtag1 == TagNo)
                    {
                        TimeSpan ts = Convert.ToDateTime(ReadTime) - Convert.ToDateTime(DBRoadTime);
                        int difftimeMin = ts.Minutes;
                        int difftimeSec = ts.Seconds;
                        string TimeDiff = difftimeMin.ToString() + ":" + difftimeSec.ToString();
                        if (difftimeMin > 5)
                        {
                            TruckFlag = 1;
                            outParamIsValid = true;
                            return true;
                        }
                        else
                        {
                            System.Console.WriteLine(TagNo + " has read Same Tag Found...");
                            Thread.Sleep(1000);
                            outParamIsValid = false;
                            errorflag = 0;
                            return false;
                        }
                    }
                    else
                    {
                        //inventoryList.Items.Clear();

                        string com2 = "SELECT [Truck No]  FROM [rfid].[dbo].[Tag_Register] where [Tag No] ='" + TagNo + "' and [TransctionStatus] = 'P' order by [Tag ID] desc";
                        SqlDataAdapter da2 = new SqlDataAdapter(com2, Sqlcon);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            TruckNo = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
                            TruckFlag = 2;
                            outParamIsValid = true;
                            errorflag = 0;
                            return true;
                        }
                        else
                        {
                            System.Console.WriteLine("Tag/Truck is Not Registered");
                            errorflag = 1;
                            return false;
                        }
                    }
                }
                //return true;
            }
            catch (Exception ex)
            {
                errorflag = 1;
                System.Console.WriteLine(ex.Message.ToString());
                CatchException(string.Format("{0},{1},{2},{3}", ex.Message, ex.StackTrace, WBNO, DateTime.Now));
                return false;
            }
        }

        private void SaveJunkData(string tagnum, string wbnum)
        {
            try
            {
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                string command = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from [rfid].[dbo].[MTFX9500JunkData] where  [wbno] ='" + WBNO + "'  order by readdt desc";
                SqlDataAdapter da = new SqlDataAdapter(command, Sqlcon);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DBFleetTime = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                    newtag = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    newtag = "0";
                }
                if (newtag == tagnum)
                {
                    TimeSpan ts = Convert.ToDateTime(ReadTime) - Convert.ToDateTime(DBFleetTime);
                    int difftimeMin = ts.Minutes;
                    int difftimeSec = ts.Seconds;
                    string TimeDiff = difftimeMin.ToString() + ":" + difftimeSec.ToString();
                    if (difftimeMin > 5)
                    {
                        string strNonQuery_PortEntry = "Insert into [rfid].[dbo].[MTFX9500JunkData] ([tagno],[Ant],[wbno],[readdt],[Reader])" + "Values(" + "'" + tagnum + "'" + "," + "'" + AntID + "'" + "," + "'" + wbnum + "'" + ",GETDATE(),'" + ReaderIP + "')";
                        SqlCommand Sqlcmd = new SqlCommand(strNonQuery_PortEntry, Sqlcon);
                        Sqlcmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string strNonQuery_PortEntry = "Insert into [rfid].[dbo].[MTFX9500JunkData] ([tagno],[Ant],[wbno],[readdt],[Reader])" + "Values(" + "'" + tagnum + "'" + "," + "'" + AntID + "'" + "," + "'" + wbnum + "'" + ",GETDATE(),'" + ReaderIP + "')";
                    SqlCommand Sqlcmd = new SqlCommand(strNonQuery_PortEntry, Sqlcon);
                    Sqlcmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                CatchException(string.Format("{0},{1},{2},{3}", ex.Message, ex.StackTrace, WBNO, DateTime.Now));
            }
        }

        private static void CatchException(string excepmsg)
        {
            StreamWriter strW = File.AppendText(Application.ExecutablePath + "ErrorLog.txt");
            strW.WriteLine(excepmsg);
            strW.Close();
        }

        Stopwatch myTagReadMaxTimeSpan = new Stopwatch();
        void brdr_EventHandlerTag(object sender, EVTADV_Tag_EventArgs EvtArgs)
        {
            //will only fire if you are using READ REPORT=EVENT or READ REPORT=EVENTALL

            if (iTagCount == 0)
            {
                myTagReadMaxTimeSpan = null;
                myTagReadMaxTimeSpan = new Stopwatch();
                myTagReadMaxTimeSpan.Start();
            }
            else if (myTagReadMaxTimeSpan.IsRunning)
            {
                //stop reading if time span has expired
                if (myTagReadMaxTimeSpan.Elapsed.Seconds >= 100)
                {
                    myTagReadMaxTimeSpan.Stop();
                    brdr.StopReadingTags();
                }
            }

            Int64 iTagIndex = 0;
            string sepc = null;
            string sfielddata = null;

            iCurrentTagCount++;

            if (myRdrOpts.sMode.Equals("TAG"))
                StopReadingTags();

            TagFields myfields = new TagFields();

            System.Console.WriteLine("brdr_EventHandlerTag: " + EvtArgs.DataString.ToString());

            if (bUseSerialPort)
            {
                mySerialPort.SendData(EvtArgs.DataString.ToString());
            }

            sepc = EvtArgs.Tag.ToString();
            string Tagno = sepc.Replace(" ", "").Substring(0, 5);
            myEPC = null;
            myEPC = EvtArgs.Tag.ToString();
            myEPC = myEPC.Replace(" ", "").Substring(0, 5);

            System.Console.WriteLine("tag => " + Tagno);

            if (myRdrOpts.iFieldCount > 0)
            {
                for (int x = 0; x < myRdrOpts.iFieldCount; x++)
                {
                    sfielddata += "," + EvtArgs.Tag.TagFields.FieldArray[x];
                }
            }

            //see if this is a new tag and update tag list.
            iTagIndex = CheckTagList(Tagno,sepc, sfielddata);

            if (myRdrOpts.sMode.Equals("TAG"))
                bReadTillTagFound = false;

            //add tag to collection list which is used to send data out the serial port.
            //MyTagCollection.Add("TAGID=" + TagIDList[iTagIndex] + ",ANT=" + TagANTList[iTagIndex] + ",COUNT=" + TagCountList[iTagIndex].ToString());
        }


        //private void CreateTriggers()
        //{
        //    //create the gpio triggers and macros to execute reads
        //    //you do not have to use macros with triggers.

        //    string sMsg = null;

        //    if (bDebug) { System.Console.WriteLine("CreateTriggers()..."); }

        //    //or you can create normal triggers
        //    sMsg = brdr.Execute("TRIGGER \"ARMAON\" GPIOEDGE 1 1 FILTER 0");
        //    if (bDebug) { System.Console.WriteLine("TRIGGER \"myTrigON\" GPIOEDGE 1 1 FILTER 0 -> " + sMsg); }
        //    sMsg = brdr.Execute("TRIGGER \"ARMAOFF\" GPIOEDGE 1 0 FILTER 0");
        //    if (bDebug) { System.Console.WriteLine("TRIGGER \"myTrigOFF\" GPIOEDGE 1 0 FILTER 0 -> " + sMsg); }

        //    //sMsg = brdr.Execute("TRIGGER \"ARMBON\" GPIOEDGE 2 2 FILTER 0");
        //    if (bDebug) { System.Console.WriteLine("TRIGGER \"myTrigON\" GPIOEDGE 1 1 FILTER 0 -> " + sMsg); }
        //    sMsg = brdr.Execute("TRIGGER \"ARMBOFF\" GPIOEDGE 2 0 FILTER 0");
        //    if (bDebug) { System.Console.WriteLine("TRIGGER \"myTrigOFF\" GPIOEDGE 1 0 FILTER 0 -> " + sMsg); }

        //    if (bDebug) { System.Console.WriteLine("CreateTriggers() Completed"); }
        //}

        //public void WriteTag()
        //{
        //    string sRSP = null;

        //    //Pick a method to use for writing tags.  Below or FOUR examples of different writing methods.
        //    //1. Issue simple write command to program the EPC code in a tag.  This write command will write to
        //    //all tags found.
        //    sRSP = this.brdr.Execute("W EPCID=H010203040506070809101112");

        //    //2. Program an EPC code using a where clause to select a subgroup of tags.
        //    //sRSP = this.brdr.Execute("W EPCID=H010203040506070809101112 WHERE HEX(1:4,2)==H1122");

        //    //3. Issue simple write command which writes the ascii string "HI" to the tag.  This write command
        //    //will write to all tags found.
        //    //sRSP = this.brdr.Execute("W STRING(1:4,2)=\"HI\"");

        //    //4. Issue write command which writes the ascii string "HI" to the all tags using a where clauise to select
        //    //all tags that do not have "HI" already programmed into them.  This write command
        //    //will write to all matching the where clause.
        //    //sRSP = this.brdr.Execute("W STRING(1:4,2)=\"HI\" WHERE STRING(1:4,2)!=\"HI\"");

        //    ParseResponseMessage(sRSP);

        //    //you need to check the response to determine if the write was successful
        //    if (sRSP.IndexOf("WROK") > 0)
        //    {
        //        //write succeeded.  Add your code here.
        //        iTagCount++;
        //    }
        //    else if (sRSP.IndexOf("WRERR") > 0)
        //    {
        //        //Write failed.  Add your code here.

        //    }
        //    else if (sRSP.IndexOf("PVERR") > 0)
        //    {
        //        //Write failed, memory is locked or password protected.  Add your code here.

        //    }
        //    else if (sRSP.IndexOf("PWERR") > 0)
        //    {
        //        //Write failed, not enough RF power to write data.  Add your code here.

        //    }
        //    else if (sRSP.IndexOf("ERR") > 0)
        //    {
        //        //catch any other type of errors that could occur during the write operation.   Add your code here.

        //    }
        //    else if (sRSP.IndexOf("ERR") == 0)
        //    {
        //        //syntax error in command.  Add your code here.

        //    }
        //}

    }//END RFIDReaderClass
}//END namespace SimpleConsoleApplication
