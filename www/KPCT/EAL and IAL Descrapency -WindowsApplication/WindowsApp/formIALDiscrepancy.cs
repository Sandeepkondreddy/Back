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

namespace WindowsApp
{
    public partial class formIALDiscrepancy : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public formIALDiscrepancy()
        {
            InitializeComponent();
        }
        string cid;
        string vesslid;
        string discid;
        string user;
        string IALfilename;
        string filename;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataSet IALds = new DataSet();
        DataTable TblIal = new DataTable();
        DataSet Commonds = new DataSet();
        DataTable TblCommon = new DataTable();
        private void formIALDiscrepancy_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"C:\KPT");
            DataTable TblEal = CreateIALTable();
            IALds.Tables.Add(TblIal);
            //DataTable TblCommon = CreateCommonTable();
            TblCommon = CreateCommonTable();
            Commonds.Tables.Add(TblCommon);
        }

        private void btnBowse_Click(object sender, EventArgs e)
        {
            TblCommon.Clear();
            rptIALReport.Visible = false;
            txtIALfilePath.Text = "";
            OpenFileDialog opendialog = new OpenFileDialog();
            opendialog.Filter = "Text Files|*.txt";
            if (opendialog.ShowDialog() == DialogResult.OK)
            {
                txtIALfilePath.Text = opendialog.FileName.ToString();

                if (txtIALfilePath.Text != "")
                {
                    createNewIAL();
                    getInIALNotInYard();
                    getContInYardNotinIAL();
                    CompareIALandDB();
                    getDiscidfromHdr();
                    inserttodb();
                    showreport();
                }
            }
        }
        //---To create new IAL file----Start----
        private void createNewIAL()
        {
            TextWriter tw = new StreamWriter(@"C:\KPT\" + IALfilename + ".txt");
            tw.Close();
        }
        //---To create new IAL file----End----

        private void getInIALNotInYard()
        {
            string strLine = "";
            int intEALfromDB = 0;
            int lCnt = 0;

            using (StreamReader reader = new StreamReader(txtIALfilePath.Text.Trim()))

                while ((strLine = reader.ReadLine()) != null)
                {

                    if (lCnt != 0)
                    {

                        string[] lines = Regex.Split(strLine, ",");
                        //dataGridViewEAL.Rows.Add(lines[1], lines[4], lines[3], lines[2], lines[9], lines[5]);
                        cid = lines[1].ToString().Trim();

                        DataRow dRow;
                        dRow = TblIal.NewRow();
                        dRow["Container"] = lines[1].ToString().Trim();
                        dRow["Weight"] = lines[2].ToString().Trim();
                        dRow["ISO"] = lines[3].ToString().Trim();
                        dRow["Status"] = lines[4].ToString().Trim();
                        //dRow["Reefer"] = lines[13].ToString();
                                              //dRow["POL"] = lines[12].ToString().Trim();
                        dRow["LINEOP"] = lines[11].ToString().Trim();

                        TblIal.Rows.Add(dRow);


                        //This is for Populating Containers in IAL not in Yard
                        //DataView dv0 = new DataView(ds.Tables[0]);
                        //string ct = dv0[0][0].ToString();
                        DataView dv = ds.Tables[0].DefaultView;
                        dv.RowFilter = "Container='" + lines[1] + "'";
                        if (dv.Count == 0)
                        //if(ct.Trim()==lines[1].ToString().Trim())
                        {
                            //dgvNotinYard.Rows.Add(lines[1], lines[4], lines[3], lines[2], lines[9], lines[5]);
                            //lblNotinYardCount.Text = dgvNotinYard.Rows.Count.ToString();

                            DataRow dRow1;
                            dRow1 = TblCommon.NewRow();
                            dRow1["ContainerNo"] = lines[1].ToString().Trim();
                            dRow1["Status"] = lines[4].ToString().Trim();
                            dRow1["ISO"] = lines[3].ToString().Trim();
                            dRow1["Weight"] = lines[2].ToString().Trim();
                            dRow1["Agent"] = lines[11].ToString().Trim();
                            //dRow1["Reefer"] = lines[13].ToString();
                                         //dRow1["POL"] = lines[12].ToString().Trim();
                            dRow1["Description"] = "10";
                            TblCommon.Rows.Add(dRow1);

                        }
                        else if (dv.Count == 1)
                        {
                            ////POD Exception---Start----
                            //DataView dv1 = new DataView(ds.Tables[0]);
                            //dv1.RowFilter = "POD='" + lines[5] + "'";
                            //string pod = dv[0][1].ToString();
                            //if (dv1.Count == 0)
                            //{
                            //    //dgvPODException.Rows.Add(lines[1], lines[5], pod, lines[9], lines[2]);
                            //    //lblPODException.Text = "POD Exception: " + dgvPODException.RowCount.ToString();


                            //    DataRow dRow1;
                            //    dRow1 = TblCommon.NewRow();
                            //    dRow1["ContainerNo"] = lines[1].ToString().Trim();
                            //    dRow1["Status"] = lines[4].ToString().Trim();
                            //    dRow1["ISO"] = lines[3].ToString().Trim();
                            //    dRow1["Weight"] = lines[2].ToString().Trim();
                            //    dRow1["Agent"] = lines[11].ToString().Trim();
                            //    //dRow1["POD"] = lines[5].ToString().Trim();
                            //    dRow1["ValinIAL"] = lines[5].ToString().Trim();
                            //    dRow1["ValinN4"] = pod;
                            //    dRow1["Description"] = "4";
                            //    TblCommon.Rows.Add(dRow1);
                            //}
                            ////POD Exception---End----

                            //Ststus Exception---Start---
                            DataView dv2 = new DataView(ds.Tables[0]);
                            dv2.RowFilter = "FrieghtKind='" + lines[4] + "'";
                            string status = dv[0][4].ToString();
                            if (dv2.Count == 0)
                            {
                                //dgvStatusException.Rows.Add(lines[1], lines[4], status, lines[9], lines[2]);
                                //lblStatusException.Text = "Status Exception: " + dgvStatusException.RowCount.ToString();

                                DataRow dRow1;
                                dRow1 = TblCommon.NewRow();
                                dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                //dRow1["Status"] = lines[4].ToString().Trim();
                                dRow1["ISO"] = lines[3].ToString().Trim();
                                dRow1["Weight"] = lines[2].ToString().Trim();
                                dRow1["Agent"] = lines[11].ToString().Trim();
                                              //dRow1["POL"] = lines[12].ToString().Trim();
                                //dRow1["Reefer"] = lines[13].ToString();
                                dRow1["ValinIAL"] = lines[4].ToString().Trim();
                                dRow1["ValinN4"] = status;
                                dRow1["Description"] = "13";
                                TblCommon.Rows.Add(dRow1);
                            }
                            //Ststus Exception---End---

                            //Gr. Weight Exception---Start---
                            DataView dv3 = new DataView(ds.Tables[0]);
                            double wt = Convert.ToDouble(lines[2].ToString());
                            dv3.RowFilter = "WeightMT='" + wt + "'";
                            string grwt = dv[0][5].ToString();
                            if (dv3.Count == 0)
                            {
                                //dgvgrwtException.Rows.Add(lines[1], wt, grwt, lines[9], lines[4]);
                                //lblgrwtException.Text = "Gr. Weight Exception: " + dgvgrwtException.RowCount.ToString();

                                DataRow dRow1;
                                dRow1 = TblCommon.NewRow();
                                dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                dRow1["Status"] = lines[4].ToString().Trim();
                                dRow1["ISO"] = lines[3].ToString().Trim();
                                //dRow1["Weight"] = lines[2].ToString().Trim();
                                dRow1["Agent"] = lines[11].ToString().Trim();
                                               //dRow1["POL"] = lines[12].ToString().Trim();
                                //dRow1["Reefer"] = lines[13].ToString();
                                dRow1["ValinIAL"] = wt.ToString();
                                dRow1["ValinN4"] = grwt;
                                dRow1["Description"] = "14";
                                TblCommon.Rows.Add(dRow1);
                            }
                            //Gr. Weight Exception---End---

                            //Agent Exception---Start---
                            DataView dv4 = new DataView(ds.Tables[0]);
                            dv4.RowFilter = "Line='" + lines[9].Trim() + "'";
                            string agent = dv[0][2].ToString();
                            //if (dv4.Count == 0)
                            if(agent.Trim()!=lines[11].ToString().Trim())
                            {
                                //dgvagentException.Rows.Add(lines[1], lines[9], agent, lines[2], lines[4]);
                                //lblagentException.Text = "Agent Exception: " + dgvagentException.RowCount.ToString();

                                DataRow dRow1;
                                dRow1 = TblCommon.NewRow();
                                dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                dRow1["Status"] = lines[4].ToString().Trim();
                                dRow1["ISO"] = lines[3].ToString().Trim();
                                dRow1["Weight"] = lines[2].ToString().Trim();
                                        //dRow1["Agent"] = lines[9].ToString().Trim();
                                        //dRow1["POL"] = lines[12].ToString().Trim();
                                //dRow1["Reefer"] = lines[13].ToString() ;
                                dRow1["ValinIAL"] = lines[11].ToString().Trim();
                                dRow1["ValinN4"] = agent;
                                dRow1["Description"] = "15";
                                TblCommon.Rows.Add(dRow1);
                            }
                            //Agent Exception---End---

                            //ISO Exception---Start---
                            DataView dv5 = new DataView(ds.Tables[0]);
                            dv5.RowFilter = "iso='" + lines[3] + "'";
                            string iso = dv[0][7].ToString();
                            if (dv5.Count == 0)
                            {
                                DataRow dRow1;
                                dRow1 = TblCommon.NewRow();
                                dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                dRow1["Status"] = lines[4].ToString().Trim();
                                //dRow1["ISO"] = lines[3].ToString().Trim();
                                dRow1["Weight"] = lines[2].ToString().Trim();
                                dRow1["Agent"] = lines[11].ToString().Trim();
                                             //dRow1["POL"] = lines[12].ToString().Trim();
                                //dRow1["Reefer"] = lines[13].ToString() ;
                                dRow1["ValinIAL"] = lines[3].ToString().Trim();
                                dRow1["ValinN4"] = iso;
                                dRow1["Description"] = "12";
                                TblCommon.Rows.Add(dRow1);
                            }
                            //ISO Exception---End--- 

                            //Hazardious Exception---Start----
                            //DataView dv6 = new DataView(ds.Tables[0]);
                            //dv6.RowFilter = "haznumber='" + lines[8] + "'";
                            string hzano = dv[0][8].ToString();
                            string unno = dv[0][9].ToString();
                            string N4hza = hzano + " - " + unno;
                            string IALhza = lines[5].ToString() + " - " + lines[6];
                            if (N4hza.ToString() != IALhza.ToString())
                            //if (dv6.Count == 0)
                            {
                                DataRow dRow1;
                                dRow1 = TblCommon.NewRow();
                                dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                dRow1["Status"] = lines[4].ToString().Trim();
                                dRow1["ISO"] = lines[3].ToString().Trim();
                                dRow1["Weight"] = lines[2].ToString().Trim();
                                dRow1["Agent"] = lines[11].ToString().Trim();
                                        //dRow1["POL"] = lines[12].ToString().Trim();
                                //dRow1["Reefer"] = lines[13].ToString();
                                dRow1["ValinIAL"] = IALhza;
                                dRow1["ValinN4"] = N4hza;
                                dRow1["Description"] = "16";
                                TblCommon.Rows.Add(dRow1);
                            }
                            //Hazardious Exception---End----

                           
                            ////Reefer Exception---Start----
                            //string reftemp = dv[0][10].ToString();
                            //string N4reefer = reftemp;
                            //string IALreef = lines[13].ToString();
                            //if (N4reefer.ToString() != IALreef.ToString())
                            ////if (dv6.Count == 0)
                            //{
                            //    DataRow dRow1;
                            //    dRow1 = TblCommon.NewRow();
                            //    dRow1["ContainerNo"] = lines[1].ToString().Trim();
                            //    dRow1["Status"] = lines[4].ToString().Trim();
                            //    dRow1["ISO"] = lines[3].ToString().Trim();
                            //    dRow1["Weight"] = lines[2].ToString().Trim();
                            //    dRow1["Agent"] = lines[11].ToString().Trim();
                            //    //dRow1["Reefer"] = lines[13].ToString() + " - " + lines[14].ToString();
                            //    //dRow1["POL"] = lines[12].ToString().Trim();
                            //    dRow1["ValinIAL"] = IALreef;
                            //    dRow1["ValinN4"] = N4reefer;
                            //    dRow1["Description"] = "17";
                            //    TblCommon.Rows.Add(dRow1);
                            //}
                            ////Reefer Exception---End----
                        }

                    }
                    else
                    {
                        try
                        {
                            string[] lines = Regex.Split(strLine, ",");
                            lblVesRefVal.Text = lines[3].ToString().Trim();//get the last value in the first line
                            //createfile(lines.ToString());
                            vesslid = lines[3].ToString().Trim();
                            ds.Clear(); // Nav 08-Aug
                            IALds.Clear(); // Nav 08-Aug
                            getDataForIAL();
                            intEALfromDB = ds.Tables[0].Rows.Count;
                            filename = ds.Tables[0].Rows[0]["vesselname"].ToString();
                            string time = System.DateTime.Now.ToString();
                            string strtime = time.Replace(":", "");
                            strtime = strtime.Replace(" ", "");
                            strtime = strtime.Replace("/", "-");
                            IALfilename = "IAL@" + filename + "@" + strtime + "";
                            lblVesRefVal.Text = lblVesRefVal.Text + " - " + ds.Tables[0].Rows[0]["vesselname"].ToString();
                            // Nav 08-Aug EALds.Tables.Add(TblEal);
                            ////if (lines[1].Trim().ToString().ToUpper() != "HDRADVANCE")
                            ////{
                            ////    MessageBox.Show("Invalid EAL file. Please check it.");
                            ////    //return;
                            ////}
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                            MessageBox.Show("Invalid IAL file. Please check it. 2");
                            //Application.ExitThread();

                            return;
                        }
                        // getdata();
                    }
                    lCnt = lCnt + 1;


                }

        }
        private DataTable CreateCommonTable()
        {
            DataColumn myDataColumn;
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ContainerNo";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Status";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ISO";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Weight";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Agent";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "POL";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ValinIAL";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ValinN4";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Description";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Reefer";
            TblCommon.Columns.Add(myDataColumn);

            return TblCommon;
        }
        private DataTable CreateIALTable()
        {
            //DataTable TblEal = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Container";
            TblIal.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Weight";
            TblIal.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ISO";
            TblIal.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Status";
            TblIal.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "POL";
            TblIal.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "LINEOP";
            TblIal.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Reefer";
            TblIal.Columns.Add(myDataColumn);

            return TblIal;
        }

        //---To get the details from the N4 data base based on the Vessel Id----Start-----
        public void getDataForIAL()
        {
            string CONN_STRING = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
            dbCon.Open();
            //string Query = "select unt.id Container,rout.id POD,lin.id Line, CATEGORY,FREIGHT_KIND FrieghtKind,GOODS_AND_CTR_WT_KG Weight from system.inv_unit unt, sYSTEM.INV_UNIT_FCY_VISIT vst,SYSTEM.ARGO_CARRIER_VISIT carr,SYSTEM.REF_BIZUNIT_SCOPED lin,SYSTEM.REF_ROUTING_POINT rout where  UNT.ACTIVE_UFV = VST.GKEY and VST.ACTUAL_OB_CV= CARR.GKEY and UNT.LINE_OP = lin.gkey and UNT.POD1_GKEY = ROUT.GKEY and unt.category in( 'TRSHP','EXPRT') and carr.id= '" + vesslid + "'";
            //string Query = "select unt.id Container,rout.id POD,lin.id Line, CATEGORY,decode(FREIGHT_KIND,'FCL', 'F','MTY', 'E') FrieghtKind,GOODS_AND_CTR_WT_KG/1000 WeightMT from " +
            //    "system.inv_unit unt, sYSTEM.INV_UNIT_FCY_VISIT vst,SYSTEM.ARGO_CARRIER_VISIT carr,SYSTEM.REF_BIZUNIT_SCOPED lin,SYSTEM.REF_ROUTING_POINT rout " +
            //    "where  UNT.gkey = VST.unit_GKEY and VST.ACTUAL_OB_CV= CARR.GKEY and UNT.LINE_OP = lin.gkey and UNT.POD1_GKEY = ROUT.GKEY and unt.category "
            //    + "in( 'TRSHP','EXPRT') and (VST.TRANSIT_STATE LIKE '%YARD%' or last_ops_pos_id = 'TIP' ) and carr.id= '" + vesslid + "'";
            string Query = "select unt.id Container,rout.id POD,lin.id Line, CATEGORY,decode(FREIGHT_KIND,'FCL', 'F','MTY', 'E') FrieghtKind,GOODS_AND_CTR_WT_KG/1000 WeightMT, " +
                "vsmst.name vesselname,eqptyp.id iso,HAZARDOUS, UN_NUM haznumber, to_char(haz.TEMP_REQD_C,'999999D9') TEMP_REQD_C  from " +
                "navis.inv_unit unt, navis.INV_UNIT_FCY_VISIT vst,navis.ARGO_CARRIER_VISIT carr,navis.REF_BIZUNIT_SCOPED lin,navis.REF_ROUTING_POINT rout , " +
                "navis.ARGO_VISIT_DETAILS vstdet, navis.VSL_VESSEL_VISIT_DETAILS vsvstdet, navis.VSL_VESSELS vsmst, navis.REF_EQUIP_TYPE eqptyp, " +
                "navis.REF_EQUIPMENT eqp, navis.inv_unit_equip unitequip, navis.INV_GOODS haz, navis.INV_HAZARD_ITEMS hazitm " +
                "where  UNT.gkey = VST.unit_GKEY and VST.ACTUAL_IB_CV= CARR.GKEY and UNT.LINE_OP = lin.gkey and UNT.POD1_GKEY = ROUT.GKEY and unt.category " +
                "in('IMPRT') and (VST.TRANSIT_STATE LIKE '%INBOUND%' or last_ops_pos_id = 'TIP' ) and carr.CVCVD_GKEY = vstdet.gkey and vsvstdet.VVD_GKEY = vstdet.gkey " +
                "and vsmst.gkey = vsvstdet.VESSEL_GKEY and unt.PRIMARY_UE = unitequip.gkey and unitequip. eq_gkey = eqp.gkey and UNT.GOODS = HAZ.GKEY and eqp.eqtyp_gkey = eqptyp.gkey " +
                "and HAZITM.HZRD_GKEY (+) = HAZ.HAZARDS_GKEY " +
                "and carr.id='" + vesslid + "'";
            OleDbCommand cmd = new OleDbCommand(Query, dbCon);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(da);
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);
            da.Fill(ds, "Query");
            //dataGridViewDB.DataMember = "Query";
            //dataGridViewDB.DataSource = ds;
            //dataGridViewDB.Visible = true;
            dbCon.Close();
        }
        //---To get the details from the N4 data base based on the Vessel Id----End-----

        //---To get containers in yard not in IAL---Start---
        private void getContInYardNotinIAL()
        {
            try
            {
                for (int lint1 = 0; lint1 < ds.Tables[0].Rows.Count; lint1++)
                {
                    //This is for Populating Containers in Yard not in EAL
                    DataView dvYard = IALds.Tables[0].DefaultView;
                    dvYard.RowFilter = "Container='" + ds.Tables[0].Rows[lint1][0].ToString() + "'";
                    //MessageBox.Show( dv.Count.ToString());

                    if (dvYard.Count == 0)
                    {
                        
                        DataRow dRow1;
                        dRow1 = TblCommon.NewRow();
                        dRow1["ContainerNo"] = ds.Tables[0].Rows[lint1][0].ToString().Trim();
                        dRow1["Status"] = ds.Tables[0].Rows[lint1][4].ToString().Trim();
                        dRow1["ISO"] = ds.Tables[0].Rows[lint1][7].ToString().Trim();
                        dRow1["Weight"] = ds.Tables[0].Rows[lint1][5].ToString().Trim();
                        dRow1["Agent"] = ds.Tables[0].Rows[lint1][2].ToString().Trim();
                        dRow1["Reefer"] = ds.Tables[0].Rows[lint1][8].ToString().Trim() + " - " + ds.Tables[0].Rows[lint1][9].ToString().Trim();//Need to change based on the DB--ds index
                        //dRow1["POL"] = ds.Tables[0].Rows[lint1][1].ToString().Trim();
                        dRow1["Description"] = "11";
                        TblCommon.Rows.Add(dRow1);

                        //dgNotinEAL.Rows.Add(ds.Tables[0].Rows[lint1][0], ds.Tables[0].Rows[lint1][4], ds.Tables[0].Rows[lint1][5], ds.Tables[0].Rows[lint1][2], ds.Tables[0].Rows[lint1][1]);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Invalid IAL file. Please check it. 1");
                //dataGridViewDB.Rows.Clear();
                //dataGridViewEAL.Rows.Clear();
                //dgvNotinYard.Rows.Clear();
                //dgNotinEAL.Rows.Clear();
            }
        }
        //---To get containers in yard not in IAL---End---

        //---To get the Common files Containers data in IAL file and N4 database----Start----
        private void CompareIALandDB()
        {
            string strLine1 = "";
            int lCnt1 = 0;
            using (StreamReader reader = new StreamReader(txtIALfilePath.Text.Trim()))

                while ((strLine1 = reader.ReadLine()) != null)
                {

                    if (lCnt1 != 0)
                    {

                        string[] lines = Regex.Split(strLine1, ",");
                        //dataGridViewEAL.Rows.Add(lines[1], lines[4], lines[3], lines[2], lines[9], lines[5]);

                        DataView dv = ds.Tables[0].DefaultView;
                        dv.RowFilter = "Container='" + lines[1] + "'";
                        if (dv.Count != 0)
                        {
                            //DataView dv1 = new DataView(ds.Tables[0]);
                            //dv1.RowFilter = "POD='" + lines[5] + "'";

                            DataView dv2 = new DataView(ds.Tables[0]);
                            dv2.RowFilter = "FrieghtKind='" + lines[4] + "'";

                            DataView dv3 = new DataView(ds.Tables[0]);
                            double wt = Convert.ToDouble(lines[2].ToString());
                            dv3.RowFilter = "WeightMT='" + wt + "'";

                            DataView dv4 = new DataView(ds.Tables[0]);
                            dv4.RowFilter = "Line='" + lines[11] + "'";

                            DataView dv5 = new DataView(ds.Tables[0]);
                            dv5.RowFilter = "iso='" + lines[3] + "'";

                            string hzano = dv[0][8].ToString();
                            string unno = dv[0][9].ToString();
                            string N4hza = hzano + " - " + unno;
                            string EALhza = lines[5].ToString() + " - " + lines[6];

                            string reftemp = dv[0][10].ToString();
                            
                            string N4reefer = reftemp ;
                            //string IALreef = lines[13].ToString();

                            if (dv2.Count != 0 && dv3.Count != 0 && dv4.Count != 0 && dv5.Count != 0 && N4hza.ToString() == EALhza.ToString() )
                            {
                                writeNewIAL(strLine1);
                            }

                        }

                    }
                    else if (lCnt1 == 0)
                    {
                        writeNewIAL(strLine1);
                        lCnt1 = lCnt1 + 1;
                    }
                    else
                    {
                        lCnt1 = lCnt1 + 1;
                    }

                }
        }
        //---To get the Common files Containers data in IAL file and N4 database----End----

        //---To Write the text in new IAL file----Start----
        private void writeNewIAL(string txt)
        {
            using (System.IO.StreamWriter fwrite = new System.IO.StreamWriter(@"c:\KPT\" + IALfilename + ".txt", true))

                try
                {
                    fwrite.WriteLine(txt.ToString());
                }
                finally
                {
                    fwrite.Close();
                }
        }
        //---To Write the text in new IAL file----End----

        //---Toget the Header Id from MS Access DB---Start----
        private void getDiscidfromHdr()
        {

            string CONN = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            OleDbConnection db_Con = new OleDbConnection(CONN);
            db_Con.Open();
            string sql;
            sql = "select count(*)+ 1  from Ct_DescHdr";
            OleDbCommand cmd = new OleDbCommand(sql, db_Con);
            int flg;
            flg = int.Parse(cmd.ExecuteScalar().ToString());
            discid = flg.ToString();
            string createddate = System.DateTime.Now.ToString();
            DateTime dt1 = Convert.ToDateTime(createddate);
            string cdate = DateTime.Now.ToString("mm/dd/yyyy HH:mm:ss");
            try
            {
                string vname = ds.Tables[0].Rows[0]["vesselname"].ToString();
                string insert;
                insert = "Insert into Ct_DescHdr (Discid,VesselRef, VesselName,CreatedDate,CreatedBy) Values('" + flg + "','" + vesslid + "','" + vname + "','" + cdate + "','" + user + "')";
                OleDbCommand cmd1 = new OleDbCommand(insert, db_Con);
                cmd1.ExecuteNonQuery();
                db_Con.Close();
            }
            catch
            {
                MessageBox.Show("Vessel Details Does Not exist");
            }
        }
        //---Toget the Header Id from MS Access DB---End----

        //---inserting data into MS Access DB---Start----
        private void inserttodb()
        {
            string CONN = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            OleDbConnection db_Con = new OleDbConnection(CONN);

            using (db_Con)
            {
                db_Con.Open();
                //var adapter = new OleDbDataAdapter();
                string strSql = "";

                for (int lint = 0; lint < TblCommon.Rows.Count; lint++)
                {
                    strSql = "INSERT INTO ct_descdtl (ContainerNo,ConStatus,ISO,Weight,Agent,POD,ValinEAL,ValinN4,DiscType,Reefer,VesseRef,Discid) VALUES ( '" + TblCommon.Rows[lint]["ContainerNo"].ToString() +
                    "','" + TblCommon.Rows[lint]["Status"].ToString() + "','" + TblCommon.Rows[lint]["ISO"].ToString() + "','" + TblCommon.Rows[lint]["Weight"] + "','" + TblCommon.Rows[lint]["Agent"].ToString() +
                    "','" + TblCommon.Rows[lint]["POL"].ToString() + "','" + TblCommon.Rows[lint]["ValinIAL"].ToString() + "','" + TblCommon.Rows[lint]["ValinN4"].ToString() +
                    "','" + TblCommon.Rows[lint]["Description"].ToString() + "','" + TblCommon.Rows[lint]["Reefer"].ToString() + "','" + vesslid + "','" + discid + "')";
                    OleDbCommand cmd = new OleDbCommand(strSql, db_Con);
                    cmd.ExecuteNonQuery();
                }
                db_Con.Close();
            }
            
        }
        //---inserting data into MS Access DB---End----

        private void showreport()
        {
            IALReport objReport = new IALReport();
            objReport.SetDatabaseLogon("", "", "Microsoft.Jet.OLEDB.4.0", Application.StartupPath + "C:\\NavisDB\\KPTDiscre.mdb", true);

            //write formula to pass parameters to report  
            string rptname = Application.StartupPath + "\\crptPrintReceipt1.rpt";
            rptIALReport.SelectionFormula = "{ct_deschdr.discid} =" + Convert.ToInt16(discid);
            rptIALReport.ReportSource = objReport;
            rptIALReport.Visible = true;
        }

        
    }
}