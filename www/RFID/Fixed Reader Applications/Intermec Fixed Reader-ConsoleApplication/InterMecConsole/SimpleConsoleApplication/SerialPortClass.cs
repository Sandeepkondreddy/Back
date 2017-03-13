using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using System.Threading;

namespace SimpleConsoleApplication
{
    class SerialPortClass
    {
        FileStream myfs = null;
        StreamWriter mysw = null;
        StreamReader mysr = null;

        //******************************************************************
        //this code will only work on IF2+ readers running fw if2-2_0_1200_1-web.bin or newer
        //******************************************************************
        
        public bool ConnectToSerialPort()
        {
            System.Console.WriteLine("opening serial port....");

            try
            {
                //   /dev/ttyS01
                string sPort = "/dev/ttyS01"; //IF2 plus internal serial port address.
                myfs = new FileStream(sPort, FileMode.Open, FileAccess.ReadWrite);
                mysw = new StreamWriter(myfs);
                mysr = new StreamReader(myfs);

                SendData("<serial port test string>");

                System.Console.WriteLine("serial port open");
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("FAILED opening serial port bye");
                System.Console.WriteLine(ee.Message);
                return false;
            }

            return true;
        }

        public bool SendData(string s)
        {
            if (mysw == null)
                return false;

            //adding \r\n for use with hypertermial only
            s = s + "\r\n";

            try
            {
                System.Console.WriteLine("writing data to serial port....");
                mysw.WriteLine(s);
                mysw.Flush();
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("Failed writing to serial port bye");
                System.Console.WriteLine(ee.Message);
                return false;
            }

            System.Console.WriteLine("Success writing to serial port");
            return true;
        }

        public string ReadData()
        {
            string s = "ERR";

            try
            {
                s = mysr.ReadLine();
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("Exception reading serial port..." + ee.Message);
                s = "ERR";
            }

            return s;
        }

        public void CloseSerialPort()
        {
            System.Console.WriteLine("Closing serial port...");
            if (mysw != null)
            {
                mysw.Close();
                mysw = null;
            }
            if (myfs != null)
            {
                myfs.Close();
                myfs = null;
            }
            System.Console.WriteLine("Serial port is closed");
        }

    }
}
