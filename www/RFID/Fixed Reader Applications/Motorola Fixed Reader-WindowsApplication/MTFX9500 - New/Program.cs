using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CS_RFID3_Host_Sample1.Live;

//namespace RFID3Test_PCApp
namespace CS_RFID3_Host_Sample1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FixedReader());//OfflineFixedReader//PortEntyGeneralrifdDB// GTWBFixedReader//PortEntryandGeneralReader
            //Application.Run(new formTest());
        }
    }
}