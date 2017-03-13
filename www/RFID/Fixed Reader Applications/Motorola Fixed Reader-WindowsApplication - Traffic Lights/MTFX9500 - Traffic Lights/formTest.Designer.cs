namespace CS_RFID3_Host_Sample1
{
    partial class formTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formTest));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.lblAntennaSet2TimeValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet1TimeValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblWBTwoValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblWBOneValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblNetworkStatusValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblNetworkStatus = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblErrorDetails = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblErrorValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet2StatusValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet2Status = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet2TagValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.AntennaSet2Tag = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet2Value = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet2Details = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet1StatusValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet1Status = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.AntennaSet1TagValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet1Tag = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet1Value = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAntennaSet1Details = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnDisconnect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnConnect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblReaderStatusValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblReaderStatus = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtReaderPort = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblReaderPort = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtReaderIP = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblReaderIP = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblReaderDetails = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.timerReader = new System.Windows.Forms.Timer(this.components);
            this.timerNetworkConnection = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.lblAntennaSet2TimeValue);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet1TimeValue);
            this.kryptonPanel.Controls.Add(this.lblWBTwoValue);
            this.kryptonPanel.Controls.Add(this.lblWBOneValue);
            this.kryptonPanel.Controls.Add(this.lblNetworkStatusValue);
            this.kryptonPanel.Controls.Add(this.lblNetworkStatus);
            this.kryptonPanel.Controls.Add(this.lblErrorDetails);
            this.kryptonPanel.Controls.Add(this.lblErrorValue);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet2StatusValue);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet2Status);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet2TagValue);
            this.kryptonPanel.Controls.Add(this.AntennaSet2Tag);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet2Value);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet2);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet2Details);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet1StatusValue);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet1Status);
            this.kryptonPanel.Controls.Add(this.AntennaSet1TagValue);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet1Tag);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet1Value);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet1);
            this.kryptonPanel.Controls.Add(this.lblAntennaSet1Details);
            this.kryptonPanel.Controls.Add(this.btnDisconnect);
            this.kryptonPanel.Controls.Add(this.btnConnect);
            this.kryptonPanel.Controls.Add(this.lblReaderStatusValue);
            this.kryptonPanel.Controls.Add(this.lblReaderStatus);
            this.kryptonPanel.Controls.Add(this.txtReaderPort);
            this.kryptonPanel.Controls.Add(this.lblReaderPort);
            this.kryptonPanel.Controls.Add(this.txtReaderIP);
            this.kryptonPanel.Controls.Add(this.lblReaderIP);
            this.kryptonPanel.Controls.Add(this.lblReaderDetails);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(764, 502);
            this.kryptonPanel.TabIndex = 0;
            // 
            // lblAntennaSet2TimeValue
            // 
            this.lblAntennaSet2TimeValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblAntennaSet2TimeValue.Location = new System.Drawing.Point(385, 305);
            this.lblAntennaSet2TimeValue.Name = "lblAntennaSet2TimeValue";
            this.lblAntennaSet2TimeValue.Size = new System.Drawing.Size(15, 20);
            this.lblAntennaSet2TimeValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2TimeValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2TimeValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblAntennaSet2TimeValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaSet2TimeValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2TimeValue.TabIndex = 30;
            this.lblAntennaSet2TimeValue.Values.Text = "-";
            // 
            // lblAntennaSet1TimeValue
            // 
            this.lblAntennaSet1TimeValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblAntennaSet1TimeValue.Location = new System.Drawing.Point(385, 172);
            this.lblAntennaSet1TimeValue.Name = "lblAntennaSet1TimeValue";
            this.lblAntennaSet1TimeValue.Size = new System.Drawing.Size(15, 20);
            this.lblAntennaSet1TimeValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1TimeValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1TimeValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblAntennaSet1TimeValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaSet1TimeValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1TimeValue.TabIndex = 29;
            this.lblAntennaSet1TimeValue.Values.Text = "-";
            // 
            // lblWBTwoValue
            // 
            this.lblWBTwoValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldPanel;
            this.lblWBTwoValue.Location = new System.Drawing.Point(250, 274);
            this.lblWBTwoValue.Name = "lblWBTwoValue";
            this.lblWBTwoValue.Size = new System.Drawing.Size(15, 20);
            this.lblWBTwoValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWBTwoValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWBTwoValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblWBTwoValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblWBTwoValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWBTwoValue.TabIndex = 28;
            this.lblWBTwoValue.Values.Text = "-";
            // 
            // lblWBOneValue
            // 
            this.lblWBOneValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldPanel;
            this.lblWBOneValue.Location = new System.Drawing.Point(250, 141);
            this.lblWBOneValue.Name = "lblWBOneValue";
            this.lblWBOneValue.Size = new System.Drawing.Size(15, 20);
            this.lblWBOneValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWBOneValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWBOneValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblWBOneValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblWBOneValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWBOneValue.TabIndex = 27;
            this.lblWBOneValue.Values.Text = "-";
            // 
            // lblNetworkStatusValue
            // 
            this.lblNetworkStatusValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblNetworkStatusValue.Location = new System.Drawing.Point(171, 470);
            this.lblNetworkStatusValue.Name = "lblNetworkStatusValue";
            this.lblNetworkStatusValue.Size = new System.Drawing.Size(15, 20);
            this.lblNetworkStatusValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetworkStatusValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetworkStatusValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblNetworkStatusValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNetworkStatusValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetworkStatusValue.TabIndex = 26;
            this.lblNetworkStatusValue.Values.Text = "-";
            // 
            // lblNetworkStatus
            // 
            this.lblNetworkStatus.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldPanel;
            this.lblNetworkStatus.Location = new System.Drawing.Point(50, 470);
            this.lblNetworkStatus.Name = "lblNetworkStatus";
            this.lblNetworkStatus.Size = new System.Drawing.Size(107, 20);
            this.lblNetworkStatus.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetworkStatus.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetworkStatus.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetworkStatus.TabIndex = 25;
            this.lblNetworkStatus.Values.Text = "Network Status : ";
            // 
            // lblErrorDetails
            // 
            this.lblErrorDetails.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblErrorDetails.Location = new System.Drawing.Point(50, 395);
            this.lblErrorDetails.Name = "lblErrorDetails";
            this.lblErrorDetails.Size = new System.Drawing.Size(89, 20);
            this.lblErrorDetails.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorDetails.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorDetails.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorDetails.TabIndex = 24;
            this.lblErrorDetails.Values.Text = "Error Details :";
            // 
            // lblErrorValue
            // 
            this.lblErrorValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblErrorValue.Location = new System.Drawing.Point(171, 395);
            this.lblErrorValue.Name = "lblErrorValue";
            this.lblErrorValue.Size = new System.Drawing.Size(15, 20);
            this.lblErrorValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblErrorValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblErrorValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorValue.TabIndex = 23;
            this.lblErrorValue.Values.Text = "-";
            // 
            // lblAntennaSet2StatusValue
            // 
            this.lblAntennaSet2StatusValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblAntennaSet2StatusValue.Location = new System.Drawing.Point(195, 358);
            this.lblAntennaSet2StatusValue.Name = "lblAntennaSet2StatusValue";
            this.lblAntennaSet2StatusValue.Size = new System.Drawing.Size(15, 20);
            this.lblAntennaSet2StatusValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2StatusValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2StatusValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblAntennaSet2StatusValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaSet2StatusValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2StatusValue.TabIndex = 22;
            this.lblAntennaSet2StatusValue.Values.Text = "-";
            // 
            // lblAntennaSet2Status
            // 
            this.lblAntennaSet2Status.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblAntennaSet2Status.Location = new System.Drawing.Point(80, 358);
            this.lblAntennaSet2Status.Name = "lblAntennaSet2Status";
            this.lblAntennaSet2Status.Size = new System.Drawing.Size(106, 20);
            this.lblAntennaSet2Status.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2Status.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2Status.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2Status.TabIndex = 21;
            this.lblAntennaSet2Status.Values.Text = "Antenna Status :";
            // 
            // lblAntennaSet2TagValue
            // 
            this.lblAntennaSet2TagValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblAntennaSet2TagValue.Location = new System.Drawing.Point(195, 332);
            this.lblAntennaSet2TagValue.Name = "lblAntennaSet2TagValue";
            this.lblAntennaSet2TagValue.Size = new System.Drawing.Size(15, 20);
            this.lblAntennaSet2TagValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2TagValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2TagValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblAntennaSet2TagValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaSet2TagValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2TagValue.TabIndex = 20;
            this.lblAntennaSet2TagValue.Values.Text = "-";
            // 
            // AntennaSet2Tag
            // 
            this.AntennaSet2Tag.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.AntennaSet2Tag.Location = new System.Drawing.Point(80, 332);
            this.AntennaSet2Tag.Name = "AntennaSet2Tag";
            this.AntennaSet2Tag.Size = new System.Drawing.Size(39, 20);
            this.AntennaSet2Tag.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AntennaSet2Tag.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AntennaSet2Tag.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AntennaSet2Tag.TabIndex = 19;
            this.AntennaSet2Tag.Values.Text = "Tag :";
            // 
            // lblAntennaSet2Value
            // 
            this.lblAntennaSet2Value.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblAntennaSet2Value.Location = new System.Drawing.Point(195, 305);
            this.lblAntennaSet2Value.Name = "lblAntennaSet2Value";
            this.lblAntennaSet2Value.Size = new System.Drawing.Size(15, 20);
            this.lblAntennaSet2Value.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2Value.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2Value.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblAntennaSet2Value.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaSet2Value.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2Value.TabIndex = 18;
            this.lblAntennaSet2Value.Values.Text = "-";
            // 
            // lblAntennaSet2
            // 
            this.lblAntennaSet2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblAntennaSet2.Location = new System.Drawing.Point(80, 305);
            this.lblAntennaSet2.Name = "lblAntennaSet2";
            this.lblAntennaSet2.Size = new System.Drawing.Size(67, 20);
            this.lblAntennaSet2.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2.TabIndex = 17;
            this.lblAntennaSet2.Values.Text = "Antenna :";
            // 
            // lblAntennaSet2Details
            // 
            this.lblAntennaSet2Details.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblAntennaSet2Details.Location = new System.Drawing.Point(50, 274);
            this.lblAntennaSet2Details.Name = "lblAntennaSet2Details";
            this.lblAntennaSet2Details.Size = new System.Drawing.Size(171, 20);
            this.lblAntennaSet2Details.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2Details.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2Details.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet2Details.TabIndex = 16;
            this.lblAntennaSet2Details.Values.Text = "# WB Out - Antenna Details";
            // 
            // lblAntennaSet1StatusValue
            // 
            this.lblAntennaSet1StatusValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblAntennaSet1StatusValue.Location = new System.Drawing.Point(195, 225);
            this.lblAntennaSet1StatusValue.Name = "lblAntennaSet1StatusValue";
            this.lblAntennaSet1StatusValue.Size = new System.Drawing.Size(15, 20);
            this.lblAntennaSet1StatusValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1StatusValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1StatusValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblAntennaSet1StatusValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaSet1StatusValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1StatusValue.TabIndex = 15;
            this.lblAntennaSet1StatusValue.Values.Text = "-";
            // 
            // lblAntennaSet1Status
            // 
            this.lblAntennaSet1Status.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblAntennaSet1Status.Location = new System.Drawing.Point(80, 225);
            this.lblAntennaSet1Status.Name = "lblAntennaSet1Status";
            this.lblAntennaSet1Status.Size = new System.Drawing.Size(106, 20);
            this.lblAntennaSet1Status.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Status.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Status.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Status.TabIndex = 14;
            this.lblAntennaSet1Status.Values.Text = "Antenna Status :";
            // 
            // AntennaSet1TagValue
            // 
            this.AntennaSet1TagValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.AntennaSet1TagValue.Location = new System.Drawing.Point(195, 199);
            this.AntennaSet1TagValue.Name = "AntennaSet1TagValue";
            this.AntennaSet1TagValue.Size = new System.Drawing.Size(15, 20);
            this.AntennaSet1TagValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AntennaSet1TagValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AntennaSet1TagValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.AntennaSet1TagValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AntennaSet1TagValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AntennaSet1TagValue.TabIndex = 13;
            this.AntennaSet1TagValue.Values.Text = "-";
            // 
            // lblAntennaSet1Tag
            // 
            this.lblAntennaSet1Tag.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblAntennaSet1Tag.Location = new System.Drawing.Point(80, 199);
            this.lblAntennaSet1Tag.Name = "lblAntennaSet1Tag";
            this.lblAntennaSet1Tag.Size = new System.Drawing.Size(39, 20);
            this.lblAntennaSet1Tag.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Tag.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Tag.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Tag.TabIndex = 12;
            this.lblAntennaSet1Tag.Values.Text = "Tag :";
            // 
            // lblAntennaSet1Value
            // 
            this.lblAntennaSet1Value.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblAntennaSet1Value.Location = new System.Drawing.Point(195, 172);
            this.lblAntennaSet1Value.Name = "lblAntennaSet1Value";
            this.lblAntennaSet1Value.Size = new System.Drawing.Size(15, 20);
            this.lblAntennaSet1Value.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Value.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Value.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblAntennaSet1Value.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaSet1Value.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Value.TabIndex = 11;
            this.lblAntennaSet1Value.Values.Text = "-";
            // 
            // lblAntennaSet1
            // 
            this.lblAntennaSet1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblAntennaSet1.Location = new System.Drawing.Point(80, 172);
            this.lblAntennaSet1.Name = "lblAntennaSet1";
            this.lblAntennaSet1.Size = new System.Drawing.Size(67, 20);
            this.lblAntennaSet1.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1.TabIndex = 10;
            this.lblAntennaSet1.Values.Text = "Antenna :";
            // 
            // lblAntennaSet1Details
            // 
            this.lblAntennaSet1Details.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblAntennaSet1Details.Location = new System.Drawing.Point(50, 141);
            this.lblAntennaSet1Details.Name = "lblAntennaSet1Details";
            this.lblAntennaSet1Details.Size = new System.Drawing.Size(161, 20);
            this.lblAntennaSet1Details.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Details.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Details.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaSet1Details.TabIndex = 9;
            this.lblAntennaSet1Details.Values.Text = "# WB In - Antenna Details";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(581, 78);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(90, 25);
            this.btnDisconnect.TabIndex = 8;
            this.btnDisconnect.Values.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(443, 78);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(90, 25);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Values.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblReaderStatusValue
            // 
            this.lblReaderStatusValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblReaderStatusValue.Location = new System.Drawing.Point(559, 21);
            this.lblReaderStatusValue.Name = "lblReaderStatusValue";
            this.lblReaderStatusValue.Size = new System.Drawing.Size(94, 20);
            this.lblReaderStatusValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatusValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatusValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblReaderStatusValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblReaderStatusValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatusValue.TabIndex = 6;
            this.lblReaderStatusValue.Values.Text = "# Reader Status";
            // 
            // lblReaderStatus
            // 
            this.lblReaderStatus.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblReaderStatus.Location = new System.Drawing.Point(443, 21);
            this.lblReaderStatus.Name = "lblReaderStatus";
            this.lblReaderStatus.Size = new System.Drawing.Size(97, 20);
            this.lblReaderStatus.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus.TabIndex = 5;
            this.lblReaderStatus.Values.Text = "Reader Status :";
            // 
            // txtReaderPort
            // 
            this.txtReaderPort.Location = new System.Drawing.Point(165, 83);
            this.txtReaderPort.Name = "txtReaderPort";
            this.txtReaderPort.ReadOnly = true;
            this.txtReaderPort.Size = new System.Drawing.Size(130, 20);
            this.txtReaderPort.TabIndex = 4;
            this.txtReaderPort.Text = "Reader Port";
            // 
            // lblReaderPort
            // 
            this.lblReaderPort.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblReaderPort.Location = new System.Drawing.Point(50, 83);
            this.lblReaderPort.Name = "lblReaderPort";
            this.lblReaderPort.Size = new System.Drawing.Size(86, 20);
            this.lblReaderPort.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderPort.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderPort.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderPort.TabIndex = 3;
            this.lblReaderPort.Values.Text = "Reader Port :";
            // 
            // txtReaderIP
            // 
            this.txtReaderIP.Location = new System.Drawing.Point(165, 57);
            this.txtReaderIP.Name = "txtReaderIP";
            this.txtReaderIP.ReadOnly = true;
            this.txtReaderIP.Size = new System.Drawing.Size(130, 20);
            this.txtReaderIP.TabIndex = 2;
            this.txtReaderIP.Text = "ReaderIP";
            // 
            // lblReaderIP
            // 
            this.lblReaderIP.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblReaderIP.Location = new System.Drawing.Point(50, 57);
            this.lblReaderIP.Name = "lblReaderIP";
            this.lblReaderIP.Size = new System.Drawing.Size(73, 20);
            this.lblReaderIP.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderIP.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderIP.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderIP.TabIndex = 1;
            this.lblReaderIP.Values.Text = "Reader IP :";
            // 
            // lblReaderDetails
            // 
            this.lblReaderDetails.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldPanel;
            this.lblReaderDetails.Location = new System.Drawing.Point(50, 21);
            this.lblReaderDetails.Name = "lblReaderDetails";
            this.lblReaderDetails.Size = new System.Drawing.Size(94, 20);
            this.lblReaderDetails.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderDetails.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderDetails.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderDetails.TabIndex = 0;
            this.lblReaderDetails.Values.Text = "Reader Details";
            // 
            // timerReader
            // 
            this.timerReader.Tick += new System.EventHandler(this.timerReader_Tick);
            // 
            // timerNetworkConnection
            // 
            this.timerNetworkConnection.Tick += new System.EventHandler(this.timerNetworkConnection_Tick);
            // 
            // formTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 502);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test";
            this.Load += new System.EventHandler(this.formTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReaderDetails;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReaderIP;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtReaderIP;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtReaderPort;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReaderPort;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReaderStatus;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReaderStatusValue;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnConnect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDisconnect;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet1Details;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet1Value;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel AntennaSet1TagValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet1Tag;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet1StatusValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet1Status;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet2StatusValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet2Status;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet2TagValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel AntennaSet2Tag;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet2Value;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet2Details;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblErrorDetails;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblErrorValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblNetworkStatus;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblNetworkStatusValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblWBTwoValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblWBOneValue;
        private System.Windows.Forms.Timer timerReader;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet1TimeValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAntennaSet2TimeValue;
        private System.Windows.Forms.Timer timerNetworkConnection;
    }
}

