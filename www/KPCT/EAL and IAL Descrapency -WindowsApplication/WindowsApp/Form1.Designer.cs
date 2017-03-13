namespace WindowsApp
{
    partial class FileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileForm));
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.EALlabel = new System.Windows.Forms.Label();
            this.Browsebutton = new System.Windows.Forms.Button();
            this.dataGridViewEAL = new System.Windows.Forms.DataGridView();
            this.Container = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyorFull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblVesRef = new System.Windows.Forms.Label();
            this.lblVesRefVal = new System.Windows.Forms.Label();
            this.dataGridViewDB = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvNotinYard = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agent1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgNotinEAL = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agent2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POD1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blMatching = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNotinYardCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNotinEALcount = new System.Windows.Forms.Label();
            this.dgvPODException = new System.Windows.Forms.DataGridView();
            this.Container1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueinEAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueinN4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Weight1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblPODException = new System.Windows.Forms.Label();
            this.dgvStatusException = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatusException = new System.Windows.Forms.Label();
            this.dgvgrwtException = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblgrwtException = new System.Windows.Forms.Label();
            this.dgvagentException = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblagentException = new System.Windows.Forms.Label();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.CrystalReport11 = new WindowsApp.CrystalReport1();
            this.btnCreatefile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEAL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotinYard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotinEAL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPODException)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatusException)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvgrwtException)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvagentException)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.SystemColors.Menu;
            this.txtFilePath.Enabled = false;
            this.txtFilePath.ForeColor = System.Drawing.Color.Red;
            this.txtFilePath.Location = new System.Drawing.Point(72, 26);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(399, 20);
            this.txtFilePath.TabIndex = 0;
            // 
            // EALlabel
            // 
            this.EALlabel.AutoSize = true;
            this.EALlabel.Location = new System.Drawing.Point(6, 29);
            this.EALlabel.Name = "EALlabel";
            this.EALlabel.Size = new System.Drawing.Size(52, 13);
            this.EALlabel.TabIndex = 1;
            this.EALlabel.Text = "EAL File :";
            // 
            // Browsebutton
            // 
            this.Browsebutton.Location = new System.Drawing.Point(467, 25);
            this.Browsebutton.Name = "Browsebutton";
            this.Browsebutton.Size = new System.Drawing.Size(60, 23);
            this.Browsebutton.TabIndex = 2;
            this.Browsebutton.Text = "Browse...";
            this.Browsebutton.UseVisualStyleBackColor = true;
            this.Browsebutton.Click += new System.EventHandler(this.Browsebutton_Click);
            // 
            // dataGridViewEAL
            // 
            this.dataGridViewEAL.AllowUserToAddRows = false;
            this.dataGridViewEAL.AllowUserToDeleteRows = false;
            this.dataGridViewEAL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEAL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Container,
            this.Weight,
            this.ISO,
            this.EmptyorFull,
            this.POD});
            this.dataGridViewEAL.Location = new System.Drawing.Point(558, 447);
            this.dataGridViewEAL.Name = "dataGridViewEAL";
            this.dataGridViewEAL.Size = new System.Drawing.Size(543, 76);
            this.dataGridViewEAL.TabIndex = 3;
            this.dataGridViewEAL.Visible = false;
            // 
            // Container
            // 
            this.Container.HeaderText = "Container";
            this.Container.Name = "Container";
            // 
            // Weight
            // 
            this.Weight.HeaderText = "Weight";
            this.Weight.Name = "Weight";
            // 
            // ISO
            // 
            this.ISO.HeaderText = "ISO";
            this.ISO.Name = "ISO";
            // 
            // EmptyorFull
            // 
            this.EmptyorFull.HeaderText = "Empty or Full";
            this.EmptyorFull.Name = "EmptyorFull";
            // 
            // POD
            // 
            this.POD.HeaderText = "POD";
            this.POD.Name = "POD";
            // 
            // lblVesRef
            // 
            this.lblVesRef.AutoSize = true;
            this.lblVesRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVesRef.Location = new System.Drawing.Point(9, 56);
            this.lblVesRef.Name = "lblVesRef";
            this.lblVesRef.Size = new System.Drawing.Size(115, 13);
            this.lblVesRef.TabIndex = 4;
            this.lblVesRef.Text = "Vessel Reference :";
            // 
            // lblVesRefVal
            // 
            this.lblVesRefVal.AutoSize = true;
            this.lblVesRefVal.Location = new System.Drawing.Point(122, 56);
            this.lblVesRefVal.Name = "lblVesRefVal";
            this.lblVesRefVal.Size = new System.Drawing.Size(16, 13);
            this.lblVesRefVal.TabIndex = 5;
            this.lblVesRefVal.Text = "...";
            // 
            // dataGridViewDB
            // 
            this.dataGridViewDB.AllowUserToAddRows = false;
            this.dataGridViewDB.AllowUserToDeleteRows = false;
            this.dataGridViewDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dataGridViewDB.Location = new System.Drawing.Point(558, 447);
            this.dataGridViewDB.Name = "dataGridViewDB";
            this.dataGridViewDB.Size = new System.Drawing.Size(543, 85);
            this.dataGridViewDB.TabIndex = 6;
            this.dataGridViewDB.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Container";
            this.dataGridViewTextBoxColumn1.HeaderText = "Container";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Weight";
            this.dataGridViewTextBoxColumn2.HeaderText = "Weight";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "ISO";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Line";
            this.dataGridViewTextBoxColumn4.HeaderText = "LineOP";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "POD";
            this.dataGridViewTextBoxColumn5.HeaderText = "POD";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dgvNotinYard
            // 
            this.dgvNotinYard.AllowUserToAddRows = false;
            this.dgvNotinYard.AllowUserToDeleteRows = false;
            this.dgvNotinYard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNotinYard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn7,
            this.Agent1,
            this.dataGridViewTextBoxColumn10});
            this.dgvNotinYard.Location = new System.Drawing.Point(9, 86);
            this.dgvNotinYard.Name = "dgvNotinYard";
            this.dgvNotinYard.Size = new System.Drawing.Size(543, 120);
            this.dgvNotinYard.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Container";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Status";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "ISO Code";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Gr. Weight";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // Agent1
            // 
            this.Agent1.HeaderText = "Agent";
            this.Agent1.Name = "Agent1";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "POD";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dgNotinEAL
            // 
            this.dgNotinEAL.AllowUserToAddRows = false;
            this.dgNotinEAL.AllowUserToDeleteRows = false;
            this.dgNotinEAL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNotinEAL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.Column1,
            this.dataGridViewTextBoxColumn12,
            this.Agent2,
            this.POD1});
            this.dgNotinEAL.Location = new System.Drawing.Point(558, 86);
            this.dgNotinEAL.Name = "dgNotinEAL";
            this.dgNotinEAL.ReadOnly = true;
            this.dgNotinEAL.Size = new System.Drawing.Size(543, 120);
            this.dgNotinEAL.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Container";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Status";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Weight";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // Agent2
            // 
            this.Agent2.HeaderText = "Agent";
            this.Agent2.Name = "Agent2";
            this.Agent2.ReadOnly = true;
            // 
            // POD1
            // 
            this.POD1.HeaderText = "POD";
            this.POD1.Name = "POD1";
            this.POD1.ReadOnly = true;
            // 
            // blMatching
            // 
            this.blMatching.AutoSize = true;
            this.blMatching.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blMatching.Location = new System.Drawing.Point(333, 70);
            this.blMatching.Name = "blMatching";
            this.blMatching.Size = new System.Drawing.Size(199, 13);
            this.blMatching.TabIndex = 9;
            this.blMatching.Text = "Container in EAL and Not in Yard:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(896, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Containers in Yard Not in EAL:";
            // 
            // lblNotinYardCount
            // 
            this.lblNotinYardCount.AutoSize = true;
            this.lblNotinYardCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotinYardCount.Location = new System.Drawing.Point(534, 69);
            this.lblNotinYardCount.Name = "lblNotinYardCount";
            this.lblNotinYardCount.Size = new System.Drawing.Size(0, 13);
            this.lblNotinYardCount.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(635, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 12;
            // 
            // lblNotinEALcount
            // 
            this.lblNotinEALcount.AutoSize = true;
            this.lblNotinEALcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotinEALcount.Location = new System.Drawing.Point(1081, 70);
            this.lblNotinEALcount.Name = "lblNotinEALcount";
            this.lblNotinEALcount.Size = new System.Drawing.Size(0, 13);
            this.lblNotinEALcount.TabIndex = 13;
            // 
            // dgvPODException
            // 
            this.dgvPODException.AllowUserToAddRows = false;
            this.dgvPODException.AllowUserToDeleteRows = false;
            this.dgvPODException.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPODException.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Container1,
            this.ValueinEAL,
            this.ValueinN4,
            this.Agent,
            this.Weight1});
            this.dgvPODException.Location = new System.Drawing.Point(9, 224);
            this.dgvPODException.Name = "dgvPODException";
            this.dgvPODException.Size = new System.Drawing.Size(543, 101);
            this.dgvPODException.TabIndex = 14;
            // 
            // Container1
            // 
            this.Container1.HeaderText = "Container";
            this.Container1.Name = "Container1";
            // 
            // ValueinEAL
            // 
            this.ValueinEAL.HeaderText = "Value In EAL";
            this.ValueinEAL.Name = "ValueinEAL";
            // 
            // ValueinN4
            // 
            this.ValueinN4.HeaderText = "Value in N4";
            this.ValueinN4.Name = "ValueinN4";
            // 
            // Agent
            // 
            this.Agent.HeaderText = "Agent";
            this.Agent.Name = "Agent";
            // 
            // Weight1
            // 
            this.Weight1.HeaderText = "Weight";
            this.Weight1.Name = "Weight1";
            // 
            // lblPODException
            // 
            this.lblPODException.AutoSize = true;
            this.lblPODException.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPODException.Location = new System.Drawing.Point(426, 209);
            this.lblPODException.Name = "lblPODException";
            this.lblPODException.Size = new System.Drawing.Size(101, 13);
            this.lblPODException.TabIndex = 15;
            this.lblPODException.Text = "POD Exception :";
            // 
            // dgvStatusException
            // 
            this.dgvStatusException.AllowUserToAddRows = false;
            this.dgvStatusException.AllowUserToDeleteRows = false;
            this.dgvStatusException.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatusException.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20});
            this.dgvStatusException.Location = new System.Drawing.Point(9, 343);
            this.dgvStatusException.Name = "dgvStatusException";
            this.dgvStatusException.Size = new System.Drawing.Size(543, 101);
            this.dgvStatusException.TabIndex = 16;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.HeaderText = "Container";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.HeaderText = "Value In EAL";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.HeaderText = "Value in N4";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.HeaderText = "Agent";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.HeaderText = "Weight";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            // 
            // lblStatusException
            // 
            this.lblStatusException.AutoSize = true;
            this.lblStatusException.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusException.Location = new System.Drawing.Point(416, 328);
            this.lblStatusException.Name = "lblStatusException";
            this.lblStatusException.Size = new System.Drawing.Size(111, 13);
            this.lblStatusException.TabIndex = 17;
            this.lblStatusException.Text = "Status Exception :";
            // 
            // dgvgrwtException
            // 
            this.dgvgrwtException.AllowUserToAddRows = false;
            this.dgvgrwtException.AllowUserToDeleteRows = false;
            this.dgvgrwtException.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvgrwtException.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn25});
            this.dgvgrwtException.Location = new System.Drawing.Point(558, 224);
            this.dgvgrwtException.Name = "dgvgrwtException";
            this.dgvgrwtException.Size = new System.Drawing.Size(543, 101);
            this.dgvgrwtException.TabIndex = 18;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.HeaderText = "Container";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.HeaderText = "Value In EAL";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.HeaderText = "Value in N4";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.HeaderText = "Agent";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.HeaderText = "Status";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            // 
            // lblgrwtException
            // 
            this.lblgrwtException.AutoSize = true;
            this.lblgrwtException.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblgrwtException.Location = new System.Drawing.Point(945, 209);
            this.lblgrwtException.Name = "lblgrwtException";
            this.lblgrwtException.Size = new System.Drawing.Size(136, 13);
            this.lblgrwtException.TabIndex = 19;
            this.lblgrwtException.Text = "Gr. Weight Exception :";
            // 
            // dgvagentException
            // 
            this.dgvagentException.AllowUserToAddRows = false;
            this.dgvagentException.AllowUserToDeleteRows = false;
            this.dgvagentException.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvagentException.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn30});
            this.dgvagentException.Location = new System.Drawing.Point(558, 342);
            this.dgvagentException.Name = "dgvagentException";
            this.dgvagentException.Size = new System.Drawing.Size(543, 101);
            this.dgvagentException.TabIndex = 20;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.HeaderText = "Container";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.HeaderText = "Value In EAL";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.HeaderText = "Value in N4";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.HeaderText = "Weight";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.HeaderText = "Status";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            // 
            // lblagentException
            // 
            this.lblagentException.AutoSize = true;
            this.lblagentException.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblagentException.Location = new System.Drawing.Point(965, 326);
            this.lblagentException.Name = "lblagentException";
            this.lblagentException.Size = new System.Drawing.Size(108, 13);
            this.lblagentException.TabIndex = 21;
            this.lblagentException.Text = "Agent Exception :";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 72);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.CrystalReport11;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1233, 451);
            this.crystalReportViewer1.TabIndex = 22;
            this.crystalReportViewer1.Visible = false;
            // 
            // btnCreatefile
            // 
            this.btnCreatefile.Location = new System.Drawing.Point(1070, 29);
            this.btnCreatefile.Name = "btnCreatefile";
            this.btnCreatefile.Size = new System.Drawing.Size(75, 23);
            this.btnCreatefile.TabIndex = 23;
            this.btnCreatefile.Text = "Create File";
            this.btnCreatefile.UseVisualStyleBackColor = true;
            // 
            // FileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1270, 535);
            this.Controls.Add(this.btnCreatefile);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.lblagentException);
            this.Controls.Add(this.dgvagentException);
            this.Controls.Add(this.lblgrwtException);
            this.Controls.Add(this.dgvgrwtException);
            this.Controls.Add(this.lblStatusException);
            this.Controls.Add(this.dgvStatusException);
            this.Controls.Add(this.lblPODException);
            this.Controls.Add(this.dgvPODException);
            this.Controls.Add(this.lblNotinEALcount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNotinYardCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blMatching);
            this.Controls.Add(this.dataGridViewDB);
            this.Controls.Add(this.lblVesRefVal);
            this.Controls.Add(this.lblVesRef);
            this.Controls.Add(this.dataGridViewEAL);
            this.Controls.Add(this.Browsebutton);
            this.Controls.Add(this.EALlabel);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.dgNotinEAL);
            this.Controls.Add(this.dgvNotinYard);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Krishnapatnam Container Terminal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FileForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEAL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotinYard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotinEAL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPODException)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatusException)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvgrwtException)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvagentException)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label EALlabel;
        private System.Windows.Forms.Button Browsebutton;
        private System.Windows.Forms.DataGridView dataGridViewEAL;
        private System.Windows.Forms.Label lblVesRef;
        private System.Windows.Forms.Label lblVesRefVal;
        private System.Windows.Forms.DataGridView dataGridViewDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridView dgvNotinYard;
        private System.Windows.Forms.DataGridViewTextBoxColumn Container;
        private System.Windows.Forms.DataGridViewTextBoxColumn Weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISO;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyorFull;
        private System.Windows.Forms.DataGridViewTextBoxColumn POD;
        private System.Windows.Forms.DataGridView dgNotinEAL;
        private System.Windows.Forms.Label blMatching;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNotinYardCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNotinEALcount;
        private System.Windows.Forms.DataGridView dgvPODException;
        private System.Windows.Forms.Label lblPODException;
        private System.Windows.Forms.DataGridViewTextBoxColumn Container1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueinEAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueinN4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Agent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Weight1;
        private System.Windows.Forms.DataGridView dgvStatusException;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.Label lblStatusException;
        private System.Windows.Forms.DataGridView dgvgrwtException;
        private System.Windows.Forms.Label lblgrwtException;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridView dgvagentException;
        private System.Windows.Forms.Label lblagentException;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Agent1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Agent2;
        private System.Windows.Forms.DataGridViewTextBoxColumn POD1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private CrystalReport1 CrystalReport11;
        private System.Windows.Forms.Button btnCreatefile;
    }
}

