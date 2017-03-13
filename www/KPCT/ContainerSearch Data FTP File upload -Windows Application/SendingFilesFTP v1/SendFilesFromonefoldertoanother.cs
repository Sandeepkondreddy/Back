using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace SendingFiles
{
    public partial class SendFilesFromonefoldertoanother : Form
    {
        private Timer _timer;
        private DateTime _lastRun = DateTime.Now.AddDays(-1);

        [DllImport("advapi32.DLL", SetLastError = true)]
        public static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        public SendFilesFromonefoldertoanother()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            Timer MyTimer = new Timer();
            MyTimer.Interval = (5 * 60 * 1000); // 6 hrs6 * 60
            MyTimer.Tick += new EventHandler(timer1_Tick);
            MyTimer.Start();
        }

        private void SendFiles()
        {
            IntPtr admin_token = default(IntPtr);
            WindowsIdentity wid_current = WindowsIdentity.GetCurrent();
            WindowsIdentity wid_admin = null;
            WindowsImpersonationContext wic = null;
            try
            {
                //MessageBox.Show("Copying file...");
                lblPath.Text = "E:\\Source to 172.168.8.176\\D$\\Target";
                lblMsg.Text = "Copying file...";
                if (LogonUser("administrator", "kpcl", "itkpcl789$", 9, 0, ref admin_token) != 0)
                {
                    wid_admin = new WindowsIdentity(admin_token);
                    wic = wid_admin.Impersonate();
                    System.IO.File.Copy("E:\\Source\\file.xlsx", "\\\\172.168.8.176\\D$\\Target\\file.xlsx", true);
                    //MessageBox.Show("Copy succeeded");
                    lblMsg.Text = "Copy succeeded";
                    lblCopiedTime.Text = DateTime.Now.ToString();
                    button1.Enabled = false;
                    button2.Enabled = true;
                }
                else
                {
                    //MessageBox.Show("Copy Failed");
                    lblMsg.Text = "Copy Failed";
                    button1.Enabled = true;
                    button2.Enabled = false;
                }
            }
            catch (System.Exception se)
            {
                int ret = Marshal.GetLastWin32Error();
               // MessageBox.Show(ret.ToString(), "Error code: " + ret.ToString());
               // MessageBox.Show(se.Message);
                lblMsg.Text = ret.ToString();
                lblMsg.Text = se.Message;
                button1.Enabled = true;
                button2.Enabled = false;
            }
            finally
            {
                if (wic != null)
                {
                    wic.Undo();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblPath.Text = "E:\\Source to 172.168.8.176\\D$\\Target";
            timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
            SendFiles();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            lblMsg.Text = "File Copying is stoped...";
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
        }

    }
}
