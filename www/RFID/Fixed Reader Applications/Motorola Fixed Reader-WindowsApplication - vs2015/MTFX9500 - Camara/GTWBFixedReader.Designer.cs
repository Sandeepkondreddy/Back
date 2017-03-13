namespace CS_RFID3_Host_Sample1
{
    partial class GTWBFixedReader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTWBFixedReader));
            this.lblWBOUT = new System.Windows.Forms.Label();
            this.lblWBIN = new System.Windows.Forms.Label();
            this.lblmsg = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tmrPortEntryArea = new System.Windows.Forms.Timer(this.components);
            this.tnrNetworkConnection = new System.Windows.Forms.Timer(this.components);
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAntennaStatus_PortEntryIn_Text = new System.Windows.Forms.Label();
            this.lblReaderPort_PortEntry_Text = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPortEntryAreaReaderDisconnect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAntenna_PortEntryIn_Text = new System.Windows.Forms.Label();
            this.lblAntenna_PortEntryIn = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRFIDTagValue_PortEntryIn_Text = new System.Windows.Forms.Label();
            this.lblRFIDTagValue_PortEntryIn = new System.Windows.Forms.Label();
            this.btnPortEntryAreaReaderConnect = new System.Windows.Forms.Button();
            this.lblPortEntryArea = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.clearReports_CB = new System.Windows.Forms.CheckBox();
            this.lblAntenna_PortEntryOut_Time_Text = new System.Windows.Forms.Label();
            this.lblAntenna_PortEntryIn_Time_Text = new System.Windows.Forms.Label();
            this.lblReaderStatus_PortEntry_Text = new System.Windows.Forms.Label();
            this.lblReaderStatus_PortEntry = new System.Windows.Forms.Label();
            this.lblPortEntryOutAntenna = new System.Windows.Forms.Label();
            this.lblPortEntryInAntenna = new System.Windows.Forms.Label();
            this.lblAntennaStatus_PortEntryOut_Text = new System.Windows.Forms.Label();
            this.lblReaderIP_PortEntry_Text = new System.Windows.Forms.Label();
            this.lblAntennaStatus_PortEntryOut = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAntenna_PortEntryOut_Text = new System.Windows.Forms.Label();
            this.lblAntenna_PortEntryOut = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblRFIDTagValue_PortEntryOut_Text = new System.Windows.Forms.Label();
            this.lblRFIDTagValue_PortEntryOut = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWBOUT
            // 
            this.lblWBOUT.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWBOUT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblWBOUT.Location = new System.Drawing.Point(215, 265);
            this.lblWBOUT.Name = "lblWBOUT";
            this.lblWBOUT.Size = new System.Drawing.Size(116, 26);
            this.lblWBOUT.TabIndex = 281;
            // 
            // lblWBIN
            // 
            this.lblWBIN.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWBIN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblWBIN.Location = new System.Drawing.Point(202, 138);
            this.lblWBIN.Name = "lblWBIN";
            this.lblWBIN.Size = new System.Drawing.Size(129, 26);
            this.lblWBIN.TabIndex = 282;
            // 
            // lblmsg
            // 
            this.lblmsg.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblmsg.Location = new System.Drawing.Point(233, 463);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(171, 18);
            this.lblmsg.TabIndex = 279;
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Black;
            this.lblVersion.Location = new System.Drawing.Point(713, 466);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(39, 18);
            this.lblVersion.TabIndex = 280;
            // 
            // tmrPortEntryArea
            // 
            this.tmrPortEntryArea.Tick += new System.EventHandler(this.tmrPortEntryArea_Tick);
            // 
            // tnrNetworkConnection
            // 
            this.tnrNetworkConnection.Tick += new System.EventHandler(this.tnrNetworkConnection_Tick);
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Maroon;
            this.lblErrorMessage.Location = new System.Drawing.Point(233, 411);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(471, 18);
            this.lblErrorMessage.TabIndex = 278;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(88, 465);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 277;
            this.label1.Text = "Network Status : ";
            // 
            // lblAntennaStatus_PortEntryIn_Text
            // 
            this.lblAntennaStatus_PortEntryIn_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaStatus_PortEntryIn_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaStatus_PortEntryIn_Text.Location = new System.Drawing.Point(233, 227);
            this.lblAntennaStatus_PortEntryIn_Text.Name = "lblAntennaStatus_PortEntryIn_Text";
            this.lblAntennaStatus_PortEntryIn_Text.Size = new System.Drawing.Size(471, 36);
            this.lblAntennaStatus_PortEntryIn_Text.TabIndex = 276;
            this.lblAntennaStatus_PortEntryIn_Text.Text = "-";
            // 
            // lblReaderPort_PortEntry_Text
            // 
            this.lblReaderPort_PortEntry_Text.AutoSize = true;
            this.lblReaderPort_PortEntry_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderPort_PortEntry_Text.Location = new System.Drawing.Point(195, 98);
            this.lblReaderPort_PortEntry_Text.Name = "lblReaderPort_PortEntry_Text";
            this.lblReaderPort_PortEntry_Text.Size = new System.Drawing.Size(85, 16);
            this.lblReaderPort_PortEntry_Text.TabIndex = 275;
            this.lblReaderPort_PortEntry_Text.Text = "Reader Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(88, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 16);
            this.label4.TabIndex = 259;
            this.label4.Text = "Antenna Status :";
            // 
            // btnPortEntryAreaReaderDisconnect
            // 
            this.btnPortEntryAreaReaderDisconnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnPortEntryAreaReaderDisconnect.Enabled = false;
            this.btnPortEntryAreaReaderDisconnect.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPortEntryAreaReaderDisconnect.ForeColor = System.Drawing.Color.Black;
            this.btnPortEntryAreaReaderDisconnect.Location = new System.Drawing.Point(573, 69);
            this.btnPortEntryAreaReaderDisconnect.Name = "btnPortEntryAreaReaderDisconnect";
            this.btnPortEntryAreaReaderDisconnect.Size = new System.Drawing.Size(131, 30);
            this.btnPortEntryAreaReaderDisconnect.TabIndex = 258;
            this.btnPortEntryAreaReaderDisconnect.Text = "Disconnect";
            this.btnPortEntryAreaReaderDisconnect.UseVisualStyleBackColor = false;
            this.btnPortEntryAreaReaderDisconnect.Click += new System.EventHandler(this.btnPortEntryAreaReaderDisconnect_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(195, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 16);
            this.label5.TabIndex = 257;
            this.label5.Text = ":";
            // 
            // lblAntenna_PortEntryIn_Text
            // 
            this.lblAntenna_PortEntryIn_Text.AutoSize = true;
            this.lblAntenna_PortEntryIn_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_PortEntryIn_Text.Location = new System.Drawing.Point(215, 166);
            this.lblAntenna_PortEntryIn_Text.Name = "lblAntenna_PortEntryIn_Text";
            this.lblAntenna_PortEntryIn_Text.Size = new System.Drawing.Size(0, 16);
            this.lblAntenna_PortEntryIn_Text.TabIndex = 256;
            // 
            // lblAntenna_PortEntryIn
            // 
            this.lblAntenna_PortEntryIn.AutoSize = true;
            this.lblAntenna_PortEntryIn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_PortEntryIn.Location = new System.Drawing.Point(99, 166);
            this.lblAntenna_PortEntryIn.Name = "lblAntenna_PortEntryIn";
            this.lblAntenna_PortEntryIn.Size = new System.Drawing.Size(63, 16);
            this.lblAntenna_PortEntryIn.TabIndex = 255;
            this.lblAntenna_PortEntryIn.Text = "Antenna";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(195, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 16);
            this.label6.TabIndex = 254;
            this.label6.Text = ":";
            // 
            // lblRFIDTagValue_PortEntryIn_Text
            // 
            this.lblRFIDTagValue_PortEntryIn_Text.AutoSize = true;
            this.lblRFIDTagValue_PortEntryIn_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFIDTagValue_PortEntryIn_Text.Location = new System.Drawing.Point(215, 193);
            this.lblRFIDTagValue_PortEntryIn_Text.Name = "lblRFIDTagValue_PortEntryIn_Text";
            this.lblRFIDTagValue_PortEntryIn_Text.Size = new System.Drawing.Size(0, 16);
            this.lblRFIDTagValue_PortEntryIn_Text.TabIndex = 253;
            // 
            // lblRFIDTagValue_PortEntryIn
            // 
            this.lblRFIDTagValue_PortEntryIn.AutoSize = true;
            this.lblRFIDTagValue_PortEntryIn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFIDTagValue_PortEntryIn.Location = new System.Drawing.Point(99, 193);
            this.lblRFIDTagValue_PortEntryIn.Name = "lblRFIDTagValue_PortEntryIn";
            this.lblRFIDTagValue_PortEntryIn.Size = new System.Drawing.Size(91, 16);
            this.lblRFIDTagValue_PortEntryIn.TabIndex = 252;
            this.lblRFIDTagValue_PortEntryIn.Text = "Readed Tag ";
            // 
            // btnPortEntryAreaReaderConnect
            // 
            this.btnPortEntryAreaReaderConnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnPortEntryAreaReaderConnect.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPortEntryAreaReaderConnect.ForeColor = System.Drawing.Color.Black;
            this.btnPortEntryAreaReaderConnect.Location = new System.Drawing.Point(409, 69);
            this.btnPortEntryAreaReaderConnect.Name = "btnPortEntryAreaReaderConnect";
            this.btnPortEntryAreaReaderConnect.Size = new System.Drawing.Size(138, 30);
            this.btnPortEntryAreaReaderConnect.TabIndex = 246;
            this.btnPortEntryAreaReaderConnect.Text = "Connect";
            this.btnPortEntryAreaReaderConnect.UseVisualStyleBackColor = false;
            this.btnPortEntryAreaReaderConnect.Click += new System.EventHandler(this.btnPortEntryAreaReaderConnect_Click);
            // 
            // lblPortEntryArea
            // 
            this.lblPortEntryArea.AutoSize = true;
            this.lblPortEntryArea.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortEntryArea.Location = new System.Drawing.Point(37, 26);
            this.lblPortEntryArea.Name = "lblPortEntryArea";
            this.lblPortEntryArea.Size = new System.Drawing.Size(226, 16);
            this.lblPortEntryArea.TabIndex = 251;
            this.lblPortEntryArea.Text = "WB Area RFID Reader Details :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(56, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 16);
            this.label9.TabIndex = 249;
            this.label9.Text = "Reader Port";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(150, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 16);
            this.label10.TabIndex = 250;
            this.label10.Text = ":";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(56, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 16);
            this.label12.TabIndex = 247;
            this.label12.Text = "Reader IP";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(150, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 16);
            this.label13.TabIndex = 248;
            this.label13.Text = ":";
            // 
            // clearReports_CB
            // 
            this.clearReports_CB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearReports_CB.AutoSize = true;
            this.clearReports_CB.Checked = true;
            this.clearReports_CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clearReports_CB.Location = new System.Drawing.Point(1022, 21);
            this.clearReports_CB.Name = "clearReports_CB";
            this.clearReports_CB.Size = new System.Drawing.Size(50, 17);
            this.clearReports_CB.TabIndex = 245;
            this.clearReports_CB.Text = "Clear";
            this.clearReports_CB.UseVisualStyleBackColor = true;
            // 
            // lblAntenna_PortEntryOut_Time_Text
            // 
            this.lblAntenna_PortEntryOut_Time_Text.AutoSize = true;
            this.lblAntenna_PortEntryOut_Time_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_PortEntryOut_Time_Text.Location = new System.Drawing.Point(316, 305);
            this.lblAntenna_PortEntryOut_Time_Text.Name = "lblAntenna_PortEntryOut_Time_Text";
            this.lblAntenna_PortEntryOut_Time_Text.Size = new System.Drawing.Size(15, 16);
            this.lblAntenna_PortEntryOut_Time_Text.TabIndex = 273;
            this.lblAntenna_PortEntryOut_Time_Text.Text = "-";
            // 
            // lblAntenna_PortEntryIn_Time_Text
            // 
            this.lblAntenna_PortEntryIn_Time_Text.AutoSize = true;
            this.lblAntenna_PortEntryIn_Time_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_PortEntryIn_Time_Text.Location = new System.Drawing.Point(317, 166);
            this.lblAntenna_PortEntryIn_Time_Text.Name = "lblAntenna_PortEntryIn_Time_Text";
            this.lblAntenna_PortEntryIn_Time_Text.Size = new System.Drawing.Size(15, 16);
            this.lblAntenna_PortEntryIn_Time_Text.TabIndex = 272;
            this.lblAntenna_PortEntryIn_Time_Text.Text = "-";
            // 
            // lblReaderStatus_PortEntry_Text
            // 
            this.lblReaderStatus_PortEntry_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus_PortEntry_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblReaderStatus_PortEntry_Text.Location = new System.Drawing.Point(533, 26);
            this.lblReaderStatus_PortEntry_Text.Name = "lblReaderStatus_PortEntry_Text";
            this.lblReaderStatus_PortEntry_Text.Size = new System.Drawing.Size(171, 18);
            this.lblReaderStatus_PortEntry_Text.TabIndex = 271;
            this.lblReaderStatus_PortEntry_Text.Text = "# Not Connected";
            // 
            // lblReaderStatus_PortEntry
            // 
            this.lblReaderStatus_PortEntry.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus_PortEntry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblReaderStatus_PortEntry.Location = new System.Drawing.Point(406, 26);
            this.lblReaderStatus_PortEntry.Name = "lblReaderStatus_PortEntry";
            this.lblReaderStatus_PortEntry.Size = new System.Drawing.Size(121, 16);
            this.lblReaderStatus_PortEntry.TabIndex = 270;
            this.lblReaderStatus_PortEntry.Text = "Reader Status :";
            // 
            // lblPortEntryOutAntenna
            // 
            this.lblPortEntryOutAntenna.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortEntryOutAntenna.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblPortEntryOutAntenna.Location = new System.Drawing.Point(56, 263);
            this.lblPortEntryOutAntenna.Name = "lblPortEntryOutAntenna";
            this.lblPortEntryOutAntenna.Size = new System.Drawing.Size(191, 16);
            this.lblPortEntryOutAntenna.TabIndex = 269;
            this.lblPortEntryOutAntenna.Text = "# WB Out - Antenna";
            // 
            // lblPortEntryInAntenna
            // 
            this.lblPortEntryInAntenna.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortEntryInAntenna.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblPortEntryInAntenna.Location = new System.Drawing.Point(56, 137);
            this.lblPortEntryInAntenna.Name = "lblPortEntryInAntenna";
            this.lblPortEntryInAntenna.Size = new System.Drawing.Size(191, 16);
            this.lblPortEntryInAntenna.TabIndex = 268;
            this.lblPortEntryInAntenna.Text = "# WB In - Antenna";
            // 
            // lblAntennaStatus_PortEntryOut_Text
            // 
            this.lblAntennaStatus_PortEntryOut_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaStatus_PortEntryOut_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaStatus_PortEntryOut_Text.Location = new System.Drawing.Point(233, 362);
            this.lblAntennaStatus_PortEntryOut_Text.Name = "lblAntennaStatus_PortEntryOut_Text";
            this.lblAntennaStatus_PortEntryOut_Text.Size = new System.Drawing.Size(499, 36);
            this.lblAntennaStatus_PortEntryOut_Text.TabIndex = 267;
            this.lblAntennaStatus_PortEntryOut_Text.Text = "-";
            // 
            // lblReaderIP_PortEntry_Text
            // 
            this.lblReaderIP_PortEntry_Text.AutoSize = true;
            this.lblReaderIP_PortEntry_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderIP_PortEntry_Text.Location = new System.Drawing.Point(195, 69);
            this.lblReaderIP_PortEntry_Text.Name = "lblReaderIP_PortEntry_Text";
            this.lblReaderIP_PortEntry_Text.Size = new System.Drawing.Size(71, 16);
            this.lblReaderIP_PortEntry_Text.TabIndex = 274;
            this.lblReaderIP_PortEntry_Text.Text = "Reader IP";
            // 
            // lblAntennaStatus_PortEntryOut
            // 
            this.lblAntennaStatus_PortEntryOut.AutoSize = true;
            this.lblAntennaStatus_PortEntryOut.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaStatus_PortEntryOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaStatus_PortEntryOut.Location = new System.Drawing.Point(88, 362);
            this.lblAntennaStatus_PortEntryOut.Name = "lblAntennaStatus_PortEntryOut";
            this.lblAntennaStatus_PortEntryOut.Size = new System.Drawing.Size(121, 16);
            this.lblAntennaStatus_PortEntryOut.TabIndex = 266;
            this.lblAntennaStatus_PortEntryOut.Text = "Antenna Status :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(195, 296);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 16);
            this.label11.TabIndex = 265;
            this.label11.Text = ":";
            // 
            // lblAntenna_PortEntryOut_Text
            // 
            this.lblAntenna_PortEntryOut_Text.AutoSize = true;
            this.lblAntenna_PortEntryOut_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_PortEntryOut_Text.Location = new System.Drawing.Point(215, 298);
            this.lblAntenna_PortEntryOut_Text.Name = "lblAntenna_PortEntryOut_Text";
            this.lblAntenna_PortEntryOut_Text.Size = new System.Drawing.Size(0, 16);
            this.lblAntenna_PortEntryOut_Text.TabIndex = 264;
            // 
            // lblAntenna_PortEntryOut
            // 
            this.lblAntenna_PortEntryOut.AutoSize = true;
            this.lblAntenna_PortEntryOut.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_PortEntryOut.Location = new System.Drawing.Point(101, 296);
            this.lblAntenna_PortEntryOut.Name = "lblAntenna_PortEntryOut";
            this.lblAntenna_PortEntryOut.Size = new System.Drawing.Size(63, 16);
            this.lblAntenna_PortEntryOut.TabIndex = 263;
            this.lblAntenna_PortEntryOut.Text = "Antenna";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(195, 326);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 16);
            this.label14.TabIndex = 262;
            this.label14.Text = ":";
            // 
            // lblRFIDTagValue_PortEntryOut_Text
            // 
            this.lblRFIDTagValue_PortEntryOut_Text.AutoSize = true;
            this.lblRFIDTagValue_PortEntryOut_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFIDTagValue_PortEntryOut_Text.Location = new System.Drawing.Point(215, 335);
            this.lblRFIDTagValue_PortEntryOut_Text.Name = "lblRFIDTagValue_PortEntryOut_Text";
            this.lblRFIDTagValue_PortEntryOut_Text.Size = new System.Drawing.Size(0, 16);
            this.lblRFIDTagValue_PortEntryOut_Text.TabIndex = 261;
            // 
            // lblRFIDTagValue_PortEntryOut
            // 
            this.lblRFIDTagValue_PortEntryOut.AutoSize = true;
            this.lblRFIDTagValue_PortEntryOut.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFIDTagValue_PortEntryOut.Location = new System.Drawing.Point(101, 326);
            this.lblRFIDTagValue_PortEntryOut.Name = "lblRFIDTagValue_PortEntryOut";
            this.lblRFIDTagValue_PortEntryOut.Size = new System.Drawing.Size(91, 16);
            this.lblRFIDTagValue_PortEntryOut.TabIndex = 260;
            this.lblRFIDTagValue_PortEntryOut.Text = "Readed Tag ";
            // 
            // GTWBFixedReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 502);
            this.Controls.Add(this.lblWBOUT);
            this.Controls.Add(this.lblWBIN);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAntennaStatus_PortEntryIn_Text);
            this.Controls.Add(this.lblReaderPort_PortEntry_Text);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnPortEntryAreaReaderDisconnect);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblAntenna_PortEntryIn_Text);
            this.Controls.Add(this.lblAntenna_PortEntryIn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblRFIDTagValue_PortEntryIn_Text);
            this.Controls.Add(this.lblRFIDTagValue_PortEntryIn);
            this.Controls.Add(this.btnPortEntryAreaReaderConnect);
            this.Controls.Add(this.lblPortEntryArea);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.clearReports_CB);
            this.Controls.Add(this.lblAntenna_PortEntryOut_Time_Text);
            this.Controls.Add(this.lblAntenna_PortEntryIn_Time_Text);
            this.Controls.Add(this.lblReaderStatus_PortEntry_Text);
            this.Controls.Add(this.lblReaderStatus_PortEntry);
            this.Controls.Add(this.lblPortEntryOutAntenna);
            this.Controls.Add(this.lblPortEntryInAntenna);
            this.Controls.Add(this.lblAntennaStatus_PortEntryOut_Text);
            this.Controls.Add(this.lblReaderIP_PortEntry_Text);
            this.Controls.Add(this.lblAntennaStatus_PortEntryOut);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblAntenna_PortEntryOut_Text);
            this.Controls.Add(this.lblAntenna_PortEntryOut);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblRFIDTagValue_PortEntryOut_Text);
            this.Controls.Add(this.lblRFIDTagValue_PortEntryOut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GTWBFixedReader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GTWBFixedReader";
            this.Load += new System.EventHandler(this.GTWBFixedReader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWBOUT;
        private System.Windows.Forms.Label lblWBIN;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Timer tmrPortEntryArea;
        private System.Windows.Forms.Timer tnrNetworkConnection;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAntennaStatus_PortEntryIn_Text;
        private System.Windows.Forms.Label lblReaderPort_PortEntry_Text;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPortEntryAreaReaderDisconnect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAntenna_PortEntryIn_Text;
        private System.Windows.Forms.Label lblAntenna_PortEntryIn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRFIDTagValue_PortEntryIn_Text;
        private System.Windows.Forms.Label lblRFIDTagValue_PortEntryIn;
        private System.Windows.Forms.Button btnPortEntryAreaReaderConnect;
        private System.Windows.Forms.Label lblPortEntryArea;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox clearReports_CB;
        private System.Windows.Forms.Label lblAntenna_PortEntryOut_Time_Text;
        private System.Windows.Forms.Label lblAntenna_PortEntryIn_Time_Text;
        private System.Windows.Forms.Label lblReaderStatus_PortEntry_Text;
        private System.Windows.Forms.Label lblReaderStatus_PortEntry;
        private System.Windows.Forms.Label lblPortEntryOutAntenna;
        private System.Windows.Forms.Label lblPortEntryInAntenna;
        private System.Windows.Forms.Label lblAntennaStatus_PortEntryOut_Text;
        private System.Windows.Forms.Label lblReaderIP_PortEntry_Text;
        private System.Windows.Forms.Label lblAntennaStatus_PortEntryOut;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAntenna_PortEntryOut_Text;
        private System.Windows.Forms.Label lblAntenna_PortEntryOut;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblRFIDTagValue_PortEntryOut_Text;
        private System.Windows.Forms.Label lblRFIDTagValue_PortEntryOut;
    }
}