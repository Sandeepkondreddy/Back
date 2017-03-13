using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace WAP3
{
    public partial class formUpdateTime : Form
    {
        string sqlConnection = classServerDetails.WifiSQLConnection;
        public formUpdateTime()
        {
            InitializeComponent();
        }
        //This will be the method you need to set the system time
        [DllImport("coredll.dll", SetLastError = true)]
        static extern bool SetLocalTime(ref SYSTEMTIME lpSystemTime);

        //This is the object you need to fill before you use SetLocalTime
        public struct SYSTEMTIME
        {
            public short year;
            public short month;
            public short dayOfWeek;
            public short day;
            public short hour;
            public short minute;
            public short second;
            public short milliseconds;
        }
        //And this is the final method to execute
        public static void SetSystemDateTime(DateTime dt)
        {
            SYSTEMTIME systime;
            systime.year = (short)dt.Year;
            systime.month = (short)dt.Month;
            systime.day = (short)dt.Day;

            systime.hour = (short)dt.Hour;
            systime.minute = (short)dt.Minute;
            systime.second = (short)dt.Second;
            systime.milliseconds = (short)dt.Millisecond;
            systime.dayOfWeek = (short)dt.DayOfWeek;

            SetLocalTime(ref systime);
        }
        private void btnSetTime_Click(object sender, EventArgs e)
        {
            string CONN_STRING = sqlConnection;
            SqlConnection dbCon = new SqlConnection(CONN_STRING);
            string cmd = "select GetDATE()";
            dbCon.Open();
            DateTime dts = System.DateTime.Now;
            SqlDataAdapter da = new SqlDataAdapter(cmd, dbCon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[0]);
            SetSystemDateTime(dt);
            MessageBox.Show("Time Updated.");
            dbCon.Close();
        }
    }
}