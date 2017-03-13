using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace CS_RFID3_Host_Sample1
{
	class classSaveTransactionDetails
	{
        public static string SaveTransaction(string Tagno,string Antina,string Location,string ReaderIP,string ImgPath)
        {
            string saveStaues="";
            SqlConnection Sqlcon = new SqlConnection(classServerConnections.SqlConn_String);
            Sqlcon.Open();
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            try
            {
                string strNonQuery = "Insert into  [dbo].[RFIDPhotoLog] ([tagno],[Ant],[Location],[readdt],[ReaderIP],[ImgPath]) Values('" + Tagno + "','" + Antina + "','" + Location + "',GETDATE(),'" + ReaderIP + "','" + ImgPath + "')";
                SqlCommand cmd = new SqlCommand(strNonQuery, Sqlcon);
                cmd.ExecuteNonQuery();
                saveStaues = "Transaction Saved Successfully.";
            }
            catch (Exception ex)
            {
                saveStaues = "Transaction Save Failed.";
            }
            Sqlcon.Close();

            return saveStaues;
        }
	}
}
