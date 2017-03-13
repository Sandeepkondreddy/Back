using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using nsAlienRFID2;
using System.Diagnostics;
using System.Data.OleDb;

namespace RFIDNotifications
{
    public partial class RFIDNotification : Form
    {
        public RFIDNotification()
        {
            InitializeComponent();
        }
        int TruckFlag = 0;
        private String msTags;
        private clsReader mReader;
        string oldtag, oldtrk, newtrk, newtag, newtag1, CREATEDDATE, sysip, systemip, fixip, loc;
        SqlCommand cmd;
        private class MyStoredReaderState
        {
            internal string userName = null;
            internal string password = null;
            internal int commandPort = 23;
            internal string readerAddress = null;
            internal string readerName = null;
            internal string notifyAddress = null;
            internal string notifyMode = null;
            internal string notifyTime = null;
            internal string tagStreamMode = null;
            internal string ioStreamMode = null;
            internal string tagStreamAddress = null;
            internal string ioStreamAddress = null;
            internal string autoMode = null;
            internal string notifyFormat = null;
            internal string tagStreamFormat = null;
            internal string ioStreamFormat = null;
            internal string tagListMillis = null;
            internal string autoWaitOutput = null;
            internal string autoWorkOutput = null;
            internal string autoTrueOutput = null;
            internal string autoFalseOutput = null;
            internal string ioStreamKeepAliveTime = null;
            internal string tagStreamKeepAliveTime = null;

            internal MyStoredReaderState() { }
        }

        private int miReadersCount = 0;

        CAlienServer[] mServers = new CAlienServer[3];
        clsReaderMonitor mDiscoverer = null;

        const int NOTIFY_PORT = 7797;
        const int TAGSTREAM_PORT = 7798;
        const int IOSTREAM_PORT = 7799;

        Hashtable mClients = new Hashtable();		// readers' IP Addresses as keys; clsReader objects as items
        Hashtable mStoredStates = new Hashtable();	// readers' IP Addresses as keys; MyStoredReaderState objects as items

        static readonly object moCurrentReadersLock = new object();

        private delegate void displayMessageDlgt(string msg);
        private delegate void addNewReaderDlgt(MyStoredReaderState rs);

        private int miOldNodeIndex = -1;	// for showing different tooltips for different nodes


        private void RFIDNotification_Load(object sender, EventArgs e)
        {
            // If you have more than 1 IP Address on your PC use the two parameters' constructor instead.
            mServers[0] = new CAlienServer(7797);	// Notifications
            mServers[1] = new CAlienServer(7798);	// TagStream
            mServers[2] = new CAlienServer(7799);	// IOStream

            chlServers.Items.Add("Notify Server", false);
            chlServers.Items.Add("TagStream Server", false);
            chlServers.Items.Add("IOStream Server", false);

            
            mDiscoverer = new clsReaderMonitor();

            mClients = new Hashtable();
            for (int i = 0; i < 3; i++)
            {
                addServerEvents(i);
            }
            mDiscoverer.ReaderAdded += new nsAlienRFID2.clsReaderMonitor.ReaderAddedEventHandler(mDiscoverer_ReaderAdded);

        }
     
        private void RFIDNotification_FormClosing(object sender, FormClosingEventArgs e)
        {
            lock (moCurrentReadersLock)
            {
                foreach (object o in mStoredStates.Keys)
                {
                    string address = o as string;
                    if (mClients.ContainsKey(address))
                    {
                        try
                        {
                            MyStoredReaderState rs = mStoredStates[address] as MyStoredReaderState;

                            clsReader reader = mClients[address] as clsReader;

                            if (!reader.IsConnected)
                                reader.ConnectAndLogin(address, rs.commandPort, rs.userName, rs.password);

                            restoreReaderState(rs, ref reader);
                            reader.Dispose();
                        }
                        catch { }
                    }
                }
            }
        }

