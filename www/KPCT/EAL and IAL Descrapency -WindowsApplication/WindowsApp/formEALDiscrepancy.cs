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
    public partial class formEALDiscrepancy : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public formEALDiscrepancy()
        {
            InitializeComponent();
        }
        string cid;
        string vesslid;
        string discid;
        string user;
        string EALfilename;
        string filename;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataSet EALds = new DataSet();
        DataTable TblEal = new DataTable();
        DataSet Commonds = new DataSet();
        DataTable TblCommon = new DataTable();
        private void btnBowse_Click(object sender, EventArgs e)
        {
            lblVesRefVal.Text = "";
            TblCommon.Clear();
            rptEALReport.Visible = false;
            txtEALfilePath.Text = "";
            OpenFileDialog opendialog = new OpenFileDialog();
            opendialog.Filter = "Text Files|*.txt";
            if (opendialog.ShowDialog() == DialogResult.OK)
            {
                txtEALfilePath.Text = opendialog.FileName.ToString();

                if (txtEALfilePath.Text != "")
                {
                    createNewEAL();
                    getInEALNotInYard();
                    getContInYardNotinEAL();
                    CompareEALandDB();
                    //lblNotinEALcount.Text = dgNotinEAL.Rows.Count.ToString();
                    getDiscidfromHdr();
                    inserttodb();
                    showreport();
                }
            }
        }
        public void consolidate()
        {
            try
            {
                for (int i = 1; i <= 9; i++)
                {
                    if (i == 9)
                        i++;
                    string CONN = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    OleDbConnection db_Con = new OleDbConnection(CONN);

                    db_Con.Open();
                    string strSql = "Select count(*) from ct_descdtl where Discid=" + discid + " and DiscType=" + i + "";
                    OleDbCommand cmd = new OleDbCommand(strSql, db_Con);
                    int flg;
                    flg = int.Parse(cmd.ExecuteScalar().ToString());
                    db_Con.Close();
                    if (flg == 0)
                    {
                        db_Con.Open();
                        string Sql = "INSERT INTO ct_descdtl (ContainerNo,ConStatus,ISO,Weight,Agent,POD,ValinEAL,ValinN4,DiscType,VesseRef,Discid) VALUES ( 'NIL','-','-','-','-','-','NIL','NIL','" + i + "','" + vesslid + "','" + discid + "')";
                        OleDbCommand cmd1 = new OleDbCommand(Sql, db_Con);
                        cmd1.ExecuteNonQuery();
                        db_Con.Close();
                        flg = 0;
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void showreport()
        {
            CrystalReport1 objReport = new CrystalReport1();
           
            //crystalReportViewer1.DisplayGroupTree = false;
           // objReport.SetDatabaseLogon("", "", "Microsoft.Jet.OLEDB.4.0", Application.StartupPath + "\\kpcl30\\VSS-Share\\NavisDB\\KPTDiscre.mdb", true);
            rptEALReport.DisplayGroupTree = false;
            //write formula to pass parameters to report  
            string rptname = Application.StartupPath + "\\crptPrintReceipt.rpt";
            rptEALReport.SelectionFormula = "{ct_deschdr.discid} =" + Convert.ToInt16(discid);
            rptEALReport.ReportSource = objReport;

            rptEALReport.Visible = true;
            rptEALReport.DisplayGroupTree = false;
        }
        //---To get containers in yard not in EAL---Start---
        private void getContInYardNotinEAL()
        {
            try
            {
                for (int lint1 = 0; lint1 < ds.Tables[0].Rows.Count; lint1++)
                {
                    //This is for Populating Containers in Yard not in EAL
                    DataView dvYard = EALds.Tables[0].DefaultView;
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
                        dRow1["POD"] = ds.Tables[0].Rows[lint1][1].ToString().Trim();
                        dRow1["Description"] = "1";//2
                        TblCommon.Rows.Add(dRow1);

                        //dgNotinEAL.Rows.Add(ds.Tables[0].Rows[lint1][0], ds.Tables[0].Rows[lint1][4], ds.Tables[0].Rows[lint1][5], ds.Tables[0].Rows[lint1][2], ds.Tables[0].Rows[lint1][1]);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Invalid EAL file. Please check it. 1");
                //dataGridViewDB.Rows.Clear();
                //dataGridViewEAL.Rows.Clear();
                //dgvNotinYard.Rows.Clear();
                //dgNotinEAL.Rows.Clear();
            }
        }
        //---To get containers in yard not in EAL---End---

        private void getInEALNotInYard()
        {
            string strLine = "";
            int intEALfromDB = 0;
            int lCnt = 0;

            using (StreamReader reader = new StreamReader(txtEALfilePath.Text.Trim()))

                while ((strLine = reader.ReadLine()) != null )
                {

                    if (lCnt != 0)
                    {

                        //if (strLine.Trim().ToString() == "")
                        //{
                        //   //lCnt++;
                        //}
                        //else
                        //{
                            string[] lines = Regex.Split(strLine, ",");

                            //dataGridViewEAL.Rows.Add(lines[1], lines[4], lines[3], lines[2], lines[9], lines[5]);

                            cid = lines[1].ToString().Trim();

                            DataRow dRow;
                            dRow = TblEal.NewRow();
                            dRow["Container"] = lines[1].ToString().Trim();
                            dRow["Weight"] = lines[2].ToString().Trim();
                            dRow["ISO"] = lines[3].ToString().Trim();
                            dRow["Status"] = lines[4].ToString().Trim();
                            dRow["POD"] = lines[5].Substring(2, 3).ToString().Trim();
                            dRow["LINEOP"] = lines[9].ToString().Trim();

                            TblEal.Rows.Add(dRow);


                            //This is for Populating Containers in EAL not in Yard
                            DataView dv = ds.Tables[0].DefaultView;
                            dv.RowFilter = "Container='" + lines[1] + "'";
                            if (dv.Count == 0)
                            {
                                //dgvNotinYard.Rows.Add(lines[1], lines[4], lines[3], lines[2], lines[9], lines[5]);
                                //lblNotinYardCount.Text = dgvNotinYard.Rows.Count.ToString();

                                DataRow dRow1;
                                dRow1 = TblCommon.NewRow();
                                dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                dRow1["Status"] = lines[4].ToString().Trim();
                                dRow1["ISO"] = lines[3].ToString().Trim();
                                dRow1["Weight"] = lines[2].ToString().Trim();
                                dRow1["Agent"] = lines[9].ToString().Trim();
                                dRow1["POD"] = lines[5].Substring(2, 3).ToString().Trim();
                                dRow1["Description"] = "2";  //1
                                TblCommon.Rows.Add(dRow1);

                            }
                            else if (dv.Count == 1)
                            {
                                //POD Exception---Start----
                                DataView dv1 = new DataView(ds.Tables[0]);
                                dv1.RowFilter = "POD='" + lines[5].Substring(2, 3) + "'";
                                string po = lines[5].Substring(2, 3);
                                string pod = dv[0][1].ToString();
                                if (dv[0][1].ToString().Trim() != po.ToString().Trim())
                                {
                                    //dgvPODException.Rows.Add(lines[1], lines[5], pod, lines[9], lines[2]);
                                    //lblPODException.Text = "POD Exception: " + dgvPODException.RowCount.ToString();


                                    DataRow dRow1;
                                    dRow1 = TblCommon.NewRow();
                                    dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                    dRow1["Status"] = lines[4].ToString().Trim();
                                    dRow1["ISO"] = lines[3].ToString().Trim();
                                    dRow1["Weight"] = lines[2].ToString().Trim();
                                    dRow1["Agent"] = lines[9].ToString().Trim();
                                    //dRow1["POD"] = lines[5].ToString().Trim();
                                    dRow1["ValinEAL"] = lines[5].Substring(2, 3).ToString().Trim();
                                    dRow1["ValinN4"] = pod;
                                    dRow1["Description"] = "3";//4
                                    TblCommon.Rows.Add(dRow1);
                                }
                                //POD Exception---End----

                                //Ststus Exception---Start---
                                DataView dv2 = new DataView(ds.Tables[0]);
                                dv2.RowFilter = "FrieghtKind='" + lines[4] + "'";
                                string status = dv[0][4].ToString();
                                //if (dv2.Count == 0)
                                if (dv[0][4].ToString().Trim() != lines[4].ToString().Trim())
                                {
                                    //dgvStatusException.Rows.Add(lines[1], lines[4], status, lines[9], lines[2]);
                                    //lblStatusException.Text = "Status Exception: " + dgvStatusException.RowCount.ToString();

                                    DataRow dRow1;
                                    dRow1 = TblCommon.NewRow();
                                    dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                    //dRow1["Status"] = lines[4].ToString().Trim();
                                    dRow1["ISO"] = lines[3].ToString().Trim();
                                    dRow1["Weight"] = lines[2].ToString().Trim();
                                    dRow1["Agent"] = lines[9].ToString().Trim();
                                    dRow1["POD"] = lines[5].Substring(2, 3).ToString().Trim();
                                    dRow1["ValinEAL"] = lines[4].ToString().Trim();
                                    dRow1["ValinN4"] = status;
                                    dRow1["Description"] = "6";//5
                                    TblCommon.Rows.Add(dRow1);
                                }
                                //Ststus Exception---End---

                                //Gr. Weight Exception---Start---
                                DataView dv3 = new DataView(ds.Tables[0]);
                                double wt = Convert.ToDouble(lines[2].ToString());
                                dv3.RowFilter = "WeightMT='" + wt + "'";
                                string grwt = dv[0][5].ToString();
                                //if (dv3.Count == 0)
                                if (wt.ToString() != grwt.ToString())
                                {
                                    //dgvgrwtException.Rows.Add(lines[1], wt, grwt, lines[9], lines[4]);
                                    //lblgrwtException.Text = "Gr. Weight Exception: " + dgvgrwtException.RowCount.ToString();

                                    DataRow dRow1;
                                    dRow1 = TblCommon.NewRow();
                                    dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                    dRow1["Status"] = lines[4].ToString().Trim();
                                    dRow1["ISO"] = lines[3].ToString().Trim();
                                    //dRow1["Weight"] = lines[2].ToString().Trim();
                                    dRow1["Agent"] = lines[9].ToString().Trim();
                                    dRow1["POD"] = lines[5].Substring(2, 3).ToString().Trim();
                                    dRow1["ValinEAL"] = wt.ToString();
                                    dRow1["ValinN4"] = grwt;
                                    dRow1["Description"] = "8";//6
                                    TblCommon.Rows.Add(dRow1);
                                }
                                //Gr. Weight Exception---End---

                                //Agent Exception---Start---
                                DataView dv4 = new DataView(ds.Tables[0]);
                                dv4.RowFilter = "Line='" + lines[9] + "'";
                                string agent = dv[0][2].ToString();
                                if (lines[9].ToString().Trim() != dv[0][2].ToString().Trim())
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
                                    dRow1["POD"] = lines[5].Substring(2, 3).ToString().Trim();
                                    dRow1["ValinEAL"] = lines[9].ToString().Trim();
                                    dRow1["ValinN4"] = agent;
                                    dRow1["Description"] = "9";//8
                                    TblCommon.Rows.Add(dRow1);
                                }
                                //Agent Exception---End---

                                //ISO Exception---Start---
                                DataView dv5 = new DataView(ds.Tables[0]);
                                dv5.RowFilter = "iso='" + lines[3] + "'";
                                string iso = dv[0][7].ToString();
                                if (lines[3].ToString().Trim() != dv[0][7].ToString().Trim())
                                {
                                    DataRow dRow1;
                                    dRow1 = TblCommon.NewRow();
                                    dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                    dRow1["Status"] = lines[4].ToString().Trim();
                                    //dRow1["ISO"] = lines[3].ToString().Trim();
                                    dRow1["Weight"] = lines[2].ToString().Trim();
                                    dRow1["Agent"] = lines[9].ToString().Trim();
                                    dRow1["POD"] = lines[5].Substring(2, 3).ToString().Trim();
                                    dRow1["ValinEAL"] = lines[3].ToString().Trim();
                                    dRow1["ValinN4"] = iso;
                                    dRow1["Description"] = "7";//3
                                    TblCommon.Rows.Add(dRow1);
                                }
                                //ISO Exception---End--- 

                                //Hazardious Exception---Start----
                                //DataView dv6 = new DataView(ds.Tables[0]);
                                //dv6.RowFilter = "haznumber='" + lines[8] + "'";
                                string hzano = dv[0][8].ToString();
                                string unno = dv[0][9].ToString();
                                string N4hza = hzano + " - " + unno;
                                string EALhza = lines[7].ToString() + " - " + lines[8];
                                if (N4hza.ToString() != EALhza.ToString())
                                //if (dv6.Count == 0)
                                {
                                    DataRow dRow1;
                                    dRow1 = TblCommon.NewRow();
                                    dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                    dRow1["Status"] = lines[4].ToString().Trim();
                                    dRow1["ISO"] = lines[3].ToString().Trim();
                                    dRow1["Weight"] = lines[2].ToString().Trim();
                                    dRow1["Agent"] = lines[9].ToString().Trim();
                                    dRow1["POD"] = lines[5].Substring(2, 3).ToString().Trim();
                                    dRow1["ValinEAL"] = EALhza;
                                    dRow1["ValinN4"] = N4hza;
                                    dRow1["Description"] = "5";//7
                                    TblCommon.Rows.Add(dRow1);
                                }
                                //Hazardious Exception---End----

                                //UN No Exception----Start----
                                //string unno = dv[0][8].ToString();
                                //if (unno.ToString() != lines[7].ToString())
                                //{
                                //    DataRow dRow1;
                                //    dRow1 = TblCommon.NewRow();
                                //    dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                //    dRow1["Status"] = lines[4].ToString().Trim();
                                //    dRow1["ISO"] = lines[3].ToString().Trim();
                                //    dRow1["Weight"] = lines[2].ToString().Trim();
                                //    dRow1["Agent"] = lines[9].ToString().Trim();
                                //    dRow1["POD"] = lines[5].ToString().Trim();
                                //    dRow1["ValinEAL"] = lines[7].ToString().Trim();
                                //    dRow1["ValinN4"] = unno;
                                //    dRow1["Description"] = "9";
                                //    TblCommon.Rows.Add(dRow1);
                                //}
                                //UN No Exception----End----

                                //Reefer Exception----Start----

                                string refinc = dv[0][10].ToString();
                                if (refinc.ToString().Trim() != lines[11].ToString().Trim())
                                {
                                    DataRow dRow1;
                                    dRow1 = TblCommon.NewRow();
                                    dRow1["ContainerNo"] = lines[1].ToString().Trim();
                                    dRow1["Status"] = lines[4].ToString().Trim();
                                    dRow1["ISO"] = lines[3].ToString().Trim();
                                    dRow1["Weight"] = lines[2].ToString().Trim();
                                    dRow1["Agent"] = lines[9].ToString().Trim();

                                    dRow1["ValinEAL"] = lines[11].ToString().Trim();
                                    dRow1["ValinN4"] = refinc.ToString();
                                    dRow1["Description"] = "4";//18
                                    TblCommon.Rows.Add(dRow1);
                                }
                                //Reefer Exception----End----
                            }
                        //}
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
                            EALds.Clear(); // Nav 08-Aug
                            getDataForEAL();
                            intEALfromDB = ds.Tables[0].Rows.Count;
                            filename = ds.Tables[0].Rows[0]["vesselname"].ToString();
                            string time = System.DateTime.Now.ToString();
                            string strtime = time.Replace(":", "");
                            strtime = strtime.Replace(" ", "");
                            strtime = strtime.Replace("/", "-");
                            EALfilename = "EAL@" + filename + "@" + strtime + "";
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
                            //MessageBox.Show(ex.Message.ToString());
                            //MessageBox.Show("Invalid EAL file. Please check it. 2");
                            //Application.ExitThread();
                            MessageBox.Show("Vessel Details Does Not exist in DB (or) Invalid EAL file." + ex.Message.ToString());
                            
                            return;
                        }
                        // getdata();
                    }
                    lCnt = lCnt + 1;


                }

        }
        //---To get the details from the N4 data base based on the Vessel Id----Start-----
        public void getDataForEAL()
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
                "vsmst.name vesselname,eqptyp.id iso,HAZARDOUS, UN_NUM haznumber,to_char(haz.TEMP_REQD_C,'999999D9') TEMP_REQD_C  from " +
                "navis.inv_unit unt, navis.INV_UNIT_FCY_VISIT vst,navis.ARGO_CARRIER_VISIT carr,navis.REF_BIZUNIT_SCOPED lin,navis.REF_ROUTING_POINT rout , " +
                "navis.ARGO_VISIT_DETAILS vstdet, navis.VSL_VESSEL_VISIT_DETAILS vsvstdet, navis.VSL_VESSELS vsmst, navis.REF_EQUIP_TYPE eqptyp, " +
                "navis.REF_EQUIPMENT eqp, navis.inv_unit_equip unitequip, navis.INV_GOODS haz, navis.INV_HAZARD_ITEMS hazitm " +
                "where  UNT.gkey = VST.unit_GKEY and VST.ACTUAL_OB_CV= CARR.GKEY and UNT.LINE_OP = lin.gkey and UNT.POD1_GKEY = ROUT.GKEY and unt.category " +
                "in( 'TRSHP','EXPRT') and (VST.TRANSIT_STATE LIKE '%YARD%' or last_ops_pos_id = 'TIP' ) and carr.CVCVD_GKEY = vstdet.gkey and vsvstdet.VVD_GKEY = vstdet.gkey " +
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

        private void formEALDiscrepancy_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"C:\KPT");
            DataTable TblEal = CreateEALTable();
            EALds.Tables.Add(TblEal);
            //DataTable TblCommon = CreateCommonTable();
            TblCommon = CreateCommonTable();
            Commonds.Tables.Add(TblCommon);
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
            myDataColumn.ColumnName = "POD";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ValinEAL";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ValinN4";
            TblCommon.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Description";
            TblCommon.Columns.Add(myDataColumn);

            return TblCommon;
        }

        private DataTable CreateEALTable()
        {
            //DataTable TblEal = new DataTable();
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

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "LINEOP";
            TblEal.Columns.Add(myDataColumn);

            return TblEal;
        }

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
                    strSql = "INSERT INTO ct_descdtl (ContainerNo,ConStatus,ISO,Weight,Agent,POD,ValinEAL,ValinN4,DiscType,VesseRef,Discid) VALUES ( '" + TblCommon.Rows[lint]["ContainerNo"].ToString() +
                    "','" + TblCommon.Rows[lint]["Status"].ToString() + "','" + TblCommon.Rows[lint]["ISO"].ToString() + "','" + TblCommon.Rows[lint]["Weight"] + "','" + TblCommon.Rows[lint]["Agent"].ToString() +
                    "','" + TblCommon.Rows[lint]["POD"].ToString() + "','" + TblCommon.Rows[lint]["ValinEAL"].ToString() + "','" + TblCommon.Rows[lint]["ValinN4"].ToString() +
                    "','" + TblCommon.Rows[lint]["Description"].ToString() + "','" + vesslid + "','" + discid + "')";
                    OleDbCommand cmd = new OleDbCommand(strSql, db_Con);
                    cmd.ExecuteNonQuery();
                }
                db_Con.Close();
                //adapter.InsertCommand = new OleDbCommand("INSERT INTO ct_descdtl (descid, ContainerNo,Status,ISO,Weight,Agent,POD,ValinEAL,ValinN4) VALUES (@Description , @ContainerNo,@Status,@ISO,@Weight,@Agent,@POD,@ValinEAL,@ValinN4)", db_Con);
                //adapter.InsertCommand.Parameters.Add("Description", OleDbType.VarChar, 40, "Description");
                //adapter.InsertCommand.Parameters.Add("ContainerNo", OleDbType.VarChar, 24, "ContainerNo");
                //adapter.InsertCommand.Parameters.Add("Status", OleDbType.VarChar, 24, "Status");
                //adapter.InsertCommand.Parameters.Add("ISO", OleDbType.VarChar, 24, "ISO");
                //adapter.InsertCommand.Parameters.Add("Weight", OleDbType.VarChar, 24, "Weight");
                //adapter.InsertCommand.Parameters.Add("Agent", OleDbType.VarChar, 24, "Agent");
                //adapter.InsertCommand.Parameters.Add("POD", OleDbType.VarChar, 24, "POD");
                //adapter.InsertCommand.Parameters.Add("ValinEAL", OleDbType.VarChar, 24, "ValinEAL");
                //adapter.InsertCommand.Parameters.Add("ValinN4", OleDbType.VarChar, 24, "ValinN4");
            }
            //foreach(DataRow row in TblCommon
            /////////////////////consolidate();
        }
        //---inserting data into MS Access DB---End----

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

        //---To create new EAL file----Start----
        private void createNewEAL()
        {
            TextWriter tw = new StreamWriter(@"C:\KPT\" + EALfilename + ".txt");
            tw.Close();
        }
        //---To create new EAL file----End----

        //---To Write the text in new EAL file----Start----
        private void writeNewEAL(string txt)
        {
            using (System.IO.StreamWriter fwrite = new System.IO.StreamWriter(@"C:\KPT\" + EALfilename + ".txt", true))

                try
                {
                    fwrite.WriteLine(txt.ToString());
                }
                finally
                {
                    fwrite.Close();
                }
        }
        //---To Write the text in new EAL file----End----

        //---To get the Common files Containers data in EAL file and N4 database----Start----
        private void CompareEALandDB()
        {
            string strLine1 = "";
            int lCnt1 = 0;
            using (StreamReader reader = new StreamReader(txtEALfilePath.Text.Trim()))

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
                            DataView dv1 = new DataView(ds.Tables[0]);
                            dv1.RowFilter = "POD='" + lines[5] + "'";

                            DataView dv2 = new DataView(ds.Tables[0]);
                            dv2.RowFilter = "FrieghtKind='" + lines[4] + "'";

                            DataView dv3 = new DataView(ds.Tables[0]);
                            double wt = Convert.ToDouble(lines[2].ToString());
                            dv3.RowFilter = "WeightMT='" + wt + "'";
                            string grwt = dv[0][5].ToString();

                            DataView dv4 = new DataView(ds.Tables[0]);
                            dv4.RowFilter = "Line='" + lines[9] + "'";
                            string po = lines[5].Substring(2, 3).ToString().Trim();
                            DataView dv5 = new DataView(ds.Tables[0]);
                            dv5.RowFilter = "iso='" + lines[3] + "'";

                            string hzano = dv[0][8].ToString();
                            string unno = dv[0][9].ToString();
                            string N4hza = hzano + " - " + unno;
                            string EALhza = lines[7].ToString() + " - " + lines[8];

                            string refinc = dv[0][10].ToString();

                            if (lines[1].ToString() == dv[0][0].ToString() && dv[0][4].ToString().Trim() != lines[4].ToString().Trim() && wt.ToString() == grwt.ToString() && dv4.Count != 0 && lines[3].ToString().Trim() == dv[0][7].ToString().Trim() && N4hza.ToString() == EALhza.ToString() && refinc.ToString().Trim() == lines[11].ToString().Trim() && po.Trim() == dv[0][1].ToString())
                            {
                                writeNewEAL(strLine1);
                            }

                        }

                    }
                    else if (lCnt1 == 0)
                    {
                        writeNewEAL(strLine1);
                        lCnt1 = lCnt1 + 1;
                    }
                    else
                    {
                        lCnt1 = lCnt1 + 1;
                    }

                }
        }

        private void kryptonPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        //---To get the Common files Containers data in EAL file and N4 database----End----
    }
}