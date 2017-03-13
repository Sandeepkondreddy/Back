using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
namespace WAP3
{
    public partial class formTest : Form
    {
        string DatabaseVarient = classServerDetails.DBType;
        public formTest()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://172.168.0.45/rfid-web/");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.Proxy = null;
                request.Timeout = 5000;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        MessageBox.Show("Hi");
                        DataSet ds = new DataSet();
                        if (DatabaseVarient == "SQL")
                            details.GetReaderDetailsSQL(classLogin.ReaderIP);
                        else if (DatabaseVarient == "Oracle")
                            ds = details.GetReaderDetailsOracle(classLogin.ReaderIP);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("Config Details Not found.");
                            classLog.writeLog("Message @:Config Details Not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}