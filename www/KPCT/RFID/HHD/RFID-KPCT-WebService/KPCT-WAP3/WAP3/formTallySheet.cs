using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace WAP3
{
    public partial class formTallySheet : Form
    {
        string localConnection = classServerDetails.SdfConnection;
        string TAG;
        string readerno;
        public formTallySheet(string tagno,string truckno,string party,string CHA,string cargo,string location,string Qty)
        {
            InitializeComponent();
            txtTruckNo.Text = truckno.ToString();
            txtPartyName.Text = party.ToString();
            txtCHA.Text = CHA.ToString();
            txtCargo.Text = cargo.ToString();
            txtLocation.Text = location.ToString();
            txtQty.Text = Qty.ToString();
            TAG = tagno;
        }




        //private void txtRows_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(txtRows.Text, "[^0-9]"))
        //    {
        //        MessageBox.Show("Please enter only numbers.");
        //        //MessageBox.Show("Please enter only numbers.");
        //        txtRows.Text = "";
        //    }
        //}
        int A = 1;
        
        //private void btnStart_Click(object sender, EventArgs e)
        //{
        //    if (txtRows.Text.Trim() == "0" || txtRows.Text.Trim() == "")
        //    {
        //        MessageBox.Show("");
        //    }
        //    else
        //    {
        //        for (int i = 1; i <= 9; i++)
        //        {
        //            workTable.Columns.Add(i.ToString(), typeof(String));

        //            //AddNewTextBox();
        //        }
        //        dataGridTallySheet.DataSource = workTable;

        //        //--Start-- Grid Style---
        //        dataGridTallySheet.TableStyles.Clear();
        //        DataGridTableStyle tableStyle = new DataGridTableStyle();
        //        tableStyle.MappingName = workTable.TableName;
        //        foreach (DataColumn item in workTable.Columns)
        //        {
        //            DataGridTextBoxColumn tbcName = new DataGridTextBoxColumn();
        //            tbcName.Width = 100;
        //            tbcName.MappingName = item.ColumnName;
        //            tbcName.HeaderText = item.ColumnName;
        //            tableStyle.GridColumnStyles.Add(tbcName);
        //        }
        //        dataGridTallySheet.TableStyles.Add(tableStyle);
        //        //--End-- Grid Style---
        //    }
        //}

        public System.Windows.Forms.TextBox AddNewTextBox()
        {
            //--Start-- Create Text Boxes---
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.tabPage1.Controls.Add(txt);
            txt.Top = A * 40;
            txt.Left = 25;
            txt.Text = "Row" + this.A.ToString();
            A = A + 1;
            return txt;
            //--End-- Create Text Boxes---
        }

        private void AddDetailstoDataGrid()
        {
            //if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "" && textBox8.Text == "" && textBox9.Text == "")
            //{
            //    MessageBox.Show("Please Enter details.");
            //}
            //else if (textBox1.Text == "0" && textBox2.Text == "0" && textBox3.Text == "0" && textBox4.Text == "0" && textBox5.Text == "0" && textBox6.Text == "0" && textBox7.Text == "0" && textBox8.Text == "0" && textBox9.Text == "0")
            //{
            //    MessageBox.Show("Please Enter details.");
            //}
            //else
            //{
            //    if (textBox1.Text == "") textBox1.Text = "0";
            //    if (textBox2.Text == "") textBox2.Text = "0";
            //    if (textBox3.Text == "") textBox3.Text = "0";
            //    if (textBox4.Text == "") textBox4.Text = "0";
            //    if (textBox5.Text == "") textBox5.Text = "0";
            //    if (textBox6.Text == "") textBox6.Text = "0";
            //    if (textBox7.Text == "") textBox7.Text = "0";
            //    if (textBox8.Text == "") textBox8.Text = "0";
            //    if (textBox9.Text == "") textBox9.Text = "0";
                object[] o = { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text };
                workTable.Rows.Add(o);
                dataGridTallySheet.DataSource = workTable;
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = "";
                //MessageBox.Show("Details Added.");

                //Display Total Qty and Status---Start---
                int sum = 0;
                for (int i = 1; i <= 8; i++)
                {
                    foreach (DataRow dr in workTable.Rows)
                    {
                        sum += Convert.ToInt32(dr[i.ToString()]);
                    }
                }
                txtOpQty.Text=txtTotalQty1.Text =  sum.ToString();
                //WAP3.formKPCTOps r = new WAP3.formKPCTOps(1);
                ////r.txtOpQty.Text = txtTotalQty.Text;
                //r.btnVerify_Click(TAG);

            //}

            lblStatus1.Text= lblStatus.Text = workTable.Rows.Count.ToString() + " Rows Inserted.";
            //Display Total Qty and Status---End---


            //this.Close();
        }


        #region DataGid
        DataTable workTable = new DataTable("TallySheet");
        public void DataGridArrangements()
        {
            for (int i = 1; i <= 8; i++)
            {
                workTable.Columns.Add(i.ToString(), typeof(String));

                //AddNewTextBox();
            }
            dataGridTallySheet.DataSource = workTable;

            //--Start-- Grid Style---
            dataGridTallySheet.TableStyles.Clear();
            DataGridTableStyle tableStyle = new DataGridTableStyle();
            tableStyle.MappingName = workTable.TableName;
            foreach (DataColumn item in workTable.Columns)
            {
                DataGridTextBoxColumn tbcName = new DataGridTextBoxColumn();
                tbcName.Width = 49;
                tbcName.MappingName = item.ColumnName;
                tbcName.HeaderText = item.ColumnName;
                tableStyle.GridColumnStyles.Add(tbcName);
            }
            dataGridTallySheet.TableStyles.Add(tableStyle);
            //--End-- Grid Style---

        }

        private void LoadTallySheetDetails()
        {
            DataSet ds = new DataSet();
            if (classLogin.Connectivity == "WIFI")
            {
                KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
                if (classServerDetails.DBVarient == "Oracle") ds = details.GetKPCTTruckTallySheetDetailsOracle(TAG);
            }
            else if (classLogin.Connectivity == "GPRS")
            {
                KPCTSDS.WebReference_GPRS.Service1 details = new KPCTSDS.WebReference_GPRS.Service1();
                if (classServerDetails.DBVarient == "Oracle") ds = details.GetKPCTTruckTallySheetDetailsOracle(TAG);
            }
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("TRUCKID");
                workTable.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    object[] o = { row["1"].ToString(), row["2"].ToString(), row["3"].ToString(), row["4"].ToString(), row["5"].ToString(), row["6"].ToString(), row["7"].ToString(), row["8"].ToString() };
                    workTable.Rows.Add(o);
                }
            }
            dataGridTallySheet.DataSource = workTable;


            ////--Start-- Grid Style---
            //dataGridTallySheet.TableStyles.Clear();
            //DataGridTableStyle tableStyle = new DataGridTableStyle();
            //tableStyle.MappingName = workTable.TableName;
            //foreach (DataColumn item in workTable.Columns)
            //{
            //    DataGridTextBoxColumn tbcName = new DataGridTextBoxColumn();
            //    tbcName.Width = 50;
            //    tbcName.MappingName = item.ColumnName;
            //    tbcName.HeaderText = item.ColumnName;
            //    tableStyle.GridColumnStyles.Add(tbcName);
            //}
            //dataGridTallySheet.TableStyles.Add(tableStyle);
            ////--End-- Grid Style---

            //Display Total Qty and Status---Start---
            int sum = 0;
            for (int i = 1; i <= 8; i++)
            {
                foreach (DataRow dr in workTable.Rows)
                {
                    sum += Convert.ToInt32(dr[i.ToString()]);
                }
            }
            txtOpQty.Text = txtTotalQty1.Text = sum.ToString();
            lblStatus1.Text = lblStatus.Text = workTable.Rows.Count.ToString() + " Rows Inserted.";
        }
        #endregion

        #region textbox validation
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox1.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox2.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox3.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox4.Text = "";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox5.Text = "";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox6.Text = "";
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox7.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox7.Text = "";
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox8.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox8.Text = "";
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox9.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox9.Text = "";
            }
        }
        #endregion

        private void formTallySheet_Load(object sender, EventArgs e)
        {
            DataGridArrangements(); LocationList(); gethandledetailsfromlocaldb(); LoadTallySheetDetails();
        }
        public void LocationList() //Location Master
        {
            string CONNSTRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONNSTRING);
            dbCon.Open();
            string query = "SELECT LocationId,LId FROM [LocationMst] ORDER BY LId";
            SqlCeDataAdapter da = new SqlCeDataAdapter(query, dbCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow drow = dt.NewRow();
            drow["LocationId"] = "Select";
            dt.Rows.InsertAt(drow, 0);
            cmbLocation.DataSource = dt;
            cmbLocation.DisplayMember = "LocationId";
            cmbLocation.ValueMember = "LId";
            dbCon.Close();
        }
        public void gethandledetailsfromlocaldb()
        {
            string myHost = System.Net.Dns.GetHostName();
            string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
            string CONN_STRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);
            string cmd = "select ReaderNo  from ReaderConfig where Status='A'";
            try
            {
                dbCon.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                readerno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                dbCon.Close();
            }
            catch
            {
                MessageBox.Show("Config Details Not found.");
            }
        }
        #region Bind Data to DataGrid
        //DataTable workTable = new DataTable();
        //public void DataGridDetails(string tagno)
        //{
        //    if (classLogin.Connectivity == "WIFI")
        //    {
        //        KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
        //        string WifiConnectionStatus = classConnectivityCheck.WifiSignalStrenthCheck();
        //        if (WifiConnectionStatus == "Success")
        //        {
        //            string ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
        //            if (ConnectionStatus == "Failed")
        //            {
        //                ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
        //            }
        //            if (ConnectionStatus == "Success")
        //            {
        //                DataSet ds = new DataSet();
        //                if (classServerDetails.DBVarient == "Oracle") ds = details.GetKPCTTruckTallySheetDetailsOracle(tagno);
        //                dataGridTallySheet.DataSource = ds.Tables;
        //                workTable = ds.Tables[0];
        //                //--Start-- Grid Style---
        //                dataGridTallySheet.TableStyles.Clear();
        //                DataGridTableStyle tableStyle = new DataGridTableStyle();
        //                tableStyle.MappingName = workTable.TableName;
        //                foreach (DataColumn item in workTable.Columns)
        //                {
        //                    DataGridTextBoxColumn tbcName = new DataGridTextBoxColumn();
        //                    tbcName.Width = 43;
        //                    tbcName.MappingName = item.ColumnName;
        //                    tbcName.HeaderText = item.ColumnName;
        //                    tableStyle.GridColumnStyles.Add(tbcName);
        //                }
        //                dataGridTallySheet.TableStyles.Add(tableStyle);
        //                //--End-- Grid Style---
        //            }
        //            else
        //            {
        //                lblStatus.Text = " Webservice Not Connected..";
        //                classLog.writeLog("Error @: KPCT Operations-TallySheet Wifi:  Webservice Not Connected..");
        //            }
        //        }
        //        else
        //        {
        //            lblStatus.Text = "Wifi Network Not Connected..";
        //            classLog.writeLog("Error @: KPCT Operations-TallySheet: Wifi Network Not Connected..");
        //        }
        //    }
        //    else if (classLogin.Connectivity == "GPRS")
        //    {
        //        string ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
        //        if (ConnectionStatus == "Failed")
        //        {
        //            ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
        //        }
        //        if (ConnectionStatus == "Success")
        //        {

        //        }
        //        else
        //        {
        //            lblStatus.Text = " Webservice Not Connected..";
        //            classLog.writeLog("Error @: KPCT Operations-TallySheet GPRS:  Webservice Not Connected..");
        //        }
        //    }
        //    else
        //    {
        //        lblStatus.Text = "Network Not Connected..";
        //        classLog.writeLog("Error @: KPCT Operations-TallySheet: Network Not Connected..");
        //    }


        //}
        #endregion

        public void btnDisable()
        {
            btnAdd.Enabled = false;
            btnSave.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            cmbLocation.Enabled = false;
            txtOpQty.Enabled = false;
        }
        public void btnEnable()
        {
            btnAdd.Enabled = true;
            btnSave.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            cmbLocation.Enabled = true;
            txtOpQty.Enabled = true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //DataGridDetails(TAG);
            //AddDetailstoDataGrid();
            btnDisable();
            savedetails();
            btnEnable();
        }

        public void savedetails()
        {
            btnDisable();
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "" && textBox8.Text == "" && textBox9.Text == "")
            {
                lblStatus.Text = "Please Enter details.";
                //MessageBox.Show("Please Enter details.");
            }
            else if (textBox1.Text == "0" && textBox2.Text == "0" && textBox3.Text == "0" && textBox4.Text == "0" && textBox5.Text == "0" && textBox6.Text == "0" && textBox7.Text == "0" && textBox8.Text == "0" && textBox9.Text == "0")
            {
                lblStatus.Text = "Please Enter details.";
                //MessageBox.Show("Please Enter details.");
            }
            else
            {
                if (textBox1.Text == "") textBox1.Text = "0";
                if (textBox2.Text == "") textBox2.Text = "0";
                if (textBox3.Text == "") textBox3.Text = "0";
                if (textBox4.Text == "") textBox4.Text = "0";
                if (textBox5.Text == "") textBox5.Text = "0";
                if (textBox6.Text == "") textBox6.Text = "0";
                if (textBox7.Text == "") textBox7.Text = "0";
                if (textBox8.Text == "") textBox8.Text = "0";
                if (textBox9.Text == "") textBox9.Text = "0";
                if (classLogin.Connectivity == "WIFI")
                {
                    KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
                    string WifiConnectionStatus = classConnectivityCheck.WifiSignalStrenthCheck();
                    if (WifiConnectionStatus == "Success")
                    {
                        string ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                        if (ConnectionStatus == "Failed")
                        {
                            ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                        }
                        if (ConnectionStatus == "Success")
                        {
                            string savetime = System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                            string state = details.InsertKPCTTruckTallySheetDetailsOracle(textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString(), textBox6.Text.ToString(), textBox7.Text.ToString(), textBox8.Text.ToString(), textBox9.Text.ToString(), TAG, classLogin.User, savetime);
                            if (state == "SUCCESS")
                            {
                                classLog.writeLog("Error @: KPCT Operations Tally Sheet: Saved successfully in DB.");
                                AddDetailstoDataGrid();
                            }
                            else
                            {
                                classLog.writeLog("Error @: KPCT Operations-TallySheet Insert: SaveFailed.");
                            }
                           }
                        else
                        {
                            lblStatus.Text = " Webservice Not Connected..";
                            classLog.writeLog("Error @: KPCT Operations-TallySheet Wifi:  Webservice Not Connected..");
                        }
                    }
                    else
                    {
                        lblStatus.Text = "Wifi Network Not Connected..";
                        classLog.writeLog("Error @: KPCT Operations-TallySheet: Wifi Network Not Connected..");
                    }
                }
                else if (classLogin.Connectivity == "GPRS")
                {
                    string ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                    if (ConnectionStatus == "Failed")
                    {
                        ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                    }
                    if (ConnectionStatus == "Success")
                    {
                        KPCTSDS.WebReference_GPRS.Service1 details = new KPCTSDS.WebReference_GPRS.Service1();
                        string savetime = System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                        string state = details.InsertKPCTTruckTallySheetDetailsOracle(textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString(), textBox6.Text.ToString(), textBox7.Text.ToString(), textBox8.Text.ToString(), textBox9.Text.ToString(), TAG, classLogin.User, savetime);
                        if (state == "SUCCESS")
                        {
                            classLog.writeLog("Error @: KPCT Operations Tally Sheet: Saved successfully in DB.");
                            AddDetailstoDataGrid();
                        }
                        else
                        {
                            classLog.writeLog("Error @: KPCT Operations-TallySheet Insert: SaveFailed.");
                        }
                    }
                    else
                    {
                        lblStatus.Text = " Webservice Not Connected..";
                        classLog.writeLog("Error @: KPCT Operations-TallySheet GPRS:  Webservice Not Connected..");
                    }
                }
                else
                {
                    lblStatus.Text = "Network Not Connected..";
                    classLog.writeLog("Error @: KPCT Operations-TallySheet: Network Not Connected..");
                }


            }
            btnEnable();
        }

        private void txtOpQty_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtOpQty.Text, "[^0-9]"))
            {
                lblStatus.Text = "Please enter only numbers.";
                //MessageBox.Show("Please enter only numbers.");
                txtOpQty.Text = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbLocation.Text == "Select")
            {
                lblStatus.Text = "Select Location details";
                classLog.writeLog("Warning @: KPCT Operations: Select Location Details"); //btnSave.Enabled = true;
            }
            else if (txtOpQty.Text == "" || txtOpQty.Text == "0")
            {
                lblStatus.Text = "Enter Qty details";
                classLog.writeLog("Warning @: KPCT Operations: Enter Qty Details"); //btnSave.Enabled = true;
            }
            else
            {
                insertKPCTDetailstoWS(TAG);
            }
        }


        public void insertKPCTDetailstoWS(string tagno) //inserting data to Web Service
        {

            if (tagno.ToString() == "")
            {
                MessageBox.Show("Tag details not found.");
                classLog.writeLog("Warning @: KPCT Operations: Tag details not found.");
            }
            else
            {
                string truckno = txtTruckNo.Text.Trim().Replace("'", "").Replace(" ", "").ToUpper().ToString();
                string savetime = System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                string OPLOCid = cmbLocation.SelectedValue.ToString();
                try
                {
                    string state = "";
                    if (classLogin.Connectivity == "WIFI")
                    {
                        KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
                        if (classServerDetails.DBType == "Oracle") state = details.InsertKPCTOperationDetailsOracle(tagno, classLogin.ReaderNo.ToString(), btnSave.Text, savetime, classLogin.User, classLogin.UserType.ToString(), classLogin.Location.ToString(), txtOpQty.Text.ToString(), OPLOCid, "", "", "", "","","");
                    }
                    else if (classLogin.Connectivity == "GPRS")
                    {
                        KPCTSDS.WebReference_GPRS.Service1 details = new KPCTSDS.WebReference_GPRS.Service1();
                        if (classServerDetails.DBType == "Oracle") state = details.InsertKPCTOperationDetailsOracle(tagno, classLogin.ReaderNo.ToString(), btnSave.Text, savetime, classLogin.User, classLogin.UserType.ToString(), classLogin.Location.ToString(), txtOpQty.Text.ToString(), OPLOCid,"","","","","","");
                    }
                    //classLog.writeLog("Info @: KPCT Operations:"+TagRegid+","+ tagno+"," +truckno+","+ trucktype+","+ readerno+","+readerip+","+ User+","+ unregtime);
                    if (state == "SUCCESS")
                    {
                        lblStatus.Text= "Saved..in Online.";
                        MessageBox.Show("Saved..Successfully.");
                        
                        WAP3.formKPCTOps r = new WAP3.formKPCTOps(1);
                        r.btnVerify_Click(TAG);
                        this.Close();
                    }
                    else
                    {
                        classLog.writeLog("Error @: KPCT Operations Tally Sheet: Connection failed.");
                        //insertexitdatatoLocalDB();
                    }
                }
                catch (Exception ex)
                {
                    classLog.writeLog("Error @: KPCT Operations TallySheet: Connection failed.");
                    classLog.writeLog("Exception @: " + ex.ToString());
                    //insertexitdatatoLocalDB();
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
            DataSet ds = new DataSet();
            if (classServerDetails.DBVarient == "Oracle") ds = details.GetKPCTTruckTallySheetDetailsOracle(TAG);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("TRUCKID");
                workTable.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    object[] o = { row["1"].ToString(), row["2"].ToString(), row["3"].ToString(), row["4"].ToString(), row["5"].ToString(), row["6"].ToString(), row["7"].ToString(), row["8"].ToString() };
                    workTable.Rows.Add(o);
                }
            }
            dataGridTallySheet.DataSource = workTable;


            ////--Start-- Grid Style---
            //dataGridTallySheet.TableStyles.Clear();
            //DataGridTableStyle tableStyle = new DataGridTableStyle();
            //tableStyle.MappingName = workTable.TableName;
            //foreach (DataColumn item in workTable.Columns)
            //{
            //    DataGridTextBoxColumn tbcName = new DataGridTextBoxColumn();
            //    tbcName.Width = 50;
            //    tbcName.MappingName = item.ColumnName;
            //    tbcName.HeaderText = item.ColumnName;
            //    tableStyle.GridColumnStyles.Add(tbcName);
            //}
            //dataGridTallySheet.TableStyles.Add(tableStyle);
            ////--End-- Grid Style---

            //Display Total Qty and Status---Start---
            int sum = 0;
            for (int i = 1; i <= 8; i++)
            {
                foreach (DataRow dr in workTable.Rows)
                {
                    sum += Convert.ToInt32(dr[i.ToString()]);
                }
            }
            txtOpQty.Text = txtTotalQty1.Text = sum.ToString();
            lblStatus1.Text = lblStatus.Text = workTable.Rows.Count.ToString() + " Rows Inserted.";
        }



        
    }
}