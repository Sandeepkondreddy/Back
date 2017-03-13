using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace CS_RFID3_Host_Sample1
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //OleDbConnection dbConn = new OleDbConnection(ConfigurationManager.ConnectionStrings["oraconn"].ConnectionString);

            //String queryString = "select * from RFID.MTFX9500on ORDER BY SNO";

            //OleDbDataAdapter da = new OleDbDataAdapter(queryString, dbConn);
            //DataSet ds = new DataSet();
            //da.Fill(ds, "RFID.MTFX9500on");
            //dataGridView1.DataSource = ds.Tables["RFID.MTFX9500on"].DefaultView;

            string Conn = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(Conn);
            String queryString = "select * from MTFX9500on ORDER BY SNO";

            SqlDataAdapter da = new SqlDataAdapter(queryString, sqlCon);
            DataSet ds = new DataSet();
            da.Fill(ds, "MTFX9500on");
            dataGridView1.DataSource = ds.Tables["MTFX9500on"].DefaultView;

        }
    }
}
