using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;
using System.Threading;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Data.SqlClient;
using DelegateMgtSystem.DAL;

namespace DelegateMgtSystem.DelegatesEventDetails
{
    public partial class LocalDataSync : Form
    {
        static string Sqlmain = GlobalVariables.MainServerSqlconnection;
        static string SqlLocal = GlobalVariables.Sqlconnection;
        SqlConnection SqlMaincon,SqlLocalcon;
        DataSet Sqlds;
        public LocalDataSync()
        {
            InitializeComponent();
            SqlMaincon = new SqlConnection(Sqlmain);
            SqlLocalcon = new SqlConnection(SqlLocal);
        }

        private void LocalDataSync_Load(object sender, EventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlLocalcon.State == ConnectionState.Closed || SqlLocalcon.State == ConnectionState.Broken)
                {
                    SqlLocalcon.Open();
                }
                string strUpd = "Select [TrackingId],[DMId],[TagNo],[ReaderNo],[ReaderIP],[ReaderLocation],[ReadTime],[Status],[CreatedBy],[CreatedTime] FROM [dbo].[DelegateTrackingLog] where [DTLSyncFlag]='N'";

                SqlDataAdapter Sqlda = new SqlDataAdapter(strUpd, SqlLocalcon);
                Sqlds = new DataSet();
                Sqlda.Fill(Sqlds);
                if (Sqlds.Tables[0].Rows.Count > 0)
                {
                    var result = MessageBox.Show("DO YOU WANT TO UPLOAD THE DATA?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        foreach (DataRow dbNewRow in Sqlds.Tables[0].Rows)
                        {
                            if (SqlMaincon.State == ConnectionState.Closed || SqlMaincon.State == ConnectionState.Broken)
                            {
                                SqlMaincon.Open();
                            }
                            SqlCommand oracmd = new SqlCommand("insert into [dbo].[DelegateTrackingLog] ([DMId],[TagNo],[ReaderNo],[ReaderIP],[ReaderLocation],[ReadTime],[Status],[CreatedBy],[CreatedTime]) Values (@DMId,@TagNo,@ReaderNo,@ReaderIP,@ReaderLocation,@ReadTime,@Status,@CreatedBy,GetDate())", SqlMaincon);
                            oracmd.Parameters.AddWithValue("@DMId", dbNewRow.ItemArray[1].ToString());
                            oracmd.Parameters.AddWithValue("@TagNo", dbNewRow.ItemArray[2].ToString());
                            oracmd.Parameters.AddWithValue("@ReaderNo", dbNewRow.ItemArray[3].ToString());
                            oracmd.Parameters.AddWithValue("@ReaderIP", dbNewRow.ItemArray[4].ToString());
                            oracmd.Parameters.AddWithValue("@ReaderLocation", dbNewRow.ItemArray[5].ToString());
                            oracmd.Parameters.AddWithValue("@ReadTime", dbNewRow.ItemArray[6].ToString());
                            oracmd.Parameters.AddWithValue("@Status", dbNewRow.ItemArray[7].ToString());
                            oracmd.Parameters.AddWithValue("@CreatedBy", GlobalVariables.LGNID);
                            int i = oracmd.ExecuteNonQuery();

                            if (i >= 1)
                            {
                                if (SqlLocalcon.State == ConnectionState.Closed || SqlLocalcon.State == ConnectionState.Broken)
                                {
                                    SqlLocalcon.Open();
                                }
                                strUpd = "update [dbo].[DelegateTrackingLog] set ModifiedBy='" + GlobalVariables.LGNID + "',[ModifiedTime]=GetDate(), [DTLSyncFlag]='Y' WHERE [TrackingId]=" + dbNewRow.ItemArray[0].ToString() + "";
                                SqlCommand cmd = new SqlCommand(strUpd, SqlLocalcon);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    MessageBox.Show("Upload Successfully..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    this.Close();
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
