namespace WAP3
{
    partial class formReason
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
            this.txtTruckNo = new System.Windows.Forms.Label();
            this.lblTruckNo = new System.Windows.Forms.Label();
            this.cmbReasonName = new System.Windows.Forms.ComboBox();
            this.labelStage = new System.Windows.Forms.Label();
            this.txtStage = new System.Windows.Forms.Label();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtTruckNo
            // 
            this.txtTruckNo.ForeColor = System.Drawing.Color.White;
            this.txtTruckNo.Location = new System.Drawing.Point(75, 6);
            this.txtTruckNo.Name = "txtTruckNo";
            this.txtTruckNo.Size = new System.Drawing.Size(157, 15);
            this.txtTruckNo.Text = "Truck No";
            // 
            // lblTruckNo
            // 
            this.lblTruckNo.Location = new System.Drawing.Point(6, 6);
            this.lblTruckNo.Name = "lblTruckNo";
            this.lblTruckNo.Size = new System.Drawing.Size(64, 15);
            this.lblTruckNo.Text = "Truck No.";
            // 
            // cmbReasonName
            // 
            this.cmbReasonName.Location = new System.Drawing.Point(75, 45);
            this.cmbReasonName.Name = "cmbReasonName";
            this.cmbReasonName.Size = new System.Drawing.Size(157, 22);
            this.cmbReasonName.TabIndex = 115;
            // 
            // labelStage
            // 
            this.labelStage.Location = new System.Drawing.Point(6, 27);
            this.labelStage.Name = "labelStage";
            this.labelStage.Size = new System.Drawing.Size(64, 15);
            this.labelStage.Text = "Stage";
            // 
            // txtStage
            // 
            this.txtStage.ForeColor = System.Drawing.Color.White;
            this.txtStage.Location = new System.Drawing.Point(75, 27);
            this.txtStage.Name = "txtStage";
            this.txtStage.Size = new System.Drawing.Size(157, 15);
            this.txtStage.Text = "Stage";
            // 
            // lblReason
            // 
            this.lblReason.Location = new System.Drawing.Point(6, 48);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(52, 15);
            this.lblReason.Text = "Reason Name";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(75, 76);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(157, 76);
            this.txtRemarks.TabIndex = 122;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(171, 158);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 22);
            this.btnSave.TabIndex = 123;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblRemarks
            // 
            this.lblRemarks.Location = new System.Drawing.Point(6, 77);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(52, 15);
            this.lblRemarks.Text = "Remarks";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.Maroon;
            this.lblStatus.Location = new System.Drawing.Point(2, 183);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(235, 15);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // formReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblRemarks);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.txtStage);
            this.Controls.Add(this.labelStage);
            this.Controls.Add(this.cmbReasonName);
            this.Controls.Add(this.txtTruckNo);
            this.Controls.Add(this.lblTruckNo);
            this.Menu = this.mainMenu1;
            this.Name = "formReason";
            this.Text = "Delay Reason";
            this.Load += new System.EventHandler(this.formReason_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label txtTruckNo;
        private System.Windows.Forms.Label lblTruckNo;
        private System.Windows.Forms.ComboBox cmbReasonName;
        private System.Windows.Forms.Label labelStage;
        private System.Windows.Forms.Label txtStage;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.Label lblStatus;
    }
}