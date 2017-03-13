namespace WAP3
{
    partial class formTaskConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.txtOperationType = new System.Windows.Forms.TextBox();
            this.txtCommodity = new System.Windows.Forms.TextBox();
            this.txtSubTaskNo = new System.Windows.Forms.TextBox();
            this.txtTaskNo = new System.Windows.Forms.TextBox();
            this.lblOperationType = new System.Windows.Forms.Label();
            this.lblCommodity = new System.Windows.Forms.Label();
            this.lblSubTaskNo = new System.Windows.Forms.Label();
            this.lblTaskNo = new System.Windows.Forms.Label();
            this.btnRequestConfig = new System.Windows.Forms.Button();
            this.txtRefCode = new System.Windows.Forms.TextBox();
            this.lblRefCode = new System.Windows.Forms.Label();
            this.txtSubRefCode = new System.Windows.Forms.TextBox();
            this.lblSubRefCode = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.lblDestination = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOperationType
            // 
            this.txtOperationType.Location = new System.Drawing.Point(90, 52);
            this.txtOperationType.Name = "txtOperationType";
            this.txtOperationType.ReadOnly = true;
            this.txtOperationType.Size = new System.Drawing.Size(136, 21);
            this.txtOperationType.TabIndex = 29;
            // 
            // txtCommodity
            // 
            this.txtCommodity.Location = new System.Drawing.Point(90, 140);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.ReadOnly = true;
            this.txtCommodity.Size = new System.Drawing.Size(135, 21);
            this.txtCommodity.TabIndex = 28;
            // 
            // txtSubTaskNo
            // 
            this.txtSubTaskNo.Location = new System.Drawing.Point(90, 96);
            this.txtSubTaskNo.Name = "txtSubTaskNo";
            this.txtSubTaskNo.ReadOnly = true;
            this.txtSubTaskNo.Size = new System.Drawing.Size(135, 21);
            this.txtSubTaskNo.TabIndex = 27;
            // 
            // txtTaskNo
            // 
            this.txtTaskNo.Location = new System.Drawing.Point(91, 25);
            this.txtTaskNo.Name = "txtTaskNo";
            this.txtTaskNo.Size = new System.Drawing.Size(73, 21);
            this.txtTaskNo.TabIndex = 26;
            // 
            // lblOperationType
            // 
            this.lblOperationType.Location = new System.Drawing.Point(7, 53);
            this.lblOperationType.Name = "lblOperationType";
            this.lblOperationType.Size = new System.Drawing.Size(69, 20);
            this.lblOperationType.Text = "Op Type";
            // 
            // lblCommodity
            // 
            this.lblCommodity.Location = new System.Drawing.Point(7, 141);
            this.lblCommodity.Name = "lblCommodity";
            this.lblCommodity.Size = new System.Drawing.Size(69, 20);
            this.lblCommodity.Text = "Commodity";
            // 
            // lblSubTaskNo
            // 
            this.lblSubTaskNo.Location = new System.Drawing.Point(6, 97);
            this.lblSubTaskNo.Name = "lblSubTaskNo";
            this.lblSubTaskNo.Size = new System.Drawing.Size(78, 20);
            this.lblSubTaskNo.Text = "Sub Task No";
            // 
            // lblTaskNo
            // 
            this.lblTaskNo.Location = new System.Drawing.Point(7, 26);
            this.lblTaskNo.Name = "lblTaskNo";
            this.lblTaskNo.Size = new System.Drawing.Size(69, 20);
            this.lblTaskNo.Text = "Task No";
            // 
            // btnRequestConfig
            // 
            this.btnRequestConfig.BackColor = System.Drawing.Color.White;
            this.btnRequestConfig.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.btnRequestConfig.Location = new System.Drawing.Point(170, 25);
            this.btnRequestConfig.Name = "btnRequestConfig";
            this.btnRequestConfig.Size = new System.Drawing.Size(56, 21);
            this.btnRequestConfig.TabIndex = 34;
            this.btnRequestConfig.Text = "Request";
            this.btnRequestConfig.Click += new System.EventHandler(this.btnRequestConfig_Click);
            // 
            // txtRefCode
            // 
            this.txtRefCode.Location = new System.Drawing.Point(90, 74);
            this.txtRefCode.Name = "txtRefCode";
            this.txtRefCode.ReadOnly = true;
            this.txtRefCode.Size = new System.Drawing.Size(135, 21);
            this.txtRefCode.TabIndex = 40;
            // 
            // lblRefCode
            // 
            this.lblRefCode.Location = new System.Drawing.Point(6, 75);
            this.lblRefCode.Name = "lblRefCode";
            this.lblRefCode.Size = new System.Drawing.Size(78, 20);
            this.lblRefCode.Text = "Ref Code";
            // 
            // txtSubRefCode
            // 
            this.txtSubRefCode.Location = new System.Drawing.Point(90, 118);
            this.txtSubRefCode.Name = "txtSubRefCode";
            this.txtSubRefCode.ReadOnly = true;
            this.txtSubRefCode.Size = new System.Drawing.Size(135, 21);
            this.txtSubRefCode.TabIndex = 43;
            // 
            // lblSubRefCode
            // 
            this.lblSubRefCode.Location = new System.Drawing.Point(7, 119);
            this.lblSubRefCode.Name = "lblSubRefCode";
            this.lblSubRefCode.Size = new System.Drawing.Size(77, 20);
            this.lblSubRefCode.Text = "SubRefCode";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(90, 162);
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(135, 21);
            this.txtSource.TabIndex = 46;
            // 
            // lblSource
            // 
            this.lblSource.Location = new System.Drawing.Point(7, 163);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(69, 20);
            this.lblSource.Text = "Source";
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(90, 184);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.ReadOnly = true;
            this.txtDestination.Size = new System.Drawing.Size(135, 21);
            this.txtDestination.TabIndex = 49;
            // 
            // lblDestination
            // 
            this.lblDestination.Location = new System.Drawing.Point(7, 185);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(69, 20);
            this.lblDestination.Text = "Destination";
            // 
            // formTaskConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.txtSubRefCode);
            this.Controls.Add(this.lblSubRefCode);
            this.Controls.Add(this.txtRefCode);
            this.Controls.Add(this.lblRefCode);
            this.Controls.Add(this.btnRequestConfig);
            this.Controls.Add(this.txtOperationType);
            this.Controls.Add(this.txtCommodity);
            this.Controls.Add(this.txtSubTaskNo);
            this.Controls.Add(this.txtTaskNo);
            this.Controls.Add(this.lblOperationType);
            this.Controls.Add(this.lblCommodity);
            this.Controls.Add(this.lblSubTaskNo);
            this.Controls.Add(this.lblTaskNo);
            this.Menu = this.mainMenu1;
            this.Name = "formTaskConfig";
            this.Text = "Task Configuration";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtOperationType;
        private System.Windows.Forms.TextBox txtCommodity;
        private System.Windows.Forms.TextBox txtSubTaskNo;
        private System.Windows.Forms.TextBox txtTaskNo;
        private System.Windows.Forms.Label lblOperationType;
        private System.Windows.Forms.Label lblCommodity;
        private System.Windows.Forms.Label lblSubTaskNo;
        private System.Windows.Forms.Label lblTaskNo;
        private System.Windows.Forms.Button btnRequestConfig;
        private System.Windows.Forms.TextBox txtRefCode;
        private System.Windows.Forms.Label lblRefCode;
        private System.Windows.Forms.TextBox txtSubRefCode;
        private System.Windows.Forms.Label lblSubRefCode;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label lblDestination;
    }
}