using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;
using System.Data.SqlClient;
using Symbol.RFID3;
using System.IO;

namespace Motorola_Fixed_Reader_Traffic_Lights
{
    public partial class formTrafficLights : Form
    {
        internal RFIDReader m_ReaderAPI;
        internal bool m_IsConnected;
        private Hashtable m_TagTable;
        public formTrafficLights()
        {
            InitializeComponent();
        }

        #region Form Load
        private void formTrafficLights_Load(object sender, EventArgs e)
        {
            txtReaderIP.Text = ConfigurationManager.AppSettings["ReaderIP"];
            txtReaderPort.Text = ConfigurationManager.AppSettings["ReaderPort"];
            //MyDataCollection.ReaderIP = ConfigurationManager.AppSettings["ReaderIP"]; 
            this.Text = ConfigurationManager.AppSettings["AntennaLoc1"] + " and " + ConfigurationManager.AppSettings["AntennaLoc2"];
            connectBackgroundWorker.RunWorkerAsync("Connect");
        }
        #endregion

        #region Form Closing
        private void formTrafficLights_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_IsConnected)
                {
                    m_ReaderAPI.Disconnect();
                }
                this.Dispose();
            }
            catch (Exception ex)
            {
                functionCallStatusLabel.Text = ex.Message;
            }
        }
        #endregion

        private void readButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_IsConnected)
                {
                    if (readButton.Text == "Start Reading")
                    {
                        //m_ReaderAPI.Actions.Inventory.Perform(null, null, null);
                        m_ReaderAPI = new RFIDReader(txtReaderIP.Text.ToString(), Convert.ToUInt32(txtReaderPort.Text.ToString()), 0);
                        m_TagTable.Clear();
                        //m_TagTotalCount = 0;

                        readButton.Text = "Stop Reading";
                        //m_ReaderAPI.Config.GPO[1].PortState = GPOs.GPO_PORT_STATE.TRUE;
                    }
                    else if (readButton.Text == "Stop Reading")
                    {
                        // m_ReaderAPI.Config.GPO[1].PortState = GPOs.GPO_PORT_STATE.FALSE;
                        if (m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0)
                        {
                            m_ReaderAPI.Actions.TagAccess.OperationSequence.StopSequence();
                        }
                        else
                        {
                            m_ReaderAPI.Actions.Inventory.Stop();
                        }
                        readButton.Text = "Start Reading";
                    }
                }
                else
                {
                    functionCallStatusLabel.Text = "Please connect to a reader";
                }
            }
            catch (InvalidOperationException ioe)
            {
                functionCallStatusLabel.Text = ioe.Message;
            }
            catch (InvalidUsageException iue)
            {
                functionCallStatusLabel.Text = iue.Info;
            }
            catch (OperationFailureException ofe)
            {
                functionCallStatusLabel.Text = ofe.Result + ":" + ofe.StatusDescription;
            }
            catch (Exception ex)
            {
                functionCallStatusLabel.Text = ex.Message;
            }
        }

        private void connectBackgroundWorker_DoWork(object sender, DoWorkEventArgs workEventArgs)
        {
            connectBackgroundWorker.ReportProgress(0, workEventArgs.Argument);

            if ((string)workEventArgs.Argument == "Connect")
            {
                m_ReaderAPI = new RFIDReader(txtReaderIP.Text.ToString(), uint.Parse(txtReaderPort.Text.ToString()), 0);

                try
                {
                    m_ReaderAPI.Connect();
                    m_IsConnected = true;
                    workEventArgs.Result = "Connect Succeed";

                }
                catch (OperationFailureException operationException)
                {
                    workEventArgs.Result = operationException.Result;
                }
                catch (Exception ex)
                {
                    workEventArgs.Result = ex.Message;
                }
            }
            else if ((string)workEventArgs.Argument == "Disconnect")
            {
                try
                {

                    m_ReaderAPI.Disconnect();
                    m_IsConnected = false;
                    workEventArgs.Result = "Disconnect Succeed";
                }
                catch (OperationFailureException ofe)
                {
                    workEventArgs.Result = ofe.Result;
                }
            }
        }

        private void connectBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }



    }
}