        private void addServerEvents(int idx)
        {
            mServers[idx].ServerMessageReceived += new CAlienServer.ServerMessageReceivedEventHandler(mServer_ServerMessageReceived);
            mServers[idx].ServerConnectionEstablished += new CAlienServer.ServerConnectionEstablishedEventHandler(mServer_ServerConnectionEstablished);
            mServers[idx].ServerConnectionEnded += new CAlienServer.ServerConnectionEndedEventHandler(mServer_ServerConnectionEnded);
            mServers[idx].ServerSocketError += new CAlienServer.ServerSocketErrorEventHandler(mServer_ServerSocketError);
        }

        private void displayText(String data)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    object[] temp = { data };
                    this.Invoke(new displayMessageDlgt(displayText), temp);
                    return;
                }
                else
                {
                    txtNotifications.Text += data.Replace("\0", "") + "\r\n";

                    NotifyInfo ni = null;
                    AlienUtils.ParseNotification(data, out ni);
                    if (ni != null)
                    {
                        lblReaderName.Text = ni.ReaderName;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in the DiscplayText(): " + ex.Message);
            }
        }

        private void decreaseConnectionsCount(string msg)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    object[] temp = { msg };
                    this.Invoke(new displayMessageDlgt(decreaseConnectionsCount), temp);
                    return;
                }
                else
                {
                    int count = int.Parse(lblConnections.Text);
                    if (count > 0)
                        count--;
                    lblConnections.Text = count.ToString();
                    txtNotifications.Text += "Connection ended: " + msg.Replace("\0", "") + "\r\n";
                    txtNotifications.Clear();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in the decreaseConnectionsCount(): " + ex.Message);
            }
        }
        private void increaseConnectionsCount(string msg)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    object[] temp = { msg };
                    this.Invoke(new displayMessageDlgt(increaseConnectionsCount), temp);
                    return;
                }
                else
                {
                    int count = int.Parse(lblConnections.Text);
                    count++;
                    lblConnections.Text = count.ToString();
                    txtNotifications.Text += "Connection added: " + msg.Replace("\0", "") + "\r\n";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in the increaseConnectionsCount(): " + ex.Message);
            }
        }

        private void btnAddReader_Click(object sender, EventArgs e)
        {
            AddReader dlg = new AddReader();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            string sIPaddress = dlg.txtAddress.Text.Trim();
            string sPort = dlg.txtPort.Text;
            string sUsername = dlg.txtUsername.Text;
            string sPassword = dlg.txtPassword.Text;

            MyStoredReaderState rs = new MyStoredReaderState();
            rs.readerAddress = sIPaddress;
            try
            {
                rs.commandPort = int.Parse(sPort);
                rs.userName = sUsername;
                rs.password = sPassword;

                lock (moCurrentReadersLock)
                {
                    if (!mStoredStates.ContainsKey(sIPaddress))
                        mStoredStates.Add(sIPaddress, rs);
                    else
                        mStoredStates[sIPaddress] = rs;

                    addNewReader(rs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNotifications.Clear();
        }
        private void mServer_ServerConnectionEstablished(string id)
        {
            increaseConnectionsCount(id);
        }
        private void mServer_ServerConnectionEnded(string id)
        {
            decreaseConnectionsCount(id);
        }

        private void mServer_ServerSocketError(string msg)
        {
            displayText(msg);
        }


        private void chkAddDiscovered_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkAddDiscovered.Checked)
                mDiscoverer.StartListening();
            else
                mDiscoverer.StopListening();
        }

        private void mDiscoverer_ReaderAdded(IReaderInfo data)
        {
            if (mStoredStates.ContainsKey(data.IPAddress))
                return;

            MyStoredReaderState rs = new MyStoredReaderState();
            rs.readerAddress = data.IPAddress;
            rs.commandPort = data.TelnetPort;
            rs.readerName = data.Name;
            lock (moCurrentReadersLock)
            {
                if (!mStoredStates.ContainsKey(data.IPAddress))
                    mStoredStates.Add(data.IPAddress, rs);

                addNewReader(rs);
            }
        }


        private void addNewReader(MyStoredReaderState rs)
        {
            if (this.InvokeRequired)
            {
                object[] temp = { rs };
                try
                {
                    this.Invoke(new addNewReaderDlgt(addNewReader), temp);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                return;
            }
            else
            {
                bool found = false;
                foreach (TreeNode n in treeView1.Nodes)
                {
                    if (n.Text == rs.readerAddress)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    TreeNode[] nodeChildren = new TreeNode[3];
                    nodeChildren[0] = new TreeNode("Notify");
                    nodeChildren[1] = new TreeNode("TagStream");
                    nodeChildren[2] = new TreeNode("IOStream");
                    TreeNode node = new TreeNode(rs.readerAddress, nodeChildren);
                    node.Tag = rs.readerAddress;

                    miReadersCount++;

                    treeView1.Nodes.Add(node);
                }
                lblReadersCount.Text = miReadersCount.ToString();
            }
        }

        private void connect(MyStoredReaderState myReaderState, ref clsReader reader)
        {
            if (myReaderState.userName == null)
                myReaderState.userName = "alien";
            if (myReaderState.password == null)
                myReaderState.password = "password";

            lock (moCurrentReadersLock)
            {
                reader.ConnectAndLogin(
                    myReaderState.readerAddress,
                    myReaderState.commandPort,
                    myReaderState.userName,
                    myReaderState.password);
            }
        }


        private void saveReaderState(MyStoredReaderState myReaderState)
        {
            string sIPaddress = myReaderState.readerAddress;

            clsReader reader = null;
            lock (moCurrentReadersLock)
            {
                try
                {
                    if (!mClients.ContainsKey(sIPaddress))
                    {
                        reader = new clsReader();
                        connect(myReaderState, ref reader);
                        if (!reader.IsConnected)
                        {
                            throw new Exception("Can't connect to the reader.");
                        }

                        string r = null;

                        try { r = reader.TagStreamAddress; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.tagStreamAddress = r; r = null; }

                        try { r = reader.TagStreamMode; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.tagStreamMode = r; r = null; }

                        try { r = reader.IOStreamAddress; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.ioStreamAddress = r; r = null; }

                        try { r = reader.IOStreamMode; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.ioStreamMode = r; r = null; }

                        try { r = reader.NotifyAddress; }
                        catch { }
                        if ((r != null) && r.IndexOf("Error") == -1)
                        { myReaderState.notifyAddress = r; r = null; }

                        try { r = reader.NotifyMode; }
                        catch { }
                        if ((r != null) && r.IndexOf("Error") == -1)
                        { myReaderState.notifyMode = r; r = null; }

                        try { r = reader.NotifyTime; }
                        catch { }
                        if ((r != null) && r.IndexOf("Error") == -1)
                        { myReaderState.notifyTime = r; r = null; }

                        try { r = reader.NotifyFormat; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.notifyFormat = r; r = null; }

                        try { r = reader.TagStreamFormat; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.tagStreamFormat = r; r = null; }

                        try { r = reader.IOStreamFormat; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.ioStreamFormat = r; r = null; }

                        try { r = reader.TagListMillis; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.tagListMillis = r; r = null; }

                        try { r = reader.AutoWaitOutput; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.autoWaitOutput = r; r = null; }

                        try { r = reader.AutoWorkOutput; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.autoWorkOutput = r; r = null; }

                        try { r = reader.AutoTrueOutput; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.autoTrueOutput = r; r = null; }

                        try { r = reader.AutoFalseOutput; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.autoFalseOutput = r; r = null; }

                        try { r = reader.IOStreamKeepAliveTime; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.ioStreamKeepAliveTime = r; r = null; }

                        try { r = reader.TagStreamKeepAliveTime; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.tagStreamKeepAliveTime = r; r = null; }

                        try { r = reader.AutoMode; }
                        catch { }
                        if ((r != null) && (r.IndexOf("Error") == -1))
                        { myReaderState.autoMode = r; r = null; }

                        mClients.Add(sIPaddress, reader);
                    }
                }
                catch (Exception ex)
                {
                    if (mClients.ContainsKey(sIPaddress))
                        mClients.Remove(sIPaddress);

                    if ((reader != null) && (reader.IsConnected))
                        reader.Dispose();
                }
            }// releasing lock
        }

        private void restoreReaderState(MyStoredReaderState rs, ref clsReader reader)
        {
            try { reader.AutoMode = "Off"; }
            catch { }
            if (rs.notifyMode != null)
                try { reader.NotifyMode = rs.notifyMode; }
                catch { }
            if (rs.tagStreamMode != null)
                try { reader.TagStreamMode = rs.tagStreamMode; }
                catch { }
            if (rs.ioStreamMode != null)
                try { reader.IOStreamMode = rs.ioStreamMode; }
                catch { }
            if (rs.notifyTime != null)
                try { reader.NotifyTime = rs.notifyTime; }
                catch { }
            if (rs.notifyAddress != null)
                try { reader.NotifyAddress = rs.notifyAddress; }
                catch { }
            if (rs.tagStreamAddress != null)
                try { reader.TagStreamAddress = rs.tagStreamAddress; }
                catch { }
            if (rs.ioStreamAddress != null)
                try { reader.IOStreamAddress = rs.ioStreamAddress; }
                catch { }
            if (rs.notifyFormat != null)
                try { reader.NotifyFormat = rs.notifyFormat; }
                catch { }
            if (rs.tagStreamFormat != null)
                try { reader.TagStreamFormat = rs.tagStreamFormat; }
                catch { }
            if (rs.ioStreamFormat != null)
                try { reader.IOStreamFormat = rs.ioStreamFormat; }
                catch { }
            if (rs.tagListMillis != null)
                try { reader.TagListMillis = rs.tagListMillis; }
                catch { }
            if (rs.autoWaitOutput != null)
                try { reader.AutoWaitOutput = rs.autoWaitOutput; }
                catch { }
            if (rs.autoWorkOutput != null)
                try { reader.AutoWorkOutput = rs.autoWorkOutput; }
                catch { }
            if (rs.autoTrueOutput != null)
                try { reader.AutoTrueOutput = rs.autoTrueOutput; }
                catch { }
            if (rs.autoFalseOutput != null)
                try { reader.AutoFalseOutput = rs.autoFalseOutput; }
                catch { }
            if (rs.ioStreamKeepAliveTime != null)
                try { reader.IOStreamKeepAliveTime = rs.ioStreamKeepAliveTime; }
                catch { }
            if (rs.tagStreamKeepAliveTime != null)
                try { reader.TagStreamKeepAliveTime = rs.tagStreamKeepAliveTime; }
                catch { }
            if (rs.autoMode != null)
                try { reader.AutoMode = rs.autoMode; }
                catch { }
        }


        private void chlServers_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (e.NewValue == CheckState.Checked)
                mServers[e.Index].StartListening();
            else
                mServers[e.Index].StopListening();

            this.Cursor = Cursors.Default;
        }


        private void treeView1_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            string tag = e.Node.Tag as string;
            if ((tag != null) && (tag != ""))
            {
                if (e.Node.Checked)
                {
                    lock (moCurrentReadersLock)
                    {
                        if (!mClients.ContainsKey(tag))	// configuring reader for the first time
                        {
                            saveReaderState(mStoredStates[tag] as MyStoredReaderState);
                            clsReader reader = null;
                            reader = mClients[tag] as clsReader;
                            if ((reader == null) || (!reader.IsConnected))
                            {
                                MessageBox.Show("Can't connect to the reader.");
                                treeView1.Nodes.Remove(e.Node);
                                lblReadersCount.Text = (--miReadersCount).ToString();
                            }
                            else
                            {
                                this.Cursor = Cursors.WaitCursor;

                                // any of these can throw, especially for older readers
                                try { reader.NotifyAddress = mServers[0].NotificationHost; }
                                catch { }
                                try { reader.TagStreamAddress = mServers[1].NotificationHost; }
                                catch { }
                                try { reader.IOStreamAddress = mServers[2].NotificationHost; }
                                catch { }
                                try { reader.TagListMillis = "off"; }
                                catch { }

                                try { reader.AutoMode = "On"; }
                                catch { }

                                // cusotmize these properties (and any of other related ones) as you desire
                                try { reader.NotifyTime = "42000"; }
                                catch { }
                                try { reader.PersistTime = "42000"; }
                                catch { }
                                try { reader.NotifyFormat = "Text"; }
                                catch { }
                                try { reader.NotifyHeader = "Off"; }
                                catch { }
                                try { reader.TagStreamFormat = "Terse"; }
                                catch { }
                                try { reader.IOStreamFormat = "Text"; }
                                catch { }
                                try { reader.IOStreamKeepAliveTime = "42000"; }
                                catch { }
                                try { reader.TagStreamKeepAliveTime = "42000"; }
                                catch { }
                                try { reader.AutoWaitOutput = "-1"; }
                                catch { }
                                try { reader.AutoWorkOutput = "-1"; }
                                catch { }
                                try { reader.AutoTrueOutput = "-1"; }
                                catch { }
                                try { reader.AutoFalseOutput = "-1"; }
                                catch { }

                                this.Cursor = Cursors.Default;
                            }
                            e.Node.Expand();
                        }
                    }// release lock
                }
                else
                {
                    e.Node.Checked = true;
                }
            }
            else	// Start / Stop automatic messages from the reader
            {
                tag = e.Node.Parent.Tag as String;
                lock (moCurrentReadersLock)
                {
                    if (!mClients.ContainsKey(tag))
                        e.Node.Parent.Checked = true;

                    clsReader reader = mClients[tag] as clsReader;
                    if ((reader != null) && !(reader.IsConnected))
                    {
                        connect((mStoredStates[tag] as MyStoredReaderState), ref reader);
                        if (!reader.IsConnected)
                        {
                            MessageBox.Show("Can't connect to the reader.");
                            treeView1.Nodes.Remove(e.Node.Parent);
                            lblReadersCount.Text = (--miReadersCount).ToString();
                            return;
                        }
                    }
                    string cmd = e.Node.Text + "Mode";
                    string strToSend = "Set " + cmd + " = " + (e.Node.Checked ? "On" : "Off");

                    try { reader.SendReceive(strToSend, false); }
                    catch (Exception ex)
                    {
                        if (e.Node.Checked)
                        {
                            MessageBox.Show(ex.Message);
                            e.Node.Checked = false;
                        }
                    }
                } // release lock
            }
        }


        private void treeView1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {											// Different tooltips for different nodes.

            TreeNode tn = this.treeView1.GetNodeAt(e.X, e.Y);

            if (tn != null)
            {
                int currentNodeIndex = tn.Index;
                if (currentNodeIndex != miOldNodeIndex)
                {
                    miOldNodeIndex = currentNodeIndex;
                    if (this.toolTip1 != null && this.toolTip1.Active)
                        this.toolTip1.Active = false; //turn it off 

                    string textToShow = "";
                    if (tn.Tag == null)
                    {
                        if (tn.Checked)
                            textToShow = "Uncheck to set the " + tn.Text + "Mode Off.";
                        else
                            textToShow = "Check to set the " + tn.Text + "Mode On";
                    }
                    else if (!tn.Checked)
                    {
                        textToShow = "Check to configure reader.";
                    }
                    else
                        textToShow = "";

                    this.toolTip1.SetToolTip(this.treeView1, textToShow);
                    this.toolTip1.Active = true; //make it active so it can show 
                }
            }
        }



        private void mServer_ServerMessageReceived(string msg)
        {
            //string tag=SELECT RIGHT('HELLO WORLD', 3);
            string Tagno = msg.Replace(" ", "").Substring(msg.Length - 37).Substring(0, 5);
            // string Tagno = msg.Replace(" ", "").Substring(17, 5);
            // string Tagno = msg.Replace(" ", "").Substring(36, 5);
            //   txtTagno.Text = Tagno;
           
            sysip = ConfigurationManager.AppSettings["systemip"];
            fixip = ConfigurationManager.AppSettings["fixreaderip"];
            loc = ConfigurationManager.AppSettings["location"];
            string Ant1 = ConfigurationManager.AppSettings["ant1"];
            string Ant2 = ConfigurationManager.AppSettings["ant2"];
            try
            {
                int execquery = 0;
                CheckForIllegalCrossThreadCalls = false;
                if(ValidateTag(Tagno))
                {
                    string Conn = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
                    SqlConnection sqlCon = new SqlConnection(Conn);
                    string sqlinternalConn = ConfigurationManager.ConnectionStrings["sqlInternalconn"].ConnectionString;
                    SqlConnection internaldbCon = new SqlConnection(sqlinternalConn);
                    if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
                    {
                        sqlCon.Open();
                    }
                    if (internaldbCon.State == ConnectionState.Closed || internaldbCon.State == ConnectionState.Broken)
                    {
                        internaldbCon.Open();
                    }
                    //string strNonQuery_Parking = "Insert into MTFX9500on ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strParkingIPaddress + "')";
                    //SqlCommand cmd = new SqlCommand(strNonQuery_Parking, sqlCon);
                    if (TruckFlag == 1)
                    {
                        //string strNonQuery_Parking = "Insert into [IFMS150311].[dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Tagno + "'" + "," + "'" + Ant1 + "'" + "," + "'" + loc + "'" + ",GETDATE(),'" + txtTruckno.Text + "','" + fixip + "')";
                        string strNonQuery_Parking = "Insert into [dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Tagno + "'" + "," + "'" + Ant1 + "'" + "," + "'" + loc + "'" + ",GETDATE(),'" + txtTruckno.Text + "','" + fixip + "')";
                        cmd = new SqlCommand(strNonQuery_Parking, internaldbCon);
                        execquery = cmd.ExecuteNonQuery();
                    }
                    else if (TruckFlag == 2)
                    {
                        string strNonQuery_Parking = "Insert into [dbo].[MTFX9500BYROAD] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Tagno + "'" + "," + "'" + Ant1 + "'" + "," + "'" + loc + "'" + ",GETDATE(),'" + txtTruckno.Text + "','" + fixip + "')";
                        cmd = new SqlCommand(strNonQuery_Parking, sqlCon);
                        execquery = cmd.ExecuteNonQuery();
                    }
                    else
                    {

                    }
                    
                    if (execquery >= 1)
                    {
                        txtNotifications.Text += " has read and Parking-In Time captured successfully";
                        
                        sqlCon.Close();
                      
                    }
                    else
                    {
                        //txtNotifications.Text += strErrorMessage_Parking;
                       
                    }
                }
                else
                {

                }

                //savedata();
            }
            catch (Exception ex)
            {
                txtNotifications.Text += ex.Message;
            }

            if (msg != null)
                displayText(msg.Insert(msg.IndexOf(" ") + 1, "\r\n"));
        }

        private bool ValidateTag(string TagNo)
        {
            try
            {
                string sqlConn = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
                SqlConnection dbCon = new SqlConnection(sqlConn);
                string sqlinternalConn = ConfigurationManager.ConnectionStrings["sqlInternalconn"].ConnectionString;
                SqlConnection internaldbCon = new SqlConnection(sqlinternalConn);
                sysip = ConfigurationManager.AppSettings["systemip"];
                fixip = ConfigurationManager.AppSettings["fixreaderip"];
                loc = ConfigurationManager.AppSettings["location"];

                //string cmdd = "select TOP 1  [Tag No],[Truck No] from Tag_Register where [Tag No]='" + Tagno + "' and [TransctionStatus]='P' order by [Reg Time] desc";
                //SqlDataAdapter da = new SqlDataAdapter(cmdd, sqlConn);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                if (dbCon.State == ConnectionState.Closed || dbCon.State == ConnectionState.Broken)
                {
                    dbCon.Open();
                }
                if (internaldbCon.State == ConnectionState.Closed || internaldbCon.State == ConnectionState.Broken)
                {
                    internaldbCon.Open();
                }
                //string command = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from [IFMS150311].[dbo].[MTFX9500InternalFleet] where  [wbno] ='" + loc + "'  order by readdt desc";
                string command = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from  [dbo].[MTFX9500InternalFleet] where  [wbno] ='" + loc + "'  order by readdt desc";
                SqlDataAdapter da = new SqlDataAdapter(command, internaldbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string command1 = "select  TOP 1 [tagno],[Ant],[wbno],[readdt] from [dbo].[MTFX9500BYROAD] where  [wbno] ='" + loc + "'  order by readdt desc";
                SqlDataAdapter dar = new SqlDataAdapter(command1, dbCon);
                DataSet dsr = new DataSet();
                dar.Fill(dsr);

                if (ds.Tables[0].Rows.Count > 0)//check in internal fleet
                {
                    newtag = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    newtag = "0";
                }

                if (dsr.Tables[0].Rows.Count > 0)//check in By Road
                {
                    newtag1 = dsr.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    newtag1 = "0";
                }

                //string com1 = "SELECT [FleetNo]  FROM [IFMS150311].[dbo].[tmst_FleetMaster]a, [IFMS150311].[dbo].[tmst_TagMaster] b where a.TagId = b.TagId and b.TagNo ='" + TagNo + "'";
                //SqlDataAdapter da1 = new SqlDataAdapter(com1, dbCon);
                string com1 = "select [TruckNo] from rfid.dbo.[Tag_TruckAllocation] A,  [dbo].[TruckMaster] B,  [dbo].[TagMaster] C where A.Tagid=C.Tagid and A.Tkid=B.Tkid and A.[RStatus]='Active' and C.TagNo='" + TagNo + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(com1, internaldbCon);
                DataSet ds1 = new DataSet();

                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    txtTagno.Text = TagNo;

                    txtTruckno.Text = ds1.Tables[0].Rows[0].ItemArray[0].ToString();

                    if (newtag == TagNo)
                    {
                        txtNotifications.Text += "Same Tag Found" + "\r\n";
                        return false;
                    }
                    else
                    {
                        TruckFlag = 1;
                        return true;
                    }

                }
                else
                {
                    if (newtag1 == TagNo)
                    {
                        txtNotifications.Text += "Same Tag Found" + "\r\n";
                        return false;
                    }
                    else
                    {
                        //inventoryList.Items.Clear();
                    
                        if (dbCon.State == ConnectionState.Closed || dbCon.State == ConnectionState.Broken)
                        {
                            dbCon.Open();
                        }
                        string com2 = "SELECT [Truck No]  FROM [dbo].[Tag_Register] where [Tag No] ='" + TagNo + "' and [TransctionStatus] = 'P' order by [Tag ID] desc";
                        SqlDataAdapter da2 = new SqlDataAdapter(com2, dbCon);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            TruckFlag = 2;
                            txtTagno.Text = TagNo;

                            txtTruckno.Text = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
                        }
                        else
                        {
                            TruckFlag = 0;
                            txtNotifications.Text = "Tag/Truck is Not Registered";
                            //return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void savedata()
        {
            CheckForIllegalCrossThreadCalls = false;

            newtag = txtTagno.Text;
            newtrk = txtTruckno.Text;
            sysip = ConfigurationManager.AppSettings["systemip"];
            fixip = ConfigurationManager.AppSettings["fixreaderip"];
            loc = ConfigurationManager.AppSettings["location"];
            //OracleConnection dbConn = new OracleConnection(ConfigurationManager.ConnectionStrings["oraconn"].ConnectionString);
            OleDbConnection dbConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["OledbConnectionInfo"].ConnectionString);

            dbConn.Open();
            //string command = "select  TOP 1 TAGID,TRUCKNO,SYSTEMIP,CREATEDDATE from (select * from  RFID_EXT_TRKDTS order by CREATEDDATE desc)";
            string command = "select   TOP 1 TAGID,TRUCKNO,SYSTEMIP,CREATEDDATE from  RFID_EXT_TRKDTS order by ID desc";
            OleDbDataAdapter da1 = new OleDbDataAdapter(command, dbConn);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            oldtag = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
            oldtrk = ds1.Tables[0].Rows[0].ItemArray[1].ToString();
            systemip = ds1.Tables[0].Rows[0].ItemArray[2].ToString();
            CREATEDDATE = ds1.Tables[0].Rows[0].ItemArray[3].ToString();
            //  txtremarks.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
            // if (newtag == oldtag && sysip==systemip )
            if (newtag == oldtag && newtrk == oldtrk)
            {
                txtNotifications.Text += "Same Tag Found" + "\r\n";
            }
            else
            {
                string Query = "Insert into RFID_EXT_TRKDTS (TAGID,TRUCKNO,GPNO,LOCATION,STATUS,SYSTEMIP,FixedreaderIP,REAMRKS,CREATEDBY,CREATEDDATE)Values ('" + txtTagno.Text + "','" + txtTruckno.Text + "','','" + loc + "','A','" + sysip + "','" + fixip + "','','" + loc + "','" + DateTime.Now.ToString() + "')";
                OleDbCommand cmd2 = new OleDbCommand(Query, dbConn);
                cmd2.ExecuteReader();
                txtNotifications.Text += "Tag Details Saved" + "\r\n";
                dbConn.Close();
                savetosql();
            }

        }

        private void savetosql()
        {
            //string locid;
            try
            {

                string olddate, oldtag;

                sysip = ConfigurationManager.AppSettings["systemip"];
                fixip = ConfigurationManager.AppSettings["fixreaderip"];
                loc = ConfigurationManager.AppSettings["location"];
                string sqlConn = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
                SqlConnection dbCon = new SqlConnection(sqlConn);
                if (dbCon.State == ConnectionState.Closed || dbCon.State == ConnectionState.Broken)
                {
                    dbCon.Open();
                }
                string command = "select  TOP 1 tagno,location,CreatedDate  from RFID_EXT_TRKDTS where  location ='" + loc + "'  order by sno desc";
                SqlDataAdapter da = new SqlDataAdapter(command, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                oldtag = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                olddate = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                //locid = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                if (oldtag == txtTagno.Text)
                {
                    txtNotifications.Text += "same tag found" + "\r\n";
                }
                else
                {

                    string str = "Insert into RFID_EXT_TRKDTS([tagno],[truckno],[readerip],[location],[status],[CreatedDate])" + "Values(" + "'" + txtTagno.Text + "'" + "," + "'" + txtTruckno.Text + "'" + "," + "'" + fixip + "'" + "," + "'" + loc + "'" + ",'A','" + DateTime.Now.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(str, dbCon);
                    cmd.ExecuteNonQuery();
                    if (fixip == "172.168.5.17")
                    {
                        string strupdate = "update top(1) [dbo].[RFID_EXT_TRKREP] set [PB DT]=GETDATE() where SNO IN (select  TOP 1 SNO  from RFID_EXT_TRKREP where [Tag No]='" + txtTagno.Text + "'   order by [Created DT] desc )";
                        SqlCommand cmdupdate = new SqlCommand(strupdate, dbCon);
                        cmdupdate.ExecuteReader();
                        dbCon.Close();
                    }
                    else if (fixip == "172.168.5.9")
                    {
                        string strupdate = "update top(1) [dbo].[RFID_EXT_TRKREP] set [WB9 DT]=GETDATE() where SNO IN (select  TOP 1 SNO  from RFID_EXT_TRKREP where [Tag No]='" + txtTagno.Text + "'   order by [Created DT] desc )";
                        SqlCommand cmdupdate = new SqlCommand(strupdate, dbCon);
                        cmdupdate.ExecuteReader();
                        dbCon.Close();
                    }
                    else if (fixip == "172.168.5.10")
                    {
                        string strupdate = "update top(1) [dbo].[RFID_EXT_TRKREP] set [WB10 DT]=GETDATE() where SNO IN (select  TOP 1 SNO  from RFID_EXT_TRKREP where [Tag No]='" + txtTagno.Text + "'   order by [Created DT] desc )";
                        SqlCommand cmdupdate = new SqlCommand(strupdate, dbCon);
                        cmdupdate.ExecuteReader();
                        dbCon.Close();
                    }
                    else if (fixip == "172.168.5.14")
                    {
                        string strupdate = "update top(1) [dbo].[RFID_EXT_TRKREP] set [ZONE4 DT]=GETDATE() where SNO IN (select  TOP 1 SNO  from RFID_EXT_TRKREP where [Tag No]='" + txtTagno.Text + "'   order by [Created DT] desc )";
                        SqlCommand cmdupdate = new SqlCommand(strupdate, dbCon);
                        cmdupdate.ExecuteReader();
                        dbCon.Close();
                    }

                    txtNotifications.Text += "tag log saved" + "\r\n";
                }


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }
    }
}
