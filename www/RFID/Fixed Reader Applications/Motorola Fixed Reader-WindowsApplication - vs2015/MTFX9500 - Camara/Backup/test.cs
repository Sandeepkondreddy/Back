using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;



namespace CS_RFID3_Host_Sample1
{
    public partial class test : Form
    {

        Ping ping = new Ping();
        PingOptions options = new PingOptions { DontFragment = true };

        //just need some data. this sends 10 bytes.
        byte[] buffer = Encoding.ASCII.GetBytes(new string('z', 10));
        string host = "172.168.8.102";


        public test()
        {
            InitializeComponent();
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
            try
            {
                var reply = ping.Send(host, 60, buffer, options);
                if (reply == null)
                {
                    MessageBox.Show("Reply was null");
                    return;
                }

                if (reply.Status == IPStatus.Success)
                {
                    //MessageBox.Show("Ping was successful.");
                    lblmsg.Text = "Network connected!";
                }
                else
                {
                    //MessageBox.Show("Ping failed.");
                    lblmsg.Text = "Network disconnected!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
                //MessageBox.Show("Network connected!");
                lblmsg.Text = "Network connected!";
            else
                //MessageBox.Show("Network disconnected!");
                lblmsg.Text = "Network disconnected!";
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            //NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
            //try
            //{
            //    var reply = ping.Send(host, 60, buffer, options);
            //    if (reply == null)
            //    {
            //        MessageBox.Show("Reply was null");
            //        return;
            //    }

            //    if (reply.Status == IPStatus.Success)
            //    {
            //        MessageBox.Show("Ping was successful.");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Ping failed.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

    }
}
