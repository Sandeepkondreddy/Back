using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace WindowsApp
{
    public partial class formBillableEventsReport : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public formBillableEventsReport()
        {
            InitializeComponent();
            CreateCommonTable();
        }
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsCommon = new DataSet();
        DataTable TblCommon = new DataTable();
        
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            cleardatagrid();
            
            
            
            if (ComboBoxTypeofChange.Text == "---Select---")
            {
                MessageBox.Show("Please Select from List.");
            }
            else if (ComboBoxTypeofChange.Text == "POD Change")
            {
                getDataForPodChange();
            }
            else if (ComboBoxTypeofChange.Text == "CarrierOutBoundIntended Change")
            {
                getDataForCarrierOutBoundIntendedChange();
            }
            else if (ComboBoxTypeofChange.Text == "CarrierOutBoundDeclared Change")
            {
                getDataForCarrierOutBoundDeclaredChange();
            }
            else if (ComboBoxTypeofChange.Text == "Category Change")
            {
                getDataForCategoryChange();
            }
            else if (ComboBoxTypeofChange.Text == "Destination")
            {
                getDataForDestinationChange();
            }
            else if (ComboBoxTypeofChange.Text == "Dray Status")
            {
                getDataForDrayStatusChange();
            }
            else if (ComboBoxTypeofChange.Text == "Group Change")
            {
                getDataForGroupChange();
            }
        }
        

        private void formBillableEventsReport_Load(object sender, EventArgs e)
        {
            try
            {
                ComboBoxTypeofChange.Text = "---Select---";
            }
            catch
            {
            }
        }
        //---To get the details from the N4 data base based on the Date of Event----Start-----PodChange
        public void getDataForPodChange()
        {
            DateTime fdt = Convert.ToDateTime(FromTimeDateTimePicker.Value.ToString());
            string fromdt = fdt.ToString("dd-MM-yy HH:MM");
            DateTime tdt = Convert.ToDateTime(ToTimeDateTimePicker.Value.ToString());
            string todt = tdt.ToString("dd-MM-yy HH:MM");
            string CONN_STRING = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
            dbCon.Open();
       //     string Query = "SELECT a.GKEY,a.placed_by, a.placed_time DateTime,a.APPLIED_TO_NATURAL_KEY ContainerNo, a.note Note,B.METAFIELD_ID Change,h.DES Description,NAVIS.Cf_Valueid (b.PRIOR_VALUE) FromValue,NAVIS.Cf_Valueid (b.NEW_VALUE) ToValue,f.ID LINE_OP, " +
       //"c.FREIGHT_KIND,c.CATEGORY,d.ID Eventtyp,e.ID Facility,c.GOODS_AND_CTR_WT_KG " +
       // "FROM navis.SRV_EVENT a, " +
       //  "navis.SRV_EVENT_FIELD_CHANGES b, " +
       //  "navis.INV_UNIT c, " +
       //  "navis.SRV_EVENT_TYPES d," +
       //  "navis.ARGO_FACILITY e, " +
       //  "navis.REF_BIZUNIT_SCOPED f, " +
       //  "navis.REF_ROUTING_POINT g, " +
       //  "NAVIS.KPCL_EVENT h " +
       //        "WHERE a.gkey = b.EVENT_GKEY AND " +
       //               "a.APPLIED_TO_NATURAL_KEY = c.ID " +
       //               "AND a.EVENT_TYPE_GKEY = d.GKEY " +
       //               "AND a.FACILITY_GKEY = e.GKEY " +
       //               "AND c.LINE_OP = f.GKEY " +
       //               "AND c.POD1_GKEY = g.gkey " +
       //               "AND b.METAFIELD_ID=h.NAME " +
       //               "AND b.METAFIELD_ID='rtgPOD1'"+
       //               "and a.placed_time between TO_DATE ('" + fromdt + "','dd-MM-yyyy HH24:MI:SS') and TO_DATE ('" + todt + "','dd-MM-yyyy HH24:MI:SS')";
            string Query = "SELECT h.DES Change,a.placed_time ChangeTime,a.APPLIED_TO_NATURAL_KEY ContainerNo,NAVIS.Cf_Valueid (b.PRIOR_VALUE) FromValue,NAVIS.Cf_Valueid (b.NEW_VALUE) ToValue,f.ID Line, " +
        "c.FREIGHT_KIND Freight,c.CATEGORY Category,e.ID Facility,c.GOODS_AND_CTR_WT_KG Wt,a.note Note " +
        "FROM navis.SRV_EVENT a, " +
         "navis.SRV_EVENT_FIELD_CHANGES b, " +
         "navis.INV_UNIT c, " +
         "navis.SRV_EVENT_TYPES d," +
         "navis.ARGO_FACILITY e, " +
         "navis.REF_BIZUNIT_SCOPED f, " +
         "navis.REF_ROUTING_POINT g, " +
         "NAVIS.KPCL_EVENT h " +
               "WHERE a.gkey = b.EVENT_GKEY AND " +
                      "a.APPLIED_TO_NATURAL_KEY = c.ID " +
                      "AND a.EVENT_TYPE_GKEY = d.GKEY " +
                      "AND a.FACILITY_GKEY = e.GKEY " +
                      "AND c.LINE_OP = f.GKEY " +
                      "AND c.POD1_GKEY = g.gkey " +
                      "AND b.METAFIELD_ID=h.NAME " +
                      "AND b.METAFIELD_ID='rtgPOD1'"+
                      "and a.placed_time between TO_TIMESTAMP ('" + fromdt + "', 'DD/MM/RR HH24:MI:SS.FF')and TO_TIMESTAMP ('" + todt + "', 'DD/MM/RR HH24:MI:SS.FF')";
            OleDbCommand cmd = new OleDbCommand(Query, dbCon);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(da);
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);
            da.Fill(ds, "Query");
            //da.Fill(ds, "DataTable"); 
            kryptonDataGridView1.DataMember = "Query";
            kryptonDataGridView1.DataSource = ds;
            kryptonDataGridView1.Visible = true;
            
            DataSet dataReport = new DataSet();
            da.Fill(dataReport, "DataTable");
            BillableEventsReport myDataReport = new BillableEventsReport();
            myDataReport.SetDataSource(dataReport);
            rptBillableEventsReport.ReportSource = myDataReport; 

            dbCon.Close();
            //report();
            //da.Fill(TblCommon, "DataTable");
        }
        //---To get the details from the N4 data base based on the Date of Event----End-----PodChange

        //---To get the details from the N4 data base based on the Date of Event----Start-----CategoryChange
        public void getDataForCategoryChange()
        {
            DateTime fdt = Convert.ToDateTime(FromTimeDateTimePicker.Value.ToString());
            string fromdt = fdt.ToString("dd-MM-yy HH:MM");
            DateTime tdt = Convert.ToDateTime(ToTimeDateTimePicker.Value.ToString());
            string todt = tdt.ToString("dd-MM-yy HH:MM");
            string CONN_STRING = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
            dbCon.Open();
            string Query = "SELECT g.DES Change,a.placed_time ChangeTime,a.APPLIED_TO_NATURAL_KEY ContainerNo,b.prior_value FromValue, b.new_value ToValue,f.ID Line," +
                "c.FREIGHT_KIND Freight,c.CATEGORY Category,e.ID Facility,c.GOODS_AND_CTR_WT_KG Wt,a.note Note " +
                "FROM navis.SRV_EVENT a, " +
                "navis.SRV_EVENT_FIELD_CHANGES b, " +
                "navis.INV_UNIT c, " +
                "navis.SRV_EVENT_TYPES d, " +
                "navis.ARGO_FACILITY e, " +
                "navis.REF_BIZUNIT_SCOPED f, " +
                "NAVIS.KPCL_EVENT g " +
                    "WHERE a.gkey = b.EVENT_GKEY AND " +
                          "a.APPLIED_TO_NATURAL_KEY = c.ID " +
                          "AND a.EVENT_TYPE_GKEY = d.GKEY " +
                          "AND a.FACILITY_GKEY = e.GKEY " +
                          "AND c.LINE_OP = f.GKEY " +
                          "AND b.METAFIELD_ID=g.NAME " +
                          "AND b.METAFIELD_ID='unitCategory'"+
                          "and a.placed_time between TO_TIMESTAMP ('" + fromdt + "', 'DD/MM/RR HH24:MI:SS.FF')and TO_TIMESTAMP ('" + todt + "', 'DD/MM/RR HH24:MI:SS.FF')";
            OleDbCommand cmd = new OleDbCommand(Query, dbCon);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(da);
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);
            da.Fill(ds, "Query");
            kryptonDataGridView1.DataMember = "Query";
            kryptonDataGridView1.DataSource = ds;
            kryptonDataGridView1.Visible = true;

            DataSet dataReport = new DataSet();
            da.Fill(dataReport, "DataTable");
            BillableEventsReport myDataReport = new BillableEventsReport();
            myDataReport.SetDataSource(dataReport);
            rptBillableEventsReport.ReportSource = myDataReport; 
            dbCon.Close();
        }
        //---To get the details from the N4 data base based on the Date of Event----End-----CategoryChange

        //---To get the details from the N4 data base based on the Date of Event----Start-----Carrier Out Bound Declared Change
        public void getDataForCarrierOutBoundDeclaredChange()
        {
            DateTime fdt = Convert.ToDateTime(FromTimeDateTimePicker.Value.ToString());
            string fromdt = fdt.ToString("dd-MM-yy HH:MM");
            DateTime tdt = Convert.ToDateTime(ToTimeDateTimePicker.Value.ToString());
            string todt = tdt.ToString("dd-MM-yy HH:MM");
            string CONN_STRING = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
            dbCon.Open();
            string Query = "SELECT h.DES Change,a.placed_time ChangeTime,a.APPLIED_TO_NATURAL_KEY ContainerNo,NAVIS.CF_Vessel (b.PRIOR_VALUE) FromValue,NAVIS.CF_Vessel (b.NEW_VALUE) ToValue,f.ID Line, " +
                "c.FREIGHT_KIND Freight,c.CATEGORY Category,e.ID Facility,c.GOODS_AND_CTR_WT_KG Wt, a.note Note " +
                "FROM navis.SRV_EVENT a, " +
                "navis.SRV_EVENT_FIELD_CHANGES b, " +
                "navis.INV_UNIT c, " +
                "navis.SRV_EVENT_TYPES d, " +
                 "navis.ARGO_FACILITY e, " +
                 "navis.REF_BIZUNIT_SCOPED f, " +
                 "navis.ARGO_CARRIER_VISIT g," +
                 "NAVIS.KPCL_EVENT h " +
                   "WHERE a.gkey = b.EVENT_GKEY AND " +
                          "a.APPLIED_TO_NATURAL_KEY = c.ID " +
                          "AND a.EVENT_TYPE_GKEY = d.GKEY " +
                          "AND a.FACILITY_GKEY = e.GKEY " +
                          "AND c.LINE_OP = f.GKEY " +
                          "AND c.CV_GKEY = g.GKEY " +
                          "AND b.METAFIELD_ID=h.NAME " +
                          "AND b.METAFIELD_ID='rtgDeclaredCv'"+
                          "and a.placed_time between TO_TIMESTAMP ('" + fromdt + "', 'DD/MM/RR HH24:MI:SS.FF')and TO_TIMESTAMP ('" + todt + "', 'DD/MM/RR HH24:MI:SS.FF')";
            OleDbCommand cmd = new OleDbCommand(Query, dbCon);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(da);
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);
            da.Fill(ds, "Query");
            kryptonDataGridView1.DataMember = "Query";
            kryptonDataGridView1.DataSource = ds;
            kryptonDataGridView1.Visible = true;
            DataSet dataReport = new DataSet();
            da.Fill(dataReport, "DataTable");
            BillableEventsReport myDataReport = new BillableEventsReport();
            myDataReport.SetDataSource(dataReport);
            rptBillableEventsReport.ReportSource = myDataReport; 
            dbCon.Close();
            //report();
        }
        //---To get the details from the N4 data base based on the Date of Event----End-----Carrier Out Bound Declared Change

        //---To get the details from the N4 data base based on the Date of Event----Start-----Carrier Out Bound Intended Change
        public void getDataForCarrierOutBoundIntendedChange()
        {
            DateTime fdt = Convert.ToDateTime(FromTimeDateTimePicker.Value.ToString());
            string fromdt = fdt.ToString("dd-MM-yy HH:MM");
            DateTime tdt = Convert.ToDateTime(ToTimeDateTimePicker.Value.ToString());
            string todt = tdt.ToString("dd-MM-yy HH:MM");
            string CONN_STRING = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
            dbCon.Open();
            string Query = "SELECT h.DES Change,a.placed_time ChangeTime,a.APPLIED_TO_NATURAL_KEY ContainerNo, NAVIS.CF_Vessel (b.PRIOR_VALUE) FromValue,NAVIS.CF_Vessel (b.NEW_VALUE) ToValue,f.ID Line, " +
                 "c.FREIGHT_KIND Freight,c.CATEGORY Category,e.ID Facility,c.GOODS_AND_CTR_WT_KG Wt,a.note Note " +
                 "FROM navis.SRV_EVENT a, " +
                 "navis.SRV_EVENT_FIELD_CHANGES b, " +
                 "navis.INV_UNIT c, " +
                 "navis.SRV_EVENT_TYPES d, " +
                 "navis.ARGO_FACILITY e, " +
                 "navis.REF_BIZUNIT_SCOPED f, " +
                 "navis.ARGO_CARRIER_VISIT g," +
                 "NAVIS.KPCL_EVENT h " +
                        "WHERE a.gkey = b.EVENT_GKEY AND " +
                              "a.APPLIED_TO_NATURAL_KEY = c.ID " +
                              "AND a.EVENT_TYPE_GKEY = d.GKEY " +
                              "AND a.FACILITY_GKEY = e.GKEY " +
                              "AND c.LINE_OP = f.GKEY " +
                              "AND c.CV_GKEY = g.GKEY " +
                              "AND b.METAFIELD_ID=h.NAME " +
                              "AND b.METAFIELD_ID='ufvIntendedObCv'"+
                              "and a.placed_time between TO_TIMESTAMP ('" + fromdt + "', 'DD/MM/RR HH24:MI:SS.FF')and TO_TIMESTAMP ('" + todt + "', 'DD/MM/RR HH24:MI:SS.FF')";
            //"and a.placed_time between '" + fromdt + "'and '" + todt + "'";
            OleDbCommand cmd = new OleDbCommand(Query, dbCon);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(da);
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);
            da.Fill(ds, "Query");
            kryptonDataGridView1.DataMember = "Query";
            kryptonDataGridView1.DataSource = ds;
            kryptonDataGridView1.Visible = true;

            DataSet dataReport = new DataSet();
            da.Fill(dataReport, "DataTable");
            BillableEventsReport myDataReport = new BillableEventsReport();
            myDataReport.SetDataSource(dataReport);
            rptBillableEventsReport.ReportSource = myDataReport; 
            dbCon.Close();
        }
        //---To get the details from the N4 data base based on the Date of Event----End-----Carrier Out Bound Intended Change

        //---To get the details from the N4 data base based on the Date of Event----Start-----Destination Change
        public void getDataForDestinationChange()
        {
            DateTime fdt = Convert.ToDateTime(FromTimeDateTimePicker.Value.ToString());
            string fromdt = fdt.ToString("dd-MM-yy HH:MM");
            DateTime tdt = Convert.ToDateTime(ToTimeDateTimePicker.Value.ToString());
            string todt = tdt.ToString("dd-MM-yy HH:MM");
            string CONN_STRING = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
            dbCon.Open();
            string Query = "SELECT g.DES Change, a.placed_time ChangeTime,a.APPLIED_TO_NATURAL_KEY ContainerNo, b.prior_value FromValue, b.new_value ToValue,f.ID Line, " +
                                 "c.FREIGHT_KIND Freight,c.CATEGORY Category,e.ID Facility,c.GOODS_AND_CTR_WT_KG Wt, a.note Note " +
                                 "FROM navis.SRV_EVENT a, " +
                                 "navis.SRV_EVENT_FIELD_CHANGES b, " +
                                 "navis.INV_UNIT c, " +
                                 "navis.SRV_EVENT_TYPES d, " +
                                 "navis.ARGO_FACILITY e, " +
                                 "navis.REF_BIZUNIT_SCOPED f, " +
                                 "NAVIS.KPCL_EVENT g " +
                                        "WHERE a.gkey = b.EVENT_GKEY AND " +
                                              "a.APPLIED_TO_NATURAL_KEY = c.ID " +
                                              "AND a.EVENT_TYPE_GKEY = d.GKEY " +
                                              "AND a.FACILITY_GKEY = e.GKEY " +
                                              "AND c.LINE_OP = f.GKEY " +
                                              "AND b.METAFIELD_ID=g.NAME " +
                                              "AND b.METAFIELD_ID='gdsDestination'"+
                                              "and a.placed_time between TO_TIMESTAMP ('" + fromdt + "', 'DD/MM/RR HH24:MI:SS.FF')and TO_TIMESTAMP ('" + todt + "', 'DD/MM/RR HH24:MI:SS.FF')";
            OleDbCommand cmd = new OleDbCommand(Query, dbCon);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(da);
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);
            da.Fill(ds, "Query");
            kryptonDataGridView1.DataMember = "Query";
            kryptonDataGridView1.DataSource = ds;
            kryptonDataGridView1.Visible = true;

            DataSet dataReport = new DataSet();
            da.Fill(dataReport, "DataTable");
            BillableEventsReport myDataReport = new BillableEventsReport();
            myDataReport.SetDataSource(dataReport);
            rptBillableEventsReport.ReportSource = myDataReport; 
            dbCon.Close();
        }
        //---To get the details from the N4 data base based on the Date of Event----End-----Destination Change

        //---To get the details from the N4 data base based on the Date of Event----Start-----Dray Status Change
        public void getDataForDrayStatusChange()
        {
            //DateTime fromdt = FromTimeDateTimePicker.Value;
            //DateTime todt = ToTimeDateTimePicker.Value;
            //string fromdt = FromTimeDateTimePicker.Value.ToString();
            DateTime fdt = Convert.ToDateTime(FromTimeDateTimePicker.Value.ToString());
            string fromdt = fdt.ToString("dd-MM-yy HH:MM");
            DateTime tdt = Convert.ToDateTime(ToTimeDateTimePicker.Value.ToString());
            string todt = tdt.ToString("dd-MM-yy HH:MM");
            string CONN_STRING = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
            dbCon.Open();
            string Query = "SELECT g.DES Change, a.placed_time ChangeTime,a.APPLIED_TO_NATURAL_KEY ContainerNo,b.prior_value FromValue, b.new_value ToValue,f.ID Line, " +
                             "c.FREIGHT_KIND Freight,c.CATEGORY Category,e.ID Facility,c.GOODS_AND_CTR_WT_KG Wt, a.note Note " +
                             "FROM navis.SRV_EVENT a, " +
                             "navis.SRV_EVENT_FIELD_CHANGES b, " +
                             "navis.INV_UNIT c, " +
                             "navis.SRV_EVENT_TYPES d, " +
                             "navis.ARGO_FACILITY e, " +
                             "navis.REF_BIZUNIT_SCOPED f, " +
                             "NAVIS.KPCL_EVENT g " +
                                    "WHERE a.gkey = b.EVENT_GKEY AND " +
                                          "a.APPLIED_TO_NATURAL_KEY = c.ID " +
                                          "AND a.EVENT_TYPE_GKEY = d.GKEY " +
                                          "AND a.FACILITY_GKEY = e.GKEY " +
                                          "AND c.LINE_OP = f.GKEY " +
                                          "AND b.METAFIELD_ID=g.NAME " +
                                          "AND b.METAFIELD_ID='unitDrayStatus'"+
                                          "and a.placed_time between TO_TIMESTAMP ('" + fromdt + "', 'DD/MM/RR HH24:MI:SS.FF')and TO_TIMESTAMP ('" + todt + "', 'DD/MM/RR HH24:MI:SS.FF')";
            OleDbCommand cmd = new OleDbCommand(Query, dbCon);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(da);
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);
            da.Fill(ds, "Query");
            kryptonDataGridView1.DataMember = "Query";
            kryptonDataGridView1.DataSource = ds;
            kryptonDataGridView1.Visible = true;

            DataSet dataReport = new DataSet();
            da.Fill(dataReport, "DataTable");
            BillableEventsReport myDataReport = new BillableEventsReport();
            myDataReport.SetDataSource(dataReport);
            rptBillableEventsReport.ReportSource = myDataReport; 
            dbCon.Close();
            //report();
        }
        //---To get the details from the N4 data base based on the Date of Event----End-----Dray Status Change

        //---To get the details from the N4 data base based on the Date of Event----Start-----Group Change
        public void getDataForGroupChange()
        {
            //DateTime fromdt = FromTimeDateTimePicker.Value;
            //DateTime todt = ToTimeDateTimePicker.Value;
            //string fromdt = FromTimeDateTimePicker.Value.ToString();
            DateTime fdt = Convert.ToDateTime(FromTimeDateTimePicker.Value.ToString());
            string fromdt = fdt.ToString("dd-MM-yy HH:MM");
            DateTime tdt = Convert.ToDateTime(ToTimeDateTimePicker.Value.ToString());
            string todt = tdt.ToString("dd-MM-yy HH:MM");
            string CONN_STRING = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
            dbCon.Open();
            string Query = "SELECT h.DES Change, a.placed_time ChangeTime,a.APPLIED_TO_NATURAL_KEY ContainerNo,NAVIS.Cf_Value (b.PRIOR_VALUE) FromValue,NAVIS.Cf_Value (b.NEW_VALUE) ToValue,f.ID Line, " +
                                 "c.FREIGHT_KIND Freight,c.CATEGORY Category,e.ID Facility,c.GOODS_AND_CTR_WT_KG Wt, a.note Note " +
                                 "FROM navis.SRV_EVENT a, " +
 		                         "navis.SRV_EVENT_FIELD_CHANGES b, " +
		                         "navis.INV_UNIT c, " +
		                         "navis.SRV_EVENT_TYPES d, " +
		                         "navis.ARGO_FACILITY e, " +
		                         "navis.REF_BIZUNIT_SCOPED f, " +
		                         "navis.REF_GROUPS g, " +
		                         "NAVIS.KPCL_EVENT h " +
                                        "WHERE a.gkey = b.EVENT_GKEY AND " +
                                              "a.APPLIED_TO_NATURAL_KEY = c.ID " +
                                              "AND a.EVENT_TYPE_GKEY = d.GKEY " +
                                              "AND a.FACILITY_GKEY = e.GKEY " +
                                              "AND c.LINE_OP = f.GKEY " +
                                              "AND c.GROUP_GKEY = g.gkey " +
                                              "AND b.METAFIELD_ID=h.NAME " +
                                              "AND b.METAFIELD_ID='rtgGroup'" +
                                              "and a.placed_time between TO_TIMESTAMP ('" + fromdt + "', 'DD/MM/RR HH24:MI:SS.FF')and TO_TIMESTAMP ('" + todt + "', 'DD/MM/RR HH24:MI:SS.FF')";
                              
                
            OleDbCommand cmd = new OleDbCommand(Query, dbCon);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(da);
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);
            da.Fill(ds, "Query");
            kryptonDataGridView1.DataMember = "Query";
            kryptonDataGridView1.DataSource = ds;
            kryptonDataGridView1.Visible = true;

            DataSet dataReport = new DataSet();
            da.Fill(dataReport, "DataTable");
            BillableEventsReport myDataReport = new BillableEventsReport();
            myDataReport.SetDataSource(dataReport);
            rptBillableEventsReport.ReportSource = myDataReport; 
            dbCon.Close();
            
        }
        //---To get the details from the N4 data base based on the Date of Event----End-----Group Change

        private DataTable CreateCommonTable()
        {
            DataColumn myDataColumn;
            

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Change";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ChangeTime";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ContainerNo";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "FromValue";
            TblCommon.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = Type.GetType("System.String");
            //myDataColumn.ColumnName = "Description";
            //TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ToValue";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Line";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "FrightKind";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Category";
            TblCommon.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = Type.GetType("System.String");
            //myDataColumn.ColumnName = "EventType";
            //TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Facility";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Wt";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Note";
            TblCommon.Columns.Add(myDataColumn);

            return TblCommon;
        }

        public void report()
        {

            for (int j = 0; j < kryptonDataGridView1.Rows.Count; j++)
            {
                DataRow dr = TblCommon.NewRow();
                dr["Change"] = kryptonDataGridView1.Columns[0].ToString();
                dr["ChangeTime"] = kryptonDataGridView1.Columns[1].ToString();
                dr["ContainerNo"] = kryptonDataGridView1.Columns[2].ToString();
                dr["FromValue"] = kryptonDataGridView1.Columns[3].ToString();
                dr["ToValue"] = kryptonDataGridView1.Columns[4].ToString();
                dr["Line"] = kryptonDataGridView1.Columns[5].ToString();
                dr["FrightKind"] = kryptonDataGridView1.Columns[6].ToString();
                dr["Category"] = kryptonDataGridView1.Columns[7].ToString();
                dr["Facility"] = kryptonDataGridView1.Columns[8].ToString();
                dr["Wt"] = kryptonDataGridView1.Columns[9].ToString();
                dr["Note"] = kryptonDataGridView1.Columns[10].ToString();
                TblCommon.Rows.Add(dr);

                             
                rptBillableEventsReport.Refresh();
                rptBillableEventsReport.Visible = true;
            }
        }

        
        public void cleardatagrid()
        {
            while (kryptonDataGridView1.Rows.Count != 0)
                for (int i = 0; i < kryptonDataGridView1.Rows.Count; i++)
                {
                    kryptonDataGridView1.Rows.RemoveAt(i);
                }
        }
    }
}