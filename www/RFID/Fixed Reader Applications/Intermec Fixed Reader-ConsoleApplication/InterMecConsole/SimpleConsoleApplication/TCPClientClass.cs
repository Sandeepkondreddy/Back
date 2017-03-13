using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace SimpleConsoleApplication
{
    //******************************************************
    //** This code is not used and is provided for example purposes
    //******************************************************
    
    class TCPClientClass
    {
        TcpClient tc = null;
        NetworkStream ns = null;
        StreamWriter sw = null;
        StreamReader sr = null;

        public bool ConnectToTCPServerTestMessage(string ip, int port)
        {
            //this is test code
            bool b = false;

            using (TcpClient tcp = new TcpClient())
            {
                IAsyncResult ar = tcp.BeginConnect("10.10.10.7", 80, null, null);
                System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                try
                {
                    if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5), false))
                    {
                        tcp.Close();
                        b = false;
                    }
                    else
                    {
                        b = true;
                        tcp.EndConnect(ar);
                    }
                }
                finally
                {
                    wh.Close();
                }
            }

            return b;


            try
            {
                System.Console.WriteLine("testing network");

                //find a port that works 6000-61000
                //"10.10.10.7", 80
                tc = new TcpClient(ip, port);
                ns = tc.GetStream();
                sw = new StreamWriter(ns);
                //sr = new StreamReader();

                //send data to pc
                sw.WriteLine("hello\r\n");
                sw.Flush();

                //sr.Close();
                sw.Close();
                ns.Close();
                tc.Close();
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("Exception testing network:" + ee.Message);
                return false;
            }
            System.Console.WriteLine("testing network completed");
            return true;
        }

        public bool SendMsgTCPServer(string ip, int port, string msg)
        {
            try
            {
                System.Console.WriteLine("testing network");

                //find a port that works 6000-61000
                tc = new TcpClient(ip, port);
                ns = tc.GetStream();
                sw = new StreamWriter(ns);
                //sr = new StreamReader(ns);

                //send data to pc
                sw.WriteLine(msg);
                sw.Flush();

                //sr.Close();
                sw.Close();
                ns.Close();
                tc.Close();
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("Exception testing network:" + ee.Message);
                return false;
            }
            System.Console.WriteLine("testing network completed");
            return true;
        }

        public void DisposeOfTCPClient()
        {
            //not used at this time
            try
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
                if (sw != null)
                {
                    sw.Close();
                    sw = null;
                }
                if (ns != null)
                {
                    ns.Close();
                    ns = null;
                }
                if (tc != null)
                {
                    tc.Close();
                    tc = null;
                }
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("Exception disposing of TCP client:");
                System.Console.WriteLine(ee.Message);
            }
        }
    }
    
}
