using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CS_RFID3_Host_Sample1
{
    public partial class Error_Log_Report : Form
    {
        public Error_Log_Report()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {

                String sLine = "";

                try
                {
                    //Pass the file you selected with the OpenFileDialog control to
                    //the StreamReader Constructor.
                    System.IO.StreamReader FileStream = new System.IO.StreamReader(openFileDialog1.FileName);
                    //You must set the value to false when you are programatically adding rows to
                    //a DataGridView.  If you need to allow the user to add rows, you
                    //can set the value back to true after you have populated the DataGridView
                    grdErrorlog.AllowUserToAddRows = false;

                    //Read the first line of the text file
                    sLine = FileStream.ReadLine();
                    //The Split Command splits a string into an array, based on the delimiter you pass.
                    //I chose to use a semi-colon for the text delimiter.
                    //Any character can be used as a delimeter in the split command.
                    string[] s = sLine.Split('-');

                    //In this example, I placed the field names in the first row.
                    //The for loop below is used to create the columns and use the text values in
                    //the first row for the column headings.
                    for (int i = 0; i <= s.Length - 1; i++)
                    {
                        DataGridViewColumn colHold = new DataGridViewTextBoxColumn();
                        colHold.Name = "col" + System.Convert.ToString(i);
                        colHold.HeaderText = (i + 1).ToString();
                        grdErrorlog.Columns.Add(colHold);
                    }

                    //Read the next line in the text file in order to pass it to the
                    //while loop below
                    sLine = FileStream.ReadLine();
                    //The while loop reads each line of text.
                    while (sLine != null)
                    {
                        //Adds a new row to the DataGridView for each line of text.
                        grdErrorlog.Rows.Add();

                        //This for loop loops through the array in order to retrieve each
                        //line of text.
                        for (int i = 0; i <= s.Length - 1; i++)
                        {
                            //Splits each line in the text file into a string array
                            s = sLine.Split('-');
                            //Sets the value of the cell to the value of the text retreived from the text file.
                            grdErrorlog.Rows[grdErrorlog.Rows.Count - 1].Cells[i].Value = s[i].ToString();
                        }
                        sLine = FileStream.ReadLine();
                    }
                    //Close the selected text file.
                    FileStream.Close();
                }
                catch (Exception err)
                {
                    //Display any errors in a Message Box.
                    System.Windows.Forms.MessageBox.Show("Error" + err.Message, "Program Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.CreatePrompt = true;
                saveFileDialog1.Title = "Export Excel File To";
                Microsoft.Office.Interop.Excel.Application ExcelApp1 = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp1.Application.Workbooks.Add(Type.Missing);
                ExcelApp1.Columns.ColumnWidth = 20;

                // Storing header part in Excel
                for (int i = 1; i < grdErrorlog.Columns.Count + 1; i++)
                {
                    ExcelApp1.Cells[1, i] = grdErrorlog.Columns[i - 1].HeaderText;
                }
                //Storing Each row and column value to excel sheet
                for (int i = 0; i <= grdErrorlog.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < grdErrorlog.Columns.Count; j++)
                    {
                        ExcelApp1.Cells[i + 2, j + 1] = grdErrorlog.Rows[i].Cells[j].Value.ToString();
                    }
                }
                ExcelApp1.Visible = true;
                ExcelApp1.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName + ".xls");
                ////ExcelApp1.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName + " : " + DateTime.Now.ToShortDateString() + ".xls");
                ExcelApp1.ActiveWorkbook.Saved = true;
                ExcelApp1.Quit();
                //MessageBox.Show("The Save button was clicked or the Enter key was pressed" + "\nThe file would have been saved as " + this.saveFileDialog1.FileName);

            }
            else MessageBox.Show("The Cancel button was clicked or Esc was pressed");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(grdErrorlog);
        }
    }
}
