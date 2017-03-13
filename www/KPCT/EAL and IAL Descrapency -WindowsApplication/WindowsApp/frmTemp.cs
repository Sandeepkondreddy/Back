using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace WindowsApp
{
    public partial class frmTemp : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public frmTemp()
        {
            InitializeComponent();
        }
        //DataSet EALds = CreateDataRow();

        private DataSet CreateDataRow()
        {
            DataTable TblEal = CreateEALTable();
            DataRow dRow;
            dRow = TblEal.NewRow();
        dRow["Container"] = 1;
            dRow["Weight"] = "Vinoth";
            dRow["ISO"] = "Udumalpet";
            dRow["Status"] = "Coimbatore";
            dRow["POD"] = "TamilNadu";


            TblEal.Rows.Add(dRow);
            DataSet dsIncomeAnalysis = new DataSet();
            dsIncomeAnalysis.Tables.Add(TblEal);
            return dsIncomeAnalysis;
        }
        public DataTable CreateEALTable()
        {
            DataTable TblEal = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Container";
            TblEal.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Weight";
            TblEal.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ISO";
            TblEal.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Status";
            TblEal.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "POD";
            TblEal.Columns.Add(myDataColumn);

            return TblEal;
        }

        private void frmTemp_Load(object sender, EventArgs e)
        {
            DataSet EALds = CreateDataRow();
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
  
        
            
        
    }
}
